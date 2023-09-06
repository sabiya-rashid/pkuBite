using pkuBite.Services.IServices;
using pkuBite.Repository.Repositories;

using pkuBite.Common.DTO;
using FluentValidation;
using Common.Validations;
using Services.Services;

namespace pkuBite;

public static class DependecnyInjectionContainer
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICategory, CategoryService>();
        services.AddScoped<IItem, ItemService>();
        services.AddScoped<ISubCategory, SubCategoryService>();
        services.AddScoped(typeof(IFeatures<>), typeof(FeaturesRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddTransient<IValidator<LoginDTO>, LoginValidation>();
        services.AddTransient<IValidator<subCategoryDTO>, SubCategoryValidation>();

        return services;
    }
}

