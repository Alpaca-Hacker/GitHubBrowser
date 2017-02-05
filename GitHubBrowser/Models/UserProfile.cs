using System.Collections.Generic;

namespace GitHubBrowser.Models
{
    public class UserProfile
    {

        public string Login { get; set; }
        public string Avatar_Url { get; set; }
        public string Html_Url { get; set; }
        public string Repos_Url { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public List<Repo> Repos { get; set; } 
    }

}