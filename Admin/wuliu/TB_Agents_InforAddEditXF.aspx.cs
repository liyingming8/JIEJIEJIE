using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using System.Text;
using commonlib;
using System.Collections.Generic;

public partial class Admin_TB_Agents_InforAddEditXF : AuthorPage
{
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();
    private MTJ_RegisterCompanys mod = new MTJ_RegisterCompanys();
    private readonly CommonFun commfun = new CommonFun();
    private readonly CommonFunWL commfunwl = new CommonFunWL();
    //commwl comwl = new commwl();
    private string[] codearrytemp = new string[0];
    //BTB_Province bprovince = new BTB_Province();
    //BTB_City bcity = new BTB_City();
    //private BTJ_BaseClass bbaseclass = new BTJ_BaseClass();
    private readonly BTB_Products_Infor bproduct = new BTB_Products_Infor();
    private readonly BTB_ProductAuthorForAgent bproductauthor = new BTB_ProductAuthorForAgent();
    private readonly BTB_CompAgentInfo bcompagent = new BTB_CompAgentInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
                //if (HF_CMD.Value.ToLower().Trim().Equals("add"))
                //{
                //    inputAgent_Code.Value = commfunwl.CreateAutoID(GetCookieCompID(), "a");
                //}
                //if (inputAgent_Code.Value.Trim().Length == 0)
                //{
                //    inputAgent_Code.Value = commfunwl.CreateAutoID(GetCookieCompID(), "a");
                //}
            }
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim();
            }
            FillDDL();
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    address.Visible = false;
                    name.Visible = false;
                    phone.Visible = false;
                    lxr.Visible = false;
                    number.Visible = false;
                    bz.Visible = false;
                    area.Visible = false;

                    break;
                case "edit":
                    Button1.Text = "修改";
                    fillinput(int.Parse(HF_ID.Value.Trim()));
                    break;
                default:
                    break;
            }
        }
    }

    //private IList<MTB_Products_Infor> prolist = null;
    //private ListItem item = new ListItem("授权产品...", "0");

    private void FillDDL()
    {
        if (GetCookieCompTypeID() == DAConfig.CompTypeIDChangJia.ToString() ||
            GetCookieCompTypeID() == DAConfig.CompTypeTianJianZongBuID.ToString())
        {
            CheckBoxList_PermitList.DataSource = bproduct.GetListsByFilterString("CompID=" + GetCookieCompID());
        }
        if (GetCookieCompTypeID() == DAConfig.CompTypeIDJingXiaoShang.ToString())
        {
            CheckBoxList_PermitList.DataSource = commfunwl.ReturnGetAgentAuthorProductInfo(GetCookieCompID());
        }
        CheckBoxList_PermitList.DataBind();
        foreach (ListItem item in CheckBoxList_PermitList.Items)
        {
            if (CheckProductIsAuthored(int.Parse(item.Value)))
            {
                item.Selected = true;
            }
            else
            {
                item.Selected = false;
            }
        }
        commfun.BindTreeCombox(ComboBox_CID, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "选择城市...", true,
            "-", "1=1");
        ComboBox_CID.SelectedValue = "0";
    }

    private bool CheckProductIsAuthored(int ProdID)
    {
        if (HF_ID.Value.Length > 0)
        {
            return bproductauthor.CheckIsExistByFilterString("AgentID=" + HF_ID.Value + " and ProdID=" + ProdID);
        }
        else
        {
            return false;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string wj = hf_file.Value;
        StreamReader sr = new StreamReader(wj.Trim(), Encoding.Default);
        string[] stringSeparators = new string[] {"\r\n"};
        codearrytemp = null;

        codearrytemp = sr.ReadToEnd().Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
        DataTable dttempline = new DataTable();
        dttempline.Columns.Add("SYSID");
        dttempline.Columns.Add("Comname");
        dttempline.Columns.Add("Address");
        dttempline.Columns.Add("Lxr");
        dttempline.Columns.Add("Mobile");
        dttempline.Columns.Add("tel");
        dttempline.Columns.Add("Xsarea");

        foreach (string line in codearrytemp)
        {
            dttempline.Rows.Add(line.Split(','));
        }
        Session.Add("dttempline", dttempline);
        //HFMaxMinValue.Value = com.MaxAndMinValue(codearrytemp, 3);
        //repeatstring = checkcodevalid(dttempline);
        //DataRow[] datarows = ((DataTable)Session["dttempline"]).Select("1=1", "AgentCode,PRCode");


        if (!CheckCompnameIsExist(HF_ID.Value, inputAgent_Name.Value, ComboBox_CID.SelectedValue, HF_CMD.Value))
        {
            if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
            {
                mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
            }

            MTB_CompAgentInfo mcompagent = new MTB_CompAgentInfo();

            mod.CTID = Convert.ToInt32(ComboBox_CID.SelectedValue.Trim());
            mod.ParentID = 0;
            mod.CompTypeID = DAConfig.CompTypeIDJingXiaoShang;
            mod.DisAuthorDate = DateTime.Now.AddYears(20);
            mod.AuthoredDate = DateTime.Now;
            mod.RegisterDate = DateTime.Now; 
            //HF_ID.Value = HF_ID.Value + 1;
            switch (HF_CMD.Value.Trim())
            {
                case "add":
                    foreach (DataRow dr in dttempline.Rows)
                    {
                        mod.CompName = dr[1].ToString().Trim();
                        mod.Agent_Code = commfunwl.CreateAutoCode(GetCookieCompID(), "A"); ;
                        mod.LegalPerson = dr[3].ToString().Trim();
                        mod.TelNumber = dr[5].ToString().Trim(); 
                        mod.MobilePhoneNumber = dr[4].ToString().Trim();
                        mod.AllowAreaInfo = dr[6].ToString().Trim();
                        mod.Remarks = "";
                        mod.SYSID = Convert.ToInt16(dr[0].ToString().Trim());
                        mod.Address = dr[2].ToString().Trim();   
                        object id = bll.Insert(mod);
                        HF_ID.Value = id.ToString();
                        //Button1.Text = "修 改";
                        //commfunwl.CreateFHTable(HF_ID.Value);
                        inputAgent_Code.Value = (int.Parse(inputAgent_Code.Value.Trim()) + 1).ToString();

                        mcompagent.CreateDate = DateTime.Now;
                        mcompagent.CompID = int.Parse(GetCookieCompID());
                        mcompagent.AgentID = int.Parse(HF_ID.Value);
                        mcompagent.Middleman = dr[3].ToString().Trim();
                        mcompagent.PhoneNumber = dr[4].ToString().Trim();
                        mcompagent.AllowArea = dr[6].ToString().Trim();
                        bcompagent.Insert(mcompagent);
                        RecordAuthorProduct();
                    }
                    break;

                case "edit":
                    mod.CompName = inputAgent_Name.Value.Trim(); 
                    mod.LegalPerson = inputMiddleman.Value.Trim();
                    mod.TelNumber = inputTelephone.Value.Trim();
                    mod.MobilePhoneNumber = inputMobiePhone.Value.Trim();
                    mod.AllowAreaInfo = inputAllowAreaInfo.Value.Trim();
                    mod.Remarks = inputRemarks.Value.Trim();
                    mod.Address = inputAgent_Addrss.Value.Trim(); 
                    mod.CompTypeID = DAConfig.CompTypeIDJingXiaoShang;
                    bll.Modify(mod); 
                    IList<MTB_CompAgentInfo> compagentlist =
                        bcompagent.GetListsByFilterString("CompID=" + GetCookieCompID() + " and AgentID=" + HF_ID.Value);
                    if (compagentlist.Count > 0)
                    {
                        mcompagent = compagentlist[0];
                        mcompagent.AllowArea = inputAllowAreaInfo.Value;
                        mcompagent.Middleman = inputMiddleman.Value;
                        mcompagent.PhoneNumber = inputMobiePhone.Value;
                        bcompagent.Modify(mcompagent);
                    } 
                    RecordAuthorProduct();
                    break;
            }

            //if (HF_CMD.Value.ToLower().Trim().Equals("edit"))
            //{
            //    IList<MTB_CompAgentInfo> compagentlist = bcompagent.GetListsByFilterString("CompID=" + GetCookieCompID() + " and AgentID=" + HF_ID.Value);
            //    if (compagentlist.Count > 0)
            //    {
            //        mcompagent = compagentlist[0];
            //        mcompagent.AllowArea = inputAllowAreaInfo.Value;
            //        mcompagent.Middleman = inputMiddleman.Value;
            //        mcompagent.PhoneNumber = inputMobiePhone.Value;
            //        bcompagent.Modify(mcompagent);
            //    }
            //}
            //else
            //{
            //    mcompagent.CreateDate = DateTime.Now;
            //    mcompagent.CompID = int.Parse(GetCookieCompID());
            //    mcompagent.AgentID = int.Parse(HF_ID.Value);
            //    mcompagent.Middleman = dr[1].ToString().Trim();
            //    mcompagent.PhoneNumber = dr[3].ToString().Trim();
            //    mcompagent.AllowArea = dr[5].ToString().Trim();
            //    bcompagent.Insert(mcompagent);
            //}
            //CreateCompanyAndAgentRelationShip();


            HF_CMD.Value = "edit";
            ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info",
                "alert('添加成功!');location.href='TB_Agents_Infor.aspx'", true);

            //this.Response.Write("<script>alert('添加成功 ！');location.href='TB_Agents_Infor.aspx'</script>");
        }
        else
        {
            Response.Write("<script>alert('该经销商名称已经存在，不可更改！');</script>");
        }
    }

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

    protected void Button_upload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            try
            {
                FileInfo file = new FileInfo(FileUpload1.PostedFile.FileName);
                if (file.Extension.ToLower().Equals(".txt"))
                {
                    // 保存文件
                    string uploadfilepath = "";
                    uploadfilepath = Server.MapPath(@"/Admin/wuliu/Fahuo/files/") +
                                     DateTime.Now.ToString("yyyyMMddhhmmssss") + ".txt";
                    FileUpload1.SaveAs(uploadfilepath);
                    ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('上传成功!');", true);
                    Label1.Text = "已上传文件:" + FileUpload1.FileName;
                    hf_file.Value = uploadfilepath;
                    Button1.Enabled = true;
                    //Button_DataCheck.Enabled = true;
                    //Button_DataCheck_Click(sender, e);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info",
                        "alert('请上传格式为(.txt)的盘点机数据库文件');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('" + ex.Message + "');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(updatepanel, GetType(), "info", "alert('请选取数据文件!');", true);
        }
    }

    private bool ReturnValue;

    private bool CheckCompnameIsExist(string CompID, string CompName, string CityID, string cmd)
    {
        ReturnValue = false;
        switch (cmd.ToLower())
        {
            case "add":
                ReturnValue =
                    bll.CheckIsExistByFilterString("CompName='" + CompName + "' and CTID=" + ComboBox_CID.SelectedValue);
                break;
            case "edit":
                ReturnValue =
                    (bll.GetListsByFilterString("CompName='" + CompName + "' and CTID=" + ComboBox_CID.SelectedValue)
                        .Count <= 1)
                        ? false
                        : true;
                break;
        }
        return ReturnValue;
    }

    private void fillinput(int id)
    {
        MTJ_RegisterCompanys ms = bll.GetList(id);
        ComboBox_CID.SelectedValue = ms.CTID.ToString().Trim();
        inputAgent_Name.Value = ms.CompName.Trim();
        if (ms.Agent_Code.Length > 0)
        {
            inputAgent_Code.Value = ms.Agent_Code.Trim();
        }
        inputMiddleman.Value = ms.LegalPerson.Trim();
        inputTelephone.Value = ms.TelNumber.Trim();
        inputMobiePhone.Value = ms.MobilePhoneNumber.Trim();
        inputAllowAreaInfo.Value = ms.AllowAreaInfo.Trim();
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
                mcompagent.AllowArea = inputAllowAreaInfo.Value;
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
            mcompagent.AllowArea = inputAllowAreaInfo.Value;
            bcompagent.Insert(mcompagent);
        }
    }

    private void FillCheckList()
    {
        if (HF_ID.Value.Length > 0)
        {
            CheckBoxList_PermitList.DataSource = commfunwl.ReturnGetAgentAuthorProductInfo(HF_ID.Value);
            CheckBoxList_PermitList.DataBind();
        }
        FillDDL();
    }
}