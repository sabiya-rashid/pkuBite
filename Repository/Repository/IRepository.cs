using pkuBite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public interface IRepository<T>
    {
        ICollection<T> GetEntities();
        T GetEntity(int id);
        T EntityExists(int id);
        bool CreateEntity(T entity);
        bool Save();
        bool DeleteEntity(T entity);
        bool UpdateEntity(T entity);  
        User GetUser(string username); 
    }
}
