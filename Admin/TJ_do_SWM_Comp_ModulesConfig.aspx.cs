using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using TJ.BLL;
using commonlib;
using TJ.Model;

public partial class Admin_TJ_do_SWM_Comp_ModulesConfig : AuthorPage
{
    BTJ_SWM_Comp_ModulesConfig bll = new BTJ_SWM_Comp_ModulesConfig();  
    TabExecute tab = new TabExecute(); 
    private int _currentindex = 1; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hdLPH.Value = GetLogoPath(GetCookieCompID());
             _currentindex = 1;
             AspNetPager1.CurrentPageIndex = 1;
             DisplayData(1, AspNetPager1.PageSize);
        }
    }

    private string GetLogoPath(string compid)
    {
        string tempreturn = ""; ;
        var tab = new TabExecute();
        DataTable dttemp = tab.ExecuteQuery("select logopath from TJ_CompFrontPage_Config where compid=" +compid, null);
        if (dttemp.Rows.Count > 0)
        {
            tempreturn = dttemp.Rows[0]["logopath"].ToString();
        }
        else
        {
            tempreturn = "blue";
        }
        dttemp.Dispose();
        return tempreturn;
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_SWM_Comp_ModulesConfigAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}',500,460,'模块信息')", Sc.EncryptQueryString(ID));
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
                ((HtmlImage) e.Row.FindControl("img_logo")).Src = (dataKey["logourl"].ToString().Contains("commup")?dataKey["logourl"].ToString():"Images/swmlogo/" + hdLPH.Value + "/" + dataKey["logourl"]);
                ((HtmlImage)e.Row.FindControl("img_edit")).Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString()));  
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
        _filtertemp = "compid=" + GetCookieCompID()+" and pid=0"; 
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from TJ_SWM_Comp_ModulesConfig where "+_filtertemp, null).Rows[0][0].ToString());
        if (AspNetPager1.RecordCount > 0)
        {
            GridView1.DataSource = tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_SWM_Comp_ModulesConfig", _filtertemp, "isshow desc,showorder asc", "id", pageSize);
            GridView1.DataBind();
        }
        else
        {
            BTJ_Role_Package btjRolePackage = new BTJ_Role_Package();
            IList<MTJ_Role_Package> rolelist = btjRolePackage.GetLists("showorder");
            if (rolelist.Count > 0)
            {
                string ridstring = "";
                foreach (MTJ_Role_Package package in rolelist)
                {
                    if (package.ridstring.Length > 0)
                    {
                        ridstring += package.ridstring.StartsWith(",") ? package.ridstring : "," + package.ridstring;
                    } 
                } 
                ridstring = ridstring.StartsWith(",") ? ridstring.Substring(1) : ridstring; 
               DataTable dt = tab.ExecuteQuery("select SiteID id,PageName nm,LinkPath lk,ShowOrder od,LogoName lg,Remarks rm from TJ_SiteMap where SysTypeID=78 and (LogoName is not null and LogoName<>'') and (Remarks is not null and Remarks<>'') and SiteID in(" + ridstring + ")",null);
                if (dt.Rows.Count > 0)
                {
                    BTJ_SWM_Comp_ModulesConfig bll = new BTJ_SWM_Comp_ModulesConfig();
                    foreach (DataRow row in dt.Rows)
                    {
                        bll.Insert(new MTJ_SWM_Comp_ModulesConfig(0, int.Parse(GetCookieCompID()),
                            int.Parse(row["id"].ToString()), row["nm"].ToString(), row["lg"].ToString(), true, row["rm"].ToString(),
                            row["lk"].ToString(), DateTime.Now, int.Parse(row["od"].ToString()), 0));
                    }
                }
                dt.Dispose();
            }  
            DisplayData(1,AspNetPager1.PageSize);
        }
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }

    protected void ckb_isshow_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender; 
        int index = ((GridViewRow)(chk.NamingContainer)).RowIndex;    //通过NamingContainer可以获取当前checkbox所在容器对象，即gridviewrow   
        DataKey dk = GridView1.DataKeys[index];
        MTJ_SWM_Comp_ModulesConfig mod = bll.GetList(int.Parse(dk["id"].ToString()));
        mod.isshow = chk.Checked;
        bll.Modify(mod);
        DisplayData(1,AspNetPager1.PageSize);
    }
}
