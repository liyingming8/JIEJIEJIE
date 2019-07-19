using System; 
using TJ.BLL;
using commonlib;
using TJ.Model;

public partial class Admin_TJ_CompADSelect : AuthorPage
{
   
    private readonly BTJ_AdInfo badinfo = new BTJ_AdInfo();  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fromurl.Value = Request.RawUrl;
            FillDll(); 
        }
    } 
    private void FillDll()
    {
        rbl_ad.DataSource = badinfo.GetListsByFilterString("ADID in (1,2,3,7,8)");
        rbl_ad.DataBind(); 
    }
    protected void rbl_ad_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(rbl_ad.SelectedValue))
        {
            MTJ_AdInfo mod = badinfo.GetList(int.Parse(rbl_ad.SelectedValue));
            switch (rbl_ad.SelectedValue)
            {
                case "1":
                    Response.Redirect(fromurl.Value.Replace("TJ_CompADSelect", "TJ_CompADInfoAddEditSimple") + "&ADID=1&bilv="+mod.MWidth+"/"+mod.MHeight, true);
                    break;
                case "2":
                    Response.Redirect(fromurl.Value.Replace("TJ_CompADSelect", "TJ_CompADInfoAddEditSimple") + "&ADID=2&bilv=" + mod.MWidth + "/" + mod.MHeight, true);
                    break;
                case "3":
                    Response.Redirect(fromurl.Value.Replace("TJ_CompADSelect", "TJ_CompADInfoAddEditSimple") + "&ADID=3&bilv=" + mod.MWidth + "/" + mod.MHeight, true);
                    break;
                case "7":
                    Response.Redirect(fromurl.Value.Replace("TJ_CompADSelect", "TJ_CompADInfoAddEditSimple") + "&ADID=7&bilv=" + mod.MWidth + "/" + mod.MHeight, true);
                    break;
                case "8":
                    Response.Redirect(fromurl.Value.Replace("TJ_CompADSelect", "TJ_CompADInfoAddEditSimple") + "&ADID=8&bilv=" + mod.MWidth + "/" + mod.MHeight, true);
                    break;
            } 
        } 
    } 
}