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
    public partial class CustomerCars : Form
    {
        private bool IsEditMode {  get; set; }
        private string CustomerName {  get; set; }
        private string CustomerAddress { get; set; }
        public CustomerCars(string customer, string address)
        {
            CustomerName = customer;
            CustomerAddress = address;
            InitializeComponent();
            TxtCustomerName.Text = CustomerName;
            TxtCustomerName.ReadOnly = true;
            LoadLicensePlates();
        }
        DatabaseHandler databaseHandler = new DatabaseHandler();
        private void LoadLicensePlates()
        {
            try
            {
                string query = "SELECT j.jarmu_id, j.rendszam FROM jarmuvek j LEFT JOIN ugyfel_jarmuvek uj ON j.jarmu_id = uj.jarmu_id WHERE uj.jarmu_id IS NULL;";
                var (rows, _) = databaseHandler.Select(query);

                if (rows != null)
                {
                    var plates = new List<KeyValuePair<int, string>>();

                    foreach (var row in rows)
                    {
                        int id = Convert.ToInt32(row[0]);
                        string nev = row[1].ToString();
                        plates.Add(new KeyValuePair<int, string>(id, nev));
                    }

                    CmbLicensePlate.DataSource = plates;
                    CmbLicensePlate.DisplayMember = "Value"; // Display the name
                    CmbLicensePlate.ValueMember = "Key";    // Use the ID as the value
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
                string selectQuery = "SELECT u.ugyfel_id FROM ugyfelek u WHERE u.nev=@nev AND u.cim=@cim;";
                var selectParameters = new Dictionary<string, object>
                    {
                        { "@nev", CustomerName },
                        { "@cim", CustomerAddress}
                    };
                int CustomerId=databaseHandler.GetScalarValue(selectQuery, selectParameters);
                string insertQuery = "INSERT INTO ugyfel_jarmuvek (ugyfel_id, jarmu_id) VALUES(@ugyfel, @jarmu)";
                    var parameters = new Dictionary<string, object>
                    {
                        { "@ugyfel", CustomerId },
                        { "@jarmu" , CmbLicensePlate.SelectedValue}
                    };

                    databaseHandler.Insert(insertQuery, parameters);

                    MessageBox.Show("Útmutató sikeresen hozzáadva!", "Siker!", MessageBoxButtons.OK);

                this.DialogResult = DialogResult.OK; 
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
