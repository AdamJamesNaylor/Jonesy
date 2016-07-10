namespace AJN.Jonesy.Website.Controllers {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Web.Mvc;
    using System.Xml;
    using System.Xml.Linq;
    using Business;
    using Model;

    public class SitemapResult
        : ActionResult {

        public SitemapResult(Collection<Question> questions) {
            if (questions == null)
                throw new ArgumentNullException(nameof(questions));
            _questions = questions;
        }

        public override void ExecuteResult(ControllerContext context) {
            var sitemap = new XDocument(new XDeclaration("1.0", "UTF-8", ""));

            var port = context.HttpContext.Request.Url.Port.ToString();
            port = port == "80" ? "" : ":" + port;

            var host = context.HttpContext.Request.Url.Scheme + "://" + context.HttpContext.Request.Url.Host + port;

            var urlSet = new XElement(_nsSitemap + "urlset", new XAttribute("xmlns", _nsSitemap),
                GenerateNode(host, "/"),
                GenerateQuestionNodes(host));

            sitemap.Add(urlSet);

            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = "text/xml";

            using (var xmlWriter = new XmlTextWriter(context.HttpContext.Response.Output)) {
                sitemap.WriteTo(xmlWriter);
            }
        }

        private XElement GenerateNode(string host, string s) {
            return new XElement(_nsSitemap + "url",
                new XElement(_nsSitemap + "loc", host + s),
                new XElement(_nsSitemap + "lastmod", "2016-07-07T20:34+00:00"),
                new XElement(_nsSitemap + "changefreq", "yearly"),
                new XElement(_nsSitemap + "priority", "0.2"));
        }

        private IEnumerable<XElement> GenerateQuestionNodes(string host) {
            return from question in _questions
                select new XElement(_nsSitemap + "url",
                    new XElement(_nsSitemap + "loc", host + QuestionUrlParser.Generate(question)),
                    GetLastModified(question),
                    new XElement(_nsSitemap + "changefreq", "yearly"),
                    new XElement(_nsSitemap + "priority", "0.5"));
        }

        private XElement GetLastModified(Question question) {
            if (question.Audit == null || question.Audit.Modified == null)
                return null;

            return new XElement(_nsSitemap + "lastmod", question.Audit.Modified.On.ToString("yyyy-MM-ddTHH:mm+00:00"));
        }

        private readonly Collection<Question> _questions;
        private XNamespace _nsSitemap = "http://www.sitemaps.org/schemas/sitemap/0.9";
        private XNamespace _nsImage = "http://www.google.com/schemas/sitemap-image/1.1";
        private XNamespace _nsVideo = "http://www.google.com/schemas/sitemap-video/1.1";
    }
}