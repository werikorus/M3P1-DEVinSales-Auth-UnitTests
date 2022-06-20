using DevInSales.Context;
using DevInSales.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace DevInSales.UnitTests
{
    internal class OrderControllerTest
    {
        private readonly DbContextOptions<SqlContext> _contextOptions;

        public OrderControllerTest()
        {
            _contextOptions = new DbContextOptionsBuilder<SqlContext>()
                .UseInMemoryDatabase("OrderControllerTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;

            using var context = new SqlContext(_contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [Test]
        public void GetBuyIdTest()
        {
            var context = new SqlContext(_contextOptions);
            
            var controller = new OrderController(context);
            var result = controller.GetBuyId(1);

            Assert.That(result, Is.Not.Null);           
        }
    }
}
