using dotnet_practice.Data;
using dotnet_practice.endpoints;
using dotnet_practice.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();


// DB Context
builder.ConnectDb();


// HttpClient
builder.Services.AddHttpClient();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Service DI
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<AuthService>();


builder.Services.AddHealthChecks();

var app = builder.Build();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSettings["Key"];
var jwtIssuer = jwtSettings["Issuer"];
var jwtAudience = jwtSettings["Audience"];

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey!))
        };
    });

builder.Services.AddAuthorization();


// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoint mapping
app.MapGamesEndpoints();
app.MapGenresEndpoints();
app.MapAuthEndpoints();



app.UseExceptionHandler("/error");

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health");

app.Map("/error", () => Results.Problem("An unexpected error occurred."));

// DB Migrations
app.MigrateDb();

app.Run();

