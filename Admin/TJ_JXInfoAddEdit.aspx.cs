using System;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Web.UI;

public partial class Admin_TJ_JXInfoAddEdit : AuthorPage
{
    private readonly BTJ_JXInfo bll = new BTJ_JXInfo();
    private readonly BTJ_LotteryActivity blotteryactivity = new BTJ_LotteryActivity();
    private MTJ_JXInfo mod = new MTJ_JXInfo();
    private DBClass db = new DBClass();

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
            DataBindLAID();
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

    private void DataBindLAID()
    {
        DropDownList1.DataSource = blotteryactivity.GetListsByFilterString("CompID=" + GetCookieCompID());
        DropDownList1.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        //mod.JxID = Convert.ToInt32(inputJxID.Value.Trim());
        mod.LAID = Convert.ToInt32(DropDownList1.SelectedItem.Value);
        mod.JxName = inputJxName.Value.Trim();
        if (DropDownList2.SelectedValue == "hb"|| DropDownList2.SelectedValue == "jf")
        {
            mod.JxContent = inputJxContent.Value.Trim();
        }
        else
        {
            mod.JxContent = "0";
        }


        mod.CJtime = DateTime.Now;
        mod.CompID = int.Parse(GetCookieCompID());
        if (TextBox_OrderNum.Text != "")
        {
            mod.OrderNum = int.Parse(TextBox_OrderNum.Text);
        }
        else
        {
            mod.OrderNum = 0;
        }
        mod.Remarks = inputRemarks.Text;
        mod.SJname = GetCookieUID();
        mod.DelFlag = DropDownList2.SelectedValue;

        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        //Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTJ_JXInfo ms = bll.GetList(id);
        //inputJxID.Value = ms.JxID.ToString().Trim();
        string la = ms.LAID.ToString().Trim();
        if (DropDownList1.Items.Contains(new ListItem(la)))
        {
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(la));
        }
        else
        {
            DropDownList1.SelectedIndex = 0;
        }
        inputJxName.Value = ms.JxName.Trim();
        inputJxContent.Value = ms.JxContent.Trim();
        inputRemarks.Text = ms.Remarks.Trim();
        TextBox_OrderNum.Text = ms.OrderNum.ToString();
        DropDownList2.SelectedValue = ms.DelFlag.Trim();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedValue == "kq")
        {
            inputJxContent.Visible = true;
            jpje.InnerText = "卡券ID";
        }
        if (DropDownList2.SelectedValue == "hb")
        {
            inputJxContent.Visible = true;
            jpje.InnerText = "红包金额";
        }
        if (DropDownList2.SelectedValue == "sw")
        {
            inputJxContent.Visible = false;
            jpje.Visible = false;
        }
    }
}