using System;
using System.Data;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Collections.Generic;
using System.Web.UI; 
public partial class Admin_TB_Agents_InforAddEdit : AuthorPage
{
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();
    private MTJ_RegisterCompanys mod = new MTJ_RegisterCompanys();
    private readonly CommonFun commfun = new CommonFun();
    private readonly CommonFunWL commfunwl = new CommonFunWL();   
    private readonly BTB_CompAgentInfo bcompagent = new BTB_CompAgentInfo();
    private readonly BTJ_User buser = new BTJ_User();
    InternetHandle ihandle = new InternetHandle(); 
    InternetHandle nethandle = new InternetHandle();

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
                    btnDelete.Style["display"] = "none";//新增界面隐藏删除按钮
                    break;
                case "edit":
                    Button1.Text = "修改";
                    Fillinput(int.Parse(HF_ID.Value.Trim()));
                    break;
                default:
                    break;
            }
        }
    }

    private string tempstring = "";
    private void FillDdl()
    { 
        if(!string.IsNullOrEmpty(HF_ID.Value)&&!HF_ID.Value.Trim().Equals("0"))
        {
            //FillCheckList(HF_ID.Value);
            string temp = nethandle.GetUrlData("http://117.34.70.23:32176/gis/authorized_area/" + HF_ID.Value + "/");
            JArray obj = JArray.Parse(temp);
            var dttemp = new DataTable();
            dttemp.Columns.Add("id", typeof(Int32));
            dttemp.Columns.Add("vl", typeof(string));
            tempstring = "";
            foreach (var jToken in obj)
            {
                var itemArray = (JArray)jToken;
                dttemp.Rows.Add(itemArray[0].ToString(), itemArray[1].ToString());
                tempstring += ","+ itemArray[1].ToString();
            }
            /*
            checkboxlist_author_area.DataSource = dttemp;
            checkboxlist_author_area.DataBind();
            hd_allow_area.Value = tempstring.StartsWith(",") ? tempstring.Substring(1) : tempstring;
            foreach (ListItem item in checkboxlist_author_area.Items)
            {
                item.Selected = true;
            }
            */
            dttemp.Dispose();
        } 
        commfun.BindTreeCombox(ComboBox_CID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "选择城市...", true,"-", "1=1");
        ComboBox_CID.SelectedValue = "0"; 
        commfun.BindTreeCombox(ddl_comptype,"CName","CID","ParentID","TJ_BaseClass",DAConfig.CompanyType,"公司类型...",true,"-","CID in (486,48)");
        ddl_comptype.SelectedValue = "48";

        if (GetCookieCompID() == "130")
        {
            qujl.Visible = true;
            ComboBox_QUJL.DataSource = buser.GetListsByFilterString("CompID=" + GetCookieCompID() + " and RID=28");
            ComboBox_QUJL.DataBind();
            ComboBox_QUJL.SelectedValue = "0";
        }
    } 
    /*
    private void FillCheckList(string AgentID)
    {  
        CheckBoxList_PermitList.DataSource = db.GetAuthorProductInfo(GetCookieCompID(), AgentID);
        CheckBoxList_PermitList.DataBind();
        tempstring = "";
        foreach(ListItem item in CheckBoxList_PermitList.Items)
        {
            item.Selected = true;
            tempstring += ","+item.Text;
        }
        hd_allow_product.Value = tempstring.StartsWith(",") ? tempstring.Substring(1) : tempstring;
    }
    */

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!CheckCompnameIsExist(HF_ID.Value, inputAgent_Name.Value, ComboBox_CID.SelectedValue, HF_CMD.Value))
        {
            if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
            {
                mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
            }
            mod.CTID = Convert.ToInt32(ComboBox_CID.SelectedValue.Trim());
            mod.ParentID = int.Parse(GetCookieCompID());
            mod.CompTypeID = int.Parse(ddl_comptype.SelectedValue);
            mod.DisAuthorDate = DateTime.Now.AddYears(20);
            mod.AuthoredDate = DateTime.Now;
            mod.RegisterDate = DateTime.Now;
            mod.CompName = inputAgent_Name.Value.Trim();
            mod.Agent_Code = inputAgent_Code.Value.Trim();
            mod.LegalPerson = inputMiddleman.Value.Trim(); 
            mod.MobilePhoneNumber = inputMobiePhone.Value.Trim(); 
            mod.Remarks = inputRemarks.Value.Trim();
            mod.Address = inputAgent_Addrss.Value.Trim();
            mod.AllowAreaInfo = hd_allow_area.Value;
            mod.AllowProduct = hd_allow_product.Value;
            switch (HF_CMD.Value.Trim())
            {
                case "add":
                    mod.Agent_Code = commfunwl.CreateAutoCode(GetCookieCompID(), "A");
                    object id = bll.Insert(mod);
                    HF_ID.Value = id.ToString();
                    Button1.Text = "修 改";
                    if (GetCookieCompID().Equals(DAConfig.JGCompID))
                    {
                        string ajson = "[{\"code\": \"" + mod.Agent_Code + "\",\"name\": \""+mod.CompName+"\"}]";
                        ihandle.Web_Go(ajson, "POST", ihandle.GetTouYunJiuGuiToken(), "data-service/isv/franchisers/add?companyCode=JGJ");//新增
                        //ihandle.web_post_frank("POST",ajson, ihandle.GetTouYunJiuGuiToken(),"data-service/isv/franchisers/add?companyCode=JGJ");
                    }
                    break;
                case "edit":
                    mod.CompTypeID = DAConfig.CompTypeIDJingXiaoShang;
                    bll.Modify(mod);
                    if (GetCookieCompID().Equals(DAConfig.JGCompID))
                    {
                        string mjson = "[{\"code\": \""+mod.Agent_Code+"\",\"name\": \""+mod.CompName+"\"}]";
                        //ihandle.web_post_frank("PUT",mjson, ihandle.GetTouYunJiuGuiToken(),"data-service/isv/franchisers/edit?companyCode=JGJ");
                        ihandle.Web_Go(mjson, "PUT", ihandle.GetTouYunJiuGuiToken(), "data-service/isv/franchisers/edit?companyCode=JGJ"); //修改
                    }
                    break;
            }  
            CreateCompanyAndAgentRelationShip();
            //RecordAuthorProduct(); 
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true); 
        }
        else
        {
            Response.Write("<script>alert('该经销商名称已经存在，不可更改！');</script>");
        } 
    } 
    /*
    private void RecordAuthorProduct()
    {
        foreach (ListItem item in CheckBoxList_PermitList.Items)
        {
            if (bproductauthor.CheckIsExistByFilterString("AgentID=" + HF_ID.Value + " and ProdID=" + item.Value))
            {
                if (!item.Selected)
                {
                    bproductauthor.Delete("AgentID=" + HF_ID.Value + " and ProdID=" + item.Value);
                }
            }
            else
            {
                if (item.Selected)
                {
                    bproductauthor.Insert(new MTB_ProductAuthorForAgent(0, int.Parse(GetCookieCompID()),
                        int.Parse(HF_ID.Value), int.Parse(item.Value), DateTime.Now, DateTime.Now, true,
                        int.Parse(GetCookieUID()), ""));
                }
            }
        }
    }
    */

    private bool ReturnValue;

    private bool CheckCompnameIsExist(string CompID, string CompName, string CityID, string cmd)
    {
        ReturnValue = false;
        switch (cmd.ToLower())
        {
            case "add":
                ReturnValue = bll.CheckIsExistByFilterString("CompName='" + CompName + "' and CTID=" + ComboBox_CID.SelectedValue);
                break;
            case "edit":
                ReturnValue = (bll.GetListsByFilterString("CompName='" + CompName + "' and CTID=" + ComboBox_CID.SelectedValue).Count <= 1)? false: true;
                break;
        }
        return ReturnValue;
    }

    private void Fillinput(int id)
    {
        MTJ_RegisterCompanys ms = bll.GetList(id);
        ComboBox_CID.SelectedValue = ms.CTID.ToString().Trim();
        inputAgent_Name.Value = ms.CompName.Trim();
        ddl_comptype.SelectedValue = ms.CompTypeID.ToString();
        if (ms.Agent_Code.Length > 0)
        {
            inputAgent_Code.Value = ms.Agent_Code.Trim();
        }

        if (GetCookieCompID() == "130")
        {
            foreach (MTB_CompAgentInfo mc in bcompagent.GetListsByFilterString("AgentID=" + id))
            {
                ComboBox_QUJL.SelectedValue = mc.Remarks;
            }
            if (GetCookieRID() != "28" && GetCookieRID() != "15" && GetCookieRID() != "25")
            {
                Button1.Enabled = false;
            }
        } 
        inputMiddleman.Value = ms.LegalPerson.Trim(); 
        inputMobiePhone.Value = ms.MobilePhoneNumber.Trim(); 
        inputAgent_Addrss.Value = ms.Address;
        inputRemarks.Value = ms.Remarks.Trim();
    }

    private void CreateCompanyAndAgentRelationShip()
    {
        MTB_CompAgentInfo mcompagent = new MTB_CompAgentInfo();
        if (HF_CMD.Value.ToLower().Trim().Equals("edit"))
        {
            IList<MTB_CompAgentInfo> compagentlist =
                bcompagent.GetListsByFilterString("CompID=" + GetCookieCompID() + " and AgentID=" + HF_ID.Value);
            if (compagentlist.Count > 0)
            {
                mcompagent = compagentlist[0]; 
                mcompagent.Middleman = inputMiddleman.Value;
                mcompagent.PhoneNumber = inputMobiePhone.Value;
                bcompagent.Modify(mcompagent);
            }
        }
        else
        {
            mcompagent.CreateDate = DateTime.Now;
            mcompagent.CompID = int.Parse(GetCookieCompID());
            mcompagent.AgentID = int.Parse(HF_ID.Value);
            mcompagent.Middleman = inputMiddleman.Value;
            mcompagent.PhoneNumber = inputMobiePhone.Value; 

            if (GetCookieCompID() == "130")
            {
                mcompagent.Remarks = ComboBox_QUJL.SelectedValue.Trim();
            }

            bcompagent.Insert(mcompagent);
        }
    }

    /*
         * 删除
         */
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
        {
            int deleteId = Convert.ToInt32(Request.QueryString["ID"].Trim());
            bll.Delete(deleteId);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
    }

}