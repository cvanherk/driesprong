using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspNetDataHandler.Functions.Database;

namespace AspNetDataHandler
{
    public partial class header : System.Web.UI.Page
    {
        public static string Username = null;

        public bool IsLoggedIn
        {
            get { return !(Session["RecordGUID"] is String); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Username))
                return;

            if (Session["RecordGUID"] is String)
                return;

            using (var db = new Database())
            {
                var result = db.ExecuteQueryWithResult("SELECT [Username] FROM [AspNetDataHandler].[dbo].[User] WHERE RecordGUID = @guid",
                    new Dictionary<string, object> { { "guid", Session["RecordGUID"] } });

                if (result.Rows.Count == 0)
                {
                    throw new Exception("Gebruiker bestaat niet!");
                }

                var row = result.Rows[0];
                Username = (string)row["Username"];
            }
        }

        public string HeaderMessage()
        {
            return IsLoggedIn ? String.Format(@"
<div style=""color: red;"">
    Welkom: {0} <a href=""/index.aspx"">Corne corporation</a>
</div> <a href=""/logout.aspx"">Logout</a>", HttpUtility.HtmlEncode(Username)) 
                : 
@"<a href=""/index.aspx"">Corne corporation</a>";
        }
    }
}