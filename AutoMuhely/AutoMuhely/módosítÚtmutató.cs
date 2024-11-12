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
    public partial class módosítÚtmutató : Form
    {
        public módosítÚtmutató()
        {
            InitializeComponent();
        }

        DatabaseHandler databaseHandler = new DatabaseHandler();

        public event EventHandler ÚtmutatóMódosítva;

        private void módosítÚtmutató_Load(object sender, EventArgs e)
        {
            var (eredmeny, oszlopNevek) = databaseHandler.Select("SELECT cim FROM szerelesi_utmutatok");

            foreach(var item in eredmeny)
            {
                comboBox1.Items.Add(item[0]);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var (eredmeny, oszlopNevek) = databaseHandler.Select($"SELECT * FROM szerelesi_utmutatok WHERE cim = '{comboBox1.Text}'");

            textBox2.Text = eredmeny[0][2].ToString();
            textBox3.Text = eredmeny[0][3].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using(var connection = new MySqlConnection(databaseHandler.ConnectionCommand))
                {
                    connection.Open();
                    string commandString = $"UPDATE szerelesi_utmutatok SET tartalom = '{textBox2.Text}', jarmu_tipus = '{textBox3.Text}' WHERE cim = '{comboBox1.Text}';";
                    MySqlCommand command = new MySqlCommand(commandString, connection);
                    var result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        ÚtmutatóMódosítva?.Invoke(this, EventArgs.Empty);
                        MessageBox.Show("Sikeres útmutató hozzáadás!", "Siker!", MessageBoxButtons.OK);
                        this.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Hiba: {ex.Message}", "Hiba!", MessageBoxButtons.OK);
            }
        }
    }
}
