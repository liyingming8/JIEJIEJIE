using System;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using TJ.Model;
using commonlib;
public partial class Admin_TJ_Activity_Agent : AuthorPage
{
    BTJ_Activity_Agent bll = new BTJ_Activity_Agent(); 
    TabExecute tab = new TabExecute();
    DBClass db = new DBClass();
    private int _currentindex = 1; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["acid"]))
            {
                hd_acid.Value = Sc.DecryptQueryString(Request.QueryString["acid"].Trim());
                hd_acnm.Value = Sc.DecryptQueryString(Request.QueryString["acnm"].Trim());
            }
            else
            {
                Response.End();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["pcompid"]))
            {
                hd_agentid.Value = Request.QueryString["pcompid"];
                hd_agentnm.Value = Request.QueryString["pcompnm"];
                if (!bll.CheckIsExistByFilterString("acid=" + hd_acid.Value + " and agentid=" + hd_agentid.Value))
                {
                    var mod = new MTJ_Activity_Agent();
                    mod.acid = int.Parse(hd_acid.Value);
                    mod.agentid = int.Parse(hd_agentid.Value);
                    mod.agentname = hd_agentnm.Value;
                    bll.Insert(mod);
                    db.Updateactivitynum(hd_acid.Value, "ag");
                } 
            }
             _currentindex = 1;
             AspNetPager1.CurrentPageIndex = 1;
             DisplayData(1, AspNetPager1.PageSize);
            add.Attributes.Add("onclick",ReturnCompnaySelectScript("指定经销商",GetCookieCompID(),""));
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_Activity_AgentAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}',600,450,'TJ_Activity_Agent')", Sc.EncryptQueryString(ID));
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
            db.Updateactivitynum(hd_acid.Value, "ag");
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
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex-1) + e.Row.RowIndex + 1).ToString();
            ((LinkButton)e.Row.Cells[3].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除当前记录吗?')");
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
        _filtertemp = "acid=" + hd_acid.Value;
        if (inputSearchKeyword.Value.Trim().Length > 0)
       {
         _filtertemp += " and "+ DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
       } 
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from TJ_Activity_Agent where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_Activity_Agent", _filtertemp, "id", "id", pageSize);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }
}
