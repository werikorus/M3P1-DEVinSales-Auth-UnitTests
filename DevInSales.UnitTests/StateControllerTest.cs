using DevInSales.Context;
using DevInSales.Controllers;
using DevInSales.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Generic;


namespace DevInSales.UnitTests
{
    internal class StateControllerTest
    {
        private readonly DbContextOptions<SqlContext> _contextOptions;

        public StateControllerTest()
        {
            _contextOptions = new DbContextOptionsBuilder<SqlContext>()
                .UseInMemoryDatabase("StateControllerTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;

            using var context = new SqlContext(_contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [Test]
        public async Task GetStateByNameTest()
        {
            var context = new SqlContext(_contextOptions);

            var controller = new StateController(context);
            var result = await controller.GetState("To");
            var content = (result.Result as ObjectResult).Value as List<State>;

            Assert.That(content[0].Name, Is.EqualTo("Tocantins"));           
        }

        [Test]
        public async Task GetStateByIDTest()
        {
            var context = new SqlContext(_contextOptions);

            var controller = new StateController(context);
            var result = await controller.GetState(17);
            var state = result.Value;

            Assert.That(state.Name, Is.EqualTo("Tocantins"));
        }

        [Test]
        public async Task GetByIdStateCityTest()
        {
            var context = new SqlContext(_contextOptions);

            var controller = new StateController(context);
            var result = await controller.GetByIdStateCity(17, 11);

            var expected = result.Result as StatusCodeResult;

            Assert.That(expected.StatusCode.ToString(), Is.EqualTo("400"));
        }
    }
}
