using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.BLL.Interfaces;

namespace Company.Moustafa.BLL.Interfaces
{
    
     public interface IUnitOfWork : IAsyncDisposable
    {
    
        public IEmployeeRepository EmployeeRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }
        Task<int> CompleteAsync();
    }

    
}
