using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Moustafa.DAL.Models;


namespace Company.Moustafa.BLL.Interfaces
{
    public interface IDepartmentRepository
    {

      IEnumerable<Department>  GetAll();

        Department? Get(int id);

        int Add(Department model);

        int Update(Department model);

        int Delete(Department model);




    }
}
