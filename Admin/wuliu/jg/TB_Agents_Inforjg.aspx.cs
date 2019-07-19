using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using System.Collections.Generic;

public partial class Admin_TB_Agents_Inforjg : AuthorPage
{
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();
    private MTJ_RegisterCompanys mod = new MTJ_RegisterCompanys();
    public CommonFun commfun = new CommonFun();
    private BTJ_User buser = new BTJ_User();
    private CommonFunWL comwl = new CommonFunWL();
    private readonly BTB_CompAgentInfo bcompagent = new BTB_CompAgentInfo();
    private readonly DBClass db = new DBClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgridview();
        }
    }

    private string AgentIDStringForQuery = "";

    private void fillgridview()
    {
        AgentIDStringForQuery = GetAgentIDStringByCompID(GetCookieCompID());
        if (AgentIDStringForQuery.Length > 0)
        {
            if (inputSearchKeyword.Value.Trim().Length > 0)
            {
                GridView1.DataSource =
                    bll.GetListsByFilterString("CompID in (" + AgentIDStringForQuery + ") and CompTypeID=" +
                                               DAConfig.CompTypeIDJingXiaoShang + " and " + DDLField.SelectedValue +
                                               " like '%" + inputSearchKeyword.Value.Trim() + "%'");
            }
            else
            {
                GridView1.DataSource =
                    bll.GetListsByFilterString("CompID in (" + AgentIDStringForQuery + ") and CompTypeID=" +
                                               DAConfig.CompTypeIDJingXiaoShang);
            }
            GridView1.DataBind();
        }
    }

    private IList<MTB_CompAgentInfo> mcompagentlist;
    private string AgentIDString = "";

    public string GetAgentIDStringByCompID(string CompID)
    {
        if (GetCookieCompID() == "130" && GetCookieRID() == "28")
        {
            mcompagentlist =
                bcompagent.GetListsByFilterString("CompID=" + CompID + "and Remarks='" + GetCookieUID() + "'");
        }
        else
        {
            mcompagentlist = bcompagent.GetListsByFilterString("CompID=" + CompID);
        }
        AgentIDString = "";
        if (mcompagentlist.Count > 0)
        {
            foreach (MTB_CompAgentInfo mca in mcompagentlist)
            {
                AgentIDString += "," + mca.AgentID;
            }
            return AgentIDString.Substring(1);
        }
        else
        {
            return "0";
        }
    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["CompID"].ToString()));

        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["CompID"].ToString()));
        db.DeleteAgent(GridView1.DataKeys[e.RowIndex]["CompID"].ToString());
        db.DeleteFhTable(GridView1.DataKeys[e.RowIndex]["CompID"].ToString());

        fillgridview();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgridview();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgridview();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["CompID"].ToString()));
        mod.CompName = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtAgent_Name")).Text.Trim();
        mod.LegalPerson = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtMiddleman")).Text.Trim();
        mod.TelNumber = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtTelephone")).Text.Trim();
        mod.MobilePhoneNumber = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtMobiePhone")).Text.Trim();
        mod.AllowAreaInfo = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtAllowAreaInfo")).Text.Trim();
        mod.Remarks = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtRemarks")).Text.Trim();
        mod.Address = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtAddressString")).Text.Trim();
        bll.Modify(mod);
        GridView1.EditIndex = -1;
        fillgridview();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillgridview();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
        //    {
        //        ((LinkButton)e.Row.Cells[11].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
        //    }
        //}

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                if (!bcompagent.CheckIsExistByFilterString("CompID=" + GridView1.DataKeys[e.Row.RowIndex][0]))
                {
                    if (GetCookieCompID() == "130")
                    {
                        if (GetCookieRID() == "28" || GetCookieRID() == "15")
                        {
                            ((LinkButton) e.Row.Cells[10].Controls[0]).Attributes.Add("onclick",
                                "javascript:return confirm('你确定要删除当前记录吗?')");
                        }
                        else
                        {
                            e.Row.Cells[10].Enabled = false;
                            e.Row.Cells[10].ForeColor = Color.LightGray;
                        }
                    }
                    else
                    {
                        ((LinkButton) e.Row.Cells[10].Controls[0]).Attributes.Add("onclick",
                            "javascript:return confirm('你确定要删除当前记录吗?')");
                    }
                }
                else
                {
                    e.Row.Cells[10].Enabled = false;
                    e.Row.Cells[10].ForeColor = Color.LightGray;
                }
            }
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview();
    }
}