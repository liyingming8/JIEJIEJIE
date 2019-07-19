using System;
using System.Data;
using System.Web.Configuration;
using System.Web.UI;
using System.Data.SqlClient; 
using commonlib;

public partial class TJ_MSnsInfoAddEdit : AuthorPage
{ 
    private readonly DBClass db = new DBClass();

    private readonly SqlConnection sqlconn =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString()); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
            }
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim();
            }
            FillDdl();
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
        if (ComboBox_PC.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('请选择批次！');", true);
        }
        else
        {
            switch (HF_CMD.Value.Trim())
            {
                case "add":
                    db.InserMSnsInfo(ComboBox_PC.SelectedValue, inputTime.Text, TxtYW.Text, TxtFL.Text, TxtFZ.Text,
                        TxtCS.Text, "115", inputBZ.Value);
                    break;
                case "edit":
                    db.updateMSnsInfo(HF_ID.Value, ComboBox_PC.SelectedValue, inputTime.Text, TxtYW.Text, TxtFL.Text,
                        TxtFZ.Text, TxtCS.Text, "115", inputBZ.Value);
                    break;
            }
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info",
                "alert('操作成功！');window.location.href='TJ_MSnsInfo.aspx'", true);
        }
    }

    private void Fillinput(int id)
    {
        DataTable Dms = db.GetMsNsInfoBystr("ID=" + id + "");
        //inputVipID.Value = ms.VipID.ToString().Trim();
        ComboBox_PC.SelectedValue = Dms.Rows[0]["FHpici"].ToString();
        inputTime.Text = Dms.Rows[0]["DTime"].ToString(); 
        TxtYW.Text = Dms.Rows[0]["UseYW"].ToString();
        TxtFL.Text = Dms.Rows[0]["UserFL"].ToString();
        TxtFZ.Text = Dms.Rows[0]["StyleFZ"].ToString();
        TxtCS.Text = Dms.Rows[0]["StyleXG"].ToString();
        inputBZ.Value = Dms.Rows[0]["Remarke"].ToString();
    }

    private void FillDdl()
    {
        string sqlstring = "select distinct FHPiCi from TB_FaHuoInfo_" + GetCookieCompID() +
                           " where FHTypeID=3 and CompID=" + GetCookieCompID();
        SqlDataAdapter sda = new SqlDataAdapter(sqlstring, sqlconn);
        DataTable dttemp = new DataTable();
        sda.Fill(dttemp);
        ComboBox_PC.DataSource = dttemp;
        ComboBox_PC.DataBind();
    }
}