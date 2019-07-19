using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using commonlib;
using Newtonsoft.Json.Linq;
using TJ.BLL;
using TJ.Model;

public partial class Admin_TJ_SWM_ActivedPidInfo : AuthorPage
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            inputstartdate.Value = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            inputenddate.Value = DateTime.Now.ToString("yyyy-MM-dd"); 
            if (IsSuperAdmin())
            {
                foradmin.Visible = true;
                inputcompid.Attributes.Add("onclick", ReturnCompnaySelectScript("所属单位", "0", "UseSWM=1"));
            }
            else
            {
                foradmin.Visible = false;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["pcompid"]))
            {
                hd_compid.Value = Request.QueryString["pcompid"]; 
            }
            if (!string.IsNullOrEmpty(Request.QueryString["pcompnm"]))
            {
                inputcompid.Value = Request.QueryString["pcompnm"];
            }
            LoadData();
        }
    }

    readonly BTJ_GoodsInfo _bgood = new BTJ_GoodsInfo();
    public string ReturnGoodsNameByWlprid(string wlproid)
    {
        IList<MTJ_GoodsInfo> goodslist = _bgood.GetListsByFilterString("WLProID=" + wlproid);
        if (goodslist.Count > 0)
        {
            return goodslist[0].GoodsName;
        }
        else
        {
            return "绑定";
        }
    }

    readonly commfrank _commfun = new commfrank();
    private void LoadData( )
    {
        string compid = (!string.IsNullOrEmpty(hd_compid.Value) && !hd_compid.Value.Equals("0"))? hd_compid.Value: GetCookieCompID();
        var internet = new InternetHandle();
        string jsonstring = internet.GetUrlData("http://www.china315net.com:35224/zhpt/qr3d/pid/from/company/?id=" + compid);
        JObject obj = JObject.Parse(jsonstring);
        JArray jarray = JArray.FromObject(obj["data"]);
        GridView1.DataSource = _commfun.ToDataTable(jarray.ToString());
        GridView1.DataBind();
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");  
            DataKey dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (IsSuperAdmin())
            {
                GridView1.Columns[6].Visible = true;
            }
            else
            {
                GridView1.Columns[6].Visible = false;
            }
            if (dataKey != null)
            {
                ((HyperLink)e.Row.FindControl("hlinkproduct")).Attributes.Add("onclick", "openWinCenter('TJ_GoodsInfoSimpleForBind.aspx?pid=" + Sc.EncryptQueryString(dataKey[0].ToString()) + "&prodid=" + Sc.EncryptQueryString(dataKey[1].ToString()) + "', 600,580,'产品绑定')");
                ((HyperLink)e.Row.FindControl("hlinksuyuan")).Attributes.Add("onclick", "openWinCenter('TJ_SWM_CommonSuyuan.aspx?pid=" + Sc.EncryptQueryString(dataKey[0].ToString()) + "&prodid=" + Sc.EncryptQueryString(dataKey[1].ToString()) + "', 780,660,'溯源信息')");
                ((HyperLink)e.Row.FindControl("hlinkmodules")).NavigateUrl="TJ_SWM_CompModulesView.aspx?pid=" + Sc.EncryptQueryString(dataKey[0].ToString());
                if (IsSuperAdmin())
                {
                    ((HtmlInputButton)e.Row.FindControl("btn_add_packagefunction")).Attributes.Add("onclick", "openWinCenter('TJ_Role_Package_Choose.aspx?pid=" + Sc.EncryptQueryString(dataKey[0].ToString()) +"', 600,580,'添加功能')");
                } 
            } 
        }
    } 

    public class ActiveMode
    {
        public string pid { get; set; }
        public string active_time { get; set; }
        public string qty { get; set; } 
    } 
}