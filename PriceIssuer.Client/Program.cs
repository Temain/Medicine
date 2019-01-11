using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json.Linq;

namespace PriceIssuer.Client
{
	class Program
	{
		public static async Task Main(string[] args)
		{
			IdentityModelEventSource.ShowPII = true; //To show detail of error and see the problem

			// discover endpoints from metadata
			var client = new HttpClient();
			ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
			var disco = await client.GetDiscoveryDocumentAsync("https://identity.api:44322");
			if (disco.IsError)
			{
				Console.WriteLine(disco.Error);
				return;
			}

			// request token
			var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
			{
				Address = "https://localhost:44322/connect/token", // disco.TokenEndpoint,

				ClientId = "piclient",
				ClientSecret = "secret",
				Scope = "priceissuer"
			});

			if (response.IsError)
			{
				Console.WriteLine(response.Error);
				return;
			}

			Console.WriteLine(response.Json);

			//client = new HttpClient();
			//var testResponse = await client.GetAsync("https://localhost:44332/api/values");
			//if (!testResponse.IsSuccessStatusCode)
			//{
			//	Console.WriteLine(testResponse.StatusCode);
			//}
			//else
			//{
			//	var content = await testResponse.Content.ReadAsStringAsync();
			//	Console.WriteLine(JArray.Parse(content));
			//}

			// call api
			client.SetBearerToken(response.AccessToken);

			var identityResponse = await client.GetAsync("https://localhost:44332/identity");
			if (!identityResponse.IsSuccessStatusCode)
			{
				Console.WriteLine(identityResponse.StatusCode);
			}
			else
			{
				var content = await identityResponse.Content.ReadAsStringAsync();
				Console.WriteLine(JArray.Parse(content));
			}

			Console.ReadLine();
		}
	}
}
