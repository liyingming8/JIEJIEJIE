using System;
using System.Collections.Specialized;
using System.IO;
using System.Web.UI;
using commonlib;
using ICSharpCode.SharpZipLib.Zip; 
using Newtonsoft.Json.Linq;

public partial class Admin_wuliu_LabelDataInsertToTouYun : AuthorPage
{
    private string _uploadfilepath = string.Empty; 
    private int _rowcout; 
    protected void Page_Load(object sender, EventArgs e)
    {
    }

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
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('上传成功!');", true);
                    Label1.Text = "已上传文件:" + FileUpload1.FileName;
                    Button_DataCheck.Enabled = true;
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

    public void UnZip(string srcFile, string dstFile, int bufferSize)
    {
        var fileStreamIn = new FileStream(srcFile, FileMode.Open, FileAccess.Read);
        var zipInStream = new ZipInputStream(fileStreamIn);
        var entry = zipInStream.GetNextEntry();
        var fileStreamOut = new FileStream(dstFile, FileMode.Create, FileAccess.Write);
        int size;
        var buffer = new byte[bufferSize];
        do
        {
            size = zipInStream.Read(buffer, 0, buffer.Length);
            fileStreamOut.Write(buffer, 0, size);
        } while (size > 0);
        zipInStream.Close();
        fileStreamOut.Close();
        fileStreamIn.Close();
    }

    protected void Button_DataCheck_Click(object sender, EventArgs e)
    {
        if (!((string) ViewState["uploadfilepath"]).Trim().Equals(string.Empty))
        {
            string outfilename = Server.MapPath(@"Uploadfiles/") + DateTime.Now.ToString("yyyyMMddhhmmssss") + ".txt";
            UnZip(ViewState["uploadfilepath"].ToString(), outfilename,5120);
            StreamReader rd = new StreamReader(outfilename);
            _rowcout = 0;
            while (!rd.EndOfStream)
            {
                if (!string.IsNullOrEmpty(rd.ReadLine()))
                {
                    _rowcout++;
                }
            }
            ViewState.Add("rownum", _rowcout);
            Label2.Text = "通过检查并计算,该数据包里面有" + _rowcout + "套物流标签的数据!";
            Button_Insert.Enabled = true;
        }
    }

    private int recount = 0;
    private string ReturnNum(string type, int rowcount, string retype)
    {
        recount = 0;
        switch (type)
        {
            case "1":
                if (retype.Equals("cap"))
                {
                    recount = rowcount*6;
                }
                if (retype.Equals("pac"))
                {
                    recount = rowcount;
                }
                break;
            case "2":
                if (retype.Equals("cap"))
                {
                    recount = rowcount*6;
                }
                if (retype.Equals("pac"))
                {
                    recount = rowcount;
                }
                break;
            case "3":
                if (retype.Equals("cap"))
                {
                    recount = rowcount*12;
                }
                if (retype.Equals("pac"))
                {
                    recount = rowcount*2;
                }
                break;
            case "4":
                 if (retype.Equals("cap"))
                {
                    recount = rowcount*4;
                }
                if (retype.Equals("pac"))
                {
                    recount = rowcount;
                }
                break;
        }
        return recount.ToString();
    }


    protected void Button_Insert_Click(object sender, EventArgs e)
    { 
        try
        {
           string tempstring = "";
            if (RBL_DataType.SelectedValue.Equals("1"))
            {
                InternetHandle internet = new InternetHandle();
                _uploadfilepath = ((string)ViewState["uploadfilepath"]).Trim();
                string filekey = internet.HashMD5_String(_uploadfilepath); 
                NameValueCollection nmCollections = new NameValueCollection();
                nmCollections.Add("fileType", "zip");
                nmCollections.Add("packageCount", ReturnNum(RBL_CodeMode.SelectedValue, int.Parse(ViewState["rownum"].ToString()), "pac"));
                nmCollections.Add("capCount", ReturnNum(RBL_CodeMode.SelectedValue, int.Parse(ViewState["rownum"].ToString()), "cap"));
                nmCollections.Add("md5", filekey);
                nmCollections.Add("codeType", RBL_CodeMode.SelectedValue);
                //tempstring = internet.HttpPostData(DAConfig.Touyunurl + "code-service/isv/codefilehandle/package/company/jiuguijiu/terminal/tjkj", 60000, "file", _uploadfilepath, nmCollections); 
                tempstring = internet.HttpPostData(DAConfig.Touyunurl + "code-service/isv/codefilehandle/package/company/JGJ/terminal/tjkj", 60000, "file", _uploadfilepath, nmCollections); 
            }
            if (RBL_DataType.SelectedValue.Equals("30"))
            {
                InternetHandle internet = new InternetHandle();
                _uploadfilepath = ((string)ViewState["uploadfilepath"]).Trim();
                string filekey = internet.HashMD5_String(_uploadfilepath);
                NameValueCollection nmCollections = new NameValueCollection();
                nmCollections.Add("fileType", "zip");
                nmCollections.Add("packageCount", ReturnNum(RBL_CodeMode.SelectedValue, int.Parse(ViewState["rownum"].ToString()),"pac"));
                nmCollections.Add("capCount", ReturnNum(RBL_CodeMode.SelectedValue, int.Parse(ViewState["rownum"].ToString()), "cap"));
                nmCollections.Add("md5", filekey);
                nmCollections.Add("codeType", "30");
                tempstring = internet.HttpPostData(DAConfig.Touyunurl + "code-service/isv/codefilehandle/mapping/company/JGJ/terminal/tjkj", 60000, "file", _uploadfilepath, nmCollections); 
            }
            JObject jo = JObject.Parse(tempstring); 
            if (jo["state"] != null)
            {
                if (jo["state"].ToString().Equals("1"))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('上传成功！');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('" + jo["msg"] + "');", true);
                }
            }
            Button_DataCheck.Enabled = false;
            Button_Insert.Enabled = false;
        }
        catch (Exception ex)
        { 
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('" + ex.Message + "');", true); 
        }
    }

    private string _res = "";
    private string Returnnum(string value, string type)
    {
        _res = "";
        switch (value)
        {
            case "1":
                if (type.ToLower().Trim().Equals("x"))
                {
                    _res="1";
                }
                else
                {
                    _res = "6";
                }
                break;
            case "2":
                if (type.ToLower().Trim().Equals("x"))
                {
                    _res = "2";
                }
                else
                {
                    _res = "6";
                }
                break;
            case "3":
                if (type.ToLower().Trim().Equals("x"))
                {
                    _res = "2";
                }
                else
                {
                    _res = "6";
                }
                break;
        }
        return _res; 
    } 

    protected void RBL_DataType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RBL_DataType.SelectedValue.Equals("1"))
        {
            rowtoinput.Visible = true;
        }
        else
        {
            rowtoinput.Visible = false;
        }
    }
}