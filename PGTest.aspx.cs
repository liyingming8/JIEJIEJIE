using System;
using System.Data;
using System.Web.UI;
using commonlib;
using TJ.DBUtility;

public partial class PGTest : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //PGTabExecuteWuLiu pgTab = new PGTabExecuteWuLiu();
        //GridView1.DataSource = pgTab.ExecuteNonQuery("select * from delivery_summary  limit 100"); //delivery_summary
        //GridView1.DataBind();
    }
    TabExecute tab = new TabExecute();
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dt = tab.ExecuteQuery("select uname,phonenumber from TJ_SMS_Info where caseid=494", null);
        if (dt.Rows.Count > 0)
        {
            HuYi_Info.HY_dxinfoNoYzm("尊敬的" + dt.Rows[0]["uname"].ToString() + ",有新终端店注册信息,请及时审核!", dt.Rows[0]["phonenumber"].ToString(), "海南天鉴防伪科技");
            //HuYi_Info.HY_dxinfoNoYzm("尊敬的" + dt.Rows[0]["uname"].ToString() + ",有新终端店注册信息,请及时审核!", "15126099002", "天鉴科技");
        }
        dt.Dispose(); 
    } 
    protected void Button_Decode_Click(object sender, EventArgs e)
    {
        Security sc = new Security();
        Label_Result.Text = sc.Decrypt_TJ(TextBox_ToDecode.Text);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Security sc = new Security();
        Label_ShowNM.Text =  sc.GetShowNumFromEncryptV2(TextBox_ToDecode.Text);
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Security sc = new Security();
        Label_OriNum.Text = sc.ReturnShowNumToOriNum(Label_ShowNM.Text);
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        Label_md5.Text = CommonFun.Md5hash_String("fgfdvgu$#&3t@j" + ("2019-05-26 19:01:23"));
    }
}