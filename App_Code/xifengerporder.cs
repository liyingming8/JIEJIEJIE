using System;
using System.Web.Services;
using Newtonsoft.Json.Linq;
using TJ.BLL;
using TJ.Model;

/// <summary>
/// xifengerporder 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class xifengerporder : System.Web.Services.WebService {

    public xifengerporder () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }
    BTJ_XF_ERP_OrderInfo _btjXfErp = new BTJ_XF_ERP_OrderInfo();
    BTJ_XF_ERP_OrderDetail _btjXfErpOrderDetail = new BTJ_XF_ERP_OrderDetail();
    /// <summary>
    /// 西凤ERP订单信息录入
    /// </summary>
    /// <param name="erpordercode">需货申请订单号</param>
    /// <param name="orderdate">需货申请订单时间</param>
    /// <param name="materialcode">物料编码</param>
    /// <param name="prodname">产品名称</param>
    /// <param name="agentname">经销商名称</param>
    /// <param name="agentcode">经销商编码</param>
    /// <returns></returns>
    //[WebMethod]
    public string ErpOrderInfo(string erpordercode, string orderdate, string materialcode, string prodname, string agentname, string agentcode)
    {
        object obj = _btjXfErp.Insert(new MTJ_XF_ERP_OrderInfo(0, erpordercode, Convert.ToDateTime(orderdate), materialcode, prodname, agentname,
            agentcode, DateTime.Now));
        if (int.Parse(obj.ToString()) > 0)
        {
            return "ok";
        }
        else
        {
            return "false";
        }
    }

    [WebMethod]
    public string ErpOderInfoJsonString(string str)
    {
        try
        {
            JObject obj = JObject.Parse(str);
            string erpordercode = obj["erpordercode"].ToString();
            string orderdate = obj["orderdate"].ToString();
            string agentname = obj["agentname"].ToString();
            string agentcode = obj["agentcode"].ToString();
            object rst =
                _btjXfErp.Insert(new MTJ_XF_ERP_OrderInfo(0, erpordercode, Convert.ToDateTime(orderdate), "", "",
                    agentname, agentcode, DateTime.Now));
            JArray detailarray = JArray.Parse(obj["orderdetail"].ToString());
            string materialcode;
            string prodname;
            foreach (var jToken in detailarray)
            {
                var o = (JObject) jToken;
                materialcode = o["materialcode"].ToString();
                prodname = o["prodname"].ToString();
                _btjXfErpOrderDetail.Insert(new MTJ_XF_ERP_OrderDetail(0, int.Parse(rst.ToString()),materialcode,prodname,1,""));
            }
            return "ok";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

}
