using System;
using System.Collections.Generic;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class yanshi_show: AuthorPage
{
    public string conturl = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty( Request.QueryString["modid"]))
        {
            hf_show.Value= Sc .DecryptQueryString(Request.QueryString["modid"].Trim());
            hf_inputbuzou.Value = "0";
            hf_end.Value = "1";
          
        }
    }
    public string returnurl(string mid,string flag)
    {
        BTJ_SysModuleSiteInfo btj = new BTJ_SysModuleSiteInfo();
        string strurl = string.Empty;
        IList<MTJ_SysModuleSiteInfo> mtj = btj.GetListsByFilterString("SMID="+mid, "ShowOrder");
        if (mtj.Count>0)
        {
            if (flag == "url")
            {
                strurl = mtj[0].LinkURL;
            }
            else
            {
                strurl = mtj[0].ShowContent;
            }
            
        }
        return strurl;
    }
}