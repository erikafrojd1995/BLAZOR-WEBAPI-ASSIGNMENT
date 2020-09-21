using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using Blazor.Models;
using System.Security.Claims;


namespace Blazor.Services
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private ISessionStorageService SessionStorage { get; set; }

        public AuthStateProvider(ISessionStorageService sessionStorageService)
        {
            SessionStorage = sessionStorageService;
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal claimsPrincipal;

            if (await SessionStorage.GetItemAsync<string>("Id") != null)
            {
                var claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim("Id", await SessionStorage.GetItemAsync<string>("Id")),
                    new Claim(ClaimTypes.Sid,  await SessionStorage.GetItemAsync<string>("Id")),
                    new Claim(ClaimTypes.Name, await SessionStorage.GetItemAsync<string>("Email")),
                    new Claim(ClaimTypes.Email, await SessionStorage.GetItemAsync<string>("Email")),
                    new Claim(ClaimTypes.Role, await SessionStorage.GetItemAsync<string>("Role")),
                    new Claim("Token", await SessionStorage.GetItemAsync<string>("Token"))
                }, "apiauth");

                claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            }
            else
            {
                claimsPrincipal = new ClaimsPrincipal();
            }

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }


        public async Task Authenticated(LoginResponseModel response)
        {
            await SessionStorage.SetItemAsync("Id", response.Id.ToString());
            await SessionStorage.SetItemAsync("Email", response.Email.ToString());
            await SessionStorage.SetItemAsync("Role", response.Role.ToString());
            await SessionStorage.SetItemAsync("Token", response.Token.ToString());

            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim("Id", response.Id.ToString()),
                new Claim(ClaimTypes.Sid, response.Id.ToString()),
                new Claim(ClaimTypes.Name, response.Email),
                new Claim(ClaimTypes.Email, response.Email),
                new Claim(ClaimTypes.Role, response.Role),
                new Claim("Token", response.Token)
            }, "apiauth");

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));

        }

        public async Task NotAuthenticated()
        {
            await SessionStorage.RemoveItemAsync("Id");
            await SessionStorage.RemoveItemAsync("Email");
            await SessionStorage.RemoveItemAsync("Role");
            await SessionStorage.RemoveItemAsync("Token");

            var claimsIdentity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

    }
}