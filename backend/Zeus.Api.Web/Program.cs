using Scalar.AspNetCore;
using Zeus.Api.Application;
using Zeus.Api.Infrastructure;
using Zeus.Api.Web.Common.Mapping;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure()
        .AddMappings()
        .AddAuthentication(builder.Configuration);

    builder.Services.AddControllers();
    builder.Services.AddOpenApi();
}

var app = builder.Build();
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    app.UseHttpsRedirection();
    
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.MapControllers();

    app.Run();
}
