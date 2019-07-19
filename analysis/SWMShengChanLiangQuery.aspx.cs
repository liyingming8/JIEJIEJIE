using System; 
using System.Data;
using System.Text;
using commonlib;
using Newtonsoft.Json.Linq;

public partial class analysis_SWMShengChanLiangQuery : AuthorPage
{ 
    DBClass db = new DBClass();
    public string resultstring = "";
    InternetHandle internet =  new InternetHandle();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Fresh();
        }
    }

    private void Fresh()
    {
        string queryurl =
            "http://www.china315net.com:35224/zhpt/qr3d/stk/inbound/period/?date_from="+DateTime.Now.AddYears(-1).ToString("yyyy-01-01")+"&date_to=" +
            DateTime.Now.ToString("yyyy-MM-dd") + "&company_id=" + GetCookieCompID();
        string tempvl = internet.GetUrlData(queryurl);
        JObject obj = JObject.Parse(tempvl);
        JObject obj1; 
        StringBuilder sb = new StringBuilder();
        string datastring = ""; 
        if (string.IsNullOrEmpty(obj["e"].ToString()))
        {
            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("year",typeof(int));
            dttemp.Columns.Add("product");
            dttemp.Columns.Add("month",typeof(int));
            dttemp.Columns.Add("package_id");
            dttemp.Columns.Add("count",typeof(int));
            JArray array = JArray.FromObject(obj["detail"]);
            foreach (var ob in array)
            {
                obj1 = JObject.Parse(ob.ToString());
                DataRow dr = dttemp.NewRow();
                dr["year"] = int.Parse(obj1["year"].ToString());
                dr["product"] = obj1["product"];
                dr["month"] = int.Parse(obj1["month"].ToString());
                dr["package_id"] = obj1["package_id"];
                dr["count"] = int.Parse(obj1["count"].ToString());
                dttemp.Rows.Add(dr.ItemArray);
            }
            if (dttemp.Rows.Count > 0)
            {
                DataView dataView = dttemp.DefaultView; 
                DataTable dataTableDistinct = dataView.ToTable(true, "year", "product");
                foreach (DataRow row in dataTableDistinct.Rows)
                {
                    DataRow[] rows = dttemp.Select("year='" + row["year"] + "' and product='" + row["product"] + "'", "month"); 
                    string[] matharray=new string[12]; 
                    foreach (var dataRow in rows)
                    {
                        matharray[int.Parse(dataRow["month"].ToString()) - 1] = dataRow["count"].ToString(); 
                    }
                    for (int m = 0; m < matharray.Length; m++)
                    {
                        if (string.IsNullOrEmpty(matharray[m]))
                        {
                            matharray[m] = "0";
                        }
                    }
                    datastring = String.Join(",", matharray);
                    sb.Append("{name: '" + row["year"] + "-" + row["product"] + "',data: [" + datastring + "]},");
                }
                dataView.Dispose();
                dataTableDistinct.Dispose();
                resultstring = sb.ToString().Substring(0, sb.ToString().Length - 1);
            }
            dttemp.Dispose();  
        }  
    }

    protected void BtnSearch0_Click(object sender, EventArgs e)
    {
        Fresh();
    }
}