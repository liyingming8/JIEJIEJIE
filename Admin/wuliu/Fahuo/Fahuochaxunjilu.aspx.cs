using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using TJ.Model;
using commonlib;
using System.Data;
using Wuqi.Webdiyer;

public partial class Admin_wuliu_fahuo_Fahuochaxunjilu : AuthorPage
{
    public MTJ_User bulluser = new MTJ_User();
    public BTJ_User bolluesr = new BTJ_User(); 
    public BTJ_RegisterCompanys bollRegisterCompanys = new BTJ_RegisterCompanys();
    public MTB_Products_Infor bullProducts = new MTB_Products_Infor();
    public BTB_StoreHouse bllsthose = new BTB_StoreHouse();
    public BTB_Products_Infor bllProducts = new BTB_Products_Infor();
    private readonly CommonFunWL comwl = new CommonFunWL();
       public string qdcompid = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            TextBox_RukuDateBegin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            TextBox_RukuDateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
            FillDDL();
            DisplayData(1, AspNetPager1.PageSize);
        } 
    }

    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);

    }
    private void DisplayData(int pageIndex, int pageSize)
    {
        string Filtertemp = "";
        string compid = GetCookieCompID();
        if (int.Parse(compid) == 99)
        {
            Filtertemp += "frcompid=" + compid;
        }
        else
        {
            Filtertemp += "frcompid=" + compid;
        }
        //string straid = "";
        //string strpid = "";
        //string strstid = "";
        qdcompid = GetCookieCompID();
        if (qdcompid !="99")
        {
            if (ComboBox_DaiLiShangID.SelectedValue != "0" && ComboBox_DaiLiShangID.SelectedValue != null)
            {//传的是ID 
                Filtertemp += " and tocompid= '" + ComboBox_DaiLiShangID.SelectedValue + "' ";
            }
            if (ComboBox_ProInfo.SelectedValue != "0" && ComboBox_ProInfo.SelectedValue != null)
            {
                Filtertemp += " and  pid='" + ComboBox_ProInfo.SelectedValue + "'";
            }
            if (ComboBox_StoreHouse.SelectedValue != "0" && ComboBox_StoreHouse.SelectedValue != null)
            {
                Filtertemp += " and frstoid= '" + ComboBox_StoreHouse.SelectedValue + "'";
            }

        } else
        {
            if (ComboBox_DaiLiShangID.SelectedValue != "0" && ComboBox_DaiLiShangID.SelectedValue != null)
            {//传的是ID 
                Filtertemp += " and tocompid= '" + bollRegisterCompanys.GetListsByFilterString("compid=" + ComboBox_DaiLiShangID.SelectedValue)[0].Agent_Code + "' ";
            }
            if (ComboBox_ProInfo.SelectedValue != "0" && ComboBox_ProInfo.SelectedValue != null)
            {
                Filtertemp += " and  pid='" + bllProducts.GetListsByFilterString("Infor_ID='" + ComboBox_ProInfo.SelectedValue + "'")[0].Product_Code + "'";
            }
            if (ComboBox_StoreHouse.SelectedValue != "0" && ComboBox_StoreHouse.SelectedValue != null)
            {
                Filtertemp += " and frstoid= '" + bllsthose.GetListsByFilterString("stid=" + ComboBox_StoreHouse.SelectedValue)[0].StoreHouseCode + "'";
            }

        }

        Filtertemp += " and  tm between '" + TextBox_RukuDateBegin.Text + "' and '" +TextBox_RukuDateEnd.Text+ "'";

        PGTabExecuteWuLiu PGNEW = new PGTabExecuteWuLiu();
        AspNetPager1.RecordCount = int.Parse(PGNEW.ExecuteQuery("select count(*) from  delivery_summary where " + Filtertemp, null).Rows[0][0].ToString());
        DataTable basetable = new DataTable();
        basetable = PGNEW.ExecuteNonQuery("SELECT tm  ,fhpici,userid,tocompid ,xiangnumber,frstoid   , pid  FROM delivery_summary  where " + Filtertemp + " order by tm desc ");
        GridView1.DataSource = basetable;
        GridView1.DataBind();
    }
    public string proname(string pid)
    {
        string name = string.Empty;
        IList<MTB_Products_Infor> mpro = bllProducts.GetListsByFilterString("Product_Code='" +pid+"'");
        if (mpro.Count > 0)
        {
            name = mpro[0].Products_Name;
        }
        else
        {
            name = "系列产品";
        }
        return name;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            bulluser = bolluesr.GetList(int.Parse(GridView1.DataKeys[e.Row.RowIndex]["userid"].ToString()));
            ((Label)e.Row.FindControl("LabelNickName")).Text = bulluser.LoginName;  
        }
    }

    private void FillDDL()
    {
        string tempagentidstring = comwl.GetAgentIDStringByCompID(GetCookieCompID());
        if (tempagentidstring.Length > 0)
        {
            ComboBox_DaiLiShangID.DataSource = bollRegisterCompanys.GetListsByFilterString("CompName !='' and CompID in (" + tempagentidstring + ")");
            ComboBox_DaiLiShangID.DataBind();
        }
        if (GetCookieCompTypeID() == DAConfig.CompTypeIDJingXiaoShang.ToString().Trim())
        {
            ComboBox_ProInfo.DataSource = comwl.ReturnGetAgentAuthorProductInfo(GetCookieCompID());
        }
        else
        {
            ComboBox_ProInfo.DataSource = bllProducts.GetListsByFilterString("Products_Name!='' and CompID=" + GetCookieCompID());
        }
        ComboBox_ProInfo.DataBind();
        ComboBox_StoreHouse.DataSource = bllsthose.GetListsByFilterString("StoreHouseName !='' and CompID=" + GetCookieCompID());
        ComboBox_StoreHouse.DataBind();
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      
        DisplayData(1, AspNetPager1.PageSize);
    }

}