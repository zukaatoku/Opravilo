using System;
using System.Threading.Tasks;
using Moq;
using Opravilo.API.Auth.External;
using Opravilo.API.Auth.External.Implementation;
using Opravilo.API.Auth.External.Models;
using Xunit;

namespace ApiUnitTests.Auth
{
    public class ExternalAuthProviderTests
    {
        private readonly Mock<IVkExternalAuth> _vkAuthMock;

        public ExternalAuthProviderTests()
        {
            _vkAuthMock = new Mock<IVkExternalAuth>();
        }

        [Fact]
        public async Task Validate_ShouldThrowNotImplemented_WhenNotVkPassed()
        {
            // Arrange
            var provider = new ExternalAuthProvider(_vkAuthMock.Object);
            
            // Act & Assert
            await Assert.ThrowsAsync<NotImplementedException>(() => provider.Validate(string.Empty, ExternalProviderType.Github));
        }
        
        [Fact]
        public async Task GetUserInfo_ShouldThrowNotImplemented_WhenNotVkPassed()
        {
            // Arrange
            var provider = new ExternalAuthProvider(_vkAuthMock.Object);
            
            // Act & Assert
            await Assert.ThrowsAsync<NotImplementedException>(() => provider.GetUserInfo(string.Empty, string.Empty, ExternalProviderType.Github));
        }

        [Fact]
        public async Task Validate_ShouldReturnResponse_WhenVkPassed()
        {
            // Arrange
            _vkAuthMock.Setup(v => v.Validate(It.IsAny<string>()))
                .ReturnsAsync(() => new AuthResponseModel());
            var provider = new ExternalAuthProvider(_vkAuthMock.Object);
            
            // Act
            var result = await provider.Validate(string.Empty, ExternalProviderType.Vkontakte);
            
            // Assert
            Assert.NotNull(result);
        }
        
        [Fact]
        public async Task GetUserInfo_ShouldReturnResponse_WhenVkPassed()
        {
            // Arrange
            _vkAuthMock.Setup(v => v.GetUserInfo(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => new UserClaims());
            var provider = new ExternalAuthProvider(_vkAuthMock.Object);
            
            // Act
            var result = await provider.GetUserInfo(string.Empty, string.Empty, ExternalProviderType.Vkontakte);
            
            // Assert
            Assert.NotNull(result);
        }
    }
}