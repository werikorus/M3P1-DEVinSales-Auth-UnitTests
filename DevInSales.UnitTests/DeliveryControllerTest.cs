using DevInSales.Context;
using DevInSales.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace DevInSales.UnitTests
{
    internal class DeliveryControllerTest
    {
        private readonly DbContextOptions<SqlContext> _contextOptions;

        public DeliveryControllerTest()
        {
            _contextOptions = new DbContextOptionsBuilder<SqlContext>()
                .UseInMemoryDatabase("DeliveryControllerTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;

            using var context = new SqlContext(_contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [Test]
        public void GetDeliveryTest()
        {
            var context = new SqlContext(_contextOptions);
            
            var controller = new DeliveryController(context);
            var result = controller.GetDelivery(0,0);

            Assert.That(result, Is.Not.Null);           
        }
    }
}
