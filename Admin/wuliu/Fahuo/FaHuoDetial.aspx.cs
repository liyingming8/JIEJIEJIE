using System;
using System.Globalization;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using TJ.BLL;
using commonlib;
using System.Data.SqlClient;
using System.Data;
using org.in2bits.MyXls;

public partial class Admin_wuliu_Fahuo_FaHuoDetial : AuthorPage
{
    public BTJ_RegisterCompanys bagent = new BTJ_RegisterCompanys();
    private BTB_Products_Infor bpro = new BTB_Products_Infor();
    private BTB_StoreHouse bstorehouse = new BTB_StoreHouse();
    private BTB_ProductAuthorForAgent bproductforagent = new BTB_ProductAuthorForAgent();
    private readonly BTJ_User buser = new BTJ_User();
    private readonly DBClass db = new DBClass();
    private readonly CommonFunWL com = new CommonFunWL();
    public commwl comm = new commwl();

    private readonly SqlConnection sqlconcom =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Agent_id"] != null && Request.QueryString["Agent_id"].Trim().Length > 0)
            {
                HF_AgentID.Value = Request.QueryString["Agent_id"].Trim();


                FillDDL();
            }
        }
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
        //string sqlstring = "SELECT fh.FHPiCi,fh.FHDate,fh.AgentID,fh.XiangNumber,pr.Products_Name,st.StoreHouseName,fh.FHUserID FROM [TB_FaHuoInfo_" + GetCookieCompID() + "] fh,TB_Products_Infor pr,TB_StoreHouse st where   fh.FHTypeID=3 and fh.ProID=pr.Infor_ID and fh.STID=st.STID";
        //sqlstring += " and fh.FHDate between '" + TextBox_RukuDateBegin.Text + "' and '" + Convert.ToDateTime(TextBox_RukuDateEnd.Text).AddDays(1) + "'";
        //if(ComboBox_DaiLiShangID.SelectedValue!="0"&&ComboBox_DaiLiShangID.SelectedValue!=null)
        //{
        //    sqlstring += " and fh.AgentID=" + ComboBox_DaiLiShangID.SelectedValue;
        //}
        //if(ComboBox_ProInfo.SelectedValue!="0"&&ComboBox_ProInfo.SelectedValue!=null)
        //{
        //    sqlstring += " and fh.ProID=" + ComboBox_ProInfo.SelectedValue;
        //}
        //if (ComboBox_StoreHouse.SelectedValue != "0" && ComboBox_StoreHouse.SelectedValue != null)
        //{
        //    sqlstring += " and fh.STID=" + ComboBox_StoreHouse.SelectedValue;
        //}
        //SqlDataAdapter sda = new SqlDataAdapter(sqlstring,sqlconcom);
        //DataTable dttemp =  new DataTable();
        //sda.Fill(dttemp);
        //GridView_RukuInfo.DataSource = dttemp;
        //GridView_RukuInfo.DataBind();

        if (db.SelectFhTable(HF_AgentID.Value).Rows[0][0].ToString() == "1")
        {
            string sqlstring =
                "SELECT fh.FHPiCi,fh.FHDate,fh.AgentID,fh.CompID,fh.XiangNumber,pr.Products_Name,st.StoreHouseName,fh.FHUserID FROM [TB_FaHuoInfo_" +
                HF_AgentID.Value +
                "] fh,TB_Products_Infor pr,TB_StoreHouse st where   fh.FHTypeID=3 and fh.ProID=pr.Infor_ID and fh.STID=st.STID and fh.FHDate>= '" +
                TextBox_RukuDateBegin.Text + "' and fh.FHDate< '" +
                Convert.ToDateTime(TextBox_RukuDateEnd.Text).AddDays(1) + "'";

            if (ComboBox_DaiLiShangID.SelectedValue != "0" && ComboBox_DaiLiShangID.SelectedValue != null)
            {
                sqlstring += " and fh.AgentID=" + ComboBox_DaiLiShangID.SelectedValue;
            }
            //sqlstring += " and fh.FHDate between '" + TextBox_RukuDateBegin.Text + "' and '" + Convert.ToDateTime(TextBox_RukuDateEnd.Text).AddDays(1) + "'";
            //if (ComboBox_DaiLiShangID.SelectedValue != "0" && ComboBox_DaiLiShangID.SelectedValue != null)
            //{
            //    sqlstring += " and fh.AgentID=" + ComboBox_DaiLiShangID.SelectedValue;
            //}
            //if (ComboBox_ProInfo.SelectedValue != "0" && ComboBox_ProInfo.SelectedValue != null)
            //{
            //    sqlstring += " and fh.ProID=" + ComboBox_ProInfo.SelectedValue;
            //}
            //if (ComboBox_StoreHouse.SelectedValue != "0" && ComboBox_StoreHouse.SelectedValue != null)
            //{
            //    sqlstring += " and fh.STID=" + ComboBox_StoreHouse.SelectedValue;
            //}
            SqlDataAdapter sda = new SqlDataAdapter(sqlstring, sqlconcom);
            DataTable dttemp = new DataTable();
            sda.Fill(dttemp);
            GridView_RukuInfo.DataSource = dttemp;
            GridView_RukuInfo.DataBind();
        }
        else
        {
            GridView_RukuInfo.DataSource = new DataTable();
            GridView_RukuInfo.DataBind();
        }
    }

    protected void GridView_RukuInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }

    private void FillDDL()
    {
        DataTable dttemp1 = new DataTable();

        string tempagentidstring = com.GetAgentIDStringByCompID(HF_AgentID.Value);
        if (tempagentidstring.Length > 0)
        {
            ComboBox_DaiLiShangID.DataSource = bagent.GetListsByFilterString("CompID in (" + tempagentidstring + ")");
            ComboBox_DaiLiShangID.DataBind();
        }

        if (Session["starTime"] != null)
        {
            string star = Session["starTime"].ToString();
            string end = Session["endTime"].ToString();

            TextBox_RukuDateBegin.Text = star;
            TextBox_RukuDateEnd.Text = end;

            if (db.SelectFhTable(HF_AgentID.Value).Rows[0][0].ToString() == "1")
            {
                string sqlstring =
                    "SELECT fh.FHPiCi,fh.FHDate,fh.AgentID,fh.CompID,fh.XiangNumber,pr.Products_Name,st.StoreHouseName,fh.FHUserID FROM [TB_FaHuoInfo_" +
                    HF_AgentID.Value +
                    "] fh,TB_Products_Infor pr,TB_StoreHouse st where   fh.FHTypeID=3 and fh.ProID=pr.Infor_ID and fh.STID=st.STID and fh.FHDate>='" +
                    star + "' and fh.FHDate<'" + Convert.ToDateTime(end).AddDays(1) + "'";

                if (ComboBox_DaiLiShangID.SelectedValue != "0" && ComboBox_DaiLiShangID.SelectedValue != null)
                {
                    sqlstring += " and fh.AgentID=" + ComboBox_DaiLiShangID.SelectedValue;
                }
                //sqlstring += " and fh.FHDate between '" + TextBox_RukuDateBegin.Text + "' and '" + Convert.ToDateTime(TextBox_RukuDateEnd.Text).AddDays(1) + "'";
                //if (ComboBox_DaiLiShangID.SelectedValue != "0" && ComboBox_DaiLiShangID.SelectedValue != null)
                //{
                //    sqlstring += " and fh.AgentID=" + ComboBox_DaiLiShangID.SelectedValue;
                //}
                //if (ComboBox_ProInfo.SelectedValue != "0" && ComboBox_ProInfo.SelectedValue != null)
                //{
                //    sqlstring += " and fh.ProID=" + ComboBox_ProInfo.SelectedValue;
                //}
                //if (ComboBox_StoreHouse.SelectedValue != "0" && ComboBox_StoreHouse.SelectedValue != null)
                //{
                //    sqlstring += " and fh.STID=" + ComboBox_StoreHouse.SelectedValue;
                //}
                SqlDataAdapter sda = new SqlDataAdapter(sqlstring, sqlconcom);
                DataTable dttemp = new DataTable();
                sda.Fill(dttemp);
                GridView_RukuInfo.DataSource = dttemp;
                GridView_RukuInfo.DataBind();
            }
            else
            {
                GridView_RukuInfo.DataSource = dttemp1;
                GridView_RukuInfo.DataBind();
            }
        }
        else
        {
            TextBox_RukuDateBegin.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            TextBox_RukuDateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    public string JiageLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            Session["starTime"] = TextBox_RukuDateBegin.Text;
            Session["endTime"] = TextBox_RukuDateEnd.Text;
            //return string.Format("javascript:var win=openWinCenter('FaHuoDetial.aspx?Agent_id={0}',900,600)", ID);
            return string.Format("javascript:window.location.href='FaHuoDetial.aspx?Agent_id={0}'", ID);
        }
        else
        {
            return "";
        }
    }

    protected void GridView_RukuInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label labelfhr = (Label) e.Row.FindControl("Label6");
            labelfhr.Text = buser.GetList(int.Parse(labelfhr.Text)).LoginName;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label Label_jianshu_ft = (Label) e.Row.FindControl("Label_jianshu_ft");
            int jianshu = 0;
            foreach (GridViewRow gr in GridView_RukuInfo.Rows)
            {
                jianshu += Convert.ToInt32(((Label) gr.FindControl("Label7")).Text);
            }
            Label_jianshu_ft.Text = jianshu.ToString();
        }
    }

    protected void BtnDC_Click(object sender, EventArgs e)
    {
        XlsDocument xls = new XlsDocument();

        xls.FileName = bagent.GetList(int.Parse(HF_AgentID.Value)).CompName + "发货信息" +
                       DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);

        Worksheet sheet = xls.Workbook.Worksheets.Add("Sheet1"); //状态栏标题名称  
        ColumnInfo cinfo = new ColumnInfo(xls, sheet);

        cinfo.Collapsed = true;

        //设置列的范围 如 0列-10列

        cinfo.ColumnIndexStart = 0; //列开始

        cinfo.ColumnIndexEnd = 8; //列结束

        cinfo.Collapsed = true;
        cinfo.Width = 80*60;
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

        //Cell cell1 = cells.Add(1, 1, "编号");
        Cell cell2 = cells.Add(1, 1, "批次", cellXF);
        Cell cell3 = cells.Add(1, 2, "发货日期", cellXF);
        Cell cell4 = cells.Add(1, 3, "发货流向", cellXF);
        Cell cell5 = cells.Add(1, 4, "产品", cellXF);
        Cell cell6 = cells.Add(1, 5, "库房", cellXF);
        Cell cell7 = cells.Add(1, 6, "发货人", cellXF);
        Cell cell8 = cells.Add(1, 7, "件数", cellXF);
        string drValue = "";


        if (GridView_RukuInfo.Rows.Count > 0)
        {
            XF dateStyle = xls.NewXF();
            dateStyle.Format = "yyyy-mm-dd";

            for (int i = 0; i < GridView_RukuInfo.Rows.Count; i++)
            {
                for (int j = 0; j < GridView_RukuInfo.Columns.Count - 1; j++)
                {
                    int rowIndex = i + 2;
                    int colIndex = j + 1;


                    drValue = ((Label) GridView_RukuInfo.Rows[i].Cells[j].FindControl("Label" + colIndex)).Text;
                    if (j == 6)
                    {
                        cells.Add(rowIndex, colIndex, int.Parse(drValue));
                    }
                    else
                    {
                        cells.Add(rowIndex, colIndex, drValue);
                    }
                }
            }
            xls.Send();
        }
    }
}