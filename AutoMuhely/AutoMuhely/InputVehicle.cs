using MySql.Data.Types;
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
    public partial class InputVehicle : Form
    {
        DatabaseHandler databaseHandler = new DatabaseHandler();

        // Properties to track mode and original values
        public bool IsEditMode { get; set; }
        public string OriginalLicensePlate { get; set; }
        public int OriginalTypeID { get; set; }
        public int OriginalErrorCodeID { get; set; }
        public int OriginalTemplateID { get; set; }
        public int OriginalYear { get; set; }
        public string OriginalEngineData { get; set; }
        public string OriginalChassisData { get; set; }
        public string OriginalPreviousRepairs { get; set; }

        public InputVehicle(string licensePlate = null, int? typeID = null, int? errorCodeID = null, int? templateID = null,
                           int? year = null, string engineData = null, string chassisData = null, string previousRepairs = null)
        {
            InitializeComponent();

            // Determine mode
            if (!string.IsNullOrEmpty(licensePlate))
            {
                IsEditMode = true;
                OriginalLicensePlate = licensePlate;
                OriginalTypeID = typeID ?? 0;
                OriginalErrorCodeID = errorCodeID ?? 0;
                OriginalTemplateID = templateID ?? 0;
                OriginalYear = year ?? DateTime.Now.Year;
                OriginalEngineData = engineData;
                OriginalChassisData = chassisData;
                OriginalPreviousRepairs = previousRepairs;

                // Populate fields
                txtRendszam.Text = licensePlate;
                nUpDDate.Value = OriginalYear;
                txtMotorAdat.Text = engineData;
                txtAlvazAdat.Text = chassisData;
                txtElozoJav.Text = previousRepairs;
                btnAdd.Text = "Módosítás"; // Update button text
            }
            else
            {
                IsEditMode = false;
                btnAdd.Text = "Hozzáadás"; // Default button text
            }

            // Load combo boxes
            LoadComboBox(comBoxTipus, "SELECT tipus_id, tipus FROM tipus");
            LoadComboBox(comBoxHibakod, "SELECT kod_id, kod FROM hibakodok", OriginalErrorCodeID);
            LoadComboBox(comBoxSablon, "SELECT sablon_id, nev FROM munkafolyamat_sablonok", OriginalTemplateID);
            if (IsEditMode)
            {
                comBoxTipus.SelectedValue = OriginalTypeID;
                comBoxHibakod.SelectedValue = OriginalErrorCodeID;
                comBoxSablon.SelectedValue = OriginalTemplateID;
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation with a single if statement
                if (string.IsNullOrWhiteSpace(txtRendszam.Text) ||
                    comBoxTipus.SelectedValue == null || (int)comBoxTipus.SelectedValue <= 0 ||
                    comBoxHibakod.SelectedValue == null || (int)comBoxHibakod.SelectedValue <= 0 ||
                    comBoxSablon.SelectedValue == null || (int)comBoxSablon.SelectedValue <= 0 ||
                    string.IsNullOrWhiteSpace(txtMotorAdat.Text) ||
                    string.IsNullOrWhiteSpace(txtAlvazAdat.Text) ||
                    string.IsNullOrWhiteSpace(txtElozoJav.Text) ||
                    nUpDDate.Value > DateTime.Now.Year || nUpDDate.Value < 1886) // Validate year range
                {
                    MessageBox.Show("Minden mezőt helyesen kell kitölteni!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Stop execution if validation fails
                }
                if (IsEditMode)
                {
                    // Edit Mode: Update the record
                    string updateQuery = "UPDATE jarmuvek SET tipus_id = @tipus_id, kod_id = @kod_id, sablon_id = @sablon_id, " +
                                         "gyartas_eve = @gyartas_eve, motor_adatok = @motor_adatok, alvaz_adatok = @alvaz_adatok, " +
                                         "elozo_javitasok = @elozo_javitasok WHERE rendszam = @originalRendszam";

                    var parameters = new Dictionary<string, object>
                    {
                        { "@tipus_id", comBoxTipus.SelectedValue },
                        { "@kod_id", comBoxHibakod.SelectedValue },
                        { "@sablon_id", comBoxSablon.SelectedValue },
                        { "@gyartas_eve", nUpDDate.Value },
                        { "@motor_adatok", txtMotorAdat.Text },
                        { "@alvaz_adatok", txtAlvazAdat.Text },
                        { "@elozo_javitasok", txtElozoJav.Text },
                        { "@originalRendszam", OriginalLicensePlate }
                    };

                    databaseHandler.Update(updateQuery, parameters);
                    MessageBox.Show("Jármű sikeresen módosítva!", "Siker!", MessageBoxButtons.OK);
                }
                else
                {
                    // Add Mode: Insert a new record
                    string insertQuery = "INSERT INTO jarmuvek (rendszam, tipus_id, kod_id, sablon_id, gyartas_eve, motor_adatok, alvaz_adatok, elozo_javitasok) " +
                                         "VALUES(@rendszam, @tipus_id, @kod_id, @sablon_id, @gyartas_eve, @motor_adatok, @alvaz_adatok, @elozo_javitasok)";

                    var parameters = new Dictionary<string, object>
                    {
                        { "@rendszam", txtRendszam.Text },
                        { "@tipus_id", comBoxTipus.SelectedValue },
                        { "@kod_id", comBoxHibakod.SelectedValue },
                        { "@sablon_id", comBoxSablon.SelectedValue },
                        { "@gyartas_eve", nUpDDate.Value },
                        { "@motor_adatok", txtMotorAdat.Text },
                        { "@alvaz_adatok", txtAlvazAdat.Text },
                        { "@elozo_javitasok", txtElozoJav.Text }
                    };

                    databaseHandler.Insert(insertQuery, parameters);
                    MessageBox.Show("Jármű sikeresen hozzáadva!", "Siker!", MessageBoxButtons.OK);
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

        private void NewVehicle_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            int currentYear = today.Year;
            //Max ez az év lehet a gyártási év
            nUpDDate.Maximum = currentYear;
        }

        private void LoadComboBox(System.Windows.Forms.ComboBox comboBox, string query, int? selectedID=null)
        {
            try
            {
                var (rows, _) = databaseHandler.Select(query);

                if (rows != null)
                {
                    var comboBoxList = new List<KeyValuePair<int, string>>();

                    foreach (var row in rows)
                    {
                        int id = Convert.ToInt32(row[0]);
                        string name = row[1].ToString();
                        comboBoxList.Add(new KeyValuePair<int, string>(id, name));
                    }
                    comboBox.DataSource = comboBoxList;
                    comboBox.DisplayMember = "Value"; // Display the name
                    comboBox.ValueMember = "Key";    // Use the ID as the value

                    // Select the current ID if in edit mode
                    if (IsEditMode && selectedID > 0)
                    {
                        comboBox.SelectedValue = selectedID;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a legördülő menü betöltésekor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
