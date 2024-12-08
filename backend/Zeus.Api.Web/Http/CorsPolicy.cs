namespace Zeus.Api.Web.Http;

public static class CorsPolicy
{
    private const string DevelopmentPolicy = "DevelopmentPolicy";
    private const string ProductionPolicy = "ProductionPolicy";

    public static IServiceCollection AddCors(this IServiceCollection services, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            services.AddDevelopmentCors();
        }
        else
        {
            services.AddProductionCors();
        }
        return services;
    }

    private static IServiceCollection AddDevelopmentCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(DevelopmentPolicy, builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return services;
    }

    private static IServiceCollection AddProductionCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(ProductionPolicy, builder =>
            {
                builder
                    .WithOrigins("https://example.com") // TODO: Set real origin
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return services;
    }

    public static IApplicationBuilder UseCors(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        var policy = environment.IsDevelopment()
            ? DevelopmentPolicy
            : ProductionPolicy;

        return app.UseCors(policy);
    }
}
