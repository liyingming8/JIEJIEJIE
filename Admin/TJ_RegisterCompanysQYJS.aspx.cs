using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.IO;

public partial class Admin_TJ_RegisterCompanysQYJS : AuthorPage
{
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();
    private MTJ_RegisterCompanys _mod = new MTJ_RegisterCompanys(); 
    public string Temp;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (GetCookieCompTypeID().EndsWith("484"))
                {
                    LabelQyjs.Text = "自我介绍";
                }
                HF_ID.Value = GetCookieCompID();
                HF_CMD.Value = "edit";
                Button1.Text = "确定";
                Fillinput(int.Parse(HF_ID.Value.Trim()));
            }
            catch (Exception a)
            {
                string s = a.ToString();

                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert(" + s + ");", true);
            } 
        }
    }
    public string Functionstring()
    {
        return Temp;
    } 
     
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.ToLower().Trim().Equals("edit"))
        {
            _mod = bll.GetList(int.Parse(HF_ID.Value));
        }

        _mod.ParentID = Convert.ToInt32(hf_parentid.Value.Length.Equals(0) ? "0" : hf_parentid.Value);  
        _mod.DetailDiscription = inputAddress.Value.Trim(); 
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                _mod.RegisterDate = DateTime.Now;
                _mod.AuthoredDate = Convert.ToDateTime("1900-01-01");
                _mod.DisAuthorDate = Convert.ToDateTime("1900-01-01");
                bll.Insert(_mod);
                break;
            case "edit":
                bll.Modify(_mod);
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
    }

    private void Fillinput(int id)
    {
        MTJ_RegisterCompanys ms = bll.GetList(id);  
        inputAddress.Value = ms.DetailDiscription.Trim(); 
        HF_LectureImage.Value = ms.BusinessLicencePicture.Trim(); 
    }

    

    public bool CheckFileType(string fileName)
    {
        string ext = Path.GetExtension(fileName);
        switch (ext.ToLower())
        {
            case ".gif":
                return true;
            case ".png":
                return true;
            case ".jpeg":
                return true;
            case "jpg":
                return true;
            default:
                return false;
        }
    }
}