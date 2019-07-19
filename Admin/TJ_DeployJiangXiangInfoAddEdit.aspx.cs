using System;
using System.Data;
using System.Web.Configuration;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Data.SqlClient;

public partial class Admin_TJ_DeployJiangXiangInfoAddEdit : AuthorPage
{
    private readonly BTJ_DeployJiangXiangInfo bll = new BTJ_DeployJiangXiangInfo();
    private MTJ_DeployJiangXiangInfo mod = new MTJ_DeployJiangXiangInfo();
    private readonly BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();
    private BTJ_RegisterCompanys bagent = new BTJ_RegisterCompanys();
    private readonly BTB_Products_Infor bproduct = new BTB_Products_Infor();
    private readonly BTJ_JXInfo bjxing = new BTJ_JXInfo();
    private readonly CommonFunWL comwl = new CommonFunWL();
    private readonly BTJ_ZJLabelCodeInfo bzjinfo = new BTJ_ZJLabelCodeInfo();

    private readonly SqlConnection sqlconn =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());

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
            FillDDL();
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
            if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
            {
                mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
            }
            mod.JxID = Convert.ToInt32(ComboBox_JX.SelectedValue);
            mod.AgentID = Convert.ToInt32(ComboBox_AgentID.SelectedValue);
            mod.DjdID = Convert.ToInt32(ComboBox_DJD.SelectedValue);
            mod.PreOderNum = Convert.ToInt32(inputPreOderNum.Value.Trim());
            mod.ProID = Convert.ToInt32(ComboBox_Prod.SelectedValue);
            mod.ChuKuDanHao = ComboBox_ChuKuDanHao.SelectedValue;
            mod.Deploydate = DateTime.Now;
            mod.UserID = Convert.ToInt32(GetCookieUID());
            mod.Remarks = inputRemarks.Value.Trim();
            mod.ChuKuDateStart = Convert.ToDateTime("1900-01-01");
            mod.ChuKuDateEnd = Convert.ToDateTime("1900-01-01");
            mod.CompID = Convert.ToInt32(GetCookieCompID());
            switch (HF_CMD.Value.Trim())
            {
                case "add":
                    object DJPXID = bll.Insert(mod);
                    GetZhongJiangInfo(DJPXID.ToString(), RBList_Mode.SelectedValue);
                    break;
                case "edit":
                    bll.Modify(mod);
                    break;
                default:
                    bll.Insert(mod);
                    break;
            }

            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
            //Response.Write("<script>alert('操作成功！');</script>");
            //Response.Write("<script>window.location.href ='TJ_DeployJiangXiangInfoAddEdit.aspx';</script>");
        }
    }

    private bool CheckInput()
    {
        if (ComboBox_DJD.SelectedValue == "0" || ComboBox_DJD.SelectedValue == null)
        {
            Response.Write("<script>alert('请指定兑奖点！');</script>");
            return false;
        }
        if (ComboBox_JX.SelectedValue == "0" || ComboBox_JX.SelectedValue == null)
        {
            Response.Write("<script>alert('请指定奖项！');</script>");
            return false;
        }
        if (inputPreOderNum.Value.Trim().Length == 0)
        {
            Response.Write("<script>alert('请数量或比例！');</script>");
            return false;
        }
        if (RBList_Mode.SelectedValue == "2")
        {
            if (Convert.ToInt32(inputPreOderNum.Value) > 100)
            {
                Response.Write("<script>alert('不能大于100%！');</script>");
                return false;
            }
        }
        if (CheckBox_LimitedTotalNum.Checked)
        {
            if (inputTotalNum.Value.Length == 0)
            {
                Response.Write("<script>alert('请输入奖项总数！');</script>");
                return false;
            }
        }
        return true;
    }


    private void GetZhongJiangInfo(string DPJXID, string Mode)
    {
        string sqlstring = "select TableNameInfo,FHKey from TB_FaHuoInfo_" + GetCookieCompID() + " where CompID=" +
                           GetCookieCompID() + " and FHTypeID=3 " +
                           (ComboBox_AgentID.SelectedValue != "0"
                               ? " and AgentID=" + ComboBox_AgentID.SelectedValue
                               : "") +
                           (ComboBox_ChuKuDanHao.SelectedValue != "0"
                               ? " and FHPiCi='" + ComboBox_ChuKuDanHao.SelectedValue + "'"
                               : "") +
                           (ComboBox_Prod.SelectedValue != "0" ? " and ProID=" + ComboBox_Prod.SelectedValue : "");
        SqlDataAdapter sdafhinfo = new SqlDataAdapter(sqlstring, sqlconn);
        DataTable dttemp = new DataTable();
        sdafhinfo.Fill(dttemp);
        string tableinfo = "";
        string FHKey = "";
        DataTable dtallbottlelabelcode = new DataTable();
        dtallbottlelabelcode.Columns.Add("BottleLabel");
        dtallbottlelabelcode.Columns.Add("BoxLabel01");
        string tablenamestring = "";
        foreach (DataRow dr in dttemp.Rows)
        {
            tableinfo = dr["TableNameInfo"].ToString();
            FHKey = dr["FHKey"].ToString();
            string[] tablearray = tableinfo.Split(',');
            foreach (string tb in tablearray)
            {
                if (tb != "" && tb != null)
                {
                    if (!tablenamestring.Contains(tb))
                    {
                        tablenamestring += "," + tb;
                    }
                    // sqlstring = "SELECT bt.BottleLabel,fh.BoxLabel01 FROM " + tb + "_BT bt," + tb + "_FH fh where (bt.IsDJ is null or bt.IsDJ<>1) and fh.BoxLabel01=bt.BoxLabel01 and fh.FHKey='" + FHKey + "'   order by fh.BoxLabel01";

                    #region 增加了箱标替换的查询 2016.7.23

                    sqlstring = " SELECT bt.BottleLabel,bt.IsDJ, bt.BoxLabel01 FROM " + tb +
                                "_BT  bt where (bt.IsDJ is null or bt.IsDJ<>1) and bt.BoxLabel01 in((select bx.BoxLabel01 from " +
                                tb + "_BX bx," + tb + "_FH fh where fh.FHKey='" + FHKey +
                                "'and fh.BoxLabel01=bx.BoxLabel  )  ) or (bt.IsDJ is null or bt.IsDJ<>1) and  bt.BoxLabel01 in (  SELECT distinct bt.BoxLabel01 FROM " +
                                tb + "_BT bt ," + tb +
                                "_FH fh where (bt.IsDJ is null or bt.IsDJ<>1) and bt.BoxLabel01=fh.BoxLabel01 and fh.FHKey='" +
                                FHKey + "')";

                    #endregion

                    SqlDataAdapter sda = new SqlDataAdapter(sqlstring, sqlconn);
                    sda.Fill(dtallbottlelabelcode);
                }
            }
        }
        if (dtallbottlelabelcode.Rows.Count > 0)
        {
            Random rd = new Random();
            string getbottlelable = "";
            int couter = 0;
            switch (Mode)
            {
                case "1":
                    DataTable distinctboxlabeltable = SelectDistinct("", dtallbottlelabelcode, "BoxLabel01");
                    string usedindexstring = "";
                    int index = 0;
                    int lindex = 0;
                    string usedlineindexstring = "";
                    if (CheckBox_LimitedTotalNum.Checked)
                    {
                        for (int l = 0; l < Convert.ToInt32(inputTotalNum.Value); l++)
                        {
                            lindex = rd.Next(0, distinctboxlabeltable.Rows.Count - 1);
                            while (usedlineindexstring.Contains("," + lindex + ","))
                            {
                                lindex = rd.Next(0, distinctboxlabeltable.Rows.Count - 1);
                            }
                            usedlineindexstring += "," + lindex + ",";
                            DataRow[] drarray =
                                dtallbottlelabelcode.Select(
                                    "BoxLabel01='" + distinctboxlabeltable.Rows[lindex][0] + "'", "BottleLabel");
                            usedindexstring = "";
                            for (int i = 0; i < Convert.ToInt32(inputPreOderNum.Value); i++)
                            {
                                index = rd.Next(0, drarray.Length - 1);
                                while (usedindexstring.Contains("," + index + ","))
                                {
                                    index = rd.Next(0, drarray.Length - 1);
                                }
                                usedindexstring += "," + index + ",";
                                getbottlelable += "," + drarray[index]["BottleLabel"];
                                bzjinfo.Insert(new MTJ_ZJLabelCodeInfo(0, drarray[index]["BottleLabel"].ToString(),
                                    drarray[index]["BoxLabel01"].ToString(), 0, int.Parse(ComboBox_JX.SelectedValue), "",
                                    inputRemarks.Value, int.Parse(GetCookieCompID()), int.Parse(DPJXID), ""));
                                couter++;
                            }
                        }
                    }
                    else
                    {
                        foreach (DataRow dr in distinctboxlabeltable.Rows)
                        {
                            DataRow[] drarray = dtallbottlelabelcode.Select("BoxLabel01='" + dr[0] + "'", "BottleLabel");
                            usedindexstring = "";
                            if (Convert.ToInt32(inputPreOderNum.Value) < (drarray.Length) &&
                                Convert.ToInt32(inputPreOderNum.Value) != 12 &&
                                Convert.ToInt32(inputPreOderNum.Value) != 12)
                            {
                                for (int i = 0; i < Convert.ToInt32(inputPreOderNum.Value); i++)
                                {
                                    index = rd.Next(0, drarray.Length - 1);
                                    while (usedindexstring.Contains("," + index + ","))
                                    {
                                        index = rd.Next(0, drarray.Length - 1);
                                    }
                                    if (usedindexstring.Length.Equals(0))
                                    {
                                        usedindexstring = "," + index + ",";
                                    }
                                    else
                                    {
                                        usedindexstring += index + ",";
                                    }
                                    if (!getbottlelable.Contains("," + drarray[index]["BottleLabel"]))
                                    {
                                        getbottlelable += "," + drarray[index]["BottleLabel"];
                                    }
                                    bzjinfo.Insert(new MTJ_ZJLabelCodeInfo(0, drarray[index]["BottleLabel"].ToString(),
                                        drarray[index]["BoxLabel01"].ToString(), 0, int.Parse(ComboBox_JX.SelectedValue),
                                        "", inputRemarks.Value, int.Parse(GetCookieCompID()), int.Parse(DPJXID), ""));
                                    couter++;
                                }
                            }
                            else
                            {
                                foreach (var dataRow in drarray)
                                {
                                    if (!getbottlelable.Contains("," + dataRow["BottleLabel"]))
                                    {
                                        getbottlelable += "," + dataRow["BottleLabel"];
                                    }
                                    bzjinfo.Insert(new MTJ_ZJLabelCodeInfo(0, drarray[index]["BottleLabel"].ToString(),
                                        dataRow["BoxLabel01"].ToString(), 0, int.Parse(ComboBox_JX.SelectedValue), "",
                                        inputRemarks.Value, int.Parse(GetCookieCompID()), int.Parse(DPJXID), ""));
                                    couter++;
                                    index++;
                                }
                            }
                            index = 0;
                        }
                    }
                    break;
                case "2":
                    int startindex = 0;
                    int totalcount = 0;
                    int shengchengshuliang = 0;
                    int pace = 0;
                    if (Convert.ToInt32(inputPreOderNum.Value) >= 100)
                    {
                        startindex = 1;
                        totalcount = dtallbottlelabelcode.Rows.Count;
                        shengchengshuliang = totalcount;
                        pace = 1;
                    }
                    else
                    {
                        startindex = rd.Next(10);
                        totalcount = dtallbottlelabelcode.Rows.Count - startindex;
                        shengchengshuliang =
                            Convert.ToInt32(dtallbottlelabelcode.Rows.Count*Convert.ToInt32(inputPreOderNum.Value)/100);
                        pace = totalcount/shengchengshuliang;
                    }
                    couter = 0;
                    for (int i = startindex - 1; i < dtallbottlelabelcode.Rows.Count; i += pace)
                    {
                        couter++;
                        getbottlelable += "," + dtallbottlelabelcode.Rows[i][0];
                        bzjinfo.Insert(new MTJ_ZJLabelCodeInfo(0, dtallbottlelabelcode.Rows[i][0].ToString(),
                            dtallbottlelabelcode.Rows[i][1].ToString(), 0, int.Parse(ComboBox_JX.SelectedValue), "",
                            inputRemarks.Value, int.Parse(GetCookieCompID()), int.Parse(DPJXID), ""));
                        if (couter >= shengchengshuliang)
                        {
                            break;
                        }
                    }
                    break;
            }

            if (tablenamestring.StartsWith(","))
            {
                tablenamestring = tablenamestring.Substring(1);
            }
            string[] tablenamearray = tablenamestring.Split(',');
            SqlCommand sqlcommand = new SqlCommand();
            foreach (string tab in tablenamearray)
            {
                sqlcommand.Connection = sqlconn;
                sqlcommand.CommandText = "update " + tab + "_BT set IsDJ=1 where BottleLabel in(" +
                                         (getbottlelable.StartsWith(",") ? getbottlelable.Substring(1) : getbottlelable) +
                                         ")";
                if (sqlconn.State != ConnectionState.Open)
                {
                    sqlconn.Open();
                }
                sqlcommand.CommandTimeout = 3600;
                sqlcommand.ExecuteNonQuery();
            }
            mod = bll.GetList(Convert.ToInt32(DPJXID));
            mod.PreOderNum = couter;
            bll.Modify(mod);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "info", "alert('尚未发现发货记录，无法布奖！')", true);
        }
    }

    //此方法将所选字段的唯一值复制到一个新的 DataTable 。 如果字段包含 NULL 值，目标表中的记录还包含 NULL 值 
    public DataTable SelectDistinct(string TableName, DataTable SourceTable, string FieldName)
    {
        DataTable dt = new DataTable(TableName);
        dt.Columns.Add(FieldName, SourceTable.Columns[FieldName].DataType);

        object LastValue = null;
        foreach (DataRow dr in SourceTable.Select("", FieldName))
        {
            if (LastValue == null || !(ColumnEqual(LastValue, dr[FieldName])))
            {
                LastValue = dr[FieldName];
                dt.Rows.Add(new object[] {LastValue});
            }
        }
        ////if (ds != null)
        ////ds.Tables.Add(dt);
        return dt;
    }

    private static bool ColumnEqual(object A, object B)
    {
        if (A == DBNull.Value && B == DBNull.Value) //   both   are   DBNull.Value   
            return true;
        if (A == DBNull.Value || B == DBNull.Value) //   only   one   is   DBNull.Value   
            return false;
        return (A.Equals(B)); //   value   type   standard   comparison   
    }

    private void fillinput(int id)
    {
        MTJ_DeployJiangXiangInfo ms = bll.GetList(id);
        ComboBox_JX.SelectedValue = ms.JxID.ToString().Trim();
        ComboBox_AgentID.SelectedValue = ms.AgentID.ToString().Trim();
        ComboBox_DJD.SelectedValue = ms.DjdID.ToString().Trim();
        inputPreOderNum.Value = ms.PreOderNum.ToString().Trim();
        ComboBox_Prod.SelectedValue = ms.ProID.ToString().Trim();
        ComboBox_ChuKuDanHao.SelectedValue = ms.ChuKuDanHao.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
    }

    private string AgentIDStringForQuery = "";

    private void FillDDL()
    {
        AgentIDStringForQuery = comwl.GetAgentIDStringByCompID(GetCookieCompID());
        ComboBox_AgentID.DataSource =
            bcompany.GetListsByFilterString("CompID in (" + AgentIDStringForQuery + ") and CompTypeID=" +
                                            DAConfig.CompTypeIDJingXiaoShang);
        ComboBox_AgentID.DataBind();
        ComboBox_DJD.DataSource = bcompany.GetListsByFilterString("ParentID=" + GetCookieCompID());
        ComboBox_DJD.DataBind();
        ComboBox_Prod.DataSource = bproduct.GetListsByFilterString("CompID=" + GetCookieCompID());
        ComboBox_Prod.DataBind();
        ComboBox_JX.DataSource = bjxing.GetListsByFilterString("CompID=" + GetCookieCompID());
        ComboBox_JX.DataBind();
        string sqlstring = "select distinct FHPiCi from TB_FaHuoInfo_" + GetCookieCompID() + " where FHDate>='"+DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd")+"' and FHTypeID=3 and CompID=" + GetCookieCompID();
        SqlDataAdapter sda = new SqlDataAdapter(sqlstring, sqlconn);
        DataTable dttemp = new DataTable();
        sda.Fill(dttemp);
        ComboBox_ChuKuDanHao.DataSource = dttemp;
        ComboBox_ChuKuDanHao.DataBind();
    }

    protected void RBList_Mode_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (RBList_Mode.SelectedValue)
        {
            case "1":
                LabelName.Text = "每箱数量：";
                Label_percent.Text = "";
                break;
            case "2":
                LabelName.Text = "所占比例：";
                Label_percent.Text = "%";
                break;
        }
    }

    protected void CheckBox_LimitedTotalNum_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_LimitedTotalNum.Checked)
        {
            inputTotalNum.Disabled = false;
            Label_LimitTotalNum.Visible = true;
        }
        else
        {
            inputTotalNum.Disabled = true;
            Label_LimitTotalNum.Visible = false;
        }
    }
}