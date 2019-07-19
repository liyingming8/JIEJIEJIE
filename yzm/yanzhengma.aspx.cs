using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.UI;

public partial class yanzhengma : Page
{
    private HttpCookie yzmcookie;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string validateNum = CreateRandomNum(4);
            //Session["ValidateNum"] = validateNum;
            yzmcookie = new HttpCookie("tjyzm");
            yzmcookie.Value = HttpUtility.UrlEncode(validateNum);
            yzmcookie.Expires.AddDays(1);
            Response.Cookies.Add(yzmcookie);
            CreateImage(validateNum); 
        }
    }

    private string CreateRandomNum(int NumCount)
    {
        string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,O,P,Q,R,S,T,U,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,m,n,o,p,q,s,t,u,w,x,y,z";
        string[] allCharArray = allChar.Split(',');//拆分成数组
        string randomNum = "";
        int temp = -1;                             //记录上次随机数的数值，尽量避免产生几个相同的随机数
        Random rand = new Random();
        for (int i = 0; i < NumCount; i++)
        {
            if (temp != -1)
            {
                rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
            }
            int t = rand.Next(35);
            if (temp == t)
            {
                return CreateRandomNum(NumCount);
            }
            temp = t;
            randomNum += allCharArray[t];


        }
        return randomNum;
    }
    //生产图片
    private void CreateImage(string validateNum)
    {
        if (validateNum == null || validateNum.Trim() == string.Empty)
            return;
        //生成BitMap图像
        Bitmap image = new Bitmap(validateNum.Length * 12 + 12, 22);
        Graphics g = Graphics.FromImage(image);
        try
        {
            //生成随机生成器
            Random random = new Random();
            //清空图片背景
            g.Clear(Color.White);
            //画图片的背景噪音线
            for (int i = 0; i < 25; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);
                g.DrawLine(new Pen(Color.Silver), x1, x2, y1, y2);
            }
            Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
            g.DrawString(validateNum, font, brush, 2, 2);
            //画图片的前景噪音点
            for (int i = 0; i < 3; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);
                image.SetPixel(x, y, Color.FromArgb(random.Next()));

            }
            //画图片的边框线
            g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
            var ms = new MemoryStream();
            //将图像保存到指定流
            image.Save(ms, ImageFormat.Gif);
            Response.ClearContent();
            Response.ContentType = "image/Gif";
            Response.BinaryWrite(ms.ToArray());
        }
        finally
        {
            g.Dispose();
            image.Dispose();
        }
    }
}