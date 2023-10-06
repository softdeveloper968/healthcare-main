using GaHealthcareNurses.Entity.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
  public class GetPaypalAccessToken
    {
        private HttpClient _httpClient;
        public GetPaypalAccessToken(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetAccessToken()
        {
            var bodyContent = new List<KeyValuePair<string, string>>();
            bodyContent.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            // create request object
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api-m.sandbox.paypal.com/v1/oauth2/token") { Content = new FormUrlEncodedContent(bodyContent) };

            // add authorization header
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", "QVpGdTdQcnl0VzRCeDY0ZTNfSWxyd2Y1WlJWLWxmRGxMYWlaZnZlODBfQlEwbGs0dDRpS2gtSzRvc2NxVWt3Tl9aNERLMk1Od2VRV1EwS3Y6RU5aMWt6RFFtR3lUTzZUcDNaZGFNT3lSNWVHTVh5aGx2bWU0NWhXckkyeF9KRnBwbXBKZENfRXlzUUp1dTdIbU05eUttNHZHUndIdVdEVHg=");

            // send request
            using var httpResponse = await _httpClient.SendAsync(request);
            var response = httpResponse.Content.ReadAsStringAsync().Result;
            PaypalAccessTokenResponseModel paypalAccessToken = (JsonConvert.DeserializeObject<PaypalAccessTokenResponseModel>(response));
            return paypalAccessToken.Access_token;
        }
    }
}
