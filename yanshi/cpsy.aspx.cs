using System;
//using xifeng_syinfo;

public partial class xifeng_cpsy : System.Web.UI.Page
{
  // xifeng_syinfo.ProductionImplService ser = new xifeng_syinfo.ProductionImplService();


    protected void Page_Load(object sender, EventArgs e)
    {
        //var cf = new ProductionImplService();
        
        if (Request.Cookies["compid"] != null && Request.Cookies["cpid"] != null)
        {
            TcompID.Value = Request.Cookies["compid"].Value;
            TcpID.Value = Request.Cookies["cpid"].Value;
            
        }
        else
        {
            TcpID.Value = "yes";
        }
    }
}