using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Moustafa.DAL.Models;

namespace Company.Moustafa.BLL.Interfaces
{
    public interface IEmployeeRepository :IGenaricRepository<Employee>
    {
       //IEnumerable<Employee> GetAll();
        
       // Employee? Get(int id);

       // int Add(Employee model);
       // int Update(Employee model);
       // int Delete(Employee model);

    }
}
