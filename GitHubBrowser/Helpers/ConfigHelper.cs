using System;
using System.Configuration;
using GitHubBrowser.Helpers.Interfaces;

namespace GitHubBrowser.Helpers
{
    public class ConfigHelper : IConfigHelper
    {
        public string Get(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

    }
}