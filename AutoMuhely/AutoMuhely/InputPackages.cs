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
    
    public partial class InputPackages : Form
    {
        public int Package_Id { get; set; }
        public bool IsEditMode { get; set; }
        public string PackageName { get; set; }
        public string PackageDesc { get; set; }
        public int PackagePrice { get; set; }
        public InputPackages( int Id=0, string name=null, string desc=null, int price=0)
        {
            PackageName= name;
            PackageDesc= desc;
            PackagePrice = price;
            InitializeComponent();
            if (!string.IsNullOrEmpty(name)) 
            {
                IsEditMode = true;
                Package_Id = Id;
                txtPackage.Text = PackageName;
                txtPackageDesc.Text = PackageDesc;
                numUpDwnPrice.Value = PackagePrice;
                btnAdd.Text = "Módosítás"; // Update button text
            }
            else 
            {
                IsEditMode = false;
                btnAdd.Text = "Hozzáadás"; // Default button text
            }
        }
        DatabaseHandler databaseHandler = new DatabaseHandler();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPackage.Text) ||
            string.IsNullOrWhiteSpace(txtPackageDesc.Text) ||
            numUpDwnPrice.Value ==0)
                {
                    MessageBox.Show("Kérjük, töltse ki az összes mezőt!", "Hiányzó adatok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (IsEditMode)
                {
                    // Edit Mode: Update the record
                    string updateQuery = "UPDATE szervizcsomagok SET nev = @nev, leiras = @leiras, ar = @ar WHERE csomag_id=@id";
                    var parameters = new Dictionary<string, object>
                    {
                        { "@nev", txtPackage.Text },
                        { "@leiras", txtPackageDesc.Text },
                        { "@ar", numUpDwnPrice.Value },
                        { "@id", Package_Id }
                    };

                    databaseHandler.Update(updateQuery, parameters);

                    MessageBox.Show("Szervíz csomag sikeresen módosítva!", "Siker!", MessageBoxButtons.OK);
                }
                else
                {
                    // Add Mode: Insert a new record
                    string insertQuery = "INSERT INTO szervizcsomagok (nev, leiras, ar) VALUES(@nev, @leiras, @ar)";
                    var parameters = new Dictionary<string, object>
                    {
                        { "@nev", txtPackage.Text },
                        { "@leiras", txtPackageDesc.Text },
                        { "@ar", numUpDwnPrice.Value }
                    };

                    databaseHandler.Insert(insertQuery, parameters);

                    MessageBox.Show("Szervíz csomag sikeresen hozzáadva!", "Siker!", MessageBoxButtons.OK);
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
