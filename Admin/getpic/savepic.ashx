<%@ WebHandler Language="C#" Class="savepic" %>

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using commonlib;

public class savepic : IHttpHandler
{
    protected Security Sc = new Security();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain"; 
        try
        { 
            if (!string.IsNullOrEmpty(context.Request.Form["source"]))
            {
                string source = context.Request.Form["source"];
                var now = DateTime.Now;
                var compcookie = context.Request.Cookies["TJOSCOMPID"];
                string compid = "1";
                if (compcookie != null)
                {
                    compid = Sc.DecryptQueryString(compcookie.Value);
                }
                string temp = context.Server.MapPath("../Images/commup/");
                string filePath = temp+"/"+ compid + "/" ;
                string fileName = "";
                string strbase64 = source.Substring(source.IndexOf(',') + 1);
                string finalfilename = string.Empty;
                string strufix = source.Substring(0, source.IndexOf(";", System.StringComparison.Ordinal)).Replace("data:",""); 
                switch (strufix.ToLower())
                {
                    case "image/png":
                        fileName = now.ToString("yyyyMMddhhmmssffff") + ".png";
                        break;
                    case "image/jpg":
                          fileName = now.ToString("yyyyMMddhhmmssffff") + ".jpg";
                        break;
                    case "image/jpeg":
                        fileName = now.ToString("yyyyMMddhhmmssffff") + ".jpeg";
                        break;
                    case "image/gif":
                        fileName = now.ToString("yyyyMMddhhmmssffff") + ".gif";
                        break;
                    default:
                        fileName = now.ToString("yyyyMMddhhmmssffff") + ".jpg";
                        break;
                }
                if (string.IsNullOrEmpty(strufix))
                {
                    strufix = "image/jpg";
                }
                strbase64 = strbase64.Trim('\0'); 
                byte[] arr = Convert.FromBase64String(strbase64);
                using (MemoryStream ms = new MemoryStream(arr))
                {
                    Bitmap bmp = new Bitmap(ms);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    } 
                    Bitmap bmp2 = new Bitmap(bmp, bmp.Width, bmp.Height);
                    //将第一个bmp拷贝到bmp2中
                    Graphics draw = Graphics.FromImage(bmp2);
                    draw.DrawImage(bmp, 0, 0);
                    draw.Dispose();
                    switch (strufix.ToLower())
                    {
                        case "image/png":
                            bmp2.Save(filePath + fileName, ImageFormat.Png);
                            break;
                        case "image/jpg": 
                            bmp2.Save(filePath + fileName, ImageFormat.Jpeg);
                            break;
                        case "image/jpeg": 
                            bmp2.Save(filePath + fileName, ImageFormat.Jpeg);
                            break;
                        case "image/gif": 
                            bmp2.Save(filePath + fileName, ImageFormat.Gif);
                            break;
                        default:
                            bmp2.Save(filePath + fileName, ImageFormat.Jpeg);
                            break;
                    } 
                    if (bmp2.Width > 700)
                    {
                        string outerror = string.Empty;
                        GetThumbnailImage(filePath + "n" + fileName, filePath + fileName, 700,
                            Convert.ToInt32(Convert.ToDecimal(bmp2.Height)*
                                            (Convert.ToDecimal(700)/Convert.ToDecimal(bmp2.Width))), 93, out outerror,strufix);
                        finalfilename = "n" + fileName;
                    }
                    if (bmp2 != null)
                    {
                        bmp2.Dispose();
                    }
                    if (bmp != null)
                    {
                        bmp.Dispose();
                    }
                    if (draw != null)
                    {
                        draw.Dispose();
                    } 
                    ms.Close();
                    context.Response.Write("Images/commup/" + compid + "/" + (string.IsNullOrEmpty(finalfilename)?fileName:finalfilename)); 
                } 
            }
            else
            {
                context.Response.Write(0);
            }
        }
        catch 
        {
            context.Response.Write(0);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
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