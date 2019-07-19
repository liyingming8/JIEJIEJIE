using System;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_AwardInfo_ImagesAddEdit : AuthorPage
{
    BTJ_AwardInfo_Images bll = new BTJ_AwardInfo_Images();
    MTJ_AwardInfo_Images mod = new MTJ_AwardInfo_Images();
    readonly BTJ_AwardInfo _btjAward = new BTJ_AwardInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
       {
           if (!string.IsNullOrEmpty(Request.QueryString["awid"]))
           {
               hd_awid.Value = Request.QueryString["awid"];
               label_awdnm.Text = _btjAward.GetList(int.Parse(hd_awid.Value)).AwardThing;
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
                       fillinput(int.Parse(HF_ID.Value.Trim()));
                       break;
                   default:
                       break;
               }
            }
           else
           {
               Response.End();
           }
       }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
           mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        } 
        mod.awid = Convert.ToInt32(hd_awid.Value);
        mod.isshow = ckb_isshow.Checked ? 1 : 0;
        mod.urlstring = HF_ImageUrl.Value.Trim();
        mod.remarks = inputremarks.Value.Trim();
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_AwardInfo_ImagesAddEdit.aspx","TJ_AwardInfo_Images","描述",DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                bll.Modify(mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_AwardInfo_ImagesAddEdit.aspx","TJ_AwardInfo_Images","描述",DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,GetType(), "reload", "closemyWindow();", true);
}

    private void fillinput(int id)
    {
        MTJ_AwardInfo_Images ms = bll.GetList(id);
        hd_awid.Value = ms.awid.ToString();
        ckb_isshow.Checked = ms.isshow > 0; 
        Image_AwardUrl.ImageUrl = ms.urlstring.Trim();
        HF_ImageUrl.Value = ms.urlstring.Trim();
        inputremarks.Value = ms.remarks.Trim();
    }
}