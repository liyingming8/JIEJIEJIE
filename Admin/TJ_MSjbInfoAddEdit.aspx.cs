using System;
using System.Data;
using System.Web.Configuration;
using System.Web.UI;
using System.Data.SqlClient;
using commonlib;

public partial class TJ_MSjbInfoAddEdit : AuthorPage
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
            FillDDL();
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    break;
                case "edit":
                    Button1.Text = "修改";
                    fillinput(int.Parse(HF_ID.Value.Trim()));
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
            DataTable mod = new DataTable();
            if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
            {
                mod = db.GetListsByFilterStringwl("id=" + Convert.ToInt32(HF_ID.Value));
            }


            string FHPC = ComboBox_PC.SelectedValue;
            string SHNum = inputSHNum.Value.Trim();
            string HZSName = inputHZSName.Value.Trim();
            string NHName = inputNHName.Value.Trim();
            string SSQuYu = inputSSQuYu.Value.Trim();
            string ChanPinPiCi = inputChanPinPiCi.Value.Trim();
            string ChanPinDengJi = inputChanPinDengJi.Value.Trim();
            string ZhiJianYuan = inputZhiJianYuan.Value.Trim();
            string LianXiRen = inputLianXiRen.Value.Trim();
            string Phone = inputPhone.Value.Trim();
            string ChanPinShuoMing = TxtChanPin.Text.Trim();
            switch (HF_CMD.Value.Trim())
            {
                case "add":
                    db.insert(FHPC, SHNum, HZSName, NHName, SSQuYu, ChanPinPiCi, ChanPinDengJi, ZhiJianYuan, LianXiRen,
                        Phone, ChanPinShuoMing);
                    break;
                case "edit":
                    db.Modify(FHPC, SHNum, HZSName, NHName, SSQuYu, ChanPinPiCi, ChanPinDengJi, ZhiJianYuan, LianXiRen,
                        Phone, ChanPinShuoMing, Convert.ToInt32(HF_ID.Value));
                    break;
            }
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
            // ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info",
            //     "alert('操作成功！');window.location.href='TJ_MSJBinfo.aspx'", true);
        }
    }

    private void fillinput(int id)
    {
        DataTable ms = db.GetListsByFilterStringwl("id=" + id);
        ComboBox_PC.SelectedValue = ms.Rows[0]["FHPC"].ToString();
        inputSHNum.Value = ms.Rows[0]["SHNum"].ToString();
        inputHZSName.Value = ms.Rows[0]["HZSName"].ToString();
        inputNHName.Value = ms.Rows[0]["NHName"].ToString();
        inputSSQuYu.Value = ms.Rows[0]["SSQuYu"].ToString();
        inputChanPinPiCi.Value = ms.Rows[0]["ChanPinPiCi"].ToString();
        inputChanPinDengJi.Value = ms.Rows[0]["ChanPinDengJi"].ToString();
        inputZhiJianYuan.Value = ms.Rows[0]["ZhiJianYuan"].ToString();
        inputLianXiRen.Value = ms.Rows[0]["LianXiRen"].ToString();
        inputPhone.Value = ms.Rows[0]["Phone"].ToString();
        TxtChanPin.Text = ms.Rows[0]["ChanPinShuoMing"].ToString();
    }

    private void FillDDL()
    {
        string sqlstring = "select distinct FHPiCi from TB_FaHuoInfo_" + GetCookieCompID() +
                           " where FHTypeID=3 and CompID=" + GetCookieCompID();
        SqlDataAdapter sda = new SqlDataAdapter(sqlstring, sqlconn);
        DataTable dttemp = new DataTable();
        sda.Fill(dttemp);
        if (dttemp.Rows.Count > 0)
        {
            ComboBox_PC.DataSource = dttemp;
            ComboBox_PC.DataBind();
        }
    }
}