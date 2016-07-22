
namespace AJN.Jonesy.Business.Services {
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Model;

    public class TagService : ITagService
    {
        public TagService(string appDataPath) {
            _appDataPath = appDataPath;
        }

        public Collection<Tag> Get(IEnumerable<int> ids) {
            string file = Path.Combine(_appDataPath, "tags.xml");
            var allTags = XElement.Load(file);

            var tags = allTags.Descendants("tag").Where(t => ids.Contains(int.Parse(t.Attribute("id").Value)));

            var result = new Collection<Tag>();
            foreach (var tagXml in tags) {
                var tag= new Tag {
                    Id = int.Parse(tagXml.Attribute("id").Value),
                    Name = tagXml.Attribute("name").Value,
                    Description = tagXml.Element("description").Value
                };

                var icon = tagXml.Element("icon");
                if (icon != null) {
                    tag.Icon = new Icon {
                        Source = icon.Attribute("src").Value,
                        CssClass = icon.Attribute("class").Value,
                    };
                }
                result.Add(tag);
            }
            return result;
        }

        private readonly string _appDataPath;
    }
}