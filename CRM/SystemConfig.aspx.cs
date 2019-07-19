using System;
using System.Data;
using commonlib; 
using TJ.DBUtility;

public partial class CRM_SystemConfig :AuthorPage
{
    PGTabExecuteCRM tabExecuteCrm = new PGTabExecuteCRM();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadPrivateInfo();
        }
    }

    private void LoadPrivateInfo()
    {
        DataTable dttemp = tabExecuteCrm.ExecuteNonQuery("select * from tj_crm_system_config where compid=" + GetCookieCompID());
        if (dttemp.Rows.Count > 0)
        {
            Image_Logo.ImageUrl = dttemp.Rows[0]["logourl"].ToString();
            HF_LogoImage.Value = dttemp.Rows[0]["logourl"].ToString();
            Image_ShouYe_Top.ImageUrl = dttemp.Rows[0]["hometoppicurl"].ToString();
            HF_Image_ShouYe_Top.Value = dttemp.Rows[0]["hometoppicurl"].ToString();
            input_sharename.Value = dttemp.Rows[0]["sharename"].ToString();
            input_authorcompanyname.Value = dttemp.Rows[0]["authorcompanyname"].ToString(); 
            Image_SignPic.ImageUrl = dttemp.Rows[0]["signpicurl"].ToString();
            HF_Image_SignPic.Value = dttemp.Rows[0]["signpicurl"].ToString();
            img_loginbackurl.ImageUrl = dttemp.Rows[0]["loginbackurl"].ToString();
            hf_loginbackurl.Value = dttemp.Rows[0]["loginbackurl"].ToString();

        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        DataTable dttemp = tabExecuteCrm.ExecuteNonQuery("select count(id) from tj_crm_system_config where compid=" + GetCookieCompID());
        if (dttemp.Rows.Count > 0)
        {
            if (int.Parse(dttemp.Rows[0][0].ToString()) > 0)
            {
                tabExecuteCrm.ExecuteNonQuery(
                    "update tj_crm_system_config set logourl='" + HF_LogoImage.Value + "',hometoppicurl='" +
                    HF_Image_ShouYe_Top.Value + "',authorcompanyname='" + input_authorcompanyname.Value + "',sharename='" + input_sharename.Value + "',signpicurl='" + HF_Image_SignPic.Value + "',loginbackurl='"+hf_loginbackurl.Value+"' where compid=" + GetCookieCompID(), null);
            }
            else
            {
                tabExecuteCrm.ExecuteNonQuery(
                  "insert into tj_crm_system_config(compid,logourl,hometoppicurl,authorcompanyname,sharename,signpicurl,loginbackurl) values(" + GetCookieCompID() + ",'" + HF_LogoImage.Value + "','" + HF_Image_ShouYe_Top.Value + "','" + input_authorcompanyname.Value + "','" + input_sharename.Value + "','" + HF_Image_SignPic.Value + "','"+hf_loginbackurl.Value+"')", null);
            }
            LoadPrivateInfo();
        }
    }
}