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
    public partial class InputBrands : Form
    {
        private bool IsEditMode { get; set; }
        private string OriginalBrandName { get; set; }

        public InputBrands(bool Edit = false, string BrandName="")
        {
            IsEditMode = Edit;
            OriginalBrandName = BrandName;
            InitializeComponent();
            txtBrand.Text = OriginalBrandName;
            if (IsEditMode)
            {
                label4.Text = "MÓDOSÍTÁSA";
                btnAdd.Text = "Módosítás";
            }
            else
            {
                label4.Text = "HOZZÁADÁSA";
                btnAdd.Text = "Hozzáadás";
            }
        }
        DatabaseHandler DatabaseHandler = new DatabaseHandler();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBrand.Text))
                {
                    MessageBox.Show("Kérjük, töltse ki az összes mezőt!", "Hiányzó adatok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (IsEditMode)  // If we're in Edit mode, update the existing record
                {

                    string sqlQuery = @"SELECT marka_id FROM marka WHERE marka_neve=@marka";
                    var param = new Dictionary<string, object>
                    {
                        { "@marka", OriginalBrandName }
                    };
                    int Id = DatabaseHandler.GetScalarValue(sqlQuery, param);

                    string updateQuery = "UPDATE marka SET marka_neve = @marka WHERE marka_id = @id";
                    var parameters = new Dictionary<string, object>
                    {
                        { "@marka", txtBrand.Text },
                        { "@id",  Id},
                    };

                    DatabaseHandler.Update(updateQuery, parameters);
                    MessageBox.Show("Márka sikeresen módosítva!", "Siker!", MessageBoxButtons.OK);

                }
                else  // If we're in Add mode, insert a new record
                {
                    string insertQuery = "INSERT INTO marka (marka_neve) VALUES(@marka)";
                    var parameters = new Dictionary<string, object>
                    {
                        { "@marka", txtBrand.Text }
                    };

                    DatabaseHandler.Insert(insertQuery, parameters);

                    MessageBox.Show("Márka sikeresen hozzáadva!", "Siker!", MessageBoxButtons.OK);

                }
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba: {ex.Message}", "Hiba!", MessageBoxButtons.OK);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}