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
            string password = txtPassword.Text;

            // Prepare parameters for the query
            var parameters = new Dictionary<string, object>
    {
        { "@name", username },
        { "@password", password }
    };

            // Query to check if username and hashed password match
            var query = @"
        SELECT szerep 
        FROM felhasznalok 
        WHERE felhasznalonev = @name AND jelszo_hash = SHA2(@password, 256);
    ";

            // Execute the query
            var (result, columns) = databaseHandler.Select(query, parameters);

            // Validate the result
            if (result != null && result.Count > 0)
            {
                // Successful login
                Username = username;
                Role = Convert.ToString(result[0][0]);

                // Clear text fields and close the form
                txtUsername.Text = "";
                txtPassword.Text = "";
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // Failed login
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