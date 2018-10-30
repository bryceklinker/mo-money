using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace Identity.Management.Tests.Common
{
    public static class JwtTokenAssert
    {
        public static async Task IsValidAsync(string jwtToken, string identityServerUrl)
        {
            IdentityModelEventSource.ShowPII = true;
            var openIdConfigManager = CreateOpenIdConfigManager(identityServerUrl);

            var configuration = await openIdConfigManager.GetConfigurationAsync();
            var parameters = CreateTokenValidationParameters(configuration);
            var principal = ValidateJwtToken(jwtToken, parameters);
            Assert.NotNull(principal);
        }

        private static ClaimsPrincipal ValidateJwtToken(string jwtToken, TokenValidationParameters parameters)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ValidateToken(jwtToken, parameters, out _);
        }

        private static TokenValidationParameters CreateTokenValidationParameters(OpenIdConnectConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                RequireSignedTokens = true,
                RequireExpirationTime = true,
                IssuerSigningKeys = configuration.SigningKeys,
                ValidateLifetime = true,
                ValidAudience = "mo.money.identity",
                ValidIssuer = configuration.Issuer
            };
        }

        private static ConfigurationManager<OpenIdConnectConfiguration> CreateOpenIdConfigManager(string identityServerUrl)
        {
            return new ConfigurationManager<OpenIdConnectConfiguration>(
                $"{identityServerUrl}.well-known/openid-configuration",
                new OpenIdConnectConfigurationRetriever(),
                new HttpDocumentRetriever());
        }
    }
}