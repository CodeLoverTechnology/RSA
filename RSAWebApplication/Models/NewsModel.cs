using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSAWebApplication.Models
{
    public class NewsModel
    {
        public int NewsID { get; set; }
        public int NewsTypeID { get; set; }
        public string NewsType { get; set; }
        public int NewsCategoryID { get; set; }
        public string NewsCategory { get; set; }
        public int NewsSubCategoryID { get; set; }
        public string NewsSubCategory { get; set; }
        public string NewsHeading { get; set; }
        public string SubHeading { get; set; }
        public string Para1 { get; set; }
        public string Para2 { get; set; }
        public string Para3 { get; set; }
        public string Para4 { get; set; }
        public string Para5 { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
        public string Img4 { get; set; }
        public string Img5 { get; set; }
        public string Remarks { get; set; }
        public string Location { get; set; }
        public int NoOfNewsViews { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public bool Active { get; set; }

    }
}