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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AutoMuhely
{
    public partial class InputGuide : Form
    {
        public bool IsEditMode { get; set; }
        public string OriginalCim { get; set; }
        public string OriginalTartalom { get; set; }
        public int OriginalJarmuTipusID { get; set; }

        public InputGuide(string cim = null, string tartalom = null, int? jarmuTipusID = null)
        {
            InitializeComponent();
            comboBox1.DrawMode = DrawMode.OwnerDrawFixed; // Enable custom drawing
            // Determine mode
            if (!string.IsNullOrEmpty(cim))
            {
                IsEditMode = true;
                OriginalCim = cim;
                OriginalTartalom = tartalom;
                OriginalJarmuTipusID = jarmuTipusID ?? 0;

                // Populate fields
                txtUtmutatoNev.Text = cim;
                txtUtmutatoLeiras.Text = tartalom;

                btnAdd.Text = "Módosítás"; // Update button text
                label4.Text = "MÓDOSÍTÁSA";
            }
            else
            {
                IsEditMode = false;
                btnAdd.Text = "Hozzáadás"; // Default button text
                label4.Text = "HOZZÁADÁSA";
            }
            LoadJarmuTipusok();

            // If in edit mode, select the current jármű típus
            if (IsEditMode)
            {
                comboBox1.SelectedValue = OriginalJarmuTipusID;
            }

        }

        DatabaseHandler databaseHandler = new DatabaseHandler();

        public event EventHandler ÚtmutatóHozzáadva;

        private void LoadJarmuTipusok()
        {
            try
            {
                string query = "SELECT tipus_id, tipus FROM tipus";
                var (rows, _) = databaseHandler.Select(query);

                if (rows != null)
                {
                    var jarmuTipusok = new List<KeyValuePair<int, string>>();

                    foreach (var row in rows)
                    {
                        int id = Convert.ToInt32(row[0]);
                        string nev = row[1].ToString();
                        jarmuTipusok.Add(new KeyValuePair<int, string>(id, nev));
                    }

                    comboBox1.DataSource = jarmuTipusok;
                    comboBox1.DisplayMember = "Value"; // Display the name
                    comboBox1.ValueMember = "Key";    // Use the ID as the value
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a járműtípusok betöltésekor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUtmutatoNev.Text) ||
            string.IsNullOrWhiteSpace(txtUtmutatoLeiras.Text) ||
            comboBox1.SelectedValue == null)
                {
                    MessageBox.Show("Kérjük, töltse ki az összes mezőt!", "Hiányzó adatok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (IsEditMode)
                {
                    // Edit Mode: Update the record
                    string updateQuery = "UPDATE szerelesi_utmutatok SET cim = @cim, tartalom = @tartalom, jarmu_tipus = @jarmu_tipus WHERE cim = @originalCim";
                    var parameters = new Dictionary<string, object>
                    {
                        { "@cim", txtUtmutatoNev.Text },
                        { "@tartalom", txtUtmutatoLeiras.Text },
                        { "@jarmu_tipus", comboBox1.SelectedValue },
                        { "@originalCim", OriginalCim }
                    };

                    databaseHandler.Update(updateQuery, parameters);

                    MessageBox.Show("Útmutató sikeresen módosítva!", "Siker!", MessageBoxButtons.OK);
                }
                else
                {
                    // Add Mode: Insert a new record
                    string insertQuery = "INSERT INTO szerelesi_utmutatok (cim, tartalom, jarmu_tipus) VALUES(@cim, @tartalom, @jarmu_tipus)";
                    var parameters = new Dictionary<string, object>
                    {
                        { "@cim", txtUtmutatoNev.Text },
                        { "@tartalom", txtUtmutatoLeiras.Text },
                        { "@jarmu_tipus", comboBox1.SelectedValue }
                    };

                    databaseHandler.Insert(insertQuery, parameters);

                    MessageBox.Show("Útmutató sikeresen hozzáadva!", "Siker!", MessageBoxButtons.OK);
                }

                ÚtmutatóHozzáadva?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
    }
}
