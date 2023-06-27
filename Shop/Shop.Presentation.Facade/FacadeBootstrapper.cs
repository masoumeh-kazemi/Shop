using Microsoft.Extensions.DependencyInjection;
using Shop.Presentation.Facade.Categories;
using Shop.Presentation.Facade.Orders;
using Shop.Presentation.Facade.Products;
using Shop.Presentation.Facade.SiteEntities.Banner;
using Shop.Presentation.Facade.Users;
using Shop.Presentation.Facade.Users.Address;

namespace Shop.Presentation.Facade;

public static class FacadeBootstrapper
{
    public static void InitFacadeDependency(this IServiceCollection services)
    {
        services.AddScoped<IOrderFacade, OrderFacade>();
        services.AddScoped<IUserFacade, UserFacade>();
        services.AddScoped<IUserAddressFacade, UserAddressFacade>();
        services.AddScoped<ICategoryFacade, CategoryFacade>();
        services.AddScoped<IBannerFacade, BannerFacade>();
        services.AddScoped<IProductFacade, ProductFacade>();

    }
}