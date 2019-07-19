using System;
using System.Data;
using System.Drawing;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using System.Collections.Generic;
using System.Data.SqlClient;
using TJ.DBUtility;
using Wuqi.Webdiyer;

public partial class Admin_TJ_DeployJiangXiangInfo : AuthorPage
{
    private readonly BTJ_DeployJiangXiangInfo bll = new BTJ_DeployJiangXiangInfo();
    private MTJ_DeployJiangXiangInfo mod = new MTJ_DeployJiangXiangInfo();
    public BTJ_User buser = new BTJ_User();
    public BTJ_RegisterCompanys bcompany = new BTJ_RegisterCompanys();
    public BTJ_JXInfo bjx = new BTJ_JXInfo();
    public BTJ_RegisterCompanys bagent = new BTJ_RegisterCompanys();
    public BTB_Products_Infor bprod = new BTB_Products_Infor();
    private readonly BTJ_DjManage bdjmanage = new BTJ_DjManage();
    private readonly BTJ_ZJLabelCodeInfo bzjlabel = new BTJ_ZJLabelCodeInfo();
    private readonly TabExecute tabex = new TabExecute();
    private readonly CommonFunWL cwl = new CommonFunWL();
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();

    private readonly SqlConnection sqlconnwuliu =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string comp = GetCookieCompID();
            if (comp == "1")
            {
                _currentindex = 1;
                DisplayData(_currentindex, AspNetPager1.PageSize);
            }
            else
            {
                fillgridview();
            }
        }
    }
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_DeployJiangXiangInfoAddEdit.aspx?cmd=edit&ID={0}',900,500,'布奖管理')", ID);
        }
        else
        {
            return "";
        }
    }
    private void fillgridview()
    {
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            GridView1.DataSource =
                bll.GetListsByFilterString("CompID=" + GetCookieCompID() + " and " + DDLField.SelectedValue + " like '%" +
                                           inputSearchKeyword.Value.Trim() + "%'");
        }
        else
        {
            GridView1.DataSource = bll.GetListsByFilterString("CompID=" + GetCookieCompID());
        }
        GridView1.DataBind();
    }
    private const string Filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_DeployJiangXiangInfo where " + Filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_DeployJiangXiangInfo", Filtertemp, "DPJXID", "DPJXID", pageSize);
        GridView1.DataBind();
    }
    

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DeleteDeployJXDetail(GridView1.DataKeys[e.RowIndex]["DPJXID"].ToString().Trim());
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["DPJXID"].ToString().Trim()));
        fillgridview();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgridview();
    }

    private bool CheckCanBeDelete(string DPLID)
    {
        return bdjmanage.CheckIsExistByFilterString("DPJXID=" + DPLID);
    }

    public string HexiaoValue(string DPJXID)
    {
        return bdjmanage.GetListsByFilterString("DPJXID=" + DPJXID + " and DjFlag='1'").Count.ToString();
    }

    public string JieYuValue(string DPJXID, string PreOrderNum)
    {
        return
            (int.Parse(PreOrderNum) - bdjmanage.GetListsByFilterString("DPJXID=" + DPJXID + " and DjFlag='1'").Count)
                .ToString();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["DPJXID"].ToString()));
        mod.JxID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtJxID")).Text.Trim());
        mod.AgentID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtAgentID")).Text.Trim());
        mod.DjdID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtDjdID")).Text.Trim());
        mod.PreOderNum = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtPreOderNum")).Text.Trim());
        mod.ProID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtProID")).Text.Trim());
        mod.ChuKuDanHao = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtChuKuDanHao")).Text.Trim();
        mod.FaHuoPici = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtFaHuoPici")).Text.Trim();
        mod.ChuKuDateStart =
            Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtChuKuDateStart")).Text.Trim());
        mod.ChuKuDateEnd =
            Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtChuKuDateEnd")).Text.Trim());
        mod.Deploydate =
            Convert.ToDateTime(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtDeploydate")).Text.Trim());
        mod.UserID = Convert.ToInt32(((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtUserID")).Text.Trim());
        mod.Remarks = ((TextBox) GridView1.Rows[e.RowIndex].FindControl("txtRemarks")).Text.Trim();
        bll.Modify(mod);
        GridView1.EditIndex = -1;
        fillgridview();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillgridview();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
            var key = GridView1.DataKeys[e.Row.RowIndex];
            if (CheckCanBeDelete(GridView1.DataKeys[e.Row.RowIndex]["DPJXID"].ToString()))
            {
                e.Row.Cells[11].ForeColor = Color.Gray;
                e.Row.Cells[11].Enabled = false;
            }
            else
            {
                ((LinkButton) e.Row.Cells[11].Controls[0]).Attributes.Add("onclick",
                    "javascript:return confirm('你确认要删除当前记录,并撤销当前记录相关的奖项信息吗?')");
            }
        }
    }

    private void DeleteDeployJXDetail(string DPJXID)
    {
        IList<MTJ_ZJLabelCodeInfo> jxlablellist = bzjlabel.GetListsByFilterString("DPJXID=" + DPJXID, "LabelCode");
        if (jxlablellist.Count > 0)
        {
            string minlabelcode = jxlablellist[0].LabelCode;
            string maxlabelcode = jxlablellist[jxlablellist.Count - 1].LabelCode;
            string tablename = cwl.gettableinfo(minlabelcode, maxlabelcode, sqlconnwuliu,GetCookieCompID());
            string labelstring = "";
            foreach (MTJ_ZJLabelCodeInfo mzjlabel in jxlablellist)
            {
                labelstring += ",'" + mzjlabel.LabelCode + "'";
            }
            if (labelstring.StartsWith(","))
            {
                labelstring = labelstring.Substring(1);
            }
            if (labelstring.Length > 0)
            {
                SqlCommand sqlcomd = new SqlCommand();
                sqlcomd.Connection = sqlconnwuliu;
                string[] tablearray = tablename.Split(',');
                foreach (string table in tablearray)
                {
                    if (table != null && table.Length > 0)
                    {
                        sqlcomd.CommandText = "update " + table + "_BT set IsDJ=0 where BottleLabel in(" + labelstring +
                                              ")";
                        if (sqlconnwuliu.State != ConnectionState.Open)
                        {
                            sqlconnwuliu.Open();
                        }
                        sqlcomd.ExecuteNonQuery();
                    }
                }
            }
            tabex.ExecuteNonQuery("delete from TJ_ZJLabelCodeInfo where DPJXID=" + DPJXID);
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        fillgridview();
    }

    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        fillgridview();
    }
    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        _currentindex = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize);
    }
}