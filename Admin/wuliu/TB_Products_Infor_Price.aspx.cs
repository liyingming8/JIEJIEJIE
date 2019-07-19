using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using TJ.DBUtility;

public partial class Admin_TB_Products_Infor_Price : AuthorPage
{
    private readonly BTB_Products_Infor bll = new BTB_Products_Infor();
    private MTB_Products_Infor mod = new MTB_Products_Infor();
    public BTB_Products_Type ptype = new BTB_Products_Type();
    public BTB_ProducStandards pstandards = new BTB_ProducStandards();
    public BTB_ProductJingHanLiang pjinghanliang = new BTB_ProductJingHanLiang();
    public BTB_ProductXiangXing pxiangxing = new BTB_ProductXiangXing();
    public BTB_ProductJiuJingDu pjiujingdu = new BTB_ProductJiuJingDu();
    private readonly TabExecutewuliu tabexwl = new TabExecutewuliu();
    private readonly BTB_ProductAuthorForAgent bproauthoragent = new BTB_ProductAuthorForAgent();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgridview();
        }
    }

    private void fillgridview()
    {
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            GridView1.DataSource =
                bll.GetListsByFilterString("CompID=" + GetCookieCompID() + " and " + DDLField.SelectedValue + " like '%" +
                                           inputSearchKeyword.Value.Trim() + "%'");
        }
        else
        {
            GridView1.DataSource = bll.GetListsByFilterString("CompID=" + GetCookieCompID());
        }
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["Infor_ID"].ToString()));
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
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["Infor_ID"].ToString()));
        mod.Products_Name = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtProducts_Name")).Text.Trim();
        mod.Products_Summary = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtProducts_Summary")).Text.Trim();
        mod.Remarks = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtRemarks")).Text.Trim();
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!bproauthoragent.CheckIsExistByFilterString("ProdID=" + GridView1.DataKeys[e.Row.RowIndex][0]))
            {
                if (
                    Convert.ToInt32(
                        tabexwl.ExecuteQuery(
                            "select count(*) from TB_FaHuoInfo_" + GetCookieCompID() + " where ProID=" +
                            GridView1.DataKeys[e.Row.RowIndex][0], null).Rows[0][0].ToString()) == 0)
                {
                    ((LinkButton) e.Row.Cells[12].Controls[0]).Attributes.Add("onclick",
                        "javascript:return confirm('你确定要删除当前记录吗?')");
                }
                else
                {
                    e.Row.Cells[12].Enabled = false;
                    e.Row.Cells[12].ForeColor = Color.LightGray;
                }
            }
            else
            {
                e.Row.Cells[12].Enabled = false;
                e.Row.Cells[12].ForeColor = Color.LightGray;
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