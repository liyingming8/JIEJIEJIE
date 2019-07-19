using System;
using System.IO;
using System.Web.UI; 
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_WXBaseInfoAddEdit : AuthorPage
{
    BTJ_WXBaseInfo bll = new BTJ_WXBaseInfo();
    MTJ_WXBaseInfo mod = new MTJ_WXBaseInfo();
    commfrank comm = new commfrank();
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
           mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        } 
        mod.WX_Appid = inputWX_Appid.Value.Trim();
        mod.WX_Appsecret = inputWX_Appsecret.Value.Trim();
        mod.WX_Payid = inputWX_Payid.Value.Trim();
        mod.WX_Paykey = inputWX_Paykey.Value.Trim();
        mod.WX_Paycert = hf_certpath.Value.Trim();
        mod.CompName = comm.GetValueByID("nm", "TJ_RegisterCompanys", "CompID", "CompName","",GetCookieCompID());
        mod.CompID = Convert.ToInt32(GetCookieCompID());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_WXBaseInfoAddEdit.aspx","TJ_WXBaseInfo","描述",DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_WXBaseInfoAddEdit.aspx","TJ_WXBaseInfo","描述",DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void Fillinput(int id)
    {
        MTJ_WXBaseInfo ms = bll.GetList(id); 
        inputWX_Appid.Value = ms.WX_Appid.Trim();
        inputWX_Appsecret.Value = ms.WX_Appsecret.Trim();
        inputWX_Payid.Value = ms.WX_Payid.Trim();
        inputWX_Paykey.Value = ms.WX_Paykey.Trim();
        hf_certpath.Value = ms.WX_Paycert.Trim(); 
    }

    private string _uploadfilepath = string.Empty; 
    protected void Button_upload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            try
            {
                FileInfo file = new FileInfo(FileUpload1.PostedFile.FileName);
                if (file.Extension.Equals(".zip"))
                {
                    // 保存文件
                    _uploadfilepath = Server.MapPath(@"Uploadfiles/") + DateTime.Now.ToString("yyyyMMddhhmmssss") + ".zip";
                    FileUpload1.SaveAs(_uploadfilepath);
                    ViewState["uploadfilepath"] = _uploadfilepath;
                    hf_certpath.Value = _uploadfilepath;
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('上传成功!');", true); 
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('请上传格式为(.zip)的txt文本压缩文件!');",
                        true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('" + ex.Message + "');",
                    true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('请选取数据文件!');", true);
        }
    }
}