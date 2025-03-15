using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using mvcglobal.Models;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;


namespace mvcglobal.Controllers
{
  
    public class ProductController : Controller
    {
        clsServiceHandler objServiceHandler = new clsServiceHandler();
        public ActionResult Index()
        {
            return View();
        }



        #region Unit
        public ActionResult Unit()
        {
            ShowUnitDataLoadToGrid();
            return View();
        }

        public ActionResult ShowUnitDataLoadToGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                String strSql = "SELECT UNIT_ID,UNIT_NAME FROM TABLE_UNIT";

                ds = objServiceHandler.ExecuteQuery(strSql);
                var model = new List<ModelProduct>();
                {
                    foreach (DataRow prow in ds.Tables["Table1"].Rows)
                    {
                        var UnitList = new ModelProduct();
                        UnitList.UnitId = prow["UNIT_ID"].ToString();
                        UnitList.UnitName = prow["UNIT_NAME"].ToString();
                        model.Add(UnitList);
                    }

                    ViewBag.UnitInfo = model;
                }
                return View(ViewBag.UnitInfo);
            }
            catch
            {
                return View("Something went wrong !");
            }
        }

        [HttpPost]
        public JsonResult ShowUnitId(string strUnitId)
        {
            try
            {
                string DataAddes = objServiceHandler.ReturnString("SELECT MAX(UNIT_ID)+1 FROM TABLE_UNIT");

                return Json(DataAddes, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong !");
            }
        }

        [HttpPost]
        public JsonResult AddUnit(string strUnitId, string strUnitName)
        {
            try
            {
                if (strUnitName.Equals(""))
                {
                    return Json("Please input Unit Name", JsonRequestBehavior.AllowGet);
                }

                string DataAddes = objServiceHandler.AddUnit(strUnitId, strUnitName);
                return Json(DataAddes, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong !", JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult UpdateUnitData(string UnitId, string UnitName)
        {
            String strSQL = "";
            try
            {

                if (UnitId.Equals(""))
                {
                    return Json("Please input Category Id", JsonRequestBehavior.AllowGet);
                }
                if (UnitName.ToString() == null)
                {
                    return Json("Please input Category Name ", JsonRequestBehavior.AllowGet);
                }


                strSQL = objServiceHandler.UpdatUnit(UnitId, UnitName);

                return Json(strSQL, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteUnitData(string UnitId)
        {
            String strSQL = "";
            try
            {

                if (UnitId.Equals(""))
                {
                    return Json("Please Input Unit Id ", JsonRequestBehavior.AllowGet);
                }
                strSQL = objServiceHandler.DeleteUnit(UnitId);

                return Json(strSQL, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong", JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
        public ActionResult Category()
        {
            ShowCategoryDataLoadToGrid();
            ShowSubCategoryDataLoadToGrid();
            LoadComboData();
            return View();
        }


        #region Category

        public ActionResult ShowCategoryDataLoadToGrid()
        {
            //if (Session["SubCategory"]==null)
            //{
            //    Session["Category"] = "Yes";
            //}
            try
            {
                DataSet ds = new DataSet();
                String strSql = "SELECT * FROM TABLE_CATEGORY WHERE(PARENT_ID = 0 OR PARENT_ID IN (SELECT CATEGORY_ID FROM TABLE_CATEGORY WHERE PARENT_ID = 0))";

                ds = objServiceHandler.ExecuteQuery(strSql);
                var model = new List<ModelProduct>();
                {
                    foreach (DataRow prow in ds.Tables["Table1"].Rows)
                    {
                       var CategoryList = new ModelProduct();
                       CategoryList.CategoryId = prow["CATEGORY_ID"].ToString();
                       CategoryList.CategoryName = prow["CATEGORY_NAME"].ToString();
                        CategoryList.ParentId = prow["PARENT_ID"].ToString();
                        model.Add(CategoryList);
                    }

                    ViewBag.CategoryInfo = model;
                }
                return View(ViewBag.CategoryInfo);
            }
            catch
            {
                return View("Something went wrong !");
            }
        }

        [HttpPost]
        public JsonResult ShowCategoryId(string strCategoryId)
        {
            try
            {
                string DataAddes = objServiceHandler.ReturnString("SELECT MAX(CATEGORY_ID)+1  FROM TABLE_CATEGORY");

                return Json(DataAddes, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong !");
            }
        }

        [HttpPost]
        public JsonResult AddCategory(string strCategoryId, string strCategoryName)
        {
            try
            {
                if (strCategoryName.Equals(""))
                {
                    return Json("Please input Category Name", JsonRequestBehavior.AllowGet);
                }

                string DataAddes = objServiceHandler.AddCategory(strCategoryId, strCategoryName);
                Session["Category"] = "Yes";
                Session["SubCategory"] = null;
                return Json(DataAddes, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong !", JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult UpdateCategoryData(string CategoryId, string CategoryName)
        {
            String strSQL = "";
            try
            {

                if (CategoryId.Equals(""))
                {
                    return Json("Please insert Category Id", JsonRequestBehavior.AllowGet);
                }
                if (CategoryName.ToString() == null)
                {
                    return Json("Please input Category Name ", JsonRequestBehavior.AllowGet);
                }


                strSQL = objServiceHandler.UpdatCategory(CategoryId, CategoryName);

                Session["Category"] = "Yes";
                Session["SubCategory"] = null;
                return Json(strSQL, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteCategoryData(string CategoryId)
        {
            String strSQL = "";
            try
            {

                if (CategoryId.Equals(""))
                {
                    return Json("Please Input Category Id ", JsonRequestBehavior.AllowGet);
                }
                strSQL = objServiceHandler.DeleteCategory(CategoryId);

                Session["Category"] = "Yes";
                Session["SubCategory"] = null;

                return Json(strSQL, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong", JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region SubCategory

        public ActionResult LoadComboData()
        {
            ModelProduct ObjCategory = new ModelProduct();
            try
            {
                DataSet ds = new DataSet();
                String strSql = "SELECT * FROM TABLE_CATEGORY";

                ds = objServiceHandler.ExecuteQuery(strSql);
                var model = new List<ModelProduct>();
                {
                    foreach (DataRow prow in ds.Tables["Table1"].Rows)
                    {
                        var CategoryList = new ModelProduct();
                        CategoryList.CategoryId = prow["CATEGORY_ID"].ToString();
                        CategoryList.CategoryName = prow["CATEGORY_NAME"].ToString();
                        CategoryList.ParentId = prow["PARENT_ID"].ToString();
                        model.Add(CategoryList);
                    }

                    ObjCategory.CategoryInfo = model;
                }
                return View(ObjCategory);
            }
            catch
            {
                return View("Something went wrong !");
            }

        }
        public ActionResult ShowSubCategoryDataLoadToGrid()
        {
           // Session["SubCategory"] = "";

            try
            {
                DataSet ds = new DataSet();
                String strSql = "SELECT *, P.CATEGORY_NAME ParentName FROM TABLE_CATEGORY C " +
"INNER JOIN TABLE_CATEGORY P ON C.PARENT_ID = P.CATEGORY_ID " +
"WHERE(C.PARENT_ID != 0 AND C.PARENT_ID NOT IN(SELECT CATEGORY_ID FROM TABLE_CATEGORY WHERE PARENT_ID = 0))";

                ds = objServiceHandler.ExecuteQuery(strSql);
                var model = new List<ModelProduct>();
                {
                    foreach (DataRow prow in ds.Tables["Table1"].Rows)
                    {
                        var SubCategoryList = new ModelProduct();
                        //SubCategoryList.SubCategoryId = prow["SUB_CATEGORY_ID"].ToString();
                        //SubCategoryList.SubCategoryName = prow["SUB_CATEGORY_NAME"].ToString();
                        SubCategoryList.CategoryId= prow["CATEGORY_ID"].ToString();
                        SubCategoryList.CategoryName = prow["CATEGORY_NAME"].ToString();
                        SubCategoryList.ParentId = prow["PARENT_ID"].ToString();
                        SubCategoryList.ParentName = prow["ParentName"].ToString();
                        model.Add(SubCategoryList);
                    }

                    ViewBag.SubCategoryInfo = model;
                }
                return View(ViewBag.SubCategoryInfo);
            }
            catch
            {
                return View("Something went wrong !");
            }
        }

        [HttpPost]
        public JsonResult ShowSubCategoryId(string strSubCategoryId)
        {
            try
            {
                string DataAddes = objServiceHandler.ReturnString("SELECT MAX(ISNULL(SUB_CATEGORY_ID,0))+1  FROM TABLE_SUB_CATEGORY");

                return Json(DataAddes, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong !");
            }
        }

        [HttpPost]
        public JsonResult AddSubCategory(string strSubCategoryId, string strSubCategoryName,string strCategoryId)
        {
            try
            {
                if (strSubCategoryName.Equals(""))
                {
                    return Json("Please input Category Name", JsonRequestBehavior.AllowGet);
                }

                string DataAddes = objServiceHandler.AddSubCategory(strSubCategoryId, strSubCategoryName,strCategoryId);
                Session["SubCategory"] = "Yes";
                Session["Category"] = null;
                return Json(DataAddes, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json("Something went wrong !", JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult UpdateSubCategoryData(string SubCategoryId, string SubCategoryName,string CategoryId)
        {
            String strSQL = "";
            try
            {

                if (SubCategoryId.Equals(""))
                {
                    return Json("Please insert SubCategory Id", JsonRequestBehavior.AllowGet);
                }
                if (SubCategoryName.ToString() == null)
                {
                    return Json("Please input SubCategory Name ", JsonRequestBehavior.AllowGet);
                }


                strSQL = objServiceHandler.UpdatSubCategory(SubCategoryId, SubCategoryName,CategoryId);
                Session["SubCategory"] = "Yes";
                Session["Category"] = null;

                return Json(strSQL, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteSubCategoryData(string SubCategoryId)
        {
            String strSQL = "";
            try
            {

                if (SubCategoryId.Equals(""))
                {
                    return Json("Please Input SubCategory Id ", JsonRequestBehavior.AllowGet);
                }
                strSQL = objServiceHandler.DeleteSubCategory(SubCategoryId);
                Session["SubCategory"] = "Yes";
                Session["Category"] = null;

                return Json(strSQL, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong", JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

        #region Item
        public ActionResult Item()
        {
            LoadItemData();
            LoadSubCategoryData();
            LoadUnitData();
            LoadItemTypeData();
            LoadItemStatusData();
            return View();
        }


        [HttpPost]
        public JsonResult ShowItemId(string strItemId)
        {
            try
            {
                string DataAddes = objServiceHandler.ReturnString("SELECT MAX(ITEM_ID)+1  FROM TABLE_ITEM");

                return Json(DataAddes, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong !");
            }
        }

        public ActionResult LoadItemData()
        {
            try
            {
                DataSet ds = new DataSet();
                //String strSql = "SELECT ITEM_ID,ITEM_NAME,ITEM_BARCODE,ITEM_BRAND,TI.UNIT_ID,TU.UNIT_NAME,TI.CATEGORY_ID,TSC.SUB_CATEGORY_NAME,TI.ITEM_TYPE_ID,TIT.ITEM_TYPE_NAME,TI.ITEM_STATUS_ID,TIS.ITEM_STATUS_NAME,ITEM_BALANCE,CREATION_BY FROM TABLE_ITEM TI,TABLE_ITEM_STATUS TIS,TABLE_SUB_CATEGORY TSC,TABLE_UNIT TU,TABLE_ITEM_TYPE TIT WHERE TI.UNIT_ID=TU.UNIT_ID AND TI.CATEGORY_ID=TSC.SUB_CATEGORY_ID AND TI.ITEM_TYPE_ID=TIT.ITEM_TYPE_ID AND TI.ITEM_STATUS_ID=TIS.ITEM_STATUS_ID";
                string strSql = " SELECT I.ITEM_NAME AS Name,I.CATEGORY_ID CategoryId,I.UNIT_ID UnitId, U.UNIT_NAME, C.CATEGORY_NAME ,ISNULL(PICTURE_NAME, 'NoImage.png') AS Image,DETAILS as Details," +
 " PUD.ITEM_ID AS Id,ISNULL(D.AMOUNT, 0) Discount,  MAX(SALES_PRICE) Price,ITEM_BRAND,ITEM_BARCODE,CREATION_BY,ITEM_STATUS_NAME," +
 " CASE WHEN D.AMOUNT IS NULL THEN MAX(SALES_PRICE) ELSE MAX(SALES_PRICE) -D.AMOUNT END AS DiscountedPrice,SUM(PURCHASE_PRICE)/SUM(PURCHASE_QTY)PurchasePrice, SUM(REMAIN_QTY) AS Quantity" +
 " FROM TABLE_ITEM I CROSS APPLY(SELECT  TOP 1 PICTURE_NAME, ITEM_ID FROM ITEM_PICTURE  WHERE   ITEM_PICTURE.ITEM_ID = I.ITEM_ID) IMAGE" +
" LEFT JOIN TABLE_PURCHASE_DETAILS PUD ON PUD.ITEM_ID = I.ITEM_ID LEFT JOIN ITEM_DISCOUNT D ON D.ITEM_ID = I.ITEM_ID" +
"  LEFT JOIN TABLE_UNIT U ON U.UNIT_ID = I.UNIT_ID" +
" LEFT JOIN TABLE_CATEGORY C ON C.CATEGORY_ID = I.CATEGORY_ID" +
" LEFT JOIN TABLE_ITEM_STATUS S ON S.ITEM_STATUS_ID = ISNULL(I.ITEM_STATUS_ID, 1)" +
" WHERE PUD.ITEM_ID IS NOT NULL GROUP BY PUD.ITEM_ID, I.ITEM_NAME, I.CATEGORY_ID,I.UNIT_ID,PICTURE_NAME,DETAILS,D.AMOUNT" +
",UNIT_NAME,CATEGORY_NAME,ITEM_BRAND,ITEM_BARCODE,CREATION_BY,ITEM_STATUS_NAME";
                ds = objServiceHandler.ExecuteQuery(strSql);
                var model = new List<ModelItem>();
                {
                    foreach (DataRow prow in ds.Tables["Table1"].Rows)
                    {
                        var ItemList = new ModelItem();
                        ItemList.ItemId = prow["Id"].ToString();
                        ItemList.ItemName = prow["NAME"].ToString();
                        ItemList.ItemSerial = prow["ITEM_BARCODE"].ToString();
                        ItemList.ItemBrand = prow["ITEM_BRAND"].ToString();
                        ItemList.UnitId = prow["UNITID"].ToString();
                        ItemList.UnitName = prow["UNIT_NAME"].ToString();
                        ItemList.CategoryId = prow["CATEGORYID"].ToString();
                        ItemList.CategoryName= prow["CATEGORY_NAME"].ToString();
                        //ItemList.ItemStatusId = prow["ITEM_STATUS_ID"].ToString();
                        ItemList.ItemStatusName = prow["ITEM_STATUS_NAME"].ToString();
                        //ItemList.ItemTypeId = prow["ITEM_TYPE_ID"].ToString();
                        //ItemList.ItemTypeName = prow["ITEM_TYPE_NAME"].ToString();
                        ItemList.PurchasePrice = prow["PURCHASEPRICE"].ToString();
                        ItemList.SalesPrice = prow["PRICE"].ToString();
                        ItemList.ItemBalance = prow["Quantity"].ToString();
                        ItemList.CreationBy = prow["CREATION_BY"].ToString();

                        model.Add(ItemList);
                    }

                    ViewBag.ItemInfo = model;
                }
                return View(ViewBag.ItemInfo);
            }
            catch
            {
                return View("Somethin went wrong !");
            }
        }

        public ActionResult LoadSubCategoryData()
        {
            try
            {
                DataSet ds = new DataSet();
                String strSql = "SELECT CATEGORY_ID,CATEGORY_NAME FROM TABLE_CATEGORY";
                ds = objServiceHandler.ExecuteQuery(strSql);
                List<SelectListItem> li = new List<SelectListItem>();
                foreach (DataRow prow in ds.Tables["Table1"].Rows)
                {
                    li.Add(new SelectListItem
                    {
                        Value = prow["CATEGORY_ID"].ToString(),
                        Text = prow["CATEGORY_NAME"].ToString()
                    });
                }
                ViewBag.CategoryList = li;
                return View();
            }
            catch
            {
                return View("Something went wrong !");
            }

        }
        public ActionResult LoadUnitData()
        {
            try
            {
                DataSet ds = new DataSet();
                String strSql = "SELECT UNIT_ID,UNIT_NAME FROM TABLE_UNIT";
                ds = objServiceHandler.ExecuteQuery(strSql);
                List<SelectListItem> li = new List<SelectListItem>();
                foreach (DataRow prow in ds.Tables["Table1"].Rows)
                {
                    li.Add(new SelectListItem
                    {
                        Value = prow["UNIT_ID"].ToString(),
                        Text = prow["UNIT_NAME"].ToString()
                    });
                }
                ViewBag.UnitList = li;
                return View();
            }
            catch
            {
                return View("Something went wrong !");
            }



        }
        public ActionResult LoadItemTypeData()
        {
            try
            {
                DataSet ds = new DataSet();
                String strSql = "SELECT ITEM_TYPE_ID,ITEM_TYPE_NAME FROM TABLE_ITEM_TYPE";
                ds = objServiceHandler.ExecuteQuery(strSql);
                List<SelectListItem> li = new List<SelectListItem>();
                foreach (DataRow prow in ds.Tables["Table1"].Rows)
                {
                    li.Add(new SelectListItem
                    {
                        Value = prow["ITEM_TYPE_ID"].ToString(),
                        Text = prow["ITEM_TYPE_NAME"].ToString()
                    });
                }
                ViewBag.ItemTypeList = li;
                return View();
            }
            catch
            {
                return View("Something went wrong !");
            }

        }
        public ActionResult LoadItemStatusData()
        {
            try
            {
                DataSet ds = new DataSet();
                String strSql = "SELECT ITEM_STATUS_ID,ITEM_STATUS_NAME FROM TABLE_ITEM_STATUS";
                ds = objServiceHandler.ExecuteQuery(strSql);
                List<SelectListItem> li = new List<SelectListItem>();
                foreach (DataRow prow in ds.Tables["Table1"].Rows)
                {
                    li.Add(new SelectListItem
                    {
                        Value = prow["ITEM_STATUS_ID"].ToString(),
                        Text = prow["ITEM_STATUS_NAME"].ToString()
                    });
                }
                ViewBag.ItemStatusList = li;
                return View();
            }
            catch
            {
                return View("Something went wrong !");
            }



        }

        [HttpPost]
        public JsonResult AddItem(string ItemId, string ItemName, string ItemSerial,string ItemBrand, string UnitId, string CategoryId, string ItemTypeId, string ItemStatusId, string PurchasePrice, string SalesPrice)
        {
            try
            {
                string strCreationBy = Session["USER_LOGIN_NAME"].ToString();
                string strItemBalance = "0";


                if (ItemId.Equals(""))
                {
                    return Json("Please input Item Id", JsonRequestBehavior.AllowGet);
                }

                if (ItemName.Equals(""))
                {
                    return Json("Please input Item Name", JsonRequestBehavior.AllowGet);
                }

                if (UnitId.Equals(""))
                {
                    return Json("Please Select Product Unit", JsonRequestBehavior.AllowGet);
                }

                if (CategoryId.Equals(""))
                {
                    return Json("Please input Product Category", JsonRequestBehavior.AllowGet);
                }

                if (ItemTypeId.Equals(""))
                {
                    return Json("Please Select Item Type", JsonRequestBehavior.AllowGet);
                }

                if (ItemStatusId.Equals(""))
                {
                    return Json("Please Select Item Status", JsonRequestBehavior.AllowGet);
                }

                string DataAddes = objServiceHandler.AddItem(ItemId,ItemName,ItemSerial,ItemBrand,UnitId,CategoryId,ItemTypeId,ItemStatusId,PurchasePrice,SalesPrice, strItemBalance,strCreationBy);
                return Json(DataAddes, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json("Something went wrong !", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateItemData(string ItemId, string ItemName, string ItemSerial, string ItemBrand, string UnitId, string CategoryId, string ItemTypeId, string ItemStatusId, string PurchasePrice, string SalesPrice)
        {
            String strSQL = "";
            string strCreationBy = Session["USER_LOGIN_NAME"].ToString();

            try
            {

                if (ItemId.Equals(""))
                {
                    return Json("Please input Item Id", JsonRequestBehavior.AllowGet);
                }

                if (ItemName.Equals(""))
                {
                    return Json("Please input Item Name", JsonRequestBehavior.AllowGet);
                }

                if (UnitId.Equals(""))
                {
                    return Json("Please Select Product Unit", JsonRequestBehavior.AllowGet);
                }

                if (CategoryId.Equals(""))
                {
                    return Json("Please input Product Category", JsonRequestBehavior.AllowGet);
                }

                if (ItemTypeId.Equals(""))
                {
                    return Json("Please Select Item Type", JsonRequestBehavior.AllowGet);
                }

                if (ItemStatusId.Equals(""))
                {
                    return Json("Please Select Item Status", JsonRequestBehavior.AllowGet);
                }

                strSQL = objServiceHandler.UpdatItem(ItemId, ItemName, ItemSerial, ItemBrand, UnitId, CategoryId, ItemTypeId, ItemStatusId, PurchasePrice, SalesPrice, strCreationBy);

                return Json(strSQL, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteItemData(string ItemId)
        {
            String strSQL = "";
            try
            {

                if (ItemId.Equals(""))
                {
                    return Json("Please Input User Id ", JsonRequestBehavior.AllowGet);
                }
                strSQL = objServiceHandler.DeleteItem(ItemId);

                return Json(strSQL, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong", JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}