using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TJ.Model;
using TJ.BLL;
using System.IO;
using commonlib;
using System.Collections.Generic;

public partial class Admin_TJ_PublishInfoAddEdit : AuthorPage
{
    private readonly BTJ_PublishInfo bll = new BTJ_PublishInfo();
    private readonly BTJ_InfoType binfotype = new BTJ_InfoType();
    private MTJ_PublishInfo mod = new MTJ_PublishInfo(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cmd"] != null && !Request.QueryString["cmd"].Trim().Equals(""))
            {
                HF_CMD.Value = Request.QueryString["cmd"].Trim();
            }
            if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
            {
                HF_ID.Value = Request.QueryString["ID"].Trim();
            } 
            FillDLL();
            CheckAndCreateDirectory("../UploadFile/" + GetCookieCompID());
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
                    btnDelete.Style["display"] = "none";//新增界面隐藏删除按钮
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HF_CMD.Value.ToLower().Trim().Equals("edit"))
        {
            mod = bll.GetList(int.Parse(HF_ID.Value.Trim()));
        }
        mod.CompID = int.Parse(GetCookieCompID());
        mod.CID = Convert.ToInt32(ddl_infotype.SelectedValue);
        mod.Title = inputTitle.Value.Trim();
        mod.Contents = TextArea1.Value;
        mod.LinkURLString = savefilepath.Value;
        mod.Remarks = inputRemarks.Value.Trim(); 
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                mod.PublishDate = DateTime.Now;
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        // ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "info", "alert('操作成功！');", true);
    }

    private void fillinput(int id)
    {
        MTJ_PublishInfo ms = bll.GetList(id);
        ddl_infotype.SelectedValue = ms.CID.ToString().Trim(); 
        inputTitle.Value = ms.Title.Trim();
        TextArea1.Value = ms.Contents.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        savefilepath.Value = ms.LinkURLString;
        showimage.Src = ms.LinkURLString; 
    }

    private void CheckAndCreateDirectory(string directorysting)
    {
        if (!Directory.Exists(Server.MapPath(directorysting)))
        {
            Directory.CreateDirectory(Server.MapPath(directorysting));
        }
    }

    private void FillDLL()
    {
        if (GetCookieRID().Equals("155"))
        {
            IList<MTJ_InfoType> list0 = binfotype.GetListsByFilterString("IFTypeID in (2,33)");
            ddl_infotype.DataSource = list0;
            ddl_infotype.DataBind();
        }
        else
        {
            IList<MTJ_InfoType> list0 = binfotype.GetListsByFilterString("(CompID=" + GetCookieCompID() + " and ParentID=0) or CompID=0");
            foreach (MTJ_InfoType mt in list0)
            {
                ddl_infotype.Items.Add(new ListItem(mt.TypeName, mt.IFTypeID.ToString()));
                IList<MTJ_InfoType> list1 = getChildList(mt.IFTypeID);
                if (list1.Count > 0)
                {
                    foreach (MTJ_InfoType mt1 in list1)
                    {
                        ddl_infotype.Items.Add(new ListItem(mt.TypeName + "-" + mt1.TypeName, mt1.IFTypeID.ToString()));
                        IList<MTJ_InfoType> list2 = getChildList(mt1.IFTypeID);
                        if (list2.Count > 0)
                        {
                            foreach (MTJ_InfoType mt2 in list2)
                            {
                                ddl_infotype.Items.Add(new ListItem(mt.TypeName + "-" + mt1.TypeName + "-" + mt2.TypeName,
                                    mt2.IFTypeID.ToString()));
                            }
                        }
                    }
                }
            }
        } 
        ddl_infotype.SelectedValue = "0"; 
    }

    private IList<MTJ_InfoType> getChildList(int ParentID)
    {
        return binfotype.GetListsByFilterString("ParentID=" + ParentID);
    }

    protected void HF_ID_ValueChanged(object sender, EventArgs e)
    {
    }

    /*
     * 删除
     */
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["ID"] != null && !Request.QueryString["ID"].Trim().Equals(""))
        {
            int deleteId = Convert.ToInt32(Request.QueryString["ID"]);
            bll.Delete(deleteId);
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "reload", "closemyWindow();", true);
        }
    }
}