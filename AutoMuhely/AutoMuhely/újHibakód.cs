using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AutoMuhely
{
    public partial class újHibakód : Form
    {
        public bool IsEditMode { get; set; }
        public string OriginalKod { get; set; }
        public string OriginalLeiras { get; set; }
        public újHibakód(string hibakod = null, string leiras = null)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(hibakod))
            {
                IsEditMode = true;
                OriginalKod = hibakod;  // Save the original kod for comparison
                OriginalLeiras = leiras; // Save the original leiras
                txtHibakod.Text = hibakod;
                txtHibakodLeiras.Text = leiras;
                btnAdd.Text = "Módosítás";  // Change the button text to Módosítás
                editLabel.Text = "MÓDOSÍTÁSA";

            }
            else
            {
                IsEditMode = false;
                btnAdd.Text = "Hozzáadás";  // Change the button text to Hozzáadás
                editLabel.Text = "HOZZÁADÁSA";
            }
        }

        DatabaseHandler DatabaseHandler = new DatabaseHandler();
        private void újHibakód_Load(object sender, EventArgs e)
        {

        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new MySqlConnection(DatabaseHandler.ConnectionCommand))
                {
                    connection.Open();

                    if (IsEditMode)  // If we're in Edit mode, update the existing record
                    {
                        string commandText = "UPDATE hibakodok SET leiras = @leiras WHERE kod = @kod";

                        using (var command = new MySqlCommand(commandText, connection))
                        {
                            command.Parameters.AddWithValue("@kod", txtHibakod.Text);
                            command.Parameters.AddWithValue("@leiras", txtHibakodLeiras.Text);

                            var result = command.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("Hibakód módosítva!", "Siker!", MessageBoxButtons.OK);
                                this.Close();
                            }
                        }
                    }
                    else  // If we're in Add mode, insert a new record
                    {
                        string commandText = "INSERT INTO hibakodok (kod, leiras) VALUES(@kod, @leiras)";

                        using (var command = new MySqlCommand(commandText, connection))
                        {
                            command.Parameters.AddWithValue("@kod", txtHibakod.Text);
                            command.Parameters.AddWithValue("@leiras", txtHibakodLeiras.Text);

                            var result = command.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("Sikeres hibakód hozzáadás!", "Siker!", MessageBoxButtons.OK);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba: {ex.Message}", "Hiba!", MessageBoxButtons.OK);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void újHibakód_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (IsEditMode)
            {
                // If it's edit mode and the user closed the form without saving, revert changes
                txtHibakod.Text = OriginalKod;
                txtHibakodLeiras.Text = OriginalLeiras;
            }
        }
    }
}
