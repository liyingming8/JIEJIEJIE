using System;
using System.Text.RegularExpressions;
using TJ.DBUtility;
using commonlib;
using System.Data;
using System.Web.UI;

public partial class Admin_TJ_ProductSettingAndEdited : AuthorPage
{
    TabExecutewuliu wuliu = new TabExecutewuliu();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
            }
            if (Request.QueryString["Infor_ID"] != null && !Request.QueryString["Infor_ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["Infor_ID"].Trim();
            }
            if (Request.QueryString["Product_Code"] != null && !Request.QueryString["Product_Code"].Trim().Equals(""))
            {
                ProductCode.Value = Request.QueryString["Product_Code"].Trim();
            }
            if (Request.QueryString["Products_Name"] != null && !Request.QueryString["Products_Name"].Trim().Equals(""))
            {
                ProductsName.Value = Request.QueryString["Products_Name"].Trim();
            }
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    break;
                case "edit":
                    Button1.Text = "修改";
                    FillData(HF_ID.Value);
                    break;
                default:
                    break;
            }
        }
    }

    protected void FillData(string Infor_ID)
    {
        string mSQL = "select Product_Code,Products_Name,Remarks from TB_Products_Infor where Infor_ID=" + Infor_ID;
        DataTable mDataTable = wuliu.ExecuteNonQuery(mSQL);
        if (mDataTable.Rows.Count > 0)
        {
            Product_Code.Value = mDataTable.Rows[0][0].ToString();
            Product_Name.Value = mDataTable.Rows[0][1].ToString();
            inputRemarks.Value = mDataTable.Rows[0][2].ToString();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string product_code = Product_Code.Value;
        string product_name = Product_Name.Value;
        string Remarks = inputRemarks.Value;
        if (IsInt(product_code))
        {
            if (product_code.Length < 6 || product_code.Length > 8)
            {
                Response.Write("<script>alert('产品编码长度有误(4-8位)！');</script>");
            }
            else
            {
                if (product_name.Length > 20)
                {
                    Response.Write("<script>alert('产品名称过长！');</script>");
                }
                else
                {
                    if (product_name.Length == 0)
                    {
                        Response.Write("<script>alert('产品名称不能为空！');</script>");
                    }
                    else
                    {
                        if (HF_CMD.Value.Equals("add"))
                        {
                            //检查产品编码是否已存在
                            DataTable mProductCode = wuliu.ExecuteNonQuery("select count(*) as total from TB_Products_Infor where Product_Code='" + product_code + "' and CompID=" + GetCookieCompID());
                            if (Convert.ToInt32(mProductCode.Rows[0][0]) < 1)
                            {
                                //检查产品名称是否已存在
                                DataTable mProductsName = wuliu.ExecuteNonQuery("select count(*) as total from TB_Products_Infor where Products_Name='" + product_name + "' and CompID=" + GetCookieCompID());
                                if (Convert.ToInt32(mProductsName.Rows[0][0]) < 1)
                                {
                                    //新增记录
                                    int result = wuliu.ExecuteNonQuery("insert into TB_Products_Infor(Product_Code,Products_Name,Products_date,CompID,Remarks) values('" + product_code + "','" + product_name + "',getdate()," + GetCookieCompID() + ",'" + Remarks + "')", null);
                                    if (result == 1)
                                    {
                                        Response.Write("<script>alert('产品新增成功！');</script>");
                                        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('产品新增失败！');</script>");
                                        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
                                    }
                                }
                                else
                                {
                                    Response.Write("<script>alert('产品名称已存在！');</script>");
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('产品编码已存在！');</script>");
                            }
                        }
                        else if (HF_CMD.Value.Equals("edit"))
                        {
                            if (ProductCode.Value.Equals(product_code) && ProductsName.Value.Equals(product_name)) {
                                wuliu.ExecuteNonQuery("update TB_Products_Infor set Remarks='"+Remarks+ "' where Infor_ID="+ HF_ID.Value+ " and CompID="+GetCookieCompID());
                                Response.Write("<script>alert('更新成功！');</script>");
                                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
                            } else if (ProductCode.Value.Equals(product_code) && !ProductsName.Value.Equals(product_name))
                            {
                                //检查产品名称是否已存在
                                DataTable mProductsName = wuliu.ExecuteNonQuery("select count(*) as total from TB_Products_Infor where Products_Name='" + product_name + "' and CompID=" + GetCookieCompID());
                                if (Convert.ToInt32(mProductsName.Rows[0][0]) < 1)
                                {
                                    int updateResult = wuliu.ExecuteNonQuery("update TB_Products_Infor set Products_Name='" + product_name + "',Remarks='" + Remarks + "',Products_date=getdate() where Infor_ID=" + HF_ID.Value + " and CompID=" + GetCookieCompID(), null);
                                    if (updateResult == 1)
                                    {
                                        Response.Write("<script>alert('更新成功！');</script>");
                                        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('更新失败！');</script>");
                                        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
                                    }
                                }
                                else
                                {
                                    Response.Write("<script>alert('产品名称已存在！');</script>");
                                }
                            }else if (!ProductCode.Value.Equals(product_code) && ProductsName.Value.Equals(product_name))
                            {
                                //检查产品编号是否已存在
                                DataTable mProductsName = wuliu.ExecuteNonQuery("select count(*) as total from TB_Products_Infor where Product_Code='" + product_code + "' and CompID=" + GetCookieCompID());
                                if (Convert.ToInt32(mProductsName.Rows[0][0]) < 1)
                                {
                                    int updateResult = wuliu.ExecuteNonQuery("update TB_Products_Infor set Product_Code='" + product_code + "' ,Remarks='" + Remarks + "',Products_date=getdate() where Infor_ID=" + HF_ID.Value + " and CompID=" + GetCookieCompID(), null);
                                    if (updateResult == 1)
                                    {
                                        Response.Write("<script>alert('更新成功！');</script>");
                                        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('更新失败！');</script>");
                                        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
                                    }
                                }
                                else
                                {
                                    Response.Write("<script>alert('产品编号已存在！');</script>");
                                }
                            }
                            else if (!ProductCode.Value.Equals(product_code) && !ProductsName.Value.Equals(product_name))
                            {
                                //检查产品编码是否已存在
                                DataTable mProductCode = wuliu.ExecuteNonQuery("select count(*) as total from TB_Products_Infor where Product_Code='" + product_code + "' and CompID=" + GetCookieCompID());
                                if (Convert.ToInt32(mProductCode.Rows[0][0]) < 1)
                                {
                                    //检查产品名称是否已存在
                                    DataTable mProductsName = wuliu.ExecuteNonQuery("select count(*) as total from TB_Products_Infor where Products_Name='" + product_name + "' and CompID=" + GetCookieCompID());
                                    if (Convert.ToInt32(mProductsName.Rows[0][0]) < 1)
                                    {
                                        int updateResult = wuliu.ExecuteNonQuery("update TB_Products_Infor set Product_Code='" + product_code + "' ,Products_Name='" + product_name + "',Remarks='" + Remarks + "',Products_date=getdate() where Infor_ID=" + HF_ID.Value + " and CompID=" + GetCookieCompID(), null);
                                        if (updateResult == 1)
                                        {
                                            Response.Write("<script>alert('更新成功！');</script>");
                                            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
                                        }
                                        else
                                        {
                                            Response.Write("<script>alert('更新失败！');</script>");
                                            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
                                        }
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('产品名称已存在！');</script>");
                                    }
                                }
                                else
                                {
                                    Response.Write("<script>alert('产品编码已存在！');</script>");
                                }
                            }
                            else { }                        
                        }
                        else
                        { }
                    }
                }
            }
        }
        else
        {
            Response.Write("<script>alert('产品编码格式有误！');</script>");
        }
    }

    private bool IsInt(string intstring)
    {
        return Regex.IsMatch(intstring, @"^[0-9]*$");
    }
}