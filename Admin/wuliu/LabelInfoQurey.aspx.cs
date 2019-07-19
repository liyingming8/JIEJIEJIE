using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.DBUtility;
using TJ.Model;
using commonlib;

public partial class Admin_wuliu_LabelInfoQurey : AuthorPage
{
    SqlConnection sqlcon = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());
    public CommonFunWL com = new CommonFunWL();
    public commwl comm = new commwl();
    CommonFun commmarket = new CommonFun();
    string Tname = "";
    BTB_Products_Infor bproduct = new BTB_Products_Infor();
    BTB_StoreHouse bstorehouse = new BTB_StoreHouse();
    BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();
    DBClass db = new DBClass();
    public BTJ_User buser = new BTJ_User();
    public string qdcompid = string.Empty;
    string lbcode = string.Empty;
    string tempcode = string.Empty;
    public string strAgentID = string.Empty;
    readonly TabExecutewuliu _tab = new TabExecutewuliu();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //try
        //{
        qdcompid = GetCookieCompID();
        lbcode = TextBox_Label.Value.Trim();
        if (lbcode.Equals("") || !lbcode.Length.Equals(12))
        {
            MessageBox.Show(Page, "该号码格式不正确,请重新输入!");
            TextBox_Label.Value = "";
        }
        else
        {
            if (lbcode.Substring(0, 3) == "311")
            {
                tempcode = returnstringABlabel(lbcode);
                if (!string.IsNullOrEmpty(tempcode))
                {
                    lbcode = tempcode;
                }
            }
            if (lbcode.Substring(0, 4) == "5022")
            {
                tempcode = returnstringAlabel(lbcode);
                if (!string.IsNullOrEmpty(tempcode))
                {
                    lbcode = tempcode;
                }
            }

            Tname = "";
            string returnalertstring = "";

            //2019-5-8南福酒业AB对应
            string sqlNanFu = "SELECT ACode FROM [TianJianWuLiuWebnew].[dbo].[tj_BA] where BCodeMM=" + lbcode;
            SqlCommand result = new SqlCommand(sqlNanFu, sqlcon);
            sqlcon.Open();
            SqlDataReader dr;
            dr = result.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                lbcode = dr.GetString(0);
            }
            dr.Close();
            sqlcon.Close();

            DataTable dt = new DataTable();
            DataTable dbq = db.GetBQLabelTH("LabelCodeNew='" + lbcode + "'");
            if (dbq.Rows.Count > 0)
            {

                dt = com.QueryLabelInfoByLabelCode(dbq.Rows[0]["LabelCodeOld"].ToString(), ref Tname, ref returnalertstring, GetCookieCompID().Equals("3830") ? "2" : GetCookieCompID());
                DataTable dth = db.GetBQLabelTH("LabelCodeNew='" + lbcode + "'or LabelCodeOld='" + lbcode + "'");
                if (dth.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<strong>替换关系</strong><br>");
                    foreach (DataRow row in dth.Rows)
                    {
                        sb.Append(", " + row["LabelCodeOld"] + "><span style=\"color:#00FF00\">" + row["LabelCodeNew"] + "<span>");
                    }
                    literaltihuan.Text = sb.ToString().Replace("<br>,", "<br>");
                }
                dth.Dispose();
            }
            else
            {
                dt = com.QueryLabelInfoByLabelCode(lbcode, ref Tname, ref returnalertstring, GetCookieCompID());
            }
            dbq.Dispose();
            if (dt.Rows.Count > 0)
            {
                if (Tname.Length > 3)
                {
                    DataTable dtc = new DataTable();
                    dtc = getBox(dt.Rows[0]["箱标1"].ToString(), Tname);
                    var sb = new StringBuilder();
                    if (dtc != null)
                    {
                        sb.Append("<strong>套标关系</strong><br>" + (dt.Rows[0]["箱标1"].Equals(lbcode) ? "<span style=\"color:#ff0000\">[" + dt.Rows[0]["箱标1"] + "]</span>" : dt.Rows[0]["箱标1"]));
                        sb.Append("→");
                        foreach (DataRow row in dtc.Rows)
                        {
                            sb.Append(", " + (row["瓶标"].Equals(lbcode) ? "<span style=\"color:#ff0000\">[" + row["瓶标"] + "]</span>" : row["瓶标"]));
                        }
                    }
                    else
                    {
                        sb.Append("<strong>套标关系</strong><br>" + (dt.Rows[0]["箱标1"].Equals(lbcode) ? "<span style=\"color:#ff0000\">[" + dt.Rows[0]["箱标1"] + "]</span>" : dt.Rows[0]["箱标1"]));
                    }
                    hfbox.Value = dt.Rows[0]["箱标1"].ToString();
                    literalrelate.Text = sb.ToString().Replace("→,", "→");
                    GridView_FaHuoXinXi.Visible = true;
                    DataTable mDataTable = com.GetFaHuoInfoSumFrank(Tname + "FH", dt.Rows[0]["箱标1"].ToString());
                    //GridView_FaHuoXinXi.DataSource = com.GetFaHuoInfoSumFrank(Tname + "FH", dt.Rows[0]["箱标1"].ToString());

                    //下级不能查看上级发货信息
                    if (mDataTable.Rows.Count > 0)
                    {
                        string mParentID = tab.ExecuteQueryForSingleValue("select ParentID from TJ_RegisterCompanys where CompID=" + GetCookieCompID());
                        //ParentID是否为0、它本身或空
                        if (((!mParentID.ToString().Equals("0"))&&(!mParentID.ToString().Equals(GetCookieCompID())))||(string.IsNullOrEmpty(mParentID)))
                        {
                            for (int i=0;i< mDataTable.Rows.Count;i++)
                            {
                                if (!mDataTable.Rows[i]["CompID"].ToString().Equals(GetCookieCompID()))
                                {
                                    mDataTable.Rows[i].Delete();
                                }
                            }
                        }
                    }
                    /*
                    if (!GetCookieCompID().Equals("2"))
                    {
                        if (mDataTable.Rows.Count > 0 )
                        {
                            if ((mDataTable.Rows[0]["CompId"].ToString().Equals("3830"))||(mDataTable.Rows[0]["FromAgentID"].ToString().Equals("2"))) {
                                mDataTable.Rows[0].Delete();
                            }
                        }
                    }
                     */
                    GridView_FaHuoXinXi.DataSource = mDataTable;
                    GridView_FaHuoXinXi.DataBind();
                    mDataTable.Dispose();
                }
                dt.Dispose();
            }
            else if (returnalertstring.Length > 0)
            {
                MessageBox.Show(Page, returnalertstring);
            }

            DisplayData(TextBox_Label.Value);
        }
        //}
        //catch (Exception ex)
        //{
        //    //MessageBox.Show(Page, ex.Message);
        //    MessageBox.Show(Page, "请联系管理员进行查询！");
        //}
    }

    MTJ_RegisterCompanys mcompany = new MTJ_RegisterCompanys();
    public string ReturnAgentNameAndCity(string AgentID, string type)
    {
        if (AgentID.Length > 0 && AgentID != "0")
        {
            if (type == "2")
            {
                if (!string.IsNullOrEmpty(returnagnetcode(hfbox.Value)))
                {
                    AgentID = returnagnetcode(hfbox.Value);
                }
            }

            mcompany = bcompany.GetList(int.Parse(AgentID));
            return "【" + commmarket.ReturnBaseClassName(mcompany.CTID.ToString(), true, false) + "】" + mcompany.CompName;
        }
        else
        {
            return "";
        }
    }

    public string returnagnetcode(string labelcode)
    {
        string str = " SELECT  [batch_id] FROM[TianJianWuLiuWebnew].[dbo].[receipt_details] where label='" + labelcode + "'";
        string code = string.Empty;
        string codeagnet = string.Empty;
        string codetoagent = string.Empty;
        SqlDataAdapter sda = new SqlDataAdapter(str, sqlcon);
        DataSet ds = new DataSet();
        sda.Fill(ds, "Tname");
        if (ds.Tables["Tname"].Rows.Count > 0)
        {
            code = ds.Tables["Tname"].Rows[0]["batch_id"].ToString();
            string strbach = "SELECT  agentid,to_agentid,tm_handled FROM [TianJianWuLiuWebnew].[dbo].[receipt_abnormal] where tm_handled is not null and  receipt_batch_id='" + code + "'";
            SqlDataAdapter sdaagent = new SqlDataAdapter(strbach, sqlcon);
            DataSet dsagent = new DataSet();
            sdaagent.Fill(ds, "Tnameagnet");
            if (ds.Tables["Tnameagnet"].Rows.Count > 0)
            {
                codeagnet = ds.Tables["Tnameagnet"].Rows[0]["agentid"].ToString();


            }
        }
        return codeagnet;
    }


    public string returnstringAlabel(string labelcode)
    {
        string str = "select YanZhengMa  from [TJMarketingSystemYin].[dbo].[TB_BaseLabelInfo] where labelcode='" + labelcode + "'";
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
    /// <summary>
    /// 针对西凤新版的AB标签zyk2019.4.9
    /// </summary>
    /// <param name="labelcode"></param>
    /// <returns></returns>
    public string returnstringABlabel(string labelcode)
    {
        string str = "SELECT  [BCode] ,[ACode] ,[BCodeMM] FROM [TianJianWuLiuWebnew].[dbo].[tj_BA] where BCodeMM='" + labelcode + "'";
        string code = string.Empty;
        SqlDataAdapter sda = new SqlDataAdapter(str, sqlcon);
        DataSet ds = new DataSet();
        sda.Fill(ds, "Tname");
        if (ds.Tables["Tname"].Rows.Count > 0)
        {
            code = ds.Tables["Tname"].Rows[0]["ACode"].ToString();
        }
        return code;
    }
    private DataTable getBox(string strbox, string TableName)
    {
        //string str = "select BottleLabel as 瓶标,BoxLabel01 as 箱标 from " + Tname + " where (BottleLabel='" + strbox + "' or BoxLabel01='" + strbox + "')";
        string str = "select  a.BottleLabel as 瓶标,a.BoxLabel01 as 箱标1,b.BoxLabel as 箱标2 from " + TableName + "BT as a left join  " + TableName + "BX as b  on  (a.BoxLabel01=b.BoxLabel or a.BoxLabel01=b.BoxLabel01) where (a.BottleLabel='" + strbox + "' or a.BoxLabel01='" + strbox + "')";
        SqlDataAdapter sda = new SqlDataAdapter(str, sqlcon);
        DataSet ds = new DataSet();
        sda.Fill(ds, "Tname");
        if (ds.Tables["Tname"].Rows.Count > 0)
        {
            sda.Dispose();
            return ds.Tables["Tname"];
        }
        return null;
    }
    /// <summary>
    /// 判断箱标是否在已确认表中====WYZ===20180812
    /// </summary>
    /// <param name="strbox"></param>
    /// <param name="TableName"></param>
    /// <returns></returns>
    private DataTable ConfirmBox(string strsql, string TableName)
    {
        //string str = "select BottleLabel as 瓶标,BoxLabel01 as 箱标 from " + Tname + " where (BottleLabel='" + strbox + "' or BoxLabel01='" + strbox + "')";
        string str = "select  parentid,acceptagentid,boxlabel,proid  from " + TableName + " where " + strsql;
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

    protected void GridView_QueryResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }

    public string ReturnProductNameByID(string prductID)
    {
        if (!string.IsNullOrEmpty(prductID) && !prductID.Equals("0"))
        {
            return bproduct.GetList(Convert.ToInt32(prductID)).Products_Name;
        }
        else
        {
            return "";
        }
    }
    public string ReturnStoreHouseNameByID(string storeHouseID)
    {
        if (!string.IsNullOrEmpty(storeHouseID) && !storeHouseID.Equals("0"))
        {
            return "[" + bstorehouse.GetList(Convert.ToInt32(storeHouseID)).StoreHouseName + "]";
        }
        return "";
    }

    protected void GridView_QueryResult_RowDataBound1(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GridView_FaHuoXinXi_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((Label)e.Row.FindControl("LabelIndex")).Text = (e.Row.RowIndex + 1).ToString();
            string strxxx = ((HiddenField)e.Row.FindControl("hd_toagentid")).Value.Trim();
            if (strAgentID.Equals(""))
            {
            }
            else
            {
                if (strxxx.Equals(strAgentID))
                {
                    e.Row.Cells[6].ForeColor = Color.Green;
                }
                else
                {
                    e.Row.Cells[6].ForeColor = Color.Red;
                }
            }
        }
    }

    private string _filtertemp = "1=1";
    readonly TabExecute tab = new TabExecute();
    private void DisplayData(string labelcode)
    {
        _filtertemp = "CompID=" + GetCookieCompID() + " and LabelCode='" + labelcode + "'";
        GridViewSMInfo.DataSource = tab.ExecuteQuery("select * from  TJ_375SMinfo where " + _filtertemp + " order by ID", null);
        GridViewSMInfo.DataBind();
    }
    protected void GridViewSMInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            var dataKey = GridViewSMInfo.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
            {
                ((HyperLink)e.Row.FindControl("hyperlinkuser")).Attributes.Add("onclick", XiangXiLinkStringForUserInfo(dataKey["UserID"].ToString()));
            }
            ((Label)e.Row.FindControl("LabelIndex")).Text = (e.Row.RowIndex + 1).ToString();

        }
    }

    public string XiangXiLinkStringForUserInfo(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('../TJ_UserInfoForShow.aspx?ID={0}',500,450,'客户信息')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }
}