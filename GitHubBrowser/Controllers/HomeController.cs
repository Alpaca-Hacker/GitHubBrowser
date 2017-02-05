using System.Web.Mvc;
using GitHubBrowser.Models;
using GitHubBrowser.Services.Interfaces;

namespace GitHubBrowser.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGitHubService _gitHubService;

        public HomeController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserProfile userProfile)
        {
            var model =_gitHubService.GetUserProfile(userProfile.Login);
            return View(model);
        }
    }
}