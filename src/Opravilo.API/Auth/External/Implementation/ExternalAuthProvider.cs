using System;
using System.Threading.Tasks;
using Opravilo.API.Auth.External.Models;

namespace Opravilo.API.Auth.External.Implementation
{
    public class ExternalAuthProvider : IExternalAuthProvider
    {
        private readonly IVkExternalAuth _vkontakte;
        
        public ExternalAuthProvider(IVkExternalAuth vkontakte)
        {
            _vkontakte = vkontakte;
        }
        
        public async Task<AuthResponseModel> Validate(string code, ExternalProviderType provider)
        {
            return provider switch
            {
                ExternalProviderType.Vkontakte => await _vkontakte.Validate(code),
                _ => throw new NotImplementedException(nameof(provider))
            };
        }

        public async Task<UserClaims> GetUserInfo(string userId, string accessToken, ExternalProviderType provider)
        {
            return provider switch
            {
                ExternalProviderType.Vkontakte => await _vkontakte.GetUserInfo(userId, accessToken),
                _ => throw new NotImplementedException(nameof(provider))
            };
        }
    }
}