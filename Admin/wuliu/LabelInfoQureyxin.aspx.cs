using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_wuliu_LabelInfoQureyxin : AuthorPage
{
    SqlConnection sqlcon = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());
    public CommonFunWL com = new CommonFunWL();
    public commwl comm = new commwl();
    string Tname = "";
    BTB_Products_Infor bproduct = new BTB_Products_Infor();
    BTB_StoreHouse bstorehouse = new BTB_StoreHouse();
    BTJ_RegisterCompanys bagent = new BTJ_RegisterCompanys();
    BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();
    BTJ_BaseClass bbase = new BTJ_BaseClass();
    DBClass db = new DBClass();
    public BTJ_User buser = new BTJ_User();
    public string qdcompid = string.Empty;
    string lbcode = string.Empty;
    string tempcode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
         
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            qdcompid = GetCookieCompID();
            GridView_QueryResult.DataSource = null;
            GridView_QueryResult.DataBind();
            GridView1.DataSource = null;
            GridView1.DataBind();
            lbcode = TextBox_Label.Value.Trim();
            if (lbcode.Substring(0, 4) == "5022")
            {
                tempcode = returnstringAlabel(lbcode);
                if (!string.IsNullOrEmpty(tempcode))
                {
                    lbcode = tempcode;
                }
            }
            if (lbcode.Equals("") || !lbcode.Length.Equals(12))
            {
                MessageBox.Show(Page, "该号码格式不正确,请重新输入!");
                TextBox_Label.Value = "";
            }
            else
            {
                Tname = "";
                string returnalertstring = "";
                //DataTable dt = com.QueryLabelInfoByLabelCode(TextBox_Label.Text.Trim(), ref Tname, ref returnalertstring);
                //if (dt.Rows.Count>0)
                //{
                //    GridView_QueryResult.DataSource = dt;
                //GridView_QueryResult.DataBind();
                DataTable dt = new DataTable();
                DataTable dbq = db.GetBQLabelTH("LabelCodeNew='" + lbcode + "'");
                if (dbq.Rows.Count > 0)
                {
                    dt = com.QueryLabelInfoByLabelCode(dbq.Rows[0]["LabelCodeOld"].ToString(), ref Tname, ref returnalertstring,GetCookieCompID());
                }
                else
                {
                    dt = com.QueryLabelInfoByLabelCode(lbcode, ref Tname, ref returnalertstring,GetCookieCompID());
                }


                DataTable dth = db.GetBQLabelTH("LabelCodeNew='" + lbcode + "'or LabelCodeOld='" + lbcode + "'");

                GridView2.DataSource = dth;
                GridView2.DataBind();

                if (dt.Rows.Count > 0)
                {
                    GridView_QueryResult.DataSource = dt;
                    GridView_QueryResult.DataBind();


                    if (Tname.Length > 3)
                    {
                        GridView1.DataSource = getBox(dt.Rows[0]["箱标1"].ToString(), Tname);
                        GridView1.DataBind();
                        Label_fahuoInfo.Visible = true;
                        GridView_FaHuoXinXi.Visible = true;
                        string mm = dt.Rows[0]["箱标1"].ToString();
                        //GridView_FaHuoXinXi.DataSource = com.getFaHuoInfo(dt.Rows[0]["箱标1"].ToString(), Tname);
                        GridView_FaHuoXinXi.DataSource = com.getFaHuoInfoSumCompanyOnly(dt.Rows[0]["箱标1"].ToString(), GetCookieCompID());
                        // GridView_FaHuoXinXi.DataSource = com.getFaHuoInfo(dt.Rows[0]["箱标1"].ToString());
                        GridView_FaHuoXinXi.DataBind();
                    }
                }
                else if (returnalertstring.Length > 0)
                {
                    MessageBox.Show(Page, returnalertstring);
                }
            }
        }
        catch  
        {
            MessageBox.Show(Page, "请联系管理员进行查询！");
        }
    }

    MTJ_RegisterCompanys mcompany = new MTJ_RegisterCompanys();
    public string ReturnAgentNameAndCity(string AgentID)
    {
        if (AgentID.Length > 0 && AgentID != "0")
        {
            mcompany = bcompany.GetList(int.Parse(AgentID));
            return "【" + bbase.GetList(mcompany.CTID).CName + "】" + mcompany.CompName;
        }
        else
        {
            return "";
        }
    }

    public string returnstringAlabel(string labelcode)
    {
        string str = "select YanZhengMa  from [TJMarketingSystemYin].[dbo].[TB_BaseLabelInfo] where labelcode='" + labelcode + "'";
        DataTable dt2 = new DataTable();
        string code = string.Empty;
        SqlDataAdapter sda = new SqlDataAdapter(str, sqlcon);
        DataSet ds = new DataSet();
        sda.Fill(ds, "Tname");
        if (ds.Tables["Tname"].Rows.Count > 0)
        {
            code = ds.Tables["Tname"].Rows[0]["YanZhengMa"].ToString();
        }
        return code;

    }

    private DataTable getBox(string strbox, string TableName)
    {
        //string str = "select BottleLabel as 瓶标,BoxLabel01 as 箱标 from " + Tname + " where (BottleLabel='" + strbox + "' or BoxLabel01='" + strbox + "')";
        string str = "select  a.BottleLabel as 瓶标,a.BoxLabel01 as 箱标1,b.BoxLabel as 箱标2 from " + TableName + "BT as a left join  " + TableName + "BX as b  on  (a.BoxLabel01=b.BoxLabel or a.BoxLabel01=b.BoxLabel01) where (a.BottleLabel='" + strbox + "' or a.BoxLabel01='" + strbox + "')";
        DataTable dt2 = new DataTable();
        dt2 = null;
        SqlDataAdapter sda = new SqlDataAdapter(str, sqlcon);
        DataSet ds = new DataSet();
        sda.Fill(ds, "Tname");
        if (ds.Tables["Tname"].Rows.Count > 0)
        {
            dt2 = ds.Tables["Tname"];
        }
        return dt2;
    }

    private string GetReturnSqlStringForBox(string tablename, string codevalue)
    {
        string returnstring = "select '' as 瓶标,BoxLabel01 as 箱标1,PID as 产品 from " + tablename + " where BoxLabel01='" + codevalue + "'";
        return returnstring;
    }
    private string GetReturnSqlStringForReplaceBox(string tablename, string codevalue)
    {
        string BoxLabel01 = com.ReturnFiledValue(tablename + "_BX", "BoxLabel", codevalue, "BoxLabel01");
        if (BoxLabel01.Trim().Equals(""))
        {
            return "";
        }
        else
        {
            string returnstring = "select '' as 瓶标,BoxLabel01 as 箱标1,PID as 产品 from " + tablename + " where BoxLabel01='" + BoxLabel01 + "'";
            return returnstring;
        }
    }
    private string GetReturnSqlStringForBottle(string tablename, string codevalue)
    {
        string returnstring = "select p.BottleLabel as 瓶标,x.BoxLabel01 as 箱标1,PID as 产品 from " + tablename + " as x," + tablename + "_BT as p where p.BottleLabel ='" + codevalue + "' and x.BoxLabel01=p.BoxLabel01";

        return returnstring;
    }
    protected void GridView_QueryResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }

    public string ReturnProductNameByID(string PrductID)
    {
        if (PrductID.Trim() == "")
        {
            return "";
        }
        else
        {

            return bproduct.GetList(Convert.ToInt32(PrductID)).Products_Name;
        }
    }
    public string ReturnStoreHouseNameByID(string StoreHouseID)
    {
        return bstorehouse.GetList(Convert.ToInt32(StoreHouseID)).StoreHouseName;
    }
}