using DevInSales.Context;
using DevInSales.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace DevInSales.UnitTests
{
    internal class FreightControllerTest
    {
        private readonly DbContextOptions<SqlContext> _contextOptions;

        public FreightControllerTest()
        {
            _contextOptions = new DbContextOptionsBuilder<SqlContext>()
                .UseInMemoryDatabase("FreightControllerTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;

            using var context = new SqlContext(_contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [Test]
        public void GetFreightTest()
        {
            var context = new SqlContext(_contextOptions);
            
            var controller = new FreightController(context);
            var result = controller.GetFreight(1);

            Assert.That(result, Is.Not.Null);           
        }
    }
}
