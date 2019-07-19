using System;
using System.Text;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using TJ.BLL;
using TJ.Model;
using commonlib;
using System.Data.SqlClient;
using System.IO;

public partial class Admin_wuliu_Fahuo_FaHuoOutFilejg : AuthorPage
{
    private readonly BTJ_RegisterCompanys bll = new BTJ_RegisterCompanys();
    private MTJ_RegisterCompanys mod = new MTJ_RegisterCompanys();
    private readonly BTB_StoreHouse bllsthose = new BTB_StoreHouse();
    private MTB_StoreHouse modsthose = new MTB_StoreHouse();
    private BTB_Products_Infor bpro = new BTB_Products_Infor();
    private BTB_StoreHouse bstorehouse = new BTB_StoreHouse();
    private readonly BTB_Products_Infor bllpro = new BTB_Products_Infor();
    private MTB_Products_Infor modpro = new MTB_Products_Infor();
    public BTB_Products_Type ptype = new BTB_Products_Type();
    public BTB_ProducStandards pstandards = new BTB_ProducStandards();
    public BTB_ProductJingHanLiang pjinghanliang = new BTB_ProductJingHanLiang();
    public BTB_ProductXiangXing pxiangxing = new BTB_ProductXiangXing();
    public BTB_ProductJiuJingDu pjiujingdu = new BTB_ProductJiuJingDu();
    private BTB_ProductAuthorForAgent bproductforagent = new BTB_ProductAuthorForAgent();
    private BTJ_User buser = new BTJ_User();
    public CommonFun commfun = new CommonFun();
    private readonly CommonFunWL comwl = new CommonFunWL();

    private SqlConnection sqlconcom =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlServerConnStringWuLiu"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    private void FillGridViewProinfo(GridView gv)
    {
        //string mm = GetCookieCompTypeID();
        //if (GetCookieCompTypeID() == DAConfig.CompTypeIDChangJia.ToString().Trim() || GetCookieCompTypeID() == "45")
        //{
        gv.DataSource = bllpro.GetListsByFilterString("CompID=" + GetCookieCompID());
        //}
        //else
        //{
        //    CommonFunWL comwl = new CommonFunWL();
        //    gv.DataSource = comwl.ReturnGetAgentAuthorProductInfo(GetCookieCompID());
        //}


        if (inputSearchProduct.Text.Trim().Length > 0)
        {
            if (GetCookieCompTypeID() == DAConfig.CompTypeIDChangJia.ToString().Trim())
            {
                gv.DataSource = bllpro.GetListsByFilterString("CompID=" + GetCookieCompID());
            }
            else
            {
                CommonFunWL comwl = new CommonFunWL();
                gv.DataSource = comwl.ReturnGetAgentAuthorProductInfo(GetCookieCompID());
            }

            ////CommonFunWL comwl = new CommonFunWL();
            ////gv.DataSource = comwl.ReturnGetAgentAuthorProductInfo(GetCookieCompID());
            ////gv.DataSource = bllpro.GetListsByFilterString("CompID=" + GetCookieCompID() + " and  Products_Name like '%" + inputSearchProduct.Text.Trim() + "%'");
        }
        else
        {
            if (GetCookieCompTypeID() == DAConfig.CompTypeIDChangJia.ToString().Trim())
            {
                gv.DataSource = bllpro.GetListsByFilterString("CompID=" + GetCookieCompID());
            }
            else
            {
                CommonFunWL comwl = new CommonFunWL();
                gv.DataSource = comwl.ReturnGetAgentAuthorProductInfo(GetCookieCompID());
            }

            //CommonFunWL comwl = new CommonFunWL();
            //gv.DataSource = comwl.ReturnGetAgentAuthorProductInfo(GetCookieCompID());
            //gv.DataSource = bllpro.GetListsByFilterString("CompID=" + GetCookieCompID());
        }
        gv.DataBind();
    }

    private void FillGridViewStoreHouse(GridView gv)
    {
        if (TextBox_AgentFilter.Text.Trim().Length > 0)
        {
            gv.DataSource =
                bllsthose.GetListsByFilterString("CompID=" + GetCookieCompID() + " and StoreHouseName like '%" +
                                                 TextBox_AgentFilter.Text.Trim() + "%'");
        }
        else
        {
            gv.DataSource = bllsthose.GetListsByFilterString("CompID=" + GetCookieCompID());
        }
        gv.DataBind();
    }

