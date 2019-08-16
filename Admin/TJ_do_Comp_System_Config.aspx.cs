using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL; 
using commonlib;
using TJ.Model;

public partial class Admin_TJ_do_Comp_System_Config : AuthorPage
{
    BTJ_Comp_System_Config bll = new BTJ_Comp_System_Config();
    //MTJ_Comp_System_Config mod = new MTJ_Comp_System_Config();
    TabExecute tab = new TabExecute();
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
            return string.Format("javascript:var win=openWinCenter('TJ_Comp_System_ConfigAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+ "&ID={0}',600,450,'积分商城设置')", Sc.EncryptQueryString(ID));
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
            bll.Delete(int.Parse(dataKey["id"].ToString()));
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
            }   
        }
    }
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
       _currentindex = 1;
       AspNetPager1.CurrentPageIndex = 1;
       DisplayData(1, AspNetPager1.PageSize);
    }

    private string _filtertemp ="1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        _filtertemp = "compid=" + GetCookieCompID();
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from TJ_Comp_System_Config where "+_filtertemp, null).Rows[0][0].ToString());
        if (AspNetPager1.RecordCount < 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(),"alert", "openWinCenter('TJ_Comp_System_ConfigAddEdit.aspx?cmd=" + Sc.EncryptQueryString("add") + "',600,450,'积分商城设置')",true);
        }
        else
        {
            GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_Comp_System_Config", _filtertemp, "id", "id", pageSize);
            GridView1.DataBind();
            input_permitgo.Value = "1";
        } 
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }

    protected void ckb_openpoints_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        int index = ((GridViewRow)(chk.NamingContainer)).RowIndex;    //通过NamingContainer可以获取当前checkbox所在容器对象，即gridviewrow   
        DataKey dk = GridView1.DataKeys[index]; 
        if (dk != null)
        {
            MTJ_Comp_System_Config mod = bll.GetList(int.Parse(dk["id"].ToString()));
            mod.openpoints = chk.Checked;
            bll.Modify(mod);
            DisplayData(1, AspNetPager1.PageSize);
        } 
    }
}
