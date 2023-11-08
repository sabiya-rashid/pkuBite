using DbContext;
using Microsoft.EntityFrameworkCore;
using Models.Base;
using pkuBite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(DataContext context)
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
            _context.Remove(entity);
            return Save();
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

        public User GetUser(string username)
        {
           return  _context.Users.Where(u => u.Username== username).FirstOrDefault();            
        }
    }
}
