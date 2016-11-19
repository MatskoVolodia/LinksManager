using LinkManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinksManagerHW.Models
{
    public class LinkModel
    {
        public int Id { get; set; }
        public string LinkName { get; set; }
        public DateTime Date { get; set; }

        public static explicit operator LinkEntity(LinkModel linkModel)
        {
            LinkEntity linkEntity = new LinkEntity()
            {
                Id = linkModel.Id,
                LinkName = linkModel.LinkName,
                Date = linkModel.Date
            };
            return linkEntity;
        }

        public static explicit operator LinkModel(LinkEntity linkEntity)
        {
            LinkModel linkModel = new LinkModel()
            {
                Id = linkEntity.Id,
                LinkName = linkEntity.LinkName,
                Date = linkEntity.Date
            };
            return linkModel;
        }
    }
}