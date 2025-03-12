using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.BLL.Interfaces;
using Company.Moustafa.BLL.Interfaces;
using Company.Moustafa.DAL.Data.Context;
using Company.Moustafa.DAL.Models;

namespace Company.Moustafa.BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private  readonly CompanyDbContext _Context;

        public DepartmentRepository(CompanyDbContext companyDbContext)
        {
            _Context = companyDbContext;
        }

        public IEnumerable<Department> GetAll()
        {
                return _Context.Departments.ToList();
        
        }

        public Department? Get(int id)
        {
            return _Context.Departments.Find(id);
        }

        public int Add(Department model)
        {
            _Context.Departments.Add(model);
            return _Context.SaveChanges();

        }

        public int Delete(Department model)
        {
            _Context.Departments.Remove(model);
            return _Context.SaveChanges();
        }

        public int Update(Department model)
        {
            _Context.Departments.Update(model);
            return _Context.SaveChanges();
        }
    }
}
