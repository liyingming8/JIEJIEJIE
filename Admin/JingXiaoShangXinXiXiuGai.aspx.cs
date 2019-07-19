using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.DBUtility;

public partial class Admin_JingXiaoShangXinXiXiuGai : System.Web.UI.Page
{
    TabExecute tab = new TabExecute();
    TabExecutewuliu tabwuliu = new TabExecutewuliu();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        

    }

    protected void btn_chaxun_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = tab.ExecuteQuery(
            "SELECT CompName,CompID,RegisterDate FROM TJ_RegisterCompanys where ParentID=2 and CompName like '%" +
            txt_jingxiaoshang.Value + "%'", null);
        GridView1.DataBind();
    }

    public string FaHuoQingKuang(string AgentID)
    {
        return tabwuliu.ExecuteQueryForSingleValue("select count(FHID) as cnt from TB_FaHuoInfo_2 where AgentID=" + AgentID);
    }

    public string XiaJiShuLiang(string AgentID)
    {
        return tab.ExecuteQueryForSingleValue(
            "select count(CompID) as cnt from TJ_RegisterCompanys where CompTypeID=486 and ParentID=" + AgentID);
    }



    protected void btn_do_Click(object sender, EventArgs e)
    {
        string saveagentid = string.Empty;
        string clearagentidstring = string.Empty;
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                if (((CheckBox) row.FindControl("ckb_save")).Checked&&saveagentid.Length.Equals(0))
                {
                    saveagentid = ((HiddenField) row.FindControl("hd_agentid")).Value;
                }
                else
                {
                    if (((CheckBox) row.FindControl("ckb_contain")).Checked)
                    {
                        if (string.IsNullOrEmpty(clearagentidstring))
                        {
                            clearagentidstring = ((HiddenField)row.FindControl("hd_agentid")).Value;
                        }
                        else
                        {
                            clearagentidstring += "," + ((HiddenField)row.FindControl("hd_agentid")).Value;
                        }
                    } 
                }
            } 
        }

        if (string.IsNullOrEmpty(saveagentid))
        {
            ClientScript.RegisterStartupScript(this.GetType(),"alert","alert('请指定保留经销商！')"); 
        }
        else
        {
            if (string.IsNullOrEmpty(clearagentidstring))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('无需处理！')");
            }
            else
            {
                //1、获取修改经销商ID涉及到的表名信息
                DataTable dttables;
                if (ckb_all.Checked)
                {
                    dttables = tabwuliu.ExecuteQuery("select distinct left(TableNameInfo,5) from TB_FaHuoInfo_2 where AgentID in (" + clearagentidstring+","+saveagentid + ")", null);
                }
                else
                {
                    dttables = tabwuliu.ExecuteQuery("select distinct left(TableNameInfo,5) from TB_FaHuoInfo_2 where AgentID in (" + clearagentidstring + ")", null);
                } 
                //2、修改发货汇总信息
                tabwuliu.ExecuteNonQuery(
                    "update TB_FaHuoInfo_2 set AgentID=" + saveagentid + " where AgentID in(" + clearagentidstring +")", null);
                //3、更新发货明细
                string tempsqlstring = string.Empty;
                foreach (DataRow dr in dttables.Rows)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        tempsqlstring = "update " + dr[0] + "_" + (100 + i).ToString().Substring(1) +
                                               "_FH set ToAgentID=" + saveagentid + " where ToAgentID in(" +
                                               clearagentidstring + ")";
                        int temp = tabwuliu.ExecuteNonQuery(tempsqlstring,null);
                    } 
                }
                //4、为终端店更换父级经销商ID
                tab.ExecuteNonQuery(
                    "update TJ_RegisterCompanys set ParentID=" + saveagentid +
                    " where CompTypeID=486 and MasterID=2 and ParentID in(" + clearagentidstring + ")", null);
                //5、更新部门授权信息
                tab.ExecuteNonQuery(
                    "update TJ_DepartMent_CompAuthor set authorcompid=" + saveagentid + " where authorcompid in(" +
                    clearagentidstring + ")", null); 
            }
        }
    }
}