using LinkManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinksManager.Repositories.Abstract
{
    public interface ILinksRepository
    {
        IEnumerable<LinkEntity> GetLinks();

        LinkEntity Add(LinkEntity link);

        LinkEntity Edit(LinkEntity link);

        void Remove(int id);
    }
}
