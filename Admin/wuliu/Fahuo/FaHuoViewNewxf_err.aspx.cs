using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.BLL;
using commonlib;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

public partial class Admin_FaHuoViewNewxf_err : AuthorPage
{
    public readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();

    SqlConnection sqlconcom = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["fhkey"]))
            {
                hf_acid.Value = Sc.DecryptQueryString(Request.QueryString["fhkey"].Trim());
                //hf_acname.Value = bll.GetList(int.Parse(hf_acid.Value)).CompName; 
                DisplayData(hf_acid.Value);
            }
            else
            {
                Response.End();
            }
        }
    }
    public string agenterrorname(string fhkey)
    {
        string sqlstring = "SELECT count(*) from receipt_normal where fhkey='" + fhkey + "'";

        var sda = new SqlDataAdapter(sqlstring, sqlconcom);
        var dttemp = new DataTable();
        sda.Fill(dttemp);
        dttemp.Dispose();
        sda.Dispose();
        return dttemp.Rows[0][0].ToString();

    }

    public string GetActivityName()
    {
        return hf_acname.Value;
    }
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_Activity_PrizesAddEdit.aspx?cmd=" + Sc.EncryptQueryString("edit") + "&ID={0}&acid={1}',600,420,'活动奖项')", Sc.EncryptQueryString(ID), Sc.EncryptQueryString(hf_acid.Value));
        }
        else
        {
            return "";
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        var dataKey = GridView1.DataKeys[e.RowIndex];
        if (dataKey != null)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd");
            string sqlstring = "  update [TianJianWuLiuWebnew].[dbo].[receipt_abnormal] set tm_handled='"+time+"'  where to_fhkey='" + dataKey["to_fhkey"].ToString() + "' ";

            SqlCommand com = new SqlCommand();
            com.Connection = sqlconcom;
            com.CommandText = sqlstring;
            sqlconcom.Open();
            com.ExecuteNonQuery();
            sqlconcom.Close();
            com.Dispose();
            DisplayData(dataKey["to_fhkey"].ToString());
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
 
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            Label labelfhkey = (Label)e.Row.FindControl("labelgegnxin");
            if (labelfhkey.Text== "未处理")
            {
                e.Row.Cells[5].ForeColor = Color.Red;
               // ((LinkButton)e.Row.Cells[0].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除当前记录吗?')");
            }
            else
            {
                e.Row.Cells[6].Enabled=false;
                e.Row.Cells[6].ForeColor =Color.Gray;
            }
        }

    }

    private string _tempvalue = string.Empty;
    readonly BTJ_AwardInfo _btjAwardInfo = new BTJ_AwardInfo();


    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        // DisplayData();
    }

 
    private void DisplayData(string fhkey)
    {
        if (fhkey.Length > 0)
        {

            string sqlstring = "   SELECT  receipt_batch_id, [agentid], sum([receipt_qty]) as 数量,[to_fhkey],[to_agentid],[dt],tm_handled FROM [TianJianWuLiuWebnew].[dbo].[receipt_abnormal] where to_fhkey='" + fhkey + "'   group by  receipt_batch_id, [agentid],[to_fhkey],[to_agentid],[dt],tm_handled";

            var sda = new SqlDataAdapter(sqlstring, sqlconcom);
            var dttemp = new DataTable();
            sda.Fill(dttemp);
            GridView1.DataSource = dttemp;
            GridView1.DataBind();
            dttemp.Dispose();
            sda.Dispose();
            //  AspNetPager1.RecordCount = int.Parse(dttemp.Rows.Count.ToString());
            // GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "[TB_FaHuoInfo_" + GetCookieCompID() + "] fh,TB_Products_Infor pr,TB_StoreHouse st", " fh.FHTypeID=3 and fh.ProID=pr.Infor_ID and fh.STID=st.STID" + " and fh.FHDate between '" + Convert.ToDateTime(TextBox_RukuDateBegin.Text).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(TextBox_RukuDateEnd.Text).AddDays(1).ToString("yyyy-MM-dd") + "'   ", "fhdate desc", "fhID", pageSize);
        }
    }
    public  string  agentaddress(string bachid)
    {
        string sqlstring = "SELECT   [location] FROM[TianJianWuLiuWebnew].[dbo].[receipt_batch] where id= '" + bachid + "'";

        var sda = new SqlDataAdapter(sqlstring, sqlconcom);
        var dttemp = new DataTable();
        sda.Fill(dttemp);
        dttemp.Dispose();
        sda.Dispose();
        if (dttemp.Rows.Count>0)
        {
            return dttemp.Rows[0][0].ToString();
        }
        else
        {
            return "";

           

        }
        
    }

}
