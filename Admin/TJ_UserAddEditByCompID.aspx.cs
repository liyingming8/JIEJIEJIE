﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Text.RegularExpressions;

public partial class Admin_TJ_UserAddEditByCompID : AuthorPage
{
    private readonly BTJ_User _bll = new BTJ_User();
    private MTJ_User _mod = new MTJ_User(); 
    private readonly BTJ_RegisterCompanys _bcomp = new BTJ_RegisterCompanys();
    private readonly CommonFun _comfun = new CommonFun();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Button2.Visible = false; 
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
            }
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim(); 
            }
            if (!string.IsNullOrEmpty(Request.QueryString["compid"]))
            {
                hf_compid.Value = Sc.DecryptQueryString(Request.QueryString["compid"]);
                txCompID.Value = _bcomp.GetList(Convert.ToInt32(hf_compid.Value)).CompName;
            }
            else
            {
                Response.End();
            }
            FillDll();
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    radiolist_Actived.SelectedValue = "1";
                    break;
                case "edit":
                    Button1.Text = "修改";
                    CheckBox_ModifyAsDefault.Visible = true;
                    inputLoginName.Disabled = true;
                    inputpassword.Disabled = true;
                    Fillinput(int.Parse(HF_ID.Value.Trim()));
                    break; 
            } 
        }
    } 
    private void FillDll()
    {
        var bllrole = new BTJ_RoleInfo();
        ComboBox_RID.DataSource = bllrole.GetListsByFilterString("RID="+DAConfig.DeliveryterminalRid, "RoleName");
        ComboBox_RID.DataBind();
        ComboBox_RID.SelectedValue = DAConfig.DeliveryterminalRid;
        _comfun.BindTreeCombox(ComboBox_From, "CName", "CID", "ParentID", "TJ_BaseClass", DAConfig.china, "请选择...", true,"-", ""); 
    } 
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (inputLoginName.Value == "")
        { 
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('请输入帐号、密码！');", true);
            return;
        }  
        if (ComboBox_RID.SelectedValue == null || ComboBox_RID.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('请指定系统角色！');", true); 
        }
        else
        {
            if (HF_CMD.Value.ToLower().Trim().Equals("edit"))
            {
                _mod = _bll.GetList(int.Parse(HF_ID.Value));
            }
            else
            {
                if (UserNameIsExist(inputLoginName.Value))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('该系统用户名已经存在！');", true);
                    return;
                }
            }
            _mod.CompID = Convert.ToInt32(hf_compid.Value);
            _mod.RID = Convert.ToInt32(ComboBox_RID.SelectedValue);
            _mod.LoginName = inputLoginName.Value.Trim(); 
            _mod.MobileNumber = inputMobileNumber.Value.Trim();
            _mod.IsActived = 1;
            foreach (ListItem item in radiolist_Actived.Items)
            {
                if (item.Selected)
                {
                    _mod.IsActived = int.Parse(item.Value);
                    break;
                }
            } 
            _mod.FromCityID = Convert.ToInt32(ComboBox_From.SelectedValue.Trim());
            _mod.Remarks = inputRemarks.Value.Trim();
            switch (HF_CMD.Value.Trim())
            {
                case "add":
                    _mod.RegisterDate = DateTime.Now;
                    _mod.PassWords = CommonFun.Md5hash_String(inputpassword.Value.Trim());
                    _bll.Insert(_mod);
                    break;
                case "edit":
                    if (CheckBox_ModifyAsDefault.Checked)
                    {
                        _mod.PassWords = CommonFun.Md5hash_String("123456");
                    }
                    _bll.Modify(_mod);
                    break;
            }
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true); 
        }
    }

    private bool UserNameIsExist(string LoginName)
    {
        return _bll.CheckIsExistByFilterString("LoginName='" + LoginName + "'");
    }

    private void Fillinput(int id)
    {
        MTJ_User ms = _bll.GetList(id);
        hf_compid.Value = ms.CompID.ToString();
        txCompID.Value = _bcomp.GetList(Convert.ToInt32(hf_compid.Value)).CompName;
        FillDll();
        ComboBox_RID.SelectedValue = ms.RID.ToString().Trim();
        inputLoginName.Value = ms.LoginName.Trim(); 
        ComboBox_From.SelectedValue = ms.FromCityID.ToString();
        inputMobileNumber.Value = ms.MobileNumber; 
        foreach (ListItem item in radiolist_Actived.Items)
        {
            if (item.Value == ms.IsActived.ToString().Trim())
            {
                item.Selected = true;
                break;
            }
        }
        inputRemarks.Value = ms.Remarks.Trim(); 
    } 
    public static bool IsMobilePhone(string input)
    {
        if (!Regex.IsMatch(input, @"^[1][1-9]\d{9}$", RegexOptions.IgnoreCase))
            return false;
        if (input.Length == 11 &&
            (input.StartsWith("13") || input.StartsWith("14") || input.StartsWith("15") || input.StartsWith("18")))
        {
            return true;
        }
        return false;
    }

    public static bool IsNum(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (!Char.IsNumber(text, i))
            {
                return true; //输入的不是数字  
            }
        }

        return false; //否则是数字
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (ComboBox_RID.SelectedValue != "")
        {
            Response.Redirect("SystemAuthorManage.aspx?RID=" + ComboBox_RID.SelectedValue + "");
        }
        else
        {
            Response.Write("<script>alert('操作成功！');</script>");
        }
    }
}