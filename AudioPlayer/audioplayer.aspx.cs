using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspNetDataHandler.Functions.Application;
using AspNetDataHandler.Functions.Database;

namespace AspNetDataHandler.AudioPlayer
{
    public partial class audioplayer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplicationFunctions.ValidateUser(Response, Session, "/index.aspx", true);

            Guid recordGuid;
            var isGuid = Guid.TryParse(Request["guid"], out recordGuid);

            if (!isGuid)
            {
                Response.Write("No valid guid");
                return;    
            }

            using (var db = new Database())
            {
                var record = db.GotoRecord("[AspNetDataHandler].[dbo].[FormEntry]", recordGuid);
                if (record == null)
                {
                    Response.Write("Record does not exists");
                    return;
                }

                ApplicationFunctions.PlayAudio(audiocontroller, (byte[])record["Music"], recordGuid);
            }

        }
    }
}