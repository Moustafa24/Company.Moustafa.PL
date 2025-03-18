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
    public class GenaricRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _Context;
        public GenaricRepository(CompanyDbContext Context)
        {
            _Context= Context;
        }

        public IEnumerable<T> GetAll()
        {
            if(typeof(T)==typeof(Employee))
            {
                return (IEnumerable<T>)_Context.Employees.Include(E => E.Department).ToList(); 
            }
            return _Context.Set<T>().ToList();
        }

        public T? Get(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return _Context.Employees.Include(E => E.Department).FirstOrDefault(E => E.Id == id) as T;
            }
            return _Context.Set<T>().Find(id);
        }

        public int Add(T model)
        {
           _Context.Set<T>().Add(model);

            return _Context.SaveChanges();
        }


        public int Update(T model)
        {
            _Context.Set<T>().Update(model);
            return _Context.SaveChanges();

        }

        public int Delete(T model)
        {
            _Context.Set<T>().Remove(model);
            return _Context.SaveChanges();

        }



    }
}
