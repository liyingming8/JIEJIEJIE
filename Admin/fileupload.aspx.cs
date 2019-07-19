using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Admin_fileupload : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (fileUp.HasFile) //判断是否有文件
        {
            Literal lt = new Literal(); //定义一个Literal用来显示脚本

            if (CheckFileType(fileUp.FileName)) //检查上传文件的类型
            {
                string filePath = "/Images/upload/" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileUp.FileName;
                fileUp.SaveAs(MapPath(filePath)); //把文件上传到服务器的绝对路径上               
            }
        }
    }

    public bool CheckFileType(string fileName)
    {
        //获取文件的扩展名,前提要用这个方法必须引入命名空间io

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