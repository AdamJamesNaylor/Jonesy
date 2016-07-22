
namespace AJN.Jonesy.Website.Controllers {
    using System;
    using System.Web.Mvc;
    using Business;
    using Business.Services;
    using Model;

    public class QuestionController
        : Controller {

        public QuestionController(IQuestionService questionService) {
            _questionService = questionService;
        }

        [Route("questions/{id}/{text}")]
        public ActionResult Get(int id) {

            var question = _questionService.Get(id);

            if (question == null)
                return HttpNotFound();

            if (question.IsCanonical)
                return View(question);

            question = _questionService.Get(question.CanonicalQuestionId);
            var authority = Request.Url.GetLeftPart(UriPartial.Authority);
            return RedirectPermanent(authority + QuestionUrlParser.Generate(question));
        }

        [Route("redirects/{id}/{text}")]
        public ActionResult GetRedirects(int id) {
            //<link rel=canonical href=“example.com/cupcake.html” />
            var question = _questionService.Get(id);

            return View("redirect", question);
        }

        [Route("questions/add/{year}/{month}/{day}")]
        public ActionResult Add(string year, string month, string day) {
            if (DateTime.Now.ToString("yyyyMMdd") != year + month + day)
                return HttpNotFound();

            return View();
        }

        [HttpPost]
        [Route("questions/add")]
        public ActionResult Add(Question model) {
            var result = _questionService.Add(model);
            
            return View(result);
        }

        private readonly IQuestionService _questionService;
    }
}