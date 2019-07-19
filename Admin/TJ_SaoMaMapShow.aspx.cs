using System.Data;
using System.Text;
using commonlib;
using System; 
using TJ.DBUtility;

public partial class Admin_TJ_SaoMaMapShow : AuthorPage
{ 
    public string Positonstring = "";
    TabExecute tab = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["startday"]) &&
            !string.IsNullOrEmpty(Request.QueryString["endday"]))
        {
            string sql = "SELECT SMAddress,lat,lng FROM TJ_SMinfo_2018 where CompID=" + GetCookieCompID() +
                         " and SMTime>='" + Request.QueryString["startday"] + "' and lat is not null and lng is not null and SMTime<'" +
                         Convert.ToDateTime(Request.QueryString["endday"]).AddDays(1).ToString("yyyy-MM-dd") + "' group by SMAddress,lat,lng";
           DataTable dt =  tab.ExecuteNonQuery(sql);
           StringBuilder sb = new StringBuilder(); 
            foreach (DataRow row in dt.Rows)
            {
                sb.AppendLine(",[[" + row["lng"] + "," + row["lat"] + "],\"" + row["SMAddress"] + "\"]");
            }

            Positonstring = "data:["+sb.ToString().Substring(1)+"]";
        } 
    }
}