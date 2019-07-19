using System;
using System.Globalization;
using System.Net;
using System.Text;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using commonlib;
using Wuqi.Webdiyer;
using System.Data;

public partial class CRM_tj_crm_customerinfo_select : AuthorPage
{
    readonly PGTabExecuteCRM tab = new PGTabExecuteCRM();
    public CommonFun Common = new CommonFun();
    private int _currentindex = 1; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(!string.IsNullOrEmpty(Request.QueryString["fr"]))
            {
                fr.Value = Sc.DecryptQueryString(Request.QueryString["fr"]);
            }
            if(!string.IsNullOrEmpty(Request.QueryString["para"]))
            {
                para.Value = Sc.DecryptQueryString(Request.QueryString["para"].ToString());
            }
             _currentindex = 1;
             AspNetPager1.CurrentPageIndex = 1;
             DisplayData(1, AspNetPager1.PageSize);
        }
    }

    DataTable dt = new DataTable();
    string temp = string.Empty;
    public string ReturnCustomerName(string id)
    {
        dt = tab.ExecuteQuery("select customername from tj_crm_customerinfo where id=" + id, null);
        if (dt.Rows.Count > 0)
        {
            temp = dt.Rows[0][0].ToString();
        }
        else
        {
            temp = "";
        }
        dt.Dispose();
        return temp;
    }

    public string ReturnCustomerGradeName(string id)
    {
        dt = tab.ExecuteQuery("select customername from tj_crm_customerinfo where id=" + id, null);
        if (dt.Rows.Count > 0)
        {
            temp = dt.Rows[0][0].ToString();
        }
        else
        {
            temp = "";
        }
        dt.Dispose();
        return temp;
    } 

    public string XiangXiLinkString(string id, string nm)
    {
        if (ID.Length > 0)
        {
            string temp = "";
            temp = fr.Value + "?" + para.Value + "&sid=" + Sc.EncryptQueryString(id) + "&snm=" + Sc.EncryptQueryString(nm);  
            return string.Format("javascript:closemyWindowReloadNewhref('" + temp + "')");
        }
        return "";
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         var dataKey = GridView1.DataKeys[e.RowIndex];
         if (dataKey != null)
         {
             var key = GridView1.DataKeys[e.RowIndex];
             if (key != null)
                 tab.ExecuteQuery("delete from tj_crm_customerinfo where id="+key["id"],null);
         }
        DisplayData(_currentindex, AspNetPager1.PageSize);
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
            e.Row.Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString(),dataKey[1].ToString()));
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex-1) + e.Row.RowIndex + 1).ToString(); 
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
       _currentindex = 1;
       AspNetPager1.CurrentPageIndex = 1;
       DisplayData(1, AspNetPager1.PageSize);
    }

    private string _filtertemp ="1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        if (inputSearchKeyword.Value.Trim().Length > 0)
       {
         _filtertemp = "compid="+GetCookieCompID()+ " and "+ DDLField.SelectedValue + " like '%" + inputSearchKeyword.Value.Trim() + "%'";
       }
       else
       {
           _filtertemp = "compid="+GetCookieCompID();
       }
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from tj_crm_customerinfo where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQuery("select id, parentid, compid, customername, username, idcardnumber, sexinfo, phonenumber, telnumber, faxnumber, cityid, addressinfo, wxappid, wxopenid, wxusernumber, authorcode, passwordstring, gradeid, cityarea, ispermit, zhuangtai, refusereasonstring, authorzhengshu, idguid, parentidguid, provincecode, citycode, districtcode from tj_crm_customerinfo where " + _filtertemp + " order by gradeid,parentid  limit " + pageSize + " offset " + (pageIndex - 1) * pageSize, null);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    }  

    public string HttpPost(string url, string postStr = "", Encoding encode = null)
    {
        string result;

        try
        {
            var webClient = new WebClient { Encoding = Encoding.UTF8 };

            if (encode != null)
                webClient.Encoding = encode;

            var sendData = Encoding.GetEncoding("UTF-8").GetBytes(postStr);

            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            webClient.Headers.Add("ContentLength", sendData.Length.ToString(CultureInfo.InvariantCulture));

            var readData = webClient.UploadData(url, "POST", sendData);

            result = Encoding.GetEncoding("UTF-8").GetString(readData);

        }
        catch (Exception ex)
        {
            result = ex.Message;
        }

        return result;
    }  
}
