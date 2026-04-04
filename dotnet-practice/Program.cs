using dotnet_practice.Data;
using dotnet_practice.dtos;
using dotnet_practice.endpoints;
using dotnet_practice.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();
builder.ConnectDb();
builder.Services.AddHttpClient();


builder.Services.AddScoped<GameService>();

var app = builder.Build();

app.MapGamesEndpoints();
app.MapGenresEndpoints();

app.MigrateDb();

app.Run();
