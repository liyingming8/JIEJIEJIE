using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.BLL;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;
using System.Web.UI.HtmlControls;

public partial class Admin_TJ_AwardInfo : AuthorPage
{
    private readonly BTJ_AwardInfo _bll = new BTJ_AwardInfo(); 
    private readonly BTJ_Integral _bintegral = new BTJ_Integral();
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();
    public BTJ_AwardType BtjAwardType = new BTJ_AwardType();
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
            return string.Format("javascript:var win=openWinCenter('TJ_AwardInfoAddEdit.aspx?cmd=edit&ID={0}',720,660,'奖品管理')", ID);
        }
        else
        {
            return "";
        }
    }

    public string FaceToString(string faceto)
    {
        string temp = "通用";
        switch (faceto)
        {
            case "0":
                temp = "通用";
                break;
            case "1":
                temp = "消费者";
                break;
            case "3":
                temp = "终端店";
                break;
        }
        return temp;
    }

    public string XiangXiLinkStringKuCun(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_AwardInfoKuCunAddEdit.aspx?ID={0}',500,300,'奖品库存')", ID);
        }
        else
        {
            return "";
        }
    }

    public string XiangXiLinkStringImageList(string awid,string awdnm)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_AwardInfo_Images.aspx?awid={0}&awnm={1}',600,500,'{1}')", awid, awdnm);
        }
        else
        {
            return "";
        }
    }

    private void DisplayData(int pageIndex, int pageSize)
    { 
        string comp = GetCookieCompID(); 
        string filtertemp = "CompID=" + GetCookieCompID();
        if (inputSearchKeyword.Value.Trim().Length > 0)
        { 
            filtertemp = "CompID =" + GetCookieCompID() + " and " + DDLField.SelectedValue +" like '%" + inputSearchKeyword.Value.Trim() + "%'";
        }
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(AWID) from TJ_AwardInfo where " + filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_AwardInfo", filtertemp, "AWID", "AWID", pageSize);
        GridView1.DataBind();
    } 

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        var dataKey = GridView1.DataKeys[e.RowIndex];
        if (dataKey != null)
        {
            _bll.Delete(int.Parse(dataKey["AWID"].ToString()));
        } 
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

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
            {
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
                ((HtmlImage)e.Row.FindControl("editimg")).Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString()));
                /*
                ((HyperLink)e.Row.FindControl("hlink_img_view")).Attributes.Add("onclick", XiangXiLinkStringImageList(dataKey["AWID"].ToString(), dataKey["AwardThing"].ToString()));
                ((HyperLink)e.Row.FindControl("hplinkkucun")).Attributes.Add("onclick", XiangXiLinkStringKuCun(dataKey["AWID"].ToString()));
                if (Convert.ToInt32(dataKey["kucunshuliang"]) < 10)
                {
                    e.Row.BackColor=Color.DarkOrange;
                }
                */
            }  
            /*
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                if (IsSuperAdmin())
                {
                    ((LinkButton)e.Row.Cells[14].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
                }
                else
                {
                    ((LinkButton) e.Row.Cells[14].Controls[0]).Enabled = false;
                    ((LinkButton) e.Row.Cells[14].Controls[0]).ForeColor = Color.Gray;
                }
            } 
            */
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
        // fillgridview();
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = _currentindex;
        DisplayData(1, AspNetPager1.PageSize);

    }

    public string ReturnIntegalName(string atgid)
    {
        return _bintegral.GetList(int.Parse(atgid)).IntegralName;
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}