using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Data.SqlClient;

public partial class Admin_TB_LJjiangAdd : AuthorPage
{
    private readonly DBClass db = new DBClass();
    private readonly BTB_SmallBuJiang bll = new BTB_SmallBuJiang();
    private MTB_SmallBuJiang mod = new MTB_SmallBuJiang();
    private SqlConnection myConn;
    private readonly BTB_Products_Infor bproduct = new BTB_Products_Infor();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (GetCookieCompID() == "15793")
            {
                DropDownList2.Items.Add(new ListItem("裂变红包", "3"));
            }
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
            }
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim();
            }

            Dpro.DataSource = bproduct.GetListsByFilterString("CompID=" + GetCookieCompID());
            Dpro.DataBind();

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
            //if(int.Parse(inputnum.Value.Trim())<int.Parse(inputcount.Value.Trim()))
            //{
            if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
            {
                mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
            }

            mod.compid = Convert.ToInt32(GetCookieCompID());
            mod.UID = Convert.ToInt32(GetCookieUID());
            mod.Startlabelcode = inputStartlabelcode.Value.Trim();
            mod.Endlabelcode = inputEndlabelcode.Text.Trim();
            mod.JXID = Convert.ToInt32(DropDownList2.SelectedValue);
            mod.Remarks = inputJxContent.Value;
            mod.count = Convert.ToInt32(inputnum.Value.Trim());
            mod.BJDate = DateTime.Now;
            mod.Remarks1 = JzTime.Value.Trim();
            mod.beizhu = Convert.ToInt32(Dpro.SelectedValue);

            switch (HF_CMD.Value.Trim())
            {
                case "add":

                    //object DJPXID = bll.Insert(mod);
                    getlabelcede(mod, inputnum.Value.Trim());
                    break;
                case "edit":
                    bll.Modify(mod);
                    if (Session["Jname"] != null)
                    {
                        db.UpdateLJinfo(
                            "DYCode>='" + Session["star"] + "' and DYCode<='" + Session["end"] + "' and JpName='" +
                            Session["Jname"] + "' and  CompID=" + GetCookieCompID() + "",
                            "JXID=" + DropDownList2.SelectedValue + " ,JpName='" + inputJxContent.Value.Trim() +
                            "',JZTime='" + JzTime.Value.Trim() + "',ProID='" + Convert.ToInt32(Dpro.SelectedValue) +
                            "' ");
                        Response.Write("<script>alert('修改成功');window.location.href='TB_LJjiang.aspx'</script>");
                    }

                    break;
            }
            ClientScript.RegisterStartupScript(GetType(), "reload", "window.opener.location.reload();window.close();",
                true);
            //}
        }
    }

    private void fillinput(int id)
    {
        MTB_SmallBuJiang ms = bll.GetList(id);


        inputStartlabelcode.Value = ms.Startlabelcode.Trim();
        inputEndlabelcode.Text = ms.Endlabelcode.Trim();
        DropDownList2.SelectedValue = ms.JXID.ToString().Trim();
        if (DropDownList2.SelectedValue == "3")
        {
            renshu.Visible = true;
        }


        JzTime.Value = ms.Remarks1.Trim();

        inputnum.Value = ms.count.ToString().Trim();

        inputJxContent.Value = ms.Remarks.Trim();
        Session["Jname"] = ms.Remarks.Trim();
        Session["star"] = ms.Startlabelcode.Trim();
        Session["end"] = ms.Endlabelcode.Trim();
    }

    private void getlabelcede(MTB_SmallBuJiang mod, string count)
    {
        string sql = string.Empty;
        string jxid = DropDownList2.SelectedValue;
        string jxname = inputJxContent.Value.Trim();
        string jztime = JzTime.Value.Trim();
        string liebianrenshu = Text_renshu.Value.Trim();


        int kbj = int.Parse(inputcount.Value.Trim());
        int bj = int.Parse(count);

        int ys = 1;

        int tm = kbj/bj;

        if (tm == 1)
        {
            ys = 0;
        }

        SqlConnection myC = GetConnection();
        if (myC.State == ConnectionState.Closed)
        {
            myC.Open();
        }
        if (!string.IsNullOrEmpty(Text_renshu.Value.Trim()))
        {
            sql = "update TB_BuJiangLuJiu set JXID=" + jxid + " ,JpName='" + jxname + "',JZTime='" + jztime +
                  "',Remarkes='" + Text_renshu.Value.Trim() + "',ProID='" + Convert.ToInt32(Dpro.SelectedValue) +
                  "'   where ID in( select top " + bj +
                  " ID  from (select row_number() over (order by ID) as rowNum,* from TB_BuJiangLuJiu  where DYCode >='" +
                  inputStartlabelcode.Value.Trim() + "' and DYCode<='" + inputEndlabelcode.Text.Trim() +
                  "' and JXID=0 and CompID=" + GetCookieCompID() + " ) as t  where rowNum%" + tm + "=" + ys + ")";
        }
        else
        {
            sql = "update TB_BuJiangLuJiu set JXID=" + jxid + " ,JpName='" + jxname + "',JZTime='" + jztime +
                  "',ProID='" + Convert.ToInt32(Dpro.SelectedValue) + "'   where ID in( select top " + bj +
                  " ID  from (select row_number() over (order by ID) as rowNum,* from TB_BuJiangLuJiu  where DYCode >='" +
                  inputStartlabelcode.Value.Trim() + "' and DYCode<='" + inputEndlabelcode.Text.Trim() +
                  "' and JXID=0 and CompID=" + GetCookieCompID() + " ) as t  where rowNum%" + tm + "=" + ys + ")";
        }

        SqlCommand cmd = new SqlCommand(sql, myC);
        int c = cmd.ExecuteNonQuery();
        cmd.Dispose();
        myC.Close();

        if (c == bj)
        {
            bll.Insert(mod);

            Response.Write("<script>alert('布奖成功');window.location.href='TB_LJjiang.aspx'</script>");
        }
        else
        {
            Response.Write("<script>alert('布奖失败');window.location.href='TB_LJjiang.aspx'</script>");
        }
    }

    private bool CheckInput()
    {
        if (DropDownList2.SelectedValue == "0" || DropDownList2.SelectedValue == null)
        {
            Response.Write("<script>alert('请指定奖项！');</script>");
            return false;
        }
        if (GetCookieCompID() == "15793")
        {
            if (Text_renshu.Value.Trim().Length == 0)
            {
                Response.Write("<script>alert('裂变人数不能为空！！');</script>");
                return false;
            }
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

    protected void inputEndlabelcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dkbj =
                db.getLabelCodeLJ("DYCode >='" + inputStartlabelcode.Value.Trim() + "' and DYCode<='" +
                                  inputEndlabelcode.Text.Trim() + "' and JXID=0");

            //IList<MTB_SmalllabelInfo> mmsmal = bsmal.GetListsByFilterString("DYlabelcode>='" + inputStartlabelcode.Value.Trim().ToString() + "' and DYlabelcode<='" + inputEndlabelcode.Text.Trim().ToString() + "' and ISBJflag is null");

            inputcount.Value = dkbj.Rows.Count.ToString();
        }
        catch (Exception er)
        {
            Response.Write(er);
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedValue == "2")
        {
            inputJxContent.Visible = true;
            jpje.InnerText = "奖品";
        }
        if (DropDownList2.SelectedValue == "1")
        {
            inputJxContent.Visible = true;
            jpje.InnerText = "红包金额";
        }
        if (DropDownList2.SelectedValue == "0")
        {
            //inputJxContent.Visible = false;
            //jpje.Visible = false;
        }
        if (DropDownList2.SelectedValue == "3")
        {
            renshu.Visible = true;
        }
    }

    public SqlConnection GetConnection()
    {
        string str = ConfigurationManager.ConnectionStrings["SqlServerConnString"].ToString();
        myConn = new SqlConnection(str);
        return myConn;
    }
}