using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using mvcglobal.Models;

namespace mvcglobal
{

    public class clsServiceHandler
    {
       // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringName"].ToString());


        public string ReturnString(string strSql)
        {
            string strReturn = "";
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(strSql, con);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        strReturn = dr[0].ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                strReturn = ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return strReturn;
        }

        public DataSet ExecuteQuery(string strSQL)
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(strSQL, con);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds, "Table1");
                return ds;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public DataSet LoginWithUserName(string strUserName, string strPassword)
        {
            string strSql = "";
            strSql = "SELECT* FROM TABLE_SYS_USER WHERE USER_LOGIN_NAME = '" + strUserName + "' AND USER_LOGIN_PASSWORD = '" + strPassword + "' ";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(strSql, con);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                DataSet oDs = new DataSet();
                oda.Fill(oDs, "TABLEUSER");
                return oDs;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        #region Unit
        public string AddUnit(string strUnitId, string strUnitName)
        {

            DataRow oOrderRow;
            string strSql = "";
            DataSet oDS = new DataSet();
            con.Open();
            try
            {
                strSql = "SELECT UNIT_ID, UNIT_NAME FROM TABLE_UNIT";
                SqlCommand cmd = new SqlCommand(strSql, con);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
                oda.FillSchema(oDS, SchemaType.Source);

                DataTable pTable = oDS.Tables["Table"];
                pTable.TableName = "TABLE_UNIT";
                oOrderRow = oDS.Tables["TABLE_UNIT"].NewRow();

                oOrderRow["UNIT_ID"] = strUnitId;
                oOrderRow["UNIT_NAME"] = strUnitName;
                oDS.Tables["TABLE_UNIT"].Rows.Add(oOrderRow);
                oda.Update(oDS, "TABLE_UNIT");
                return "Successful";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }

        }


        public string UpdatUnit(string strUnitId, string strUnitName)
        {
            SqlTransaction dbTransaction;
            con.Open();
            dbTransaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;//Active Connection
                cmd.Transaction = dbTransaction;

                cmd.CommandText = " UPDATE TABLE_UNIT SET UNIT_NAME = '" + strUnitName + "' WHERE UNIT_ID = '" + strUnitId + "'";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                dbTransaction.Commit();
                return "Successful";
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                ex.Message.ToString();
                return "Unsuccessful";
            }
            finally
            {
                con.Close();
                con = null;
            }
        }

