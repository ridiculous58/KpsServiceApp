using Entities.Dtos;
using Infrastructure.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserService
{
    public interface IUserService
    {
        List<UserOperationClaimDto> GetClaims(User user);
        void Add(User user);
        User GetByMail(string email);
    }
}
