using System.Text.Json.Serialization;

using Scalar.AspNetCore;

using Zeus.Api.Application;
using Zeus.Api.Infrastructure;
using Zeus.Api.Presentation.Shared;
using Zeus.Api.Presentation.Web.Http;
using Zeus.Api.Presentation.Web.Manifest;
using Zeus.Api.Presentation.Web.Mapping;
using Zeus.Common.Domain.ProvidersSettings;

var builder = WebApplication.CreateBuilder(args);
{
    #region Configuration

    builder.Configuration
        .AddSharedAppSettings()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddUserSecrets<Program>(optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();

    #endregion Configuration

    #region Services

    await builder.Services.AddProvidersSettingsAsync();
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
    builder.Services.AddSingleton<ApiManifestProvider>();

    #endregion Services
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
