using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using commonlib;
using Wuqi.Webdiyer;
using System.Web.UI.HtmlControls;

public partial class CRM_TJ_CRM_BrandInfo : AuthorPage
{
    readonly PGTabExecuteCRM tab = new PGTabExecuteCRM();
    private int _currentindex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _currentindex = 1;
            AspNetPager1.CurrentPageIndex = 1;
            DisplayData(1, AspNetPager1.PageSize);
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('/crm/TJ_CRM_BrandInfoAddEdit.aspx?cmd=" + Sc.EncryptQueryString("edit") + "&ID={0}',820,560,'品牌信息')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    public string XiangXiLinkStringImage(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('/crm/tj_crm_brandpictures.aspx?bid={0}',800,500,'品牌图像组')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    public string ImageCutLink(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:openWinCenter(\"../imagecut/ImgCropper.htm?file=" + ID + "\", 820,560,\"图像裁剪\")");
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
            tab.ExecuteNonQuery("delete from public.tj_crm_brandinfo where ID=" + dataKey["ID"]);
            DisplayData(_currentindex, AspNetPager1.PageSize);
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
            if (dataKey != null)
            {
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
                ((HtmlImage)e.Row.FindControl("editimg")).Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString()));
                ((HyperLink)e.Row.FindControl("hlpictures")).Attributes.Add("onclick", XiangXiLinkStringImage(dataKey[0].ToString())); 
                ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex - 1) + e.Row.RowIndex + 1).ToString();
                /*
                if (IsSuperAdmin() && Checkpermitdel(int.Parse(dataKey["ID"].ToString())))
                {
                    ((LinkButton)e.Row.Cells[8].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
                }
                else
                {
                    e.Row.Cells[8].Enabled = false;
                    e.Row.Cells[8].ForeColor = Color.LightGray;
                }
                */
            }
        }
    }

    private bool Checkpermitdel(int id)
    {
        object vl = tab.ExecuteQueryForValue("select count(id) as vl from public.tj_crm_brandpictures where brandid=" + id);
        int outvl = 0;
        int.TryParse(vl.ToString(), out outvl);
        if (outvl > 0)
        {
            return false;
        }
        vl = tab.ExecuteQueryForValue("select count(id) as vl from public.tj_crm_brandprice where brandid=" + id);
        int.TryParse(vl.ToString(), out outvl);
        if (outvl > 0)
        {
            return false;
        }
        vl = tab.ExecuteQueryForValue("select count(id) as vl from public.tj_crm_customerorderinfo where brandid=" + id);
        int.TryParse(vl.ToString(), out outvl);
        if (outvl > 0)
        {
            return false;
        }
        return true;
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        DisplayData(1, AspNetPager1.PageSize);
    }

    private string _filtertemp = "";
    private void DisplayData(int pageIndex, int pageSize)
    {
        _filtertemp = "compid=" + GetCookieCompID();
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            _filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
        }
        AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from public.tj_crm_brandinfo where " + _filtertemp, null).Rows[0][0].ToString()); 
        GridView1.DataSource = tab.ExecuteQuery("select * from tj_crm_brandinfo where " + _filtertemp + " order by id limit " + pageSize + " offset " + (pageIndex - 1) * pageSize, null);
        GridView1.DataBind();
    }

    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}
