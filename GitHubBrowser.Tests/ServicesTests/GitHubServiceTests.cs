using System.Collections.Generic;
using GitHubBrowser.Models;
using GitHubBrowser.Services;
using GitHubBrowser.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GitHubBrowser.Tests.ServicesTests
{
    [TestClass]
    public class GitHubServiceTests
    {

        [TestMethod]
        public void Service_Returns_Empty_When_Called_With_Empty_String()
        {
            //Arrange
            var apiService = new Mock<IGitHubApiService>();
            var class_to_test = new GitHubService(apiService.Object);

            //Act
            var results = class_to_test.GetUserProfile("");

            //Assert
            Assert.IsTrue(string.IsNullOrEmpty(results.Name));
        }

        [TestMethod]
        public void Service_Returns_Not_Found_When_Called_With_Unknown_User()
        {
            //Arrange
            var apiService = new Mock<IGitHubApiService>();
            apiService.Setup(a => a.GetUserProfile(It.IsAny<string>())).Returns((UserProfile)null);

            var class_to_test = new GitHubService(apiService.Object);

            //Act
            var results = class_to_test.GetUserProfile("UNKNOWN");

            //Assert
            Assert.IsTrue(results.Name == "Not Found");
        }

        [TestMethod]
        public void Service_Returns_UserProfile_Without_Repos()
        {
            //Arrange
            var test_profile_to_return = new UserProfile
            {
                Name = "Test User",
                Repos_Url = "ReposURL"
            };

            var apiService = new Mock<IGitHubApiService>();
            apiService.Setup(a => a.GetUserProfile(It.IsAny<string>())).Returns(test_profile_to_return);
            apiService.Setup(a => a.GetUserRepoListWithUrl(It.IsAny<string>())).Returns(new List<Repo>());

            var class_to_test = new GitHubService(apiService.Object);

            //Act
            var results = class_to_test.GetUserProfile("TestWithoutRepos");

            //Assert
            Assert.IsTrue(results.Name == "Test User");
            Assert.IsTrue(results.Repos.Count == 0);
        }

        [TestMethod]
        public void Service_Returns_UserProfile_With_One_Repo()
        {
            //Arrange
            var test_profile_to_return = new UserProfile
            {
                Name = "Test User",
                Repos_Url = "ReposURL"
            };

            var repo_to_return = new List<Repo>
            {
                new Repo
                {
                    Name = "repo 1"
                }
            };

            var apiService = new Mock<IGitHubApiService>();
            apiService.Setup(a => a.GetUserProfile(It.IsAny<string>())).Returns(test_profile_to_return);
            apiService.Setup(a => a.GetUserRepoListWithUrl(It.IsAny<string>())).Returns(repo_to_return);

            var class_to_test = new GitHubService(apiService.Object);

            //Act
            var results = class_to_test.GetUserProfile("TestWithOneRepo");

            //Assert
            Assert.IsTrue(results.Name == "Test User");
            Assert.IsTrue(results.Repos.Count == 1);
        }

        [TestMethod]
        public void Service_Returns_UserProfile_With_Six_Repos()
        {
            //Arrange
            var test_profile_to_return = new UserProfile
            {
                Name = "Test User",
                Repos_Url = "ReposURL"
            };

            var repos_to_return = new List<Repo>
            {
                new Repo {Name = "repo 1", Stargazers_Count = 5},
                new Repo {Name = "repo 2", Stargazers_Count = 5},
                new Repo {Name = "repo 3", Stargazers_Count = 5},
                new Repo {Name = "repo 4", Stargazers_Count = 3},
                new Repo {Name = "repo 5", Stargazers_Count = 25},
                new Repo {Name = "repo 6", Stargazers_Count = 15}
            };

            var apiService = new Mock<IGitHubApiService>();
            apiService.Setup(a => a.GetUserProfile(It.IsAny<string>())).Returns(test_profile_to_return);
            apiService.Setup(a => a.GetUserRepoListWithUrl(It.IsAny<string>())).Returns(repos_to_return);

            var class_to_test = new GitHubService(apiService.Object);

            //Act
            var results = class_to_test.GetUserProfile("TestWithOneRepo");

            //Assert
            Assert.IsTrue(results.Name == "Test User");
            Assert.IsTrue(results.Repos.Count == 5);
            Assert.IsFalse(results.Repos.Exists(r => r.Name == "repo 4"));
        }
    }
}
