using System;
using commonlib;
using TJ.Model;
using TJ.BLL;
using TJ.DBUtility;
using TJ.DAL;
using System.Data;
using System.Web.UI;

public partial class Admin_commonswm_TJ_RegistercommonswmJiHuoAndEdit : AuthorPage
{
    TabExecute _tab = new TabExecute();
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();
    private MTJ_RegisterCompanys _mod = new MTJ_RegisterCompanys();
    private readonly CommonFun _comfun = new CommonFun();
    private readonly BTJ_RoleInfo _brole = new BTJ_RoleInfo();
    public string Temp;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                HF_ID.Value = Request.QueryString["ID"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["CompTypeID"]))
            {
                hf_parentid.Value = Request.QueryString["CompTypeID"];
            }
            
            if (Request.QueryString["CompTypeID"].Equals("484"))
            {
                comp.InnerText = "个人";
                intruct.InnerText = "个人介绍";
                images.InnerText = "身份证";
                
            }
            else
            {
                comp.InnerText = "公司";
                intruct.InnerText = "公司介绍";
                images.InnerText = "营业执照";
            }
            inputCompName.Disabled = true;
            qiyejieshao.Disabled = true;
            inputTelNumber.Disabled = true;
            inputAddress.Disabled = true;
            Fillinput(int.Parse(HF_ID.Value.Trim()));
        }
    }

    private void Fillinput(int id)
    {
        MTJ_RegisterCompanys ms = bll.GetList(id);
        inputCompName.Value = ms.CompName.ToString().Trim();//CompTypeID.ToString().Trim();
        qiyejieshao.Value=ms.DetailDiscription.ToString();
        /*
        DataTable mDataTable = _tab.ExecuteNonQuery("select distinct case CompAutherID when 67 then '未激活' when 68 then '激活' when 69 then '已冻结' end as CompAuther from TJ_RegisterCompanys where useswm=1");
        ComboBox_CompAutherID.DataSource=mDataTable;
        ComboBox_CompAutherID.DataBind();
        if (ms.CompAutherID.ToString().Trim().Equals("67")) {
            ComboBox_CompAutherID.SelectedValue = "未激活";
        }else if(ms.CompAutherID.ToString().Trim().Equals("68"))
        {
            ComboBox_CompAutherID.SelectedValue = "激活";
        }
        else
        {
            ComboBox_CompAutherID.SelectedValue = "已冻结";
        }
        */
        if (ms.BusinessLicencePicture.Trim().Length > 0)
        {
            Image_Logo.ImageUrl = "http://os.china315net.com/commonswm/" + ms.BusinessLicencePicture.Trim();
        }
        inputAddress.Value = ms.Address.Trim();
        inputTelNumber.Value = ms.MobilePhoneNumber.Trim();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {      
        
        MTJ_RegisterCompanys ms = bll.GetList(Convert.ToInt32(HF_ID.Value));
        //未激活状态
        if (radOff.Checked)
        {
            _tab.ExecuteNonQuery("update TJ_RegisterCompanys set CompAutherID=67  where CompID=" + HF_ID.Value);
            _tab.ExecuteNonQuery("update TJ_User set IsActived=0 where  CompID=" + ms.CompID);
        }
        //激活状态
        if (radOn.Checked)
        {
            _tab.ExecuteNonQuery("update TJ_RegisterCompanys set CompAutherID=68,AuthoredDate=getdate(),ManagerUserID='"+ GetCookieUID ()+ "' where CompID=" + HF_ID.Value);
            _tab.ExecuteNonQuery("update TJ_User set IsActived=1 where  CompID=" + ms.CompID);
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);  
          
    }
}