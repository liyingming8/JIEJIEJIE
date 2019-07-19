using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using commonlib;
using Image = System.Drawing.Image;

public partial class Admin_Attachment : AuthorPage
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
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "scriptOperate", "alert('请选择你要上传的图片！');", true);
            return;
        }
        else if (Convert.ToInt32(fuLogo.FileBytes.Count()) > 1024 * Convert.ToInt32(paraImgMaxSize))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "scriptOperate",
                "alert('图片大小不能超过" + paraImgMaxSize + "K！');", true);
            return;
        }
        else if (Convert.ToInt32(fuLogo.FileBytes.Count()) > 20480000)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "scriptOperate", "alert('图片大小不能超过20M！');", true);
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
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "scriptOperate",
                    "window.parent.callBackForCss('" + paraTargetHd + "','" + strFile + "');", true);
            }
            else
            {
                strFile = strUpLoadFile(fuLogo,
                    "Images/" + paraPicUrl + "/" + GetCookieCompID() + "/");
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "scriptOperate",
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
            string strReturn = strDir +strFileName;
            string strSaveFileName = HttpContext.Current.Server.MapPath(strDir) + strFileName;
            string strNewSaveFileName = HttpContext.Current.Server.MapPath(strDir) +"n"+ strFileName;
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
                Image orImage = Image.FromFile(strSaveFileName);
                string outerror = "";
                if (orImage.Width > 700)
                { 
                    GetThumbnailImage(strNewSaveFileName, strSaveFileName, 700,Convert.ToInt32(Convert.ToDecimal(orImage.Height)*(Convert.ToDecimal(700) / Convert.ToDecimal(orImage.Width))), 93, out outerror, "image/jpeg");
                    strReturn = strDir +"n"+ strFileName; 
                } 
                GetThumbnailImage(strSmallpicSaveFileName, strSaveFileName, 100, Convert.ToInt32(Convert.ToDecimal(orImage.Height) * (Convert.ToDecimal(100) / Convert.ToDecimal(orImage.Width))), 93, out outerror, "image/jpeg");
                orImage.Dispose(); 
            }
            return strReturn;
        }
        catch
        {
            return null;
        }
    }

    public bool GetThumbnailImage(string destPath, string srcPath, int destWidth, int destHeight, int quality, out string error, string mimeType = "image/jpeg")
    {
        bool retVal = false;
        error = string.Empty;
        //宽高不能小于0
        if (destWidth < 0 || destHeight < 0)
        {
            error = "目标宽高不能小于0";
            return retVal;
        }
        //宽高不能同时为0
        if (destWidth == 0 && destHeight == 0)
        {
            error = "目标宽高不能同时为0";
            return retVal;
        }
        Image srcImage = null;
        Image destImage = null;
        Graphics graphics = null;
        try
        {
            //获取源图像
            srcImage = Image.FromFile(srcPath, false);
            //计算高宽比例
            float d = (float)srcImage.Height / srcImage.Width;
            //如果输入的宽为0，则按高度等比缩放
            if (destWidth == 0)
            {
                destWidth = Convert.ToInt32(destHeight / d);
            }
            //如果输入的高为0，则按宽度等比缩放
            if (destHeight == 0)
            {
                destHeight = Convert.ToInt32(destWidth * d);
            }
            //定义画布
            destImage = new Bitmap(destWidth, destHeight);
            //获取高清Graphics
            graphics = GetGraphics(destImage);
            //将源图像画到画布上，注意最后一个参数GraphicsUnit.Pixel
            graphics.DrawImage(srcImage, new Rectangle(0, 0, destWidth, destHeight), new Rectangle(0, 0, srcImage.Width, srcImage.Height), GraphicsUnit.Pixel);
            //如果是覆盖则先释放源资源
            if (destPath == srcPath)
            {
                srcImage.Dispose();
            }
            //保存到文件，同时进一步控制质量
            SaveImage2File(destPath, destImage, quality, mimeType);
            retVal = true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }
        finally
        {
            if (srcImage != null)
                srcImage.Dispose();
            if (destImage != null)
                destImage.Dispose();
            if (graphics != null)
                graphics.Dispose();
        }
        return retVal;
    }
    public void SaveImage2File(string path, Image destImage, int quality, string mimeType = "image/jpeg")
    {
        if (quality <= 0 || quality > 100) quality = 95;
        //创建保存的文件夹
        FileInfo fileInfo = new FileInfo(path);
        if (!Directory.Exists(fileInfo.DirectoryName))
        {
            Directory.CreateDirectory(fileInfo.DirectoryName);
        }
        //设置保存参数，保存参数里进一步控制质量
        EncoderParameters encoderParams = new EncoderParameters();
        long[] qua = new long[] { quality };
        EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
        encoderParams.Param[0] = encoderParam;
        //获取指定mimeType的mimeType的ImageCodecInfo
        var codecInfo = ImageCodecInfo.GetImageEncoders().FirstOrDefault(ici => ici.MimeType == mimeType);
        destImage.Save(path, codecInfo, encoderParams);
    }

    /// <summary>
    /// 获取高清的Graphics
    /// </summary>
    /// <param name="img"></param>
    /// <returns></returns>
    public Graphics GetGraphics(Image img)
    {
        var g = Graphics.FromImage(img);
        //设置质量
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.CompositingQuality = CompositingQuality.HighQuality;
        //InterpolationMode不能使用High或者HighQualityBicubic,如果是灰色或者部分浅色的图像是会在边缘处出一白色透明的线
        //用HighQualityBilinear却会使图片比其他两种模式模糊（需要肉眼仔细对比才可以看出）
        g.InterpolationMode = InterpolationMode.Default;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        return g;
    } 
}