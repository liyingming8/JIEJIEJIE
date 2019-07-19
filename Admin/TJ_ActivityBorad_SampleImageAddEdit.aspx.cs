using System; 
using System.Web.UI; 
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_ActivityBorad_SampleImageAddEdit : AuthorPage
{
    BTJ_ActivityBorad_SampleImage bll = new BTJ_ActivityBorad_SampleImage();
    MTJ_ActivityBorad_SampleImage mod = new MTJ_ActivityBorad_SampleImage();
    readonly BTJ_Activity_BoardInfo _boardInfo = new BTJ_Activity_BoardInfo();
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
          }
          if (!string.IsNullOrEmpty(Request.QueryString["abid"]))
          {
              HF_ABID.Value = Request.QueryString["abid"].Trim();
              labelaboardname.Text = _boardInfo.GetList(int.Parse(Request.QueryString["abid"].Trim())).ActivityName;
          }
       }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
           mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }  
        mod.ABID = Convert.ToInt32(HF_ABID.Value.Trim());
        mod.KeyValue = inputKeyValue.Value.Trim();
        mod.ShowImage = HF_Image.Value; 
        mod.ToShow = CheckBox_ForShow.Checked;
        mod.Remarks = inputRemarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_ActivityBorad_SampleImageAddEdit.aspx","TJ_ActivityBorad_SampleImage","描述",DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_ActivityBorad_SampleImageAddEdit.aspx","TJ_ActivityBorad_SampleImage","描述",DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void Fillinput(int id)
    {
        MTJ_ActivityBorad_SampleImage ms = bll.GetList(id);
        HF_ABID.Value = ms.ABID.ToString();
        labelaboardname.Text = _boardInfo.GetList(ms.ABID).ActivityName;
        inputKeyValue.Value = ms.KeyValue.Trim();
        HF_Image.Value = ms.ShowImage;
        if (!string.IsNullOrEmpty(HF_Image.Value))
        {
            Image_ShowPic.ImageUrl = HF_Image.Value;
        }
        CheckBox_ForShow.Checked = ms.ToShow; 
        inputRemarks.Value = ms.Remarks.Trim();
    }
}