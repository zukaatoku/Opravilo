using System.Threading.Tasks;
using Opravilo.API.Auth.External.Models;

namespace Opravilo.API.Auth.External
{
    public interface IExternalAuth
    {
        Task<AuthResponseModel> Validate(string code);
        Task<UserClaims> GetUserInfo(string userId, string accessToken);
    }
}