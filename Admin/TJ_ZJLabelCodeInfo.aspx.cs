using System;
using System.Reflection;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;
using System.Data;
using System.Collections.Generic;

public partial class Admin_TJ_ZJLabelCodeInfo : AuthorPage
{
    private readonly BTJ_ZJLabelCodeInfo bll = new BTJ_ZJLabelCodeInfo();
    private MTJ_ZJLabelCodeInfo mod = new MTJ_ZJLabelCodeInfo();
    private BTJ_JXInfo bjx = new BTJ_JXInfo();
    public BTJ_JXInfo bjxinfo = new BTJ_JXInfo();
    private int _currentindex = 1;
    readonly TabExecute _tab = new TabExecute();

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

    private void FillDDL()
    {
        DDLJX.DataSource = bjxinfo.GetListsByFilterString("CompID=" + GetCookieCompID());
        DDLJX.DataBind();
    }

    private void fillgridview()
    {
        ////===========wyz==20170921======================
        //DataTable dt11 = new DataTable();
        ////===========wyz==20170921======================
        if (inputSearchKeyword.Value.Trim().Length > 0)
        {
            GridView1.DataSource =
                bll.GetListsByFilterString("CompID=" + GetCookieCompID() +
                                           (DDLJX.SelectedValue != "0" ? " and JxID=" + DDLJX.SelectedValue : "") +
                                           " and " + DDLField.SelectedValue + " like '%" +
                                           inputSearchKeyword.Value.Trim() + "%'");
            ////===========wyz==20170921==================================================================================
            //dt11 = ConvertToDataSet(bll.GetListsByFilterString("CompID=" + GetCookieCompID() +
            //                               (DDLJX.SelectedValue != "0" ? " and JxID=" + DDLJX.SelectedValue : "") +
            //                               " and " + DDLField.SelectedValue + " like '%" +
            //                               inputSearchKeyword.Value.Trim() + "%'")).Tables[0];
            ////===========wyz==20170921==================================================================================
        }
        else
        {
            GridView1.DataSource =
                bll.GetListsByFilterString("CompID=" + GetCookieCompID() +
                                           (DDLJX.SelectedValue != "0"
                                               ? " and JxID=" + DDLJX.SelectedValue
                                               : "" + "ORDER BY ZJCDID DESC"));
            ////===========wyz==20170921==================================================================================
            //dt11 = ConvertToDataSet(bll.GetListsByFilterString("CompID=" + GetCookieCompID() +
            //                               (DDLJX.SelectedValue != "0"
            //                                   ? " and JxID=" + DDLJX.SelectedValue
            //                                   : "" + "ORDER BY ZJCDID DESC"))).Tables [0];
            ////===========wyz==20170921==================================================================================
        }
        GridView1.DataBind();
        //AspNetPager1.RecordCount = dt11.Rows.Count;// int.Parse(_tab.ExecuteQuery("select count(*) from TJ_ZJLabelCodeInfo where " + Filtertemp, null).Rows[0][0].ToString());
        //AspNetPager1.Visible = false;//因无法给_tab.ExecuteQueryByProPagerNew一些确切的参数，所以暂时不显示此分页情况

    }
    private const string Filtertemp = "1=1";
    private void DisplayData(int pageIndex, int pageSize)
    {
        //AspNetPager1.Visible = true ;
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TJ_ZJLabelCodeInfo where " + Filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteQueryByProPagerNew(pageIndex, "TJ_ZJLabelCodeInfo", Filtertemp, "ZJCDID", "ZJCDID", pageSize);
        GridView1.DataBind();
    }
    
    //=====================wyz---20170921=============================    
    /// <summary>  
        /// List<T>转换成DataTable  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="list"></param>  
        /// <returns></returns>  
        public static DataSet ConvertToDataSet<T>(IList<T> list)  
        {  
            if (list == null || list.Count <= 0)  
            {  
                return null;  
            }  
  
            DataSet ds = new DataSet();  
            DataTable dt = new DataTable(typeof(T).Name);  
            DataColumn column;  
            DataRow row;  
  
            PropertyInfo[] myPropertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);  
  
            foreach (T t in list)  
            {  
                if (t == null)  
                {  
                    continue;  
                }  
  
                row = dt.NewRow();  
  
                for (int i = 0, j = myPropertyInfo.Length; i < j; i++)  
                {  
                    PropertyInfo pi = myPropertyInfo[i];  
  
                    string name = pi.Name;  
  
                    if (dt.Columns[name] == null)  
                    {  
                        column = new DataColumn(name, pi.PropertyType);  
                        dt.Columns.Add(column);  
                    }  
  
                    row[name] = pi.GetValue(t, null);  
                }  
  
                dt.Rows.Add(row);  
            }  
  
            ds.Tables.Add(dt);  
  
            return ds;  
        }  
   
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bll.Delete(int.Parse(GridView1.DataKeys[e.RowIndex]["ZJCDID"].ToString()));
        fillgridview();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgridview();
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
        mod = bll.GetList(int.Parse(GridView1.DataKeys[e.RowIndex]["ZJCDID"].ToString()));
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
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
            }
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