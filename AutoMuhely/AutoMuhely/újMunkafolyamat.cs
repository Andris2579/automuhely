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
    public partial class újMunkafolyamat : Form
    {
        DatabaseHandler databaseHandler = new DatabaseHandler();
        public újMunkafolyamat()
        {
            InitializeComponent();
        }

        public event EventHandler újMunkafolyamatHozzáadva;

        private void újMunkafolyamat_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new MySqlConnection(databaseHandler.ConnectionCommand))
                {
                    connection.Open();
                    string commandText = "INSERT INTO munkafolyamat_sablonok (nev, lepesek, becsult_ido) VALUES(@nev, @lepesek, @becsult_ido)";

                    using (var command = new MySqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("@nev", txtSablonNev.Text);
                        command.Parameters.AddWithValue("@lepesek", txtSablonleiras.Text);
                        command.Parameters.AddWithValue("@becsult_ido", numSablonIdo.Value+" nap");

                        var result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Sikeres alkatrész hozzáadás!", "Siker!", MessageBoxButtons.OK);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
