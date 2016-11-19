using LinksManager.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkManager.Entities;

namespace LinksManager.Repositories.Concrete
{
    public class LinksRepository : ILinksRepository
    {
        private static readonly List<LinkEntity> links;
        private static int lastGeneratedId;

        static LinksRepository()
        {
            links = new List<LinkEntity>()
            {
                new LinkEntity() { Id = 1, LinkName = "http://fb.com", Date = new DateTime(2015, 02, 02) },
                new LinkEntity() { Id = 2, LinkName = "http://twitter.com", Date = new DateTime(2015, 02, 02) },
                new LinkEntity() { Id = 3, LinkName = "http://hackerrank.com", Date = new DateTime(2015, 12, 02) },
                new LinkEntity() { Id = 4, LinkName = "http://microsoft.com", Date = new DateTime(2016, 02, 02) },
                new LinkEntity() { Id = 5, LinkName = "https://github.com", Date = new DateTime(2015, 02, 02) },
                new LinkEntity() { Id = 6, LinkName = "https://translate.google.com.ua/", Date = new DateTime(2016, 06, 06) },
                new LinkEntity() { Id = 7, LinkName = "https://www.dropbox.com", Date = new DateTime(2014, 12, 02) },
                new LinkEntity() { Id = 8, LinkName = "https://play.google.com", Date = new DateTime(2016, 05, 02) },
            };

            lastGeneratedId = 4;
        }

        private int GenerateNextId()
        {
            lastGeneratedId += 1;
            return lastGeneratedId;
        }
        
        public LinkEntity Add(LinkEntity link)
        {
            link.Id = GenerateNextId();
            link.Date = DateTime.Now;
            links.Add(link);
            return link;
        }

        public LinkEntity Edit(LinkEntity link)
        {
            var existingLink = links
                .Select((l, i) => new { LinkName = l, Index = i })
                .Where(x => x.LinkName.Id == link.Id)
                .Single();
            links[existingLink.Index] = link;
            return link;
        }

        public IEnumerable<LinkEntity> GetLinks()
        {
            return links;
        }

        public void Remove(int id)
        {
            var existingLink = links
                .Where(r => r.Id == id)
                .Single();

            links.Remove(existingLink);
        }
    }
}