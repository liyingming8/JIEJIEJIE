using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL; 
using commonlib;
using TJ.Model;

public partial class Admin_TJ_do_Holiday : AuthorPage
{
    BTJ_Holiday bll = new BTJ_Holiday(); 
    TabExecute tab = new TabExecute();
    private int _currentindex = 1; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string tempsql = "SELECT * FROM TJ_BaseHoliday bs where bs.isdefault=1 and startdate>'" + DateTime.Now +
                             "' and bs.id not in (SELECT h.bhid FROM TJ_Holiday h where h.compid=" + GetCookieCompID() +")";
            DataTable dt = tab.ExecuteQuery(tempsql, null);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    bll.Insert(new MTJ_Holiday(0, row["name"].ToString(), row["subtitle"].ToString(),
                        row["contents"].ToString(), row["img"].ToString(), false, int.Parse(GetCookieCompID()), 0, 0,
                        int.Parse(row["id"].ToString()), Convert.ToDateTime(row["startdate"]),
                        Convert.ToDateTime(row["enddate"])));
                }
            }
            AspNetPager1.CurrentPageIndex = 1;
            DisplayData(1, AspNetPager1.PageSize); 
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_HolidayAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}',600,450,'活动信息')", Sc.EncryptQueryString(ID));
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
            BTJ_Games_Details btjGamesDetails = new BTJ_Games_Details();
            btjGamesDetails.Delete("compid="+GetCookieCompID()+" and hid="+dataKey["id"]); 
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
            //((LinkButton)e.Row.Cells[6].Controls[0]).Attributes.Add("onclick", "javascript:setstop();return confirm('你确认要删除当前记录吗?');");
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
        if (inputSearchKeyword.Value.Trim().Length > 0)
       {
         _filtertemp += " and name like '%" + inputSearchKeyword.Value.Trim() + "%'";
       } 
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from TJ_Holiday where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_Holiday", _filtertemp, "startdate", "id", pageSize);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }

    protected void ckb_isopen_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        int index = ((GridViewRow)(chk.NamingContainer)).RowIndex;    //通过NamingContainer可以获取当前checkbox所在容器对象，即gridviewrow   
        DataKey dk = GridView1.DataKeys[index];
        MTJ_Holiday mod = bll.GetList(int.Parse(dk["id"].ToString()));
        mod.isopen = chk.Checked;
        bll.Modify(mod);
        DisplayData(1, AspNetPager1.PageSize); 
    }
}
