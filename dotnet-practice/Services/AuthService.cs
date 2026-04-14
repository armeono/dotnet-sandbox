using dotnet_practice.Data;

namespace dotnet_practice.Services;

public class AuthService
{
    private GameStoreContext db;


    public AuthService(GameStoreContext db)
    {
        this.db = db;
    }


    public Task<bool> LoginUser(string username, string password)
    {
        return Task.FromResult(username == "admin" && password == "password");
    }

}
