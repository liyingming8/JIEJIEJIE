using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using TJ.DBUtility;
using commonlib;
using Wuqi.Webdiyer;

public partial class Admin_TB_AllowPhoneNum : AuthorPage
{
    private readonly BTB_AllowPhoneNum bll = new BTB_AllowPhoneNum();
    private MTB_AllowPhoneNum mod = new MTB_AllowPhoneNum();
    private int _currentindex = 1;
    readonly TabExecutewuliu _tab = new TabExecutewuliu(); 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            _currentindex = 1;
            AspNetPager1.CurrentPageIndex = _currentindex;
            DisplayData(1, AspNetPager1.PageSize); 
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return
                string.Format(
                    "javascript:var win=openWinCenter('TB_AllowPhoneNumAddEdit.aspx?cmd=edit&ID={0}',450,300,'窜货督察授权')", ID);
        }
        else
        {
            return "";
        }
    }

    //private void fillgridview()
    //{
    //    if (inputSearchKeyword.Value.Trim().Length > 0)
    //    {
    //        GridView1.DataSource =
    //            bll.GetListsByFilterString(DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'");
    //    }
    //    else
    //    {
    //        GridView1.DataSource = bll.GetListsByFilterString("CompID=" + GetCookieCompID());
    //    }
    //    GridView1.DataBind();
    //}
    
    private void DisplayData(int pageIndex, int pageSize)
    {
        string comp = GetCookieCompID();
        if (GetCookieRID().Equals("1")||GetCookieRID().Equals("2"))
        {
            string filtertemp = "1=1"; 
            if (inputSearchKeyword.Value.Trim().Length > 0)
            {

                filtertemp = " " + DDLField.SelectedValue +" like '%" + inputSearchKeyword.Value.Trim() + "%'";
              
            } 
            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TB_AllowPhoneNum where " + filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TB_AllowPhoneNum", filtertemp, "ID", "ID", pageSize);
            GridView1.DataBind();
        }
        else
        {
            string filtertemp = "CompID=" + comp;
            if (inputSearchKeyword.Value.Trim().Length > 0)
            {
                filtertemp = "CompID =" + comp + " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'"; 
            } 
            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TB_AllowPhoneNum where " + filtertemp, null).Rows[0][0].ToString());
            GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TB_AllowPhoneNum", filtertemp, "ID", "ID", pageSize);
            GridView1.DataBind();
        } 
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["ID"].ToString()));
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        //fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#CCFF83'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
            var key = GridView1.DataKeys[e.Row.RowIndex];
            //((Label)e.Row.FindControl("LabelIndex")).Text = (GridView1.PageSize * GridView1.PageIndex + e.Row.RowIndex + 1).ToString();
            //if (1 == 1)
            //{
            //    ((LinkButton)e.Row.Cells[6].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
            //}
            //else
            //{
            //    e.Row.Cells[6].Enabled = false;
            //    e.Row.Cells[6].ForeColor = System.Drawing.Color.LightGray;
            //}

            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton) e.Row.Cells[3].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')");
            }
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        //fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        //fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }


    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}
