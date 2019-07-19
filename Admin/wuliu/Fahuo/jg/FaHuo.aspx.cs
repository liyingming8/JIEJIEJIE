using System.IO;
using System.Web.UI;
using commonlib;
using System;
using System.Collections;
using System.Data;
using System.Web;

public partial class Admin_wuliu_Fahuo_jg_FaHuo : Page
{
    private readonly CommonFunWL com = new CommonFunWL();
    private commwl comm = new commwl();
    private string[] codearrytemp = new string[0];
    private readonly ArrayList strList = new ArrayList();
    private readonly ArrayList stroneList = new ArrayList();
    private readonly ArrayList arry = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dttempline = new DataTable();
        HttpPostedFile oFile = Context.Request.Files["Filedata"];
        if (oFile != null)
        {
            //string topDir = Context.Request["folder"];
            ////创建顶级目录  
            //if (!Directory.Exists(HttpContext.Current.Server.MapPath(topDir)))
            //{
            //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(topDir));
            //}

            ////当天上传的文件放到已当天日期命名的文件夹中  
            //string dateFolder = HttpContext.Current.Server.MapPath(topDir) + "//" + DateTime.Now.Date.ToString("yyyymmddhhmmss");

            //if (!Directory.Exists(dateFolder))
            //{
            //    Directory.CreateDirectory(dateFolder);
            //}
            //oFile.SaveAs(dateFolder + "//" + oFile.FileName);
            //Context.Response.Write("1");
            //oFile = Context.Request.Files["Filedata"];
            string repeatstring = string.Empty;
            //if (!uploadfilepath.Trim().Equals(string.Empty))
            if (oFile != null)
            {
                //string currentworkdir = Server.MapPath("/");
                string topDir = Context.Request["folder"];
                //创建顶级目录  
                //if (!Directory.Exists(HttpContext.Current.Server.MapPath(topDir)))
                //{
                //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(topDir));
                //}

                //当天上传的文件放到已当天日期命名的文件夹中  
                string dateFolder = HttpContext.Current.Server.MapPath(topDir) + "\\files";

                //if (!Directory.Exists(dateFolder))
                //{
                //    Directory.CreateDirectory(dateFolder);
                //}
                oFile.SaveAs(dateFolder + "\\" + oFile.FileName);
                Context.Response.Write("1");
                StreamReader sr = new StreamReader(dateFolder + "\\" + oFile.FileName);
                string[] stringSeparators = new string[] {"\r\n"};
                codearrytemp = null;
                codearrytemp = sr.ReadToEnd().Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                string mm = com.MaxAndMinValue(codearrytemp, 3);

                if (Session["strList"] != null)
                {
                    string Smin = Session["strList"].ToString().Split(',')[0].Trim();
                    string Smax = Session["strList"].ToString().Split(',')[1].Trim();
                    string Cmin = mm.Split(',')[0].Trim();
                    string Cmax = mm.Split(',')[1].Trim();

                    string Smm = MinMaxvalue(Smin, Smax, Cmin, Cmax);

                    Session["strList"] = Smm;
                    //stroneList = (ArrayList)Session["strList"];
                    ////Session.Remove("strList");
                    //if (!stroneList.Contains(mm))
                    //{
                    //    stroneList.Add(mm);
                    //    stroneList.Sort();
                    //    Session.Add("strList", stroneList);
                    //}
                }
                else
                {
                    arry.Add(mm);
                    strList.Add(mm);
                    Session.Add("strList", mm);
                }
                int ee = stroneList.Count;
                // int mm = strList.Count;
                if (!string.IsNullOrEmpty(oFile.FileName))

                {
                    if (Session["dttempline"] != null)
                    {
                        dttempline = (DataTable) Session["dttempline"];
                        foreach (string line in codearrytemp)
                        {
                            dttempline.Rows.Add(line.Split(','));
                        }

                        Session.Add("dttempline", dttempline);
                    }
                    else
                    {
                        dttempline.Columns.Add("FHPC");
                        dttempline.Columns.Add("AgentCode");
                        dttempline.Columns.Add("PRCode");
                        dttempline.Columns.Add("LBCode");
                        dttempline.Columns.Add("STCode");
                        //dttempline.Columns.Add("CKCode");
                        //dttempline.Columns.Add("ZJCode");
                        foreach (string line in codearrytemp)
                        {
                            dttempline.Rows.Add(line.Split(','));
                        }

                        Session.Add("dttempline", dttempline);
                    }
                    sr.Close();
                }
            }
            else
            {
                Context.Response.Write("0");
            }
        }
    }

    public string MinMaxvalue(string Smin, string Smax, string Cmin, string Cmax)
    {
        string min = Smin;
        string max = Smax;
        if (Convert.ToInt64(Cmin) < Convert.ToInt64(Smin))
        {
            min = Cmin;
        }

        if (Convert.ToInt64(Cmax) > Convert.ToInt64(Smax))
        {
            max = Cmax;
        }
        return min + "," + max;
    }
}