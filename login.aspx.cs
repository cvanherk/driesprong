using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspNetDataHandler.Functions.Database;

namespace AspNetDataHandler
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginControl_OnAuthenticate(object sender, AuthenticateEventArgs e)
        {
            string pass;
            Guid recordGuid;

            using (var db = new Database())
            {
                var parameters = new Dictionary<String, String> {{"username", LoginControl.UserName}};
                var result = db.ExecuteQueryWithResult("SELECT * FROM [dbo].[User] WHERE Username = @username", parameters);

                if (result.Rows.Count == 0)
                    return;

                var row = result.Rows[0];

                

                pass = (string)row["Password"];
                recordGuid = (Guid) row["RecordGUID"];
            }

            if (LoginControl.Password == pass)
            {
                Session["RecordGUID"] = recordGuid;

                Response.Redirect((string)Session["RedirectLoginUrl"]);
            }
        }
    }
}