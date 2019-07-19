using System;
using System.Drawing.Imaging;
using System.Web.UI;
using MyClassLab;
using ThoughtWorks.QRCode.Codec;

public partial class Admin_QrProduce : Page
{
    private jiamijiemi jiamijiemi = new jiamijiemi();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim().Length > 0)
            {
                string Type = Request.QueryString["tp"].Trim();
                string QRContent = "";
                switch (Type)
                {
                    case "smck":
                        QRContent = "http://ymyg.utrue.net/mycheck.aspx?smid=" +
                                    jiamijiemi.EncryptDES(Request.QueryString["ID"].Trim());
                        break;
                    default:
                        break;
                }
                QRCodeEncoder qren = new QRCodeEncoder();
                qren.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                qren.QRCodeScale = 6;
                qren.QRCodeVersion = 8;
                string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".Png";
                qren.Encode(QRContent).Save(Server.MapPath("Temp") + "\\" + filename, ImageFormat.Png);
                Image_QRCode.ImageUrl = "Temp//" + filename;
            }
        }
    }
}