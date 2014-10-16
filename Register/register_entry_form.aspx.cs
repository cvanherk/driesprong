using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspNetDataHandler.Functions.Application;
using AspNetDataHandler.Functions.Database;

namespace AspNetDataHandler.Register
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                return;
            }

            using (var db = new Database())
            {
                var result = db.ExecuteQueryWithResult("SELECT [RecordGUID], [Name] FROM [AspNetDataHandler].[dbo].[Horses]");

                if (result.Rows.Count == 0)
                {
                    horse1DropDownList.Items.Add(new ListItem("Geen paarden beschikbaar"));
                    return;
                }

                var item = new ListItem
                {
                    Selected = true,
                    Text = "Kies een paard...",
                    Value = ""
                };

                if (!IsPostBack)
                {
                    horse1DropDownList.Items.Add(item);
                    horse2DropDownList.Items.Add(item);
                    horse3DropDownList.Items.Add(item);
                }

                foreach (DataRow row in result.Rows)
                {
                    var guid = (Guid) row["RecordGUID"];
                    item = new ListItem
                    {
                        Text = (string) row["Name"],
                        Value = guid.ToString("N")
                    };

                    if (IsPostBack) 
                        continue;
                    
                    horse1DropDownList.Items.Add(item);
                    horse2DropDownList.Items.Add(item);
                    horse3DropDownList.Items.Add(item);
                }

                result = db.ExecuteQueryWithResult(@"SELECT [RecordGUID]
      ,[Name]
  FROM [AspNetDataHandler].[dbo].[Category]");

                if (result.Rows.Count != 0)
                {
                    if (!IsPostBack)
                    {
                        item = new ListItem
                        {
                            Text = "Kies een klasse...",
                            Value = "",
                            Selected = true
                        };
                        KlasseDropDownList.Items.Add(item);
                    }
                    foreach (DataRow row in result.Rows)
                    {
                        var guid = (Guid) row["RecordGUID"];
                        item = new ListItem
                        {
                            Text = (string) row["Name"],
                            Value = guid.ToString("N")
                        };

                        if (IsPostBack)
                            continue;

                        KlasseDropDownList.Items.Add(item);
                    }
                }
                else
                {
                    item = new ListItem
                    {
                        Text = "Er zijn geen klasses beschikbaar"
                    };
                    
                    if (!IsPostBack) 
                        KlasseDropDownList.Items.Add(item);
                }
            }
        }

        protected void Inschrijven_OnClick(object sender, EventArgs e)
        {
            NameTextBox.BackColor = Color.White;
            horse1DropDownList.BackColor = Color.White;
            horse2DropDownList.BackColor = Color.White;
            horse3DropDownList.BackColor = Color.White;

            if (String.IsNullOrEmpty(NameTextBox.Text))
            {
                ApplicationFunctions.Alert(Response, "Voer een geldige naam in!");
                NameTextBox.BackColor = Color.Orange;
                NameTextBox.Focus();
                return;
            }

            if (String.IsNullOrEmpty(KlasseDropDownList.SelectedItem.Value))
            {
                ApplicationFunctions.Alert(Response, "Je moet een klasse kiezen!");
                KlasseDropDownList.BackColor = Color.Orange;
                KlasseDropDownList.Focus();
                return;
            }

            if (String.IsNullOrEmpty(horse1DropDownList.SelectedItem.Value))
            {
                ApplicationFunctions.Alert(Response, "Je moet een 1ste paard kiezen!");
                horse1DropDownList.BackColor = Color.Orange;
                horse1DropDownList.Focus();
                return;
            }

            if (String.IsNullOrEmpty(horse2DropDownList.SelectedItem.Value))
            {
                ApplicationFunctions.Alert(Response, "Je moet een 2ste paard kiezen!");
                horse2DropDownList.BackColor = Color.Orange;
                horse2DropDownList.Focus();
                return;
            }

            if (String.IsNullOrEmpty(horse3DropDownList.SelectedItem.Value))
            {
                ApplicationFunctions.Alert(Response, "Je moet een 3ste paard kiezen!");
                horse3DropDownList.BackColor = Color.Orange;
                horse3DropDownList.Focus();
                return;
            }

            if (horse1DropDownList.SelectedItem.Value.Equals(horse2DropDownList.SelectedItem.Value) ||
                horse1DropDownList.SelectedItem.Value.Equals(horse3DropDownList.SelectedItem.Value) ||
                horse2DropDownList.SelectedItem.Value.Equals(horse1DropDownList.SelectedItem.Value) ||
                horse2DropDownList.SelectedItem.Value.Equals(horse3DropDownList.SelectedItem.Value) ||
                horse3DropDownList.SelectedItem.Value.Equals(horse1DropDownList.SelectedItem.Value) ||
                horse3DropDownList.SelectedItem.Value.Equals(horse2DropDownList.SelectedItem.Value))
            {
                ApplicationFunctions.Alert(Response, "Kies drie verschillende paarden AUB!");
                horse1DropDownList.BackColor = Color.Orange;
                horse2DropDownList.BackColor = Color.Orange;
                horse3DropDownList.BackColor = Color.Orange;
                return;
            }

            if (FileUploader.HasFile)
            {
                var extension = Path.GetExtension(FileUploader.FileName);
                if (extension != null && !extension.ToLower().Equals(".mp3"))
                {
                    FileUploader.Focus();
                    ApplicationFunctions.Alert(Response, "Je moet een muziek bestand van het type .MP3 uploaden!");
                    return;
                }
            }

            try
            {
                using (var db = new Database())
                {

                    using (var stream = FileUploader.FileContent)
                    {
                        var mp3Bytes = new byte[FileUploader.FileContent.Length];
                        stream.Read(mp3Bytes, 0, mp3Bytes.Length);

                        db.ExecuteNonResultQuery(@"INSERT INTO [AspNetDataHandler].[dbo].[FormEntry]
           ([Name]
           ,[ref_Category]
           ,[ref_Horse1]
           ,[ref_Horse2]
           ,[ref_Horse3]
           ,[Music])
     VALUES (@name, @category, @horse1, @horse2, @horse3, @music)", new Dictionary<string, object>
                        {
                            {"name", NameTextBox.Text},
                            {"category", Guid.Parse(KlasseDropDownList.SelectedItem.Value)},
                            {"horse1", Guid.Parse(horse1DropDownList.SelectedItem.Value)},
                            {"horse2", Guid.Parse(horse2DropDownList.SelectedItem.Value)},
                            {"horse3", Guid.Parse(horse3DropDownList.SelectedItem.Value)},
                            {"music", FileUploader.HasFile ? (object)mp3Bytes : -1 }
                        });
                    }
                }

                Response.Redirect("/Register/register_entry_form_completed.aspx");
            }
            catch (Exception err)
            {
                ApplicationFunctions.Alert(Response,
                    String.Format("Er is iets mis gegaan tijdens het verwerken! Error: {0}", err.Message));
            }
        }

        private void IndexChanged(DropDownList dropDownList)
        {
            if (dropDownList == null) 
                throw new ArgumentNullException("dropDownList");

            if (String.IsNullOrEmpty(dropDownList.SelectedItem.Value))
            {
                RemarkTextBox.Visible = false;
                return;
            }

            using (var db = new Database())
            {
                var result =
                    db.ExecuteQueryWithResult(
                        "SELECT [Remark] FROM [AspNetDataHandler].[dbo].[Horses] WHERE RecordGUID = @guid",
                        new Dictionary<string, object> {{"guid", Guid.Parse(dropDownList.SelectedItem.Value)}});

                var row = result.Rows[0];

                RemarkTextBox.Text = (string)row["Remark"];

                RemarkTextBox.Visible = !String.IsNullOrEmpty(RemarkTextBox.Text);
            }
        }

        protected void horse1DropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            IndexChanged((DropDownList)sender);
        }

        protected void horse2DropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            IndexChanged((DropDownList)sender);        
        }

        protected void horse3DropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            IndexChanged((DropDownList)sender);
        }
    }
}