<%@ WebHandler Language="C#" Class="LabelSearch" %>

using System;
using System.Web;
using TJ.DBUtility;
using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Collections;

public class LabelSearch : IHttpHandler
{
    TabExecute _tab = new TabExecute();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        //context.Response.Write("Hello World");
        ArrayList mArrayList = new ArrayList();
        string label5 = context.Request.QueryString["label"];
        if (!string.IsNullOrEmpty(context.Request.QueryString["label"]))
        {
            string label = context.Request.QueryString["label"];
            //string mUserName = context.Request.QueryString["name"];

            DataTable mDataTable = _tab.ExecuteQuery("select top 5 * from TB_BaseLabelInfo where LabelCode=" + label, null);
            if (mDataTable.Rows.Count > 0)
            {
                for (int i = 0; i < mDataTable.Rows.Count; i++)
                {
                    Dictionary<String, Object> mDictionary = new Dictionary<string, object>();
                    mDictionary.Add("LabelCode", mDataTable.Rows[i]["LabelCode"]);
                    mDictionary.Add("YanZhengMa", mDataTable.Rows[i]["YanZhengMa"]);
                    mDictionary.Add("JXName", mDataTable.Rows[i]["JXName"]);
                    //mDictionary.Add("AuthorDiscount", mDataTable.Rows[i]["AuthorDiscount"]);
                    mArrayList.Add(mDictionary);
                }

                context.Response.Write(JsonConvert.SerializeObject(formatJsonMessage(1, "查询成功！", mArrayList)));
            }
            else
            {
                context.Response.Write(JsonConvert.SerializeObject(formatJsonMessage(2, "没有数据！", mArrayList)));
            }
        }
        else
        {
            context.Response.Write(JsonConvert.SerializeObject(formatJsonMessage(0, "参数格式错误2！", mArrayList)));
        }
    }

    public Dictionary<String, Object> formatJsonMessage(int code, String message, ArrayList mArrayList)
    {
        Dictionary<String, Object> mDictionary = new Dictionary<String, Object>();

        mDictionary.Add("code", code);
        String code_message = "";
        if (code == 1)
        {
            code_message = "1";
            if (!string.IsNullOrEmpty(message))
            {
                code_message = message;
            }
        }
        else if (code == 0)
        {
            code_message = "0";
            if (!string.IsNullOrEmpty(message))
            {
                code_message = message;
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(message))
            {
                code_message += message;
            }
        }

        mDictionary.Add("detail", mArrayList);
        mDictionary.Add("message", code_message);
        return mDictionary;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}