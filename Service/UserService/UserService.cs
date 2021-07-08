using DataAccess.Interfaces;
using Entities.Dtos;
using Infrastructure.Entities.Concrete;
using Infrastructure.Utilities.BusinessLogic;
using Infrastructure.Utilities.Results;
using Service.ServiceAspect.Autofac;
using Service.UserControlService;
using Service.UserControlService.KpsServiceAdapter.Entities;
using Service.UserControlService.KpsServiceAdapter.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;
       
        public UserService(IUserDal userDal)
        {
            
            _userDal = userDal;
        }
        public List<UserOperationClaimDto> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public void Add(User user)
        {
            
            _userDal.Add(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

    }
}
