using System;
using commonlib;
using TJ.DBUtility;
using System.Web.UI.WebControls;


public partial class Admin_TJ_MyCardAndDetails : AuthorPage
{
    readonly TabExecute mTabExecute = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //用户账号
            string mUserId = Request.QueryString["userid"];
            if (!string.IsNullOrEmpty(mUserId))
            {
                DetailsDisplay(mUserId);
            }
        }
    }

    /*
     * 卡片详情
     */
    private void DetailsDisplay(string mUserId)
    {
        string mSQL = @"SELECT (SELECT AwardThing FROM TJ_AwardInfo WHERE AWID=a.awid) as AwardThing,(SELECT awardtype FROM 
                        TJ_AwardType WHERE id=a.awtypeid) as awardtype,num FROM TJ_MyCard a WHERE userid='" + mUserId+ "' and compid='" + GetCookieCompID()+ "' order by awardtype";
        GridView1.DataSource = mTabExecute.ExecuteQuery(mSQL, null);
        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
           
        }
    }
}