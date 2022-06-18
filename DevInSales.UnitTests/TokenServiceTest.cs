using DevInSales.Mock;
using DevInSales.Services;
using DevInSales.Models;
using DevInSales.Enums;
using System.Security.Claims;


namespace DevInSales.UnitTests
{
    public class TokenServiceTest
    {
        public string FirstToken { get; set; }
        public string SecondToken { get; set; }
        public string RefreshToken { get; set; }
        public ClaimsPrincipal Principal { get; set; }

        private readonly TokenService _TokenService;


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestBearerToken()
        {
            User user = UsersMock.GetUserMock("Test", "000");
            FirstToken = TokenService.GenerateToken(user);

            Assert.That(FirstToken, Is.Not.Empty);
        }

        [Test]
        public void TestRefreshToken()
        {
            RefreshToken = TokenService.GenerateRefreshToken();
            TokenService.SaveRefreshToken("Test", RefreshToken);
            TokenService.GetRefreshToken("Test");
            Assert.That(RefreshToken, Is.Not.Empty);
        }

        [Test]
        public void TestGetMainFromExpiredToken()
        {
            Principal = TokenService.GetPrincipalFromExpiredToken(FirstToken);

            Assert.That(Principal, Is.Not.Null);
        }

        [Test] 
        public void TestGenerateBearerToken()
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, "Test"),
                new Claim(ClaimTypes.Role, "Administrador")
            };

            SecondToken = TokenService.GenerateBearerToken(claims);
            Assert.That(SecondToken, Is.Not.Empty);
        }

        [Test]
        public void TestDeleteRefreshToken()
        {
            var deleted = TokenService.DeleteRefreshToken("Test", RefreshToken);
            Assert.That(deleted, Is.True);
        }
    }
}
