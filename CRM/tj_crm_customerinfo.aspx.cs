using System;
using System.Globalization;
using System.Net;
using System.Text;
using System.Web.UI.WebControls;
using TJ.DBUtility;
using commonlib;
using Wuqi.Webdiyer;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class CRM_tj_crm_customerinfo : AuthorPage
{
    readonly PGTabExecuteCRM tab = new PGTabExecuteCRM();
    public CommonFun Common = new CommonFun();
    private int _currentindex = 1; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
        dt = tab.ExecuteQuery("select gradename from tj_crm_customergrade where id=" + id, null);
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

    public string ReturnIsPermit(string ispermit)
    {
        if (ispermit.Equals("True")|| ispermit.Equals("TRUE")|| ispermit.Equals("true")|| ispermit.Equals("t"))
        {
            return "已审核";
        }
        else
        {
            return "未审核";
        }
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('/crm/tj_crm_customerinfoAddEdit.aspx?cmd=" + Sc.EncryptQueryString("edit") + "&ID={0}',700,490,'经销商信息')", Sc.EncryptQueryString(ID));
        }
        else
        {
            return "";
        }
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
            e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
            ((HtmlImage)e.Row.FindControl("editimg")).Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString()));
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (_currentindex-1) + e.Row.RowIndex + 1).ToString();
            string status=((Label)e.Row.FindControl("Status")).Text;
            if (status.Equals("未审核"))
            {
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
            }else
            {
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Green;
            }
            //((LinkButton)e.Row.Cells[12].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
            //if (IsSuperAdmin())
            //{
            //    ((LinkButton)e.Row.Cells[12].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确定要删除当前记录吗?')");
            //}
            //else
            //{
            //     e.Row.Cells[12].Enabled= false;
            //     e.Row.Cells[12].ForeColor = Color.LightGray;
            //}
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
            _filtertemp = "compid="+ GetCookieCompID();
       }
        if (!StatusDropDownList.SelectedValue.Equals("All"))
        {
            if (StatusDropDownList.SelectedValue.Equals("Checked")) {
                _filtertemp += " and ispermit=True";
            }else
            {
                _filtertemp += " and ispermit=False";
            }
        }
       AspNetPager1.RecordCount = int.Parse(tab.ExecuteQuery("select count(id) from tj_crm_customerinfo where "+_filtertemp, null).Rows[0][0].ToString());
       GridView1.DataSource = tab.ExecuteQuery("select id, parentid, compid, customername, username, idcardnumber, sexinfo, phonenumber, telnumber, faxnumber, cityid, addressinfo, wxappid, wxopenid, wxusernumber, authorcode, passwordstring, gradeid, cityarea, ispermit, zhuangtai, refusereasonstring, authorzhengshu, idguid, parentidguid, provincecode, citycode, districtcode from tj_crm_customerinfo where " + _filtertemp + " order by ispermit,gradeid,parentid  limit " + pageSize + " offset " + (pageIndex - 1) * pageSize, null);
       GridView1.DataBind();
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
       _currentindex = e.NewPageIndex;
       DisplayData(e.NewPageIndex, AspNetPager1.PageSize); 
    } 
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("prozs"))
        {
            string result = HttpPost("http://os.china315net.com/crm/ajax/producezhengshu.ashx",
                   "{cid:" +e.CommandArgument + ",compid:" + GetCookieCompID()+ "}");
        }
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
