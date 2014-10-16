using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspNetDataHandler.Functions.Application;
using AspNetDataHandler.Functions.Database;

namespace AspNetDataHandler
{
    public partial class managing_horses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplicationFunctions.ValidateUser(Response, Session, Request.Url.AbsolutePath);
        }

        public string GenerateHorseTable()
        {
            DataTable result;
            using (var db = new Database())
            {
                result = db.ExecuteQueryWithResult("SELECT [RecordGUID], [Name] FROM [AspNetDataHandler].[dbo].[Horses] ORDER BY Name");
            }
           
            return result.Rows.Cast<DataRow>().Aggregate("", (current, o) => current + String.Format(@"
<tr>
    <td id=""horseItem"" guid=""{1}"" style=""width: 310px; border-left: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black; border-top: 1px solid black;"">
        {0}
    </td>
    <td>
       <div id=""deleteButton"" guid=""{1}""style=""color: red; cursor: pointer;"">X</div> 
    </td>
</tr>", HttpUtility.HtmlEncode(o["Name"]), HttpUtility.HtmlEncode(o["RecordGUID"])));
        }
    }
}