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
    public partial class NewOrder : Form
    {
        private int alkId {  get; set; }
        private int felhId { get; set; }
        private string alkNev { get; set; }
        public NewOrder(int Id, string alkatresz, int Id2)
        {
            alkId = Id;
            alkNev = alkatresz;
            felhId = Id2;
            InitializeComponent();
            txtComponent.Text = alkNev;
        }
        DatabaseHandler databaseHandler = new DatabaseHandler();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Add Mode: Insert a new record
                string insertQuery = "INSERT INTO rendelesek (felhasznalo_id, alkatresz_id, mennyiseg, statusz) VALUES (@FelhId, @AlkId, @mennyiseg, 'Kérvényezve');";
                var parameters = new Dictionary<string, object>
                    {
                        { "@FelhId", felhId},
                        { "@AlkId", alkId},
                        { "@mennyiseg", numQuantity.Value }
                    };

                databaseHandler.Insert(insertQuery, parameters);

                MessageBox.Show("Rendelés kérvényezve!", "Siker!", MessageBoxButtons.OK);
            
                this.Close();
        }
            catch (Exception)
            {
                MessageBox.Show($"Hiba történt", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
