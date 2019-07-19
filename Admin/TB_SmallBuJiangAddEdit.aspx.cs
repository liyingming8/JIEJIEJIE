using System;
using System.Data;
using System.Web.Configuration;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class Admin_TB_SmallBuJiangAddEdit : AuthorPage
{
    private readonly BTB_SmallBuJiang bll = new BTB_SmallBuJiang();
    private MTB_SmallBuJiang mod = new MTB_SmallBuJiang();
    private readonly BTJ_JXInfo bjxing = new BTJ_JXInfo();
    private MTB_SmalllabelInfo smod = new MTB_SmalllabelInfo();
    private readonly BTB_SmalllabelInfo bsmal = new BTB_SmalllabelInfo();
    private readonly BTJ_ZJLabelCodesmallInfo bzjinfo = new BTJ_ZJLabelCodesmallInfo();

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
            FillDDL();
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    break;
                case "edit":
                    Button1.Text = "修改";
                    fillinput(int.Parse(HF_ID.Value.Trim()));
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
            if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
            {
                mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
            }

            mod.compid = Convert.ToInt32(GetCookieCompID());
            mod.UID = Convert.ToInt32(GetCookieUID());
            mod.Startlabelcode = inputStartlabelcode.Value.Trim();
            mod.Endlabelcode = inputEndlabelcode.Text.Trim();
            mod.JXID = Convert.ToInt32(ComboBox_JX.SelectedValue);
            mod.count = Convert.ToInt32(inputnum.Value.Trim());
            mod.BJDate = DateTime.Now;
            mod.Remarks = inputRemarks.Value.Trim();
            //---wyz-20170921
            if (Convert.ToInt32(mod.Startlabelcode) - Convert.ToInt32(mod.Endlabelcode) > 0)
            {
                Response.Write("<script>alert('开始号码不能大于结束号码！');</script>");
                //ClientScript.RegisterStartupScript(GetType(), "reload", "window.opener.location.reload();window.close();",true);
                return;
            }

            switch (HF_CMD.Value.Trim())
            {
                case "add":
                    //===wyz==20170921================================================================================
                    IList<MTB_SmalllabelInfo> mmsmal =
                        bsmal.GetListsByFilterString("DYlabelcode>='" + inputStartlabelcode.Value.Trim() + "' and DYlabelcode<='" +
                                                     inputEndlabelcode.Text.Trim() + "' and ISBJflag is null");

                    int countnum = mmsmal.Count;
                    int num = 0;
                    //===wyz=20170921===加入判断是否为空，如果为空时默认为0，否则程序会报错
                    if (!inputnum.Value.Trim().Equals(""))
                    {
                        num = int.Parse(inputnum.Value.Trim());
                    }
                    if (num - countnum > 0)
                    {
                        Response.Write("<script>alert('实际有奖数量小于要布奖数量，请检查数据！');</script>");
                        return;
                    }
                    else
                    {
                    //==============================================================================================wyz^|
                        object DJPXID = bll.Insert(mod);
                        getlabelcede(DJPXID.ToString(), inputnum.Value.Trim());
                    }
                    break;
                case "edit":
                    bll.Modify(mod);
                    break;
            }

            Response.Write("<script>alert('操作成功！');</script>");
            ClientScript.RegisterStartupScript(GetType(), "reload", "window.opener.location.reload();window.close();", true);
        }
    }

    private void fillinput(int id)
    {
        MTB_SmallBuJiang ms = bll.GetList(id);


        inputStartlabelcode.Value = ms.Startlabelcode.Trim();
        inputEndlabelcode.Text = ms.Endlabelcode.Trim();
        ComboBox_JX.SelectedValue = ms.JXID.ToString().Trim();
        inputnum.Value = ms.count.ToString().Trim();

        inputRemarks.Value = ms.Remarks.Trim();
    }

    private void getlabelcede(string zjid, string count)
    {
        string sql = string.Empty;
        string rownum = string.Empty;
        string label = string.Empty;
        string flag = string.Empty;
        string mm = string.Empty;
        string ID = string.Empty;
        int countnum = 0;
        int tiaobu = 0;
        int index = 0;
        int i = 0;

        #region 布奖开始

        IList<MTB_SmalllabelInfo> mmsmal =
            bsmal.GetListsByFilterString("DYlabelcode>='" + inputStartlabelcode.Value.Trim() + "' and DYlabelcode<='" +
                                         inputEndlabelcode.Text.Trim() + "' and ISBJflag is null");

        sql =
            "select ROW_NUMBER() over(order by ID) as rownum,[DYlabelcode],ID ,[ISBJflag] from TB_SmalllabelInfo where DYlabelcode>='" +
            inputStartlabelcode.Value.Trim() + "' and DYlabelcode<='" + inputEndlabelcode.Text.Trim() +
            "' and ISBJflag is null";
        SqlDataAdapter sdafhinfo = new SqlDataAdapter(sql, sqlconn);
        DataTable dttemp = new DataTable();
        sdafhinfo.Fill(dttemp);

        DataTable dtallbottlelabelcode = new DataTable();
        dtallbottlelabelcode.Columns.Add("rownum");
        dtallbottlelabelcode.Columns.Add("labelcod");
        dtallbottlelabelcode.Columns.Add("isflag");
        countnum = mmsmal.Count;
        tiaobu = (int)(countnum / int.Parse(count));
        foreach (DataRow dr in dttemp.Rows)
        {
            ID = dr["ID"].ToString();
            rownum = dr["rownum"].ToString();
            label = dr["DYlabelcode"].ToString();
            flag = dr["ISBJflag"].ToString();
            if (i < int.Parse(count))
            {
                if (int.Parse(rownum) % tiaobu == 0)
                {
                    if (flag == "1")
                    {
                        index = int.Parse(ID) + 1;
                    }
                    else
                    {
                        index = int.Parse(ID);
                    }
                    i++;
                    if ((i) == int.Parse(count))
                    {
                        mm += (index.ToString());
                        break;
                    }
                    else
                    {
                        mm += (index + ",");
                    }
                }
            }
            else
            {
                break;
            }
        }
        IList<MTB_SmalllabelInfo> mmmsmal = bsmal.GetListsByFilterString("ID in (" + mm + ")");

        #endregion

        if (mmmsmal.Count > 0)
        {
            foreach (MTB_SmalllabelInfo ms in mmmsmal)
            {
                bzjinfo.Insert(new MTJ_ZJLabelCodesmallInfo(0, ms.labelcode, ms.DYlabelcode, 0,
                    int.Parse(ComboBox_JX.SelectedValue), ms.remarks, inputRemarks.Value, int.Parse(GetCookieCompID()),
                    int.Parse(zjid), DateTime.Now));
            }
            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.Connection = sqlconn;
            sqlcommand.CommandText = "update TB_SmalllabelInfo set ISBJflag=1 where ID in( " + mm + ")";
            if (sqlconn.State != ConnectionState.Open)
            {
                sqlconn.Open();
            }
            sqlcommand.CommandTimeout = 3600;
            sqlcommand.ExecuteNonQuery();
            sqlcommand.Dispose();
        }
    }

    private bool CheckInput()
    {
        if (ComboBox_JX.SelectedValue == "0" || ComboBox_JX.SelectedValue == null)
        {
            Response.Write("<script>alert('请指定奖项！');</script>");
            return false;
        }
        if (inputnum.Value.Trim().Length == 0)
        {
            Response.Write("<script>alert('设定数量不能为空！！');</script>");
            return false;
        }
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

    private void FillDDL()
    {
        ComboBox_JX.DataSource = bjxing.GetListsByFilterString("CompID=" + GetCookieCompID());
        ComboBox_JX.DataBind();
    }

    protected void inputEndlabelcode_TextChanged(object sender, EventArgs e)
    {
        int num = 0;
        //===wyz=20170921===加入判断是否为空，如果为空时默认为0，否则程序会报错
        if (!inputnum.Value.Trim().Equals(""))
        {
            num = int.Parse(inputnum.Value.Trim());
        }
        //=====================================================================
        IList<MTB_SmalllabelInfo> mmsmal = bsmal.GetListsByFilterString("DYlabelcode>='" + inputStartlabelcode.Value.Trim() + "' and DYlabelcode<='" +
                                         inputEndlabelcode.Text.Trim() + "' and ISBJflag is null");
        if (mmsmal.Count > 0 && num <= mmsmal.Count)
        {
            inputcount.Value = mmsmal.Count.ToString();
        }
        else
        {
            if (num > mmsmal.Count)
            {
                Response.Write("<script>alert('设定数量已经超过可布奖数量！')</script>");
                return;
            }
            else
            {
                Response.Write("<script>alert('数量为0，请输入完整的标签序号！')</script>");
                return;
            }
        }
    }
}