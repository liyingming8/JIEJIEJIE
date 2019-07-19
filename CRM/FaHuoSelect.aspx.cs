using System.Text;
using commonlib;
using Newtonsoft.Json.Linq;
using System;

public partial class CRM_FaHuoSelect : AuthorPage
{
    public string cpid = string.Empty;
    InternetHandle internet = new InternetHandle();
    public string fasongtime = "";
    public string gettime = "";
    public string fasongagent = "";
    public string getagent = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Response.Write("<script>alert('" + GetCookieCompTypeID().ToString()+ "')</script>");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (inputSearchProduct.Text.Trim().Equals(""))
        {
            Response.Write("<script>alert('标签序号不能为空！')</script>");
            return;

        }
        cpid = inputSearchProduct.Text.Trim();
        try
        {
            string tempstring = "http://117.34.70.23:32176/wuliu/tjjs/label/" + cpid + "/";

            string temp = internet.GetUrlData(tempstring);

            JObject obj = JObject.Parse(temp);
            var obj1 = JArray.Parse(obj["delivery"].ToString()); 
            StringBuilder sb = new StringBuilder();
            foreach (var o in obj1)
            {
                sb.AppendLine("时间:<strong>" + o["tm"]+"</strong>");
                sb.Append("<br>");
                sb.AppendLine("【从】" + o["frcomp_name"] + "【发货至】" + o["tocomp_name"]);
                sb.Append("<br>");
            } 
            literalfahuoxinxi.Text = sb.ToString();
            //fasongtime = obj["delivery"][0][0].ToString().Replace("\r\n", "");
            //fasongagent = obj["delivery"][0][4].ToString().Replace("\r\n", "");
            //gettime = obj["delivery"][1][0].ToString().Replace("\r\n", "");
            //getagent = obj["delivery"][0][4].ToString().Replace("\r\n", "");
            //if (string.IsNullOrEmpty(fasongtime))
            //{
            //  //  nofahuo.Visible = true;
            //    fahuo.Visible = false;
            //    Response.Write("<script>alert('未查询到发货记录！')</script>");
            //    return;
            //}
            //else
            //{
            //    fahuo.Visible = true;
            //}


        }
        catch (Exception)
        {

            //fahuo.Visible = false;
            Response.Write("<script>alert('未查询到发货记录！')</script>");
            return;
        }



    }
}