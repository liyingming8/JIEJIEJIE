using System;
using TJ.DBUtility;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Collections.Generic;
using System.Web.UI;
public partial class Admin_TB_Agents_Infor_TerminalAddEdit : AuthorPage
{
    private readonly BTJ_RegisterCompanys _bll = new BTJ_RegisterCompanys();
    private MTJ_RegisterCompanys _mod = new MTJ_RegisterCompanys();
    private readonly CommonFun _commfun = new CommonFun();
    private readonly CommonFunWL _commfunwl = new CommonFunWL();   
    private readonly BTB_CompAgentInfo _bcompagent = new BTB_CompAgentInfo();
    readonly TabExecute _tabexe = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
                if (GetCookieCompID().Equals(DAConfig.JGCompID))
                {
                    inputAgent_Code.Disabled = false;
                }
                //if (HF_CMD.Value.ToLower().Trim().Equals("add"))
                //{
                //    inputAgent_Code.Value = _commfunwl.CreateAutoCode(GetCookieCompID(), "T"); 
                //}
                //if (inputAgent_Code.Value.Trim().Length == 0)
                //{
                //    inputAgent_Code.Value = _commfunwl.CreateAutoID(GetCookieCompID(), "T");
                //}
            }
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim();
            } 
            FillDdl();
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加"; 
                    break;
                case "edit":
                    Button1.Text = "修改";
                    Fillinput(int.Parse(HF_ID.Value.Trim()));
                    break; 
            }
            if (!string.IsNullOrEmpty(Request.QueryString["agid"]))
            {
                hdagentid.Value = Request.QueryString["agid"].Trim();
                inputparentid.Value = Request.QueryString["agname"].Trim();
            }
            if (GetCookieCompTypeID().Equals(DAConfig.CompTypeIDJingXiaoShang.ToString()))
            {
                inputparentid.Value = GetCookieTJUName();
                hdagentid.Value = GetCookieCompID();
            }
            else
            {
                inputparentid.Attributes.Add("onclick", XiangXiLinkString());
            } 
        }
    }

    public string XiangXiLinkString()
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TB_Agents_Infor_jxs_forselect.aspx?fr=" + Sc.EncryptQueryString("TB_Agents_Infor_TerminalAddEdit.aspx?cmd=" + HF_CMD.Value + (HF_ID.Value.Trim().Equals("") ? "" : "&ID=" + HF_ID.Value)) + "',660,480,'指定父级经销商')");
        }
        return "";
    }

    private void FillDdl()
    {  
        _commfun.BindTreeCombox(ComboBox_CID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "选择城市...", true,"-", "1=1");
        ComboBox_CID.SelectedValue = "0";
        ddl_city_managerid.DataSource = _tabexe.ExecuteQuery(
            "select UserID,LoginName from TJ_User where RID=168 and departid=" + GetCookieTJDepartID() + " order by LoginName", null);
        ddl_city_managerid.DataBind();
    }  

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!CheckCompnameIsExist(inputAgent_Name.Value, ComboBox_CID.SelectedValue, HF_CMD.Value))
        {
            if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
            {
                _mod = _bll.GetList(Convert.ToInt32(HF_ID.Value));
            }
            _mod.CTID = Convert.ToInt32(ComboBox_CID.SelectedValue.Trim());
            _mod.ParentID = int.Parse(hdagentid.Value);
            _mod.CompTypeID = string.IsNullOrEmpty(ddl_comptype.SelectedValue) ? 486 : int.Parse(ddl_comptype.SelectedValue);
            _mod.DisAuthorDate = DateTime.Now.AddYears(20);
            _mod.AuthoredDate = DateTime.Now;
            _mod.RegisterDate = DateTime.Now;
            _mod.CompName = inputAgent_Name.Value.Trim(); 
            _mod.LegalPerson = inputMiddleman.Value.Trim(); 
            _mod.MobilePhoneNumber = inputMobiePhone.Value.Trim(); 
            _mod.Remarks = inputRemarks.Value.Trim();
            _mod.Address = inputAgent_Addrss.Value.Trim();
            _mod.AllowAreaInfo = hd_allow_area.Value;
            _mod.AllowProduct = hd_allow_product.Value;
            _mod.ManagerUserID = int.Parse(ddl_city_managerid.SelectedValue);
            _mod.ManagerName= ddl_city_managerid.SelectedItem.Text;
            switch (HF_CMD.Value.Trim())
            {
                case "add":
                    _mod.Agent_Code = _commfunwl.CreateAutoCode(GetCookieCompID(),"T");
                    object id = _bll.Insert(_mod);
                    HF_ID.Value = id.ToString();
                    Button1.Text = "修 改"; 
                    break;
                case "edit":
                    _mod.CompTypeID = string.IsNullOrEmpty(ddl_comptype.SelectedValue)?486:int.Parse(ddl_comptype.SelectedValue);
                    _bll.Modify(_mod); 
                    break;
            }  
            CreateCompanyAndAgentRelationShip(); 
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true); 
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('该终端店名称已经存在!')", true); 
        } 
    }  

    private bool _returnValue;

    private bool CheckCompnameIsExist(string compName, string cityID, string cmd)
    {
        _returnValue = false;
        switch (cmd.ToLower())
        {
            case "add":
                _returnValue = _bll.CheckIsExistByFilterString("CompName='" + compName + "' and CTID=" + cityID);
                break;
            case "edit":
                _returnValue = (_bll.GetListsByFilterString("CompName='" + compName + "' and CTID=" + cityID).Count > 1);
                break;
        }
        return _returnValue;
    }

    private void Fillinput(int id)
    {
        MTJ_RegisterCompanys ms = _bll.GetList(id);
        hdagentid.Value = ms.ParentID.ToString();
        inputparentid.Value = _bll.GetList(ms.ParentID).CompName;
        ComboBox_CID.SelectedValue = ms.CTID.ToString().Trim();
        inputAgent_Name.Value = ms.CompName.Trim();
        ddl_comptype.SelectedValue = ms.CompTypeID.ToString();
        if (ms.Agent_Code.Length > 0)
        {
            inputAgent_Code.Value = ms.Agent_Code.Trim();
        } 
        inputMiddleman.Value = ms.LegalPerson.Trim(); 
        inputMobiePhone.Value = ms.MobilePhoneNumber.Trim(); 
        inputAgent_Addrss.Value = ms.Address;
        inputRemarks.Value = ms.Remarks.Trim();
        if (!ms.ManagerUserID.Equals(0))
        {
            ddl_city_managerid.SelectedValue = ms.ManagerUserID.ToString();
        }
    }

    private void CreateCompanyAndAgentRelationShip()
    {
        var mcompagent = new MTB_CompAgentInfo();
        IList<MTB_CompAgentInfo> compagentlist =
                _bcompagent.GetListsByFilterString("CompID=" + hdagentid.Value + " and AgentID=" + HF_ID.Value);
        if (compagentlist.Count > 0)
        {
            mcompagent = compagentlist[0];
            mcompagent.Middleman = inputMiddleman.Value;
            mcompagent.PhoneNumber = inputMobiePhone.Value;
            _bcompagent.Modify(mcompagent);
        }
        else
        {
            mcompagent.CreateDate = DateTime.Now;
            mcompagent.CompID = int.Parse(hdagentid.Value);
            mcompagent.AgentID = int.Parse(HF_ID.Value);
            mcompagent.Middleman = inputMiddleman.Value;
            mcompagent.PhoneNumber = inputMobiePhone.Value;
            _bcompagent.Insert(mcompagent);
        } 
    } 
}