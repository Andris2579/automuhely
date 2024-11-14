using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                var (result, columns) = databaseHandler.Select($"SELECT jelszo_hash from felhasznalok WHERE felhasznalonev='{username}';");
                // Simple username and password validation
                if (password == Convert.ToString(result[0][0])) // Customize as needed
                {
                    this.DialogResult = DialogResult.OK;
                    var (result2, columns2) = databaseHandler.Select($"SELECT szerep from felhasznalok WHERE felhasznalonev='{username}';");
                    Username = username;
                    Role = Convert.ToString(result2[0][0]);
                txtUsername.Text = "";
                txtPassword.Text = "";
                this.Close();
                    
                
                }
                else {
                    MessageBox.Show("Rossz felhasználónév vagy jelszó. Kérem próbálja újra!", "Bejelentzkezés sikertelen", MessageBoxButtons.OK, MessageBoxIcon.Error);
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