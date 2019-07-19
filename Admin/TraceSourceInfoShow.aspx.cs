using System;
using commonlib;
using TJ.BLL;
public partial class Admin_TraceSourceInfoShow : AuthorPage
{
    public string JsonString = "";
    readonly BTB_SuYuanShengChanPC _bsyscpc = new BTB_SuYuanShengChanPC();
         
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            JsonString = _bsyscpc.GetList(int.Parse(Sc.DecryptQueryString(Request.QueryString["ID"]))).SYJSONString;
        }
        else
        {
            Response.End();
        } 
    }
}