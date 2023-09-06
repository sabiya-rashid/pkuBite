using System;
using pkuBite.Models.Models;

namespace pkuBite.Services.IServices;

	public interface IFeatures<T> 
    {
        IQueryable<T> Filter(IQueryable<T> query, string search);
        IQueryable<T> Sort(IQueryable<T> query, string sort);
        IQueryable<T> Pagination(IQueryable<T> query, int pageNo, int PageSize);
    }

