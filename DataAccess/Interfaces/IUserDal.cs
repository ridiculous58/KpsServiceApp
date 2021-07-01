using Entities.Dtos;
using Infrastructure.DataAccess;
using Infrastructure.Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IUserDal : IEntityRepository<User> 
    {
        List<UserOperationClaimDto> GetClaims(User user);
    }
}
