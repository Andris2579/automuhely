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

        Label alkatrészNév_Lb = new Label();
        TextBox alkatrészNév_Tb = new TextBox();
        Label alkatrészLeírás_Lb = new Label();
        TextBox alkatrészLeírás_Tb = new TextBox();
        Label alkatrészKezdetiKészlet_Lb = new Label();
        NumericUpDown alkatrészKezdetiKészletMennyiség_NUD = new NumericUpDown();

        private void alkatrészInputs_Load(object sender, EventArgs e)
        {
            alkatrészNév_Lb.Location = new Point(25, 25);
            alkatrészNév_Lb.Size = new Size(250, 25);
            alkatrészNév_Lb.Text = "Alkatrész neve";
            this.Controls.Add(alkatrészNév_Lb);

            alkatrészNév_Tb.Location = new Point(25, 50);
            alkatrészNév_Tb.Size = new Size(250, 25);
            alkatrészNév_Tb.MaxLength = 100;
            this.Controls.Add(alkatrészNév_Tb);

            alkatrészLeírás_Lb.Location = new Point(25, 90);
            alkatrészLeírás_Lb.Size = new Size(250, 25);
            alkatrészLeírás_Lb.Text = "Alkatrész leírása";
            this.Controls.Add(alkatrészLeírás_Lb);

            alkatrészLeírás_Tb.Location = new Point(25, 115);
            alkatrészLeírás_Tb.Size = new Size(250, 100);
            alkatrészLeírás_Tb.Multiline = true;
            alkatrészLeírás_Tb.ScrollBars = ScrollBars.Both;
            this.Controls.Add(alkatrészLeírás_Tb);

            alkatrészKezdetiKészlet_Lb.Location = new Point(25, 225);
            alkatrészKezdetiKészlet_Lb.Size = new Size(250, 25);
            alkatrészKezdetiKészlet_Lb.Text = "Kezdeti készlet mennyiség";
            this.Controls.Add(alkatrészKezdetiKészlet_Lb);

            alkatrészKezdetiKészletMennyiség_NUD.Location = new Point(25, 250);
            alkatrészKezdetiKészletMennyiség_NUD.Size = new Size(250, 25);
            alkatrészKezdetiKészletMennyiség_NUD.Minimum = 0;
            this.Controls.Add(alkatrészKezdetiKészletMennyiség_NUD);

            Button alkatrészHozzáadás_Btn = new Button();
            alkatrészHozzáadás_Btn.Location = new Point(50, 290);
            alkatrészHozzáadás_Btn.Size = new Size(200, 50);
            alkatrészHozzáadás_Btn.Text = "Hozzáadás";
            alkatrészHozzáadás_Btn.Click += AlkatrészHozzáadás_Btn_Click;
            this.Controls.Add(alkatrészHozzáadás_Btn);
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
