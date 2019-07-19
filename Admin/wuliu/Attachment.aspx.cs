using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using commonlib;
using Image = System.Drawing.Image;

public partial class Admin_wuliu_Attachment : AuthorPage
{
    private string paraPicUrl = "";
    private string paraTargetImg = "";
    private string paraTargetHd = "";
    private string paraImgMaxSize = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["PicUrl"] != null && Request.QueryString["PicUrl"] != "")
        {
            paraPicUrl = Request.QueryString["PicUrl"];
        }
        else
        {
            Response.End();
        }

        if (Request.QueryString["TargetImg"] != null && Request.QueryString["TargetImg"] != "")
        {
            paraTargetImg = Request.QueryString["TargetImg"];
            fuLogo.Attributes.Add("onpropertychange",
                "javascript:window.parent.ShowImg(this.value,'" + paraTargetImg + "');");
        }
        if (Request.QueryString["TargetHd"] != null && Request.QueryString["TargetHd"] != "")
        {
            paraTargetHd = Request.QueryString["TargetHd"];
        }
        else
        {
            Response.End();
        }

        if (Request.QueryString["imgMaxSize"] != null && Request.QueryString["imgMaxSize"] != "")
        {
            paraImgMaxSize = Request.QueryString["imgMaxSize"];
        }
        else
        {
            paraImgMaxSize = "200";
        }
    }


    protected void btUploadSave_Click(object sender, EventArgs e)
    {
        string strFile = "";
        if (fuLogo.FileName.Trim() == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, typeof (Page), "scriptOperate", "alert('请选择你要上传的图片！');", true);
            return;
        }
        else if (Convert.ToInt32(fuLogo.FileBytes.Count()) > 1024*Convert.ToInt32(paraImgMaxSize))
        {
            ScriptManager.RegisterStartupScript(Page, typeof (Page), "scriptOperate",
                "alert('图片大小不能超过" + paraImgMaxSize + "K！');", true);
            return;
        }
        else if (Convert.ToInt32(fuLogo.FileBytes.Count()) > 20480000)
        {
            ScriptManager.RegisterStartupScript(Page, typeof (Page), "scriptOperate", "alert('图片大小不能超过20M！');", true);
            return;
        }
        else
        {
            if (fuLogo.FileName.Trim().Substring(fuLogo.FileName.Trim().LastIndexOf('.')).ToLower() == ".css" ||
                fuLogo.FileName.Trim().Substring(fuLogo.FileName.Trim().LastIndexOf('.')).ToLower() == ".mp3" ||
                fuLogo.FileName.Trim().Substring(fuLogo.FileName.Trim().LastIndexOf('.')).ToLower() == ".wav" ||
                fuLogo.FileName.Trim().Substring(fuLogo.FileName.Trim().LastIndexOf('.')).ToLower() == ".mp4" ||
                fuLogo.FileName.Trim().Substring(fuLogo.FileName.Trim().LastIndexOf('.')).ToLower() == ".ogg" ||
                fuLogo.FileName.Trim().Substring(fuLogo.FileName.Trim().LastIndexOf('.')).ToLower() == ".webm")
            {
                strFile = strUpLoadFile(fuLogo,
                    "Comm/" + paraPicUrl + "/" + GetCookieCompID() + "/");
                ScriptManager.RegisterStartupScript(Page, typeof (Page), "scriptOperate",
                    "window.parent.callBackForCss('" + paraTargetHd + "','" + strFile + "');", true);
            }
            else
            {
                strFile = strUpLoadFile(fuLogo,
                    "Images/" + paraPicUrl + "/" + GetCookieCompID() + "/");
                ScriptManager.RegisterStartupScript(Page, typeof (Page), "scriptOperate",
                    "window.parent.callBack('" + paraTargetImg + "','" + paraTargetHd + "','" + strFile + "');", true);
            }
        }
    }

    public string strUpLoadFile(FileUpload fileUpload, string strDir)
    {
        //上传图片保存
        try
        {
            string strFullName = fileUpload.PostedFile.FileName;
            string strSuffix = strFullName.Substring(strFullName.LastIndexOf('.'));
            string strFileName = DateTime.Now.ToString("yyMMddhhmmsss") + strSuffix;
            string strReturn = strDir + strFileName;
            string strSaveFileName = HttpContext.Current.Server.MapPath(strDir) + strFileName;
            string strSmallpicSaveFileName = HttpContext.Current.Server.MapPath(strDir) + "sm_" + strFileName;
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(strDir)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(strDir));
            }
            fileUpload.PostedFile.SaveAs(strSaveFileName);
            if (strSuffix.Trim().ToLower() != ".css" && strSuffix.Trim().ToLower() != ".mp3" &&
                strSuffix.Trim().ToLower() != ".wav" && strSuffix.Trim().ToLower() != ".mp4" &&
                strSuffix.Trim().ToLower() != ".webm" && strSuffix.Trim().ToLower() != ".ogg")
            {
                Image image = Image.FromFile(strSaveFileName);
                //生成缩略图
                Image newimage = image.GetThumbnailImage(100,
                    Convert.ToInt32(Convert.ToDecimal(image.Height)*
                                    (Convert.ToDecimal(100)/Convert.ToDecimal(image.Width))), null, new IntPtr());
                newimage.Save(strSmallpicSaveFileName);
            }
            return strReturn;
        }
        catch
        {
            return null;
        }
    }
}