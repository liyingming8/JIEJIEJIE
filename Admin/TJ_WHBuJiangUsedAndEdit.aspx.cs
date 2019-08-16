using System;
using TJ.DBUtility;
using Wuqi.Webdiyer;
using System.Web.UI.WebControls;
using commonlib;

public partial class Admin_TJ_WHBuJiangUsedAndEdit : AuthorPage
{
    int currentPage = 1;
    TabExecute _tab = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                hf_id.Value = Request.QueryString["ID"].Trim();
            }
            DisplayData(1, AspNetPager1.PageSize, hf_id.Value);
        }
    }

    protected void DisplayData(int pageIndex,int pageSize,string ID)
    {
        string mBaoBianHaoSQL = "select BaoBianHao from TB_SmallBuJiang where ID="+ID;
        string mBaoBianHao=_tab.ExecuteQueryForSingleValue(mBaoBianHaoSQL);
        string mLabelCode = "";
        if (inputSearchKeyword.Value.Length>0)
        {
            mLabelCode = inputSearchKeyword.Value.ToString();
        }
        if (!string.IsNullOrEmpty(mBaoBianHao))
        {
            string mLabelIsDJ = @"select top "+ pageSize + @" aa.* from (select top 100 percent aw.ID,aw.codestr,aw.gettm,aw.compid,bc.LabelCodeInfo,bc.DanHao,aw.prizevl,us.NickName from TJ_Activity_Win_2018 
                                            aw left join TJ_BaseLabelCodeInfo_2019  bc 
                                            on bc.LabelCodeInfo=aw.codestr left join TJ_User us on aw.userid=us.UserID where codestr is not null and len(codestr)>0 and 
                                            LabelCodeInfo is not null and len(LabelCodeInfo)>0 and bc.ISDJ=1 and aw.compid=" + GetCookieCompID() + @" and bc.danhao='" + mBaoBianHao + @"'  
                                             and bc.LabelCodeInfo like '%" + mLabelCode + @"%'  order by id asc)aa where id not in
                                            (select top " + (pageIndex-1)*pageSize + @" aw.ID from TJ_Activity_Win_2018  aw left join TJ_BaseLabelCodeInfo_2019  bc 
                                            on bc.LabelCodeInfo=aw.codestr left join TJ_User us on aw.userid=us.UserID where codestr is not null and len(codestr)>0 and 
                                            LabelCodeInfo is not null and len(LabelCodeInfo)>0 and bc.ISDJ=1 and aw.compid=" + GetCookieCompID() + @" and bc.danhao='" + mBaoBianHao + @"' 
                                             and bc.LabelCodeInfo like '%" + mLabelCode + @"%' order by id asc) order by id asc";
            AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery(@"select  count(aw.ID) from TJ_Activity_Win_2018  aw left join TJ_BaseLabelCodeInfo_2019  bc 
                                            on bc.LabelCodeInfo = aw.codestr left join TJ_User us on aw.userid = us.UserID where codestr is not null and len(codestr) > 0 and
                                            LabelCodeInfo is not null and len(LabelCodeInfo) > 0 and bc.ISDJ = 1 and aw.compid = " + GetCookieCompID() + @" and bc.danhao = '" + mBaoBianHao + @"'
                                             and bc.LabelCodeInfo like '%" + mLabelCode + @"%'  ", null).Rows[0][0].ToString());
            GridView1.DataSource = _tab.ExecuteNonQuery(mLabelIsDJ);
            GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            ((Label)e.Row.FindControl("LabelIndex")).Text = (AspNetPager1.PageSize * (currentPage - 1) + e.Row.RowIndex + 1).ToString();   
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        DisplayData(1, AspNetPager1.PageSize, hf_id.Value);
    }

    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        currentPage = e.NewPageIndex;
        DisplayData(e.NewPageIndex, AspNetPager1.PageSize, hf_id.Value);
    }
}