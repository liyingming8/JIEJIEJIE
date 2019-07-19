using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.DBUtility;
using TJ.Model;
using commonlib;
using Wuqi.Webdiyer;

public partial class Admin_TJ_User_ZhongDuan : AuthorPage
{
    private readonly BTJ_User bll = new BTJ_User(); 
    private readonly BTJ_RoleInfo bllrole = new BTJ_RoleInfo();
    private readonly BTJ_RegisterCompanys blrcompany = new BTJ_RegisterCompanys();
    private readonly CommonFun comfun = new CommonFun();
    private readonly CommonFunWL comwl = new CommonFunWL();
    readonly TabExecute _tab = new TabExecute();
    private int _currentindex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            DisplayData(1, AspNetPager1.PageSize);
        }
    }

    private string returnvalue = "";

    public string ReturnUserStatu(object IsActive)
    {
        switch (IsActive.ToString())
        {
            case "0":
                returnvalue = "未激活";
                break;
            case "1":
                returnvalue = "已激活";
                break;
            case "2":
                returnvalue = "试用中";
                break;
            case "3":
                returnvalue = "已冻结";
                break;
            default:
                returnvalue = "";
                break;
        }
        return returnvalue;
    } 

    private string _filtertemp = "";
    private void DisplayData(int pageIndex, int pageSize)
    {
        //string tempagentid = comwl.GetAgentIDStringByCompID(GetCookieCompID());
        //string tempchildcompid = comfun.ReturnChildCompIDString(GetCookieCompID(), true);
        //_filtertemp = "RID is not null and (CompID in (" + tempagentid + ") or CompID in (" + tempchildcompid + ") or CompID=" + GetCookieCompID() + ")";
        string tempagentid = comwl.GetAgentIDStringByCompID("2");
        string tempchildcompid = comfun.ReturnChildCompIDString("2", true);
        _filtertemp = "RID in (22,160) and (CompID in (" + tempagentid + ") or CompID in (" + tempchildcompid + ") or CompID=" +2 + ")";
        if (inputSearchKeyword.Value.Length > 0)
        {
            _filtertemp += " and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value + "%'";
        }
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(UserID) from TJ_User where " + _filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_User", _filtertemp, "UserID", "UserID", pageSize);
        GridView1.DataBind();
    }
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            //双击后编辑修改方法
            return string.Format("javascript:var win=openWinCenter('TJ_UserAddEdit.aspx?cmd=edit&ID={0}',680,580,' 用户信息编辑')", ID);
        }
        else
        {
            return "";
        }
    }

    private string temp = "";
    public string ReturnRoleNameByRid(string RID)
    { 
        temp = "";
        if (!string.IsNullOrEmpty(RID))
        {
            DataTable dttemp = _tab.ExecuteQuery("select RoleName from TJ_RoleInfo where RID=" + RID, null);
            if (dttemp.Rows.Count > 0)
            {
                temp = dttemp.Rows[0][0].ToString();
            }
            dttemp.Dispose();
        } 
        return temp; 
    }
   

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["UserID"].ToString()));
        if (_currentindex == 0)
        {
            _currentindex = 1;
        }
        DisplayData(_currentindex, AspNetPager1.PageSize);
    }

    public string ReturnPlaceByID(string CID)
    {
        if (string.IsNullOrEmpty(CID))
        {
            return "";
        }
        return comfun.ReturnBaseClassName(CID, true, false);
        
    } 
    public IList<MTJ_RoleInfo> GetRoleInfo()
    {
        return
            bllrole.GetListsByFilterString("RID in(" + bllrole.GetList(int.Parse(GetCookieRID())).AuthorRoleIDInfo + ")");
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
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                if (GridView1.DataKeys[e.Row.RowIndex]["CompID"].ToString().Equals("0"))
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = Color.Gray;
                }
                else
                {
                    Label labactived = (Label) e.Row.FindControl("LabelIsActived");
                    switch (labactived.Text.Trim())
                    {
                        case "未激活":
                            labactived.ForeColor = Color.Red;
                            break;
                        case "已激活":
                            labactived.ForeColor = Color.Green;
                            break;
                        case "试用中":
                            labactived.ForeColor = Color.Orange;
                            break;
                        case "已冻结":
                            labactived.ForeColor = Color.Gray;
                            break;
                    }
                }
            }
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        DisplayData(_currentindex, AspNetPager1.PageSize);
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        if (_currentindex == 0)
        {
            _currentindex = 1;
        }
        DisplayData(_currentindex,AspNetPager1.PageSize);
    }

    public string GetCompanyNameByID(string CID)
    {
        if (CID == "0")
        {
            return "普通消费者";
        }
        if (string.IsNullOrEmpty(CID))
        {
            return "---";
        }
        return blrcompany.GetList(int.Parse(CID)).CompName;
    }

    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}