using dotnet_practice.Data;
using dotnet_practice.dtos;
using dotnet_practice.endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();
builder.ConnectDb();

var app = builder.Build();

app.MapGamesEndpoints();
app.MapGenresEndpoints();

app.MigrateDb();

app.Run();
