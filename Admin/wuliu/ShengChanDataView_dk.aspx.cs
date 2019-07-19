using System;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using TJ.BLL;
using commonlib;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_wuliu_ShengChanDataView_dk : AuthorPage
{
    public BTJ_RegisterCompanys bagent = new BTJ_RegisterCompanys();
    private readonly BTB_Products_Infor bpro = new BTB_Products_Infor();
    private readonly BTB_StoreHouse bstorehouse = new BTB_StoreHouse();
    private BTB_ProductAuthorForAgent bproductforagent = new BTB_ProductAuthorForAgent();
    private BTJ_User buser = new BTJ_User();
    private readonly CommonFunWL comwl = new CommonFunWL();

    private readonly SqlConnection sqlconcom =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBox_RukuDateBegin.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            TextBox_RukuDateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
            FillDDL();
            string sqlstring =
                "SELECT fh.FHPiCi,fh.FHDate,fh.AgentID,fh.XiangNumber,pr.Products_Name,st.StoreHouseName,fh.FHUserID FROM [TB_FaHuoInfo_" +
                GetCookieCompID() +
                "] fh,TB_Products_Infor pr,TB_StoreHouse st where   fh.FHTypeID=1 and fh.ProID=pr.Infor_ID and fh.STID=st.STID";
            sqlstring += " and fh.FHDate between '" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + "' and '" +
                         TextBox_RukuDateEnd.Text + "' ";

            if (ComboBox_ProductInfo.SelectedValue != "0" && ComboBox_ProductInfo.SelectedValue != null)
            {
                sqlstring += " and fh.ProID=" + ComboBox_ProductInfo.SelectedValue;
            }
            if (ComboBox_StoreHouse.SelectedValue != "0" && ComboBox_StoreHouse.SelectedValue != null)
            {
                sqlstring += " and fh.STID=" + ComboBox_StoreHouse.SelectedValue;
            }
            SqlDataAdapter sda = new SqlDataAdapter(sqlstring, sqlconcom);
            DataTable dttemp = new DataTable();
            sda.Fill(dttemp);
            GridView_RukuInfo.DataSource = dttemp;
            GridView_RukuInfo.DataBind();
            FillDDL();
        }
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
        string sqlstring =
            "SELECT fh.FHPiCi,fh.FHDate,fh.AgentID,fh.XiangNumber,pr.Products_Name,st.StoreHouseName,fh.FHUserID FROM [TB_FaHuoInfo_" +
            GetCookieCompID() +
            "] fh,TB_Products_Infor pr,TB_StoreHouse st where   fh.FHTypeID=1 and fh.ProID=pr.Infor_ID and fh.STID=st.STID";
        sqlstring += " and fh.FHDate between '" + TextBox_RukuDateBegin.Text + "' and '" +
                     Convert.ToDateTime(TextBox_RukuDateEnd.Text).AddDays(1) + "'";
        //if (ComboBox_DaiLiShangID.SelectedValue != "0" && ComboBox_DaiLiShangID.SelectedValue != null)
        //{
        //    sqlstring += " and fh.AgentID=" + ComboBox_DaiLiShangID.SelectedValue;
        //}
        //if (ComboBox_ProInfo.SelectedValue != "0" && ComboBox_ProInfo.SelectedValue != null)
        //{
        //    sqlstring += " and fh.ProID=" + ComboBox_ProInfo.SelectedValue;
        //}
        if (ComboBox_StoreHouse.SelectedValue != "0" && ComboBox_StoreHouse.SelectedValue != null)
        {
            sqlstring += " and fh.STID=" + ComboBox_StoreHouse.SelectedValue;
        }
        SqlDataAdapter sda = new SqlDataAdapter(sqlstring, sqlconcom);
        DataTable dttemp = new DataTable();
        sda.Fill(dttemp);
        GridView_RukuInfo.DataSource = dttemp;
        GridView_RukuInfo.DataBind();
    }

    protected void GridView_RukuInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }

    private void FillDDL()
    {
        string tempagentidstring = comwl.GetAgentIDStringByCompID(GetCookieCompID());
        //if (tempagentidstring.Length > 0)
        //{
        //    ComboBox_DaiLiShangID.DataSource = bagent.GetListsByFilterString("CompID in (" + tempagentidstring + ")");
        //    ComboBox_DaiLiShangID.DataBind();
        //}
        ////ComboBox_DaiLiShangID.DataSource = bagent.GetListsByFilterString("ParentID="+GetCookieCompID()+" and CompTypeID="+DAConfig.CompTypeIDJingXiaoShang);
        //ComboBox_DaiLiShangID.DataBind();
        if (GetCookieCompTypeID() == DAConfig.CompTypeIDJingXiaoShang.ToString().Trim())
        {
            ComboBox_ProductInfo.DataSource = comwl.ReturnGetAgentAuthorProductInfo(GetCookieCompID());
        }
        else
        {
            ComboBox_ProductInfo.DataSource = bpro.GetListsByFilterString("CompID=" + GetCookieCompID());
        }


        ComboBox_ProductInfo.DataBind();
        ComboBox_StoreHouse.DataSource = bstorehouse.GetListsByFilterString("CompID=" + GetCookieCompID());
        ComboBox_StoreHouse.DataBind();
    }

    protected void GridView_RukuInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Label labelfhr = (Label)e.Row.FindControl("LabelFHR");
        //    labelfhr.Text = buser.GetList(int.Parse(labelfhr.Text)).LoginName;
        //}
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label Label_jianshu_ft = (Label) e.Row.FindControl("Label_jianshu_ft");
            int jianshu = 0;
            foreach (GridViewRow gr in GridView_RukuInfo.Rows)
            {
                jianshu += Convert.ToInt32(((Label) gr.FindControl("Label3")).Text);
            }
            Label_jianshu_ft.Text = jianshu.ToString();
        }
    }
}