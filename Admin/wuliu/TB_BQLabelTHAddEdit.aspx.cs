using System;
using System.Data;
using System.IO;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Text;

public partial class Admin_TB_BQLabelTHAddEdit : AuthorPage
{
    private BTB_Products_Type bll = new BTB_Products_Type();
    private MTB_Products_Type mod = new MTB_Products_Type();
    private CommonFunWL comfun = new CommonFunWL();
    private readonly DBClass db = new DBClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
            }
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim();
            }

            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    break;
                case "edit":
                    Button1.Text = "修改";
                    fillinput(int.Parse(HF_ID.Value.Trim()));
                    break;
                default:
                    break;
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        //{
        //   mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        //}       
        //mod.TypeCode = inputTypeCode.Value.Trim();
        //mod.TypeName = inputTypeName.Value.Trim();
        //mod.ParentID = Convert.ToInt32(ComboBox_ParentID.SelectedValue);
        //mod.CompID = Convert.ToInt32(GetCookieCompID());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                db.InserBQLabelTH(inputCodeOld.Value, inputCodeNew.Value, RadioButtonList_Mode.SelectedValue,
                    GetCookieCompID(), inputBZ.Value);
                break;
            case "edit":
                db.updateBQLabelTH(HF_ID.Value, inputCodeOld.Value, inputCodeNew.Value, inputBZ.Value);
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        //this.Response.Write("<script>alert('操作成功！');</script>");
        // ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('操作成功!');", true);
    }

    private void fillinput(int id)
    {
        //MTB_Products_Type ms = bll.GetList(id);
        DataTable dt = db.GetBQLabelTH("ID=" + id);
        if (dt.Rows.Count > 0)
        {
            inputCodeOld.Value = dt.Rows[0]["LabelCodeOld"].ToString().Trim();
            inputCodeNew.Value = dt.Rows[0]["LabelCodeNew"].ToString().Trim();
            inputBZ.Value = dt.Rows[0]["Remarks"].ToString().Trim();
            RadioButtonList_Mode.SelectedValue = dt.Rows[0]["Flag"].ToString().Trim();
        }
    }

    protected void Button_upload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            try
            {
                FileInfo file = new FileInfo(FileUpload1.PostedFile.FileName);
                if (file.Extension.ToLower().Equals(".txt"))
                {
                    // 保存文件
                    string uploadfilepath = "";
                    uploadfilepath = Server.MapPath(@"/Admin/wuliu/Uploadfiles/") +
                                     DateTime.Now.ToString("yyyyMMddhhmmssss") + ".txt";
                    FileUpload1.SaveAs(uploadfilepath);
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('上传成功!');", true);
                    Label1.Text = "已上传文件:" + FileUpload1.FileName;
                    hf_file.Value = uploadfilepath;
                    Button1.Enabled = true;
                    //Button_DataCheck.Enabled = true;
                    //Button_DataCheck_Click(sender, e);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('请上传格式为(.txt)的替换文件');",
                        true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('" + ex.Message + "');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('请选取数据文件!');", true);
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string wj = hf_file.Value;
        string[] codearrytemp = new string[0];
        StreamReader sr = new StreamReader(wj.Trim(), Encoding.Default);
        string[] stringSeparators = new string[] {"\r\n"};
        codearrytemp = null;

        codearrytemp = sr.ReadToEnd().Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
        DataTable dttempline = new DataTable();
        dttempline.Columns.Add("LabelCodeNew");
        dttempline.Columns.Add("LabelCodeOld");


        foreach (string line in codearrytemp)
        {
            //dttempline.Rows.Add(line.Split(','));


            //string LabelCodeOld = line.Split(',')[0];

            db.InserBQLabelTH(line.Split(',')[0], line.Split(',')[1], RadioButtonList_Mode.SelectedValue,
                GetCookieCompID(), inputBZ.Value);
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('操作成功!');", true);
    }
}