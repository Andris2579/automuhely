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
    public partial class módosítHibakód : Form
    {
        public módosítHibakód()
        {
            InitializeComponent();
        }

        public event EventHandler MódosítHibakód;
        DatabaseHandler databaseHandler =  new DatabaseHandler();

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string commandString = $"SELECT leiras FROM hibakodok WHERE kod = '{comboBox1.Text}'";
            var (eredmeny, oszlopNevek) = databaseHandler.Select(commandString);
            textBox2.Text = eredmeny[0][0].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string commandString = $"UPDATE hibakodok SET leiras = '{textBox2.Text}' WHERE kod = '{comboBox1.Text}'";
            try
            {
                using(var connection = new MySqlConnection(databaseHandler.ConnectionCommand))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(commandString, connection);
                    var result = command.ExecuteNonQuery();
                    if(result > 0)
                    {
                        MódosítHibakód?.Invoke(this, EventArgs.Empty);
                        MessageBox.Show("Sikeres hibakód módosítás!", "Siker!", MessageBoxButtons.OK);
                        this.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Hiba: {ex.Message}", "Hiba!", MessageBoxButtons.OK);
            }
        }

        private void módosítHibakód_Load(object sender, EventArgs e)
        {
            string commandString = "SELECT kod FROM hibakodok";
            var (eredmeny, oszlopNevek) = databaseHandler.Select(commandString);
            foreach(var item in eredmeny)
            {
                comboBox1.Items.Add(item[0]);
            }
        }
    }
}
