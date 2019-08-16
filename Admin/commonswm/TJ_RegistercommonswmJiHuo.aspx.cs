using System;
using System.Web.UI.WebControls;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;
using TJ.BLL;
using System.Web.UI;
using System.Web.UI.HtmlControls;


public partial class Admin_commonswm_TJ_RegistercommonswmJiHuo : AuthorPage
{
    TabExecute _tab = new TabExecute();
    public CommonFun comfun = new CommonFun();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DisplayData(1, AspNetPager1.PageSize);
        }
    }

    protected void DisplayData(int pageIndex, int pageSize)
    {
        string _filtertemp = "UseSWM=1 and CompAutherID=67";
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            _filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
        }
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(CompID) from TJ_RegisterCompanys where " + _filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_RegisterCompanys", _filtertemp, "CompID", "CompID", pageSize);
        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    // ((HtmlImage)e.Row.FindControl("editimg")).Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString(), dataKey[1].ToString()));
                    e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString(), dataKey[1].ToString()));
                    ((HtmlImage)e.Row.FindControl("editimg")).Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString(), dataKey[1].ToString()));
                }
            }
        }
    }

    public string XiangXiLinkString(string ID,string CompTypeID)
    {
        if (ID.Length > 0)
        {
            if (CompTypeID.Equals("484")) {
                //双击后编辑修改方法
                return string.Format("javascript:var win=openWinCenter('TJ_RegistercommonswmJiHuoAndEdit.aspx?ID={0}&CompTypeID={1}',760,520,'个人信息')", ID, CompTypeID);
            }else
            {
                return string.Format("javascript:var win=openWinCenter('TJ_RegistercommonswmJiHuoAndEdit.aspx?ID={0}&CompTypeID={1}',760,520,'公司信息')", ID, CompTypeID);
            }
        }
        else
        {
            return "";
        }
    }

    protected string CompTypeID(string CompTypeID)
    {
        if (!string.IsNullOrEmpty(CompTypeID))
        {
            if (CompTypeID.Equals("484"))
            {
                return "个人";
            }else 
            {
                return "公司";
            }          
        }else
        {
            return "";
        }
    }

    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        DisplayData(1, AspNetPager1.PageSize);
    }
}