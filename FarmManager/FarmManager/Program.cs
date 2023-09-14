var builder = WebApplication.CreateBuilder(args);

var webUIUrl = builder.Configuration["WebUIUrl"];
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins(webUIUrl).WithMethods("GET", "POST", "DELETE").AllowAnyHeader();
        }
    );
});

builder.Services.AddControllers();
builder.Services.AddSingleton<IAnimalsRepository, AnimalsRepository>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();
app.UseCors();

app.Run();
