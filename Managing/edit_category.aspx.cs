using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspNetDataHandler.Functions.Application;
using AspNetDataHandler.Functions.Database;

namespace AspNetDataHandler.Managing
{
    public partial class edit_category : System.Web.UI.Page
    {
        private Guid _recordGuid;

        protected void Page_Load(object sender, EventArgs e)
        {
            ApplicationFunctions.ValidateUser(Response, Session, Request.Url.AbsolutePath);

            var isGuid = Guid.TryParse(Request["recordguid"], out _recordGuid);

            if (isGuid)
            {
                SaveButton.Text = "Toepassen";

                using (var db = new Database())
                {
                    var result = db.ExecuteQueryWithResult(@"SELECT [Name] FROM [AspNetDataHandler].[dbo].[Category] WHERE RecordGUID = @guid", 
                        new Dictionary<string, object> { { "guid", _recordGuid } });

                    if (result.Rows.Count == 0)
                        return;

                    var row = result.Rows[0];

                    if (!IsPostBack)
                    {
                        NameTextBox.Text = (string)row["Name"];
                    }

                }
            }
            else
            {
                SaveButton.Text = "Opslaan";
            }

            NameTextBox.Focus();

        }

        protected void SaveButton_OnClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(NameTextBox.Text))
            {
                ApplicationFunctions.Alert(Response, "Voer een naam in!");
                NameTextBox.BackColor = Color.Orange;
                NameTextBox.Focus();
                return;
            }
            using (var db = new Database())
            {
                if (_recordGuid == Guid.Empty)
                {

                    db.ExecuteNonResultQuery(
                        @"INSERT INTO [AspNetDataHandler].[dbo].[Category] ([Name]) VALUES (@name)",
                        new Dictionary<string, object>
                        {
                            {"name", NameTextBox.Text}
                        });
                }
                else
                {
                    DataRow record = db.GotoRecord("[AspNetDataHandler].[dbo].[Category]", _recordGuid);
                    record["Name"] = NameTextBox.Text;
                }
            }

            Response.Redirect("/Managing/managing_categorys.aspx");
        }
    }
}