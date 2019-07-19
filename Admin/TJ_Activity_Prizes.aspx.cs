using System;
using System.Drawing;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;

public partial class Admin_TJ_Activity_Prizes : AuthorPage
{
    BTJ_Activity_Prizes bll = new BTJ_Activity_Prizes();
    public BTJ_AwardType BtjAwardType = new BTJ_AwardType();
    MTJ_Activity_Prizes mod = new MTJ_Activity_Prizes(); 
    public BTJ_Activity BtjActivity = new BTJ_Activity(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["acid"]))
            {
                hf_acid.Value = Sc.DecryptQueryString(Request.QueryString["acid"].Trim());
                hf_acname.Value = BtjActivity.GetList(int.Parse(hf_acid.Value)).AName; 
                DisplayData();
            }
            else
            {
                Response.End();
            } 
        }
    } 

    public string GetActivityName()
    {
        return hf_acname.Value;
    }
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_Activity_PrizesAddEdit.aspx?cmd=" + Sc.EncryptQueryString("edit") + "&ID={0}&acid={1}',600,420,'活动奖项')", Sc.EncryptQueryString(ID),Sc.EncryptQueryString(hf_acid.Value));
        }
        else
        {
            return "";
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         var dataKey = GridView1.DataKeys[e.RowIndex];
        if (dataKey != null)
        {
            bll.Delete(int.Parse(dataKey["id"].ToString()));
            DisplayData();
        } 
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#C5ECFB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            ((Label) e.Row.FindControl("Labelacid")).Text = hf_acname.Value;
            var dataKey = GridView1.DataKeys[e.Row.RowIndex];
            if (dataKey != null)
            {
                e.Row.Attributes.Add("ondblclick", XiangXiLinkString(dataKey[0].ToString()));
                ((Label) e.Row.FindControl("lab_activity_prize_discription")).Text =
                    GetActivityPrizeJieShao(dataKey[0].ToString());
            } 
            ((Label)e.Row.FindControl("LabelIndex")).Text = (e.Row.RowIndex + 1).ToString();
            if (IsSuperAdmin())
            {
                ((LinkButton)e.Row.Cells[6].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除当前记录吗?')");
            }
            else
            {
                 e.Row.Cells[6].Enabled= false;
                 e.Row.Cells[6].ForeColor = Color.LightGray;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
             Label LabelpercentvlTotal = (Label)e.Row.FindControl("LabelpercentvlTotal");
            decimal bilvall = 0;
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    bilvall+=Convert.ToDecimal(((Label)row.FindControl("Labelpercentvl")).Text);
                }
            }
            LabelpercentvlTotal.Text = bilvall.ToString();
        }
    }

    private string _tempvalue=string.Empty;
    readonly BTJ_AwardInfo _btjAwardInfo = new BTJ_AwardInfo();
    private string GetActivityPrizeJieShao(string acprid)
    {
        _tempvalue = string.Empty;
        mod = bll.GetList(int.Parse(acprid));
        switch (mod.awtype)
        {
            case 1:
                _tempvalue = "红包金额:￥" + mod.prizevalue;
                break;
            case 2:
                _tempvalue = "积分值:" + mod.prizevalue.ToString("0");
                break;
            case 3:
                _tempvalue = _btjAwardInfo.GetList(mod.awid).AwardThing;
                break;
            case 4:
                _tempvalue = _btjAwardInfo.GetList(mod.awid).AwardThing;
                break;
        }
        return _tempvalue;
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    { 
        DisplayData();
    }

    private string _filtertemp ="1=1";
    private void DisplayData()
    {
        if (hf_acid.Value.Length > 0)
        {
            _filtertemp = "acid=" + hf_acid.Value;
            GridView1.DataSource = bll.GetListsByFilterString(_filtertemp);
            GridView1.DataBind();
        } 
    } 
}
