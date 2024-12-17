using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AutoMuhely
{
    public partial class InputWorkFlow : Form
    {
        DatabaseHandler databaseHandler = new DatabaseHandler();

        // Properties to track mode and original values
        public bool IsEditMode { get; set; }
        public string OriginalNev { get; set; }
        public string OriginalLepesek { get; set; }
        public string OriginalBecsultIdo { get; set; }

        public InputWorkFlow(string nev = null, string lepesek = null, string becsultIdo = null)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(nev))
            {
                // Edit mode: Populate fields with existing data
                IsEditMode = true;
                OriginalNev = nev;
                OriginalLepesek = lepesek;
                OriginalBecsultIdo = becsultIdo;

                txtSablonNev.Text = nev;
                txtSablonleiras.Text = lepesek;
                numSablonIdo.Value = Convert.ToDecimal(becsultIdo.Substring(0, becsultIdo.Length - 4));

                btnAdd.Text = "Módosítás"; // Update button text
            }
            else
            {
                // Add mode
                IsEditMode = false;
                btnAdd.Text = "Hozzáadás"; // Default button text
            }
        }
        public event EventHandler MunkafolyamatHozzaadva;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSablonNev.Text) ||
            string.IsNullOrWhiteSpace(txtSablonleiras.Text) ||
            numSablonIdo.Value <= 0)
                {
                    MessageBox.Show("Kérjük, töltse ki az összes mezőt és adjon meg pozitív értékeket!", "Hiányzó vagy érvénytelen adatok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (IsEditMode)
                {
                    // Edit Mode: Update the record
                    string updateQuery = "UPDATE munkafolyamat_sablonok SET nev = @nev, lepesek = @lepesek, becsult_ido = @becsult_ido WHERE nev = @originalNev";
                    var parameters = new Dictionary<string, object>
                    {
                        { "@nev", txtSablonNev.Text },
                        { "@lepesek", txtSablonleiras.Text },
                        { "@becsult_ido", numSablonIdo.Value + " nap" },
                        { "@originalNev", OriginalNev }
                    };
                    databaseHandler.Update(updateQuery, parameters);

                    MessageBox.Show("Munkafolyamat sablon sikeresen módosítva!", "Siker!", MessageBoxButtons.OK);
                }
                else
                {
                    // Add Mode: Insert a new record
                    string insertQuery = "INSERT INTO munkafolyamat_sablonok (nev, lepesek, becsult_ido) VALUES(@nev, @lepesek, @becsult_ido)";
                    var parameters = new Dictionary<string, object>
                        {
                            { "@nev", txtSablonNev.Text },
                            { "@lepesek", txtSablonleiras.Text },
                            { "@becsult_ido", numSablonIdo.Value + " nap"}
                        };
                    databaseHandler.Insert(insertQuery, parameters);

                    MessageBox.Show("Munkafolyamat sablon hozzáadva!", "Siker!", MessageBoxButtons.OK);
                }
                MunkafolyamatHozzaadva?.Invoke(this, EventArgs.Empty);
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

        private void újMunkafolyamat_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsEditMode)
            {
                // Reset fields to their original values if the form is closed without saving
                txtSablonNev.Text = OriginalNev;
                txtSablonleiras.Text = OriginalLepesek;
                numSablonIdo.Value = Convert.ToDecimal(OriginalBecsultIdo?.Replace(" nap", ""));
            }
        }
    }
}