using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using mvcglobal.Models;

namespace mvcglobal.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        clsServiceHandler objServiceHandler = new clsServiceHandler();


        #region Create User
        public ActionResult Index()
        {
            
            LoadUserData();
            LoadUserGroupComboData();
            LoadDesignationComboData();
            LoadBranchComboData();
          
            return View();
        }

        public ActionResult LoadDesignationComboData()
        {
        //    ModelUser ObjDropDown = new ModelUser();
            try
            {
                DataSet ds = new DataSet();
                String strSql = "SELECT DESIGNATION_ID,DESIGNATION_NAME FROM TABLE_DESIGNATION";
                ds = objServiceHandler.ExecuteQuery(strSql);
                var model = new List<ModelUser>();
                List<SelectListItem> li = new List<SelectListItem>();
                    foreach (DataRow prow in ds.Tables["Table1"].Rows)
                    {
                    li.Add(new SelectListItem {
                        Value = prow["DESIGNATION_ID"].ToString(),
                        Text = prow["DESIGNATION_NAME"].ToString()});
                    }
                ViewBag.DesignationList = li;
                return View();
            }
            catch
            {
                return View("Something went wrong !");
            }

        }
        public ActionResult LoadBranchComboData()
        {
            try
            {
                DataSet ds = new DataSet();
                String strSql = "SELECT BRANCH_ID,BRANCH_TITLE FROM TABLE_BRANCH_INFO";
                ds = objServiceHandler.ExecuteQuery(strSql);
                var model = new List<ModelUser>();
                List<SelectListItem> li = new List<SelectListItem>();
                foreach (DataRow prow in ds.Tables["Table1"].Rows)
                {
                    li.Add(new SelectListItem
                    {
                        Value = prow["BRANCH_ID"].ToString(),
                        Text = prow["BRANCH_TITLE"].ToString()
                    });
                }
                ViewBag.BranchList = li;
                return View();
            }
            catch
            {
                return View("Something went wrong !");
            }



        }
        public ActionResult LoadUserGroupComboData()
        {
            ModelUser ObjGroupData = new ModelUser();
            try
            {
                DataSet ds = new DataSet();
                String strSql = "SELECT USER_GROUP_ID,USER_GROUP_NAME FROM TABLE_USER_GROUP WHERE USER_GROUP_CODE='U'";

                ds = objServiceHandler.ExecuteQuery(strSql);
                var model = new List<ModelUser>();
                {
                    foreach (DataRow prow in ds.Tables["Table1"].Rows)
                    {
                        var DropDownList = new ModelUser();
                        DropDownList.GroupId = prow["USER_GROUP_ID"].ToString();
                        DropDownList.GroupName = prow["USER_GROUP_NAME"].ToString();
                        model.Add(DropDownList);
                    }

                    ObjGroupData.GroupInfo = model;
                }
                return View(ObjGroupData);
            }
            catch
            {
                return View("Something went wrong !");
            }

        }

        public ActionResult LoadUserData()
        {
            try
            {
                DataSet ds = new DataSet();
                String strSql = "SELECT TSU.USER_ID,TSU.USER_FULL_NAME,TSU.USER_LOGIN_NAME,TSU.CREATION_BY,TSU.USER_DESIGNATION_ID,TD.DESIGNATION_NAME,TSU.BRANCH_ID,TBI.BRANCH_TITLE,TSU.USER_GROUP_ID,TUG.USER_GROUP_NAME FROM TABLE_SYS_USER TSU,TABLE_USER_GROUP TUG,TABLE_BRANCH_INFO TBI,TABLE_DESIGNATION TD WHERE TSU.BRANCH_ID=TBI.BRANCH_ID AND TSU.USER_DESIGNATION_ID=TD.DESIGNATION_ID AND TSU.USER_GROUP_ID=TUG.USER_GROUP_ID";

                ds = objServiceHandler.ExecuteQuery(strSql);
                var model = new List<ModelUser>();
                {
                    foreach (DataRow prow in ds.Tables["Table1"].Rows)
                    {
                        var UserList = new ModelUser();
                        UserList.UserId = prow["USER_ID"].ToString();
                        UserList.UserFullName = prow["USER_FULL_NAME"].ToString();
                        UserList.UserLoginName = prow["USER_LOGIN_NAME"].ToString();
                        UserList.CreationBy = prow["CREATION_BY"].ToString();
                        UserList.DesignationId = prow["USER_DESIGNATION_ID"].ToString();
                        UserList.DesignationName = prow["DESIGNATION_NAME"].ToString();
                        UserList.BranchId = prow["BRANCH_ID"].ToString();
                        UserList.BranchTitle = prow["BRANCH_TITLE"].ToString();
                        UserList.GroupId = prow["USER_GROUP_ID"].ToString();
                        UserList.GroupName = prow["USER_GROUP_NAME"].ToString();

                        UserList.Phone = "";
                        UserList.EmailAddress = "";
                        UserList.Address = "";
                        model.Add(UserList);
                    }



                    ViewBag.UserInfo = model;
                }
                return View(ViewBag.UserInfo);
            }
            catch
            {
                return View("Somethin went wrong !");
            }
        }

        [HttpPost]
        public JsonResult ShowUserId(string strUserId)
        {
            try
            {
                string DataAddes = objServiceHandler.ReturnString("SELECT MAX(USER_ID)+1  FROM TABLE_SYS_USER");

                return Json(DataAddes, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong !");
            }
        }

        [HttpPost]
        public JsonResult AddUser(string UserId, string UserFullName, string UserLoginName,string Phone,string EmailAddress,string Address,string JoiningDate,string LoginPassword,string ConfirmPassword, string GroupId, string BranchId, string DesignationId)
        {
            try
            {
                string strCreationBy = Session["USER_LOGIN_NAME"].ToString();

                if (UserId.Equals(""))
                {
                    return Json("Please input User Id", JsonRequestBehavior.AllowGet);
                }

                if (UserFullName.Equals(""))
                {
                    return Json("Please input Full Name", JsonRequestBehavior.AllowGet);
                }

                if (DesignationId.Equals(""))
                {
                    return Json("Please Select Designation", JsonRequestBehavior.AllowGet);
                }

                if (Phone.Equals(""))
                {
                    return Json("Please input Phone Number", JsonRequestBehavior.AllowGet);
                }

                if (GroupId.Equals(""))
                {
                    return Json("Please Select User Group", JsonRequestBehavior.AllowGet);
                }

                if (BranchId.Equals(""))
                {
                    return Json("Please Select Branch", JsonRequestBehavior.AllowGet);
                }

                if (UserLoginName.Equals(""))
                {
                    return Json("Please input Login Name", JsonRequestBehavior.AllowGet);
                }

                if (LoginPassword.Equals(""))
                {
                    return Json("Please input Login Password", JsonRequestBehavior.AllowGet);
                }

                if (ConfirmPassword.Equals(""))
                {
                    return Json("Please input Confirm Password", JsonRequestBehavior.AllowGet);
                }

                if (LoginPassword != ConfirmPassword)
                {
                    return Json("Please Confirm Password Not Match", JsonRequestBehavior.AllowGet);
                }

             

                if (!Phone.Equals(""))
                {
                    try
                    {
                        string strContactTypeId = "1";

                        string addContact = objServiceHandler.AddContactAddress(strContactTypeId, Phone, UserId, GroupId);
                        if (!addContact.Equals("Successful"))
                        {
                            return Json("Phone Number Added Failed", JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch
                    {
                        return Json("Somethin went wrong !", JsonRequestBehavior.AllowGet);
                    }
                }

                if (!EmailAddress.Equals(""))
                {
                    try
                    {
                        string strContactTypeId = "2";

                        string addContact = objServiceHandler.AddContactAddress(strContactTypeId, EmailAddress, UserId, GroupId);
                        if (!addContact.Equals("Successful"))
                        {
                            return Json("Supplier Email Added Failed", JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch
                    {
                        return Json("Somethin went wrong !", JsonRequestBehavior.AllowGet);
                    }
                }

                if (!Address.Equals(""))
                {
                    try
                    {
                        string strContactTypeId = "3";

                        string addContact = objServiceHandler.AddContactAddress(strContactTypeId, Address, UserId, GroupId);
                        if (!addContact.Equals("Successful"))
                        {
                            return Json("Supplier Address Added Failed", JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch
                    {
                        return Json("Somethin went wrong !", JsonRequestBehavior.AllowGet);
                    }
                }

                if (GroupId.ToString()==null)
                {
                    return Json("Please Select User Group", JsonRequestBehavior.AllowGet);
                }

                if (BranchId.ToString()==null)
                {
                    return Json("Please Select Branch", JsonRequestBehavior.AllowGet);
                }

                if (DesignationId.ToString()==null)
                {
                    return Json("Please Select Designation", JsonRequestBehavior.AllowGet);
                }

                if (LoginPassword.ToString() == null)
                {
                    return Json("Please input Login Password", JsonRequestBehavior.AllowGet);
                }

                if (ConfirmPassword.ToString() == null)
                {
                    return Json("Please input Confirm Password", JsonRequestBehavior.AllowGet);
                }

                if(LoginPassword!=ConfirmPassword)
                {
                    return Json("Please Confirm Password Not Match", JsonRequestBehavior.AllowGet);
                }

                string DataAddes = objServiceHandler.AddUser(UserId, UserFullName, UserLoginName, LoginPassword, strCreationBy, DesignationId, BranchId, GroupId);
                return Json(DataAddes, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json("Something went wrong !", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateUserData(string UserId,string UserFullName ,string UserLoginName ,string GroupId ,string BranchId ,string DesignationId)
        {
            String strSQL = "";
           string strUserId = Session["USER_LOGIN_NAME"].ToString();

            try
            {

                if (UserId.Equals(""))
                {
                    return Json("Please insert user Id", JsonRequestBehavior.AllowGet);
                }
                if (UserFullName.Equals(""))
                {
                    return Json("Please input Full Name ", JsonRequestBehavior.AllowGet);
                }

                if (UserLoginName.Equals(""))
                {
                    return Json("Please insert Login Id", JsonRequestBehavior.AllowGet);
                }
                if (BranchId.ToString()==null)
                {
                    return Json("Please Select Branch", JsonRequestBehavior.AllowGet);
                }

                if (GroupId.ToString() == null)
                {
                    return Json("Please Select Group ", JsonRequestBehavior.AllowGet);
                }
                if (DesignationId.ToString() == null)
                {
                    return Json("Please Select Designation", JsonRequestBehavior.AllowGet);
                }

                strSQL = objServiceHandler.UpdatUser(UserId, UserFullName, UserLoginName,GroupId,BranchId,DesignationId,strUserId);

                return Json(strSQL, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteUserData(string UserId)
        {
            String strSQL = "";
            try
            {

                if (UserId.Equals(""))
                {
                    return Json("Please Input User Id ", JsonRequestBehavior.AllowGet);
                }
                strSQL = objServiceHandler.DeleteUser(UserId);

                return Json(strSQL, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong", JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region User Group
        public ActionResult UserGroup()
        {
            LoadUserGroupData();
            return View();
        }
        public ActionResult LoadUserGroupData()
        {
            try
             {
                DataSet ds = new DataSet();
                String strSql = "SELECT USER_GROUP_ID,USER_GROUP_NAME,USER_GROUP_SHORT_NAME,USER_GROUP_CODE FROM TABLE_USER_GROUP WHERE USER_GROUP_CODE='U'";

                ds = objServiceHandler.ExecuteQuery(strSql);
                var model = new List<ModelUser>();
                {
                    foreach (DataRow prow in ds.Tables["Table1"].Rows)
                    {
                        var UserGroupList = new ModelUser();
                        UserGroupList.GroupId = prow["USER_GROUP_ID"].ToString();
                        UserGroupList.GroupName = prow["USER_GROUP_NAME"].ToString();
                        UserGroupList.GroupShortName = prow["USER_GROUP_SHORT_NAME"].ToString();
                       // UserGroupList.GroupCode = prow["USER_GROUP_CODE"].ToString();
                        model.Add(UserGroupList);
                    }



                    ViewBag.GroupInfo = model;
                }
                return View(ViewBag.GroupInfo);
            }
            catch
            {
                return View("Somethin went wrong !");
            }
        }
        [HttpPost]
        public JsonResult ShowGroupId(string strGroupId)
        {
            try
            {
                string DataAddes = objServiceHandler.ReturnString("SELECT MAX(USER_GROUP_ID)+1  FROM TABLE_USER_GROUP");

                return Json(DataAddes, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong !");
            }
        }
        [HttpPost]
        public JsonResult AddGroup(string strGroupId, string strGroupName, string strGroupShortName)
        {
            try
            {
                string strUserId = Session["USER_LOGIN_NAME"].ToString();
                string strGroupCode = "U";

                if (strGroupId.Equals(""))
                {
                    return Json("Please input Group Id", JsonRequestBehavior.AllowGet);
                }

                if (strGroupName.Equals(""))
                {
                    return Json("Please input Group Name", JsonRequestBehavior.AllowGet);
                }

                if (strGroupShortName.Equals(""))
                {
                    return Json("Please input Group Short Name", JsonRequestBehavior.AllowGet);
                }

                string DataAddes = objServiceHandler.AddGroup(strGroupId, strGroupName, strGroupShortName, strGroupCode, strUserId);
                return Json(DataAddes, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json("Something went wrong !", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult UpdateGroupData(string strGroupId, string strGroupName, string strGroupShortName)
        {
            string strUserId = Session["USER_LOGIN_NAME"].ToString();
            String strSQL = "";
            try
            {

                if (strGroupId.Equals(""))
                {
                    return Json("Please insert Group Id", JsonRequestBehavior.AllowGet);
                }
                if (strGroupName.ToString() == null)
                {
                    return Json("Please input Group Name ", JsonRequestBehavior.AllowGet);
                }


                strSQL = objServiceHandler.UpdatGroup(strGroupId, strGroupName, strGroupShortName, strUserId);

                return Json(strSQL, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult DeleteGroupData(string GroupId)
        {
            String strSQL = "";
            try
            {

                if (GroupId.Equals(""))
                {
                    return Json("Please Input Group Id ", JsonRequestBehavior.AllowGet);
                }
                strSQL = objServiceHandler.DeleteGroup(GroupId);

                return Json(strSQL, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong", JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Supplier
        public ActionResult Supplier()
        {
            LoadSupplierData();
            return View();
        }

        public ActionResult LoadSupplierData()
        {
            try
            {
                DataSet ds = new DataSet();
                String strSql = "SELECT SUPPLIER_ID,SUPPLIER_NAME,SUPPLIER_SHORT_NAME,USER_ID FROM TABLE_SUPPLIER";

                ds = objServiceHandler.ExecuteQuery(strSql);
                var model = new List<ModelSupplier>();
                {
                    foreach (DataRow prow in ds.Tables["Table1"].Rows)
                    {
                        var SupplierList = new ModelSupplier();
                        SupplierList.SupplierId = prow["SUPPLIER_ID"].ToString();
                        SupplierList.SupplierName = prow["SUPPLIER_NAME"].ToString();
                        SupplierList.SupplierShortName = prow["SUPPLIER_SHORT_NAME"].ToString();
                        //SupplierList.SupplierEmail = prow["CONTACT_NUMBER"].ToString();
                        //SupplierList.SupplierPhone = prow["CONTACT_NUMBER"].ToString();
                        //SupplierList.SupplierAddress = prow["CONTACT_NUMBER"].ToString();
                        SupplierList.SupplierEmail = "";
                        SupplierList.SupplierPhone = "";
                        SupplierList.SupplierAddress = "";
                        SupplierList.UserId = prow["USER_ID"].ToString();
                        model.Add(SupplierList);
                    }



                    ViewBag.SupplierInfo = model;
                }
                return View(ViewBag.SupplierInfo);
            }
            catch
            {
                return View("Somethin went wrong !");
            }
        }


        [HttpPost]
        public JsonResult ShowSupplierId(string strSupplierId)
        {
            try
            {
                string DataAddes = objServiceHandler.ReturnString("SELECT MAX(SUPPLIER_ID)+1  FROM TABLE_SUPPLIER");

                return Json(DataAddes, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong !");
            }
        }

        [HttpPost]
        public JsonResult AddSupplier(string strSupplierId, string strSupplierName, string strSupplierShortName,string strPhone, string strEmailAddress, string strAddress)
        {
            try
            {
                string strUserGroupId = "2";
                string strUserId = Session["USER_LOGIN_NAME"].ToString();

                if (strSupplierId.Equals(""))
                {
                    return Json("Please input Supplier Id", JsonRequestBehavior.AllowGet);
                }

                if (strSupplierName.Equals(""))
                {
                    return Json("Please input Supplier Name", JsonRequestBehavior.AllowGet);
                }

                if (strPhone.Equals(""))
                {
                    return Json("Please input Phone Number", JsonRequestBehavior.AllowGet);
                }

                if (!strPhone.Equals(""))
                {
                    try { 
                    string strContactTypeId = "1";

                    string addContact = objServiceHandler.AddContactAddress(strContactTypeId, strPhone, strSupplierId, strUserGroupId);
                    if (!addContact.Equals("Successful"))
                    {
                        return Json("Supplier Phone Number Added Failed", JsonRequestBehavior.AllowGet);
                    }
                    }
                     catch
                    {
                        return Json("Somethin went wrong !", JsonRequestBehavior.AllowGet);
                    } 
                }

                if (!strEmailAddress.Equals(""))
                {
                    try { 
                    string strContactTypeId = "2";

                    string addContact = objServiceHandler.AddContactAddress(strContactTypeId, strEmailAddress, strSupplierId, strUserGroupId);
                    if (!addContact.Equals("Successful"))
                    {
                        return Json("Supplier Email Added Failed", JsonRequestBehavior.AllowGet);
                    }
                    }
                    catch
                    {
                        return Json("Somethin went wrong !", JsonRequestBehavior.AllowGet);
                    }
                }

                if (!strAddress.Equals(""))
                {
                    try {
                    string strContactTypeId = "3";

                    string addContact = objServiceHandler.AddContactAddress(strContactTypeId, strAddress, strSupplierId, strUserGroupId);
                    if (!addContact.Equals("Successful"))
                    {
                        return Json("Supplier Address Added Failed", JsonRequestBehavior.AllowGet);
                    }
                    }
                    catch
                    {
                        return Json("Somethin went wrong !", JsonRequestBehavior.AllowGet);
                    }
                }

                string DataAddes = objServiceHandler.AddSupplier(strSupplierId, strSupplierName, strSupplierShortName,strUserId);
                return Json(DataAddes, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json("Something went wrong !", JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult UpdateSupplierData(string strSupplierId, string strSupplierName, string strSupplierShortName)
        {
            String strSQL = "";
            string strUserId = Session["USER_LOGIN_NAME"].ToString();
            try
            {

                if (strSupplierId.Equals(""))
                {
                    return Json("Please insert Supplier Id", JsonRequestBehavior.AllowGet);
                }
                if (strSupplierName.ToString() == null)
                {
                    return Json("Please input Supplier Name ", JsonRequestBehavior.AllowGet);
                }
                if (strSupplierShortName.ToString() == null)
                {
                    return Json("Please input Supplier Short Name ", JsonRequestBehavior.AllowGet);
                }


                strSQL = objServiceHandler.UpdatSupplier(strSupplierId, strSupplierName, strSupplierShortName, strUserId);

                return Json(strSQL, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Something went wrong", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteSupplierData(string SupplierId)
        {
            String strSQL = "";
            try
            {

                if (SupplierId.Equals(""))
                {
                    return Json("Please Input SubCategory Id ", JsonRequestBehavior.AllowGet);
                }
                strSQL = objServiceHandler.DeleteSupplier(SupplierId);

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