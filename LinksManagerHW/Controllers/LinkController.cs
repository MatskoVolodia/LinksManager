using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinksManager.Repositories.Concrete;
using Newtonsoft.Json;
using LinksManager.Repositories.Abstract;
using LinksManagerHW.Models;
using LinkManager.Entities;
using PagedList;

namespace LinksManagerHW.Controllers
{
    public class LinkController : Controller
    {
        private readonly ILinksRepository _linksRepository;

        private static int pageSize;
        private static int pageNumber;
        private static IEnumerable<LinkEntity> currentLinks;

        public LinkController()
        {
            _linksRepository = new LinksRepository();
        }
        // GET: Link
        [HttpGet]
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            currentLinks = _linksRepository.GetLinks();

            if (!String.IsNullOrEmpty(searchString))
            {
                currentLinks = currentLinks.Where(s => s.LinkName.ToUpper().Contains(searchString.ToUpper()));
            }

            pageSize = 3;
            pageNumber = (page ?? 1);

            var OnePageOfLinks = currentLinks.ToPagedList(pageNumber, pageSize);

            return View(OnePageOfLinks);
        }

        [HttpGet]
        public JsonResult GetLinks()
        {
            var links = currentLinks.ToPagedList(pageNumber, pageSize);

            var temp = JsonConvert.SerializeObject(links);
            return Json(links, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddLink(LinkModel link)
        {
            LinkEntity linkEntity = (LinkEntity)link;
            LinkEntity addedLinkEntity = _linksRepository.Add(linkEntity);

            return Json(addedLinkEntity);
        }

        [HttpPost]
        public ActionResult EditLink(LinkModel link)
        {
            LinkEntity linkEntity = (LinkEntity)link;
            LinkEntity editedLinkEntity = _linksRepository.Edit(linkEntity);

            return Json(editedLinkEntity);
        }

        [HttpPost]
        public ActionResult RemoveLink(LinkModel link)
        {
            _linksRepository.Remove(link.Id);

            return new EmptyResult();
        }
    }
}