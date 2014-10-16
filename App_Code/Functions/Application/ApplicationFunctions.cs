using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using AspNetDataHandler.Functions.Application;

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
                    var parameters = new Dictionary<String, object> { { "guid", session["RecordGUID"]} };
                    var result = db.ExecuteQueryWithResult("SELECT COUNT(RecordGUID) FROM  [AspNetDataHandler].[dbo].[User] WHERE RecordGUID = @guid", parameters);

                    var row = result.Rows[0];

                    if (!row[0].Equals(0)) 
                        return;
                    if (!ajaxRequest)
                        response.Redirect("/login.aspx");
                    else
                        response.Write("Not logged in");
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

        //Bestaat audio al? Dan wordt die alleen geladen.
        public static void PlayAudio(HtmlEmbed audioController, byte[] bytes, Guid recordGuid)
        {
            const string tempPath = ApplicationGlobals.TempPath;
            var directory = tempPath + recordGuid.ToString("N");
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(directory)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(directory));
            }

            var file = directory + "/music.mp3";

            if (File.Exists(HttpContext.Current.Server.MapPath(file)))
            {
                audioController.Src = file;
                return;
            }

            using (var filestream = File.Create(HttpContext.Current.Server.MapPath(file)))
            {
                foreach (var b in bytes)
                    filestream.WriteByte(b);
            }

            audioController.Src = file;

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