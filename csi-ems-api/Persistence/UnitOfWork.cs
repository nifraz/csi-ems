using Csi.Ems.Api.Core;
using Csi.Ems.Api.Core.Repositories;
using Csi.Ems.Api.Persistence.Repositories;
using System.Threading.Tasks;

namespace Csi.Ems.Api.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmsDbContext context;

        public UnitOfWork(EmsDbContext context)
        {
            this.context = context;

            Employees = new EmployeeRepository(this.context);
        }

        public IEmployeeRepository Employees { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
