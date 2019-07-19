using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_TJ_CompADInfo : AuthorPage
{
    private readonly BTJ_CompADInfo bll = new BTJ_CompADInfo();
    private MTJ_CompADInfo mod = new MTJ_CompADInfo();
    public BTJ_User buser = new BTJ_User();
    private readonly BTJ_AdInfo badinfo = new BTJ_AdInfo(); 
    private readonly BTJ_RegisterCompanys bregistercompany = new BTJ_RegisterCompanys();
    private readonly BTB_Products_Infor bproduct = new BTB_Products_Infor();
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _currentindex = 1;
            AspNetPager1.CurrentPageIndex = _currentindex;
            DisplayData(1, AspNetPager1.PageSize);
        }
    }

    public string ReturnProductName(string Info_ID)
    {
        if (Info_ID.Equals("") || Info_ID == null || Info_ID == "0")
        {
            return "不限";
        }
        else
        {
            return bproduct.GetList(int.Parse(Info_ID)).Products_Name;
        }
    } 

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        var dataKey = GridView1.DataKeys[e.RowIndex];
        if (dataKey != null)
        {
            bll.Delete(int.Parse(dataKey["CPADID"].ToString())); 
            _currentindex = 1;
            AspNetPager1.CurrentPageIndex = _currentindex;
            DisplayData(1, AspNetPager1.PageSize);
        } 
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["CPADID"].ToString()));
        mod.Discriptions = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtDiscriptions")).Text.Trim();
        bll.Modify(mod);
        GridView1.EditIndex = -1;
        //fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            //双击后编辑修改方法
            return string.Format("javascript:var win=openWinCenter('TJ_CompADInfoAddEdit.aspx?cmd=edit&ID={0}',620,480,'广告信息编辑')", ID);
        }
        else
        {
            return "";
        }
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#CCFF83'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
            var key = GridView1.DataKeys[e.Row.RowIndex];
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton)e.Row.Cells[11].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')");
            }
        }
    }
   
    private void DisplayData(int pageIndex, int pageSize)
    {
        //string comp = GetCookieCompID();
        //if (comp == "1")
        //{
        //    string Filtertemp = "1=1";
        //    if (inputSearchKeyword.Value.Trim().Length > 0)
        //    {
        //        GridView1.DataSource =
        //            bll.GetListsByFilterString(" " + DDLField.SelectedValue + " like '%" +
        //                                       inputSearchKeyword.Value.Trim() + "%'");
        //    }
        //    else
        //    {
        //        //GridView1.DataSource = bll.GetListsByFilterString("CompID=" + GetCookieCompID());
        //        GridView1.DataSource = bll.GetListsByFilterString("1=1");
        //    }

        //    AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_CompADInfo where " + Filtertemp, null).Rows[0][0].ToString());
        //    GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_CompADInfo", Filtertemp, "CPADID", "CPADID", pageSize);
        //    GridView1.DataBind();
        //}
        //else
        //{
        //    string Filtertemp = "CompID=" + comp;


        //    if (inputSearchKeyword.Value.Trim().Length > 0)
        //    {
        //        GridView1.DataSource =
        //            bll.GetListsByFilterString("CompID=" + GetCookieCompID() + " and " + DDLField.SelectedValue + " like '%" +
        //                                       inputSearchKeyword.Value.Trim() + "%'");
        //    }
        //    else
        //    {
        //        GridView1.DataSource = bll.GetListsByFilterString("CompID=" + GetCookieCompID());
        //    }

        //    AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_CompADInfo where " + Filtertemp, null).Rows[0][0].ToString());
        //    GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_CompADInfo", Filtertemp, "CPADID", "CPADID", pageSize);
        //    GridView1.DataBind();
        //}

        string comp = GetCookieCompID();
        if (comp == "1")
        {
            string Filtertemp = "1=1";
            // string Filtertemp = "CompID =1";
            if (inputSearchKeyword.Value.Trim().Length > 0)
            {

                Filtertemp = " " + DDLField.SelectedValue +
                                                 " like '%" + inputSearchKeyword.Value.Trim() + "%'";
                //Filtertemp = "CompID = 1 and " + DDLField.SelectedValue +
                //                              " like '%" + inputSearchKeyword.Value.Trim() + "%'";//可以查询全部的
            }

            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_CompADInfo where " + Filtertemp, null).Rows[0][0].ToString());
           GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_CompADInfo", Filtertemp, "CPADID", "CPADID", pageSize);
            GridView1.DataBind();
        }
        else
        {
            string Filtertemp = "CompID=" + comp;
            if (inputSearchKeyword.Value.Trim().Length > 0)
            {

                Filtertemp = "CompID =" + GetCookieCompID() + " and " + DDLField.SelectedValue +
                                             " like '%" + inputSearchKeyword.Value.Trim() + "%'";
                //GridView1.DataSource =
                //    bll.GetListsByFilterString("CompID =" + GetCookieCompID() + " and " + DDLField.SelectedValue +
                //                               " like '%" + inputSearchKeyword.Value.Trim() + "%'");
            }
            //else
            //{
            //    GridView1.DataSource = bll.GetListsByFilterString("CompID =" + GetCookieCompID());
            //}
            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_CompADInfo where " + Filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_CompADInfo", Filtertemp, "CPADID", "CPADID", pageSize);
            GridView1.DataBind();
        }


    }
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);
        ///fillgridview();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);
        //  fillgridview();
    }

    public string ReturnAdinfoName(string ADID)
    {
        return badinfo.GetList(int.Parse(ADID)).ADName;
    }

    public string ReturnCompName(string CompID)
    {
        return bregistercompany.GetList(int.Parse(CompID)).CompName;
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}