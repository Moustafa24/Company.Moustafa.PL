using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Moustafa.DAL.Models;

namespace Company.Moustafa.BLL.Interfaces
{
    public interface IGenaricRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();

        T? Get(int id);

        int Add(T model);

        int Update(T model);

        int Delete(T model);

    }
   
}
