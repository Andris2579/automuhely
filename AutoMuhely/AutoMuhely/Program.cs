using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoMuhely
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm loginForm = new LoginForm();

            // Show the login form
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Create an instance of Main_Form with the username and role
                Main_Form mainForm = new Main_Form
                {
                    Username = loginForm.Username,
                    Role = loginForm.Role
                };

                // Run the main form
                Application.Run(mainForm);
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
