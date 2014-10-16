using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspNetDataHandler.Functions.Database;

namespace AspNetDataHandler
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
		{
            using (var db = new Database())
            {
                var record = db.GotoRecord("[AspNetDataHandler].[dbo].[FormEntry]",
                    Guid.Parse("2764CBCD-F3CB-464F-8D35-4D87D5694C00"));
                
            }
		}
    }
}