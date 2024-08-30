using Shop.Api.Infrastructure.Gateways.Zibal;

namespace Shop.Api.Infrastructure;

public static class DependencyRegister
{
    public static void RegisterApiDependency(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddAutoMapper(typeof(MapperProfile).Assembly);
        service.AddHttpClient<IZibalService, ZibalService>();
        service.AddCors(options =>
        {
            options.AddPolicy(name: "ShopApi",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
    }
}