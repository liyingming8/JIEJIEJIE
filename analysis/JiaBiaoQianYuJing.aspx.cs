using commonlib;
using System;

public partial class analysis_JiaBiaoQianYuJing : AuthorPage
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        string comp = DAConfig.Showmode.Equals("1")?"2":GetCookieCompID();
        TCompid.Value = comp;
        ShowMode.Value = DAConfig.Showmode.Equals("1") ? "1" : "";
    }
}