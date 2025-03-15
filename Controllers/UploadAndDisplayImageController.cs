using mvcglobal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvcglobal.Controllers
{
    public class UploadAndDisplayImageController : Controller
    {
        clsServiceHandler objServicehandler = new clsServiceHandler();
        public ActionResult UploadImage()
        {
            LoadItemData();
            return View();
        }

        public ActionResult LoadItemData()
        {
            try
            {
                DataSet ds = new DataSet();
                String strSql = "SELECT ITEM_ID,ITEM_NAME FROM TABLE_ITEM";
                ds = objServicehandler.ExecuteQuery(strSql);
                List<SelectListItem> li = new List<SelectListItem>();
                foreach (DataRow prow in ds.Tables["Table1"].Rows)
                {
                    li.Add(new SelectListItem
                    {
                        Value = prow["ITEM_ID"].ToString(),
                        Text = prow["ITEM_NAME"].ToString()
                    });
                }
                ViewBag.ItemList = li;
                return View();
            }
            catch
            {
                return View("Something went wrong !");
            }

        }

        [HttpPost]
        public JsonResult AddImage(UploadImages ImageDetails)
        {
            try
            {
                string strSql = "";
                var profile = Request.Files;

                string imgname = string.Empty;
                string ImageName = string.Empty;

                int ItemId = ImageDetails.ItemId;
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var logo = System.Web.HttpContext.Current.Request.Files["file"];
                    if (logo.ContentLength > 0)
                    {
                        var profileName = Path.GetFileName(logo.FileName);
                        var ext = Path.GetExtension(logo.FileName);

                        ImageName = profileName;
                        var comPath = Server.MapPath("/UploadItemImages/") + DateTime.Now.ToString("ddmmyyyy") + ImageName;

                        logo.SaveAs(comPath);
                        strSql = objServicehandler.AddImage(ItemId, comPath);
                    }

                }
                int response = 0;
                if (strSql.Equals("Success"))
                {
                    response = 1;
                }
                else
                {
                    response = 2;
                }


                //int response = 1;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went worng !", JsonRequestBehavior.AllowGet);
            }
        }

    }
}