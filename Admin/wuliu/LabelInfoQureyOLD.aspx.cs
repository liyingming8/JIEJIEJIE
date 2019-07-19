using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_wuliu_LabelInfoQureyOLD : AuthorPage
{
    private readonly SqlConnection sqlcon =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());

    public CommonFunWL com = new CommonFunWL();
    public commwl comm = new commwl();
    private string Tname = "";
    private readonly BTB_Products_Infor bproduct = new BTB_Products_Infor();
    private readonly BTB_StoreHouse bstorehouse = new BTB_StoreHouse();
    private readonly BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();
    private readonly BTJ_BaseClass bbase = new BTJ_BaseClass();
    private readonly DBClass db = new DBClass();
    public BTJ_User buser = new BTJ_User();
    public string qdcompid = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

      
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            qdcompid = GetCookieCompID();
            GridView_QueryResult.DataSource = null;
            GridView_QueryResult.DataBind();
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (TextBox_Label.Text.Trim().Equals("") || !TextBox_Label.Text.Trim().Length.Equals(12))
            {
                MessageBox.Show(Page, "该号码格式不正确,请重新输入!");
                TextBox_Label.Text = "";
            }
            else
            {
                Tname = "";
                string returnalertstring = "";
                //DataTable dt = com.QueryLabelInfoByLabelCode(TextBox_Label.Text.Trim(), ref Tname, ref returnalertstring);
                //if (dt.Rows.Count>0)
                //{
                //    GridView_QueryResult.DataSource = dt;
                //GridView_QueryResult.DataBind();
                DataTable dt = new DataTable();
                DataTable dbq = db.GetBQLabelTH("LabelCodeNew='" + TextBox_Label.Text.Trim() + "'");
                if (dbq.Rows.Count > 0)
                {
                    dt = com.QueryLabelInfoByLabelCode(dbq.Rows[0]["LabelCodeOld"].ToString(), ref Tname,
                        ref returnalertstring,GetCookieCompID());
                }
                else
                {
                    dt = com.QueryLabelInfoByLabelCode(TextBox_Label.Text.Trim(), ref Tname, ref returnalertstring,GetCookieCompID());
                }


                DataTable dth =
                    db.GetBQLabelTH("LabelCodeNew='" + TextBox_Label.Text.Trim() + "'or LabelCodeOld='" +
                                    TextBox_Label.Text.Trim() + "'");

                GridView2.DataSource = dth;
                GridView2.DataBind();

                if (dt.Rows.Count > 0)
                {
                    GridView_QueryResult.DataSource = dt;
                    GridView_QueryResult.DataBind();


                    if (Tname.Length > 3)
                    {
                        GridView1.DataSource = getBox(dt.Rows[0]["箱标1"].ToString(), Tname);
                        GridView1.DataBind();
                        Label3.Visible = true;
                        Label_fahuoInfo.Visible = true;
                        GridView_FaHuoXinXi.Visible = true;
                        string mm = dt.Rows[0]["箱标1"].ToString();
                        //GridView_FaHuoXinXi.DataSource = com.getFaHuoInfo(dt.Rows[0]["箱标1"].ToString(), Tname);
                        GridView_FaHuoXinXi.DataSource = com.getFaHuoInfoSumCompanyOnly(dt.Rows[0]["箱标1"].ToString(),
                            GetCookieCompID());
                        // GridView_FaHuoXinXi.DataSource = com.getFaHuoInfo(dt.Rows[0]["箱标1"].ToString());
                        GridView_FaHuoXinXi.DataBind();
                    }
                    else
                    {
                        Label3.Visible = false;
                    }
                }
                else if (returnalertstring.Length > 0)
                {
                    MessageBox.Show(Page, returnalertstring);
                }
            }
        }
        catch 
        {
            MessageBox.Show(Page, "请联系管理员进行查询！");
        }
    }

    private MTJ_RegisterCompanys mcompany = new MTJ_RegisterCompanys();

    public string ReturnAgentNameAndCity(string AgentID)
    {
        if (AgentID.Length > 0 && AgentID != "0")
        {
            mcompany = bcompany.GetList(int.Parse(AgentID));
            return "【" + bbase.GetList(mcompany.CTID).CName + "】" + mcompany.CompName;
        }
        else
        {
            return "";
        }
    }

    protected void Button_SearchByTxt_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            try
            {
                string uploadfilepath = Server.MapPath(@"~/Uploadfiles/") + DateTime.Now.ToString("yyyyMMddhhmmssss") +
                                        "_" + HttpUtility.UrlDecode(Request.Cookies["TJUserID"].Value) + ".txt";
                FileUpload1.SaveAs(uploadfilepath);
                HiddenField1.Value = uploadfilepath;
                StreamReader sdr = new StreamReader(uploadfilepath);
                DataTable dtout = new DataTable();
                // DataTable dtout2 = new DataTable();
                dtout.Columns.Add("状态");
                dtout.Columns.Add("瓶标");
                dtout.Columns.Add("箱标1");
                dtout.Columns.Add("货仓");
                dtout.Columns.Add("发货时间");
                dtout.Columns.Add("代理商");
                dtout.Columns.Add("产品名称");

                //dtout2.Columns.Add("瓶标");
                //dtout2.Columns.Add("箱标1");
                //dtout2.Columns.Add("箱标2");

                string templabelcodestring = "";
                while (!sdr.EndOfStream)
                {
                    templabelcodestring = sdr.ReadLine();
                    DataTable dtforreturn = com.QueryLabelInfoByLabelCode(templabelcodestring,GetCookieCompID());
                    if (dtforreturn != null)
                    {
                        foreach (DataRow dr in dtforreturn.Rows)
                        {
                            dtout.ImportRow(dr);
                        }
                    }
                    else
                    {
                        if (!templabelcodestring.Trim().Equals(""))
                        {
                            DataRow drforerr = dtout.NewRow();
                            drforerr.BeginEdit();
                            drforerr["状态"] = "异常";
                            drforerr["瓶标"] = templabelcodestring;
                            drforerr["箱标1"] = "";
                            drforerr["货仓"] = "";
                            drforerr["发货时间"] = "";
                            drforerr["代理商"] = "";
                            drforerr["产品名称"] = "";
                            drforerr.EndEdit();
                            dtout.Rows.Add(drforerr);
                        }
                    }
                }
                GridView_QueryResult.DataSource = dtout;
                GridView_QueryResult.DataBind();
                sdr.Close();
                com.clearfile(HiddenField1.Value.Trim(), false);
            }
            catch  
            {
                MessageBox.Show(Page, "请联系管理员进行查询！");
            }
        }
    }

    //DataTable dtforreturn;

    private DataTable getBox(string strbox, string TableName)
    {
        //string str = "select BottleLabel as 瓶标,BoxLabel01 as 箱标 from " + Tname + " where (BottleLabel='" + strbox + "' or BoxLabel01='" + strbox + "')";
        string str = "select  a.BottleLabel as 瓶标,a.BoxLabel01 as 箱标1,b.BoxLabel as 箱标2 from " + TableName +
                     "BT as a left join  " + TableName +
                     "BX as b  on  (a.BoxLabel01=b.BoxLabel or a.BoxLabel01=b.BoxLabel01) where (a.BottleLabel='" +
                     strbox + "' or a.BoxLabel01='" + strbox + "')";
        DataTable dt2 = new DataTable();
        dt2 = null;
        SqlDataAdapter sda = new SqlDataAdapter(str, sqlcon);
        DataSet ds = new DataSet();
        sda.Fill(ds, "Tname");
        if (ds.Tables["Tname"].Rows.Count > 0)
        {
            dt2 = ds.Tables["Tname"];
        }
        return dt2;
    }

    private string GetReturnSqlStringForBox(string tablename, string codevalue)
    {
        string returnstring = "select '' as 瓶标,BoxLabel01 as 箱标1,PID as 产品 from " + tablename + " where BoxLabel01='" +
                              codevalue + "'";
        return returnstring;
    }

    private string GetReturnSqlStringForReplaceBox(string tablename, string codevalue)
    {
        string BoxLabel01 = com.ReturnFiledValue(tablename + "_BX", "BoxLabel", codevalue, "BoxLabel01");
        if (BoxLabel01.Trim().Equals(""))
        {
            return "";
        }
        else
        {
            string returnstring = "select '' as 瓶标,BoxLabel01 as 箱标1,PID as 产品 from " + tablename +
                                  " where BoxLabel01='" + BoxLabel01 + "'";
            return returnstring;
        }
    }

    private string GetReturnSqlStringForBottle(string tablename, string codevalue)
    {
        string returnstring = "select p.BottleLabel as 瓶标,x.BoxLabel01 as 箱标1,PID as 产品 from " + tablename + " as x," +
                              tablename + "_BT as p where p.BottleLabel ='" + codevalue +
                              "' and x.BoxLabel01=p.BoxLabel01";

        return returnstring;
    }

    protected void GridView_QueryResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }

    private void CreateQueryOutTxtFile(GridView gv)
    {
        if (Directory.Exists(Server.MapPath(@"~/QUREY/")))
        {
            Directory.CreateDirectory(Server.MapPath(@"~/QUREY/"));
        }

        StreamWriter sw = new StreamWriter(Server.MapPath(@"~/QUREY/") + "//查询结果_" + FileUpload1.FileName.Trim(), false,
            Encoding.UTF8);

        string tempstring = "";
        foreach (GridViewRow dr in GridView_QueryResult.Rows)
        {
            for (int i = 0; i < dr.Cells.Count; i++)
            {
                if (!dr.Cells[i].Text.Trim().Equals("") && !dr.Cells[i].Text.Trim().Equals(";"))
                {
                    tempstring += "," + dr.Cells[i].Text.Trim();
                }
            }
            tempstring = tempstring.Substring(1);
            sw.WriteLine(tempstring);
            tempstring = "";
        }
        sw.Close();
    }


    public string ReturnProductNameByID(string PrductID)
    {
        if (PrductID.Trim() == "")
        {
            return "";
        }
        else
        {
            return bproduct.GetList(Convert.ToInt32(PrductID)).Products_Name;
        }
    }

    public string ReturnStoreHouseNameByID(string StoreHouseID)
    {
        return bstorehouse.GetList(Convert.ToInt32(StoreHouseID)).StoreHouseName;
    }

    protected void CheckBox_ByTxtFile_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_ByTxtFile.Checked)
        {
            Label2.Visible = true;
            FileUpload1.Visible = true;
            Button_SearchByTxt.Visible = true;

            Label1.Visible = false;
            TextBox_Label.Visible = false;
            Button1.Visible = false;

            GridView1.Visible = false;
            Label3.Visible = false;
        }
        else
        {
            Label2.Visible = false;
            FileUpload1.Visible = false;
            Button_SearchByTxt.Visible = false;

            Label1.Visible = true;
            TextBox_Label.Visible = true;
            Button1.Visible = true;

            GridView1.Visible = true;
        }
    }
}