using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace mvcglobal.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        clsServiceHandler objServiceHandler = new clsServiceHandler();
        public ActionResult Login()
        {
            return View();
        }
        
     [HttpPost]
     public JsonResult LoginUser (string username,string password)
        {
       try {

            DataSet ds = new DataSet();

            if (username.Equals(""))
            {
                return Json("Please Input User Name", JsonRequestBehavior.AllowGet);
            }
            if (password.Equals(""))
            {
                return Json("Please Input Password", JsonRequestBehavior.AllowGet);
            }

          ds = objServiceHandler.LoginWithUserName(username, password);
          if(ds.Tables["TABLEUSER"].Rows.Count.Equals(0))
            {
                return Json("Please insert valid user name and password", JsonRequestBehavior.AllowGet);
            }
            else
            {
                foreach (DataRow prow in ds.Tables["TABLEUSER"].Rows)
                {
                    Session["USER_ID"] = prow["USER_ID"].ToString();
                    Session["USER_FULL_NAME"] = prow["USER_FULL_NAME"].ToString();
                    Session["USER_LOGIN_NAME"] = prow["USER_LOGIN_NAME"].ToString();
                    Session["USER_LOGIN_PASSWORD"] = prow["USER_LOGIN_PASSWORD"].ToString();
                    Session["CREATION_ID"] = prow["CREATION_BY"].ToString();
                    Session["USER_DESIGNATION_ID"] = prow["USER_DESIGNATION_ID"].ToString();
                    Session["BRANCH_ID"] = prow["BRANCH_ID"].ToString();
                    Session["USER_GROUP_ID"] = prow["USER_GROUP_ID"].ToString();

                }

                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            }
            catch
            {
                return Json("Something went wrong !", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Welcome()
        {
            return View();
        }
    }
}