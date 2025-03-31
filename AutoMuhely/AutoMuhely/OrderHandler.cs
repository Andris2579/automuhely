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
    public partial class OrderHandler : Form
    {
        private int PartId;
        private int OrderId;
        private int PartCount;
        private string Part;
        public OrderHandler(int Id2, int Id, int count,string part)
        {
            this.PartId = Id2;
            this.OrderId = Id;
            this.PartCount = count;
            this.Part = part;
            InitializeComponent();
            txtModelName.Text = part;
            LoadComboBox(comboBox1);
        }
        DatabaseHandler databaseHandler = new DatabaseHandler();
        private void LoadComboBox(CustomComboBox comboBox)
        {
            try
            {
                var comboBoxList = new List<KeyValuePair<int, string>>
        {
            new KeyValuePair<int, string>(1, "Leadva"),
            new KeyValuePair<int, string>(2, "Elutasítva")
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
                string statusz = (comboBox1.SelectedItem is KeyValuePair<int, string> selectedItem) ? selectedItem.Value : string.Empty;
                string updateQuery = @"UPDATE rendelesek r SET r.statusz = @stat WHERE r.rendeles_id=@Id;";
                var parameters = new Dictionary<string, object>
                    {
                        { "@stat", statusz },
                        { "@Id", OrderId }
                    };

                databaseHandler.Update(updateQuery, parameters);
                if (statusz == "Leadva")
                {
                    updateQuery = @"UPDATE alkatreszek SET utanrendelesi_szint = @menny WHERE alkatresz_id=( SELECT alkatresz_id FROM alkatreszek a WHERE a.nev=@nev);";
                    var parameters2 = new Dictionary<string, object>
                    {
                        { "@nev", Part},
                        { "@menny", PartCount}
                    };
                    databaseHandler.Update(updateQuery, parameters2);
                }
               
                MessageBox.Show("Rednelés sikeresen módosítva!", "Siker!", MessageBoxButtons.OK);
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
