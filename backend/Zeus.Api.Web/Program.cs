using Scalar.AspNetCore;

using Zeus.Api.Application;
using Zeus.Api.Infrastructure;
using Zeus.Api.Web.Http;
using Zeus.Api.Web.Mapping;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddMappings();

    builder.Services.AddControllers();
    builder.Services.AddOpenApi();
    builder.Services.AddCors(builder.Environment);
}

var app = builder.Build();
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    
    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();
    
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseCors(app.Environment);
    
    app.MapControllers();

    app.Run();
}
