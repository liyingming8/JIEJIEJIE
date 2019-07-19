using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_Role_PackageAddEdit : AuthorPage
{
    BTJ_Role_Package bll = new BTJ_Role_Package();
    MTJ_Role_Package mod = new MTJ_Role_Package();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["cmd"]))
            {
                HF_CMD.Value = Sc.DecryptQueryString(Request.QueryString["cmd"].Trim());
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                HF_ID.Value = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
            }
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    break;
                case "edit":
                    Button1.Text = "修改";
                    Fillinput(int.Parse(HF_ID.Value.Trim()));
                    break;
                default:
                    break;
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.rpackage = inputrpackage.Value.Trim();
        mod.remarks = inputremarks.Value.Trim();
        mod.showorder = Convert.ToInt32(inputshoworder.Value.Trim());
        mod.pricevalue = Convert.ToDecimal(inputpricevalue.Value.Trim());
        //mod.pricemodel = Convert.ToInt32(inputpricemodel.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_Role_PackageAddEdit.aspx", "TJ_Role_Package", "描述", System.DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                bll.Modify(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_Role_PackageAddEdit.aspx", "TJ_Role_Package", "描述", System.DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "reload", "closemyWindow();", true);
    }

    private void Fillinput(int id)
    {
        MTJ_Role_Package ms = bll.GetList(id);
        inputrpackage.Value = ms.rpackage.Trim();
        Literalridstring.Text = ShowMenuInfo(ms.ridstring.Trim());
        inputremarks.Value = ms.remarks.Trim();
        inputpricevalue.Value = ms.pricevalue.ToString().Trim();
        inputshoworder.Value = ms.showorder.ToString();
        //inputpricemodel.Value = ms.pricemodel.ToString().Trim();
    }

    readonly BTJ_SiteMap _btjsitemap = new BTJ_SiteMap(); 
    private string ShowMenuInfo(string ridString)
    {
        if (!ridString.Contains(","))
        {
            return "";
        }
        if (ridString.StartsWith(","))
        {
            ridString = ridString.Substring(1);
        }
        if (ridString.EndsWith(","))
        {
            ridString = ridString.Substring(0, ridString.Length - 1);
        }
        IList<MTJ_SiteMap> sitelist = _btjsitemap.GetListsByFilterString("ParentID=0 and SiteID in (" + ridString + ")");
        if (sitelist.Count > 0)
        {
            var sb = new StringBuilder();
            foreach (var mtjRoleInfo in sitelist)
            {
                sb.AppendLine("【"+mtjRoleInfo.PageName+"】:");
                IList<MTJ_SiteMap> subrolelist = _btjsitemap.GetListsByFilterString("ParentID=" + mtjRoleInfo.SiteID + " and SiteID in (" + ridString + ")","ShowOrder");
                if (subrolelist.Count > 0)
                { 
                    var temp = new string[subrolelist.Count];
                    int i = 0;
                    foreach (var roleInfo in subrolelist)
                    {
                        temp[i] = roleInfo.PageName;
                        i++;
                    }
                    sb.Append(String.Join(",", temp));
                    sb.Append("<br>");  
                }
            }
            return sb.ToString();
        }
        else
        {
            return "";
        } 
    }
}