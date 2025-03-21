using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.BLL.Interfaces;
using Company.Moustafa.BLL.Interfaces;
using Company.Moustafa.BLL.Repositories;
using Company.Moustafa.DAL.Data.Context;

namespace Company.Moustafa.BLL
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly CompanyDbContext _context;

        public IEmployeeRepository EmployeeRepository { get; } // NULL

        public IDepartmentRepository DepartmentRepository { get; } // NULL

        public UnitOfWork(CompanyDbContext context) // ASK CLR to Create Object from CompanyDbContext
        {
            _context = context;
            EmployeeRepository = new EmployeeRepository(_context);
            DepartmentRepository = new DepartmentRepository(_context);
        }

        public async Task<int> CompleteAsync()

        { return await _context.SaveChangesAsync(); }

        public async ValueTask DisposeAsync()

        { await _context.DisposeAsync(); }
    }
}
