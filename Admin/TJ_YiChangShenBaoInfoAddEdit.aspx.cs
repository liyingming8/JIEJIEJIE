using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using TJ.Model;
using TJ.BLL;
using TJ.DBUtility;
using commonlib;
using Newtonsoft.Json.Linq;

public partial class Admin_TJ_YiChangShenBaoInfoAddEdit : AuthorPage
{
    BTJ_YiChangShenBaoInfo bll = new BTJ_YiChangShenBaoInfo();
    MTJ_YiChangShenBaoInfo mod = new MTJ_YiChangShenBaoInfo();
    BTJ_RegisterCompanys bcomp = new BTJ_RegisterCompanys();
    TabExecute tab = new TabExecute();
    TabExecutewuliu tabwl = new TabExecutewuliu();
    protected new void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["cmd"]))
            {
                HF_CMD.Value = Sc.DecryptQueryString(Request.QueryString["cmd"].Trim());
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                HF_ID.Value = Sc.DecryptQueryString(Request.QueryString["ID"].Trim());
            }
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    break;
                case "edit":
                    Button1.Text = "确定";
                    Fillinput(int.Parse(HF_ID.Value.Trim()));
                    break;
                default:
                    break;
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.iscorrect = ckb_iscorrect.Checked ? 1 : 0;
        mod.isconfirm = 1;
        //mod.confirmstr = inputconfirmstr.Value.Trim();
        mod.dealuserid = Convert.ToInt32(GetCookieUID());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_YiChangShenBaoInfoAddEdit.aspx", "TJ_YiChangShenBaoInfo", "描述", System.DateTime.Now, int.Parse(GetCookieUID()), "新增", ""));
                break;
            case "edit":
                bll.Modify(mod);
                RecordDealLog(new MTJ_DealLog(0, "TJ_YiChangShenBaoInfoAddEdit.aspx", "TJ_YiChangShenBaoInfo", "描述", System.DateTime.Now, int.Parse(GetCookieUID()), "修改", ""));
                break;
        } 
        if (ckb_iscorrect.Checked)
        {
            commwl comwl = new commwl();
            string blabeltabnm = comwl.GetBoxLabelAndTableName(mod.labelcode);
            string[] blabtabarray = blabeltabnm.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            DateTime tmsb = DateTime.Now;
            InternetHandle internet = new InternetHandle();
            string requeststring = "http://117.34.70.23:8888/update/fh/product_id/?userid=1&company=" +
                                   GetCookieCompID() + "&agentid=" + HF_AgentID.Value + "&key=" +
                                   CommonFun.Md5hash_String("fgfdvgu$#&3t@j"+ tmsb.ToString("yyyy-MM-dd HH:mm:ss")).ToLower() + "&time=" + tmsb.ToString("yyyy-MM-dd HH:mm:ss").Replace(" ", "%20") +"&productid=" + ddl_pid.SelectedValue + "&label=" + mod.labelcode;
            string temp = internet.GetUrlData(requeststring);
            JObject jo = JObject.Parse(temp); 
            string sql = "select ID,isexception from AgentAcceptInfo_2019 where BoxLabel='" + blabtabarray[0] + "'";
            DataTable dttemp = tabwl.ExecuteQuery(sql,null);
            if (dttemp.Rows.Count > 0)
            {
                if (dttemp.Rows[0]["isexception"].ToString().Equals("1")) //说明扫码了，没有积分
                {
                    tabwl.ExecuteNonQuery(
                        "update AgentAcceptInfo_2019 set isexception=0,isprized=0 where ID=" + dttemp.Rows[0]["ID"],
                        null);
                } 
            }
            else
            {
                if (!string.IsNullOrEmpty(HF_AgentID.Value) && !string.IsNullOrEmpty(HF_TerminalID.Value))
                {
                    temp = internet.GetUrlData(
                        "http://os.china315net.com/appajax/AgentAcceptHandleGet_V2.ashx?masterid=" + GetCookieCompID() +
                        "&parid=" + HF_AgentID.Value + "&lbcode=" + mod.labelcode + "&agid=" + HF_TerminalID.Value +
                        "&userid=" + mod.userid + "&pid=" + ddl_pid.SelectedValue);
                }
            }

            ClearSMinfo(blabtabarray[1], blabtabarray[0]);
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "reload", "closemyWindow();", true);
    }

    private void ClearSMinfo(string tablename, string boxLabel)
    {
         string fhkey =  tabwl.ExecuteQueryForSingleValue("select FHkey from " + tablename + "_FH where BoxLabel01='" + boxLabel + "'");
        if (!string.IsNullOrEmpty(fhkey))
        {
            string sqlstring = "select TableNameInfo from TB_FaHuoInfo_"+GetCookieCompID()+ " where FHKey='"+fhkey+"'";
            DataTable dttemp = tabwl.ExecuteQuery(sqlstring, null);
            if (dttemp.Rows.Count > 0)
            {
                string temptablenames = string.Empty;
                foreach (DataRow row in dttemp.Rows)
                {
                    temptablenames += "," + row[0];
                } 
                temptablenames = temptablenames.StartsWith(",") ? temptablenames.Substring(1) : temptablenames;
                string[] tablearray = temptablenames.Split(new []{','},StringSplitOptions.RemoveEmptyEntries);
                StringBuilder sb = new StringBuilder();
                foreach (string t in tablearray)
                {
                    sb.Append("union select BottleLabel from " + t +
                              "_BT where BoxLabel01 in (select BoxLabel01 from " + t + "_FH where FHKey='" + fhkey +
                              "')"); 
                } 
                sqlstring = sb.ToString();
                if (sqlstring.StartsWith("union"))
                {
                    sqlstring = sqlstring.Substring(5);
                } 
               dttemp = tabwl.ExecuteQuery(sqlstring, null);
                sb.Clear();
                foreach (DataRow row in dttemp.Rows)
                {
                    sb.Append("," + row[0]); 
                }
                string bottlelabelstring = sb.ToString().Substring(1);
            }
        }
    }

    private void Fillinput(int id)
    {
        MTJ_YiChangShenBaoInfo ms = bll.GetList(id);
        lab_sbr.Text = ms.uname.Trim();
        lab_labcode.Text = ms.labelcode;
        yichangimg.Src = "../"+ ms.imgurl;
        ckb_iscorrect.Checked = ms.iscorrect.Equals(1);
        lab_sbtm.Text = ms.sbtime.ToString();
        //目前西凤属于三级管理模式，可以直接用parentid替代AgentID
        HF_AgentID.Value = ms.parentid.ToString();
        if (ms.parentid > 0)
        {
            FillDDL(ms.parentid.ToString());
        } 
    }

    private void FillDDL(string agentid)
    {
        DBClass db = new DBClass();
        ddl_pid.DataSource = db.GetAuthorProductInfoForJSON(GetCookieCompID(), agentid);
        ddl_pid.DataBind();
    }
}