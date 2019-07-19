using System;
using TJ.DBUtility;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Collections.Generic;
using System.Web.UI;
public partial class Admin_TB_Agents_Infor_TerminalAddEdit_ShenHe : AuthorPage
{
    private readonly BTJ_RegisterCompanys _bll = new BTJ_RegisterCompanys();
    private MTJ_RegisterCompanys _mod = new MTJ_RegisterCompanys();
    private readonly CommonFun _commfun = new CommonFun();
    private readonly BTB_CompAgentInfo _bcompagent = new BTB_CompAgentInfo();
    readonly TabExecute _tabexe = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim();
            } 
            FillDdl();
            Button1.Text = "审核通过";
            Fillinput(int.Parse(HF_ID.Value.Trim()));
            if (!string.IsNullOrEmpty(Request.QueryString["agid"]))
            {
                hdagentid.Value = Request.QueryString["agid"].Trim();
                hd_input_parentname.Value = Request.QueryString["agname"].Trim();
                inputparentid.Value = Request.QueryString["agname"].Trim();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["smuid"]))
            {
                hdcitymnid.Value = Request.QueryString["smuid"].Trim();
                hdcitymnnm.Value = Request.QueryString["smunm"].Trim();
                inputcitymanager.Value = Request.QueryString["smunm"].Trim();
            }
            inputparentid.Attributes.Add("onclick", XiangXiLinkString());
            inputcitymanager.Attributes.Add("onclick", XiangXiLinkStringForManagerSelect());
        }
    }

    public string XiangXiLinkString()
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TB_Agents_Infor_jxs_forselect.aspx?"+ (hd_input_parentname.Value.Length > 0 ? "pnm=" + hd_input_parentname.Value + "&" : "") + "fr=" + Sc.EncryptQueryString("TB_Agents_Infor_TerminalAddEdit_ShenHe.aspx?ID=" + HF_ID.Value+(string.IsNullOrEmpty(hdcitymnid.Value)?"": "&smuid="+hdcitymnid.Value+ "&smunm="+hdcitymnnm.Value)) + "',660,460,'指定父级经销商')");
        }
        return "";
    }

    public string XiangXiLinkStringForManagerSelect()
    {
        if (ID.Length > 0)
        {
            return string.Format("javascript:var win=openWinCenter('TJ_CityManager_Select.aspx?"+ (hdcitymnnm.Value.Length > 0 ? "ctmnnm=" +Sc.EncryptQueryString(hdcitymnnm.Value) + "&" : "") + "&fr=" + Sc.EncryptQueryString("TB_Agents_Infor_TerminalAddEdit_ShenHe.aspx?ID=" + HF_ID.Value+(string.IsNullOrEmpty(hdagentid.Value)?"": "&agid="+hdagentid.Value+ "&agname="+hd_input_parentname.Value)) + "',660,460,'指定城市经理')");
        }
        return "";
    }

    private void FillDdl()
    {  
        _commfun.BindTreeCombox(ComboBox_CID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "选择城市...", true,"-", "1=1");
        ComboBox_CID.SelectedValue = "0"; 
    }  

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hdagentid.Value)||hdagentid.Value.Equals("0"))
        { 
            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('请指定经销商信息！')", true);
        }
        else
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
                _mod.Agent_Code = inputAgent_Code.Value.Trim();
                _mod.LegalPerson = inputMiddleman.Value.Trim();
                _mod.MobilePhoneNumber = inputMobiePhone.Value.Trim();
                _mod.Address = inputAgent_Addrss.Value.Trim(); 
                _mod.ManagerName = inputcitymanager.Value;
                //_mod.ManagerUserID = int.Parse(string.IsNullOrEmpty(ddl_city_managerid.SelectedValue)?"0":ddl_city_managerid.SelectedValue);
                //_mod.ManagerName = (string.IsNullOrEmpty(ddl_city_managerid.SelectedValue)?"":ddl_city_managerid.SelectedItem.Text);
                switch (HF_CMD.Value.Trim())
                {
                    case "add":
                        object id = _bll.Insert(_mod);
                        HF_ID.Value = id.ToString();
                        Button1.Text = "修 改";
                        break;
                    case "edit":
                        _mod.CompTypeID = string.IsNullOrEmpty(ddl_comptype.SelectedValue) ? 486 : int.Parse(ddl_comptype.SelectedValue);
                        _bll.Modify(_mod);
                        break;
                }
                CreateCompanyAndAgentRelationShip();
                ActiveTerminalUser(HF_ID.Value);
                //HuYi_Info.HY_dxinfoAutoSign(inputMiddleman.Value.Trim() + "您好!您的资料已经通过审核,请用当前手机号码登录,初始密码为本手机号码后6位,谢谢!", inputMobiePhone.Value);
                HuYi_Info.HY_dxinfoAutoSign(inputMiddleman.Value.Trim() + "您好!您的资料已经通过审核,用户名:"+ inputMobiePhone.Value + ",初始密码:"+ inputMobiePhone.Value.Trim().Substring(inputMobiePhone.Value.Length-6) + ",谢谢!", inputMobiePhone.Value);
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alert", "alert('该经销商名称已经存在，不可重复申请！')", true); 
            }
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
        lab_gps_address.Text = ms.Position; 
        if (ms.BusinessLicencePicture.Length>0)
        {
            Image_yyzz.ImageUrl = "http://os.china315net.com/" + ms.BusinessLicencePicture;
        }
        inputparentid.Attributes.Add("placeholder", ms.Remarks.Trim());
        hd_input_parentname.Value = ms.Remarks.Trim();
        inputcitymanager.Attributes.Add("placeholder", ms.ManagerName);
        hdcitymnnm.Value = ms.ManagerName;
        //if (!ms.ManagerUserID.Equals(0))
        //{
        //    ddl_city_managerid.SelectedValue = ms.ManagerUserID.ToString();
        //}
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
    
    private void ActiveTerminalUser(string terminalid)
    {
        _tabexe.ExecuteNonQuery("update TJ_User set IsActived=1 where CompID=" + terminalid, null);
    }
}