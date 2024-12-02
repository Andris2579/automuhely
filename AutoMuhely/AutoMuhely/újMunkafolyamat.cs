using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AutoMuhely
{
    public partial class újMunkafolyamat : Form
    {
        DatabaseHandler databaseHandler = new DatabaseHandler();

        // Properties to track mode and original values
        public bool IsEditMode { get; set; }
        public string OriginalNev { get; set; }
        public string OriginalLepesek { get; set; }
        public string OriginalBecsultIdo { get; set; }

        public újMunkafolyamat(string nev = null, string lepesek = null, string becsultIdo = null)
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

        public event EventHandler újMunkafolyamatHozzáadva;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new MySqlConnection(databaseHandler.ConnectionCommand))
                {
                    connection.Open();

                    if (IsEditMode)
                    {
                        // Modify existing record
                        string commandText = "UPDATE munkafolyamat_sablonok SET nev = @nev, lepesek = @lepesek, becsult_ido = @becsult_ido WHERE nev = @originalNev";

                        using (var command = new MySqlCommand(commandText, connection))
                        {
                            command.Parameters.AddWithValue("@nev", txtSablonNev.Text);
                            command.Parameters.AddWithValue("@lepesek", txtSablonleiras.Text);
                            command.Parameters.AddWithValue("@becsult_ido", numSablonIdo.Value + " nap");
                            command.Parameters.AddWithValue("@originalNev", OriginalNev);

                            var result = command.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("Munkafolyamat sablon módosítva!", "Siker!", MessageBoxButtons.OK);
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        // Add new record
                        string commandText = "INSERT INTO munkafolyamat_sablonok (nev, lepesek, becsult_ido) VALUES(@nev, @lepesek, @becsult_ido)";

                        using (var command = new MySqlCommand(commandText, connection))
                        {
                            command.Parameters.AddWithValue("@nev", txtSablonNev.Text);
                            command.Parameters.AddWithValue("@lepesek", txtSablonleiras.Text);
                            command.Parameters.AddWithValue("@becsult_ido", numSablonIdo.Value + " nap");

                            var result = command.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("Munkafolyamat sablon hozzáadva!", "Siker!", MessageBoxButtons.OK);
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