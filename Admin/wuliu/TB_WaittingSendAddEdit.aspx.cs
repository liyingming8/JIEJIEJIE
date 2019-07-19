﻿using System;
using TJ.Model;
using TJ.BLL;
using commonlib;

public partial class Admin_TB_WaittingSendAddEdit : AuthorPage
{
    private readonly BTB_WaittingSend bll = new BTB_WaittingSend();
    private MTB_WaittingSend mod = new MTB_WaittingSend();

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
            switch (HF_CMD.Value)
            {
                case "add":
                    Button1.Text = "添加";
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
        if (HF_CMD.Value.Trim().ToLower().Equals("edit"))
        {
            mod = bll.GetList(Convert.ToInt32(HF_ID.Value));
        }
        mod.ID = Convert.ToInt32(inputID.Value.Trim());
        mod.Phone_Num = inputPhone_Num.Value.Trim();
        mod.Contents = inputContents.Value.Trim();
        mod.Received_Time = Convert.ToDateTime(inputReceived_Time.Value.Trim());
        mod.State = inputState.Value.Trim();
        mod.MSGCenterNo = inputMSGCenterNo.Value.Trim();
        mod.Remarks = inputRemarks.Value.Trim();
        mod.CompID = Convert.ToInt32(inputCompID.Value.Trim());
        switch (HF_CMD.Value.Trim())
        {
            case "add":
                bll.Insert(mod);
                break;
            case "edit":
                bll.Modify(mod);
                break;
        }
        Response.Write("<script>alert('操作成功！');</script>");
    }

    private void fillinput(int id)
    {
        MTB_WaittingSend ms = bll.GetList(id);
        inputID.Value = ms.ID.ToString().Trim();
        inputPhone_Num.Value = ms.Phone_Num.Trim();
        inputContents.Value = ms.Contents.Trim();
        inputReceived_Time.Value = ms.Received_Time.ToString().Trim();
        inputState.Value = ms.State.Trim();
        inputMSGCenterNo.Value = ms.MSGCenterNo.Trim();
        inputRemarks.Value = ms.Remarks.Trim();
        inputCompID.Value = ms.CompID.ToString().Trim();
    }
}