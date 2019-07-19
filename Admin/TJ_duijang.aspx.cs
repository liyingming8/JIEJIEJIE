using System;
using System.Web.UI;
using commonlib;
using System.Data;

public partial class TJ_duijang : Page
{
    private readonly DBClass db = new DBClass();

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
    }

    protected void BtnSearch2_Click(object sender, EventArgs e)
    {
        DataTable dt;

        string Phone = inputSearchKeyword.Value.Trim();
        string YZcode = inputSearchKeyword0.Value.Trim();

        if (inputSearchKeyword.Value.Trim().Equals("") || inputSearchKeyword0.Value.Trim().Equals(""))
        {
            MessageBox.Show(Page, "手机号或验证码不能为空");
        }


        dt = db.GetDuiJiang(Phone, YZcode);
        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        string Phone = inputSearchKeyword.Value.Trim();
        string YZcode = inputSearchKeyword0.Value.Trim();
        if (inputSearchKeyword.Value.Trim().Equals("") || inputSearchKeyword0.Value.Trim().Equals(""))
        {
            MessageBox.Show(Page, "手机号或验证码不能为空");
        }
        else
        {
            if (db.updateDjFlag(Phone, YZcode) > 0)
            {
                MessageBox.Show(Page, "兑奖成功");
            }
            else
            {
                MessageBox.Show(Page, "已领奖或手机号码无效");
            }
        }

        DataTable dt = new DataTable();
        dt = db.GetDuiJiang(Phone, YZcode);
        gv.DataSource = dt;
        gv.DataBind();
    }
}