using System;
using System.Globalization;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using TJ.BLL;
using commonlib;
using System.Data.SqlClient;
using System.Data;
using org.in2bits.MyXls;

public partial class Admin_wuliu_Fahuo_FaHuoKuCunJXC : AuthorPage
{ 
    public BTJ_RegisterCompanys bagent = new BTJ_RegisterCompanys();
    public BTB_Products_Infor bpro = new BTB_Products_Infor(); 
    private readonly BTJ_User buser = new BTJ_User(); 
    private readonly CommonFunWL com = new CommonFunWL();

    private readonly SqlConnection sqlconcom =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());

    public commwl comm = new commwl();
    public string kucun = string.Empty;
    public string daohuo = string.Empty;
    public string fahuo = string.Empty;
    public string jieyu = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBox_RukuDateBegin.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            TextBox_RukuDateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
            FillDDL();
            if (GetCookieCompID() != "130")
            {
                Button1.Visible = false;
            }
        }
    }


    protected void Button_Search_Click(object sender, EventArgs e)
    {
        //IList<MTB_CompAgentInfo> mAgent = bbagent.GetListsByFilterString("AgentID=" + GetCookieCompID());
        //if (mAgent.Count > 0)
        //{
        //    int coid;
        //    foreach (MTB_CompAgentInfo ma in mAgent)
        //    {
        //        coid = ma.CompID;
        //        string gro = "group by fh.AgentID";
        //        string shstr = "select fh.AgentID,max(fh.CompID)as CompID, max(fh.FHDate) as FHDate,sum(fh.XiangNumber) as XiangNumber,max(pr.Products_Name) as Products_Name,max(st.StoreHouseName ) as StoreHouseName,max(fh.FHUserID) as FHUserID FROM [TB_FaHuoInfo_" + coid + "] fh,TB_Products_Infor pr,TB_StoreHouse st where fh.AgentID=" + GetCookieCompID() + " and fh.FHTypeID=3 and fh.ProID=pr.Infor_ID and fh.STID=st.STID";
        //        shstr += " and fh.FHDate between '" + TextBox_RukuDateBegin.Text + "' and '" + Convert.ToDateTime(TextBox_RukuDateEnd.Text)+ "'";
        //        shstr += gro;
        //        SqlDataAdapter sda1 = new SqlDataAdapter(shstr, sqlconcom);
        //        DataTable dttemp1 = new DataTable();
        //        sda1.Fill(dttemp1);
        //        GridViewSH.DataSource = dttemp1;
        //        GridViewSH.DataBind();
        //    }

        //} 
        string grouy = "order by kucun  desc";
        //string sqlstring = "SELECT fh.FHPiCi,fh.FHDate,fh.AgentID,sum(fh.XiangNumber) XiangNumber,pr.Products_Name,st.StoreHouseName,fh.FHUserID FROM [TB_FaHuoInfo_" + GetCookieCompID() + "] fh,TB_Products_Infor pr,TB_StoreHouse st where   fh.FHTypeID=3 and fh.ProID=pr.Infor_ID and fh.STID=st.STID";
        //string sqlstring = "select fh.AgentID,max(fh.FHDate) as FHDate,sum(fh.XiangNumber) as XiangNumber,max(pr.Products_Name) as Products_Name,max(st.StoreHouseName ) as StoreHouseName,max(fh.FHUserID) as FHUserID FROM [TB_FaHuoInfo_" + GetCookieCompID() + "] fh,TB_Products_Infor pr,TB_StoreHouse st where   fh.FHTypeID=3 and fh.ProID=pr.Infor_ID and fh.STID=st.STID";
        //sqlstring += " and fh.FHDate between '" + TextBox_RukuDateBegin.Text + "' and '" + Convert.ToDateTime(TextBox_RukuDateEnd.Text).AddDays(1)+ "'";
        string sqlstring =
            "select distinct d.AgentID,d.CompName,isnull(c.XiangNumber,0) as kucun ,isnull(c.ProID,0) as ProID from (SELECT a.AgentID,b.CompName  FROM[TianJianWuLiuWebnew].[dbo].[TB_CompAgentInfo] a,[TJMarketingSystemYin].[dbo].[TJ_RegisterCompanys] b,[TianJianWuLiuWebnew].[dbo].[TB_FaHuoInfo_" +
            GetCookieCompID() + " ]c where a.CompID=" + GetCookieCompID() +
            " and a.AgentID=b.CompID ) d left join(select AgentID, sum(XiangNumber) as XiangNumber,[ProID] from TB_FaHuoInfo_" +
            GetCookieCompID() + "  where FHDate<'" + Convert.ToDateTime(TextBox_RukuDateEnd.Text).AddDays(1) +
            "' and FHDate >='2016/01/01 0:00:00'   group by AgentID, [ProID]) c on d.AgentID=c.AgentID ";
        if (ComboBox_DaiLiShangID.SelectedValue != "0" && ComboBox_DaiLiShangID.SelectedValue != null)
        {
            sqlstring += " and d.AgentID=" + ComboBox_DaiLiShangID.SelectedValue + grouy;
        }
        else
        {
            sqlstring += grouy;
        }


        //if (ComboBox_ProInfo.SelectedValue != "0" && ComboBox_ProInfo.SelectedValue != null)
        //{
        //    sqlstring += " and fh.ProID=" + ComboBox_ProInfo.SelectedValue + grouy;
        //}
        //if (ComboBox_StoreHouse.SelectedValue != "0" && ComboBox_StoreHouse.SelectedValue != null)
        //{
        //    sqlstring += " and fh.STID=" + ComboBox_StoreHouse.SelectedValue + grouy;
        //}

        //kucun = comm.getDailishangkucun(GetCookieCompID(), agentid.Text, TextBox_RukuDateBegin.Text, TextBox_RukuDateEnd.Text, "kuncun");
        //daohuo = comm.getDailishangkucun(GetCookieCompID(), agentid.Text, TextBox_RukuDateBegin.Text, TextBox_RukuDateEnd.Text, "daohuo");
        //fahuo = comm.getDailishangkucun(GetCookieCompID(), agentid.Text, TextBox_RukuDateBegin.Text, TextBox_RukuDateEnd.Text, "fahuo");
        //jieyu = (int.Parse(kucun) + int.Parse(daohuo) - int.Parse(fahuo)).ToString();

        SqlDataAdapter sda = new SqlDataAdapter(sqlstring, sqlconcom);
        DataTable dttemp = new DataTable();
        sda.Fill(dttemp);

        GridView_RukuInfo.DataSource = dttemp;
        GridView_RukuInfo.DataBind();
    }

    protected void GridView_RukuInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }

    private void FillDDL()
    {
        string tempagentidstring = com.GetAgentIDStringByCompID(GetCookieCompID());
        if (tempagentidstring.Length > 0)
        {
            ComboBox_DaiLiShangID.DataSource = bagent.GetListsByFilterString("CompID in (" + tempagentidstring + ")");
            ComboBox_DaiLiShangID.DataBind();
        }

        //ComboBox_DaiLiShangID.DataSource = bagent.GetListsByFilterString("ParentID=" + GetCookieCompID() + " and CompTypeID=" + DAConfig.CompTypeIDJingXiaoShang);
        //ComboBox_DaiLiShangID.DataBind();
        //if(GetCookieCompTypeID()==DAConfig.CompTypeIDJingXiaoShang.ToString().Trim())
        //{
        //    CommonFunWL comwl = new CommonFunWL();
        //    ComboBox_ProInfo.DataSource = comwl.ReturnGetAgentAuthorProductInfo(GetCookieCompID());
        //}
        //else
        //{
        //    ComboBox_ProInfo.DataSource = bpro.GetListsByFilterString("CompID=" + GetCookieCompID());
        //}
        //ComboBox_ProInfo.DataBind();
        //ComboBox_StoreHouse.DataSource = bstorehouse.GetListsByFilterString("CompID="+GetCookieCompID());
        //ComboBox_StoreHouse.DataBind();
    }

    public int kcsy(int num, string agID)
    {
        int agnum =
            int.Parse(comm.getDailishangkucun(GetCookieCompID(), agID, TextBox_RukuDateBegin.Text,
                TextBox_RukuDateEnd.Text, ""));
        int synum = num - agnum;
        if (synum < 0)
        {
            return 0;
        }
        else
        {
            return synum;
        }
    }

    public string JiageLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            Session["starTime"] = TextBox_RukuDateBegin.Text;
            Session["endTime"] = TextBox_RukuDateEnd.Text;

            return string.Format("javascript:window.location.href='FaHuoDetial.aspx?Agent_id={0}'", ID);


            //return string.Format("javascript:var win=openWinCenter('FaHuoDetial.aspx?Agent_id={0}',900,600)", ID);
            //return string.Format("javascript: window.open('FaHuoDetial.aspx?Agent_id={0}','_blank','width=900,height=900')", ID);
            //return string.Format("javascript:window.location.href='FaHuoDetial.aspx?Agent_id={0}'", ID);
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
            //Label labelfhr =
            //labelfhr.Text = buser.GetList(int.Parse(labelfhr.Text)).LoginName;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label Label_jianshu_ft = (Label) e.Row.FindControl("Label_jianshu_ft");
            int jianshu = 0;
            foreach (GridViewRow gr in GridView_RukuInfo.Rows)
            {
                jianshu += Convert.ToInt32(((Label) gr.FindControl("Label_jianshu")).Text);
            }
            Label_jianshu_ft.Text = jianshu.ToString();
        }
    }

    protected void GridViewSH_RukuInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label labelfhs = (Label) e.Row.FindControl("LabelFHS");
            labelfhs.Text = buser.GetList(int.Parse(labelfhs.Text)).LoginName;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lallnum = (Label) e.Row.FindControl("LallNum");
            int jianshu = 0;
            foreach (GridViewRow gr in GridViewSH.Rows)
            {
                jianshu += Convert.ToInt32(((Label) gr.FindControl("LFhNum")).Text);
            }
            lallnum.Text = jianshu.ToString();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        DBClass db = new DBClass();
        //IList<MTB_CompAgentInfo> mAgent = bbagent.GetListsByFilterString("AgentID=" + GetCookieCompID());
        //if (mAgent.Count > 0)
        //{
        //    int coid;
        //    foreach (MTB_CompAgentInfo ma in mAgent)
        //    {
        //        coid = ma.CompID;
        //        string gro = "group by fh.AgentID";
        //        string shstr = "select fh.AgentID,max(fh.CompID)as CompID, max(fh.FHDate) as FHDate,sum(fh.XiangNumber) as XiangNumber,max(pr.Products_Name) as Products_Name,max(st.StoreHouseName ) as StoreHouseName,max(fh.FHUserID) as FHUserID FROM [TB_FaHuoInfo_" + coid + "] fh,TB_Products_Infor pr,TB_StoreHouse st where fh.AgentID=" + GetCookieCompID() + " and fh.FHTypeID=3 and fh.ProID=pr.Infor_ID and fh.STID=st.STID";
        //        shstr += " and fh.FHDate between '" + TextBox_RukuDateBegin.Text + "' and '" + Convert.ToDateTime(TextBox_RukuDateEnd.Text).AddDays(1) + "'";
        //        shstr += gro;
        //        SqlDataAdapter sda1 = new SqlDataAdapter(shstr, sqlconcom);
        //        DataTable dttemp1 = new DataTable();
        //        sda1.Fill(dttemp1);
        //        GridViewSH.DataSource = dttemp1;
        //        GridViewSH.DataBind();
        //    }

        //}


        //string grouy = "group by fh.AgentID,fh.ProID";
        ////string sqlstring = "SELECT fh.FHPiCi,fh.FHDate,fh.AgentID,sum(fh.XiangNumber) XiangNumber,pr.Products_Name,st.StoreHouseName,fh.FHUserID FROM [TB_FaHuoInfo_" + GetCookieCompID() + "] fh,TB_Products_Infor pr,TB_StoreHouse st where   fh.FHTypeID=3 and fh.ProID=pr.Infor_ID and fh.STID=st.STID";
        //string sqlstring = "select fh.AgentID,max(fh.FHDate) as FHDate,sum(fh.XiangNumber) as XiangNumber,max(pr.Products_Name) as Products_Name,max(st.StoreHouseName ) as StoreHouseName,max(fh.FHUserID) as FHUserID FROM [TB_FaHuoInfo_" + GetCookieCompID() + "] fh,TB_Products_Infor pr,TB_StoreHouse st where   fh.FHTypeID=3 and fh.ProID=pr.Infor_ID and fh.STID=st.STID";
        //sqlstring += " and fh.FHDate between '" + TextBox_RukuDateBegin.Text + "' and '" + Convert.ToDateTime(TextBox_RukuDateEnd.Text).AddDays(1) + "'";
        //if (ComboBox_DaiLiShangID.SelectedValue != "0" && ComboBox_DaiLiShangID.SelectedValue != null)
        //{
        //    sqlstring += " and fh.AgentID=" + ComboBox_DaiLiShangID.SelectedValue + grouy;
        //}
        //else
        //{
        //    sqlstring += grouy;
        //}


        ////if(ComboBox_ProInfo.SelectedValue!="0"&&ComboBox_ProInfo.SelectedValue!=null)
        ////{
        ////    sqlstring += " and fh.ProID=" + ComboBox_ProInfo.SelectedValue + grouy;
        ////}
        ////if (ComboBox_StoreHouse.SelectedValue != "0" && ComboBox_StoreHouse.SelectedValue != null)
        ////{
        ////    sqlstring += " and fh.STID=" + ComboBox_StoreHouse.SelectedValue + grouy;
        ////}

        //SqlDataAdapter sda = new SqlDataAdapter(sqlstring, sqlconcom);
        //DataTable dttemp = new DataTable();
        //sda.Fill(dttemp);

        // dt = dttemp;

        XlsDocument xls = new XlsDocument();
        xls.FileName = "发货信息表" + DateTime.Now.ToString("yyyyMMddHHmmssffff", DateTimeFormatInfo.InvariantInfo);
        xls.SummaryInformation.Author = "西凤375"; //填加xls文件作者信息  
        xls.SummaryInformation.NameOfCreatingApplication = "375"; //填加xls文件创建程序信息  
        xls.SummaryInformation.LastSavedBy = "西凤375"; //填加xls文件最后保存者信息  
        xls.SummaryInformation.Comments = "Comments"; //填加xls文件作者信息  
        xls.SummaryInformation.Title = "发货信息表"; //填加xls文件标题信息  
        //xls.SummaryInformation.Subject = "Subject";//填加文件主题信息  
        xls.DocumentSummaryInformation.Company = "西凤375"; //填加文件公司信息  


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
        Cell cell2 = cells.Add(1, 1, "代理商名称", cellXF);
        Cell cell3 = cells.Add(1, 2, "产品名称", cellXF);
        Cell cell4 = cells.Add(1, 3, "到查询截止时间开始库存", cellXF);
        Cell cell6 = cells.Add(1, 4, "发货数量", cellXF);
        Cell cell7 = cells.Add(1, 5, "代理商已发数量", cellXF);
        Cell cell8 = cells.Add(1, 6, "代理商库存量", cellXF);


        //cell.Font.FontFamily = FontFamilies.Roman; //字体  
        //cell.Font.Bold = true;  //字体为粗体    

        //}  

        #region 填充内容

        //XF dateStyle = xls.NewXF();
        //dateStyle.Format = "yyyy-mm-dd hh-mm-ss";
        if (GridView_RukuInfo.Rows.Count > 0)
        {
            DataTable dt = (DataTable) GridView_RukuInfo.DataSource;
            for (int i = 0; i < GridView_RukuInfo.Rows.Count; i++)
            {
                for (int j = 1; j < GridView_RukuInfo.Columns.Count; j++)
                {
                    //sheet.Cells.Add(i + 2, j + 1,dt.Rows[i][j].ToString());
                    int rowIndex = i + 2;
                    int colIndex = j;
                    //string dr = GridView1.Rows[i].Cells[j].ToString();
                    string agentname = ((Label) GridView_RukuInfo.Rows[i].Cells[j].FindControl("labelAgentID")).Text;
                    string proname = ((Label) GridView_RukuInfo.Rows[i].Cells[j].FindControl("Label2")).Text;
                    string fahuokucun = ((Label) GridView_RukuInfo.Rows[i].Cells[j].FindControl("LabYiFaHuokucun")).Text;
                    // string fhren = ((Label)GridView_RukuInfo.Rows[i].Cells[j].FindControl("LabelFHR")).Text;
                    string fhnum = ((Label) GridView_RukuInfo.Rows[i].Cells[j].FindControl("Label_jianshu")).Text;
                    string dlyfnum = ((Label) GridView_RukuInfo.Rows[i].Cells[j].FindControl("LabYiFaHuo")).Text;
                    string dlsnum = ((Label) GridView_RukuInfo.Rows[i].Cells[j].FindControl("Label1")).Text;

                    cells.Add(rowIndex, 1, agentname);
                    cells.Add(rowIndex, 2, proname);
                    cells.Add(rowIndex, 3, fahuokucun);
                    // cells.Add(rowIndex, 4, fhren);
                    cells.Add(rowIndex, 4, int.Parse(fhnum));
                    cells.Add(rowIndex, 5, int.Parse(dlyfnum));
                    cells.Add(rowIndex, 6, int.Parse(dlsnum));
                }
            }

            #endregion

            //Server.MapPath("~/DC");
            xls.Send();
            Response.Write("<script>alert('导出数据成功！')</script>");
        }
        else
        {
            Response.Write("");
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (ComboBox_DaiLiShangID.SelectedValue != "0" && ComboBox_DaiLiShangID.SelectedValue != null)
        {
            string compid = ComboBox_DaiLiShangID.SelectedValue;

            Session["starTime"] = TextBox_RukuDateBegin.Text;
            Session["endTime"] = TextBox_RukuDateEnd.Text;

            Response.Redirect("FaHuoDetial.aspx?Agent_id=" + compid);


            //string grouy = "group by fh.AgentID,fh.ProID";
            //string sqlstring = "select fh.AgentID,max(fh.FHDate) as FHDate,sum(fh.XiangNumber) as XiangNumber,max(pr.Products_Name) as Products_Name,max(st.StoreHouseName ) as StoreHouseName,max(fh.FHUserID) as FHUserID FROM [TB_FaHuoInfo_" + compid + "] fh,TB_Products_Infor pr,TB_StoreHouse st where   fh.FHTypeID=3 and fh.ProID=pr.Infor_ID and fh.STID=st.STID";
            //sqlstring += " and fh.FHDate between '" + TextBox_RukuDateBegin.Text + "' and '" + Convert.ToDateTime(TextBox_RukuDateEnd.Text).AddDays(1) + "'";

            //sqlstring += grouy;

            //SqlDataAdapter sda = new SqlDataAdapter(sqlstring, sqlconcom);
            //DataTable dttemp = new DataTable();
            //sda.Fill(dttemp);
            //GridView_RukuInfo.DataSource = dttemp;
            //GridView_RukuInfo.DataBind();
        }
    }
}