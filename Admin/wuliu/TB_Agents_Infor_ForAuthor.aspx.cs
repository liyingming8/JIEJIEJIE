using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using System.Web.UI.WebControls;
using TJ.BLL;
using commonlib;
using TJ.DBUtility;
using TJ.Model;

public partial class Admin_TB_Agents_Infor_ForAuthor : AuthorPage
{
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();
    public CommonFun commfun = new CommonFun();
    BTJ_RegisterCompanys btjRegister = new BTJ_RegisterCompanys();
    public int ctid = 0;
    readonly BTJ_DepartMent _btjDepart = new BTJ_DepartMent();
    BTJ_DepartMent_CompAuthor _btjDepartMentCompAuthor = new BTJ_DepartMent_CompAuthor();
    public string Parentstring = "";
    TabExecute tab = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL();
            LoadNewTree();
        }
    }

    private void FillDDL()
    {
        if (IsCompGrade())
        {
            DDL_DepartID.Visible = true;
            DDL_DepartID.DataSource = _btjDepart.GetListsByFilterString("compid=" + GetCookieCompID() + " and parentid=" + GetCookieTJDepartID());
            DDL_DepartID.DataBind();
        }
        else
        {
            DDL_DepartID.Visible = false;
        }
    }

    private void DisplayData()
    {
        string departid = GetCookieTJDepartID();
        if (!string.IsNullOrEmpty(departid) && !departid.Equals("0"))
        {
            IList<MTJ_DepartMent_CompAuthor> list =
                _btjDepartMentCompAuthor.GetListsByFilterString("departid=" + GetCookieTJDepartID());
            tvagent.Nodes.Add(new TreeNode("代理商", "0"));
            if (list.Count > 0)
            {
                foreach (MTJ_DepartMent_CompAuthor mod in list)
                {
                    tvagent.Nodes[0].ChildNodes.Add(new TreeNode(bll.GetList(mod.authorcompid).CompName,
                        mod.authorcompid.ToString()));
                }
            }
        }
    }

    private void LoadNewTree()
    {
        string departid = GetCookieTJDepartID();
        if (IsCompGrade())
        {
            departid = DDL_DepartID.SelectedValue;
        }
        if (!string.IsNullOrEmpty(departid))
        {
            string sqlstring =
                "select ta.authorcompnm nm,ta.authorcompid compid,(select count(r.CompID) from TJ_RegisterCompanys r where r.MasterID=2 and r.ParentID=ta.authorcompid) cnt from TJ_DepartMent_CompAuthor ta where ta.departid=" +
                departid+" order by nm";
            DataTable dttemp = tab.ExecuteQuery(sqlstring, null);
            if (dttemp.Rows.Count > 0)
            {
                foreach (DataRow row in dttemp.Rows)
                {
                    //首先清除树型控件内的结点
                    tvagent.Nodes.Clear();
                    if (string.IsNullOrEmpty(Parentstring))
                    {
                        Parentstring = "{ name: \"" + row["nm"]+ "\", id: \"" + row["compid"]+"\", count: 0, times: 1, isParent: " + (string.IsNullOrEmpty(row["cnt"].ToString())?"false":(int.Parse(row["cnt"].ToString())>0?"true":"false")) + " }";
                    }
                    else
                    {
                        Parentstring += ",{ name: \"" + row["nm"] + "\", id: \"" + row["compid"] + "\", count: 0, times: 1, isParent: " + (string.IsNullOrEmpty(row["cnt"].ToString()) ? "false" : (int.Parse(row["cnt"].ToString()) > 0 ? "true" : "false")) + " }";
                    }
                }
            }
            //IList<MTJ_DepartMent_CompAuthor> list =
            //   _btjDepartMentCompAuthor.GetListsByFilterString("departid=" + departid);
            //if (list.Count > 0)
            //{
            //    //首先清除树型控件内的结点
            //    tvagent.Nodes.Clear();
            //    foreach (MTJ_DepartMent_CompAuthor mod in list)
            //    {
            //        if (string.IsNullOrEmpty(Parentstring))
            //        {
            //            Parentstring = "{ name: \"" + mod.authorcompnm + "\", id: \"" + mod.authorcompid +
            //                           "\", count: 0, times: 1, isParent: "+ isparent(mod.authorcompid) + " }";
            //        }
            //        else
            //        {
            //            Parentstring += ",{ name: \"" + mod.authorcompnm + "\", id: \"" + mod.authorcompid +
            //                           "\", count: 0, times: 1, isParent: "+isparent(mod.authorcompid)+" }";
            //        }
            //        //TreeNode Node = new TreeNode();
            //        //Node.Text = mod.authorcompnm;
            //        //Node.Value = mod.authorcompid.ToString();
            //        //tvagent.Nodes.Add(Node);//增加父节点，这时直接往树TreeView1上加
            //        ////LoadAddSign(Node.ChildNodes, mod.authorcompid);//wsw更改
            //        //tvagent.CollapseAll();
            //    }
            //}
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('您尚未具备部门授权，该功能无法使用！');", true);
        }
    }

    private string isparent(int parentid)
    {
        string temp = tab.ExecuteQueryForSingleValue(
            "select count(CompID) as cnt from TJ_RegisterCompanys where MasterID=2 and ParentID=" + parentid);
        if (string.IsNullOrEmpty(temp))
        {
            return "false";
        }
        else
        {
            if (int.Parse(temp) > 0)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
    }

    /// <summary>
    /// 用于加载显示 树形控件 +号的,加载下面的一个节点。
    /// </summary> 
    /// <param name="tn"></param> 
    /// <param name="compid"></param>
    private void LoadAddSign(TreeNodeCollection tn, int compid) //wsw更改
    {
        IList<MTJ_RegisterCompanys> complist = btjRegister.GetListsByFilterString("ParentID=" + compid, "CompName");
        if (complist.Count > 0)
        {
            foreach (MTJ_RegisterCompanys mod in complist)
            {
                tn.Add(new TreeNode(mod.CompName, mod.CompID.ToString()));
            }
        }
    }
    public string XiangXiLinkString(string ID)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TB_Agents_InforAddEdit.aspx?cmd=edit&ID={0}',780,560,'经销商信息编辑')", ID);
        }
        else
        {
            return "";
        }
    }

    public string XiangXiLinkForDepartAuthorString(string agentid, string agentnm)
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('../TJ_DepartMentForAuthor.aspx?agentid={0}&agentnm={1}',780,560,'职能部门授权')", agentid, agentnm);
        }
        else
        {
            return "";
        }
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        LoadNewTree();
    }

    protected void DDL_DepartID_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadNewTree();
    }

    protected void tvagent_SelectedNodeChanged(object sender, EventArgs e)
    {

    }
}
