using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;     
using commonlib;
using Wuqi.Webdiyer;

public partial class Admin_wuliu_TJ_FaHuoConfirm : AuthorPage
{                                    
    DBClass db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            inputsdate.Text = DateTime.Now.ToString("yyyy-MM-01");
            inputenddate.Text = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(1).AddDays(-1)).ToString("yyyy-MM-dd");
            //FillDDL("2");
            inputagentid.Attributes.Add("onclick", ReturnSelectScriptString());
            if (!string.IsNullOrEmpty(Request.QueryString["agid"]))
            {
                hd_agentid.Value = Request.QueryString["agid"];
                inputagentid.Value = Request.QueryString["agname"];
                ddlfhpici.DataSource = db.GetFaHuoPicInfo(GetCookieCompID(), hd_agentid.Value, inputsdate.Text, inputenddate.Text);
                ddlfhpici.DataBind();
                btn_go_Click(sender, e);
            }
        }
    }

    private string ReturnSelectScriptString()
    {
        return "openWinCenter('TB_Agents_Infor_forselect.aspx?fr=" +Sc.EncryptQueryString(Request.RawUrl) + "', 700, 580, '选择经销商')";
    } 

    //private void FillDDL(string compid)
    //{ 
    //    ddlagent.DataSource = db.GetMyAgentInfo(compid);
    //    ddlagent.DataBind(); 
    //    ddlagent.Items.Insert(0,new ListItem("请指定经销商...","0"));
    //    ddlagent.SelectedValue = "0";
    //}
    //protected void ddlagent_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ddlfhpici.DataSource = db.GetFaHuoPicInfo("2", ddlagent.SelectedValue, inputsdate.Text, inputenddate.Text);
    //    ddlfhpici.DataBind();
    //}
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {

    }

    commwl commwl = new commwl(); 

    protected void btn_go_Click(object sender, EventArgs e)
    {
        gvfhinfo.DataSource = null;
        gvfhinfo.DataBind();
        if (int.Parse(hd_agentid.Value)> 0 && ddlfhpici.SelectedValue.Length > 0)
        {
            go.Value = "1";
            DataTable dt = db.GetFaHuoInfoByFHID(GetCookieCompID(), ddlfhpici.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                string temptablenamestring = dt.Rows[0]["TableNameInfo"].ToString();
                string[] tablearray = temptablenamestring.Split(new char[','], StringSplitOptions.RemoveEmptyEntries);
                DataSet dtSet = new DataSet();
                string tbname = "";
                for (int i = 0; i < tablearray.Length; i++)
                {
                    if (!string.IsNullOrEmpty(tablearray[i]))
                    {
                        tbname = tablearray[i];
                        dtSet.Tables.Add(db.GetFahuoDetailByTableAndKey(tablearray[i], dt.Rows[0]["FHKey"].ToString()));
                    }
                }
                DataTable dtall = GetAllDataTable(dtSet);
                dtSet.Dispose();
                if (dtall.Rows.Count > 0)
                {
                    int maxcs = int.Parse(dtall.Compute("Max(cs)", "true").ToString());
                    maxcs++;
                    for (int i = 1; i <= maxcs; i++)
                    {
                        dtall.Columns.Add(getint(char.Parse(i.ToString())) + "级确认", typeof(String));
                    }
                    foreach (DataRow row in dtall.Rows)
                    {
                        if (maxcs > 1)
                        {
                            DataTable dttemp = commwl.GetConfirmInfo(tbname + "_FH", row[1].ToString());
                            if (dttemp.Rows.Count > 0)
                            {
                                for (int c = 0; c < dttemp.Rows.Count; c++)
                                {
                                    if (c == 0)
                                    {
                                        if (dttemp.Rows[c]["compname"].Equals(row["至"]))
                                        {
                                            row[6 + c] = "已确认(" + dttemp.Rows[c]["AcceptDate"] + ")";
                                        }
                                        else
                                        {
                                            row[6 + c] = "更新为(" + dttemp.Rows[c]["compname"] + dttemp.Rows[c]["AcceptDate"] + ")";
                                        }
                                    }
                                    else
                                    {
                                        row[6 + c] = "已确认(" + dttemp.Rows[c]["AcceptDate"] + ")";
                                    }
                                }
                            }
                        }
                    }
                    dtall.Columns.Remove("cs");
                    gvfhinfo.DataSource = dtall;
                    gvfhinfo.DataBind();
                } 
            }
            dt.Dispose();
        } 
    }
    
    public DataTable GetAllDataTable(DataSet ds)
    {
        DataTable newDataTable = ds.Tables[0].Clone();                
        object[] objArray = new object[newDataTable.Columns.Count];   
        for (int i = 0; i < ds.Tables.Count; i++)
        {
            for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
            {
                ds.Tables[i].Rows[j].ItemArray.CopyTo(objArray, 0);    
                newDataTable.Rows.Add(objArray);                    
            }
        }
        ds.Dispose(); 
        return newDataTable;       
    }

    private string getint(char c)
    {
        string str = "";
        switch (c)
        {
            case '0':
                str = "零";
                break;
            case '1':
                str = "一";
                break;
            case '2':
                str = "二";
                break;
            case '3':
                str = "三";
                break;
            case '4':
                str = "四";
                break;
            case '5':
                str = "五";
                break;
            case '6':
                str = "六";
                break;
            case '7':
                str = "七";
                break;
            case '8':
                str = "八";
                break;
            case '9':
                str = "九";
                break;
        }
        return str;
    }


    protected void gvfhinfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text.Contains("已确认"))
            {    
                e.Row.Cells[5].ForeColor = Color.White;
                e.Row.Cells[5].BackColor = Color.Green;
            }
            else
            {
                if (e.Row.Cells[5].Text.Contains("更新为"))
                {
                    e.Row.Cells[5].ForeColor = Color.White;
                    e.Row.Cells[5].BackColor = Color.Red;
                } 
            }
            if (e.Row.Cells.Count > 6)
            {
                if (e.Row.Cells[6].Text.Contains("已确认"))
                {
                    e.Row.Cells[6].ForeColor = Color.White;
                    e.Row.Cells[6].BackColor = Color.Green;
                }
                else
                {
                    if (e.Row.Cells[6].Text.Contains("更新为"))
                    {
                        e.Row.Cells[6].ForeColor = Color.White;
                        e.Row.Cells[6].BackColor = Color.Red;
                    }
                } 
            }
            if (e.Row.Cells.Count > 7)
            {
                if (e.Row.Cells[7].Text.Contains("已确认"))
                {
                    e.Row.Cells[7].ForeColor = Color.White;
                    e.Row.Cells[7].BackColor = Color.Green;
                }
                else
                {
                    if (e.Row.Cells[7].Text.Contains("更新为"))
                    {
                        e.Row.Cells[7].ForeColor = Color.White;
                        e.Row.Cells[7].BackColor = Color.Red;
                    }
                }
            }
        }
    }
    protected void ddlfhpici_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn_go_Click(sender,e);
    }
}