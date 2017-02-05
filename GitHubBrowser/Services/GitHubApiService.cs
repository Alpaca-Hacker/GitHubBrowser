using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;
using GitHubBrowser.Helpers.Interfaces;
using GitHubBrowser.Models;
using GitHubBrowser.Services.Interfaces;

namespace GitHubBrowser.Services
{
    public class GitHubApiService : IGitHubApiService
    {
        private readonly IConfigHelper _config;

        public GitHubApiService(IConfigHelper config)
        {
            _config = config;
        }

        public UserProfile GetUserProfile(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            var userJson = CallApi(username);
            var userProfile = new JavaScriptSerializer().Deserialize<UserProfile>(userJson);
            return userProfile;
        }

        public IList<Repo> GetUserRepoList(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            var userJson = CallApi(username);
            var userProfile = new JavaScriptSerializer().Deserialize<UserProfile>(userJson);

            return GetUserRepoListWithUrl(userProfile.Repos_Url);
        }

        public IList<Repo> GetUserRepoListWithUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }
            var repoString = CallApi(url);
            var repos = new JavaScriptSerializer().Deserialize<List<Repo>>(repoString);
            
            return repos ?? new List<Repo>();
        }

        private string CallApi(string url)
        {
            var baseUrl = _config.Get("GithubAPIUrl");

            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("User-Agent", "SuperGent");
                webClient.BaseAddress = baseUrl;
                string response;
                try
                {
                    response = webClient.DownloadString(url);
                }
                catch (WebException e)
                {
                    var errorResponse = e.Response as HttpWebResponse;
                    if (errorResponse.StatusCode != HttpStatusCode.NotFound)
                    {
                        throw;
                    }
                    response = string.Empty;
                }
                return response;
            }
        }
    }
}