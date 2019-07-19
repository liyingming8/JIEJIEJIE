using System;
using System.Data;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using TJ.DBUtility;
using TJ.BLL; 
using commonlib;
public partial class Admin_TJ_Activity_JXS_Win : AuthorPage
{
    BTJ_Activity_JXS_Win bll = new BTJ_Activity_JXS_Win(); 
    TabExecute _tab = new TabExecute();
    public BTJ_RegisterCompanys BtjRegister = new BTJ_RegisterCompanys();
    public BTJ_AwardType BtjAwardType = new BTJ_AwardType();
    private int _currentindex = 1; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hdcompid.Value = !string.IsNullOrEmpty(Request.QueryString["pcompid"]) ? Request.QueryString["pcompid"].Trim() : "";
            if (!string.IsNullOrEmpty(Request.QueryString["pcompnm"]))
            {
                inputjxs.Value = Request.QueryString["pcompnm"].Trim();
            }
            txt_start.Text = DateTime.Now.ToString("yyyy-MM-01");
            txt_end.Text =
                Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).ToString("yyyy-MM-dd");
             _currentindex = 1;
             AspNetPager1.CurrentPageIndex = 1;
             DisplayData(1, AspNetPager1.PageSize);
            if (GetCookieCompID() == "2")
            {
                inputjxs.Attributes.Add("onclick", XiangXiLinkStringForTerminal());
                btn_refresh.Visible = true;
            }
            else
            {
                inputjxs.Visible = false;
                btn_refresh.Visible = false;
            } 
             //FillDdl();
        }
    }
    public string XiangXiLinkStringForTerminal()
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('../Admin/wuliu/TB_Agents_Infor_Terminal_select.aspx?fr=" + Sc.EncryptQueryString(Request.RawUrl) + "',580,460,'指定终端店')");
        }
        return "";
    }
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_Activity_JXS_WinAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}',600,450,'TJ_Activity_JXS_Win')", Sc.EncryptQueryString(ID));
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
             var key = GridView1.DataKeys[e.RowIndex];
             if (key != null)
                 bll.Delete(int.Parse(key["id"].ToString()));
         }
        DisplayData(_currentindex, AspNetPager1.PageSize);
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
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex-1) + e.Row.RowIndex + 1).ToString(); 
        }
    } 
    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
       _currentindex = 1;
       AspNetPager1.CurrentPageIndex = 1;
       DisplayData(1, AspNetPager1.PageSize);
    } 
    
    private void DisplayData(int pageIndex, int pageSize)
    {
        string filtertemp = "";
        if (string.IsNullOrEmpty(hdcompid.Value))
        {
            filtertemp = "agentid=" + GetCookieCompID(); 
        }
        else
        {
            filtertemp = "agentid=" + hdcompid.Value; 
        }
        if (inputSearchKeyword.Value.Trim().Length > 0)
       {
         filtertemp += " and "+ DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
       } 
       AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(id) from TJ_Activity_JXS_Win where "+filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_Activity_JXS_Win", filtertemp, "id", "id", pageSize);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    } 
    TabExecutewuliu _tabwuliu  = new TabExecutewuliu();
    readonly InternetHandle _internet = new InternetHandle();
    protected void btn_refresh_Click(object sender, EventArgs e)
    {
        DataTable dttemp = _tabwuliu.ExecuteNonQuery(
            "SELECT COUNT(ID) cnt,AcceptAgentID,ProID,AcceptDay,KhDDH FROM AgentAcceptInfo_2019 where ParentID=" +
            GetCookieCompID() + " and isprized=0 group by AcceptAgentID,ProID,AcceptDay,KhDDH");
        if (dttemp.Rows.Count > 0)
        {
            foreach (DataRow row in dttemp.Rows)
            {
                 string temp =  _internet.GetUrlData("http://os.china315net.com/ajax/getactivityinfoforjingxiaoshang.ashx?unid=" + GetCookieCompID() +
                                    "&agentid=" + row["AcceptAgentID"] + "&proid=" + row["ProID"] + "&fhdate=" +
                                    Convert.ToDateTime(row["AcceptDay"]).ToString("yyyy-MM-dd")  + (row["KhDDH"].ToString().Equals("")?"":"&khddh="+row["KhDDH"]));
                if (!temp.Equals("0"))
                {   
                    JArray jsonarray = JArray.Parse(temp);
                    if (jsonarray.Count > 0)
                    {
                        JObject obj = JObject.Parse(jsonarray[0].ToString());
                        int t =  _tab.ExecuteNonQuery("INSERT INTO TJ_Activity_JXS_Win(compid,agentid,awtypeid,winreason,prizevl,prizeintro,gettm,confirmtm) VALUES(" +GetCookieCompID() + "," + row["AcceptAgentID"] + "," + obj["tpid"] + ",'经销商扫码积分'," +Convert.ToDecimal(obj["prvl"].ToString())*Convert.ToInt32(row["cnt"]) + ",'" + obj["awnm"] + "','" + Convert.ToDateTime(row["AcceptDay"]) + "','" +DateTime.Now + "')",null);
                        if (t > 0)
                        {
                            _tabwuliu.ExecuteQuery(
                                "update AgentAcceptInfo_2019 set isprized=1 where AcceptAgentID=" + row["AcceptAgentID"] +
                                " and  ProID=" + row["ProID"] + " and AcceptDay='" +
                                Convert.ToDateTime(row["AcceptDay"]).ToString("yyyy-MM-dd")+"'" + (row["KhDDH"].ToString().Length>0? " and KhDDH='" +
                                row["KhDDH"] + "'":""), null);
                        }
                    }
                }
            } 
        }
        dttemp.Dispose();
        DisplayData(1, AspNetPager1.PageSize);
    }
}
