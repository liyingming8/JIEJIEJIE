using System; 
using System.Data; 
using commonlib;
using TJ.DBUtility;

public partial class Admin_wuliu_Fahuo_crm_order_detail : AuthorPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["khddh"]))
            {
                string khddh = Request.QueryString["khddh"];
                khddh = Sc.DecryptQueryString(khddh);
                TabExecute tab = new TabExecute();
                DataTable dt = tab.ExecuteQuery("select [id],[erpordercode],[orderdate],[materialcode],[prodname],[agentname] FROM TJ_XF_ERP_OrderInfo where erpordercode='" +khddh + "'", null);
                if (dt.Rows.Count > 0)
                {
                    lab_erpordercode.Text = dt.Rows[0]["erpordercode"].ToString();
                    lab_orderdate.Text = Convert.ToDateTime(dt.Rows[0]["orderdate"]).ToString("yyyy-MM-dd");
                    lab_agentname.Text = dt.Rows[0]["agentname"].ToString();
                    //lab_prodname.Text = dt.Rows[0]["prodname"].ToString();
                    string odid = dt.Rows[0]["id"].ToString();
                    dt = new DataTable();
                    dt = tab.ExecuteQuery("select * from TJ_XF_ERP_OrderDetail where odid=" + odid, null);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                dt.Dispose();
            } 
        }
    }
}