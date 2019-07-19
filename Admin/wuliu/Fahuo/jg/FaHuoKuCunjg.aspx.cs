using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_wuliu_Fahuo_FaHuoKuCunjg : AuthorPage
{
    public BTJ_RegisterCompanys bagent = new BTJ_RegisterCompanys();
    private BTB_Products_Infor bpro = new BTB_Products_Infor();
    private BTB_StoreHouse bstorehouse = new BTB_StoreHouse();
    private BTB_ProductAuthorForAgent bproductforagent = new BTB_ProductAuthorForAgent();
    private readonly BTJ_User buser = new BTJ_User();
    private readonly BTB_CompAgentInfo bbagent = new BTB_CompAgentInfo();

    private readonly SqlConnection sqlconcom =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());

    public commwl comm = new commwl();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBox_RukuDateBegin.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            TextBox_RukuDateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
            FillDDL();
        }
    }


    protected void Button_Search_Click(object sender, EventArgs e)
    {
        IList<MTB_CompAgentInfo> mAgent = bbagent.GetListsByFilterString("AgentID=" + GetCookieCompID());
        if (mAgent.Count > 0)
        {
            int coid;
            foreach (MTB_CompAgentInfo ma in mAgent)
            {
                coid = ma.CompID;
                string gro = "group by fh.AgentID";
                string shstr =
                    "select fh.AgentID,max(fh.CompID)as CompID, max(fh.FHDate) as FHDate,sum(fh.XiangNumber) as XiangNumber,max(pr.Products_Name) as Products_Name,max(st.StoreHouseName ) as StoreHouseName,max(fh.FHUserID) as FHUserID FROM [TB_FaHuoInfo_" +
                    coid + "] fh,TB_Products_Infor pr,TB_StoreHouse st where fh.AgentID=" + GetCookieCompID() +
                    " and fh.FHTypeID=3 and fh.ProID=pr.Infor_ID and fh.STID=st.STID";
                shstr += " and fh.FHDate between '" + TextBox_RukuDateBegin.Text + "' and '" +
                         Convert.ToDateTime(TextBox_RukuDateEnd.Text).AddDays(1) + "'";
                shstr += gro;
                SqlDataAdapter sda1 = new SqlDataAdapter(shstr, sqlconcom);
                DataTable dttemp1 = new DataTable();
                sda1.Fill(dttemp1);
                GridViewSH.DataSource = dttemp1;
                GridViewSH.DataBind();
            }
        }


        string grouy = "group by fh.AgentID";
        //string sqlstring = "SELECT fh.FHPiCi,fh.FHDate,fh.AgentID,sum(fh.XiangNumber) XiangNumber,pr.Products_Name,st.StoreHouseName,fh.FHUserID FROM [TB_FaHuoInfo_" + GetCookieCompID() + "] fh,TB_Products_Infor pr,TB_StoreHouse st where   fh.FHTypeID=3 and fh.ProID=pr.Infor_ID and fh.STID=st.STID";
        string sqlstring =
            "select fh.AgentID,max(fh.FHDate) as FHDate,sum(fh.XiangNumber) as XiangNumber,max(pr.Products_Name) as Products_Name,max(st.StoreHouseName ) as StoreHouseName,max(fh.FHUserID) as FHUserID FROM [TB_FaHuoInfo_" +
            GetCookieCompID() +
            "] fh,TB_Products_Infor pr,TB_StoreHouse st where   fh.FHTypeID=3 and fh.ProID=pr.Infor_ID and fh.STID=st.STID";
        sqlstring += " and fh.FHDate between '" + TextBox_RukuDateBegin.Text + "' and '" +
                     Convert.ToDateTime(TextBox_RukuDateEnd.Text).AddDays(1) + "'";
        if (ComboBox_DaiLiShangID.SelectedValue != "0" && ComboBox_DaiLiShangID.SelectedValue != null)
        {
            sqlstring += " and fh.AgentID=" + ComboBox_DaiLiShangID.SelectedValue + grouy;
        }
        else
        {
            sqlstring += grouy;
        }


        //if(ComboBox_ProInfo.SelectedValue!="0"&&ComboBox_ProInfo.SelectedValue!=null)
        //{
        //    sqlstring += " and fh.ProID=" + ComboBox_ProInfo.SelectedValue + grouy;
        //}
        //if (ComboBox_StoreHouse.SelectedValue != "0" && ComboBox_StoreHouse.SelectedValue != null)
        //{
        //    sqlstring += " and fh.STID=" + ComboBox_StoreHouse.SelectedValue + grouy;
        //}

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
        ComboBox_DaiLiShangID.DataSource =
            bagent.GetListsByFilterString("ParentID=" + GetCookieCompID() + " and CompTypeID=" +
                                          DAConfig.CompTypeIDJingXiaoShang);
        ComboBox_DaiLiShangID.DataBind();
        //if(GetCookieCompTypeID()==DAConfig.CompTypeIDJingXiaoShang.ToString().Trim())
        //{
        //    CommonFunWL comwl = new CommonFunWL();
        //    ComboBox_ProInfo.DataSource = comwl.ReturnGetAgentAuthorProductInfo(GetCookieCompID());
        //}
        //else
        //{
        //    ComboBox_ProInfo.DataSource = bpro.GetListsByFilterString("CompID=" + GetCookieCompID());
        //}
        //ComboBox_ProInfo.DataBind();
        //ComboBox_StoreHouse.DataSource = bstorehouse.GetListsByFilterString("CompID="+GetCookieCompID());
        //ComboBox_StoreHouse.DataBind();
    }

    public int kcsy(int num, string agID)
    {
        int agnum = int.Parse(comm.getDailishangkucun(agID, TextBox_RukuDateBegin.Text, TextBox_RukuDateEnd.Text));
        int synum = num - agnum;
        if (synum < 0)
        {
            return 0;
        }
        else
        {
            return synum;
        }
    }

    public string JiageLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('FaHuoDetialjg.aspx?Agent_id={0}',900,600)", ID);
        }
        else
        {
            return "";
        }
    }

    protected void GridView_RukuInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label labelfhr = (Label) e.Row.FindControl("LabelFHR");
            labelfhr.Text = buser.GetList(int.Parse(labelfhr.Text)).LoginName;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label Label_jianshu_ft = (Label) e.Row.FindControl("Label_jianshu_ft");
            int jianshu = 0;
            foreach (GridViewRow gr in GridView_RukuInfo.Rows)
            {
                jianshu += Convert.ToInt32(((Label) gr.FindControl("Label_jianshu")).Text);
            }
            Label_jianshu_ft.Text = jianshu.ToString();
        }
    }

    protected void GridViewSH_RukuInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label labelfhs = (Label) e.Row.FindControl("LabelFHS");
            labelfhs.Text = buser.GetList(int.Parse(labelfhs.Text)).LoginName;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lallnum = (Label) e.Row.FindControl("LallNum");
            int jianshu = 0;
            foreach (GridViewRow gr in GridViewSH.Rows)
            {
                jianshu += Convert.ToInt32(((Label) gr.FindControl("LFhNum")).Text);
            }
            lallnum.Text = jianshu.ToString();
        }
    }
}