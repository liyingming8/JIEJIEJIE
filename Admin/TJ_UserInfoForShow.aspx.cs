using System;
using TJ.Model;
using TJ.BLL;
using commonlib;
using System.Text.RegularExpressions;

public partial class Admin_TJ_UserInfoForShow : AuthorPage
{
    private readonly BTJ_User _bll = new BTJ_User(); 
    private readonly BTJ_RegisterCompanys _bcomp = new BTJ_RegisterCompanys(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {  
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Sc.DecryptQueryString(Request.QueryString["ID"].Trim()); 
            }
          
            Fillinput(int.Parse(HF_ID.Value.Trim()));
        }
    } 
   

    private void FillDiscountCheckList(int compid)
    {
        //CheckBoxList_Discout.Items.Clear();
        string authorDiscountString = _bcomp.GetList(compid).AuthorDiscount;
        if (authorDiscountString.Length > 0)
        {
            authorDiscountString = authorDiscountString.Substring(1);
            authorDiscountString = authorDiscountString.Substring(0, authorDiscountString.Length - 1);
            //CheckBoxList_Discout.DataSource = _bllbase.GetListsByFilterString("CID in (" + authorDiscountString + ")");
            //CheckBoxList_Discout.DataBind();
        }
    }
  

    private void Fillinput(int id)
    {
        MTJ_User ms = _bll.GetList(id);
        hf_compid.Value = ms.CompID.ToString();
        imgheader.Src = ms.HeaderImageUrl;
        labnickname.Text = ms.NickName;
        Label_RegisterDate.Text = ms.RegisterDate.ToString("yyyy-MM-dd HH:mm:ss"); 
        //foreach (ListItem item in CheckBoxList_Discout.Items)
        //{
        //    if (ms.AuthorDiscount.Contains("," + item.Value + ","))
        //    {
        //        item.Selected = true;
        //    }
        //    else
        //    {
        //        item.Selected = false;
        //    }
        //}
    }

    //protected void DDLCompID_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillDiscountCheckList(int.Parse(DDLCompID.SelectedValue));
    //}

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
}