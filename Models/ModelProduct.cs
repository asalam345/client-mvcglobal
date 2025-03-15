using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvcglobal.Models
{
    public class ModelProduct
    {
        public string IEnumerable;
        public string UnitId { get; set; }
        public string UnitName { get; set; }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string ParentId { get; set; }
        public string ParentName { get; set; }
        public List<ModelProduct> CategoryInfo { get; set; }

    }

    public class ModelItem
    {
        public string IEnumerable;
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemSerial { get; set; }
        public string ItemBrand { get; set; }
        public string ItemBalance { get; set; }
        public string ItemShortCode { get; set; }

        public string ItemStatusId { get; set; }
        public string ItemStatusName { get; set; }
        public string ItemTypeId { get; set; }
        public string ItemTypeName { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }

        public string PurchasePrice { get; set; }
        public string SalesPrice { get; set; }
        public string CreationBy { get; set; }

        public List<ModelItem> ItemInfo { get; set; }

    }

    public class ListCategoryViewModel
    {
        public List<ModelProduct> CategoryInfo { get; set; }
    }





}