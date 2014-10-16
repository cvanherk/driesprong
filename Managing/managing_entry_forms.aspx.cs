using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspNetDataHandler.Functions.Application;
using AspNetDataHandler.Functions.Database;

namespace AspNetDataHandler.Managing
{
    public partial class managing_entry_forms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplicationFunctions.ValidateUser(Response, Session, Request.Url.AbsolutePath);
        }

        public string RenderQueueTable()
        {
            var str = "";

            using (var db = new Database())
            {
                var result = db.ExecuteQueryWithResult(@"SELECT        FormEntry.RecordGUID, FormEntry.Name, FormEntry.ref_Horse1, Horses.Name AS Horse1Name, Horses_1.Name AS Horse2Name, FormEntry.ref_Horse2, Horses_2.Name AS Horse3Name, 
                         FormEntry.ref_Horse3, Category.Name AS CategoryName
FROM            FormEntry INNER JOIN
                         Horses ON FormEntry.ref_Horse1 = Horses.RecordGUID INNER JOIN
                         Horses AS Horses_1 ON FormEntry.ref_Horse2 = Horses_1.RecordGUID INNER JOIN
                         Horses AS Horses_2 ON FormEntry.ref_Horse3 = Horses_2.RecordGUID INNER JOIN
                         Category ON FormEntry.ref_Category = Category.RecordGUID");


                var i = 1;
                foreach (DataRow record in result.Rows)
                {
                    str += String.Format(@"
                <tr>
                    <td>
                        Naam: <b>{0}</b>
                    </td>
                    <td>
                        Klasse: <b>{1}</b>
                    </td>
                    <td>
                        Paard 1: <b>{2}</b> <input type=""radio"" name=""row_{3}"" value=""{4}"">
                    </td>
                    <td>
                        Paard 2: <b>{5}</b> <input type=""radio"" name=""row_{3}"" value=""{6}"">
                    </td>
                    <td>
                        Paard 3: <b>{7}</b> <input type=""radio"" name=""row_{3}"" value=""{8}"">
                    </td>
                    <td>
                        <img id=""buttonAccept"" src=""/Content/Images/Buttons/accept.png"" guid=""{9}""/>
                    </td>
                </tr>", HttpUtility.HtmlEncode(record["Name"]), HttpUtility.HtmlEncode(record["CategoryName"]), HttpUtility.HtmlEncode(record["Horse1Name"]), i, 
                      record["ref_Horse1"], HttpUtility.HtmlEncode(record["Horse2Name"]), record["ref_Horse2"], HttpUtility.HtmlEncode(record["Horse3Name"]), record["ref_Horse3"], record["RecordGUID"]);
                    i++;
                }
            }

            return str;
        }
    }
}