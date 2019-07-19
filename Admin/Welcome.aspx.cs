using System;
using System.Collections.Generic;
using System.Web.UI;
using TJ.Model;
using TJ.BLL;

public partial class Admin_Welcome : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BTB_CompAgentInfo btjcompagent = new BTB_CompAgentInfo();
            // MTB_CompAgentInfo mtjcompagent = new MTB_CompAgentInfo();
            if (Request.Cookies["TJCOMPID"] != null)
            {
                string compid = Request.Cookies["TJCOMPID"].Value;
                IList<MTB_CompAgentInfo> mtjcompagent = btjcompagent.GetListsByFilterString("AgentID=" + compid);
                if (mtjcompagent.Count > 0 && mtjcompagent[0].CompID == 130 || compid == "130")
                {
                    xiazai.Style.Add("display", "block");
                }
                else
                {
                    xiazai.Style.Add("display", "none");
                }
            }
        }
    }

    protected void download_Click(object sender, EventArgs e)
    {
        string filename = Server.MapPath("~/Admin/software/JXS发货系统.rar");
        string name = "JXS发货系统.rar";


        outfile(filename, name);
    }

    public void outfile(string ServerFilePath, string filename)
    {
        //string filePath = ServerFilePath;
        //FileInfo Fi = new FileInfo(ServerFilePath);
        //if (Fi.Exists)
        //{
        //    FileStream fs = new FileStream(filePath, FileMode.Open);
        //    byte[] bytes = new byte[(int)fs.Length];
        //    fs.Read(bytes, 0, bytes.Length);
        //    fs.Close();
        //    Response.ContentType = "application/octet-stream";
        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
        //    Response.BinaryWrite(bytes);
        //    Response.Flush();
        //    Response.End();
        //}
    }
}