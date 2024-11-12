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

namespace AutoMuhely
{
    public partial class újÚtmutató : Form
    {
        public újÚtmutató()
        {
            InitializeComponent();
        }

        DatabaseHandler databaseHandler = new DatabaseHandler();

        public event EventHandler ÚtmutatóHozzáadva;


        private void újÚtmutató_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new MySqlConnection(databaseHandler.ConnectionCommand))
                {
                    connection.Open();
                    string commandText = "INSERT INTO szerelesi_utmutatok (cim, tartalom, jarmu_tipus) VALUES(@cim, @tartalom, @jarmu_tipus)";

                    using (var command = new MySqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("@cim", textBox1.Text);
                        command.Parameters.AddWithValue("@tartalom", textBox2.Text);
                        command.Parameters.AddWithValue("@jarmu_tipus", textBox3.Text);

                        var result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            ÚtmutatóHozzáadva?.Invoke(this, EventArgs.Empty);
                            MessageBox.Show("Sikeres útmutató hozzáadás!", "Siker!", MessageBoxButtons.OK);
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}", "Hiba", MessageBoxButtons.OK);
            }
        }
    }
}
