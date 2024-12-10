using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Mvc;

using Scalar.AspNetCore;

using Zeus.Api.Application;
using Zeus.Api.Infrastructure;
using Zeus.Api.Web.Http;
using Zeus.Api.Web.Mapping;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Configuration.AddJsonFile("services-settings.json", optional: false, reloadOnChange: true);

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddAuthentication(builder.Configuration)
        .AddMappings();

    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
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
