using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiIntegrationTests.Helpers;

public static class HttpResponseMessageExtensions
{
    public static async Task<T> DeserializeResponseAsync<T>(this HttpResponseMessage response)
    {
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }
}