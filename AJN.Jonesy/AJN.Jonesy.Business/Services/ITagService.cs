using AJN.Jonesy.Model;

namespace AJN.Jonesy.Business.Services {
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public interface ITagService {
        Collection<Tag> Get(IEnumerable<int> ids);
    }
}