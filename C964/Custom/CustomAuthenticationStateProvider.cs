using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

//public interface ICustomAuthenticationStateProvider
//{
//    void MarkUserAsAuthenticated(string username);
//}

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Implement your custom logic to determine authentication state
        // For example, retrieve an authentication cookie or token
        var identity = new ClaimsIdentity();

        // Add claims if authenticated (e.g., username, roles)
        // if (IsUserAuthenticated) 
        // {
        //     identity.AddClaim(new Claim(ClaimTypes.Name, "username"));
        //     identity.AddClaim(new Claim(ClaimTypes.Role, "Admin")); 
        // }

        var user = new ClaimsPrincipal(identity);
        return await Task.FromResult(new AuthenticationState(user));
    }

    public void MarkUserAsAuthenticated(string username)
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, username)
        }, "CustomAuth");
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void MarkUserAsLoggedOut()
    {
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
    }
}

