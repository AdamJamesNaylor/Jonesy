
namespace AJN.Jonesy.Scraper {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Supremes;
    using Supremes.Nodes;

    internal class Program {
        private static readonly Uri _mcWikiDomain = new Uri("http://minecraft.gamepedia.com/");

        private static void Main(string[] args) {

            var categoryLinks = DownloadCategoryLinks("Aggressive_mobs");

            List<string> t = new List<string>();
            foreach (var link in categoryLinks) {
                var page = DownloadPage(link);

                string questionText = "How much health does a " + link.Text + " have?";
                var infobox = page.Select("div.notaninfobox").First;
                foreach (var tr in infobox.Select("tr")) {
                    if (!tr.Select("a[title=Health]").Any())
                        continue;

                    string answer = tr.Select("td").First.Select("span").Text.Split(' ')[0];
                    t.Add(questionText + " " + answer);
                    break;
                }

            }
        }

        private static Document DownloadPage(Element link) {
            return Dcsoup.Parse(new Uri(_mcWikiDomain, link.Attr("href")), 5000);
        }

        private static Elements DownloadCategoryLinks(string category) {
            
            var doc = Dcsoup.Parse(new Uri(_mcWikiDomain, "/Category:" + category), 5000);

            return doc.Select("div.mw-category").First.Select("a");
        }
    }
}