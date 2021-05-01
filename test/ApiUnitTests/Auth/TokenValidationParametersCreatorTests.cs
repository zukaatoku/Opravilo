using Opravilo.API.Auth;
using Opravilo.API.Options;
using Xunit;

namespace ApiUnitTests.Auth
{
    public class TokenValidationParametersCreatorTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Create_Should_Utilize_AuthOptions_And_CheckLifetimeParameter(bool shouldCheckLifetime)
        {
            var options = GetFakeOptions();
            var creator = new TokenValidationParametersCreator();

            var parameters = creator.Create(options, shouldCheckLifetime);

            Assert.Equal(shouldCheckLifetime, parameters.ValidateLifetime);
            Assert.True(parameters.ValidateIssuer);
            Assert.True(parameters.ValidateAudience);
            Assert.True(parameters.ValidateIssuerSigningKey);
            Assert.Equal(options.Audience, parameters.ValidAudience);
            Assert.Equal(options.Issuer, parameters.ValidIssuer);
        }

        private JwtAuthOptions GetFakeOptions()
        {
            return new JwtAuthOptions()
            {
                Issuer = "fakeIssuer",
                Audience = "fakeAudience",
                Key = "fake_key",
            };
        }
    }
}