
namespace AJN.Jonesy.Website.Controllers {
    using System.Web.Mvc;
    using Business.Services;
    using Models;

    public class HomeController
        : Controller {

        public HomeController(IQuestionService questionService) {
            _questionService = questionService;
        }

        public ActionResult Index() {

            var model = new HomeViewModel {
                PopularQuestions = _questionService.GetPopularQuestions()
            };

            return View(model);
        }

        public SitemapResult Sitemap() {
            var questions = _questionService.List();
            return new SitemapResult(questions);
        }

        private readonly IQuestionService _questionService;
    }
}