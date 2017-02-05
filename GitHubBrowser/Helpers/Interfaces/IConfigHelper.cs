
namespace GitHubBrowser.Helpers.Interfaces
{   
    public interface IConfigHelper
    {
        /// <summary>
        /// Gets the value of the key from Web.Config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        
        string Get(string key);
    }
}
