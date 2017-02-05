using System.Web.Mvc;
using GitHubBrowser.Controllers;
using GitHubBrowser.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GitHubBrowser.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var gitHubService = new Mock<IGitHubService>();
            var controller = new HomeController(gitHubService.Object);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
