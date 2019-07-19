using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.BLL;
using commonlib;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

public partial class Admin_FaHuoViewNewxf_ok : AuthorPage
{
    public readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();

    SqlConnection sqlconcom = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());
    public string labelcode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["cpid"]))
            {
                hf_acid.Value = Sc.DecryptQueryString(Request.QueryString["cpid"].Trim());
                labelcode = hf_acid.Value;
                //hf_acname.Value = bll.GetList(int.Parse(hf_acid.Value)).CompName; 
                DisplayData(hf_acid.Value);
            }
            else
            {
               
                  Response.End();

            }
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
            Label labelfhkey = (Label)e.Row.FindControl("labelgegnxin");
            if (labelfhkey.Text == "已确认")
            {
                e.Row.Cells[3].ForeColor = Color.Red;
                // ((LinkButton)e.Row.Cells[0].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除当前记录吗?')");
            }
            else
            {
                // e.Row.Cells[3].Enabled=false;
                e.Row.Cells[3].ForeColor = Color.Gray;
            }
        }

    }

    private string _tempvalue = string.Empty;
    readonly BTJ_AwardInfo _btjAwardInfo = new BTJ_AwardInfo();


    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        // DisplayData();
    }


    private void DisplayData(string cpid)
    {
        if (cpid.Length > 0)
        {


            string sqlstring = " SELECT [agentid],[label],[fhkey],tm FROM [TianJianWuLiuWebnew].[dbo].[receipt_details] where label='" + cpid + "'";

            var sda = new SqlDataAdapter(sqlstring, sqlconcom);
            var dttemp = new DataTable();
            sda.Fill(dttemp);
            if (dttemp.Rows.Count > 0)
            {
                GridView1.DataSource = dttemp;
                GridView1.DataBind();
                dttemp.Dispose();
                sda.Dispose();
            }
            else
            {

                GridView1.DataSource = dttemp;
                GridView1.DataBind();
                dttemp.Dispose();
                sda.Dispose();
                // DataTable dt = new DataTable();

                // DataRow dr = dt.NewRow();

                // dt.Columns.Add("agentid");

                // dt.Columns.Add("label");

                // dt.Columns.Add("fhkey");

                // dt.Columns.Add("tm");
                //// dt.Rows.Add(dt.NewRow());

                // dr["agentid"] = "12";
                // dr["label"] = cpid;
                // dr["fhkey"] = "";
                // dr["tm"] = "";

                // dt.Rows.Add(dr);
                // int s = dt.Rows.Count;
                // this.GridView1.DataSource = dt;

                // this.GridView1.DataBind();


            }

        }
    }

}
