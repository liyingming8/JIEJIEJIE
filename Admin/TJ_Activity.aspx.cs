using System;
using System.Drawing; 
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using commonlib;
using Wuqi.Webdiyer;
using System.Web.UI.HtmlControls;

public partial class Admin_TJ_Activity : AuthorPage
{
    BTJ_Activity bll = new BTJ_Activity();
    TabExecute tab = new TabExecute();
    public BTJ_Activity_Strategy BtjActivityStrategy = new BTJ_Activity_Strategy();
    public BTJ_User BtjUser = new BTJ_User();
    private int _currentindex = 1;
    public commfrank Commfrank = new commfrank();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _currentindex = 1;
            AspNetPager1.CurrentPageIndex = 1;
            DisplayData(1, AspNetPager1.PageSize);
        }
    }

    private string temp = "";
    public string ReturnFaceTo(string id)
    {
        temp = "";
        switch (id)
        {
            case "1":
                temp = "消费者";
                break;
            case "2":
                temp = "经销商";
                break;
            case "3":
                temp = "终端店";
                break; 
        }
        return temp;
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_ActivityAddEdit.aspx?cmd=" + Sc.EncryptQueryString("edit") + "&ID={0}',600,470,'活动信息')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    public string XiangXiJiangXiangSheZhiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_Activity_Prizes.aspx?acid={0}',680,560,'活动奖项设置')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    public string XiangXiZhongJiangHaoMaSpanLinkString(string acid,string type,string name)
    {
        string tempvl = "";
        if (acid.Length > 0)
        {
            switch (type)
            {
                case "1":
                    tempvl = string.Format("javascript:var win=openWinCenter('TJ_Activity_Product.aspx?acid={0}&acnm={1}',780,620,'产品范围')", Sc.EncryptQueryString(acid), Sc.EncryptQueryString(name));
                    break;
                case "2":
                    tempvl = string.Format("javascript:var win=openWinCenter('TJ_Activity_Agent.aspx?acid={0}&acnm={1}',780,620,'经销商范围')", Sc.EncryptQueryString(acid), Sc.EncryptQueryString(name));
                    break;
                case "3":
                    tempvl = string.Format("javascript:var win=openWinCenter('TJ_Activity_Terminal.aspx?acid={0}&acnm={1}',780,620,'终端范围')", Sc.EncryptQueryString(acid), Sc.EncryptQueryString(name));
                    break;
                case "4":
                    tempvl = string.Format("javascript:var win=openWinCenter('TJ_Activity_CodeSpanAddEdit.aspx?acid={0}&acnm={1}',600,430,'其他限定')", Sc.EncryptQueryString(acid), Sc.EncryptQueryString(name));
                    break;
            }
            return tempvl; 
        }
        return "";
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
                ((HyperLink)e.Row.FindControl("hyperlinkJiangxiang")).Attributes.Add("onclick", XiangXiJiangXiangSheZhiLinkString(dataKey["id"].ToString()));
                ((HyperLink)e.Row.FindControl("hlinkterminal")).Attributes.Add("onclick", XiangXiZhongJiangHaoMaSpanLinkString(dataKey["id"].ToString(), "3", dataKey["AName"].ToString()));
                ((HyperLink)e.Row.FindControl("hlinkagent")).Attributes.Add("onclick", XiangXiZhongJiangHaoMaSpanLinkString(dataKey["id"].ToString(), "2", dataKey["AName"].ToString()));
                if (dataKey["FaceTo"].ToString().Equals("1"))
                {
                    ((HyperLink)e.Row.FindControl("hlinkterminal")).Enabled = false;
                    ((HyperLink)e.Row.FindControl("hlinkterminal")).Attributes.Clear();
                    ((HyperLink)e.Row.FindControl("hlinkterminal")).ForeColor = Color.Gray;
                }
                if (dataKey["FaceTo"].ToString().Equals("2"))
                { 
                    ((HyperLink) e.Row.FindControl("hlinkterminal")).Enabled = false;
                    ((HyperLink)e.Row.FindControl("hlinkterminal")).Attributes.Clear();
                    ((HyperLink) e.Row.FindControl("hlinkterminal")).ForeColor = Color.Gray;
                }
                if (dataKey["FaceTo"].ToString().Equals("3"))
                {
                    ((HyperLink) e.Row.FindControl("hlinkagent")).Enabled = false;
                    ((HyperLink)e.Row.FindControl("hlinkagent")).Attributes.Clear();
                    ((HyperLink) e.Row.FindControl("hlinkagent")).ForeColor = Color.Gray;
                }
                ((HyperLink)e.Row.FindControl("hlinkprod")).Attributes.Add("onclick", XiangXiZhongJiangHaoMaSpanLinkString(dataKey["id"].ToString(), "1", dataKey["AName"].ToString()));
                ((HyperLink)e.Row.FindControl("hlinkother")).Attributes.Add("onclick", XiangXiZhongJiangHaoMaSpanLinkString(dataKey["id"].ToString(), "4", dataKey["AName"].ToString()));
            } 
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex - 1) + e.Row.RowIndex + 1).ToString();
            /*
            if (IsSuperAdmin())
            {
                ((LinkButton)e.Row.Cells[12].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除当前记录吗?')");
            }
            else
            {
                e.Row.Cells[12].Enabled = false;
                e.Row.Cells[12].ForeColor = Color.LightGray;
            }
            */
        }
    }

    public string ReturnCodeType(string prodidstring,object yzm)
    {
        string[] prodarray = prodidstring.Split(new []{','}, StringSplitOptions.RemoveEmptyEntries);
        string tempvalue = "";
        foreach (string s in prodarray)
        {
            switch (s)
            {
                case  "1":
                    tempvalue += string.IsNullOrEmpty(tempvalue) ? "箱码" : "," + "箱码";
                    break;
                case "2":
                    tempvalue += string.IsNullOrEmpty(tempvalue) ? "瓶码" : "," + "瓶码";
                    break;
                case "3":
                    tempvalue += string.IsNullOrEmpty(tempvalue) ? "活动码" : "," + "活动码";
                    break;
            }
        }
        if (Convert.ToBoolean(yzm))
        {
            tempvalue += "+验证码";
        }
        return tempvalue;
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        _currentindex = 1;
        AspNetPager1.CurrentPageIndex = 1;
        DisplayData(1, AspNetPager1.PageSize);
    }

    private string _filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        _filtertemp = "CompID="+GetCookieCompID();
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            _filtertemp += " and "+ DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
        } 
        AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from TJ_Activity where " + _filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_Activity", _filtertemp, "id", "id", pageSize);
        GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    } 
}
