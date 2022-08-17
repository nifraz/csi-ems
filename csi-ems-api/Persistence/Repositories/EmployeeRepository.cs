using Csi.Ems.Api.Core.Domain;
using Csi.Ems.Api.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Csi.Ems.Api.Persistence.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmsDbContext context) : base(context)
        {

        }

        public override Task<bool> IsExistingAsync(Employee entity)
        {
            return IsExistingAsync(e => e.Id == entity.Id);
        }
    }
}
