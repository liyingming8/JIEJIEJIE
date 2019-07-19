using System;
using System.Collections.Generic;
using commonlib;
using System.Data;
using TJ.Model;
using TJ.BLL;

public partial class TJ_duijangJG : AuthorPage
{
    private readonly DBClass db = new DBClass();
    private readonly BTJ_RegisterCompanys bcom = new BTJ_RegisterCompanys();
    private readonly BTB_CompAgentInfo bcomagt = new BTB_CompAgentInfo();

    protected void BtnSearch2_Click(object sender, EventArgs e)
    {
        DataTable dt;

        string Phone = inputSearchKeyword.Value.Trim();
        string YZcode = inputSearchKeyword0.Value.Trim();

        if (inputSearchKeyword.Value.Trim().Equals("") || inputSearchKeyword0.Value.Trim().Equals(""))
        {
            MessageBox.Show(Page, "手机号或验证码不能为空");
        } 
        dt = db.GetDuiJiangJGXQ(Phone, YZcode);
        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        string Phone = inputSearchKeyword.Value.Trim();
        string YZcode = inputSearchKeyword0.Value.Trim();
        IList<MTB_CompAgentInfo> mcom = bcomagt.GetListsByFilterString("AgentID=" + GetCookieCompID());
        if (mcom.Count > 0)
        {
            if (
                db.updateDjFlagJG(Phone, YZcode, GetCookieCompID(), bcom.GetList(int.Parse(GetCookieCompID())).CompName,
                    mcom[0].CompID.ToString()) > 0)
            {
                MessageBox.Show(Page, "兑奖成功");
            }
            else
            {
                MessageBox.Show(Page, "已领奖或手机号码无效");
            }
        }
        else
        {
            if (
                db.updateDjFlagJG(Phone, YZcode, GetCookieCompID(), bcom.GetList(int.Parse(GetCookieCompID())).CompName,
                    "0") > 0)
            {
                MessageBox.Show(Page, "兑奖成功");
            }
            else
            {
                MessageBox.Show(Page, "已领奖或手机号码无效");
            }
        }

        DataTable dt = new DataTable();
        dt = db.GetDuiJiangJGXQ(Phone, YZcode);
        gv.DataSource = dt;
        gv.DataBind();
    }
}