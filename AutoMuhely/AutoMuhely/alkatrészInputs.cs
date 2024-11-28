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
        public bool IsEditMode { get; set; }
        public string OriginalAlkatresz { get; set; }
        public alkatrészInputs(string alkatrészName = null, string leiras = null, decimal keszlet = 0, decimal utanrendeles = 0)
        {
            InitializeComponent();

            // If any data is passed, we are in Edit mode
            if (!string.IsNullOrEmpty(alkatrészName))
            {
                IsEditMode = true;
                OriginalAlkatresz = alkatrészName; // Save the original name for comparison
                alkatrészNév_Tb.Text = alkatrészName;
                alkatrészLeírás_Tb.Text = leiras;
                alkatrészKezdetiKészletMennyiség_NUD.Value = keszlet;
                numAlkatreszUtanrendelesMenny.Value = utanrendeles;
                lblIsEdit.Text = "MÓDOSÍTÁSA";
                btnAdd.Text = "Módosítás";  // Change button text to Módosítás
            }
            else
            {
                IsEditMode = false;
                btnAdd.Text = "Hozzáadás";  // Change button text to Hozzáadás
            }
        }

        private void alkatrészInputs_Load(object sender, EventArgs e)
        {
            /*if (IsEditMode)
            {
                lblIsEdit.Text = "MÓDOSÍTÁS";
                lblIsEdit.Location = new Point();
            }
            else
            {
                lblIsEdit.Text = "HOZZÁADÁSA";
                lblIsEdit.Location = new Point(55, 177);
            }*/
        }

        DatabaseHandler databaseHandler = new DatabaseHandler();

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new MySqlConnection(databaseHandler.ConnectionCommand))
                {
                    connection.Open();

                    if (IsEditMode)  // If we're in Edit mode, update the record
                    {
                        string commandText = "UPDATE alkatreszek SET leiras = @leiras, keszlet_mennyiseg = @keszlet_mennyiseg, utanrendelesi_szint = @utanrendelesi_szint WHERE nev = @nev";

                        using (var command = new MySqlCommand(commandText, connection))
                        {
                            command.Parameters.AddWithValue("@nev", alkatrészNév_Tb.Text);
                            command.Parameters.AddWithValue("@leiras", alkatrészLeírás_Tb.Text);
                            command.Parameters.AddWithValue("@keszlet_mennyiseg", alkatrészKezdetiKészletMennyiség_NUD.Value);
                            command.Parameters.AddWithValue("@utanrendelesi_szint", numAlkatreszUtanrendelesMenny.Value);

                            var result = command.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("Alkatrész módosítva!", "Siker!", MessageBoxButtons.OK);
                                this.Close();
                            }
                        }
                    }
                    else  // If we're in Add mode, insert a new record
                    {
                        string commandText = "INSERT INTO alkatreszek (nev, leiras, keszlet_mennyiseg, utanrendelesi_szint) VALUES(@nev, @leiras, @keszlet_mennyiseg, @utanrendelesi_szint)";

                        using (var command = new MySqlCommand(commandText, connection))
                        {
                            command.Parameters.AddWithValue("@nev", alkatrészNév_Tb.Text);
                            command.Parameters.AddWithValue("@leiras", alkatrészLeírás_Tb.Text);
                            command.Parameters.AddWithValue("@keszlet_mennyiseg", alkatrészKezdetiKészletMennyiség_NUD.Value);
                            command.Parameters.AddWithValue("@utanrendelesi_szint", numAlkatreszUtanrendelesMenny.Value);

                            var result = command.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("Sikeres alkatrész hozzáadás!", "Siker!", MessageBoxButtons.OK);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}", "Hiba", MessageBoxButtons.OK);
            }
        }

        private void alkatrészInputs_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsEditMode)
            {
                // If it's edit mode and the user closed the form without saving, revert changes
                alkatrészNév_Tb.Text = OriginalAlkatresz;
                lblIsEdit.Text = "HOZZÁADÁSA";
                lblIsEdit.Location = new Point(55, 177);
            }
        }
    }
}
