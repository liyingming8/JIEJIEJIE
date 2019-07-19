using System;
using System.Web;
using commonlib;

public partial class ckeditor_Upload : AuthorPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        if (Request.Files.Count > 0)
        {
            string output = @"<script type=""text/javascript"">window.parent.CKEDITOR.tools.callFunction({0} ,'{1}');</script>";//回调页面函数
            HttpPostedFile f = Request.Files["upload"];//获取文件名
            string filename = "../UploadFile/"+GetCookieCompID()+"/"+ Guid.NewGuid() + f.FileName.Substring(f.FileName.LastIndexOf("."));
            f.SaveAs(Server.MapPath(filename));
            output = string.Format(output, Request["CKEditorFuncNum"], filename);//回调页面
            Response.Write(output);
            Response.End();
        }
        else
        {
            Response.Write("no file");
            Response.End();
        }
    }
}