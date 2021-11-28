using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Opravilo.API.Auth.External.Models;
using Opravilo.API.Options;

namespace Opravilo.API.Auth.External.Implementation
{
    public class VkontakteExternalAuth : IVkExternalAuth
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly VkAuthOptions _options;

        public VkontakteExternalAuth(IHttpClientFactory clientFactory, VkAuthOptions options)
        {
            _clientFactory = clientFactory;
            _options = options;
        }

        public async Task<AuthResponseModel> Validate(string code)
        {
            const string accessTokenUrl = "https://oauth.vk.com/access_token?client_id={0}&client_secret={1}&redirect_uri={2}&code={3}";
            using (var client = _clientFactory.CreateClient())
            {
                var url = string.Format(accessTokenUrl, _options.ClientId, _options.Secret, "https://localhost:5011/vk-login-callback", code);
                var result = await client.GetAsync(url);

                result.EnsureSuccessStatusCode();

                var content = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<AuthResponseModel>(content);
                return response;
            }
        }

        public async Task<UserClaims> GetUserInfo(string userId, string accessToken)
        {
            const string usersInfo = "https://api.vk.com/method/users.get?user_ids={0}&access_token={1}&v=5.131";
            using (var client = _clientFactory.CreateClient())
            {
                var url = string.Format(usersInfo, userId, accessToken);
                var result = await client.GetAsync(url);

                result.EnsureSuccessStatusCode();

                var content = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ClaimsResponseModel>(content);
                return response.response.First();
            }
        }
    }
}