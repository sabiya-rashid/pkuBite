using System;
using pkuBite.Models.Models;

namespace pkuBite.Services.IServices;

	public interface IRepository<TEntity> 
	{
        IQueryable<TEntity> GetAll();

        TEntity GetById(int id);

        bool Create(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TEntity entity);

        bool Save();
    }

