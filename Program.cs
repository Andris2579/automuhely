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

            // Create an instance of the login form
            LoginForm loginForm = new LoginForm();

            // Show the login form
            if (loginForm.ShowDialog() == DialogResult.OK) // If login is successful
            {
                // Open the main form after successful login
                Application.Run(new Main_Form()); // Your main form
            }
            else
            {
                // Exit if the login is canceled or failed
                Application.Exit();
            }
        }
    }
}
