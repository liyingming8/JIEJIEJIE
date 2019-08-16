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
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim();
            }
            switch (HF_CMD.Value)
            {
                case "add":
                    FillComboBoxProduct();
                    Button1.Text = "添加";                  
                    break;
                case "edit":
                    Button1.Text = "修改";
                    PackCodeEdit.Disabled = true;
                    ComboBox_Product.Enabled = false;
                    fillinput(HF_ID.Value.Trim());
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["uid"]))
            {
                PackCodeEdit.Value= Request.QueryString["BaoBianHao"];
            }
            PackCodeEdit.Attributes.Add("onclick", XiangXiLinkString());
        }
    }

    protected string XiangXiLinkString()
    {
        string hh = Request.Url.PathAndQuery;
        return "openWinCenter('TJ_WHBuJiangPackCodeAndEdit.aspx?r=" +
                   Sc.EncryptQueryString(Request.Url.PathAndQuery) + "',500, 360, '标签包编码 ')";
    }

    private void fillinput(string ID)
    {
        string mSQL = "select  beizhu1,BaoBianHao as DanHao,beizhu,Remarks,Remarks1 as Products_Name from TB_SmallBuJiang where ID=" + ID;
        DataTable mDataTable = _tab.ExecuteNonQuery(mSQL);
        if (mDataTable.Rows.Count > 0)
        {
            ComboBox_Product.DataSource = mDataTable;
            PackCodeEdit.Value = mDataTable.Rows[0]["DanHao"].ToString();
            ComboBox_Product.DataBind();
            ComboBox_Product.SelectedValue = mDataTable.Rows[0]["Products_Name"].ToString();
            Jifen_Text.Value = mDataTable.Rows[0]["beizhu"].ToString();
            inputRemarks.Value = mDataTable.Rows[0]["Remarks"].ToString();
            if (mDataTable.Rows[0]["beizhu1"].ToString().Equals("1"))
            {
                CheckBox_IsActive.Checked = true;
            }
            else
            {
                CheckBox_IsActive.Checked = false;
            }
        }
        mDataTable.Dispose();
    }

    protected void FillComboBoxProduct()
    {
        ComboBox_Product.DataSource = wuliu.ExecuteNonQuery("select Products_Name from TB_Products_Infor where CompID=" + GetCookieCompID());
        ComboBox_Product.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string Remarks = inputRemarks.Value;
        if (CheckInput())
        {
            string DanHao = PackCodeEdit.Value;
            string mProductName = ComboBox_Product.SelectedValue;
            if (Jifen_Text.Value.Length > 0)
            {
                if (IsInt(Jifen_Text.Value))
                {
                    if (Convert.ToInt32(Jifen_Text.Value) < 100)
                    {
                        int JiFen = Convert.ToInt32(Jifen_Text.Value);
                        string mSQL = "select count(ID) from TB_SmallBuJiang where BaoBianHao='" + DanHao + "' and Compid=" + GetCookieCompID();
                        int IsActive = 0;
                        if (CheckBox_IsActive.Checked)
                        {
                            IsActive = 1;
                        }
                        string rowCount = _tab.ExecuteQueryForSingleValue(mSQL);
                        if (int.Parse(rowCount) > 0)
                        {
                            _tab.ExecuteNonQuery("update TB_SmallBuJiang set beizhu=cast(" + JiFen + " as int),BJDate=GETDATE(),Remarks='" + Remarks + "',beizhu1="+ IsActive + " where BaoBianHao='" + DanHao + "' and compid=" + GetCookieCompID());
                            _tab.ExecuteNonQuery("update TJ_BaseLabelCodeInfo_2019 set HBJE=cast(" + JiFen + " as int) where DanHao='" + DanHao + "' and compid=" + GetCookieCompID());
                        }
                        else
                        {
                            string insertSQL = "insert into TB_SmallBuJiang(compid,UID,BJDate,Remarks,BaoBianHao,beizhu,Remarks1,beizhu1) values(" + GetCookieCompID() + "," + GetCookieUID() + ",GETDATE(),'" + inputRemarks.Value + "','" + DanHao + "',cast(" + JiFen + " as int),'" + mProductName + "',"+ IsActive + ")";
                            _tab.ExecuteNonQuery(insertSQL);
                            _tab.ExecuteNonQuery("update TJ_BaseLabelCodeInfo_2019 set HBJE=cast(" + JiFen + " as int) where DanHao='" + DanHao + "' and compid=" + GetCookieCompID());
                        }
                        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
                    }
                    else
                    {
                        MessageBox.Show(this, "积分值小于100！");
                    }
                }
                else
                {
                    MessageBox.Show(this, "积分值为正整数！");
                }
            }
            else
            {
                MessageBox.Show( this,"积分值不能为空！");
            }
        } 
    }

    private bool IsInt(string intstring)
    {
        return Regex.IsMatch(intstring, @"^[1-9]\d*$");
    }


    private bool CheckInput()
    {
        if (!(PackCodeEdit.Value.Length > 0))
        {
            MessageBox.Show(this, "请指定标签包编号！"); 
            return false;
        }
        if (ComboBox_Product.SelectedValue == "0" || ComboBox_Product.SelectedValue == null)
        {
            MessageBox.Show(this, "请指定产品名称！"); 
            return false;
        } 
        return true;
    }
}