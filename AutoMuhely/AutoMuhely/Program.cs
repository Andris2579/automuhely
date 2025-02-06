using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoMuhely
{
    internal static class Program
    {
        private static readonly string MutexGuid = "Global\\{E2F8896C-7A4B-4A3F-973F-6355C90B2EAA}";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(true, MutexGuid, out bool createdNew))
            {
                // Check if another instance is already running
                if (!createdNew)
                {
                    MessageBox.Show("Another instance of this application is already running.",
                                    "Instance Already Running",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                try
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
                finally
                {
                    // Release the mutex if the application exits
                    mutex.ReleaseMutex();
                }
            }
        }
    }
}
