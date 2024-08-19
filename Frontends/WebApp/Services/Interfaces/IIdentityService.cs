using IdentityModel.Client;
using Shared.Dtos;
using WebApp.Models;

namespace WebApp.Services.Interfaces;

public interface IIdentityService
{
    Task<Response<bool>> SignIn(SignInInput signInInput);
    Task<TokenResponse> GetAccessTokenByRefreshToken();
    Task RevokeRefreshToken();
}
