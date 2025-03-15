using mvcglobal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace mvcglobal
{

    public class clsPurchaseHandler
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringName"].ToString());
        private decimal GetId(string strSql)
        {
            SqlDataAdapter sda = new SqlDataAdapter(strSql, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            decimal id = 1;

            if (dt.Rows.Count > 0)
            {
                id = Convert.ToDecimal(dt.Rows[0][0]) + 1;
            }
            return id;
        }
        public string EntryPurchase(Purchase purchase, List<PurchaseDetail> purchaseDetail)
        {
            //DataRow oOrderRow;
            string strSql = "";
            SqlTransaction transaction;
            con.Open();
            strSql = "SELECT MAX(PURCHASE_ID) FROM TABLE_PURCHASE";
            decimal PURCHASE_ID = GetId(strSql);
            strSql = "SELECT MAX(PURCHASE_DETAILS_ID) FROM TABLE_PURCHASE_DETAILS";
            decimal PURCHASE_DETAIL_ID = GetId(strSql);

            SqlCommand command = con.CreateCommand();
            transaction = con.BeginTransaction("SampleTransaction");
            try
            {
                strSql = "INSERT INTO TABLE_PURCHASE(PURCHASE_ID,CHALLAN_NO, SUPPLIER_ID, USERID, CREATION_DATE, COMPUTER_NAME)" +
                           "VALUES(" + PURCHASE_ID + ",'" + purchase.ChalanNo + "'," + purchase.SupplierId + ", " + purchase.CreatedBy + ",'" 
                           + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + purchase.IPAddress + "') ";
                //cmd = new SqlCommand(strSql, con);
                // cmd.ExecuteNonQuery();
                command.Connection = con;
                command.Transaction = transaction;
                command.CommandText = strSql;
                command.ExecuteNonQuery();

                string purchaseDetailInsertStrig = "";
                int i = 0;
                foreach (PurchaseDetail p in purchaseDetail)
                {
                    i++;
                    purchaseDetailInsertStrig += "(" + PURCHASE_DETAIL_ID +  "," + PURCHASE_ID + "," + p.ItemId + "," + p.PurchasePrice + "," + p.SalesPrice + "," + p.Quantity + "," + p.Quantity + ")";
                    if (i < purchaseDetail.Count) purchaseDetailInsertStrig += ",";
                    PURCHASE_DETAIL_ID++;
                }

                strSql = "INSERT INTO TABLE_PURCHASE_DETAILS(PURCHASE_DETAILS_ID, PURCHASE_ID, ITEM_ID, PURCHASE_PRICE, SALES_PRICE, PURCHASE_QTY,REMAIN_QTY) " +
                            "VALUES" + purchaseDetailInsertStrig;
                //cmd = new SqlCommand(strSql, con);
                //cmd.ExecuteNonQuery();
                command.CommandText = strSql;
                command.ExecuteNonQuery();


                transaction.Commit();
                return "Successful";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
        }
    }
}