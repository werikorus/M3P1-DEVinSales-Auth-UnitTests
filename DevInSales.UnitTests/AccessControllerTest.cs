using DevInSales.Controllers;
using DevInSales.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;

namespace DevInSales.UnitTests
{
    internal class AccessControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AccessPublicTest()
        {
            var controller = new AccessController();
            var actionResult = controller.AccessPublic();
            var result = actionResult.Result as OkObjectResult;
            var msgText = result.Value as string;
            
            Assert.That(msgText, Is.EqualTo("Acesso público de todos os colaboradores"));
        }

        [Test]
        public void EmployeePublicTest()
        {
            var controller = new AccessController();
            var actionResult = controller.AccessEmployee();
            var result = actionResult.Result as OkObjectResult;
            var msgText = result.Value as string;

            Assert.That(msgText, Is.EqualTo("Bem-vindo à página de funcionários!"));
        }

        [Test]
        public void AccessManagerTest()
        {
            var controller = new AccessController();
            var actionResult = controller.AccessManager();
            var result = actionResult.Result as OkObjectResult;
            var msgText = result.Value as string;

            Assert.That(msgText, Is.EqualTo("Acesso exclusivo à gerentes"));
        }

        [Test]
        public void AccessAdministratorTest()
        {
            var controller = new AccessController();
            var actionResult = controller.AccessAdministrator();
            var result = actionResult.Result as OkObjectResult;
            var msgText = result.Value as string;

            Assert.That(msgText, Is.EqualTo("Acesso exclusivo à administradores"));
        }
    }
}
