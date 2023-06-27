using Common.Application._Utilities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Categories.Create;
using Shop.Domain.CategoryAgg.Services;
using Shop.Infrastructure;
using Shop.Presentation.Facade;
using System.Reflection;
using Shop.Query.Categories.GetById;
using Shop.Application.Users;
using Shop.Domain.UserAgg.Services;
using Shop.Application.Categories;
using Shop.Application.Products;
using Shop.Application.Sellers;
using Shop.Domain.ProductAgg.Services;
using Shop.Domain.SellerAgg.Services;

namespace Shop.Config;

public static class ShopBootstrapper
{
    public static void RegisterShopDependency(this IServiceCollection services, string connectionString)
    {
        InfrastructureBootstrapper.Init(services, connectionString);
        //services.AddMediatR(typeof(Directories).Assembly);
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(CreateCategoryCommandHandler).GetTypeInfo().Assembly));

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(GetCategoryByIdQueryHandler).GetTypeInfo().Assembly));

        services.AddTransient<ISellerDomainService, SellerDomainService>();
        services.AddTransient<IUserDomainService, UserDomainService>();
        services.AddTransient<ICategoryDomainService, CategoryDomainService>();
        services.AddTransient<IProductDomainService, ProductDomainService>();


        services.AddValidatorsFromAssembly(typeof(CreateCategoryCommandValidator).Assembly);
        services.InitFacadeDependency();


    }
}