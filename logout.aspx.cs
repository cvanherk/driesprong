using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspNetDataHandler.Functions.Application;

namespace AspNetDataHandler
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplicationFunctions.Logout(Response, Session, false);
        }
    }
}