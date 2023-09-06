    using Microsoft.EntityFrameworkCore;
    using pkuBite.Data.Data;
    using pkuBite.Models.Models;
    using pkuBite.Services.IServices;


    namespace pkuBite.Repository.Repositories;

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Base
    {
            private readonly DataContext _db;
             private readonly DbSet<TEntity> _dbSet;

            public Repository(DataContext db)
            {
                _dbSet = db.Set<TEntity>();
                _db = db;
            }

            public bool Create(TEntity entity)
            {
                _dbSet.Add(entity);
                return Save();
            }

            public bool Delete(TEntity entity)
            {
                 _dbSet.Remove(entity);
                 return Save();
             }
        
            public IQueryable<TEntity> GetAll()
            {
        
                return (IQueryable<TEntity>)_dbSet;
            
            }

            public TEntity GetById(int id)
            {
                var entity = _dbSet.Find(id);
                return entity;
            }

            public bool Save()
            {
                var saved = _db.SaveChanges();
                return saved > 0 ? true : false;
            }

            public bool Update(TEntity entity)
            {
                    _dbSet.Update(entity);
                    return Save();
            }

}



