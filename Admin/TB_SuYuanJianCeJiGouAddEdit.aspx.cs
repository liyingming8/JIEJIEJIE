using System;
using System.Web.Configuration;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Data;
using System.Data.SqlClient;
public partial class Admin_TB_SuYuanJianCeJiGouAddEdit : AuthorPage
{
    readonly BTB_SuYuanJianCeJiGou bll = new BTB_SuYuanJianCeJiGou();
    MTB_SuYuanJianCeJiGou mod = new MTB_SuYuanJianCeJiGou();
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
       {
          if (!string.IsNullOrEmpty(Request.QueryString["cmd"]))
          {
             HF_CMD.Value = Sc.DecryptQueryString(Request.QueryString["cmd"].Trim());
          }
          if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
          {
             HF_ID.Value = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
          }
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
    private void opensqlconn(SqlConnection sqlconn)
    {
        if (sqlconn.State == ConnectionState.Closed)
        {
            sqlconn.Open();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
           mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        } 
        mod.JGName = inputJGName.Value.Trim();
        mod.LianXiRen = inputLianXiRen.Value.Trim();
        mod.TelPhone = inputTelPhone.Value.Trim();
        mod.CompID = Convert.ToInt32(GetCookieCompID());
        mod.Remarks = inputRemarks.Value.Trim();
        //===================================================
        SqlConnection connwyz = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString()); ;
        string str1 = "select JGName from [TB_SuYuanJianCeJiGou] where jgname='" + mod.JGName + "'";
        SqlCommand sqlcmd=new SqlCommand ();
        sqlcmd.CommandText = str1;
        sqlcmd.Connection = connwyz;
        opensqlconn(connwyz);
        SqlDataReader sdr = sqlcmd.ExecuteReader();
        if (sdr.Read())
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('检测机构名称必须唯一，请检查！');", true);
            sdr.Close();
            return;
        }
        sdr.Close();
        //===================================================
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                // RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanJianCeJiGouAddEdit.aspx","TB_SuYuanJianCeJiGou","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                // RecordDealLog(new MTJ_DealLog(0,"TB_SuYuanJianCeJiGouAddEdit.aspx","TB_SuYuanJianCeJiGou","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void Fillinput(int id)
    {
        MTB_SuYuanJianCeJiGou ms = bll.GetList(id); 
        inputJGName.Value = ms.JGName.Trim();
        inputLianXiRen.Value = ms.LianXiRen.Trim();
        inputTelPhone.Value = ms.TelPhone.Trim(); 
        inputRemarks.Value = ms.Remarks.Trim();
    }
}