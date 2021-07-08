using DataAccess.EntityFramework.Contexts;
using DataAccess.Interfaces;
using Infrastructure.DataAccess.EntityFramework;
using Infrastructure.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.Concrete
{
    public class OperationClaimDal : EfEntityRepositoryBase<OperationClaim, KpsServiceAppContext>, IOperationClaimDal
    {
    }
}
