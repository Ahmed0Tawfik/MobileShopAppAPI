using MobileShop.API.DependencyInjection;
using MobileShop.API.EndPoints;
using MobileShop.API.Middleware;
using MobileShop.Infrastructure.DependencyInjection;
using Microsoft.Identity.Client;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// ADDING DEPENDENCIES
builder.Services.AddAutoRegisterHandlers();
builder.Services.AddFluentValidation();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()   // Allows requests from any domain
                   .AllowAnyMethod()   // Allows all HTTP methods (GET, POST, PUT, DELETE, etc.)
                   .AllowAnyHeader();  // Allows any header
        });
});



var app = builder.Build();

app.UseCors("AllowAll");

// OPEN API
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// MIDDLEWARE
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<ValidationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// MAPPING ENDPOINTS
app.MapProductEndPoints();
app.Run();