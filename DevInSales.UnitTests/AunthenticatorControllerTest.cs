using DevInSales.Context;
using DevInSales.Controllers;
using DevInSales.DTOs;
using DevInSales.Mock;
using DevInSales.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DevInSales.UnitTests
{
    internal class AunthenticatorControllerTest
    {
        private readonly DbContextOptions<SqlContext> _contextOptions;
        public string FirstToken { get; set; }
        public string SecondToken { get; set; }
        public string RefreshToken { get; set; }

        public AunthenticatorControllerTest()
        {
            _contextOptions = new DbContextOptionsBuilder<SqlContext>()
                .UseInMemoryDatabase("AunthenticatorControllerTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;

            using var context = new SqlContext(_contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [Test]
        public void LoginTest()
        {
            var context = new SqlContext(_contextOptions);
            var userDTO = new LoginDto("romeu", "romeu123@");

            var controller = new AuthenticatorController(context);
            var actionResult = controller.Login(userDTO);
            
            var result = actionResult.Result as OkObjectResult;
          
            FirstToken = result.Value.GetType().GetProperty("token").GetValue(result.Value) as string;
            RefreshToken = result.Value.GetType().GetProperty("newRefreshToken").GetValue(result.Value) as string;

            Assert.That(FirstToken, Is.Not.Empty);
        }

        [Test]
        public void RefreshTokenTest()
        {
            var context = new SqlContext(_contextOptions);
            
            var controller = new AuthenticatorController(context);
            var result = controller.RefreshToken(FirstToken, RefreshToken);
        
            Assert.That(result, Is.Not.Null);
        }
    }
}
