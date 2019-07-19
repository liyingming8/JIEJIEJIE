using System;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_TJ_QuerLabelCodeJG : AuthorPage
{
    private readonly BTJ_QuerLabelCodeJG bll = new BTJ_QuerLabelCodeJG();
    private MTJ_QuerLabelCodeJG mod = new MTJ_QuerLabelCodeJG();
    public BTB_Products_Infor bpro = new BTB_Products_Infor();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgridview();
        }
    }


    public string JiageLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            Session["starTime"] = TextBox_RukuDateBegin.Text;
            Session["endTime"] = TextBox_RukuDateEnd.Text;

            return string.Format("javascript:window.location.href='TJ_QuerLabelCodeMingXi.aspx?ID={0}'", ID);


            //return string.Format("javascript:var win=openWinCenter('FaHuoDetial.aspx?Agent_id={0}',900,600)", ID);
            //return string.Format("javascript: window.open('FaHuoDetial.aspx?Agent_id={0}','_blank','width=900,height=900')", ID);
            //return string.Format("javascript:window.location.href='FaHuoDetial.aspx?Agent_id={0}'", ID);
        }
        else
        {
            return "";
        }
    }

    private void fillgridview()
    {
        string str = string.Empty;
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            str = "labeldoe like '%" + inputSearchKeyword.Value.Trim() + "%'";
        }
        if (!IsNumeric(inputSearchKeywordcishu.Value.Trim()) && inputSearchKeywordcishu.Value.Trim().Length > 0)
        {
            Response.Write("<script>alert('请输入数字！');</script>");
            return;
        }
        if (!string.IsNullOrEmpty(str))
        {
            str += "and num>=" + inputSearchKeywordcishu.Value.Trim();
        }
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            GridView1.DataSource = bll.GetListsByFilterString(str);
        }
        else
        {
            GridView1.DataSource = bll.GetLists();
        }
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["ID"].ToString()));
        fillgridview();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgridview();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    /// <summary>
    /// 判断是不是数字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private static bool IsNumeric(string str)
    {
        Regex reg1
            = new Regex(@"^[-]?\d+[.]?\d*$");
        return reg1.IsMatch(str);
    }
}