    private void FillGridViewAgentInfo(GridView gv)
    {
        string AgentIDStringForQuery = comwl.GetAgentIDStringByCompID(GetCookieCompID());
        if (AgentIDStringForQuery.Length > 0)
        {
            if (TextBox_StoreHouseFilter.Text.Trim().Length > 0)
            {
                gv.DataSource =
                    bll.GetListsByFilterString("CompID in (" + AgentIDStringForQuery + ") and CompTypeID=" +
                                               DAConfig.CompTypeIDJingXiaoShang + " and CompName  like '%" +
                                               TextBox_StoreHouseFilter.Text.Trim() + "%'");
            }
            else
            {
                gv.DataSource =
                    bll.GetListsByFilterString("CompID in (" + AgentIDStringForQuery + ") and CompTypeID=" +
                                               DAConfig.CompTypeIDJingXiaoShang);
            }
            gv.DataBind();
        }
    }


    protected void GridView_TbProductsInfor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_TbProductsInfor.PageIndex = e.NewPageIndex;
        FillGridViewProinfo(GridView_TbProductsInfor);
    }

    protected void GridView_AgentsInfor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_AgentsInfor.PageIndex = e.NewPageIndex;
        FillGridViewAgentInfo(GridView_AgentsInfor);
    }

    protected void GridView_StoreHouse_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_StoreHouse.PageIndex = e.NewPageIndex;
        FillGridViewStoreHouse(GridView_StoreHouse);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        FillGridViewProinfo(GridView_TbProductsInfor);
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        string filename = Server.MapPath("~/Admin/wuliu/fahuo/ss/proinfo.txt");
        string name = "proinfo.txt";
        StreamWriter SW = new StreamWriter(filename, false, Encoding.GetEncoding("gb2312"));

        foreach (GridViewRow GVR in GridView_TbProductsInfor.Rows)
        {
            if (!GVR.Cells[0].Text.Trim().Equals("") && !(GVR.Cells[1].Text.Trim().Equals("")))
            {
                SW.WriteLine(GVR.Cells[0].Text.Trim() + "," + GVR.Cells[1].Text.Trim());
            }
        }
        SW.Flush();
        SW.Close();
        outfile(filename, name);
    }

    public void outfile(string ServerFilePath, string filename)
    {
        string filePath = ServerFilePath;
        FileInfo Fi = new FileInfo(ServerFilePath);
        if (Fi.Exists)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int) fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }

    //private void OutPutFile(string ServerFilePath)
    //{
    //    FileInfo file = new FileInfo(ServerFilePath);//用于获得文件信息
    //    Response.Clear();//清空输出
    //    Response.Charset = "GB2312";//设定编码
    //    // Response.Charset = "UTF-8";
    //    Response.ContentEncoding = System.Text.Encoding.UTF8;
    //    // 添加头信息,为"文件下载/另存为"对话框指定默认文件名 
    //    //Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(ServerFilePath));
    //    Response.AddHeader("Content-Disposition", "attachment; filename=" + ServerFilePath);
    //    // 添加头信息,指定文件大小,让浏览器能够显示下载进度 
    //    Response.AddHeader("Content-Length", file.Length.ToString());

    //    // 指定返回的是一个不能被客户端读取的流,必须被下载 
    //    Response.ContentType = "application/ms-txt";

    //    // 把文件流发送到客户端 
    //    Response.WriteFile(file.FullName);


    //}
    protected void Button2_Click(object sender, EventArgs e)
    {
        FillGridViewAgentInfo(GridView_AgentsInfor);
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        string filename = Server.MapPath("~/Admin/wuliu/fahuo/ss/agentinfo.txt");
        string name = "agentinfo.txt";
        StreamWriter SW = new StreamWriter(filename, false, Encoding.GetEncoding("gb2312"));
        foreach (GridViewRow GVR in GridView_AgentsInfor.Rows)
        {
            if (!GVR.Cells[0].Text.Trim().Equals("") && !GVR.Cells[1].Text.Trim().Equals(""))
            {
                SW.WriteLine(GVR.Cells[0].Text.Trim() + "," + GVR.Cells[1].Text.Trim());
            }
        }
        SW.Flush();
        SW.Close();
        outfile(filename, name);
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        FillGridViewStoreHouse(GridView_StoreHouse);
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        string filename = Server.MapPath("~/Admin/wuliu/fahuo/ss/storehousinfo.txt");
        string name = "storehousinfo.txt";
        StreamWriter SW = new StreamWriter(filename, false, Encoding.GetEncoding("gb2312"));
        foreach (GridViewRow GVR in GridView_StoreHouse.Rows)
        {
            if (!GVR.Cells[0].Text.Trim().Equals("") && !GVR.Cells[1].Text.Trim().Equals(""))
            {
                SW.WriteLine(GVR.Cells[0].Text.Trim() + "," + GVR.Cells[1].Text.Trim());
            }
        }
        SW.Flush();
        SW.Close();
        outfile(filename, name);
    }
}