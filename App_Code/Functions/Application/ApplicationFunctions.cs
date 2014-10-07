﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace AspNetDataHandler.Functions.Application
{
    public class ApplicationFunctions
    {

        public static void ValidateUser(HttpResponse response, HttpSessionState session, string urlLoginSucceded, bool ajaxRequest = false)
        {
            session["RedirectLoginUrl"] = urlLoginSucceded;
            if (session["RecordGUID"] is Guid)
            {
                using (var db = new Database.Database())
                {
                    var parameters = new Dictionary<String, String> { { "guid", Guid.Parse((session["RecordGUID"].ToString())).ToString("N") } };
                    var result = db.ExecuteQueryWithResult("SELECT COUNT(RecordGUID) FROM  [AspNetDataHandler].[dbo].[User] WHERE RecordGUID = @guid", parameters);

                    var row = result.Rows[0];

                    if (row[0].Equals(0))
                    {
                        if (!ajaxRequest)
                            response.Redirect("/login.aspx");
                        else
                            response.Write("Not logged in");
                    }
                }
            }
            else
            {
                if (!ajaxRequest)
                    response.Redirect("/login.aspx");
                else
                    response.Write("Not logged in");
            }
        }

        public static void Alert(HttpResponse response, string message)
        {
            response.Write(String.Format(@"<SCRIPT LANGUAGE=""JavaScript"">alert(""{0}"")</SCRIPT>", message));
        }

        public static void Logout(HttpResponse response, HttpSessionState session, bool debug)
        {
            session["RecordGUID"] = "";
            if (!debug)
                response.Redirect("/index.aspx");
        }
    }
}