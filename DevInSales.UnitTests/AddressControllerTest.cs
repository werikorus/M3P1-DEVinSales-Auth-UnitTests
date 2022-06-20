using DevInSales.Context;
using DevInSales.Controllers;
using DevInSales.DTOs;
using DevInSales.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DevInSales.UnitTests
{
    internal class AddressControllerTest
    {
        private readonly DbContextOptions<SqlContext> _contextOptions;

        public AddressControllerTest()
        {
            _contextOptions = new DbContextOptionsBuilder<SqlContext>()
                .UseInMemoryDatabase("AddressControllerTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;

            using var context = new SqlContext(_contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [Test]
        public async Task GetAddressTest()
        {
            var context = new SqlContext(_contextOptions);
            var qtdUser = context.Address.Count();

            var controller = new AddressController(context);
            var result = await controller.GetAddress();
            var value = result.Value;
            var content = value as List<Address>;

            Assert.That(content.Count, Is.EqualTo(qtdUser));         
        }
    }
}
