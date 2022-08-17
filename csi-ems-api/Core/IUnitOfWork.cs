using Csi.Ems.Api.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Csi.Ems.Api.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }

        Task<int> CompleteAsync();
    }
}
