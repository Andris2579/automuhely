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
    public partial class alkatrészInputs : Form
    {
        public event EventHandler AlkatrészHozzáadva;

        public alkatrészInputs()
        {
            InitializeComponent();
            this.Size = new Size(225, 280);
            this.Font = new Font("Arial Round MT", 12);
        }

        private void alkatrészInputs_Load(object sender, EventArgs e)
        {
        }

        DatabaseHandler databaseHandler = new DatabaseHandler();

        private void AlkatrészHozzáadás_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new MySqlConnection(databaseHandler.ConnectionCommand))
                {
                    connection.Open();
                    string commandText = "INSERT INTO alkatreszek (nev, leiras, keszlet_mennyiseg, utanrendelesi_szint) VALUES(@nev, @leiras, @keszlet_mennyiseg, @utanrendelesi_szint)";

                    using (var command = new MySqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("@nev", alkatrészNév_Tb.Text);
                        command.Parameters.AddWithValue("@leiras", alkatrészLeírás_Tb.Text);
                        command.Parameters.AddWithValue("@keszlet_mennyiseg", alkatrészKezdetiKészletMennyiség_NUD.Value);
                        command.Parameters.AddWithValue("@utanrendelesi_szint", 0);  // vagy lehetne dinamikus is, ha szükséges

                        var result = command.ExecuteNonQuery();
                        if(result > 0)
                        {
                            AlkatrészHozzáadva?.Invoke(this, EventArgs.Empty);
                            MessageBox.Show("Sikeres alkatrész hozzáadás!", "Siker!", MessageBoxButtons.OK);
                            this.Close();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}", "Hiba", MessageBoxButtons.OK);
            }
        }
    }
}
