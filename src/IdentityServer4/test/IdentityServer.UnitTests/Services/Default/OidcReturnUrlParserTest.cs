using FluentAssertions;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace IdentityServer.UnitTests.Services.Default
{
    public class OidcReturnUrlParserTest
    {
        private readonly OidcReturnUrlParser _sut = new OidcReturnUrlParser(null, null, NullLogger<OidcReturnUrlParser>.Instance);

        [Theory]
        [InlineData("connect/authorize")]
        [InlineData("/connect/authorize")]
        [InlineData("~/connect/authorize")]
        [InlineData("connect/authorize/callback")]
        [InlineData("/connect/authorize/callback")]
        [InlineData("~/connect/authorize/callback")]
        public void IsValidReturnUrl_PassesForValidReturnUrls(string url)
        {
            var result = _sut.IsValidReturnUrl(url);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("/connect/notAuthorize")]
        [InlineData("/notConnect/authorize")]
        [InlineData("/connect/authorize/notCallback")]
        [InlineData("http://example.com/authorize/callback")]
        [InlineData("//example.com/authorize/callback")]
        public void IsValidReturnUrl_FailsForInvalidReturnUrls(string url)
        {
            var result = _sut.IsValidReturnUrl(url);

            result.Should().BeFalse();
        }
    }
}