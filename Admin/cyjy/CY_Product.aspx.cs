using System;
using System.Web.UI.WebControls;
using System.Data;
using commonlib;
public partial class CY_Product : AuthorPage
{
    DB375 db = new DB375();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgridview();
        }
    }

    public string Pname(string pro)
    {
        DataTable dpro = db.GetProInfo("ProID=" + pro);
        return dpro.Rows[0]["ProName"].ToString();
    }

    private void fillgridview()
    {
      
         if (inputSearchKeyword.Value.Trim().Length > 0)
         {
             GridView1.DataSource = db.GetProInfo("ProID !='' and " + DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'");
         }
         else
         {
             GridView1.DataSource = db.GetProInfo("ProID !=''");
         }
      
        GridView1.DataBind();
      
    }
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('CY_ProductAdd.aspx?cmd=edit&ID={0}',900,500,'经销商信息编辑')", ID);
        }
        else
        {
            return "";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        db.DelProduct("ProID=" + GridView1.DataKeys[e.RowIndex]["ID"]);
      
        fillgridview();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgridview();

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {


                ((LinkButton)e.Row.Cells[5].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
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
