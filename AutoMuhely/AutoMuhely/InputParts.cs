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
    public partial class InputParts : Form
    {
        public bool IsEditMode { get; set; }
        public string OriginalAlkatresz { get; set; }
        public InputParts(string alkatrészName = null, string leiras = null, decimal keszlet = 0, decimal utanrendeles = 0)
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
                lblIsEdit.Text = "HOZZÁADÁSA";
                btnAdd.Text = "Hozzáadás";  // Change button text to Hozzáadás
            }
        }

        private void alkatrészInputs_Load(object sender, EventArgs e)
        {
        }

        DatabaseHandler databaseHandler = new DatabaseHandler();
        public event EventHandler AlkatreszHozzaadva;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(alkatrészNév_Tb.Text) ||
           string.IsNullOrWhiteSpace(alkatrészLeírás_Tb.Text))
                {
                    MessageBox.Show("Kérjük, töltse ki az összes mezőt és adjon meg pozitív értékeket!", "Hiányzó vagy érvénytelen adatok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (IsEditMode)  // If we're in Edit mode, update the record
                    {
                    string updateQuery = "UPDATE alkatreszek SET leiras = @leiras, keszlet_mennyiseg = @keszlet_mennyiseg, utanrendelesi_szint = @utanrendelesi_szint WHERE nev = @nev";
                    var parameters = new Dictionary<string, object>
                    {
                        { "@nev", alkatrészNév_Tb.Text },
                        { "@leiras", alkatrészLeírás_Tb.Text },
                        { "@keszlet_mennyiseg", alkatrészKezdetiKészletMennyiség_NUD.Value},
                        { "@utanrendelesi_szint", numAlkatreszUtanrendelesMenny.Value }
                    };
                    databaseHandler.Update(updateQuery, parameters);

                    MessageBox.Show("Alkatrész sikeresen módosítva!", "Siker!", MessageBoxButtons.OK);

                    }
                    else  // If we're in Add mode, insert a new record
                    {
                    // Add Mode: Insert a new record
                    string insertQuery = "INSERT INTO alkatreszek (nev, leiras, keszlet_mennyiseg, utanrendelesi_szint) VALUES(@nev, @leiras, @keszlet_mennyiseg, @utanrendelesi_szint)";
                    var parameters = new Dictionary<string, object>
                        {
                            { "@nev", alkatrészNév_Tb.Text },
                            { "@leiras", alkatrészLeírás_Tb.Text },
                            { "@keszlet_mennyiseg", alkatrészKezdetiKészletMennyiség_NUD.Value},
                            { "@utanrendelesi_szint", numAlkatreszUtanrendelesMenny.Value}
                        };
                    databaseHandler.Insert(insertQuery, parameters);

                    MessageBox.Show("Alkatrész sikeresen hozzáadva!", "Siker!", MessageBoxButtons.OK);
                    }
                AlkatreszHozzaadva?.Invoke(this, EventArgs.Empty);
                this.Close();
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
