using DataAccess.EntityFramework.Contexts;
using DataAccess.Interfaces;
using Entities.Dtos;
using Infrastructure.DataAccess.EntityFramework;
using Infrastructure.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.Concrete
{
    public class UserDal : EfEntityRepositoryBase<User, KpsServiceAppContext>, IUserDal
    {
        public List<UserOperationClaimDto> GetClaims(User user)
        {
            using (var context = new KpsServiceAppContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new UserOperationClaimDto { Id = operationClaim.Id, Name = operationClaim.Name };
                
                return result.ToList();
            }
        }
    }
}
