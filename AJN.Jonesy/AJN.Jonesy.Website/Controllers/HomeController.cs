
namespace AJN.Jonesy.Website.Controllers {
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Web.Mvc;
    using System.Xml;
    using System.Xml.Linq;
    using Business;
    using Business.Services;
    using Model;
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

    public class SitemapResult
        : ActionResult {

        public SitemapResult(Collection<Question> questions) {
            if (questions == null)
                throw new ArgumentNullException(nameof(questions));
            _questions = questions;
        }

        public override void ExecuteResult(ControllerContext context) {

            XNamespace nsSitemap = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XNamespace nsImage = "http://www.google.com/schemas/sitemap-image/1.1";
            XNamespace nsVideo = "http://www.google.com/schemas/sitemap-video/1.1";

            var sitemap = new XDocument(new XDeclaration("1.0", "UTF-8", ""));

            var port = context.HttpContext.Request.Url.Port.ToString();
            port = port == "80" ? "" : ":" + port;

            var host = context.HttpContext.Request.Url.Scheme + "://" + context.HttpContext.Request.Url.Host + port;

            var urlSet = new XElement(nsSitemap + "urlset", new XAttribute("xmlns", nsSitemap),
                from question in _questions
                select new XElement("url",
                    new XElement("loc", host + QuestionUrlParser.Generate(question)),
                    GetLastModified(question),
                    new XElement("changefreq", "yearly"),
                    new XElement("priority", "0.5")));

            sitemap.Add(urlSet);

            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = "text/xml";

            using (var xmlWriter = new XmlTextWriter(context.HttpContext.Response.Output)) {
                sitemap.WriteTo(xmlWriter);
            }
        }

        private XElement GetLastModified(Question question) {
            if (question.Audit == null || question.Audit.Modified == null)
                return null;

            return new XElement("lastmod", question.Audit.Modified.On.ToString("yyyy-MM-ddTHH:mm:ss"));
        }

        private readonly Collection<Question> _questions;
    }
}