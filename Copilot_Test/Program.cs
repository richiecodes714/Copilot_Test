var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Register application services
builder.Services.AddSingleton<Copilot_Test.Repositories.IUserRepository, Copilot_Test.Repositories.UserRepository>();
builder.Services.AddScoped<Copilot_Test.Services.IUserService, Copilot_Test.Services.UserService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
