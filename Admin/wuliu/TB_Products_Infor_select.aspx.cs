using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.DBUtility;
using commonlib;
using TJ.Model;

public partial class Admin_TB_Products_Infor_select : AuthorPage
{  
    readonly TabExecutewuliu _tab = new TabExecutewuliu(); 
    DBClass db = new DBClass();
    TabExecutewuliu tbexe = new TabExecutewuliu();
    BTJ_Integral btjIntegral = new BTJ_Integral();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["agentid"]))
            {
                hd_agentid.Value = Request.QueryString["agentid"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ITGRID"]))
            {
                hd_ITGRID.Value = Sc.DecryptQueryString(Request.QueryString["ITGRID"]);
                hd_ProductsInfo.Value = btjIntegral.GetList(int.Parse(hd_ITGRID.Value)).ProductsInfo;
            }
            else
            {
                hd_author_products.Value = Filltheauthoredproductid(GetCookieCompID(), hd_agentid.Value);
            } 
            FillCheckBoxList("");
        }
    }

    private void FillCheckBoxList(string filterstring)
    {
        checkboxlist_product.DataSource = _tab.ExecuteQuery("select Infor_ID,Products_Name from TB_Products_Infor where CompID=" +
            GetCookieCompID() +(string.IsNullOrEmpty(filterstring)?"":" and "+filterstring)+ " order by Products_Name", null);
        checkboxlist_product.DataBind();
        AutoSelectItems();
    }

    private string Filltheauthoredproductid(string compid, string agentid)
    {
        DataTable dt = db.GetAuthorProductInfo(compid, agentid);
        string tempstring = "";
        foreach (DataRow row in dt.Rows)
        {
            tempstring += ","+row["ProdID"];
        }
        dt.Dispose();
        return tempstring.StartsWith(",") ? tempstring.Substring(1) : tempstring;
    }

    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return
                string.Format(
                    "javascript:var win=openWinCenter('TB_Products_InforAddEdit.aspx?cmd=edit&ID={0}',800,400,'产品信息编辑')", ID);
        }
        else
        {
            return "";
        }
    }

    private void AutoSelectItems()
    {
        if (hd_ITGRID.Value.Trim().Equals("0"))
        {
            foreach (ListItem item in checkboxlist_product.Items)
            {
                if (("," + hd_author_products.Value + ",").Contains("," + item.Value + ","))
                {
                    item.Selected = true;
                }
            }
        }
        else
        {
             foreach (ListItem item in checkboxlist_product.Items)
            {
                if (("," + hd_ProductsInfo.Value + ",").Contains("," + item.Value + ","))
                {
                    item.Selected = true;
                }
            }
        } 
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (hd_ITGRID.Value.Trim().Equals("0"))
        {
            foreach (ListItem item in checkboxlist_product.Items)
            {
                if (item.Selected)
                {
                    if (!("," + hd_author_products.Value + ",").Contains("," + item.Value + ","))
                    {
                        tbexe.ExecuteNonQuery("insert into TB_ProductAuthorForAgent(CompID,AgentID,ProdID,UserID) values(" +
                                              GetCookieCompID() + "," + hd_agentid.Value + "," + item.Value + "," +
                                              GetCookieUID() + ")");
                    }
                }
                else
                {
                    if (("," + hd_author_products.Value + ",").Contains("," + item.Value + ","))
                    {
                        tbexe.ExecuteNonQuery("delete from TB_ProductAuthorForAgent where CompID=" +
                                              GetCookieCompID() + " and AgentID=" + hd_agentid.Value + " and ProdID=" + item.Value);
                    }
                }
            }
        }
        else
        {
            string tempproductidstring = ","+ hd_ProductsInfo.Value+",";
            foreach (ListItem item in checkboxlist_product.Items)
            {
                if (item.Selected)
                {
                    if (!("," + hd_ProductsInfo.Value + ",").Contains("," + item.Value + ","))
                    {
                        tempproductidstring += item.Value + ",";
                    }
                }
                else
                {
                    if (("," + hd_ProductsInfo.Value + ",").Contains("," + item.Value + ","))
                    {
                        tempproductidstring = tempproductidstring.Replace("," + item.Value + ",", ",");
                    }
                }
            }
            if (!tempproductidstring.Equals(",,"))
            {
                tempproductidstring = tempproductidstring.Replace(",,", "");
                tempproductidstring = tempproductidstring.StartsWith(",")
                    ? tempproductidstring.Substring(1)
                    : tempproductidstring;
                tempproductidstring = tempproductidstring.EndsWith(",")
                    ? tempproductidstring.Substring(0, tempproductidstring.Length - 1)
                    : tempproductidstring;
                MTJ_Integral mod = btjIntegral.GetList(int.Parse(hd_ITGRID.Value));
                mod.ProductsInfo = tempproductidstring;
                mod.ProductsInfoInChinese = getproductnamebyidstring(tempproductidstring);
                btjIntegral.Modify(mod);
            }
        }

        if (!string.IsNullOrEmpty(Request.QueryString["flag"]) && Convert.ToInt32(Request.QueryString["flag"]) == 1)
        {

            DataTable dt = db.GetAuthorProductInfo(GetCookieCompID(), hd_agentid.Value);
            string tempstring = "";
            
            foreach (DataRow item in dt.Rows)
            {
                //item.Selected = true;
                tempstring += "," + item[1].ToString();
            }
            string AllowProduct = tempstring.StartsWith(",") ? tempstring.Substring(1) : tempstring;            
            _tab.ExecuteNonQuery("update [TJMarketingSystemYin].[dbo].[TJ_RegisterCompanys] set AllowProduct='" + AllowProduct + "' where [CompID]=" + Convert.ToInt32(hd_agentid.Value), null);
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true); 
    }

    private string getproductnamebyidstring(string proidstring)
    {
        TabExecutewuliu tab = new TabExecutewuliu();
        DataTable dt = tab.ExecuteNonQuery("select Products_Name from TB_Products_Infor where Infor_ID in(" + proidstring + ")");
        string tempstring = "";
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (string.IsNullOrEmpty(tempstring))
                {
                    tempstring = row[0].ToString();
                }
                else
                {
                    tempstring += "," + row[0];
                }
            }
        }
        return tempstring;
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        FillCheckBoxList("Products_Name like '%"+inputSearchKeyword.Value+"%'");
    }
}
