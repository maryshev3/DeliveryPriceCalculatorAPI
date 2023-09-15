using DeliveryPriceCalculatorAPI.Model;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace DeliveryPriceCalculatorAPI.Services
{
    public class CDEKService
    {
        public class AuthorizationData 
        {
            [Newtonsoft.Json.JsonProperty("access_token")]
            public string AccessToken;
        }

        static HttpClient Client = new HttpClient();

        public CDEKService() 
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Authorize().Result);
        }

        async private Task<string> Authorize() 
        {
            var parameters = new Dictionary<string, string> 
            {
                { "grant_type", "client_credentials" },
                { "client_id", "EMscd6r9JnFiQ3bLoyjJY6eM78JrJceI" },
                { "client_secret", "PjLZkKBHEiLK3YsjtNrt3TGNG0ahs3kG" }
            };

            var encodedContent = new FormUrlEncodedContent(parameters);

            HttpResponseMessage response = await Client.PostAsync("https://api.edu.cdek.ru/v2/oauth/token", encodedContent);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<AuthorizationData>(responseString).AccessToken;
        }

        async public Task<string> Calculator(PackageDelivery Data) 
        {
            HttpResponseMessage response = await Client.PostAsJsonAsync("https://api.edu.cdek.ru/v2/calculator/tarifflist", Data);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
