using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace AutoMuhely
{
    public partial class InputCustomers : Form
    {
        public bool IsEditMode { get; set; }
        public string OriginalNev { get; set; }
        public string OriginalCim { get; set; }

        public InputCustomers(string nev = null, string telefonszam = null, string cim = null, string email = null)
        {
            InitializeComponent();

            // If any data is passed, we are in Edit mode
            if (!string.IsNullOrEmpty(nev))
            {
                IsEditMode = true;

                // Split `nev` into `csaladnev` and `keresztnev`
                var nameParts = nev.Split(new[] { ' ' }, 2, StringSplitOptions.None);
                txtCsaladNev.Text = nameParts.Length > 0 ? nameParts[0] : string.Empty;
                txtKeresztNev.Text = nameParts.Length > 1 ? nameParts[1] : string.Empty;
                OriginalNev = nev;

                // Split `cim` into components
                if (!string.IsNullOrEmpty(cim))
                {
                    var cimParts = cim.Split(',');
                    numIranyitoSz.Text = cimParts.Length > 0 ? cimParts[0].Trim() : string.Empty;
                    txtVaros.Text = cimParts.Length > 1 ? cimParts[1].Trim() : string.Empty;
                    txtUtca.Text = cimParts.Length > 2 ? cimParts[2].Trim() : string.Empty;
                    txtOptional.Text = cimParts.Length > 3 ? cimParts[3].Trim() : string.Empty;
                }

                txtTel.Text = telefonszam;
                txtEmail.Text = email;

                editLabel.Text = "MÓDOSÍTÁSA";
                btnAdd.Text = "Módosítás"; // Change button text to Módosítás
            }
            else
            {
                IsEditMode = false;
                editLabel.Text = "HOZZÁADÁSA";
                btnAdd.Text = "Hozzáadás"; // Change button text to Hozzáadás
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(txtCsaladNev.Text) ||
                    string.IsNullOrWhiteSpace(txtKeresztNev.Text) ||
                    string.IsNullOrWhiteSpace(txtTel.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(numIranyitoSz.Text) ||
                    string.IsNullOrWhiteSpace(txtVaros.Text) ||
                    string.IsNullOrWhiteSpace(txtUtca.Text))
                {
                    MessageBox.Show("Kérjük, töltse ki az összes kötelező mezőt!", "Hiányzó adatok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Combine `nev` and `cim`
                string nev = $"{txtCsaladNev.Text.Trim()} {txtKeresztNev.Text.Trim()}";
                string cim = $"{numIranyitoSz.Text.Trim()}, {txtVaros.Text.Trim()}, {txtUtca.Text.Trim()}";
                if (!string.IsNullOrWhiteSpace(txtOptional.Text))
                {
                    cim += $", {txtOptional.Text.Trim()}";
                }

                DatabaseHandler databaseHandler = new DatabaseHandler();

                if (IsEditMode) // Edit mode: update the record
                {
                    string updateQuery = "UPDATE ugyfelek SET nev = @nev, telefonszam = @telefonszam, cim = @cim, email = @email WHERE nev = @originalNev";
                    var parameters = new Dictionary<string, object>
                    {
                        { "@nev", nev },
                        { "@telefonszam", txtTel.Text.Trim() },
                        { "@cim", cim },
                        { "@email", txtEmail.Text.Trim() },
                        { "@originalNev", OriginalNev }
                    };
                    databaseHandler.Update(updateQuery, parameters);
                    MessageBox.Show("Ügyfél sikeresen módosítva!", "Siker", MessageBoxButtons.OK);
                }
                else // Add mode: insert a new record
                {
                    string insertQuery = "INSERT INTO ugyfelek (nev, telefonszam, cim, email) VALUES(@nev, @telefonszam, @cim, @email)";
                    var parameters = new Dictionary<string, object>
                    {
                        { "@nev", nev },
                        { "@telefonszam", txtTel.Text.Trim() },
                        { "@cim", cim },
                        { "@email", txtEmail.Text.Trim() }
                    };
                    databaseHandler.Insert(insertQuery, parameters);
                    MessageBox.Show("Ügyfél sikeresen hozzáadva!", "Siker", MessageBoxButtons.OK);
                }

                // Notify parent form and close
                this.DialogResult = DialogResult.OK;
                this.Close();
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
