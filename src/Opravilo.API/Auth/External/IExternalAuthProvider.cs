using System.Threading.Tasks;
using Opravilo.API.Auth.External.Models;

namespace Opravilo.API.Auth.External
{
    public interface IExternalAuthProvider
    {
        Task<AuthResponseModel> Validate(string code, ExternalProviderType provider);
        Task<UserClaims> GetUserInfo(string userId, string accessToken, ExternalProviderType provider);
    }
}