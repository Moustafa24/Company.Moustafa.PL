using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Moustafa.BLL.Interfaces;
using Company.Moustafa.DAL.Data.Context;
using Company.Moustafa.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Moustafa.BLL.Repositories
{
    public class EmployeeRepository :GenaricRepository<Employee>, IEmployeeRepository
    {
       private readonly CompanyDbContext _Cotext;
        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
            _Cotext = context;
        }

       

        public List<Employee> GetByName(string name)
        {
           return _Cotext.Employees.Include(E=>E.Department).Where(E=>E.Name.ToLower().Contains(name.ToLower())).ToList(); 
        }
    }
}
