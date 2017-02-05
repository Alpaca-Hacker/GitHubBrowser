using System.Linq;
using GitHubBrowser.Models;
using GitHubBrowser.Services.Interfaces;

namespace GitHubBrowser.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly IGitHubApiService _gitHubApiService;

        public GitHubService(IGitHubApiService gitHubApiService)
        {
            _gitHubApiService = gitHubApiService;
        }

        public UserProfile GetUserProfile(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return new UserProfile();
            }

            var userProfile = _gitHubApiService.GetUserProfile(username);

            if (userProfile == null)
            {
                return new UserProfile
                {
                    Login = username,
                    Name = "Not Found"
                };
            }

            AddRepos(userProfile);

            return userProfile;
        }

        private void AddRepos(UserProfile userProfile)
        {
            var repos = _gitHubApiService.GetUserRepoListWithUrl(userProfile.Repos_Url);
            var top5 = repos.OrderByDescending(r => r.Stargazers_Count).Take(5).ToList();

            userProfile.Repos = top5;
        }
    }
}