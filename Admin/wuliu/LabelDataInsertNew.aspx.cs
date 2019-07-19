using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Web.Configuration;
using System.Web.UI;
using System.Text;
using System.Data;
using commonlib;

public partial class Admin_wuliu_LabelDataInsertNew : AuthorPage
{
    private string uploadfilepath = string.Empty;
    private OleDbConnection conn_mdb;

    private readonly SqlConnection sqlconn =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());

    private readonly SqlConnection sqlconntemp =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());

    private int rowcout;
    private int orirowcout;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Button_upload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            try
            {
                FileInfo file = new FileInfo(FileUpload1.PostedFile.FileName);
                if (file.Extension.Equals(".mdb"))
                {
                    // 保存文件
                    uploadfilepath = Server.MapPath(@"Uploadfiles/") + DateTime.Now.ToString("yyyyMMddhhmmssss") +
                                     ".mdb";
                    FileUpload1.SaveAs(uploadfilepath);
                    ViewState["uploadfilepath"] = uploadfilepath;
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('上传成功!');", true);
                    Label1.Text = "已上传文件:" + FileUpload1.FileName;
                    Button_DataCheck.Enabled = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('请上传格式为(.mdb)的数据库文件!');",
                        true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('" + ex.Message + "');",
                    true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('请选取数据文件!');", true);
        }
    }

    protected void Button_DataCheck_Click(object sender, EventArgs e)
    {
        if (!((string) ViewState["uploadfilepath"]).Trim().Equals(string.Empty))
        {
            conn_mdb =
                new OleDbConnection(@"provider=microsoft.jet.oledb.4.0;data source=" +
                                    ((string) ViewState["uploadfilepath"]).Trim() +
                                    ";Persist security Info=false;jet OLEDB:Database Password=;");
            OleDbCommand odm_mdb = new OleDbCommand("select count(*) as num from LabelData", conn_mdb);
            conn_mdb.Open();
            OleDbDataReader odr = odm_mdb.ExecuteReader();
            if (odr.Read())
            {
                rowcout = Convert.ToInt32(odr["num"].ToString());
                Label2.Text = "通过检查并计算,该数据包里面有" + rowcout + "套物流标签的数据!";
                Button_Insert.Enabled = true;
            }
            conn_mdb.Close();
            odr.Close();
        }
    }

    private string getinsertdatastringforw(string tablename)
    {
        string retstring = "insert into " + tablename + "(BoxLabel01,DaoRuShiJian,BoxLabelNum,BottleLabelNum) values(";
        return retstring;
    }

    private string getinsertdatastringforbt(string tablename)
    {
        string retstring = "insert into " + tablename + "(BottleLabel,BoxLabel01) values(";
        return retstring;
    }

    private string getinsertdatastringforbx(string tablename)
    {
        string retstring = "insert into " + tablename + "(BoxLabel,BoxLabel01) values(";
        return retstring;
    }

    private void opensqlconn(SqlConnection conn)
    {
        if (!conn.State.Equals(ConnectionState.Open))
        {
            conn.Open();
        }
    }

    private void openolecon(OleDbConnection conn)
    {
        if (conn.State.Equals(ConnectionState.Closed))
        {
            conn.Open();
        }
    }

    private string GetNewTableName(string startfourcharacter)
    {
        //string sqlstringforstarfourcode = "select count(*) as num from TB_LabelCodeInfo where startfournum='" + startfourcharacter + "'";
        SqlCommand sqlcmd =
            new SqlCommand(
                "select count(*) as num from TB_LabelCodeInfo where startfournum='" + startfourcharacter + "'", sqlconn);
        int IndexForStart = 0;
        opensqlconn(sqlconn);
        SqlDataReader sdr = sqlcmd.ExecuteReader();
        if (sdr.Read())
        {
            IndexForStart = Convert.ToInt32(sdr["num"].ToString().Trim());
        }
        sdr.Close();
        string returntablename = string.Empty;
        string sqlstring = "select * from TB_LabelCodeInfo where startfournum='" + startfourcharacter + "'";
        SqlDataAdapter sda = new SqlDataAdapter(sqlstring, sqlconn);
        DataSet ds = new DataSet();
        sda.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (Convert.ToInt32(dr["rowcout"].ToString()) < 3000000)
                {
                    returntablename = dr["tablenameinfo"].ToString().Trim() + ",0";
                    orirowcout = Convert.ToInt32(dr["rowcout"].ToString());
                }
                else
                {
                    returntablename = "W" + startfourcharacter + "_" +
                                      Convert.ToString(100 + IndexForStart).Substring(1) + ",1";
                }
            }
        }
        else
        {
            returntablename = "W" + startfourcharacter + "_" + Convert.ToString(100 + IndexForStart).Substring(1) + ",1";
        }
        return returntablename;
    }

    /// <summary>
    /// 检查是否具备了相关的表
    /// </summary>
    /// <param name="startfourcharacter"></param>
    /// <returns>true:需要创建,false:无需创建</returns>
    private bool CheckIsNeedCreateNewTable(string startfourcharacter)
    {
        bool returnvalue = false;
        SqlCommand sqlcmd =
            new SqlCommand(
                "select count(*) as num from TB_LabelCodeInfo where startfournum='" + startfourcharacter + "'",
                sqlconntemp);
        opensqlconn(sqlconntemp);
        SqlDataReader sdr = sqlcmd.ExecuteReader();
        if (sdr.Read())
        {
            if (int.Parse(sdr["num"].ToString().Trim()) > 0)
            {
                returnvalue = false;
            }
            else
            {
                returnvalue = true;
            }
        }
        else
        {
            returnvalue = false;
        }
        sdr.Close();
        sqlconntemp.Close();
        return returnvalue;
    }

    private string tempstartfournumber = "";
    private int tempBoxLabelIndexValue;
    private string tempreturn = "";

    /// <summary>
    /// 根据箱标返回对应表的名称
    /// </summary>
    /// <param name="BoxLabelCode">箱标号码</param>
    /// <returns></returns>
    private string ReturnObjectTable(string BoxLabelCode)
    {
        tempreturn = "";
        tempstartfournumber = BoxLabelCode.Substring(0, 4).Trim();
        tempBoxLabelIndexValue = int.Parse(BoxLabelCode.Substring(4, 7));
        if (tempBoxLabelIndexValue < 2500000)
        {
            tempreturn = "W" + tempstartfournumber + "_00";
        }
        else
        {
            if (tempBoxLabelIndexValue >= 2500000 && tempBoxLabelIndexValue < 5000000)
            {
                tempreturn = "W" + tempstartfournumber + "_01";
            }
            else
            {
                if (tempBoxLabelIndexValue >= 5000000 && tempBoxLabelIndexValue < 7250000)
                {
                    tempreturn = "W" + tempstartfournumber + "_02";
                }
                else
                {
                    tempreturn = "W" + tempstartfournumber + "_03";
                }
            }
        }
        return tempreturn;
    }

    private void GetLabelCodeUpdateScript(string startfournum, int boxnum, bool iscreate)
    {
        string startnumstringbox = "";
        string endnumstringbox = "";
        string startnumstringbottle = "";
        string endnumstringbottle = "";
        string rowcoutstring = "";
        string sqlstring = "";
        string sqlstring1 = "";
        string sqlstring2 = "";
        string tablename = "";
        SqlCommand sqlcmd = new SqlCommand();
        for (int i = 0; i < 4; i++)
        {
            tablename = "W" + startfournum + "_0" + i;
            sqlstring = "select min(BoxLabel01) as minvalue,max(BoxLabel01) as maxvalue,count(*) as num from " +
                        tablename;
            sqlstring1 = "select min(BottleLabel) as minvalue,max(BottleLabel) as maxvalue,count(*) as num from " +
                         tablename + "_BT";
            sqlstring2 = "select min(BoxLabel) as minvalue,max(BoxLabel) as maxvalue,count(*) as num from " + tablename +
                         "_BX";
            sqlcmd.CommandText = sqlstring;
            sqlcmd.Connection = sqlconntemp;
            opensqlconn(sqlconntemp);
            SqlDataReader sdr = sqlcmd.ExecuteReader();
            if (sdr.Read())
            {
                startnumstringbox = sdr["minvalue"].ToString().Trim();
                endnumstringbox = sdr["maxvalue"].ToString().Trim();
            }
            sdr.Close();
            sqlcmd.CommandText = sqlstring1;
            opensqlconn(sqlconntemp);
            SqlDataReader sdr1 = sqlcmd.ExecuteReader();
            if (sdr1.Read())
            {
                startnumstringbottle = sdr1["minvalue"].ToString().Trim();
                endnumstringbottle = sdr1["maxvalue"].ToString().Trim();
                rowcoutstring = sdr1["num"].ToString().Trim();
            }
            sdr1.Close();
            string startnumstring = (Convert.ToInt64(startnumstringbottle.Equals("") ? "0" : startnumstringbottle) <
                                     Convert.ToInt64(startnumstringbox.Equals("") ? "0" : startnumstringbox))
                ? startnumstringbottle
                : startnumstringbox;
            string endnumstring = (Convert.ToInt64(endnumstringbox.Equals("") ? "0" : endnumstringbox) >
                                   Convert.ToInt64(endnumstringbottle.Equals("") ? "0" : endnumstringbottle))
                ? endnumstringbox
                : endnumstringbottle;
            string returnstring = string.Empty;

            sqlcmd.CommandText = sqlstring2;
            SqlDataReader sdr2 = sqlcmd.ExecuteReader();
            long addboxnum = 0;
            if (sdr2.Read())
            {
                startnumstringbottle = sdr2["minvalue"].ToString().Trim();
                endnumstringbottle = sdr2["maxvalue"].ToString().Trim();
                addboxnum = Convert.ToInt64(sdr2["num"].ToString().Trim());
            }
            sdr2.Close();
            if (addboxnum > 0)
            {
                startnumstring = (Convert.ToInt64(startnumstring.Equals("") ? "0" : startnumstring) <
                                  Convert.ToInt64(startnumstringbottle.Equals("") ? "0" : startnumstringbottle))
                    ? startnumstring
                    : startnumstringbottle;
                endnumstring = (Convert.ToInt64(endnumstring.Equals("") ? "0" : endnumstring) >
                                Convert.ToInt64(endnumstringbottle.Equals("") ? "0" : endnumstringbottle))
                    ? endnumstring
                    : endnumstringbottle;
            }

            if (iscreate)
            {
                returnstring =
                    "insert into TB_LabelCodeInfo(startfournum,tablenameinfo,startvalue,endvalue,rowcout) values('" +
                    startfournum + "','" + tablename + "','" +
                    (startnumstring.Trim().Equals("") ? "0" : startnumstring.Trim()) + "','" +
                    (endnumstring.Trim().Equals("") ? "0" : endnumstring) + "'," + rowcoutstring + ")";
            }
            else
            {
                returnstring = "update TB_LabelCodeInfo set startvalue='" + startnumstring + "',endvalue='" +
                               endnumstring + "',rowcout=" + rowcoutstring + " where tablenameinfo='" + tablename +
                               "' and startfournum='" + startfournum + "'";
            }
            sqlcmd.CommandText = returnstring;
            opensqlconn(sqlconntemp);
            sqlcmd.ExecuteNonQuery();
        }
    }

    private string GetScriptToCreateTable(string StartFourCharacer)
    {
        StringBuilder result = new StringBuilder();
        string tempstring = "";
        string tablename = "";
        for (int i = 0; i < 4; i++)
        {
            tablename = "W" + StartFourCharacer + "_0" + i;
            tempstring = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + tablename +
                         "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [dbo].[" + tablename +
                         "] CREATE TABLE [" + tablename + "](";
            tempstring += "[BoxLabel01] [varchar] (16) PRIMARY KEY NOT NULL,";
            tempstring +=
                "[IsFaHuo] [bit]  NULL,[PID] [int] NULL,[ShengChanShiJian] [datetime] NULL,[DaoRuShiJian] [datetime] NULL,[FHKey] [varchar] (50)  COLLATE Chinese_PRC_CI_AS NULL,[BeiZhu] [varchar] (30) COLLATE Chinese_PRC_CI_AS NULL,[BoxLabelNum] [int] NULL,[BottleLabelNum] [int] NULL,[WSID] [int] NULL,[WLID] [int] NULL,[STID] [int] NULL,[CompID] [int] NULL)";
            result.Append(tempstring + ";");
        }
        return result.ToString();
    }

    private string GetScriptToCreateTableFH(string StartFourCharacer)
    {
        StringBuilder result = new StringBuilder();
        string tempstring = "";
        string tablename = "";
        for (int i = 0; i < 4; i++)
        {
            tablename = "W" + StartFourCharacer + "_0" + i + "_FH";
            tempstring = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + tablename +
                         "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [dbo].[" + tablename +
                         "] CREATE TABLE [" + tablename + "](";
            tempstring += "[ID] [int] IDENTITY(1,1) NOT NULL,	[BoxLabel01] [varchar](16) NOT NULL,";
            tempstring +=
                "[FromAgentID] [int] NULL,[FromStorehouseID] [int] NULL,[ToAgentID] [int] NULL,	[ToStoreHouseID] [int] NULL,	[FHDate] [datetime] NULL,	[FHType] [varchar](20) NULL,	[UserID] [int] NULL,	[FHPici] [varchar](50) NULL,[FHKey] [varchar](50) NULL,[CompID] [int] NULL,[ProductName] [varchar](60) NULL,[PID] [int] NULL)";
            result.Append(tempstring + ";");
        }
        return result.ToString();
    }

    private string GetScriptToCreateTableForModify(string tablenameformodify)
    {
        string str = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + tablenameformodify +
                     "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [dbo].[" + tablenameformodify +
                     "] CREATE TABLE [" + tablenameformodify +
                     "]([NewLabelCode] [varchar] (12) PRIMARY KEY NOT NULL ,[OriLabelCode] [varchar] (12) COLLATE Chinese_PRC_CI_AS NULL ) ON [PRIMARY]";
        return str;
    }

    private string GetScriptToCreateTableForBottleLabels(string startfournumber)
    {
        StringBuilder resultduilder = new StringBuilder();
        string tablenameforbottlelabel = "";
        string tablenameformodify = "";
        for (int i = 0; i < 4; i++)
        {
            tablenameforbottlelabel = "W" + startfournumber + "_0" + i + "_BT";
            tablenameformodify = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" +
                                 tablenameforbottlelabel +
                                 "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [dbo].[" +
                                 tablenameforbottlelabel + "] CREATE TABLE [" + tablenameforbottlelabel +
                                 "]([BottleLabel] [varchar] (16) PRIMARY KEY NOT NULL ,[BoxLabel01] [varchar] (16)  NULL,[IsDJ] [bit] NULL) ON [PRIMARY]";
            resultduilder.Append(tablenameformodify + ";");
        }
        return resultduilder.ToString();
    }

    private string GetScriptToCreateTableForBoxLabels(string startfourcharacternumber)
    {
        StringBuilder resultbd = new StringBuilder();
        string[] tempCreateSqlArray = new string[4];
        string tempstring = "";
        string tablenametemp = "";
        for (int i = 0; i < 4; i++)
        {
            tablenametemp = "W" + startfourcharacternumber + "_0" + i + "_BX";
            tempstring = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + tablenametemp +
                         "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [dbo].[" + tablenametemp +
                         "] CREATE TABLE [" + tablenametemp +
                         "]([BoxLabel] [varchar] (16) PRIMARY KEY NOT NULL ,[BoxLabel01] [varchar] (16) COLLATE Chinese_PRC_CI_AS NULL ) ON [PRIMARY]";
            resultbd.Append(tempstring);
        }
        return resultbd.ToString();
    }


    protected void Button_Insert_Click(object sender, EventArgs e)
    {
        try
        {
            uploadfilepath = ((string) ViewState["uploadfilepath"]).Trim();
            Button_DataCheck.Enabled = false;
            Button_Insert.Enabled = false;
            DateTime dtst = DateTime.Now;
            conn_mdb =
                new OleDbConnection(@"provider=microsoft.jet.oledb.4.0;data source=" + uploadfilepath +
                                    ";Persist security Info=false;jet OLEDB:Database Password=;");
            OleDbCommand odm_mdb = new OleDbCommand("select * from LabelData order by BoxLabel1", conn_mdb);
            SqlCommand sqlcmd = new SqlCommand();
            SqlTransaction transaction = null;
            sqlcmd.Connection = sqlconn;
            if (conn_mdb.State.Equals(ConnectionState.Closed))
            {
                conn_mdb.Open();
            }
            OleDbDataReader odr = odm_mdb.ExecuteReader();
            string tempstring = string.Empty;
            string startfourcharacter = string.Empty;
            string tablenameforinsert = string.Empty;
            int bottlenum = 0;
            //int boxnum = 0;
            int replaceboxnum = 0;
            //string[] temparry = new string[2];
            bool needcreate = false;
            string sqltempstring = string.Empty;
            string sqltempstringforbottle = string.Empty;
            string sqltempstringforbox = string.Empty;
            //string sqlstring = string.Empty;
            string startnumstring = string.Empty;
            string endnumstring = string.Empty;
            bool iscreate = false;
            string bottleindex = "";
            string boxindex = "";
            string BoxLabel01 = "";
            string TableNameToInsert = "";
            if (odr.HasRows)
            {
                odr.Read();
                DataTable dt_temp = odr.GetSchemaTable();
                for (int c = 0; c < dt_temp.Rows.Count; c++)
                {
                    //tempstring = odr[c].ToString().Substring(0,3).ToLower();
                    if (dt_temp.Rows[c]["ColumnName"].ToString().Trim().Length > 3)
                    {
                        tempstring = dt_temp.Rows[c]["ColumnName"].ToString().ToLower().Substring(0, 3);
                        switch (tempstring)
                        {
                            case "bot":
                                bottlenum++;
                                bottleindex += dt_temp.Rows[c]["ColumnName"].ToString().Trim() + ",";
                                break;
                            case "box":
                                //boxnum++;
                                boxindex += dt_temp.Rows[c]["ColumnName"].ToString().Trim() + ",";
                                break;
                            case "rep":
                                bottleindex += dt_temp.Rows[c]["ColumnName"].ToString().Trim() + ",";
                                break;
                            case "rex":
                                replaceboxnum++;
                                boxindex += dt_temp.Rows[c]["ColumnName"].ToString().Trim() + ",";
                                break;
                            default:
                                break;
                        }
                    }
                }
                startnumstring = odr["BoxLabel1"].ToString();
                startfourcharacter = startnumstring.Substring(0, 4).ToLower();
                needcreate = CheckIsNeedCreateNewTable(startfourcharacter);
                string[] bottleindexarray = bottleindex.Split(',');
                string[] boxindexarray = boxindex.Split(',');

                if (needcreate)
                {
                    iscreate = true;
                    sqlcmd.CommandText = GetScriptToCreateTable(startfourcharacter);
                    sqlcmd.Connection = sqlconn;
                    opensqlconn(sqlconn);
                    sqlcmd.ExecuteNonQuery();
                    sqlcmd.CommandText = GetScriptToCreateTableForBottleLabels(startfourcharacter);
                    sqlcmd.ExecuteNonQuery();
                    sqlcmd.CommandText = GetScriptToCreateTableForBoxLabels(startfourcharacter);
                    sqlcmd.ExecuteNonQuery();
                    sqlcmd.CommandText = GetScriptToCreateTableFH(startfourcharacter);
                    sqlcmd.ExecuteNonQuery();
                }
                BoxLabel01 = odr["BoxLabel1"].ToString();
                TableNameToInsert = ReturnObjectTable(BoxLabel01);
                sqltempstring = getinsertdatastringforw(TableNameToInsert);
                sqltempstringforbottle = getinsertdatastringforbt(TableNameToInsert + "_BT");

                if (replaceboxnum > 0)
                {
                    sqltempstringforbox = getinsertdatastringforbx(TableNameToInsert + "_BX");
                }
                openolecon(conn_mdb);
                string sqlstringall = "";
                foreach (string bottlelabelcode in bottleindexarray)
                {
                    if (!bottlelabelcode.Trim().Equals(""))
                    {
                        if (!odr[bottlelabelcode].ToString().Trim().Equals(""))
                        {
                            sqlstringall += sqltempstringforbottle + odr[bottlelabelcode] + ",";

                            sqlstringall += odr["BoxLabel1"].ToString().Trim() + ",";
                            if (sqlstringall.EndsWith(","))
                            {
                                sqlstringall = sqlstringall.Substring(0, sqlstringall.LastIndexOf(',')) + ");";
                            }
                        }
                    }
                }
                if (replaceboxnum > 0)
                {
                    foreach (string boxlabel in boxindexarray)
                    {
                        if (!boxlabel.Trim().Equals(""))
                        {
                            if (!boxlabel.Equals("BoxLabel1"))
                            {
                                if ((!odr[boxlabel].Equals(null)) && (!odr[boxlabel].ToString().Equals("")))
                                {
                                    sqlstringall += sqltempstringforbox + odr[boxlabel] + ",";
                                    sqlstringall += odr["BoxLabel1"].ToString().Trim() + ",";
                                    if (sqlstringall.EndsWith(","))
                                    {
                                        sqlstringall = sqlstringall.Substring(0, sqlstringall.LastIndexOf(',')) + ");";
                                    }
                                }
                            }
                        }
                    }
                }
                sqlstringall += sqltempstring + odr["BoxLabel1"] + ",'" + DateTime.Now + "',1," + bottlenum + ")";
                sqlcmd.CommandText = sqlstringall;
                opensqlconn(sqlconn);
                transaction = sqlconn.BeginTransaction("insertdata");
                sqlcmd.Transaction = transaction;
                sqlcmd.Connection = sqlconn;
                try
                {
                    sqlcmd.ExecuteNonQuery();
                    sqlstringall = "";

                    openolecon(conn_mdb);
                    try
                    {
                        while (odr.Read())
                        {
                            BoxLabel01 = odr["BoxLabel1"].ToString();
                            TableNameToInsert = ReturnObjectTable(BoxLabel01);
                            sqltempstring = getinsertdatastringforw(TableNameToInsert);
                            sqltempstringforbottle = getinsertdatastringforbt(TableNameToInsert + "_BT");
                            foreach (string bottlelabelcode in bottleindexarray)
                            {
                                if (!bottlelabelcode.Trim().Equals(""))
                                {
                                    if (!odr[bottlelabelcode].ToString().Trim().Equals(""))
                                    {
                                        sqlstringall += sqltempstringforbottle + odr[bottlelabelcode] + ",";
                                        sqlstringall += odr["BoxLabel1"].ToString().Trim() + ",";
                                        if (sqlstringall.EndsWith(","))
                                        {
                                            sqlstringall = sqlstringall.Substring(0, sqlstringall.LastIndexOf(',')) +
                                                           ");";
                                        }
                                    }
                                }
                            }
                            if (replaceboxnum > 0)
                            {
                                sqltempstringforbox = getinsertdatastringforbx(TableNameToInsert + "_BX");
                                foreach (string boxlabel in boxindexarray)
                                {
                                    if (!boxlabel.Trim().Equals(""))
                                    {
                                        if (!boxlabel.Equals("BoxLabel1"))
                                        {
                                            if (!odr[boxlabel].Equals(null) && !odr[boxlabel].ToString().Equals(""))
                                            {
                                                sqlstringall += sqltempstringforbox + odr[boxlabel] + ",";
                                                sqlstringall += odr["BoxLabel1"].ToString().Trim() + ",";
                                                if (sqlstringall.EndsWith(","))
                                                {
                                                    sqlstringall =
                                                        sqlstringall.Substring(0, sqlstringall.LastIndexOf(',')) + ");";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            sqlstringall += sqltempstring + odr["BoxLabel1"] + ",'" + DateTime.Now + "',1," + bottlenum +
                                            ")";
                            sqlcmd.CommandText = sqlstringall;
                            sqlcmd.ExecuteNonQuery();
                            sqlstringall = "";
                        }
                        transaction.Commit();
                        odr.Close();
                        odr.Dispose();
                        conn_mdb.Close();
                        sqlconn.Close();
                        sqlcmd.Dispose();
                        GetLabelCodeUpdateScript(startfourcharacter, 1, iscreate);
                        DateTime dted = DateTime.Now;
                        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info",
                            "alert('数据导入成功!耗时:" + Convert.ToString(dted - dtst) + "');", true);
                    }
                    catch (Exception ex)
                    {
                        odr.Close();
                        odr.Dispose();
                        conn_mdb.Close();
                        sqlconn.Close();
                        sqlcmd.Dispose();
                        transaction.Rollback("insertdata");
                        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info",
                            "alert('" + ex.Message + "');", true);
                        //Response.Write("<script>alert('在数据导入过程中有异常,请检查标签数据,本次导入失败!')</script>");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback("insertdata");
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('" + ex.Message + "');",
                        true);
                }
            }
            Label1.Text = "";
            Label2.Text = "";
            CommonFunWL comn = new CommonFunWL();
            if (!comn.clearfile(uploadfilepath, false))
            {
                odr.Close();
                odr.Dispose();
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('无法删除当前上传的数据文件,请手工删除!);",
                    true);
            }
        }
        catch (Exception ex)
        {
            //Response.Write("<script>alert('"+ex.Message.ToString()+"')</script>");            
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('" + ex.Message + "');", true);
            //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "info", "alert('系统提示:当前标签数据包中至少已经有一套标签入库!');", true);
        }
    }
}