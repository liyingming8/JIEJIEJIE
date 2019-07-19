using System; 
using System.Web.UI.WebControls;
using TJ.BLL;
using commonlib;
using TJ.Model;

public partial class Admin_TJ_Role_Package_Choose : AuthorPage
{
    BTJ_Role_Package bll = new BTJ_Role_Package(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
            {
                string pid = Sc.DecryptQueryString(Request.QueryString["pid"]);
                hd_pid.Value = pid;
                DisplayData();
            }
            else
            {
                Response.End();
            }
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_Role_PackageAddEdit.aspx?cmd=" + Sc.EncryptQueryString("edit") + "&ID={0}',600,580,'功能打包')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c"); 
            ((Label)e.Row.FindControl("LabelIndex")).Text = (e.Row.RowIndex + 1).ToString();
            tempid = ((HiddenField) e.Row.FindControl("hf_id")).Value;
            if (bpidrpid.CheckIsExistByFilterString("rpid=" + tempid + " and pid=" + hd_pid.Value))
            {
                ((CheckBox) e.Row.FindControl("CheckBoxSelect")).Checked = true;
            }
        }
    }
    private void DisplayData()
    { 
        GridView1.DataSource = bll.GetLists();
        GridView1.DataBind();
    }

    BTJ_SWM_PID_RPID bpidrpid = new BTJ_SWM_PID_RPID();
    private string tempid = "";
    protected void ButtonYes_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (((CheckBox) row.FindControl("CheckBoxSelect")).Checked)
            {
                tempid = ((HiddenField) row.FindControl("hf_id")).Value;
                if (!bpidrpid.CheckIsExistByFilterString("rpid=" + tempid + " and pid=" + hd_pid.Value))
                {
                    bpidrpid.Insert(new MTJ_SWM_PID_RPID(0, int.Parse(hd_pid.Value), int.Parse(tempid),DateTime.Now, DateTime.Now.AddDays(30)));
                }
            }
        } 
        ClientScript.RegisterStartupScript(this.GetType(),"reload","closemyWindow();",true); 
    }
}
