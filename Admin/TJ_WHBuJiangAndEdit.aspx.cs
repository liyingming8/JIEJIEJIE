using System;
using commonlib;
using TJ.DBUtility;
using System.Web.UI;
using System.Data;
using System.Text.RegularExpressions;


public partial class Admin_TJ_WHBuJiangAndEdit : AuthorPage
{
    TabExecute _tab = new TabExecute();
    TabExecutewuliu wuliu = new TabExecutewuliu();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
            }
            if (Request.QueryString["DanHao"] != null && !Request.QueryString["DanHao"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["DanHao"].Trim();
            }
            switch (HF_CMD.Value)
            {
                case "add":
                    FillComboBoxProduct();
                    FillComboBoxDanHao();
                    Button1.Text = "添加";
                    break;
                case "edit":
                    Button1.Text = "修改";
                    //FillComboBoxProduct();
                    fillinput(HF_ID.Value.Trim());
                    break;
                default:
                    break;
            }
        }
    }

    private void fillinput(string DanHao)
    {
        //string mSQL = "select top 1 * from TJ_BaseLabelCodeInfo_2019 where DanHao='" + DanHao+ "' and HBJE is not null and cast(HBJE as int)>0 and Compid="+GetCookieCompID();
        string mSQL = "select top 1 beizhu1 as DanHao,beizhu,Remarks,Remarks1 as Products_Name from TB_SmallBuJiang where Remarks1='" + DanHao + "' and Compid=" + GetCookieCompID();
        DataTable mDataTable=_tab.ExecuteNonQuery(mSQL);//ComboBox_DanHao
        if (mDataTable.Rows.Count>0)
        {
            ComboBox_DanHao.DataSource = mDataTable;
            ComboBox_DanHao.DataBind();
            ComboBox_DanHao.SelectedValue= mDataTable.Rows[0]["DanHao"].ToString();
            ComboBox_Product.DataSource = mDataTable;
            ComboBox_Product.DataBind();
            ComboBox_Product.SelectedValue = mDataTable.Rows[0]["Products_Name"].ToString();
            Jifen_Text.Value= mDataTable.Rows[0]["beizhu"].ToString();
            inputRemarks.Value = mDataTable.Rows[0]["Remarks"].ToString();
        }
    }

    protected void FillComboBoxProduct()
    {
        ComboBox_Product.DataSource = wuliu.ExecuteNonQuery("select Products_Name from TB_Products_Infor where CompID="+GetCookieCompID());
        ComboBox_Product.DataBind();
    }

    protected void FillComboBoxDanHao()
    {
        string mSQL = "select distinct DanHao from TJ_BaseLabelCodeInfo_2019 where DanHao is not null and len(DanHao)>0 and HBJE is null and Compid=" + GetCookieCompID();
        ComboBox_DanHao.DataSource = _tab.ExecuteNonQuery(mSQL);
        ComboBox_DanHao.DataBind();
    }
  
    protected void Button1_Click(object sender, EventArgs e)
    {
        string Remarks = inputRemarks.Value;
        if (CheckInput())
        {
            int DanHao=Convert.ToInt32(ComboBox_DanHao.SelectedValue);
            string mProductName = ComboBox_Product.SelectedValue;
            if (Jifen_Text.Value.Length>0) {
                if (IsInt(Jifen_Text.Value)) {
                    if (Convert.ToInt32(Jifen_Text.Value)<=100) {
                        int JiFen = Convert.ToInt32(Jifen_Text.Value);
                        string mSQL = "select * from TB_SmallBuJiang where beizhu1='" + DanHao + "' and Compid=" + GetCookieCompID()+ " and Remarks1='"+ mProductName + "'";
                        DataTable mDataTable = _tab.ExecuteNonQuery(mSQL);
                        if (mDataTable.Rows.Count > 0)
                        {
                            _tab.ExecuteNonQuery("update TB_SmallBuJiang set beizhu=cast(" + JiFen + " as int),BJDate=GETDATE(),Remarks='"+ Remarks + "' where beizhu1='" + DanHao + "' and compid=" + GetCookieCompID());
                            _tab.ExecuteNonQuery("update TJ_BaseLabelCodeInfo_2019 set HBJE=cast(" + JiFen + " as int) where DanHao='" + DanHao + "' and compid=" + GetCookieCompID());
                        }
                        else
                        {
                            string insertSQL = "insert into TB_SmallBuJiang(compid,UID,BJDate,Remarks,beizhu1,beizhu,Remarks1) values(" + GetCookieCompID() + "," + GetCookieUID() + ",GETDATE(),'" + inputRemarks.Value + "'," + DanHao + ",cast(" + JiFen + " as int),'"+ mProductName + "')";
                            _tab.ExecuteNonQuery(insertSQL);
                            _tab.ExecuteNonQuery("update TJ_BaseLabelCodeInfo_2019 set HBJE=cast(" + JiFen + " as int) where DanHao='" + DanHao + "' and compid=" + GetCookieCompID());
                        }
                    }else
                    {
                        MessageBox.Show(this, "积分值不能超过100！");
                    }
                } else
                {
                    MessageBox.Show(this, "积分值为正整数！");
                }
            }else
            {
                MessageBox.Show(this, "积分值不能为空！");
            }
            MessageBox.Show(this, "提交成功！");
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
    }

    private bool IsInt(string intstring)
    {
        return Regex.IsMatch(intstring, @"^[1-9]\d*$");
    }

    private bool CheckInput()
    {
        if (ComboBox_Product.SelectedValue == "0" || ComboBox_Product.SelectedValue == null)
        {
            Response.Write("<script>alert('请指定产品名称！');</script>");
            return false;
        }
        if (ComboBox_DanHao.SelectedValue == "0" || ComboBox_DanHao.SelectedValue == null)
        {
            Response.Write("<script>alert('请指定标签包编号！');</script>");
            return false;
        }
        return true;
    }
}