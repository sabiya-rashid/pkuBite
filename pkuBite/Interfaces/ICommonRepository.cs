using pkuBite.Models;

namespace pkuBite.Interfaces
{
    public interface ICommonRepository<T>
    {
        ICollection<T> GetEntities();
        T GetEntity(int id);
        T EntityExists(int id);
        bool CreateEntity(T entity);
        bool Save();
        bool DeleteEntity(T entity);
        bool UpdateEntity(T entity);
    }
}
