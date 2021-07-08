using Infrastructure.Entities.Concrete;
using Infrastructure.Exceptions;
using Service.UserControlService.KpsServiceAdapter.Extensions;
using Service.UserControlService.KpsServiceAdapter.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Service.UserControlService.KpsServiceAdapter
{
    public class KpsServiceAdapter : IUserControlService
    {
        KpsRequestHelper _kpsRequestHelper;
        public KpsServiceAdapter(KpsRequestHelper kpsRequestHelper)
        {
            _kpsRequestHelper = kpsRequestHelper;
        }
        public User FillUserInfo(User user)
        {
            return GetUserInfo(user);
        }

        private User GetUserInfo(User user)
        {
            var tcNo = long.Parse(user.TcNo);
            var result = _kpsRequestHelper.BilesikKisiSorgula(tcNo, KpsConfigurationHelper.UserName, KpsConfigurationHelper.Password);

            var maviKartNode = result.GetElementsByTagName("MaviKartliKisiKutukleri");
            var tcNode = result.GetElementsByTagName("TCVatandasiKisiKutukleri");
            var yabanciNode = result.GetElementsByTagName("YabanciKisiKutukleri");

            if ((maviKartNode.Count > 0 && !maviKartNode[0].InnerXml.HasEmpty()) ||
                (tcNode.Count > 0 && !tcNode[0].InnerXml.HasEmpty()) ||
                (yabanciNode.Count > 0 && !yabanciNode[0].InnerXml.HasEmpty())
                )
            {
                user.FirstName = result.GetElementsByTagName("Ad")[0].InnerText;
                user.LastName = result.GetElementsByTagName("Soyad")[0].InnerText;
            }
            else
            {
                throw new CustomException("Tc Kimlik Numarasi Geçerli Değil");
            }
            return user;
        }
    }
}
