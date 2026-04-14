namespace dotnet_practice.Auth;

public record class LoginRequest(
    string Username,
    string Password
);

public record class LoginResponse(
    string Token
);
