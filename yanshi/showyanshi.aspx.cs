using System;
using commonlib;

public partial class yanshi_showyanshi : AuthorPage
{
    public string conturl = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty( Request.QueryString["modid"]))
        {
            //hf_show.Value= Sc .DecryptQueryString(Request.QueryString["modid"].Trim());
            //hf_inputbuzou.Value = "0";
            //hf_end.Value = "1"; 
        }
    }
     
}