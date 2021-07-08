using Infrastructure.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Service.UserControlService
{
    public interface IUserControlService
    {
        User FillUserInfo(User user);
    }
}
