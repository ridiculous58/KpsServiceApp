using Service.UserControlService;
using Service.UserControlService.KpsServiceAdapter;
using Service.UserControlService.KpsServiceAdapter.Extensions;
using System;
using System.ServiceModel.Channels;

namespace Executer
{
    class Program
    {
        static void Main(string[] args)
        {
            IUserControlService userControlService = new KpsServiceAdapter(new Service.UserControlService.KpsServiceAdapter.Helpers.KpsRequestHelper());

            var result = userControlService.CheckUserValid(15702033348);

            Console.WriteLine(result.GetElementsByTagName("TCVatandasiKisiKutukleri"));

			var maviKartNode = result.GetElementsByTagName("MaviKartliKisiKutukleri");
			var tcNode = result.GetElementsByTagName("TCVatandasiKisiKutukleri");
			var yabanciNode = result.GetElementsByTagName("YabanciKisiKutukleri");

			if ((maviKartNode.Count > 0 && !maviKartNode[0].InnerXml.HasEmpty()) ||
				(tcNode.Count > 0 && !tcNode[0].InnerXml.HasEmpty()) ||
				(yabanciNode.Count > 0 && !yabanciNode[0].InnerXml.HasEmpty())
				)
			{
				string ad = result.GetElementsByTagName("Ad")[0].InnerText;
				string soyad = result.GetElementsByTagName("Soyad")[0].InnerText;
				Console.WriteLine(string.Format("{0}-{1} {2}", 15702033348, ad, soyad));
				Console.WriteLine("---------------------\n\n");
			}
			else
			{
				Console.WriteLine("Kişi Bulunamadı");
				Console.WriteLine("---------------------\n\n");
			}

		}
	}
}
