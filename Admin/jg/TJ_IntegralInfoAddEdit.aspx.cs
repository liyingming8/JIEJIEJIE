using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Collections.Generic;

public partial class Admin_jg_TJ_IntegralInfoAddEdit : AuthorPage
{
    private readonly BTJ_IntegralInfo bll = new BTJ_IntegralInfo();
    private MTJ_IntegralInfo mod = new MTJ_IntegralInfo();
    private readonly BTJ_RegisterCompanys bllcomp = new BTJ_RegisterCompanys();
    private BTJ_IntegraItems bllinteritem = new BTJ_IntegraItems();
    private readonly BTJ_BaseClass bassclass = new BTJ_BaseClass();
    private readonly BTJ_Integral bllinteg = new BTJ_Integral();
    private readonly BTJ_GoodsInfo bgoods = new BTJ_GoodsInfo();
    public CommonFun commfun = new CommonFun();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ITID"] != null && !Request.QueryString["ITID"].Trim().Equals(""))
            {
                HF_ITID.Value = Request.QueryString["ITID"].Trim();
            }
            else
            {
                Response.End();
            }
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
            }
            if (Request.QueryString["ITGID"] != null && !Request.QueryString["ITGID"].Trim().Equals(""))
            {
                HF_ITGID.Value = Request.QueryString["ITGID"].Trim();
            }
            inputIntegralName.Value = GetIntegralName(HF_ITID.Value.Trim());
            FillDLL();
            if (HF_CMD.Value.Trim().Equals("edit"))
            {
                fillinput(int.Parse(HF_ITGID.Value));
                Button1.Text = "修改";
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(int.Parse(HF_ITGID.Value));
        }
        //string goodsstring = "";
        //foreach (ListItem item in CHKList_Goods.Items)
        //{
        //    if (item.Selected)
        //    {
        //        goodsstring += "," + item.Value;
        //    }
        //}
        //mod.GoodID = goodsstring.Substring(1);
        mod.ITGRID = int.Parse(HF_ITID.Value.Trim());
        mod.IntegralItemID = Convert.ToInt32(ddl_IntegelItems.SelectedValue);
        mod.IntegralReword = Convert.ToInt32(inputIntegralReword.Value.Trim());
        //mod.ParentReword = Convert.ToDecimal(inputIntegralParentReword.Value.Trim());
        mod.Remarks = inputRemarks.Value.Trim();
        if (HF_CMD.Value.Equals("edit"))
        {
            bll.Modify(mod);
        }
        else
        {
            bll.Insert(mod);
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('操作成功!');", true);
        FillDLL();
        //MessageBox.ShowAndRedirect(this.Page, "操作成功!", "TJ_IntegralInfoAddEdit.aspx?ITID=" + HF_ITID.Value);
    }

    private void fillinput(int id)
    {
        MTJ_IntegralInfo ms = bll.GetList(id); 
        //string goosstring = ms.GoodID.Trim();
        //if (goosstring.Length.Equals(0))
        //{
        //    goosstring = ",0,";
        //}
        //else
        //{
        //    if (!goosstring.StartsWith(","))
        //    {
        //        goosstring = "," + goosstring;
        //    }
        //    else
        //    {
        //        if (!goosstring.EndsWith(","))
        //        {
        //            goosstring = goosstring + ",";
        //        }
        //    }
        //}
        //foreach (ListItem item in CHKList_Goods.Items)
        //{
        //    if (goosstring.Contains("," + item.Value + ","))
        //    {
        //        item.Selected = true;
        //    }
        //}

        ddl_IntegelItems.SelectedValue = ms.IntegralItemID.ToString().Trim();
        inputIntegralReword.Value = ms.IntegralReword.ToString().Trim();
        //inputIntegralParentReword.Value = ms.ParentReword.ToString();
        inputRemarks.Value = ms.Remarks.Trim();
    }

    private void FillDLL()
    {
        commfun.BindTreeCombox(ddl_IntegelItems, "CName", "CID", "ParentID", "TJ_BaseClass", 364, "首次查询", true, "-", "");
        ddl_IntegelItems.SelectedValue = "365";
        //CombIntegelItems.DataSource = bllinteritem.GetListsByFilterString("CompID=" + HttpUtility.UrlDecode(Request.Cookies["TJCOMPID"].Value));
        //CombIntegelItems.DataBind();
        GRV_IntegralItems.DataSource = bll.GetListsByFilterString("ITGRID=" + HF_ITID.Value.Trim());
        GRV_IntegralItems.DataBind();
    }

    public string GetCompanyName()
    {
        return bllcomp.GetList(int.Parse(HttpUtility.UrlDecode(Request.Cookies["TJCOMPID"].Value))).CompName;
    }

    public string GetIntegralName(string INTGID)
    {
        if (INTGID == "")
        {
            return "";
        }
        else
        {
            return bllinteg.GetList(int.Parse(INTGID)).IntegralName;
        }
    }

    public string GetIntegralItemName(string INTERITID)
    {
        if (INTERITID == "")
        {
            return "";
        }
        else
        {
            return bassclass.GetList(int.Parse(INTERITID)).CName;
        }
    }

    public string GetGoodNameByIDString(string GoodsIDSring)
    {
        if (GoodsIDSring.Equals("0"))
        {
            return "所有产品";
        }
        else
        {
            IList<MTJ_GoodsInfo> list =
                bgoods.GetListsByFilterString("CompID=" + GetCookieCompID() + " and GoodsID in (" + GoodsIDSring + ")");
            string tempstring = "";
            foreach (MTJ_GoodsInfo item in list)
            {
                tempstring += "," + item.GoodsName;
            }
            return tempstring.Substring(1);
        }
    }

    protected void GRV_IntegralItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        GridViewRow grv = GRV_IntegralItems.Rows[index];
        int ID_Itgid = int.Parse(GRV_IntegralItems.Rows[index].Cells[0].Text);
        // int ID_Itgrid = int.Parse(GRV_IntegralItems.DataKeys[e.RowIndex].Value.ToString());
        bll.Delete(ID_Itgid);
        FillDLL();
    }

    protected void GRV_IntegralItems_DataBound(object sender, GridViewRowEventArgs e)
    {
        int index = int.Parse(GRV_IntegralItems.DataKeys[e.Row.RowIndex].Value.ToString());
    }

    protected void GRV_IntegralItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
    }

    protected void GRV_IntegralItems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GRV_IntegralItems.EditIndex = -1;
        FillDLL();
    }

    protected void GRV_IntegralItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton) e.Row.Cells[7].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确定要删除当前记录吗?')");
            }
        }
    }

    private bool CheckIsUsed(string ITGRID)
    {
        if (bll.CheckIsExistByFilterString("CompID=" + ITGRID))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void GRV_IntegralItems_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
    }
}