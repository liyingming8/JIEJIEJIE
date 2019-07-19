using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// DB375 的摘要说明
/// </summary>
public class DB375
{
    SqlConnection myConn;
    public SqlDataAdapter ada;
    
    public SqlCommand cmd;
    public DataSet Mydataset;
 
	public DB375()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public SqlConnection GetConnection
    {
        get
        {
            string str = ConfigurationManager.ConnectionStrings["SqlServerConnStringCYJY"].ToString();
            myConn = new SqlConnection(str);
            return myConn;

        }

       

    }
 

  

    public SqlConnection GetConnectionCYJY
    {
        get
        {
            string str = ConfigurationManager.ConnectionStrings["SqlServerConnStringCYJY"].ToString();
            var myConnCYJY = new SqlConnection(str);
            return myConnCYJY;
        }

    }

    /// <summary>
    /// 获取userid信息
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public DataTable GetUserInfoByUid(string uid)
    {
        using (var myConn = GetConnectionCYJY)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from CY_User where " + uid + "";

            SqlDataAdapter ada1 = new SqlDataAdapter(myCmd, myConn);
            DataSet ds = new DataSet();

            ada1.Fill(ds, "ob");
            myConn.Close();
            return ds.Tables["ob"];
        }

    }


    /// <summary>
    /// 插入招聘信息
    /// </summary>
    /// <param name="UserID"></param>
    /// <param name="ProID"></param>
    /// <param name="ProImg"></param>
    /// <param name="ProNum"></param>
    /// <param name="Proprice"></param>
    /// <param name="Remarkes"></param>
    /// <returns></returns>
    public int InZPinfo(string UserID, string ZPzw, string ZPjj, string GZdy, string GZdd, string ZPrs, DateTime JZTime, string RZyq, string GZzz, string Email, string Remarkes)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string sql = "insert into  CY_ZPinfo (UserID,ZPzw,ZPjj,GZdy,GZdd,ZPrs,JZTime,RZyq,GZzz,Email,Remarkes)  values('" + UserID + "','" + ZPzw + "','" + ZPjj + "','" + GZdy + "','" + GZdd + "','" + ZPrs + "','" + JZTime + "','" + RZyq + "','" + GZzz + "','" + Email + "','" + Remarkes + "')";
            SqlCommand cmd = new SqlCommand(sql, myConn);
            int c = cmd.ExecuteNonQuery();
            cmd.Dispose();
            myConn.Close();
            return c;
        }
    }

    public DataTable GetZPinfo(string str)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from CY_ZPinfo where " + str + "";

            SqlDataAdapter ada1 = new SqlDataAdapter(myCmd, myConn);
            DataSet ds = new DataSet();

            ada1.Fill(ds, "ob");
            myConn.Close();
            return ds.Tables["ob"];
        }

    }

    public int UpdteZPinfo(string str, string wstr)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string sql = "";

            sql = "update CY_ZPinfo  set " + str + " where " + wstr + "";
            SqlCommand cmd = new SqlCommand(sql, myConn);
            int c = cmd.ExecuteNonQuery();
            cmd.Dispose();
            myConn.Close();
            return c;

        }
    }

    public int DelZPinfo(string str)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            SqlCommand myCmd = new SqlCommand("Delete CY_ZPinfo where  " + str + "", myConn);

            int c = myCmd.ExecuteNonQuery();
            myCmd.Dispose();
            myConn.Close();
            return c;
        }

    }


    /// <summary>
    /// 插入产品图片
    /// </summary>
    /// <param name="UserID"></param>
    /// <param name="ProID"></param>
    /// <param name="ProImg"></param>
    /// <param name="ProNum"></param>
    /// <param name="Proprice"></param>
    /// <param name="Remarkes"></param>
    /// <returns></returns>
    public int InProImage(string UserID, string ProID, bool Show, string ImagePath, string SImagePath, string Type, string ShowOrder, string Remarkes)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string sql = "insert into  Pro_Image (UserID,ProID,Show,ImagePath,SImagePath,Type,ShowOrder,Remarkes)  values('" + UserID + "','" + ProID + "','" + Show + "','" + ImagePath + "','" + SImagePath + "','" + Type + "','" + ShowOrder + "','" + Remarkes + "')";
            SqlCommand cmd = new SqlCommand(sql, myConn);
            int c = cmd.ExecuteNonQuery();
            cmd.Dispose();
            myConn.Close();
            return c;
        }
    }

    public DataTable GetProImage(string str)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from Pro_Image where " + str + "";

            SqlDataAdapter ada1 = new SqlDataAdapter(myCmd, myConn);
            DataSet ds = new DataSet();

            ada1.Fill(ds, "ob");
            myConn.Close();
            return ds.Tables["ob"];
        }

    }

    public int UpdteProImage(string str, string wstr)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string sql = "";

            sql = "update Pro_Image  set " + str + " where " + wstr + "";
            SqlCommand cmd = new SqlCommand(sql, myConn);
            int c = cmd.ExecuteNonQuery();
            cmd.Dispose();
            myConn.Close();
            return c;

        }
    }

    public int DelProImage(string str)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            SqlCommand myCmd = new SqlCommand("Delete Pro_Image where " + str + "", myConn);

            int c = myCmd.ExecuteNonQuery();
            myCmd.Dispose();
            myConn.Close();
            return c;
        }

    }



    /// <summary>
    /// 插入购物车
    /// </summary>
    /// <param name="UserID"></param>
    /// <param name="ProID"></param>
    /// <param name="ProImg"></param>
    /// <param name="ProNum"></param>
    /// <param name="Proprice"></param>
    /// <param name="Remarkes"></param>
    /// <returns></returns>
    public int InOrderInfo(string UserID, string ProID, string ProImg, string ProNum, string Proprice, string Remarkes)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string sql = "insert into  OrderInfo_375  (UserID,ProID,ProImg,ProNum,Proprice,Remarkes)  values('" + UserID + "','" + ProID + "','" + ProImg + "','" + ProNum + "','" + Proprice + "','" + Remarkes + "')";
            SqlCommand cmd = new SqlCommand(sql, myConn);
            int c = cmd.ExecuteNonQuery();
            cmd.Dispose();
            myConn.Close();
            return c;
        }
    }

    public DataTable GetOrderInfo(string str)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from OrderInfo_375 where " + str + "";

            SqlDataAdapter ada1 = new SqlDataAdapter(myCmd, myConn);
            DataSet ds = new DataSet();

            ada1.Fill(ds, "ob");
            myConn.Close();
            return ds.Tables["ob"];
        }

    }

    public int UpdteOrderInfo(string str, string wstr)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string sql = "";

            sql = "update OrderInfo_375  set " + str + " where " + wstr + "";
            SqlCommand cmd = new SqlCommand(sql, myConn);
            int c = cmd.ExecuteNonQuery();
            cmd.Dispose();
            myConn.Close();
            return c;

        }
    }




    /// <summary>
    /// 插入地址
    /// </summary>
    /// <param name="UserID"></param>
    /// <param name="AD_pro"></param>
    /// <param name="AD_sj"></param>
    /// <param name="AD_xj"></param>
    /// <param name="AD_jd"></param>
    /// <param name="AD_all"></param>
    /// <param name="AD_bm"></param>
    /// <param name="Name"></param>
    /// <param name="Phone"></param>
    /// <param name="MrFlag"></param>
    /// <param name="Type"></param>
    /// <returns></returns>
    public int InAddressInfo(string UserID, string AD_pro, string AD_sj, string AD_xj, string AD_jd, string AD_all, string AD_bm, string Name, string Phone, string MrFlag, string Type)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string sql = "insert into  Address_375  (UserID,AD_pro,AD_sj,AD_xj,AD_jd,AD_all,AD_bm,Name,Phone,MrFlag,Type)  values('" + UserID + "','" + AD_pro + "','" + AD_sj + "','" + AD_xj + "','" + AD_jd + "','" + AD_all + "','" + AD_bm + "','" + Name + "','" + Phone + "','" + MrFlag + "','" + Type + "')";
            SqlCommand cmd = new SqlCommand(sql, myConn);
            int c = cmd.ExecuteNonQuery();
            cmd.Dispose();
            myConn.Close();
            return c;
        }
    }


    public DataTable GetAddInfo(string str)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from Address_375 where " + str + "";

            SqlDataAdapter ada1 = new SqlDataAdapter(myCmd, myConn);
            DataSet ds = new DataSet();

            ada1.Fill(ds, "ob");
            myConn.Close();
            return ds.Tables["ob"];
        }

    }

    public int UpdteAddInfo(string str, string wstr)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string sql = "";

            sql = "update Address_375  set " + str + " where " + wstr + "";
            SqlCommand cmd = new SqlCommand(sql, myConn);
            int c = cmd.ExecuteNonQuery();
            cmd.Dispose();
            myConn.Close();
            return c;

        }
    }



    /// <summary>
    /// 查询产品信息
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable GetProInfo(string str)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from Product_375 where " + str + "";

            SqlDataAdapter ada1 = new SqlDataAdapter(myCmd, myConn);
            DataSet ds = new DataSet();

            ada1.Fill(ds, "ob");
            myConn.Close();
            return ds.Tables["ob"];
        }

    }


    /// <summary>
    /// 插入产品
    /// </summary>
    /// <param name="ProName"></param>
    /// <param name="Price"></param>
    /// <param name="ProImg"></param>
    /// <param name="UseFlag"></param>
    /// <returns></returns>
    public int InProduct(string ProName, decimal Price, string ProImg, bool UseFlag)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string sql = "insert into  Product_375 (ProName,Price,ProImg,UseFlag)  values('" + ProName + "','" + Price + "','" + ProImg + "','" + UseFlag + "')";
            SqlCommand cmd = new SqlCommand(sql, myConn);
            int c = cmd.ExecuteNonQuery();
            cmd.Dispose();
            myConn.Close();
            return c;
        }
    }



    public int UpdteProduct(string str, string wstr)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string sql = "";

            sql = "update Product_375  set " + str + " where " + wstr + "";
            SqlCommand cmd = new SqlCommand(sql, myConn);
            int c = cmd.ExecuteNonQuery();
            cmd.Dispose();
            myConn.Close();
            return c;

        }
    }

    public int DelProduct(string str)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            SqlCommand myCmd = new SqlCommand("Delete Product_375 where " + str + "", myConn);

            int c = myCmd.ExecuteNonQuery();
            myCmd.Dispose();
            myConn.Close();
            return c;
        }

    }



    /// <summary>
    /// get payinfo
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public DataTable GetPayInfo(string str)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }

            string myCmd = "select * from PayInfo_WX  where " + str + "";
           
            SqlDataAdapter ada1 = new SqlDataAdapter(myCmd, myConn);
            DataSet ds = new DataSet();

            ada1.Fill(ds, "ob");
            myConn.Close();
            return ds.Tables["ob"];
        }

    }

    /// <summary>
    /// 插入WXpayinfo
    /// </summary>
    /// <param name="UserID"></param>
    /// <param name="PayID"></param>
    /// <param name="GoodsID"></param>
    /// <param name="GoodsNum"></param>
    /// <param name="GoodsPrice"></param>
    /// <param name="WXappid"></param>
    /// <param name="WXmchid"></param>
    /// <param name="PaySign"></param>
    /// <param name="PayBody"></param>
    /// <param name="PayFee"></param>
    /// <param name="WXorderID"></param>
    /// <param name="WXopenid"></param>
    /// <param name="PayTime"></param>
    /// <param name="PayOK"></param>
    /// <param name="Remarkes"></param>
    public int InPayinfoWX(string UserID, string AddID, string Address, string PayID, string GoodsID, string GoodsNum, string GoodsPrice,string AllNum, string WXappid, string WXmchid, string PaySign, string PayBody, string PayFee, string WXorderID, string WXopenid, DateTime PayTime, string PayOK, string Remarkes)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string sql = "insert into  PayInfo_WX (UserID,AddID,Address,PayID,GoodsID,GoodsNum,GoodsPrice,AllNum,WXappid,WXmchid,PaySign,PayBody,PayFee,WXorderID,WXopenid,PayTime,PayOK,Remarkes)  values('" + UserID + "','" + AddID + "','" + Address + "','" + PayID + "','" + GoodsID + "','" + GoodsNum + "','" + GoodsPrice + "','" + AllNum + "', '" + WXappid + "','" + WXmchid + "','" + PaySign + "','" + PayBody + "','" + PayFee + "','" + WXorderID + "','" + WXopenid + "','" + PayTime + "','" + PayOK + "','" + Remarkes + "')";
            SqlCommand cmd = new SqlCommand(sql, myConn);
            int c = cmd.ExecuteNonQuery();

            cmd.Dispose();
            myConn.Close();
            return c;
        }
    }

    /// <summary>
    /// 支付完成 更新
    /// </summary>
    /// <param name="str"></param>
    /// <param name="wstr"></param>
    /// <returns></returns>
    public int UpdtePayinfoWX(string str, string wstr)
    {
        using (var myConn = GetConnection)
        {
            if (myConn.State == ConnectionState.Closed)
            {
                myConn.Open();
            }
            string sql = "";

            sql = "update PayInfo_WX  set " + str + " where " + wstr + "";
            SqlCommand cmd = new SqlCommand(sql, myConn);
            int c = cmd.ExecuteNonQuery();
            cmd.Dispose();
            myConn.Close();
            return c;

        }
    }

}