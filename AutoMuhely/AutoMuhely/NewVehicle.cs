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
    public partial class NewVehicle : Form
    {
        DatabaseHandler databaseHandler = new DatabaseHandler();
        public NewVehicle()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

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
            LoadComboBox(comBoxTipus, "SELECT tipus_id, tipus FROM tipus");
            LoadComboBox(comBoxHibakod, "SELECT kod_id, kod FROM hibakodok; ");
            LoadComboBox(comBoxSablon, "SELECT sablon_id, nev FROM munkafolyamat_sablonok; ");
        }
        
        private void LoadComboBox(System.Windows.Forms.ComboBox comboBox, string query)
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
                        string nev = row[1].ToString();
                        comboBoxList.Add(new KeyValuePair<int, string>(id, nev));
                    }

                    comboBox.DataSource = comboBoxList;
                    comboBox.DisplayMember = "Value"; // Display the name
                    comboBox.ValueMember = "Key";    // Use the ID as the value
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a legördülő menü betöltésekor: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
