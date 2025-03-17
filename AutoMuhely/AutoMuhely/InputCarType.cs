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
    public partial class InputCarType : Form
    {
        private bool IsEditMode { get; set; }
        private int OriginalId { get; set; }
        private string OriginalName { get; set; }
        public InputCarType(bool Edit=false, int Id=0, string Name="")
        {
            IsEditMode = Edit;
            OriginalName = Name;
            OriginalId = Id;
            InitializeComponent();
            LoadComboBox();
            if (IsEditMode)
            {
                label4.Text = "MÓDOSÍTÁSA";
                btnAdd.Text = "Módosítás";
                comboBox1.SelectedValue = OriginalId;
                txtModelName.Text = OriginalName;
            }
            else
            {
                btnAdd.Text = "Hozzáadás";
            }
        }
        DatabaseHandler databaseHandler = new DatabaseHandler();
        private void LoadComboBox()
        {
            try
            {
                string query = "SELECT * FROM marka";
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
                MessageBox.Show($"Hiba történt a márkák betöltésekor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtModelName.Text) ||
            comboBox1.SelectedValue == null)
                {
                    MessageBox.Show("Kérjük, töltse ki az összes mezőt!", "Hiányzó adatok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (IsEditMode)
                {
                    // Edit Mode: Update the record
                    string updateQuery = @"UPDATE tipus SET tipus = @modell, marka_id = @marka WHERE tipus_id=(SELECT tipus_id FROM tipus t WHERE t.tipus=@originalCim AND t.marka_id=@orignalId);";
                    var parameters = new Dictionary<string, object>
                    {
                        { "@modell", txtModelName.Text },
                        { "@marka", comboBox1.SelectedValue },
                        {"@originalCim", OriginalName },
                        {"@orignalId", OriginalId }
                    };

                    databaseHandler.Update(updateQuery, parameters);

                    MessageBox.Show("Modell sikeresen módosítva!", "Siker!", MessageBoxButtons.OK);
                }
                else
                {
                    // Add Mode: Insert a new record
                    string insertQuery = "INSERT INTO tipus (tipus, marka_id) VALUES(@modell, @marka)";
                    var parameters = new Dictionary<string, object>
                    {
                        { "@modell", txtModelName.Text },
                        { "@marka", comboBox1.SelectedValue }
                    };
                    databaseHandler.Insert(insertQuery, parameters);

                    MessageBox.Show("Modell sikeresen hozzáadva!", "Siker!", MessageBoxButtons.OK);
                }
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
