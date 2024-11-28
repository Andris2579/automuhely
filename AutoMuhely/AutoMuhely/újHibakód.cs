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
        public újHibakód()
        {
            InitializeComponent();
        }

        DatabaseHandler DatabaseHandler = new DatabaseHandler();
        private void újHibakód_Load(object sender, EventArgs e)
        {

        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            string commandString = $"INSERT INTO hibakodok (kod, leiras) VALUES ('{txtHibakod.Text}', '{txtHibakodLeiras.Text}');";

            try
            {
                using (var connection = new MySqlConnection(DatabaseHandler.ConnectionCommand))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(commandString, connection);
                    var result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Sikeres hibakód hozzáadás!", "Siker!", MessageBoxButtons.OK);
                        this.Close();
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
    }
}
