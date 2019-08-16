using System;
using System.Web.UI.WebControls;
using TJ.BLL;
using commonlib;
using TJ.DBUtility;
using Wuqi.Webdiyer;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Admin_TJ_WHBuJiang : AuthorPage
{
    public BTJ_User buser = new BTJ_User();
    TabExecute _tab = new TabExecute();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Display(1, AspNetPager1.PageSize);
        }
    }

    protected void Display(int pageIndex, int pageSize)
    {
        string Filtertemp = " Compid=" + GetCookieCompID();
        string fillSQL = "";
        if (inputSearchKeyword.Value.Length > 0)
        {
            Filtertemp += " and " + DDLField.SelectedValue + " like  '%" + inputSearchKeyword.Value + "%'";
            fillSQL+= " and " + DDLField.SelectedValue + " like  '%" + inputSearchKeyword.Value + "%'";
        }
        string mSQL = @"select top "+pageSize+@" * from (SELECT top 100 percent dd.ID,Remarks1,Remarks,BaoBianHao,beizhu,BJDate,beizhu1,cc.* from TB_SmallBuJiang dd left join (  select aa.DanHao,aa.total,case  when bb.used is null then 0 else bb.used end as used,(aa.total-(case  when bb.used is null then 0 else bb.used end)) as no_use from 
  (select DanHao, count(DanHao) as total from TJ_BaseLabelCodeInfo_2019 where compid = "+GetCookieCompID()+ @" group by DanHao)aa
     left join
     (select DanHao,case count(DanHao) when null then 0 else count(DanHao) end as used
   
      from TJ_BaseLabelCodeInfo_2019 where compid = " + GetCookieCompID() + @" and isdj = 1 group by DanHao)bb on aa.DanHao = bb.DanHao)cc on dd.BaoBianHao = cc.DanHao where compid = " + GetCookieCompID() + @"  "+ fillSQL + @" order by bjdate desc )ee where id not in

 (select top " + (pageIndex-1)*pageSize+ @" bt.ID from (SELECT top 100 percent dd.ID, Remarks1, Remarks, BaoBianHao, beizhu, BJDate, beizhu1, cc.* from TB_SmallBuJiang dd left join (  select aa.DanHao, aa.total,case  when bb.used is null then 0 else bb.used end as used, (aa.total - (case  when bb.used is null then 0 else bb.used end)) as no_use from
             (select DanHao, count(DanHao) as total from TJ_BaseLabelCodeInfo_2019 where compid = " + GetCookieCompID() + @" group by DanHao)aa
                left join
                (select DanHao,case count(DanHao) when null then 0 else count(DanHao) end as used
              
                 from TJ_BaseLabelCodeInfo_2019 where compid = " + GetCookieCompID() + @" and isdj = 1 group by DanHao)bb on aa.DanHao = bb.DanHao)cc on dd.BaoBianHao = cc.DanHao where compid = " + GetCookieCompID() + @"  "+ fillSQL + @")bt order by bjdate desc)  order by bjdate desc ";
        AspNetPager1.RecordCount = int.Parse(_tab.ExecuteQuery("select count(*) from TB_SmallBuJiang where " + Filtertemp, null).Rows[0][0].ToString());
        GridView1.DataSource = _tab.ExecuteNonQuery(mSQL);
        GridView1.DataBind();

    }

    protected void AspNetPager1_PageChanging(object src, PageChangingEventArgs e)
    {
        Display(e.NewPageIndex, AspNetPager1.PageSize);
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        Display(1, AspNetPager1.PageSize);
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
            {
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
                ((HtmlImage)e.Row.FindControl("editimg")).Attributes.Add("onclick", XiangXiLinkString(dataKey[0].ToString()));
                ((HyperLink)e.Row.FindControl("used")).Attributes.Add("onclick", XiangXiLinkStringUsed(dataKey["ID"].ToString()));
            } 
        }
    }

    private string XiangXiLinkStringUsed(string ID)
    {
        if (ID.Length>0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_WHBuJiangUsedAndEdit.aspx?ID=" + ID + "',660,500,'积分领取详细')");
        }
        else
        {
            return "";
        }
    }
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_WHBuJiangAndEdit.aspx?cmd=edit&ID=" + ID + "',660,500,'布奖管理')");
        }
        else
        {
            return "";
        }
    }

    protected string ReturnIsActive(string isActive)
    {
        if (isActive.Equals("1"))
        {
            return "是";
        }else
        {
            return "否";
        }
    }
}