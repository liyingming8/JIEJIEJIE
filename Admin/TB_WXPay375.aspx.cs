using System;
using System.Data;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using org.in2bits.MyXls;

public partial class TB_WXPay375 : AuthorPage
{
    private BTJ_User bll = new BTJ_User();
    private MTJ_User mod = new MTJ_User();
    private BTJ_RoleInfo bllrole = new BTJ_RoleInfo();
    private BTJ_RegisterCompanys blrcompany = new BTJ_RegisterCompanys();
    private CommonFun comfun = new CommonFun();
    private CommonFunWL comwl = new CommonFunWL();
    public BTJ_User buser = new BTJ_User();

    private readonly DB375 db = new DB375();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgridview();
        }
    }

    private void fillgridview()
    {
        DataTable dpay = db.GetPayInfo(ReturnFilterStringNuser());
        Session.Add("dpay", dpay);
        GridView1.DataSource = dpay;
        GridView1.DataBind();
    }

    private string tempstring = "";

    private string ReturnFilterStringNuser()
    {
        tempstring += "PayOK='1'";
        if (inputSearchKeyword.Value.Length > 0)
        {
            tempstring += "and" + DDLField.SelectedValue + "='" + inputSearchKeyword.Value.Trim() + "'";
        }

        //tempstring += "order by PayOKTime";
        return tempstring;
    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgridview();
    }

    protected void gvwDesignationName_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // 得到该控件
        GridView theGrid = sender as GridView;
        int newPageIndex = 0;
        if (e.NewPageIndex == -3)
        {
            //点击了Go按钮
            TextBox txtNewPageIndex = null;

            //GridView较DataGrid提供了更多的API，获取分页块可以使用BottomPagerRow 或者TopPagerRow，当然还增加了HeaderRow和FooterRow
            GridViewRow pagerRow = theGrid.BottomPagerRow;

            if (pagerRow != null)
            {
                //得到text控件
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
            }
            if (txtNewPageIndex != null)
            {
                //得到索引
                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
            }
        }
        else
        {
            //点击了其他的按钮
            newPageIndex = e.NewPageIndex;
        }
        //防止新索引溢出
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;

        //得到新的值
        theGrid.PageIndex = newPageIndex;

        //重新绑定
        fillgridview();
    }


    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#CCFF83'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
    }

    public string ReturnCPInfo(string proid, string num)
    {
        string[] bid = proid.Split(',');
        string[] bnum = num.Split(',');
        string text = string.Empty;
        if (bid.Length > 1)
        {
            for (int i = 1; i < bid.Length; i++)
            {
                text += "\r\n" + db.GetProInfo("ProID=" + bid[i] + "").Rows[0]["ProName"] + " : " + bnum[i] + "\r\n";
            }
            return text;
        }
        else
        {
            text = "\r\n" + db.GetProInfo("ProID=" + bid[0] + "").Rows[0]["ProName"] + " : " + bnum[0];
            return text;
        }
    }

    public string ReturnUserName(string Userid)
    {
        DataTable duser = db.GetUserInfoByUid("ID=" + Userid + "");
        if (duser.Rows.Count > 0)
        {
            return duser.Rows[0]["NickName"].ToString();
        }
        else
        {
            return "";
        }
    }

    public string ReturnAdd(string Addid, string id)
    {
        DataTable add = db.GetAddInfo("ID=" + Addid + "");
        if (add.Rows.Count > 0)
        {
            if (id == "ad")
            {
                return add.Rows[0]["AD_all"].ToString();
            }
            else if (id == "ph")
            {
                return add.Rows[0]["Phone"].ToString();
            }
            else if (id == "nm")
            {
                return add.Rows[0]["Name"].ToString();
            }
            else
            {
                return add.Rows[0]["AD_bm"].ToString();
            }
        }
        else
        {
            return "";
        }
    }


    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
    }

    protected void BtnSearch0_Click1(object sender, EventArgs e)
    {
    }

    protected void BtDC_ServerClick(object sender, EventArgs e)
    {
        XlsDocument xls = new XlsDocument();

        xls.FileName = "订单统计";

        Worksheet sheet = xls.Workbook.Worksheets.Add("Sheet1"); //状态栏标题名称  
        ColumnInfo cinfo = new ColumnInfo(xls, sheet);

        cinfo.Collapsed = true;

        //设置列的范围 如 0列-10列

        cinfo.ColumnIndexStart = 1; //列开始

        cinfo.ColumnIndexEnd = 10; //列结束

        cinfo.Collapsed = true;
        cinfo.Width = 100*60;
        sheet.AddColumnInfo(cinfo);

        XF cellXF = xls.NewXF();

        cellXF.VerticalAlignment = VerticalAlignments.Centered;

        cellXF.HorizontalAlignment = HorizontalAlignments.Centered;

        cellXF.Font.Height = 18*12;

        //cellXF.Font.Bold = true;

        //cellXF.Pattern = 1;//设定单元格填充风格。如果设定为0，则是纯色填充
        cellXF.UseBackground = true;

        cellXF.PatternBackgroundColor = Colors.Red; //填充的背景底色

        cellXF.PatternColor = Colors.Red; //设定填充线条的颜色


        Cells cells = sheet.Cells;

        Cell cell1 = cells.Add(1, 1, "订购人", cellXF);
        Cell cell2 = cells.Add(1, 2, "产品", cellXF);
        Cell cell3 = cells.Add(1, 3, "数量", cellXF);
        Cell cell4 = cells.Add(1, 4, "收货人", cellXF);
        Cell cell5 = cells.Add(1, 5, "收货地址", cellXF);
        Cell cell6 = cells.Add(1, 6, "联系电话", cellXF);
        Cell cell7 = cells.Add(1, 7, "付款金额", cellXF);
        Cell cell8 = cells.Add(1, 8, "发货状态", cellXF);

        DataTable pay = (DataTable) Session["dpay"];

        int row = pay.Rows.Count; 
        int nj = 0;
        if (row > 0)
        {
            for (int i = 0; i < row; i++)
            {
                string[] proid = pay.Rows[i]["GoodsID"].ToString().Split(',');
                string[] num = pay.Rows[i]["GoodsNum"].ToString().Split(',');
                int cs = 0;
                if (proid.Length > 1)
                {
                    for (int cl = 0; cl < (proid.Length - 1); cl++)
                    {
                        nj++;
                        cs++;

                        //cells.Add(j + 1, 1, pay.Rows[i]["UserID"].ToString(), cellXF);
                        cells.Add(nj + 1, 2, db.GetProInfo("ProID=" + proid[cs] + "").Rows[0]["ProName"].ToString(),
                            cellXF);
                        cells.Add(nj + 1, 3, int.Parse(num[cs]), cellXF);
                        //cells.Add(j + 1, 4, pay.Rows[i]["AddID"].ToString(), cellXF);
                    }

                    if (cs > 1)
                    {
                        cells.Merge(nj - cs + 2, nj + 1, 1, 1);
                        cells.Merge(nj - cs + 2, nj + 1, 4, 4);
                        cells.Merge(nj - cs + 2, nj + 1, 5, 5);
                        cells.Merge(nj - cs + 2, nj + 1, 6, 6);
                        cells.Merge(nj - cs + 2, nj + 1, 7, 7);
                        cells.Merge(nj - cs + 2, nj + 1, 8, 8);
                    }
                    cells.Add(nj - cs + 2, 1, ReturnUserName(pay.Rows[i]["UserID"].ToString()), cellXF);
                    cells.Add(nj - cs + 2, 4, ReturnAdd(pay.Rows[i]["AddID"].ToString(), "nm"), cellXF);
                    cells.Add(nj - cs + 2, 5, ReturnAdd(pay.Rows[i]["AddID"].ToString(), "ad"), cellXF);
                    cells.Add(nj - cs + 2, 6, ReturnAdd(pay.Rows[i]["AddID"].ToString(), "ph"), cellXF);
                    cells.Add(nj - cs + 2, 7, pay.Rows[i]["GoodsPrice"].ToString(), cellXF);
                    cells.Add(nj - cs + 2, 8, pay.Rows[i]["WLFH_Flag"].ToString().Equals("1") ? "已发货" : "待发货", cellXF);
                }
                else
                {
                    cells.Add(nj + 2, 1, ReturnUserName(pay.Rows[i]["UserID"].ToString()), cellXF);
                    cells.Add(nj + 2, 2,
                        db.GetProInfo("ProID=" + pay.Rows[i]["GoodsID"] + "").Rows[0]["ProName"].ToString(), cellXF);
                    cells.Add(nj + 2, 3, int.Parse(pay.Rows[i]["GoodsNum"].ToString()), cellXF);
                    cells.Add(nj + 2, 4, ReturnAdd(pay.Rows[i]["AddID"].ToString(), "nm"), cellXF);
                    cells.Add(nj + 2, 5, ReturnAdd(pay.Rows[i]["AddID"].ToString(), "ad"), cellXF);
                    cells.Add(nj + 2, 6, ReturnAdd(pay.Rows[i]["AddID"].ToString(), "ph"), cellXF);
                    cells.Add(nj + 2, 7, pay.Rows[i]["GoodsPrice"].ToString(), cellXF);
                    cells.Add(nj + 2, 8, pay.Rows[i]["WLFH_Flag"].ToString().Equals("1") ? "已发货" : "待发货", cellXF);

                    nj++;
                }
            }
            xls.Send();
        }
        else
        {
            //xls.Send();
        }
    }
}