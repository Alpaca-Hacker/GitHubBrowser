using GitHubBrowser.Models;

namespace GitHubBrowser.Services.Interfaces
{
    public interface IGitHubService
    {
        /// <summary>
        /// Get the user profile of a login
        /// </summary>
        /// <param name="username">Github login to search</param>
        /// <returns>UserProfile with a list of the top 5 repos</returns>
        UserProfile GetUserProfile(string username);
    }
}
