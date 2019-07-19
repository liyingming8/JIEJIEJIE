using System; 
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using commonlib; 
using Newtonsoft.Json.Linq;
using TJ.Model;
using TJ.BLL;
using TJ.DBUtility;

public partial class Admin_wuliu_AreaAuthor : AuthorPage
{
    InternetHandle nethHandle = new InternetHandle();
    DBClass db = new DBClass();
    private DataTable dttemp;
    InternetHandle nethandle = new InternetHandle();
    private MTJ_RegisterCompanys mod = new MTJ_RegisterCompanys();
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();
    TabExecute tab = new TabExecute();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["agentid"]))
            {
                hd_agentid.Value = Request.QueryString["agentid"];
                hd_author_areaid.Value = GetServerString(hd_agentid.Value);
                dttemp = GetServerData("http://117.34.70.23:32176/gis/adm/", "1/");
                ddl_province.DataSource = dttemp;
                ddl_province.DataBind();
                ckeckboxlist_area.DataSource = dttemp;
                ckeckboxlist_area.DataBind();
                Autoselectchecklist();
            } 
        } 
    }

    private string GetServerString(string agentid)
    {
        string urlstring = "http://117.34.70.23:32176/gis/authorized_area/"+agentid+"/"; 
        string tempstring = nethHandle.GetUrlData(urlstring);
        var obj1 = JArray.Parse(tempstring);
        string returnstring = "";
        foreach (var jToken in obj1)
        {
            var itemArray = (JArray)jToken;
            returnstring += "," + itemArray[0]; 
        }
        return returnstring.StartsWith(",") ? returnstring.Substring(1) : returnstring;
    }

    private DataTable GetServerData(string url, string param)
    {
        var dttemp = new DataTable();
        dttemp.Columns.Add("id", typeof (Int32));
        dttemp.Columns.Add("vl", typeof (string));
        string tempstring = nethHandle.GetUrlData(url+param);
        var obj1 = JArray.Parse(tempstring);
        foreach (var jToken in obj1)
        {
            var itemArray = (JArray) jToken;
            dttemp.Rows.Add(itemArray[0].ToString(), itemArray[1].ToString());
        }
        return dttemp; 
    }
    protected void ddl_province_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_city.Items.Clear();
        ddl_city.Items.Add(new ListItem("全省", "0"));
        if (ddl_province.SelectedValue.Equals("0"))
        {
            dttemp = GetServerData("http://117.34.70.23:32176/gis/adm/","1/"); 
        }
        else
        {
            dttemp = GetServerData("http://117.34.70.23:32176/gis/adm/","2/" + ddl_province.SelectedValue + "/"); 
            ddl_city.DataSource = dttemp;
            ddl_city.DataBind();
            ddl_city.SelectedValue = "0";
        }  
        ckeckboxlist_area.DataSource = dttemp;
        ckeckboxlist_area.DataBind();
        Autoselectchecklist();
    }
    protected void ddl_city_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_city.SelectedValue.Equals("0"))
        {
            dttemp = GetServerData("http://117.34.70.23:32176/gis/adm/","2/" + ddl_province.SelectedValue + "/");
        }
        else
        {
            dttemp = GetServerData("http://117.34.70.23:32176/gis/adm/","3/" + ddl_city.SelectedValue + "/");
        }  
        ckeckboxlist_area.DataSource = dttemp;
        ckeckboxlist_area.DataBind();
        Autoselectchecklist();
    }

    private void Autoselectchecklist()
    {
        foreach (ListItem item in ckeckboxlist_area.Items)
        {
            if (("," + hd_author_areaid.Value + ",").Contains("," + item.Value + ","))
            {
                item.Selected = true;
            }
        }
    }
    protected void btnok_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in ckeckboxlist_area.Items)
        {
            if (item.Selected)
            {
                if (!("," + hd_author_areaid.Value + ",").Contains("," + item.Value + ","))
                {
                    db.PggisSqlExecute("insert into authorized_area(compid,adm_id) values(" + hd_agentid.Value + "," +
                                       item.Value + ")");
                } 
            }
            else
            {
                if (("," + hd_author_areaid.Value + ",").Contains("," + item.Value + ","))
                {
                    db.PggisSqlExecute("delete from authorized_area where compid=" + hd_agentid.Value + " and adm_id=" +item.Value);
                } 
            } 
        }
        if (!string.IsNullOrEmpty(Request.QueryString["flag"])&&Convert.ToInt32(Request.QueryString["flag"])==1)
        {
            string flag = Request.QueryString["flag"];
            string temp = nethandle.GetUrlData("http://117.34.70.23:32176/gis/authorized_area/" + hd_agentid.Value + "/");
            JArray obj = JArray.Parse(temp);
            var dttemp = new DataTable();
            dttemp.Columns.Add("id", typeof(Int32));
            dttemp.Columns.Add("vl", typeof(string));
            string tempstring = "";
            foreach (var jToken in obj)
            {
                var itemArray = (JArray)jToken;
                dttemp.Rows.Add(itemArray[0].ToString(), itemArray[1].ToString());
                tempstring += "," + itemArray[1].ToString();
            }
            /*
            mod.CompID = Convert.ToInt32(hd_agentid.Value);
            mod.AllowAreaInfo = tempstring;
            bll.Modify(mod);
            */
            string sql = "update [TJMarketingSystemYin].[dbo].[TJ_RegisterCompanys] set AllowAreaInfo='" + (tempstring.StartsWith(",") ? tempstring.Substring(1) : tempstring) + "' where [CompID]=" + Convert.ToInt32(hd_agentid.Value);
            tab.ExecuteNonQuery(sql,null);
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true); 
    }
}