        public string DeleteUnit(string strUnitId)
        {
            SqlTransaction dbTransaction;
            con.Open();
            dbTransaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;//Active Connection
                cmd.Transaction = dbTransaction;

                cmd.CommandText = " DELETE FROM TABLE_UNIT WHERE UNIT_ID = '" + strUnitId + "'";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                dbTransaction.Commit();
                return "Successful";
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                ex.Message.ToString();
                return "Unsuccessful";
            }
            finally
            {
                con.Close();
                con = null;
            }
        }

        #endregion

        #region Category

        public string AddCategory(string strCategoryId, string strCategoryName)
        {

            DataRow oOrderRow;
            string strSql = "";
            DataSet oDS = new DataSet();
            con.Open();
            try
            {
                strSql = "SELECT CATEGORY_ID, CATEGORY_NAME FROM TABLE_CATEGORY";
                SqlCommand cmd = new SqlCommand(strSql, con);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
                oda.FillSchema(oDS, SchemaType.Source);

                DataTable pTable = oDS.Tables["Table"];
                pTable.TableName = "TABLE_CATEGORY";
                oOrderRow = oDS.Tables["TABLE_CATEGORY"].NewRow();

                oOrderRow["CATEGORY_ID"] = strCategoryId;
                oOrderRow["CATEGORY_NAME"] = strCategoryName;
                oDS.Tables["TABLE_CATEGORY"].Rows.Add(oOrderRow);
                oda.Update(oDS, "TABLE_CATEGORY");
                return "Successful";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }

        }

        public string UpdatCategory(string strCategoryId, string strCategoryName)
        {
            SqlTransaction dbTransaction;
            con.Open();
            dbTransaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;//Active Connection
                cmd.Transaction = dbTransaction;

                cmd.CommandText = " UPDATE TABLE_CATEGORY SET CATEGORY_NAME = '" + strCategoryName + "' WHERE CATEGORY_ID = '" + strCategoryId + "'";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                dbTransaction.Commit();
                return "Successful";
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                ex.Message.ToString();
                return "Unsuccessful";
            }
            finally
            {
                con.Close();
                con = null;
            }
        }

      

        public string DeleteCategory(string strCategoryId)
        {
            SqlTransaction dbTransaction;
            con.Open();
            dbTransaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;//Active Connection
                cmd.Transaction = dbTransaction;

                cmd.CommandText = " DELETE FROM TABLE_CATEGORY WHERE CATEGORY_ID = '" + strCategoryId + "'";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                dbTransaction.Commit();
                return "Successful";
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                ex.Message.ToString();
                return "Unsuccessful";
            }
            finally
            {
                con.Close();
                con = null;
            }
        }

        #endregion

        #region SubCategory

        public string AddSubCategory(string strSubCategoryId, string strSubCategoryName,string strCategoryId)
        {

            DataRow oOrderRow;
            string strSql = "";
            DataSet oDS = new DataSet();
            con.Open();
            try
            {
                strSql = "SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME,CATEGORY_ID FROM TABLE_SUB_CATEGORY";
                SqlCommand cmd = new SqlCommand(strSql, con);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
                oda.FillSchema(oDS, SchemaType.Source);

                DataTable pTable = oDS.Tables["Table"];
                pTable.TableName = "TABLE_SUB_CATEGORY";
                oOrderRow = oDS.Tables["TABLE_SUB_CATEGORY"].NewRow();

                oOrderRow["SUB_CATEGORY_ID"] = strSubCategoryId;
                oOrderRow["SUB_CATEGORY_NAME"] = strSubCategoryName;
                oOrderRow["CATEGORY_ID"] = strCategoryId;
                oDS.Tables["TABLE_SUB_CATEGORY"].Rows.Add(oOrderRow);
                oda.Update(oDS, "TABLE_SUB_CATEGORY");
                return "Successful";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }

        }

       

        public string UpdatSubCategory(string strSubCategoryId, string strSubCategoryName,string CategoryId)
        {
            SqlTransaction dbTransaction;
            con.Open();
            dbTransaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;//Active Connection
                cmd.Transaction = dbTransaction;

                cmd.CommandText = " UPDATE TABLE_SUB_CATEGORY SET SUB_CATEGORY_NAME = '" + strSubCategoryName + "', CATEGORY_ID='"+ CategoryId +"' WHERE SUB_CATEGORY_ID = '" + strSubCategoryId + "'";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                dbTransaction.Commit();
                return "Successful";
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                ex.Message.ToString();
                return "Unsuccessful";
            }
            finally
            {
                con.Close();
                con = null;
            }
        }

        public string DeleteSubCategory(string strSubCategoryId)
        {
            SqlTransaction dbTransaction;
            con.Open();
            dbTransaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;//Active Connection
                cmd.Transaction = dbTransaction;

                cmd.CommandText = " DELETE FROM TABLE_SUB_CATEGORY WHERE SUB_CATEGORY_ID = '" + strSubCategoryId + "'";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                dbTransaction.Commit();
                return "Successful";
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                ex.Message.ToString();
                return "Unsuccessful";
            }
            finally
            {
                con.Close();
                con = null;
            }
        }

        
        #endregion

        #region Supplier

        public string AddContactAddress(string strContactTypeId, string strPhone, string strSupplierId, string strUserGroupId)
        {
            DataRow oOrderRow;
            string strSql = "";
            DataSet oDS = new DataSet();
            con.Open();
            try
            {
                strSql = "SELECT CONTACT_TYPE_ID,CONTACT_NUMBER,CONTACT_PERSON_ID,USER_GROUP_ID FROM TABLE_CONTACT";
                SqlCommand cmd = new SqlCommand(strSql, con);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
                oda.FillSchema(oDS, SchemaType.Source);

                DataTable pTable = oDS.Tables["Table"];
                pTable.TableName = "TABLE_CONTACT";
                oOrderRow = oDS.Tables["TABLE_CONTACT"].NewRow();

                oOrderRow["CONTACT_TYPE_ID"] = strContactTypeId;
                oOrderRow["CONTACT_NUMBER"] = strPhone;
                oOrderRow["CONTACT_PERSON_ID"] = strSupplierId;
                oOrderRow["USER_GROUP_ID"] = strUserGroupId;
                oDS.Tables["TABLE_CONTACT"].Rows.Add(oOrderRow);
                oda.Update(oDS, "TABLE_CONTACT");
                return "Successful";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
        }
        public string AddSupplier(string strSupplierId, string strSupplierName, string strSupplierShortName,string strUserId)
        {
            DataRow oOrderRow;
            string strSql = "";
            DataSet oDS = new DataSet();
            con.Open();
            try
            {
                strSql = "SELECT SUPPLIER_ID, SUPPLIER_NAME,SUPPLIER_SHORT_NAME,USER_ID FROM TABLE_SUPPLIER";
                SqlCommand cmd = new SqlCommand(strSql, con);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
                oda.FillSchema(oDS, SchemaType.Source);

                DataTable pTable = oDS.Tables["Table"];
                pTable.TableName = "TABLE_SUPPLIER";
                oOrderRow = oDS.Tables["TABLE_SUPPLIER"].NewRow();

                oOrderRow["SUPPLIER_ID"] = strSupplierId;
                oOrderRow["SUPPLIER_NAME"] = strSupplierName;
                oOrderRow["SUPPLIER_SHORT_NAME"] = strSupplierShortName;
                oOrderRow["USER_ID"] = strUserId;
                oDS.Tables["TABLE_SUPPLIER"].Rows.Add(oOrderRow);
                oda.Update(oDS, "TABLE_SUPPLIER");
                return "Successful";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
        }
        public string UpdatSupplier(string strSupplierId, string strSupplierName, string strSupplierShortName, string strUserId)
        {
            SqlTransaction dbTransaction;
            con.Open();
            dbTransaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;//Active Connection
                cmd.Transaction = dbTransaction;

                cmd.CommandText = " UPDATE TABLE_SUPPLIER SET SUPPLIER_NAME = '" + strSupplierName + "',SUPPLIER_SHORT_NAME='" + strSupplierShortName + "', USER_ID='" + strUserId + "' WHERE SUPPLIER_ID = '" + strSupplierId + "'";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                dbTransaction.Commit();
                return "Successful";
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                ex.Message.ToString();
                return "Unsuccessful";
            }
            finally
            {
                con.Close();
                con = null;
            }
        }

        public string DeleteSupplier(string supplierId)
        {
            SqlTransaction dbTransaction;
            con.Open();
            dbTransaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;//Active Connection
                cmd.Transaction = dbTransaction;

                cmd.CommandText = " DELETE FROM TABLE_SUPPLIER WHERE SUPPLIER_ID = '" + supplierId + "'";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                dbTransaction.Commit();
                return "Successful";
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                ex.Message.ToString();
                return "Unsuccessful";
            }
            finally
            {
                con.Close();
                con = null;
            }
        }
        #endregion

        #region Group

        public string AddGroup(string strGroupId, string strGroupName, string strGroupShortName, string strGroupCode, string strUserId)
        {
            DataRow oOrderRow;
            string strSql = "";
            DataSet oDS = new DataSet();
            con.Open();
            try
            {
                strSql = "SELECT USER_GROUP_ID, USER_GROUP_NAME,USER_GROUP_SHORT_NAME,USER_GROUP_CODE,CREATED_BY FROM TABLE_USER_GROUP";
                SqlCommand cmd = new SqlCommand(strSql, con);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
                oda.FillSchema(oDS, SchemaType.Source);

                DataTable pTable = oDS.Tables["Table"];
                pTable.TableName = "TABLE_USER_GROUP";
                oOrderRow = oDS.Tables["TABLE_USER_GROUP"].NewRow();

                oOrderRow["USER_GROUP_ID"] = strGroupId;
                oOrderRow["USER_GROUP_NAME"] = strGroupName;
                oOrderRow["USER_GROUP_SHORT_NAME"] = strGroupShortName;
                oOrderRow["USER_GROUP_CODE"] = strGroupCode;
                oOrderRow["CREATED_BY"] = strUserId;
                oDS.Tables["TABLE_USER_GROUP"].Rows.Add(oOrderRow);
                oda.Update(oDS, "TABLE_USER_GROUP");
                return "Successful";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }

        }

        public string UpdatGroup(string strGroupId, string strGroupName, string strGroupShortName, string strUserId)
        {
            SqlTransaction dbTransaction;
            con.Open();
            dbTransaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;//Active Connection
                cmd.Transaction = dbTransaction;

                cmd.CommandText = " UPDATE TABLE_USER_GROUP SET USER_GROUP_NAME = '" + strGroupName + "', USER_GROUP_SHORT_NAME='" + strGroupShortName + "',CREATED_BY='"+strUserId+ "' WHERE USER_GROUP_ID = '" + strGroupId + "'";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                dbTransaction.Commit();
                return "Successful";
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                ex.Message.ToString();
                return "Unsuccessful";
            }
            finally
            {
                con.Close();
                con = null;
            }
        }

        public string DeleteGroup(string strGroupId)
        {
            SqlTransaction dbTransaction;
            con.Open();
            dbTransaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;//Active Connection
                cmd.Transaction = dbTransaction;

                cmd.CommandText = " DELETE FROM TABLE_USER_GROUP WHERE USER_GROUP_ID = '" + strGroupId + "'";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                dbTransaction.Commit();
                return "Successful";
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                ex.Message.ToString();
                return "Unsuccessful";
            }
            finally
            {
                con.Close();
                con = null;
            }
        }

        #endregion

        #region User 

        public string AddUser(string userId, string userFullName, string userLoginName, string loginPassword, string strCreationBy, string designationId, string branchId, string groupId)
        {
            DataRow oOrderRow;
            string strSql = "";
            DataSet oDS = new DataSet();
            con.Open();
            try
            {
                strSql = "SELECT USER_ID, USER_FULL_NAME, USER_LOGIN_NAME, USER_LOGIN_PASSWORD, CREATION_BY, USER_DESIGNATION_ID, BRANCH_ID, USER_GROUP_ID FROM TABLE_SYS_USER";
                SqlCommand cmd = new SqlCommand(strSql, con);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
                oda.FillSchema(oDS, SchemaType.Source);

                DataTable pTable = oDS.Tables["Table"];
                pTable.TableName = "TABLE_SYS_USER";
                oOrderRow = oDS.Tables["TABLE_SYS_USER"].NewRow();

                oOrderRow["USER_ID"] = userId;
                oOrderRow["USER_FULL_NAME"] = userFullName;
                oOrderRow["USER_LOGIN_NAME"] = userLoginName;
                oOrderRow["USER_LOGIN_PASSWORD"] = loginPassword;
                oOrderRow["CREATION_BY"] = strCreationBy;
                oOrderRow["USER_DESIGNATION_ID"] = designationId;
                oOrderRow["BRANCH_ID"] = branchId;
                oOrderRow["USER_GROUP_ID"] = groupId;

                oDS.Tables["TABLE_SYS_USER"].Rows.Add(oOrderRow);
                oda.Update(oDS, "TABLE_SYS_USER");
                return "Successful";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
        }

        public  string UpdatUser(string userId, string userFullName, string userLoginName, string groupId, string branchId, string designationId, string strUserId)
        {
            SqlTransaction dbTransaction;
            con.Open();
            dbTransaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;//Active Connection
                cmd.Transaction = dbTransaction;

                cmd.CommandText = " UPDATE TABLE_SYS_USER SET USER_FULL_NAME = '" + userFullName + "',USER_LOGIN_NAME='"+userLoginName+"', CREATION_BY='"+strUserId+ "',USER_DESIGNATION_ID='" + designationId + "', USER_GROUP_ID='" + groupId + "', BRANCH_ID='" + branchId + "' WHERE USER_ID = '" + userId + "'";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                dbTransaction.Commit();
                return "Successful";
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                ex.Message.ToString();
                return "Unsuccessful";
            }
            finally
            {
                con.Close();
                con = null;
            }
        }
        public string DeleteUser(string userId)
        {
            SqlTransaction dbTransaction;
            con.Open();
            dbTransaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;//Active Connection
                cmd.Transaction = dbTransaction;

                cmd.CommandText = " DELETE FROM TABLE_SYS_USER WHERE USER_ID = '" + userId + "'";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                dbTransaction.Commit();
                return "Successful";
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                ex.Message.ToString();
                return "Unsuccessful";
            }
            finally
            {
                con.Close();
                con = null;
            }
        }

        #endregion

        #region Item

        public string AddItem(string itemId, string itemName, string itemSerial, string itemBrand, string unitId, string categoryId, string itemTypeId, string itemStatusId, string purchasePrice, string salesPrice,string strItemBalance, string strCreationBy)
        {
            DataRow oOrderRow;
            string strSql = "";
            DataSet oDS = new DataSet();
            con.Open();
            try
            {
                strSql = "SELECT ITEM_ID,ITEM_NAME,ITEM_BARCODE,ITEM_BRAND,UNIT_ID,CATEGORY_ID,ITEM_TYPE_ID,ITEM_STATUS_ID,ITEM_PURCHASE_PRICE,ITEM_SALES_PRICE,ITEM_BALANCE,CREATION_BY FROM TABLE_ITEM ";
                SqlCommand cmd = new SqlCommand(strSql, con);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
                oda.FillSchema(oDS, SchemaType.Source);

                DataTable pTable = oDS.Tables["Table"];
                pTable.TableName = "TABLE_ITEM";
                oOrderRow = oDS.Tables["TABLE_ITEM"].NewRow();

                oOrderRow["ITEM_ID"] = itemId;
                oOrderRow["ITEM_NAME"] = itemName;
                oOrderRow["ITEM_BARCODE"] = itemSerial;
                oOrderRow["ITEM_BRAND"] = itemBrand;
                oOrderRow["UNIT_ID"] = unitId;
                oOrderRow["CATEGORY_ID"] = categoryId;
                oOrderRow["ITEM_TYPE_ID"] = itemTypeId;
                oOrderRow["ITEM_STATUS_ID"] = itemStatusId;
                //oOrderRow["ITEM_PURCHASE_PRICE"] = purchasePrice;
                //oOrderRow["ITEM_SALES_PRICE"] = salesPrice;
                //oOrderRow["ITEM_BALANCE"] = strItemBalance;
                oOrderRow["CREATION_BY"] = strCreationBy;
                
                oDS.Tables["TABLE_ITEM"].Rows.Add(oOrderRow);
                oda.Update(oDS, "TABLE_ITEM");

                return "Successful";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
        }
        public string UpdatItem(string itemId, string itemName, string itemSerial, string itemBrand, string unitId, string categoryId, string itemTypeId, string itemStatusId, string purchasePrice, string salesPrice, string strCreationBy)
        {
            SqlTransaction dbTransaction;
            con.Open();
            dbTransaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;//Active Connection
                cmd.Transaction = dbTransaction;

                cmd.CommandText = " UPDATE TABLE_ITEM SET ITEM_NAME = '" + itemName + "',ITEM_BARCODE='" + itemSerial + "', ITEM_BRAND='" + itemBrand + "',UNIT_ID='" + unitId + "', CATEGORY_ID='" + categoryId + "', ITEM_TYPE_ID='" + itemTypeId + "',ITEM_STATUS_ID='"+ itemStatusId + "',ITEM_PURCHASE_PRICE='"+ purchasePrice + "',ITEM_SALES_PRICE='"+ salesPrice + "',CREATION_BY='"+ strCreationBy + "' WHERE ITEM_ID = '" + itemId + "'";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                dbTransaction.Commit();
                return "Successful";
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                ex.Message.ToString();
                return "Unsuccessful";
            }
            finally
            {
                con.Close();
                con = null;
            }
        }

        public string DeleteItem(string itemId)
        {
            SqlTransaction dbTransaction;
            con.Open();
            dbTransaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;//Active Connection
                cmd.Transaction = dbTransaction;

                cmd.CommandText = " DELETE FROM TABLE_ITEM WHERE ITEM_ID = '" + itemId + "'";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                dbTransaction.Commit();
                return "Successful";
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                ex.Message.ToString();
                return "Unsuccessful";
            }
            finally
            {
                con.Close();
                con = null;
            }
        }

        #endregion

        #region Item Image
        public string AddImage(int strItemId, string strImagePath)
        {

            DataRow oOrderRow;
            string strSql = "";
            DataSet oDS = new DataSet();
            con.Open();
            try
            {
                strSql = "SELECT ITEM_ID, IMAGE_PATH FROM TABLE_ITEM_IMAGE";
                SqlCommand cmd = new SqlCommand(strSql, con);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
                oda.FillSchema(oDS, SchemaType.Source);

                DataTable pTable = oDS.Tables["Table"];
                pTable.TableName = "TABLE_ITEM_IMAGE";
                oOrderRow = oDS.Tables["TABLE_ITEM_IMAGE"].NewRow();

                oOrderRow["ITEM_ID"] = strItemId;
                oOrderRow["IMAGE_PATH"] = strImagePath;
                oDS.Tables["TABLE_ITEM_IMAGE"].Rows.Add(oOrderRow);
                oda.Update(oDS, "TABLE_ITEM_IMAGE");
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }

        }
        #endregion


    }
}