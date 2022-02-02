using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiIntegrationTests.Helpers;

public static class HttpClientExtensions
{
    public static async Task<HttpResponseMessage> PostJsonFormAsync(this HttpClient client, string uri, object form)
    {
        var json = JsonConvert.SerializeObject(form);
        return await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
    }
}