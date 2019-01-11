using System.Collections.Generic;
using IdentityServer4.Models;

namespace Identity.Api.Configuration
{
	public class Config
	{
		// ApiResources define the apis in your system
		public static IEnumerable<ApiResource> GetApis()
		{
			return new List<ApiResource>
			{
				new ApiResource("priceissuer", "Price Issuer API")
			};
		}

		// Identity resources are data like user ID, name, or email address of a user
		// see: http://docs.identityserver.io/en/release/configuration/resources.html
		public static IEnumerable<IdentityResource> GetResources()
		{
			return new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile()
			};
		}

		// client want to access resources (aka scopes)
		public static IEnumerable<Client> GetClients()
		{
			return new List<Client>
			{
				new Client
				{
					ClientId = "piclient",

					// no interactive user, use the clientid/secret for authentication
					AllowedGrantTypes = GrantTypes.ClientCredentials,

					// secret for authentication
					ClientSecrets =
					{
						new Secret("secret".Sha256())
					},

					// scopes that client has access to
					AllowedScopes = { "priceissuer" }
				}
			};
		}
	}
}
