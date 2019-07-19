using System;
using System.Data;
using TJ.BLL;
using TJ.DBUtility;
using System.Text.RegularExpressions;

public partial class Admin_TJ_SiteMapPageNameAddEdit : System.Web.UI.Page
{
    readonly TabExecute tab = new TabExecute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string mDirectoryId = Request.QueryString["ID"];
            if (!string.IsNullOrEmpty(mDirectoryId))
            {
                DisplayData(mDirectoryId);
            }
        }

    }

    private void DisplayData(string mDirectoryId)
    {
        DataTable mDataTable = tab.ExecuteNonQuery("select [AuthorMenuInfo],[RID] from [TJ_RoleInfo]");
        string mRoleNameString = "";
        int num = 0;
        if (mDataTable.Rows.Count > 0)
        {

            for (int i = 0; i < mDataTable.Rows.Count; i++)
            {
                string mAuthorMenu = mDataTable.Rows[i][0].ToString();
                string[] mAuthorMenuArray = Regex.Split(mAuthorMenu, ",", RegexOptions.IgnoreCase);
                foreach (string mAuthorMenuCode in mAuthorMenuArray)
                {
                    if (mAuthorMenuCode.Equals(mDirectoryId))
                    {
                        if (num==0)
                        {
                            mRoleNameString += "<tr>"
;                        }
                        string mRoleName = tab.ExecuteQueryForSingleValue("select [RoleName] from [TJ_RoleInfo] where [RID]=" + mDataTable.Rows[i][1].ToString());
                        if (num < 3) {
                            mRoleNameString += "<td>" + mRoleName + "</td>";
                            num++;
                        }else
                        {
                            mRoleNameString += "<td>" + mRoleName + "</td><tr>";
                            num = 0;
                        }
                    }
                }
            }
        }
        role_name_list.InnerHtml = mRoleNameString;
    }
}