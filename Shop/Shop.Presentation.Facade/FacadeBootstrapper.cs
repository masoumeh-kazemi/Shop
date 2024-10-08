﻿using Microsoft.Extensions.DependencyInjection;
using Shop.Presentation.Facade.Categories;
using Shop.Presentation.Facade.Comments;
using Shop.Presentation.Facade.Orders;
using Shop.Presentation.Facade.Products;
using Shop.Presentation.Facade.Roles;
using Shop.Presentation.Facade.Sellers;
using Shop.Presentation.Facade.Sellers.Inventories;
using Shop.Presentation.Facade.SiteEntities.Banner;
using Shop.Presentation.Facade.SiteEntities.ShippingMethods;
using Shop.Presentation.Facade.SiteEntities.Slider;
using Shop.Presentation.Facade.Users;
using Shop.Presentation.Facade.Users.Address;

namespace Shop.Presentation.Facade;

public static class FacadeBootstrapper
{
    public static void InitFacadeDependency(this IServiceCollection services)
    {
        //services.AddScoped<IOrderFacade, OrderFacade>();
        //services.AddScoped<IUserFacade, UserFacade>();
        //services.AddScoped<IUserAddressFacade, UserAddressFacade>();
        //services.AddScoped<ICategoryFacade, CategoryFacade>();
        //services.AddScoped<IBannerFacade, BannerFacade>();
        //services.AddScoped<IProductFacade, ProductFacade>();
        //services.AddScoped<IRoleFacade, RoleFacade>();
        //services.AddScoped<ICommentFacade, CommentFacade>();
        //services.AddScoped<ISellerFacade, SellerFacade>();
        //services.AddScoped<ISellerInventoryFacade, SellerInventoryFacade>();

        services.AddScoped<ICategoryFacade, CategoryFacade>();
        services.AddTransient<ICommentFacade, CommentFacade>();
        services.AddScoped<IOrderFacade, OrderFacade>();
        services.AddScoped<IProductFacade, ProductFacade>();
        services.AddScoped<IRoleFacade, RoleFacade>();
        services.AddScoped<ISellerFacade, SellerFacade>();
        services.AddScoped<IBannerFacade, BannerFacade>();
        services.AddScoped<ISliderFacade, SliderFacade>();
        services.AddScoped<ISellerInventoryFacade, SellerInventoryFacade>();
        services.AddScoped<IShippingMethodFacade, ShippingMethodFacade>();

        services.AddScoped<IUserFacade, UserFacade>();
        services.AddScoped<IUserAddressFacade, UserAddressFacade>();
    }
}