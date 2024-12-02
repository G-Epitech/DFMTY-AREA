using Scalar.AspNetCore;
using Zeus.Api.Application;
using Zeus.Api.Infrastructure;
using Zeus.Api.Web.Common.Mapping;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddMappings();

    builder.Services.AddControllers();
    builder.Services.AddOpenApi();
}

var app = builder.Build();
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
