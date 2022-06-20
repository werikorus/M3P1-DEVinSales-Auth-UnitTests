using DevInSales.Context;
using DevInSales.Controllers;
using DevInSales.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace DevInSales.UnitTests
{
    internal class UserControllerTest
    {
        private readonly DbContextOptions<SqlContext> _contextOptions;

        public UserControllerTest()
        {
            _contextOptions = new DbContextOptionsBuilder<SqlContext>()
                .UseInMemoryDatabase("UserControllerTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;

            using var context = new SqlContext(_contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [Test]
        public async Task GetUserTest()
        {
            var context = new SqlContext(_contextOptions);
            var controller = new UserController(context);
            var result = controller.Get(null, null, null);

            Assert.That(result, Is.Not.Null);           
        }
    }
}
