using System;
using System.Data;
using System.Web.Configuration;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class Admin_TB_SmallBuJiangdelte : AuthorPage
{
    private BTB_SmallBuJiang bll = new BTB_SmallBuJiang();
    private MTB_SmallBuJiang mod = new MTB_SmallBuJiang();
    private BTJ_JXInfo bjxing = new BTJ_JXInfo();
    private MTB_SmalllabelInfo smod = new MTB_SmalllabelInfo();
    private readonly BTB_SmalllabelInfo bsmal = new BTB_SmalllabelInfo();
    private BTJ_ZJLabelCodesmallInfo bzjinfo = new BTJ_ZJLabelCodesmallInfo();

    private readonly SqlConnection sqlconn =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
            }
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim();
            }
            switch (HF_CMD.Value)
            {
                case "delete":
                    Button1.Text = "删除";
                    break;
                default:
                    break;
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CheckInput())
        {
            int str = deletelabelcede();
            if (str != 0)
            {
                Response.Write("<script>alert('删除成功！');window.opener.location.reload();window.close();</script>");
            }
            else
            {
                Response.Write("<script>alert('删除失败！');window.opener.location.reload();window.close();</script>");
            }

            //ClientScript.RegisterStartupScript(this.GetType(), "reload", "window.opener.location.reload();window.close();", true);
        }
    }


    private int deletelabelcede()
    {
        string startlabelcode = inputStartlabelcode.Value.Trim();
        string endlabelcode = inputEndlabelcode.Text.Trim();

        string sql = string.Empty;
        string rownum = string.Empty;
        string label = string.Empty;
        string flag = string.Empty;
        string sqlstr = string.Empty;
        string mm = string.Empty;
        string ID = string.Empty;

        #region 删除开始

        SqlCommand sqlcommand = new SqlCommand();
        sqlcommand.Connection = sqlconn;
        sqlcommand.CommandText = "update TB_SmalllabelInfo set ISBJflag=00000 where DYlabelcode>='" +
                                 inputStartlabelcode.Value.Trim() + "' and DYlabelcode<='" +
                                 inputEndlabelcode.Text.Trim();
        if (sqlconn.State != ConnectionState.Open)
        {
            sqlconn.Open();
        }
        sqlcommand.CommandTimeout = 3600;
        int s = sqlcommand.ExecuteNonQuery();
        sqlcommand.Dispose();
        if (s != 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }

        #endregion
    }

    private bool CheckInput()
    {
        if (inputStartlabelcode.Value.Trim().Length == 0)
        {
            Response.Write("<script>alert('开始号码不能为空！');</script>");
            return false;
        }
        if (inputEndlabelcode.Text.Trim().Length == 0)
        {
            Response.Write("<script>alert('结束不能为空！');</script>");
            return false;
        }

        return true;
    }

    protected void inputEndlabelcode_TextChanged(object sender, EventArgs e)
    {
        IList<MTB_SmalllabelInfo> mmsmal =
            bsmal.GetListsByFilterString("DYlabelcode>='" + inputStartlabelcode.Value.Trim() + "' and DYlabelcode<='" +
                                         inputEndlabelcode.Text.Trim() + "' and ISBJflag is null");
        if (mmsmal.Count > 0)
        {
            inputcount.Value = mmsmal.Count.ToString();
        }
    }
}