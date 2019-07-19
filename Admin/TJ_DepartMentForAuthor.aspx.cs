using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.BLL; 
using commonlib;
using TJ.Model;

public partial class Admin_TJ_DepartMentForAuthor : AuthorPage
{
    BTJ_DepartMent bll = new BTJ_DepartMent();
    readonly BTJ_DepartMent_CompAuthor _btjDepartMentCompAuthor = new BTJ_DepartMent_CompAuthor();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["agentid"]))
            {
                hd_agentid.Value = Request.QueryString["agentid"].Trim();
                hd_authordepartid.Value = GetAuthorDepartIdByAgentid(hd_agentid.Value);
                author_agent.Text = string.IsNullOrEmpty(Request.QueryString["agentnm"])? "": Request.QueryString["agentnm"];
                DisplayData();
                AutoSelect();
            }
            else
            {
                Response.End();
            }
        }
    }

    private string GetAuthorDepartIdByAgentid(string agentid)
    {
        IList<MTJ_DepartMent_CompAuthor> list = _btjDepartMentCompAuthor.GetListsByFilterString("authorcompid=" + agentid);
        var array = new string[list.Count];
        int i = 0;
        foreach (MTJ_DepartMent_CompAuthor compAuthor in list)
        {
            array[i] = compAuthor.departid.ToString();
            i++;
        }
        return String.Join(",",array);
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_DepartMentAddEdit.aspx?cmd="+Sc.EncryptQueryString("edit")+"&ID={0}',680,520,'组织结构')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    private CheckBox _ckbSelect;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            //var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            //if (dataKey != null)
            //{
            //    e.Row.Attributes.Add("onclick", "select("+e.Row.RowIndex+");"); 
            //}
            ((Label)e.Row.FindControl("LabelIndex")).Text = (e.Row.RowIndex + 1).ToString(); 
        }
    }

    private void AutoSelect()
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                _ckbSelect = (CheckBox)row.FindControl("ckb_select");
                if (("," + hd_authordepartid.Value + ",").Contains("," + ((HiddenField)row.FindControl("hf_id")).Value + ","))
                {
                    _ckbSelect.Checked = true;
                }
                else
                {
                    _ckbSelect.Checked = false;
                }
            }
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    { 
       DisplayData();
    }

    private string _filtertemp ="1=1";

    private void DisplayData()
    {
        _filtertemp = "compid="+GetCookieCompID();
        //_filtertemp = "compid=2";
        GridView1.DataSource = bll.GetListsByFilterString(_filtertemp, "parentid,department");
        GridView1.DataBind();
    }
    
    protected void btn_confirm_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                var hfID = (HiddenField)row.FindControl("hf_id");
                Boolean isexist = _btjDepartMentCompAuthor.CheckIsExistByFilterString("authorcompid=" + hd_agentid.Value + " and departid=" + hfID.Value);
                if (((CheckBox)row.FindControl("ckb_select")).Checked)
                {
                    if (!isexist)
                    {
                        _btjDepartMentCompAuthor.Insert(new MTJ_DepartMent_CompAuthor(0, int.Parse(hfID.Value),
                            int.Parse(hd_agentid.Value), DateTime.Now, int.Parse(GetCookieUID()), author_agent.Text,GetCookieTJUName()));
                    }
                }
                else
                {
                    if (isexist)
                    {
                        _btjDepartMentCompAuthor.Delete("departid=" + hfID.Value + " and authorcompid=" + hd_agentid.Value);
                    }
                }
            }
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true); 
    }
}
