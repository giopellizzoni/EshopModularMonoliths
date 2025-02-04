namespace Catalog;

public static class CatalogModule
{
    public static IServiceCollection AddCatalogModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddDataInfrastructure(configuration);

        return services;
    }

    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {
        return app;
    }

    private static IServiceCollection AddDataInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCatalogDbContext(configuration);

        return services;
    }

    private static IServiceCollection AddCatalogDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<CatalogDbContext>(options => options.UseNpgsql(connectionString));
        return services;
    }
}


