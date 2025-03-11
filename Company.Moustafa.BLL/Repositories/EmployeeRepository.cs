using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Moustafa.BLL.Interfaces;
using Company.Moustafa.DAL.Data.Context;
using Company.Moustafa.DAL.Models;

namespace Company.Moustafa.BLL.Repositories
{
    public class EmployeeRepository :GenaricRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(CompanyDbContext context) : base(context) { }

    }
}
