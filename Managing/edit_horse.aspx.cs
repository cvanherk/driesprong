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
    public partial class edit_horse : System.Web.UI.Page
    {
        private Guid _recordGuid;

        protected void Page_Load(object sender, EventArgs e)
        {
            ApplicationFunctions.ValidateUser(Response, Session, Request.Url.AbsolutePath);
            Guid.TryParse(Request["recordguid"], out _recordGuid);

            if (!_recordGuid.Equals(Guid.Empty))
            {
                SaveButton.Text = "Toepassen";

                using (var db = new Database())
                {
                    var result = db.ExecuteQueryWithResult(@"SELECT [Name]
      ,[Remark]
  FROM [AspNetDataHandler].[dbo].[Horses] WHERE RecordGUID = @guid", new Dictionary<string, string> {{"guid", _recordGuid.ToString("N")}});

                    if (result.Rows.Count == 0)
                        return;

                    var row = result.Rows[0];

                    if (!IsPostBack)
                    {
                        NameTextBox.Text = (string)row["Name"];
                        RemarkTextBox.Text = (string)row["Remark"];
                    }
                    
                }
            }
            else
            {
                SaveButton.Text = "Opslaan";
            }

        }

        protected void SaveButton_OnClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(NameTextBox.Text))
            {
                ApplicationFunctions.Alert(Response, "Naam mag niet leeg zijn!");
                NameTextBox.BackColor = Color.Orange;
                NameTextBox.Focus();
                return;
            }

            using (var db = new Database())
            {
                if (!_recordGuid.Equals(Guid.Empty))
                {
                    var row = db.GotoRecord("[AspNetDataHandler].[dbo].[Horses]", _recordGuid);

                    if (row == null)
                        return;

                    row["Name"] = NameTextBox.Text;
                    row["Remark"] = RemarkTextBox.Text;
                }
                else
                {
                    db.ExecuteNonResultQuery(@"INSERT INTO [AspNetDataHandler].[dbo].[Horses]
           ([Name]
           ,[Remark])
     VALUES
           (@name, @remark)", new Dictionary<string, string> { { "name", NameTextBox.Text }, { "remark", RemarkTextBox.Text } }
                    
                );
                }
            }

            Response.Redirect("/Managing/managing_horses.aspx");
        }
    }
}