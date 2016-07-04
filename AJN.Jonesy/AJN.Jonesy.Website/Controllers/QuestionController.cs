
namespace AJN.Jonesy.Website.Controllers {
    using System.Web.Mvc;
    using Business.Services;

    public class QuestionController
        : Controller {

        public QuestionController(IQuestionService questionService) {
            _questionService = questionService;
        }

        [Route("{id}/{text}")]
        public ActionResult Get(int id) {

            var question = _questionService.Get(id);

            return View(question);
        }

        private readonly IQuestionService _questionService;
    }
}