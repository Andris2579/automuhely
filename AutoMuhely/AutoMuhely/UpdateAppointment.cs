using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoMuhely
{
    public partial class UpdateAppointment : Form
    {
        public int AppointmentId { get; set; }
        public bool IsStatusMode { get; set; }
        public UpdateAppointment(int appointmentId, bool StatusMode = false)
        {
            InitializeComponent();
            AppointmentId = appointmentId;
            IsStatusMode=StatusMode;
            
        }
        DatabaseHandler databaseHandler= new DatabaseHandler();
        

        private void UpdateAppointment_Load(object sender, EventArgs e)
        {
            if (IsStatusMode)
            {
                StatusPicker.Visible = true;
                DatePicker.Visible = false;
                TimePicker.Visible = false;
                LoadComboBox(StatusPicker);
            }
            else
            {
                StatusPicker.Visible = false;
                DatePicker.Visible = true;
                TimePicker.Visible = true;
            }
            DatePicker.Value = DateTime.Today;
            DatePicker.MinDate = DateTime.Today;
        }
        private void LoadComboBox(CustomComboBox comboBox)
        {
            try
            {
                var comboBoxList = new List<KeyValuePair<int, string>>
        {
            new KeyValuePair<int, string>(1, "Foglalt"),
            new KeyValuePair<int, string>(2, "Folyamatban"),
            new KeyValuePair<int, string>(1, "Befejezett"),
            new KeyValuePair<int, string>(2, "Lemondva")
        };
                comboBox.LoadItems<int>(comboBoxList);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a legördülő menü betöltésekor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsStatusMode) { 
                if (DatePicker == null || TimePicker == null) 
                {
                    MessageBox.Show("Minden mezőt helyesen kell kitölteni!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Stop execution if validation fails
                }
                else
                    {
                        string updateQuery = "UPDATE idopontfoglalasok SET idopont = @datum, allapot='Folyamatban' WHERE idopont_id = @idop_id;";

                        var parameters = new Dictionary<string, object>
                    {
                        { "@datum",  FormatDateTime()},
                        { "@idop_id", AppointmentId }
                    };

                        databaseHandler.Update(updateQuery, parameters);
                        MessageBox.Show("Időpont sikeresen hozzáadva!", "Siker!", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    string status = (StatusPicker.SelectedItem is KeyValuePair<int, string> selectedItem)
                        ? selectedItem.Value
                        : string.Empty;
                    if (status=="Befejezett")
                    {
                        if (HasNullColumns(AppointmentId))
                        {
                            DialogResult result = MessageBox.Show(
                            "Biztosan befejezettnek nyilvánítod a szerelést? A járműnek hiányzó adatai vannak.",
                            "Megerősítés",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning
                            );

                            if (result == DialogResult.No)
                            {
                                this.Close(); // Close the form if the user clicks "No"
                                return;
                            }
                            //if "befejezett" it will add the "csomag" to the "előző javítások" on the car
                            string query = "SELECT j.elozo_javitasok FROM idopontfoglalasok i JOIN jarmuvek j ON j.jarmu_id=i.jarmu_id WHERE i.idopont_id=@idop_id";
                            var paramBef = new Dictionary<string, object>
                    {
                        { "@idop_id", AppointmentId }
                    };
                            string previous = databaseHandler.LookUpOne(query, paramBef);

                            query = "SELECT s.nev FROM idopontfoglalasok i JOIN szervizcsomagok s ON s.csomag_id=i.csomag_id WHERE i.idopont_id=@idop_id";
                            var paramBef1 = new Dictionary<string, object>{ { "@idop_id", AppointmentId } };
                            string current = databaseHandler.LookUpOne(query, paramBef1);
                            previous = previous + "; \n" + current;

                            string updateBef = "UPDATE jarmuvek j SET j.elozo_javitasok = @elozo_jav WHERE j.jarmu_id=(SELECT j.jarmu_id FROM idopontfoglalasok i JOIN jarmuvek j ON j.jarmu_id= i.jarmu_id WHERE i.idopont_id=@idop_id);";
                            var parmaBef0 = new Dictionary<string, object>
                    {
                        { "@elozo_jav",  previous},
                        { "@idop_id", AppointmentId }
                    };
                            databaseHandler.Update(updateBef, parmaBef0);
                        }
                    }

                    string updateQuery = "UPDATE idopontfoglalasok SET allapot=@allapot WHERE idopont_id = @idop_id;";                   
                    var parameters = new Dictionary<string, object>
                    {
                        { "@allapot",  status},
                        { "@idop_id", AppointmentId }
                    };

                    databaseHandler.Update(updateQuery, parameters);
                    MessageBox.Show("Időpont sikeresen hozzáadva!", "Siker!", MessageBoxButtons.OK);
                }
                

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string FormatDateTime()
        {
            // Get selected date (only date part)
            DateTime selectedDate = DatePicker.Value.Date;

            // Parse entered time
            DateTime enteredTime;
            if (DateTime.TryParseExact(TimePicker.Text, "HH:mm", null, System.Globalization.DateTimeStyles.None, out enteredTime))
            {
                // Combine date and time
                DateTime fullDateTime = selectedDate.AddHours(enteredTime.Hour).AddMinutes(enteredTime.Minute);
                string formattedDateTime = fullDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                return formattedDateTime;
            }
            else
            {
                MessageBox.Show("Invalid time format! Please enter HH:mm (e.g., 14:30).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "invalid";
            }
        }
        public bool HasNullColumns(int idopId)
        {
            string query = @"SELECT rendszam AS Rendszám, t.tipus AS Típus, m.marka_neve AS Márka, h.kod AS Hibakód, mf.nev AS Sablon, gyartas_eve AS 'Gyártás éve', motor_adatok AS 'Motor adatok', alvaz_adatok AS 'Alváz adatok', elozo_javitasok AS 'Előző javítások' FROM jarmuvek j LEFT JOIN tipus t ON j.tipus_id = t.tipus_id LEFT JOIN hibakodok h ON h.kod_id = j.kod_id LEFT JOIN munkafolyamat_sablonok mf ON mf.sablon_id = j.sablon_id LEFT JOIN marka m ON t.marka_id = m.marka_id WHERE j.jarmu_id = (SELECT j.jarmu_id FROM jarmuvek j JOIN idopontfoglalasok i ON i.jarmu_id=j.jarmu_id WHERE i.idopont_id=@id);";

            var parameters = new Dictionary<string, object> { { "@id", idopId } };
            var (rows, columnNames) = databaseHandler.Select(query, parameters);

            if (rows == null || rows.Count == 0)
            {
                return false; // No data found, assuming no nulls
            }

            foreach (var row in rows)
            {
                if (row.Any(value => value == null || value.ToString() == "NULL"))
                {
                    return true; // Found at least one null value
                }
            }

            return false; // No null values found
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
