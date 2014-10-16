﻿using System;
using System.Collections.Generic;
using AspNetDataHandler.Functions.Application;
using AspNetDataHandler.Functions.Database;

namespace AspNetDataHandler.Managing
{
    public partial class delete_category : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplicationFunctions.ValidateUser(Response, Session, "/Managing/managing_categorys.aspx", true);
            Guid guid;
            var validGuid = Guid.TryParse(Request["guid"], out guid);

            if (!validGuid) 
                return;
            
            using (var db = new Database())
            {
                db.ExecuteNonResultQuery("DELETE FROM [dbo].[Category] WHERE RecordGUID = @guid", new Dictionary<string, object> {{ "guid", guid }});
            }
        }
    }
}