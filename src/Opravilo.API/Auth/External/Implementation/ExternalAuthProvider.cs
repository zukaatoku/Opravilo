using System;
using System.Threading.Tasks;
using Opravilo.API.Auth.External.Models;

namespace Opravilo.API.Auth.External.Implementation
{
    public class ExternalAuthProvider : IExternalAuthProvider
    {
        private readonly IExternalAuth _vkontakte;
        
        public ExternalAuthProvider(VkontakteExternalAuth vkontakte)
        {
            _vkontakte = vkontakte;
        }
        
        public Task<AuthResponseModel> Validate(string code, ExternalProviderType provider)
        {
            return provider switch
            {
                ExternalProviderType.Vkontakte => _vkontakte.Validate(code),
                _ => throw new NotImplementedException(nameof(provider))
            };
        }

        public Task<UserClaims> GetUserInfo(string userId, string accessToken, ExternalProviderType provider)
        {
            return provider switch
            {
                ExternalProviderType.Vkontakte => _vkontakte.GetUserInfo(userId, accessToken),
                _ => throw new NotImplementedException(nameof(provider))
            };
        }
    }
}