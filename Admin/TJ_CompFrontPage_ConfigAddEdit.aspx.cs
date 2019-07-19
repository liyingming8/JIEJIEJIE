using System; 
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
public partial class Admin_TJ_CompFrontPage_ConfigAddEdit : AuthorPage
{
    readonly BTJ_CompFrontPage_Config _bll = new BTJ_CompFrontPage_Config();
    MTJ_CompFrontPage_Config _mod = new MTJ_CompFrontPage_Config();
    readonly BTJ_CompTheme_color _btjCompTheme = new BTJ_CompTheme_color();
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
       {
          IList<MTJ_CompFrontPage_Config> list = _bll.GetListsByFilterString("compid=" + GetCookieCompID());
           if (list.Count > 0)
           {
               HF_CMD.Value = "edit";
               HF_ID.Value = list[0].id.ToString();
           }
           else
           {
               HF_CMD.Value = "add";
           }
           FillDdl();
           switch (HF_CMD.Value)
           {
               case "add":
                   Button1.Text = "添加";
                   break;
               case "edit":
                   Button1.Text = "修改";
                   Fillinput(int.Parse(HF_ID.Value.Trim()));
                   break;
               default:
                   break;
           }
           ckb_isshowlogo.Attributes.Add("onclick", "showuploadlogo()");
           ckb_gzwx.Attributes.Add("onclick", "guanzhuweixin()");
           ckb_show_bottom.Attributes.Add("onclick", "showbottomrow()");
       }
    }

    private void FillDdl()
    {
        var btjLayout = new BTJ_LayoutInfo();
        lbl_layout.DataSource = btjLayout.GetLists();
        lbl_layout.DataBind();
        lbl_layout.SelectedIndex = 0;
        var btjCompTheme = new BTJ_CompTheme_color();
        lbl_theme_color.DataSource = btjCompTheme.GetListsByFilterString("layid=" + lbl_layout.SelectedValue);
        lbl_theme_color.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
           _mod = _bll.GetList(Convert.ToInt32(HF_ID.Value));
        } 
        _mod.compid = Convert.ToInt32(GetCookieCompID());
        _mod.layid = Convert.ToInt32(lbl_layout.SelectedValue.Trim());
        MTJ_CompTheme_color mtjCompTheme = _btjCompTheme.GetList(int.Parse(string.IsNullOrEmpty(lbl_theme_color.SelectedValue) ? lbl_theme_color.Items[0].Value : lbl_theme_color.SelectedValue));
        _mod.themecolorid = mtjCompTheme.id;
        _mod.frantbackcolor = mtjCompTheme.themecolor;
        _mod.logopath = mtjCompTheme.path;
        //mod.frantbackcolor = inputfrantbackcolor.Value.Trim();
        _mod.guanzhuweix = Convert.ToBoolean(ckb_gzwx.Checked);
        _mod.getnicknmandheader = ckb_getnicknameandtouxiang.Checked;
        _mod.getweizhi = ckb_saomadiliweizhi.Checked;
        _mod.isshowlogo = ckb_isshowlogo.Checked;
        _mod.guanzhuqrcodeurl = inputguanzhuqrcodeurl.Value.Trim();
        _mod.showlogo = HF_Image_showlogo.Value.Trim();
        _mod.updatetime = DateTime.Now;
        _mod.upuserid = Convert.ToInt32(GetCookieUID());
        _mod.bigbackgroudimg = HF_Image_dbjpic.Value.Trim();
        _mod.wxgzhmc = inputgongzhonghaomingcheng.Value;
        _mod.showbottom = ckb_show_bottom.Checked;
        _mod.bottomcontent = bottomcontent.Value;
        _mod.pzyz = false;
        _mod.gotourl = inputgourl.Value;
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                _bll.Insert(_mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_CompFrontPage_ConfigAddEdit.aspx","TJ_CompFrontPage_Config","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"新增",""));
                break;
            case "edit":
                _bll.Modify(_mod);
                 RecordDealLog(new MTJ_DealLog(0,"TJ_CompFrontPage_ConfigAddEdit.aspx","TJ_CompFrontPage_Config","描述",System.DateTime.Now,int.Parse(GetCookieUID()),"修改",""));
                break;
        }
         ScriptManager.RegisterStartupScript(UpdatePanel1,this.GetType(), "reload", "closemyWindow();", true);
}

    private void Fillinput(int id)
    {
        MTJ_CompFrontPage_Config ms = _bll.GetList(id); 
        //inputlayid.Value = ms.layid.ToString().Trim();
        foreach (ListItem item in lbl_theme_color.Items)
        {
            if (item.Value.Equals(ms.themecolorid.ToString()))
            {
                item.Selected = true;
                break;
            }
        }
        if (string.IsNullOrEmpty(lbl_theme_color.SelectedValue))
        {
            lbl_theme_color.Items[0].Selected = true;
        }
        ckb_gzwx.Checked = ms.guanzhuweix;
        if (ms.guanzhuweix)
        {
            wxlogo.Attributes.Remove("style"); 
            wxgzhm.Attributes.Remove("style");  
        }
        else
        {
            wxlogo.Attributes.Add("style", "display:none");
            wxgzhm.Attributes.Add("style", "display:none");  
        }
        lbl_layout.SelectedValue = ms.layid.ToString();
        inputguanzhuqrcodeurl.Value = ms.guanzhuqrcodeurl.Trim();
        Image_showlogo.ImageUrl = ms.showlogo.Trim();
        HF_Image_showlogo.Value = ms.showlogo.Trim();
        Image_dbjpic.ImageUrl = ms.bigbackgroudimg;
        HF_Image_dbjpic.Value = ms.bigbackgroudimg.Trim();
        inputgongzhonghaomingcheng.Value = ms.wxgzhmc;
        ckb_show_bottom.Checked = ms.showbottom;
        inputgourl.Value = ms.gotourl; 
        bottomcontent.Value = ms.bottomcontent;
        if (ms.layid.Equals(2))
        {
            dbjpic.Attributes.Remove("style");  
        }
        else
        {
            dbjpic.Attributes.Add("style", "display:none");  
        }
        ckb_isshowlogo.Checked = ms.isshowlogo;
        if (ms.isshowlogo)
        {
            trshowlogo.Attributes.Remove("style"); 
        }
        else
        {
            trshowlogo.Attributes.Add("style", "display:none"); 
        }
        if (ms.showbottom)
        { 
            bottomrow.Attributes.Remove("style");  
        }
        else
        {
            bottomrow.Attributes.Add("style", "display:none");  
        }
    }
    protected void lbl_theme_color_DataBound(object sender, EventArgs e)
    {
        int hs = e.GetHashCode();
    }
    protected void lbl_layout_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lbl_layout.SelectedValue.Equals("2"))
        {
            dbjpic.Attributes.Remove("style");  
        }
        else
        {
            dbjpic.Attributes.Add("style", "display:none");  
        }
        ckb_gzwx.Checked = false;
        ckb_isshowlogo.Checked = false;
        ckb_show_bottom.Checked = false;
        Fillthemerbl(lbl_layout.SelectedValue);  
    }
    
    private void Fillthemerbl(string layid)
    {
        var btjCompTheme = new BTJ_CompTheme_color();
        lbl_theme_color.DataSource = btjCompTheme.GetListsByFilterString("layid=" + layid);
        lbl_theme_color.DataBind();
        _mod = _bll.GetList(int.Parse(HF_ID.Value));
        foreach (ListItem item in lbl_theme_color.Items)
        {
            if (_mod.layid.ToString().Equals(item.Value))
            {
                item.Selected = true;
            }
        }
        if (string.IsNullOrEmpty(lbl_theme_color.SelectedValue))
        {
            lbl_theme_color.Items[0].Selected = true;
        } 
    } 
}