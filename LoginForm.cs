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

            // Simple username and password validation
            if (username == "admin" && password == "password") // Customize as needed
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // Show an error message
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
    }
}