using dotnet_practice.Auth;
using dotnet_practice.Data;
using dotnet_practice.dtos.Mapping;
using dotnet_practice.Models;
using dotnet_practice.Services;
using Microsoft.EntityFrameworkCore;

namespace dotnet_practice.endpoints;

public static class AuthEndpoints
{

    public static void MapAuthEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("/auth");

        group.MapPost("/login", async (AuthService authService, LoginRequest request) =>
        {
            // In a real application, you would generate a JWT token here
            var token = "fake-jwt-token";

            var result = await authService.LoginUser(request.Username, request.Password);

            return Results.Ok(new { Token = token });
        });


    }

}
