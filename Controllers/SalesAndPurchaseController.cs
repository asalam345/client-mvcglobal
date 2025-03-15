using mvcglobal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvcglobal.Controllers
{
    public class SalesAndPurchaseController : Controller
    {
        clsServiceHandler objServiceHandler = new clsServiceHandler();
        public ActionResult Sales()
        {
            return View();
        }

        public ActionResult Purchase()
        {
            try
            {
                DataSet ds = new DataSet();
                string strSql = "SELECT * FROM TABLE_ITEM";
                ds = objServiceHandler.ExecuteQuery(strSql);
                List<SelectListItem> li = new List<SelectListItem>();
                foreach (DataRow prow in ds.Tables["Table1"].Rows)
                {
                    li.Add(new SelectListItem
                    {
                        Value = prow["ITEM_ID"].ToString(),
                        Text = prow["ITEM_NAME"].ToString()
                    });
                }
                ViewBag.ITEM = li;

                ds = new DataSet();
                strSql = "SELECT * FROM TABLE_SUPPLIER";
                ds = objServiceHandler.ExecuteQuery(strSql);
                li = new List<SelectListItem>();
                foreach (DataRow prow in ds.Tables["Table1"].Rows)
                {
                    li.Add(new SelectListItem
                    {
                        Value = prow["SUPPLIER_ID"].ToString(),
                        Text = prow["SUPPLIER_NAME"].ToString()
                    });
                }
                ViewBag.SUPLIER = li;

                return View();
            }
            catch
            {
                return View("Something went wrong !");
            }

        }
        [HttpPost]
        public JsonResult Purchase(Purchase purchase, List<PurchaseDetail> purchaseDetail)
        {
            clsPurchaseHandler objClsPurchase = new clsPurchaseHandler();
            purchase.CreatedBy = Convert.ToDecimal(Session["USER_ID"]);
            objClsPurchase.EntryPurchase(purchase, purchaseDetail);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult SalesReturn()
        {
            return View();
        }

        public ActionResult PurchaseReturn()
        {
            return View();
        }
    }
}