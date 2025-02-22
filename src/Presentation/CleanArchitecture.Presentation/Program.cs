using Carter;
using CleanArc.Application.ServiceConfiguration;
using CleanArc.Web.Api.Controllers.V1.UserManagement;
using CleanArchitecture.Application.Models.ApiResult;
using CleanArchitecture.Domain.Entities.User;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.Infrastructure.Identity.Dtos;
using CleanArchitecture.Infrastructure.Identity.Jwt;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Shared.Extensions;
using CleanArchitecture.WebFramework;
using CleanArchitecture.WebFramework.Filters;
using CleanArchitecture.WebFramework.Middlewares;
using CleanArchitectureWebFramework.Swagger;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

Activity.DefaultIdFormat = ActivityIdFormat.W3C;

builder.Services.Configure<IdentitySettings>(configuration.GetSection(nameof(IdentitySettings)));
var identitySettings = configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>();


builder.Services.AddControllers(options =>
{
options.Filters.Add(typeof(OkResultAttribute));
options.Filters.Add(typeof(NotFoundResultAttribute));
options.Filters.Add(typeof(ContentResultFilterAttribute));
options.Filters.Add(typeof(ModelStateValidationAttribute));
options.Filters.Add(typeof(BadRequestResultFilterAttribute));
options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiResult<Dictionary<string, List<string>>>),
    StatusCodes.Status400BadRequest));
options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiResult),
    StatusCodes.Status401Unauthorized));
options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiResult),
    StatusCodes.Status403Forbidden));
options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiResult),
    StatusCodes.Status500InternalServerError));


}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
    options.SuppressMapClientErrors = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger("v1", "v1.1");

builder.Services.AddCarter(configurator: configurator => { configurator.WithEmptyValidators(); });


builder.Services.AddApplicationServices()
    .RegisterIdentityServices(identitySettings)
    .AddPersistenceServices(configuration)
    .AddWebFrameworkServices();

builder.Services.RegisterValidatorsAsServices();
builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddAutoMapper(expression =>
{
    expression.AddMaps(typeof(User), typeof(JwtService), typeof(UserController));
});


var app = builder.Build();

await app.ApplyMigrationsAsync();
await app.SeedDefaultUsersAsync();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler(_ => { });
app.UseSwaggerAndUi();

app.MapCarter();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


await app.RunAsync();


// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
