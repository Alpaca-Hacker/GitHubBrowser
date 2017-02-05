using GitHubBrowser.Helpers.Interfaces;
using GitHubBrowser.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GitHubBrowser.Tests.ServicesTests
{

    [TestClass]
    public class GitHubApiServiceTests
    {
        [TestMethod]

        public void Api_Returns_Null_When_Username_Is_Null_Or_Empty()
        {
            //Arrange
            var configHelper = new Mock<IConfigHelper>();
            configHelper.Setup(c => c.Get(It.IsAny<string>())).Returns("https://api.github.com/users/");

            var class_to_test = new GitHubApiService(configHelper.Object);

            //Act
            var result = class_to_test.GetUserProfile("");

            //Assert
            Assert.IsTrue(result == null);

        }
        [TestMethod, TestCategory("Integration")]
        
        public void Api_Gets_User_Profile_Correctly()
        {
            //Arrange
            var configHelper = new Mock<IConfigHelper>();
            configHelper.Setup(c => c.Get(It.IsAny<string>())).Returns("https://api.github.com/users/");

            var class_to_test = new GitHubApiService(configHelper.Object);

            //Act
            var result = class_to_test.GetUserProfile("test");

            //Assert
            Assert.IsTrue(result != null);
        }

        [TestMethod, TestCategory("Integration")]

        public void Api_Returns_Null_If_Not_Found()
        {
            //Arrange
            var configHelper = new Mock<IConfigHelper>();
            configHelper.Setup(c => c.Get(It.IsAny<string>())).Returns("https://api.github.com/users/");

            var class_to_test = new GitHubApiService(configHelper.Object);

            //Act
            var result = class_to_test.GetUserProfile("XXXNOTFOUNDXXX");

            //Assert
            Assert.IsTrue(result == null);
        }

        [TestMethod, TestCategory("Integration")]

        public void Api_Returns_Repos_With_Username()
        {
            //Arrange
            var configHelper = new Mock<IConfigHelper>();
            configHelper.Setup(c => c.Get(It.IsAny<string>())).Returns("https://api.github.com/users/");

            var class_to_test = new GitHubApiService(configHelper.Object);

            //Act
            var result = class_to_test.GetUserRepoList("test");

            //Assert
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod, TestCategory("Integration")]
        public void Api_Returns_Repos_With_Url()
        {
            //Arrange
            var configHelper = new Mock<IConfigHelper>();
            configHelper.Setup(c => c.Get(It.IsAny<string>())).Returns("https://api.github.com/users/");

            var class_to_test = new GitHubApiService(configHelper.Object);

            //Act
            var result = class_to_test.GetUserRepoListWithUrl("https://api.github.com/users/test/repos");

            //Assert
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod, TestCategory("Integration")]
        public void Api_Returns_Empty_Repos_When_Not_Found_With_Url()
        {
            //Arrange
            var configHelper = new Mock<IConfigHelper>();
            configHelper.Setup(c => c.Get(It.IsAny<string>())).Returns("https://api.github.com/users/");

            var class_to_test = new GitHubApiService(configHelper.Object);

            //Act
            var result = class_to_test.GetUserRepoListWithUrl("https://api.github.com/users/xxxnotfoundxxx/repos");

            //Assert
            Assert.IsTrue(result.Count == 0);
        }
    }
}
