using pkuBite.Services.IServices;
using pkuBite.Models.Models;

namespace pkuBite.Repository.Repositories;

public class FeaturesRepository<T> : IFeatures<T> where T : Base
{ 
    public IQueryable<T> Filter(IQueryable<T> query, string filter)
    {
        if (!string.IsNullOrWhiteSpace(filter))
        {
            query = query.Where(category => category.Name.Contains(filter));
        }
        return query;
    }

    public IQueryable<T> Pagination(IQueryable<T> query, int pageNo, int PageSize)
    {
        var skip = (pageNo - 1) * PageSize;
        return query.Skip(skip).Take(PageSize);
    }

    public IQueryable<T> Sort(IQueryable<T> query, string sort)
    {
        if (!string.IsNullOrWhiteSpace(sort))
        {
            switch (sort.ToLower())
            {
                case "name":
                    return query.OrderBy(n => n.Name);
                case "Id":
                    return query.OrderBy(n => n.Id);
            };
        }
        return query;
    }
}

