using System;
using System.Web;
using commonlib;
using Newtonsoft.Json.Linq;
using TJ.BLL;

public partial class commonswm_swmactive : System.Web.UI.Page
{
    DBClass db = new DBClass(); 
    Security sc = new Security();
    public string FromURL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["pid"]) &&!string.IsNullOrEmpty(Request.QueryString["openid"]))
            {
                hf_pid.Value = Request.QueryString["pid"];
                hf_openid.Value = Request.QueryString["openid"];
                FromURL = sc.EncryptQueryString(Request.Url.ToString());
                string compidanduserid = db.GetCompidBySwmOpenid(Request.QueryString["openid"]);
                if (compidanduserid.Equals(string.Empty)) //没有注册过
                {
                    Response.Redirect("register.aspx?openid=" + Request.QueryString["openid"]+"&pid="+ Request.QueryString["pid"]);
                }
                else
                {  
                    string[] temparray = compidanduserid.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                    hf_compid.Value = temparray[0];
                    hf_userid.Value = temparray[1];
                    isactive.Value = temparray[2];
                    if (isactive.Value.Equals("0")) //审核中
                    {
                        //显示您的资料正在审核中，请稍候，同时2秒检查一次是否被激活
                        //divchanpin.Visible = false;
                        ButtonActive.Visible = false;
                    }
                    else //审核通过
                    {
                        var btjRegister = new BTJ_RegisterCompanys();
                        //创建Cookie
                        var userID = new HttpCookie("TJOSUID");
                        userID.Value = sc.EncryptQueryString(hf_userid.Value);
                        userID.Expires.AddDays(1);
                        Response.Cookies.Add(userID);

                        var userName = new HttpCookie("TJOSUName");
                        userName.Value = sc.EncryptQueryString("三维码用户");
                        userName.Expires.AddDays(1);
                        Response.Cookies.Add(userName);

                        var tjUserRid = new HttpCookie("TJOSRID");
                        tjUserRid.Value = sc.EncryptQueryString("155");
                        tjUserRid.Expires.AddDays(1);
                        Response.Cookies.Add(tjUserRid);

                        var userUnid = new HttpCookie("TJOSCOMPID");
                        userUnid.Value = sc.EncryptQueryString(hf_compid.Value);
                        userUnid.Expires.AddDays(1);
                        Response.Cookies.Add(userUnid);

                        labelcompname.Text = btjRegister.GetList(int.Parse(temparray[0])).CompName; 
                        var internet = new InternetHandle();
                        string tempvalue = internet.GetUrlData("http://www.china315net.com:35224/zhpt/qr3d/pkg/info/?pid=" + hf_pid.Value);
                        JObject jObject = JObject.Parse(tempvalue);
                        int actcompid = Convert.ToInt32(jObject["active_company_id"].ToString());
                        hdmode.Value = jObject["mode"].ToString();
                        hdcount.Value = jObject["cnt"].ToString();
                        string temp = "编号:" + jObject["lbl_txt"] + "<br>数量:" + ((string.IsNullOrEmpty(jObject["cnt"].ToString()) || jObject["cnt"].ToString().Equals("0"))? "" : (jObject["cnt"] + "枚")) + "<br>出厂日期:" + (string.IsNullOrEmpty(jObject["tm"].ToString()) ? "" : Convert.ToDateTime(jObject["tm"].ToString()).ToString("yyyy-MM-dd"));
                        JArray jarray = JArray.Parse(jObject["code_from"].ToString());
                        Literal_discription.Text = temp;
                        if (jarray.Count > 0)
                        {
                            hdcpid.Value = jarray[0].ToString();
                            if (actcompid > 0) //已经激活
                            {
                                if (int.Parse(hf_compid.Value) == actcompid) //是自己的产品
                                {
                                    Response.Redirect("http://tjfnew.china315net.com/common/route.ashx?cpid=" + hdcpid.Value, true);
                                }
                                else //不是自己的产品
                                {
                                    ButtonActive.BackColor = System.Drawing.Color.Gray;
                                    ButtonActive.Enabled = false;
                                    ButtonActive.Text = "该三维码产品已激活";
                                    divout.Style["display"] = "block";
                                }
                            }
                            else //尚未激活
                            {
                                var btbProducts = new BTB_Products_Infor();
                                //ddl_product.DataSource = btbProducts.GetListsByFilterString("CompID=" + temparray[0]);
                                //ddl_product.DataSource = btbProducts.GetListsByFilterString("CompID=" + temparray[0]);
                                //ddl_product.DataBind();
                            }
                        }
                        else
                        {
                            ButtonActive.BackColor = System.Drawing.Color.Gray;
                            ButtonActive.Enabled = false;
                            ButtonActive.Text = "尚未找到该批次三维码";
                            divout.Style["display"] = "block";
                        }
                    } 
                }
            }
            else
            {
                Response.Write("<script>alert('请正确输入!');</script>");
                Response.End();
            }
        }
    } 

    protected void ButtonActive_Click(object sender, EventArgs e)
    { 
        var internet = new InternetHandle();
        string result = internet.GetUrlData("http://www.china315net.com:35224/zhpt/active/label/?pid="+hf_pid.Value+"&product_id=0&company_id="+hf_compid.Value+"&user_id="+hf_userid.Value);
        JObject jo = JObject.Parse(result);
        if (jo["e"].ToString().Trim().Length.Equals(0))
        {
            db.ActivePidAndAuthorAllMoudleInfoFrank(hf_compid.Value, hf_pid.Value, int.Parse(hdcount.Value), 60,labelcompname.Text);
            Response.Redirect("../Admin/TJ_Role_Package_Choose_Simple.aspx?cnt=" + hf_pid.Value + "|" + hdcount.Value + "|" + 0 + "|" + hdcpid.Value, true);
        } 
    } 
}