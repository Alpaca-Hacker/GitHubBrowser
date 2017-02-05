
using System.Collections.Generic;
using GitHubBrowser.Models;

namespace GitHubBrowser.Services.Interfaces
{
    public interface IGitHubApiService
    {
        /// <summary>
        /// Gets the user profile from the Github API
        /// </summary>
        /// <param name="username">Login of github user</param>
        /// <returns></returns>
        UserProfile GetUserProfile(string username);

        /// <summary>
        /// Gets all the user's repos when suppied with a login
        /// </summary>
        /// <param name="username">Login of github user</param>
        /// <returns></returns>
        IList<Repo> GetUserRepoList(string username);

        /// <summary>
        /// Gets all the user's repos when suppied with a repo url
        /// </summary>
        /// <param name="url">Repo url of a user</param>
        /// <returns></returns>
        IList<Repo> GetUserRepoListWithUrl(string url);
    }
}
