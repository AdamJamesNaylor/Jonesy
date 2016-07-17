
namespace AJN.Jonesy.Website.Controllers {
    using System;
    using System.Web.Mvc;
    using Business;
    using Business.Services;

    public class QuestionController
        : Controller {

        public QuestionController(IQuestionService questionService) {
            _questionService = questionService;
        }

        [Route("questions/{id}/{text}")]
        public ActionResult Get(int id) {

            var question = _questionService.Get(id);

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

        private readonly IQuestionService _questionService;
    }
}