using Microsoft.EntityFrameworkCore;
using pkuBite.Data;
using pkuBite.Interfaces;
using pkuBite.Models;

namespace pkuBite.Repositories
{
    public class CommonRepository<T> : ICommonRepository<T> where T : Base
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _dbSet;
        public CommonRepository(DataContext context)
        {
                _context = context;
                _dbSet = context.Set<T>();
        }
        public bool CreateEntity(T entity)
        {
            _context.Add(entity);
            //_context.SaveChanges();
            return Save();
        }

        public bool DeleteEntity(T entity)
        {
            _dbSet.Remove(entity);
            return true;
        }
        public T EntityExists(int id)
        {
            return _dbSet.Where(e => e.Id == id).FirstOrDefault();
        }
        
        public ICollection<T> GetEntities()
        {
            return _dbSet.ToList<T>();
        }

        public T GetEntity(int id)
        {
            return _dbSet.Where(e => e.Id == id).FirstOrDefault();            
        }

        public bool Save()
        {
           var saved = _context.SaveChanges();
           return saved > 0 ? true : false;
        }

        public bool UpdateEntity(T entity)
        {
            return Save();
        }   
        
    }
}
