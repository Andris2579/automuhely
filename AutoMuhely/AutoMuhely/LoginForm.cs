using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AutoMuhely
{

    public partial class LoginForm : Form
    {
        public string Username { get; private set; }
        public string Role { get; private set; }
        DatabaseHandler databaseHandler = new DatabaseHandler();
        // Constructor
        public LoginForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }
        private void LogIn()
        {
            string username = txtUsername.Text;
            string password = databaseHandler.HashPassword(txtPassword.Text);
            var parameter1 = new Dictionary<string, object>
    {
        { "@name", username },  // For the username lookup
    };

            var parameter2 = new Dictionary<string, object>
    {
        { "@jelszo_hash", password },  // For the password comparison
    };

            // Query to check if username exists (check for username first)
            var (result, columns) = databaseHandler.Select(@"SELECT jelszo_hash FROM felhasznalok WHERE felhasznalonev=@name;", parameter1);

            // Check if result contains rows (i.e., username exists)
            if (result != null && result.Count > 0 && Convert.ToString(result[0][0]) != "")
            {
                // Compare password if the username exists
                if (password == Convert.ToString(result[0][0]))
                {
                    // Query to fetch role after successful password check
                    var (result2, columns2) = databaseHandler.Select(@"SELECT szerep FROM felhasznalok WHERE felhasznalonev=@name;", parameter1);

                    // Check if result2 contains valid data (role exists)
                    if (result2 != null && result2.Count > 0)
                    {
                        Username = username;
                        Role = Convert.ToString(result2[0][0]);

                        // Clear text fields and close the form
                        txtUsername.Text = "";
                        txtPassword.Text = "";
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hiba történt a szerep lekérdezésekor!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Rossz felhasználónév vagy jelszó. Kérem próbálja újra!", "Bejelentkezés sikertelen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Rossz felhasználónév vagy jelszó. Kérem próbálja újra!", "Bejelentkezés sikertelen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            LogIn();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close the login form and return DialogResult.Cancel for a canceled login
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LogIn();
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            databaseHandler.DatabaseConnect();
            txtUsername.Text = "admin";
            txtPassword.Text = "password";
        }
    }
}