using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace AutoMuhely
{
    public partial class Main_Form : Form
    {

        private string aktivMenu;
        static DataGridView table_DGV = new DataGridView();

        public string Username { get; set; }
        public string Role { get; set; }
        static DatabaseHandler databaseHandler = new DatabaseHandler();
        //Default sql queries
        static string ugyfelekSql = "SELECT nev AS Név, telefonszam AS Telefonszám, email AS 'E-mail' ,cim AS Cím FROM ugyfelek; ";
        static string alkatreszekSql = "SELECT nev AS Alkatrész, leiras AS Leírás, keszlet_mennyiseg AS Készlet, utanrendelesi_szint AS 'Utánrendelési szint' FROM alkatreszek";
        static string jarmuvekSql = "SELECT rendszam AS Rendszám, t.tipus AS Típus, m.marka_neve AS Márka, h.kod AS Hibakód, mf.nev AS Sablon, gyartas_eve AS 'Gyártás éve', motor_adatok AS 'Motor adatok', alvaz_adatok AS 'Alváz adatok', elozo_javitasok AS 'Előző javítások' FROM jarmuvek j LEFT JOIN tipus t ON j.tipus_id= t.tipus_id LEFT JOIN hibakodok h ON h.kod_id = j.kod_id LEFT JOIN munkafolyamat_sablonok mf ON mf.sablon_id = j.sablon_id LEFT JOIN marka m ON t.marka_id= m.marka_id;";
        static string hibakodSql = "SELECT kod AS Hibakód, leiras AS Leírás FROM hibakodok";
        static string munkafolySql = "SELECT nev AS 'Sablon neve', lepesek AS Lépések, becsult_ido AS 'Becsült idő' FROM munkafolyamat_sablonok";
        static string utmutatoSql = "SELECT s.cim AS 'Útmutató címe', s.tartalom AS Útmutató, m.marka_neve AS 'Autó márka', t.tipus AS 'Autó típusa' FROM szerelesi_utmutatok s JOIN tipus t ON s.jarmu_tipus=t.tipus_id JOIN marka m ON m.marka_id= t.marka_id";
        static string szervizSql = "SELECT s.nev AS Csomag, s.leiras AS Leírás, CONCAT(s.ar,' Ft') AS Ár FROM szervizcsomagok s;";
        static string rendelesekSql = "SELECT a.nev AS Alkatrész, r.mennyiseg AS Mennyiség, f.felhasznalonev AS Igénylő, r.statusz AS Státusz FROM rendelesek r JOIN felhasznalok f ON f.felhasznalo_id=r.felhasznalo_id JOIN alkatreszek a ON a.alkatresz_id=r.alkatresz_id WHERE r.statusz='Kérvényezve';";
        static string markakTipusSql = "SELECT t.tipus AS Modell, m.marka_neve AS Márka FROM tipus t RIGHT JOIN marka m ON m.marka_id=t.marka_id ORDER BY Márka;";


        public Main_Form()
        {
            InitializeComponent();
            //no paddig on tablelayoutpan
            foreach (Control control in this.tableLayoutPanel1.Controls)
            {
                control.Margin = new Padding(0);
            }
            customersPanel.PanelClicked += (sender, e) => LoadData("Ügyfelek", ugyfelekSql);
            partsPanel.PanelClicked += (sender, e) => LoadData("Alkatrészek", alkatreszekSql);
            repairsPanel.PanelClicked += (sender, e) => LoadSzerelesek();
            carsPanel.PanelClicked += (sender, e) => LoadData("Járművek", jarmuvekSql);
            logOutPan.PanelClicked += LogOutPan_Clicked;
            settingsPanel.PanelClicked += SettingsPanel_Clicked;
            brandsPanel.PanelClicked += (sender, e) => LoadData("Márkák", markakTipusSql);
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            LabelUser.Text = Username;

            // Trim the email if it's in email format
            if (Username.Contains("@"))
            {
                string[] emailParts = Username.Split('@');
                LabelUser.Text = emailParts[0];  // Get the part before '@'
            }

            // Adjust font size based on the length of the username
            AdjustLabelFontSize(LabelUser.Text);

            if (Role == "Adminisztrátor")
            {
                PicBoxRole.Image = AutoMuhely.Properties.Resources.admin;
                PicBoxRole.Location = new Point(22, 34);
                settingsPanel.Visible = true;
                brandsPanel.Visible = true;
            }
            else if (Role == "Szerelő")
            {
                PicBoxRole.Image = AutoMuhely.Properties.Resources.user;
                PicBoxRole.Location = new Point(22, 27);
                settingsPanel.Visible = false;
                brandsPanel.Visible = false;

            }

            foreach (Control control in searchButton.Controls)
            {
                control.Click += searchButton_Click; // Attach the same event to child controls
            }
        }

        private void AdjustLabelFontSize(string text)
        {
            if (text.Length <= 7)
            {
                LabelUser.Font = new Font("Open Sans Extrabold", 27.75F, FontStyle.Bold);
            }
            else if (text.Length <= 10)
            {
                LabelUser.Font = new Font("Open Sans Extrabold", 22F, FontStyle.Bold);
            }
            else if (text.Length <= 15)
            {
                LabelUser.Font = new Font("Open Sans Extrabold", 18F, FontStyle.Bold);
            }
            else if (text.Length <= 20)
            {
                LabelUser.Font = new Font("Open Sans Extrabold", 11F, FontStyle.Bold);
            }
            else
            {
                LabelUser.Font = new Font("Open Sans Extrabold", 10F, FontStyle.Bold);
            }
        }
        private void LoadData(string menu, string query)
        {
            aktivMenu = menu;
            panelTable.Controls.Clear();
            InitializeTable(query);
            panelButtons.Controls.Clear();
            searchBar.Text = "";
            if (aktivMenu == "Ügyfelek")
            {
                if (Role == "Adminisztrátor")
                {
                    GenerateHoverPanel("Új hozzáadása", new Point(0, 0), new Size(180, 63), UjHozzaadasa_Click);
                    GenerateHoverPanel("Módosítás", new Point(180, 0), new Size(140, 63), Modositas_Click);
                    GenerateHoverPanel("Ügyfél autói", new Point(180+140, 0), new Size(160, 63), Ugyfel_Autok);
                    GenerateHoverPanel("Jármű hozzáadása", new Point(180 + 140+160, 0), new Size(240, 63), Ugyfel_Jarmuvek);
                }
                else
                {
                    GenerateHoverPanel("Új hozzáadása", new Point(0, 0), new Size(180, 63), UjHozzaadasa_Click);
                    GenerateHoverPanel("Ügyfél autói", new Point(180, 0), new Size(160, 63), Ugyfel_Autok);
                    GenerateHoverPanel("Ügyfélhez csatolás", new Point(340, 0), new Size(240, 63), Ugyfel_Jarmuvek);
                }

            }
            else if (aktivMenu == "Alkatrészek")
            {
                if (Role == "Adminisztrátor")
                {
                    GenerateHoverPanel("Új hozzáadása", new Point(0, 0), new Size(180, 63), UjHozzaadasa_Click);
                    GenerateHoverPanel("Módosítás", new Point(180, 0), new Size(140, 63), Modositas_Click);
                    GenerateHoverPanel("Rendelések", new Point(320, 0), new Size(150, 63), Rendelesek_Click);
                    GenerateHoverPanel("Rendelés kérvényezése", new Point(470, 0), new Size(280, 63), RendelesKervenyezes_Click);
                }
                else
                {
                    GenerateHoverPanel("Rendeléseim", new Point(0, 0), new Size(180, 63), Rendelesek_Click);
                    GenerateHoverPanel("Rendelés kérvényezése", new Point(180, 0), new Size(280, 63), RendelesKervenyezes_Click);
                }

            }
            else if (aktivMenu == "Járművek")
            {
                GenerateHoverPanel("Új hozzáadása", new Point(0, 0), new Size(180, 63), UjHozzaadasa_Click);
                GenerateHoverPanel("Módosítás", new Point(180, 0), new Size(140, 63), Modositas_Click);
                
            }
            else if (aktivMenu == "Márkák")
            {
                if (Role == "Adminisztrátor")
                {
                    GenerateHoverPanel("Új márka", new Point(0, 0), new Size(120, 63), UjHozzaadasa_Click);
                    GenerateHoverPanel("Márka módosítása", new Point(120, 0), new Size(220, 63), Modositas_Click);
                    GenerateHoverPanel("Új típus", new Point(340, 0), new Size(110, 63), Ujtipus_Click);
                    GenerateHoverPanel("Típus módosítása", new Point(450, 0), new Size(210, 63), TipusModositas_Click);
                }
            }
        }


        private void UjHozzaadasa_Click(object sender, EventArgs e)
        {
            if (aktivMenu == "Ügyfelek")
            {
                InputCustomers newCustomer = new InputCustomers();
                newCustomer.ShowDialog();
                LoadData("Ügyfelek", ugyfelekSql);
            }
            else if (aktivMenu == "Alkatrészek")
            {
                InputParts newPart = new InputParts();
                newPart.ShowDialog();
                LoadData("Alkatrészek", alkatreszekSql);
            }
            else if (aktivMenu == "Járművek")
            {
                InputVehicle newVehicle = new InputVehicle();
                newVehicle.ShowDialog();
                LoadData("Járművek", jarmuvekSql);
            }
            else if (aktivMenu == "Márkák")
            {
                InputBrands newBrands = new InputBrands();
                newBrands.ShowDialog();
                LoadData("Márkák", markakTipusSql);
            }
        }
        private void Modositas_Click(object sender, EventArgs e)
        {
            if (aktivMenu == "Ügyfelek")
            {
                if (table_DGV.SelectedRows.Count == 0 || table_DGV.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Kérlek válassz egy ügyfelet a módosításhoz!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DataGridViewRow selectedRow = table_DGV.SelectedRows[0];

                    // Get the data from the selected row
                    string nev = selectedRow.Cells["Név"].Value?.ToString();
                    string telefon = selectedRow.Cells["Telefonszám"].Value?.ToString();
                    string email = selectedRow.Cells["E-mail"].Value?.ToString();
                    string cim = selectedRow.Cells["Cím"].Value?.ToString();

                    // Pass data to alkatrészInputs for editing
                    InputCustomers inputCustomers = new InputCustomers(nev, telefon, cim, email);
                    inputCustomers.ShowDialog();
                }
                InitializeTable(ugyfelekSql);
            }
            else if (aktivMenu == "Alkatrészek")
            {
                if (table_DGV.SelectedRows.Count == 0 || table_DGV.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Kérlek válassz egy alkatrészt a módosításhoz!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DataGridViewRow selectedRow = table_DGV.SelectedRows[0];

                    // Get the data from the selected row
                    string alkatrész = selectedRow.Cells["Alkatrész"].Value?.ToString();
                    string leiras = selectedRow.Cells["Leírás"].Value?.ToString();
                    decimal keszlet = Convert.ToDecimal(selectedRow.Cells["Készlet"].Value);
                    decimal utanrendeles = Convert.ToDecimal(selectedRow.Cells["Utánrendelési szint"].Value);

                    // Pass data to alkatrészInputs for editing
                    InputParts aInput = new InputParts(alkatrész, leiras, keszlet, utanrendeles);
                    aInput.ShowDialog();
                }
                InitializeTable(alkatreszekSql);
            }
            else if (aktivMenu == "Járművek")
            {
                if (table_DGV.SelectedRows.Count == 0 || table_DGV.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Kérlek válassz egy járművet a módosításhoz!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DataGridViewRow selectedRow = table_DGV.SelectedRows[0];

                    // Extract data from the selected row
                    string rendszam = selectedRow.Cells["Rendszám"].Value?.ToString();
                    string tipus = selectedRow.Cells["Típus"].Value?.ToString();
                    string kod = selectedRow.Cells["Hibakód"].Value?.ToString();
                    string sablon = selectedRow.Cells["Sablon"].Value?.ToString();
                    int gyartasEve = Convert.ToInt32(selectedRow.Cells["Gyártás éve"].Value);
                    string motorAdatok = selectedRow.Cells["Motor adatok"].Value?.ToString();
                    string alvazAdatok = selectedRow.Cells["Alváz adatok"].Value?.ToString();
                    string elozoJavitasok = selectedRow.Cells["Előző javítások"].Value?.ToString();
                    string sqlQuery = "SELECT tipus_id FROM tipus WHERE tipus = @tipus";
                    var parameters = new Dictionary<string, object>
                {
                    { "@tipus", tipus }
                };
                    int tipusID = databaseHandler.LookupID(sqlQuery, parameters);
                    sqlQuery = "SELECT kod_id FROM hibakodok WHERE kod=@kod";
                    parameters = new Dictionary<string, object>
                {
                    { "@kod", kod }
                };
                    int kodID = databaseHandler.LookupID(sqlQuery, parameters);
                    sqlQuery = "SELECT sablon_id FROM munkafolyamat_sablonok WHERE nev=@nev";
                    parameters = new Dictionary<string, object>
                {
                    { "@nev", sablon }
                };
                    int sablonID = databaseHandler.LookupID(sqlQuery, parameters);

                    // Open the NewVehicle form in edit mode
                    InputVehicle vehicleForm = new InputVehicle(
                        rendszam,
                        tipusID,
                        kodID,
                        sablonID,
                        gyartasEve,
                        motorAdatok,
                        alvazAdatok,
                        elozoJavitasok
                    );

                    vehicleForm.ShowDialog();
                }
                InitializeTable(jarmuvekSql);
            }
            else if (aktivMenu == "Márkák")
            {
                if (table_DGV.SelectedRows.Count == 0 || table_DGV.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Kérlek válassz egy márkát a módosításhoz!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DataGridViewRow selectedRow = table_DGV.SelectedRows[0];

                    // Get the data from the selected row
                    string marka = selectedRow.Cells["Márka"].Value?.ToString();

                    // Pass data to alkatrészInputs for editing
                    InputBrands updateBrands = new InputBrands(true, marka);
                    updateBrands.ShowDialog();
                }
                InitializeTable(markakTipusSql);
            }
        }
        private void Ujtipus_Click(object sender, EventArgs e)
        {
            InputCarType newType = new InputCarType();
            newType.ShowDialog();
            LoadData("Márkák", markakTipusSql);
        }
        private void TipusModositas_Click(object sender, EventArgs e)
        {
            if (table_DGV.SelectedRows.Count == 0 || table_DGV.SelectedRows.Count > 1)
            {
                MessageBox.Show("Kérlek válassz egy Modellt a módosításhoz!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DataGridViewRow selectedRow = table_DGV.SelectedRows[0];

                // Get the data from the selected row
                string modell = selectedRow.Cells["Modell"].Value?.ToString();
                string marka = selectedRow.Cells["Márka"].Value?.ToString();
                var parameters = new Dictionary<string, object>
                {
                    {"@marka", marka }
                };
                string sqlQuery = @"SELECT marka_id FROM marka WHERE marka_neve=@marka;";
                int Id=databaseHandler.GetScalarValue(sqlQuery, parameters);
                // Pass data to alkatrészInputs for editing
                InputCarType updateType = new InputCarType(true,Id,modell);
                updateType.ShowDialog();
            }
            InitializeTable(markakTipusSql);
        }
        private void Alkatreszek_Click(object sender, EventArgs e)
        {
            LoadData("Alkatrészek", alkatreszekSql);
        }
        private void Rendelesek_Click(object sender, EventArgs e)
        {
            panelTable.Controls.Clear();
            aktivMenu = "Rendelesek";
            if (Role=="Szerelő") 
            {
                string sqlQuery = @"SELECT a.nev AS Alkatrész, r.mennyiseg AS Mennyiség, f.felhasznalonev AS Igénylő, r.statusz AS Státusz FROM rendelesek r JOIN felhasznalok f ON f.felhasznalo_id=r.felhasznalo_id JOIN alkatreszek a ON a.alkatresz_id=r.alkatresz_id WHERE f.felhasznalonev=@nev;";
                var parameters = new Dictionary<string, object>
                {
                    {"@nev", Username }
                };
                InitializeTable(sqlQuery,parameters); 
            }
            else { InitializeTable(rendelesekSql); }
            
            panelButtons.Controls.Clear();
            if (Role == "Adminisztrátor")
            {
                GenerateHoverPanel("Kezelés", new Point(0, 0), new Size(120, 63), RendelesKezeles_Click);
                GenerateHoverPanel("Vissza", new Point(120, 0), new Size(140, 63), Alkatreszek_Click);
            }
            else
            {
                GenerateHoverPanel("Vissza", new Point(0, 0), new Size(140, 63), Alkatreszek_Click);
            }

        }
        private void RendelesKervenyezes_Click(object sender, EventArgs e)
        {
            if (table_DGV.SelectedRows.Count == 0 || table_DGV.SelectedRows.Count > 1)
            {
                MessageBox.Show("Kérlek válassz egy alkatrészt a rendelés leadásához!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DataGridViewRow selectedRow = table_DGV.SelectedRows[0];

                // Get the data from the selected row
                string alkatresz = selectedRow.Cells["Alkatrész"].Value?.ToString();
                string leiras = selectedRow.Cells["Leírás"].Value?.ToString();
                int keszlet = Convert.ToInt32(selectedRow.Cells["Készlet"].Value?.ToString());
                int utanrend = Convert.ToInt32(selectedRow.Cells["Utánrendelési szint"].Value?.ToString());
                var parameters = new Dictionary<string, object>
                {
                    { "@nev", alkatresz },
                    { "@leiras", leiras },
                    { "@keszlet", keszlet},
                    { "@utanrend", utanrend}
                };
                string sqlQuery = @"SELECT alkatresz_id FROM alkatreszek a WHERE nev=@nev AND leiras= @leiras AND keszlet_mennyiseg = @keszlet AND utanrendelesi_szint = @utanrend;";
                int Id = databaseHandler.GetScalarValue(sqlQuery, parameters);
                
                var param= new Dictionary<string, object>
                {
                    { "@nev", Username }
                };
                sqlQuery = @"SELECT felhasznalo_id FROM felhasznalok WHERE felhasznalonev=@nev;";
                int Id2 = databaseHandler.GetScalarValue(sqlQuery, param);
                NewOrder rendelesForm = new NewOrder(Id, alkatresz, Id2);
                rendelesForm.ShowDialog();
                InitializeTable(alkatreszekSql);
            }
        }
        private void RendelesKezeles_Click(object sender, EventArgs e)
        {
            if (table_DGV.SelectedRows.Count == 0 || table_DGV.SelectedRows.Count > 1)
            {
                MessageBox.Show("Kérlek válassz egy alkatrészt a rendelés leadásához!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DataGridViewRow selectedRow = table_DGV.SelectedRows[0];

                // Get the data from the selected row
                string alkatresz = selectedRow.Cells["Alkatrész"].Value?.ToString();
                int menny = Convert.ToInt32(selectedRow.Cells["Mennyiség"].Value?.ToString());
                string igenylo = selectedRow.Cells["Igénylő"].Value?.ToString();
                string statusz = selectedRow.Cells["Státusz"].Value?.ToString();
                var parameters = new Dictionary<string, object>
                {
                    { "@nev", alkatresz },
                    { "@menny",  menny},
                    { "@igeny", igenylo},
                    { "@stat", statusz}
                };
                string sqlQuery = @"SELECT rendeles_id FROM rendelesek r WHERE alkatresz_id=(SELECT a.alkatresz_id FROM alkatreszek a WHERE a.nev=@nev) AND r.mennyiseg=@menny AND felhasznalo_id=(SELECT f.felhasznalo_id FROM felhasznalok f WHERE f.felhasznalonev=@igeny) AND statusz=@stat;";
                
                int Id = databaseHandler.GetScalarValue(sqlQuery, parameters);
                string sqlQuery2 = @"SELECT r.alkatresz_id FROM rendelesek r WHERE alkatresz_id=(SELECT a.alkatresz_id FROM alkatreszek a WHERE a.nev=@nev) AND r.mennyiseg=@menny AND felhasznalo_id=(SELECT f.felhasznalo_id FROM felhasznalok f WHERE f.felhasznalonev=@igeny) AND statusz=@stat;";
                int Id2=databaseHandler.GetScalarValue(sqlQuery2, parameters);
                OrderHandler handleOrder = new OrderHandler(Id2,Id, menny,alkatresz);
                handleOrder.ShowDialog();
                InitializeTable(rendelesekSql);
            }
        }

        private void Ugyfel_Jarmuvek(object sender, EventArgs e)
        {
            if (table_DGV.SelectedRows.Count == 0 || table_DGV.SelectedRows.Count > 1)
            {
                MessageBox.Show("Kérlek válassz egy ügyfelet az autó hozzácsatolásohoz!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DataGridViewRow selectedRow = table_DGV.SelectedRows[0];

                // Get the data from the selected row
                string Name = selectedRow.Cells["Név"].Value?.ToString();
                string Address = selectedRow.Cells["Cím"].Value?.ToString();
                CustomerCars customerCars = new CustomerCars(Name, Address);

                // Show the dialog and capture the result
                DialogResult result = customerCars.ShowDialog();

                // Check the DialogResult
                if (result == DialogResult.No)
                {
                    InitializeTable(ugyfelekSql); // Reset to the original customers table
                }
                else // Proceed with the original logic if not DialogResult.No
                {
                    aktivMenu = "Járművek";
                    if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Address))
                    {
                        
                        // Modify the SQL query to join tables
                        string query = jarmuvekSql.Substring(0, jarmuvekSql.Length - 1) +
                            @" JOIN ugyfel_jarmuvek uj ON j.jarmu_id = uj.jarmu_id 
                       JOIN ugyfelek u ON uj.ugyfel_id = u.ugyfel_id 
                       WHERE u.nev = @name AND u.cim = @address;";

                        // Load the car data into the table
                        var parameters = new Dictionary<string, object>
                {
                    { "@name", Name },
                    { "@address", Address }
                };
                        InitializeTable(query, parameters);
                        panelButtons.Controls.Clear();
                        GenerateHoverPanel("Vissza", new Point(0, 0), new Size(140, 63), Ugyfelek_Click);
                    }
                    else
                    {
                        MessageBox.Show("Nem található az ügyfél!", "Hiba");
                    }
                }
            }
        }
        private void InitializeTable(string query, Dictionary<string, object> parameters = null)
        {
            CustomizeTable(table_DGV);
            table_DGV.Rows.Clear();
            table_DGV.Columns.Clear();
            table_DGV.CellDoubleClick -= Table_CellDoubleClick;
            table_DGV.CellDoubleClick += Table_CellDoubleClick;
            var (results, columnNames) = databaseHandler.Select(query, parameters);
            if (results != null && columnNames != null)
            {
                foreach (var columnName in columnNames)
                {
                    table_DGV.Columns.Add(columnName, columnName);
                }
                foreach (var row in results)
                {
                    table_DGV.Rows.Add(row.ToArray());
                }
            }
            panelTable.Controls.Add(table_DGV);
        }
        private void Ugyfel_Autok(object sender, EventArgs e)
        {
            if (table_DGV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Válasszon ki egy ügyfelet!", "Hiba");
                return;
            }

            DataGridViewRow selectedRow = table_DGV.SelectedRows[0]; // Get the first selected row

            // Extract all columns to uniquely identify the customer
            string name = selectedRow.Cells["Név"]?.Value?.ToString();
            string address = selectedRow.Cells["Cím"]?.Value?.ToString();
            aktivMenu = "Járművek";
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(address))
            {
                // Modify the SQL query to join tables
                string query = jarmuvekSql.Substring(0, jarmuvekSql.Length - 1) +
                    @" JOIN ugyfel_jarmuvek uj ON j.jarmu_id = uj.jarmu_id 
               JOIN ugyfelek u ON uj.ugyfel_id = u.ugyfel_id 
               WHERE u.nev = @name AND u.cim = @address;";

                // Load the car data into the table
                var parameters = new Dictionary<string, object>
        {
            { "@name", name },
            { "@address", address }
        };
                InitializeTable(query, parameters);
                panelButtons.Controls.Clear();
                GenerateHoverPanel("Vissza", new Point(0, 0), new Size(140, 63), Ugyfelek_Click);
            }
            else
            {
                MessageBox.Show("Nem található az ügyfél!", "Hiba");
            }
        }
        private void GenerateHoverPanel(string labelText, Point location, Size size,  EventHandler clickEvent, Image icon=null)
        {
            HoverPanel hoverPanel = new HoverPanel
            {
                Anchor= AnchorStyles.Left,
                Location = location,
                Size = size, 
                OriginalColor = Color.FromArgb(24, 30, 54),
                HoverColor = Color.FromArgb(46, 51, 73),
                BackColor = Color.FromArgb(24, 30, 54)
            };

            // Add label to the panel
            Label label = new Label
            {
                Text = labelText,
                Font = new Font("Open Sans SemiBold", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(3, 135, 246),
                Location = new Point(10, 15), // Padding inside the panel
                AutoSize = true
            };
            hoverPanel.Controls.Add(label);

            // Add optional icon to the panel
            if (icon != null)
            {
                PictureBox pictureBox = new PictureBox
                {
                    Image = icon,
                    Size = new Size(50, 50),
                    Location = new Point(186, 6),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                hoverPanel.Controls.Add(pictureBox);
            }

            // Attach click event to the panel
            hoverPanel.PanelClicked += clickEvent;
            panelButtons.Controls.Add(hoverPanel);
        }

        private void LoadSzerelesek()
        {
            aktivMenu = "Szerelések";
            panelButtons.Controls.Clear();
            panelTable.Controls.Clear();
            searchBar.Text = "";
            InitSzerelesek();
            GenerateHoverPanel("Szerelési Útmutatók", new Point(0, 0), new Size(246, 63), SzerelesiUtmutatok_Click);
            GenerateHoverPanel("Hibakódok", new Point(246, 0), new Size(140, 63), Hibakodok_Click);
            GenerateHoverPanel("Munkafolyamat Sablonok", new Point(140+246, 0), new Size(300, 63), MunkafolyamatSablonok_Click);
            GenerateHoverPanel("Szervizcsomagok", new Point(140 + 246+300, 0), new Size(221, 63), SzervizCsomagok_Click);
        }

        private int currentPage = 1;
        private int totalPages = 1;
        private const int pageSize = 4;
        private void InitSzerelesek()
        {
            string query = $"SELECT COUNT(*) FROM idopontfoglalasok i WHERE i.allapot!='Befejezett' ";
            int totalRecords = databaseHandler.GetScalarValue(query);
            totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            LoadAppointments();
        }
        private void LoadAppointments()
        {
            var results = GetAppointmentData(currentPage);
            DisplayAppointmentCards(results);
            AddPagingButtons();
            ResizeAppointmentCards();
        }
        public static List<List<object>> GetAppointmentData( int currentPage)
        {
            int offset = (currentPage - 1) * pageSize;
            string query = $"SELECT u.nev, j.rendszam, t.tipus, m.marka_neve, sz.nev, i.idopont, i.allapot FROM idopontfoglalasok i LEFT JOIN jarmuvek j ON j.jarmu_id = i.jarmu_id LEFT JOIN tipus t ON t.tipus_id = j.tipus_id LEFT JOIN marka m ON m.marka_id = t.marka_id LEFT JOIN ugyfel_jarmuvek uj ON j.jarmu_id = uj.jarmu_id LEFT JOIN ugyfelek u ON u.ugyfel_id = uj.ugyfel_id LEFT JOIN szervizcsomagok sz ON sz.csomag_id = i.csomag_id WHERE i.allapot!='Befejezett' ORDER BY i.idopont LIMIT {pageSize} OFFSET {offset}"; 
            // Execute the query and get results
            var (results, _) = databaseHandler.Select(query);
            return results;
        }
        private void AddPagingButtons()
        {
            // Remove only pagination buttons and label
            foreach (Control control in panelTable.Controls.OfType<Button>().ToList())
            {
                panelTable.Controls.Remove(control);
            }
            foreach (Control control in panelTable.Controls.OfType<Label>().ToList())
            {
               panelTable.Controls.Remove(control);
                
            }
            Button btnPrev = new Button
            {
                Text = "◀",
                Location = new Point(panelTable.Width - 140, panelTable.Height - 35),
                Size = new Size(30, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.FromArgb(245, 245, 241),
                BackColor = Color.FromArgb(24, 30, 54),
                Enabled = currentPage > 1
            }; 
            btnPrev.FlatAppearance.BorderColor = Color.FromArgb(91, 92, 95);

            Label lblPageNumber = new Label
            {
                Text = currentPage.ToString(),
                Location = new Point(panelTable.Width - 105, panelTable.Height - 35),
                Size = new Size(30, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };

            Button btnNext = new Button
            {
                Text = "▶",
                Location = new Point(panelTable.Width - 70, panelTable.Height - 35),
                Size = new Size(30, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                BackColor = Color.FromArgb(24, 30, 54),
                ForeColor = Color.FromArgb(245, 245, 241),
                Enabled = currentPage < totalPages
            }; 
            btnNext.FlatAppearance.BorderColor = Color.FromArgb(91, 92, 95);

            btnPrev.Click += (s, e) => { currentPage--; LoadAppointments(); };
            btnNext.Click += (s, e) => { currentPage++; LoadAppointments(); };

            panelTable.Controls.Add(btnPrev);
            panelTable.Controls.Add(lblPageNumber);
            panelTable.Controls.Add(btnNext);
        }

        private void DisplayAppointmentCards(List<List<object>> results)
{
    panelTable.Controls.Clear();

    int panelWidth = panelTable.Width;
    int panelHeight = panelTable.Height;
    int cardWidth = panelWidth / 2 - 40;
    int cardHeight = panelHeight / 2 - 40;
    int spacingX = 20;
    int spacingY = 20;

    for (int i = 0; i < results.Count; i++)
    {
        int row = i / 2;
        int col = i % 2;

        // Create the card panel
        Panel card = new Panel
        {
            Size = new Size(cardWidth, cardHeight),
            Location = new Point(spacingX + col * (cardWidth + spacingX), spacingY + row * (cardHeight + spacingY)),
            BackColor = Color.FromArgb(24, 30, 54),
            BorderStyle = BorderStyle.FixedSingle,
            ForeColor = Color.FromArgb(245, 245, 241)
        };

        // Create the TextBox for card content
        TextBox txtCardContent = new TextBox
        {
            Multiline = true,
            ReadOnly = true,
            BackColor = card.BackColor,
            ForeColor = Color.White,
            BorderStyle = BorderStyle.None,
            Location = new Point(10, 10),
            Size = new Size(cardWidth - 20, cardHeight - 20), 
            Font = new Font("Segoe UI", Math.Max(13, panelWidth / 60), FontStyle.Regular),
            Text = GetCardText(results[i]), 
            HideSelection = true,
            Cursor = Cursors.Default
        };

        // Add the TextBox to the card
        card.Controls.Add(txtCardContent);

        // Determine which button to add based on appointmentText
        string appointmentText = GetAppointmentText(results[i]);

                if (appointmentText == "Visszajelzésre vár")
                {
                    // Button to set a date-time for the appointment
                    Button btnSetDateTime = new Button
                    {
                        Text = "Időpont módosítás",
                        Location = new Point(card.Width - 110, card.Height - 40),
                        Size = new Size(100, 30),
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", 10, FontStyle.Regular),
                        ForeColor = Color.White,
                        BackColor = Color.FromArgb(44, 84, 94),
                        Tag = i // Tároljuk a teljes sort az eseménykezelő számára
                    };
                    btnSetDateTime.FlatAppearance.BorderColor = Color.FromArgb(91, 92, 95);

                    // Click event for adding date-time
                    btnSetDateTime.Click += BtnSetDateTime_Click;

                    card.Controls.Add(btnSetDateTime);
                    btnSetDateTime.BringToFront();
                }
                else
                {
                    // Button to change the status of the appointment
                    Button btnChangeStatus = new Button
                    {
                        Text = "Állapot módosítás",
                        Location = new Point(card.Width - 140, card.Height - 40),
                        Size = new Size(120, 30),
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", 10, FontStyle.Regular),
                        ForeColor = Color.White,
                        BackColor = Color.FromArgb(84, 24, 54),
                        Tag = i // Tároljuk a teljes sort az eseménykezelő számára
                    };
                    btnChangeStatus.FlatAppearance.BorderColor = Color.FromArgb(91, 92, 95);

                    // Click event for changing status
                    btnChangeStatus.Click += BtnChangeStatus_Click;

                    card.Controls.Add(btnChangeStatus);
                    btnChangeStatus.BringToFront();
                }
                // Add the card to the panel
                panelTable.Controls.Add(card);
    }
}
       


        private string GetAppointmentText(List<object> result)
        {
            DateTime appointmentTime;
            var dateString = result[5]?.ToString();
            return string.IsNullOrEmpty(dateString) || dateString == "0000-00-00 00:00:00"
                ? "Visszajelzésre vár"
                : DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out appointmentTime)
                ? appointmentTime.ToString("yyyy. MM. dd. HH:mm")
                : "Visszajelzésre vár";
        }

        private void BtnSetDateTime_Click(object sender, EventArgs e)
        {
            var results = GetAppointmentData(currentPage);
            if (sender is Button btn && btn.Tag is int index)
            {
                // Az eredeti rekordot az index alapján kinyerjük
                var result = results[index];
                var parameters = new Dictionary<string, object>
                {
                    { "@rendszam", result[1]},
                    { "@szerviz", result[4]}
                };
                string sqlQuery = "SELECT idopont_id FROM idopontfoglalasok i JOIN szervizcsomagok s ON s.csomag_id=i.csomag_id JOIN jarmuvek j ON j.jarmu_id= i.jarmu_id WHERE s.nev=@szerviz AND j.rendszam=@rendszam;";
                int Id= databaseHandler.GetScalarValue(sqlQuery, parameters);
                UpdateAppointment setDateTime= new UpdateAppointment(Id);
                setDateTime.ShowDialog();
                LoadAppointments();
            }
            else
            {
                MessageBox.Show("Nem található adat a gombhoz!");
            }
        }

        private void BtnChangeStatus_Click(object sender, EventArgs e)
        {
            var results = GetAppointmentData(currentPage);
            if (sender is Button btn && btn.Tag is int index)
            {
                // Az eredeti rekordot az index alapján kinyerjük
                var result = results[index];
                var parameters = new Dictionary<string, object>
                {
                    { "@rendszam", result[1]},
                    { "@szerviz", result[4]}
                };
                string sqlQuery = "SELECT idopont_id FROM idopontfoglalasok i JOIN szervizcsomagok s ON s.csomag_id=i.csomag_id JOIN jarmuvek j ON j.jarmu_id= i.jarmu_id WHERE s.nev=@szerviz AND j.rendszam=@rendszam;";
                int Id = databaseHandler.GetScalarValue(sqlQuery, parameters);
                UpdateAppointment setStatus = new UpdateAppointment(Id, true);
                setStatus.ShowDialog();
                LoadAppointments();
            }
            else
            {
                MessageBox.Show("Nem található adat a gombhoz!");
            }
        }




        private string GetCardText(List<object> result)
        {
            // Combine all the text for the card in one string
            DateTime appointmentTime;
            var dateString = result[5]?.ToString();
            string appointmentText = string.IsNullOrEmpty(dateString) || dateString == "0000-00-00 00:00:00" ? "Visszajelzésre vár" :
                DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out appointmentTime) ?
                appointmentTime.ToString("yyyy. MM. dd. HH:mm") : "Visszajelzésre vár";

            return $"Ügyfél: {result[0]}\r\n" +
                   $"Rendszám: {result[1]}\r\n" +
                   $"Típus: {result[2]}\r\n" +
                   $"Márka: {result[3]}\r\n" +
                   $"Szervizcsomag: {result[4]}\r\n" +
                   $"Időpont: {appointmentText}\r\n" +
                   $"Állapot: {result[6]}";
        }

        private void ResizeAppointmentCards()
        {
            int panelWidth = panelTable.Width;
            int panelHeight = panelTable.Height;
            int cardWidth = panelWidth / 2 - 40;
            int cardHeight = panelHeight / 2 - 40;
            int spacingX = 20;
            int spacingY = 20;

            // Adjust the font size based on the panel width
            Font newFont = new Font("Segoe UI", Math.Max(13, panelWidth / 80), FontStyle.Regular);

            foreach (Control control in panelTable.Controls)
            {
                if (control is Button btnPrev && btnPrev.Text == "◀")
                {
                    btnPrev.Location = new Point(panelWidth - 140, panelHeight - 35);
                }
                else if (control is Label lblPageNumber)
                {
                    lblPageNumber.Location = new Point(panelWidth - 105, panelHeight - 35);
                }
                else if (control is Button btnNext && btnNext.Text == "▶")
                {
                    btnNext.Location = new Point(panelWidth - 70, panelHeight - 35); 
                }
                else if (control is Panel card)
                {
                    int index = panelTable.Controls.GetChildIndex(card);
                    int row = index / 2;
                    int col = index % 2;

                    // Resize and reposition the cards
                    card.Size = new Size(cardWidth, cardHeight);
                    card.Location = new Point(spacingX + col * (cardWidth + spacingX), spacingY + row * (cardHeight + spacingY));

                    foreach (Control child in card.Controls)
                    {
                        if (child is TextBox txtCardContent)
                        {
                            txtCardContent.Font = newFont;
                            txtCardContent.Size = new Size(cardWidth - 20, cardHeight - 20);
                        }
                        else if (child is Button btn)
                        {
                            // Ensure the button has the correct text and handle positioning
                            if (btn.Text == "Időpont módosítás" || btn.Text == "Állapot módosítás")
                            {
                                btn.Size = new Size(160, 30); 
                                btn.Location = new Point(card.Width - btn.Width - 10, card.Height - btn.Height - 10);
                                btn.Font = new Font("Segoe UI", 12, FontStyle.Regular);
                            }
                        }
                    }
                }
            }
        }


        private void Szerelesek_Click(object sender, EventArgs e)
        {
            LoadSzerelesek();
        }
        private void Ugyfelek_Click(object sender, EventArgs e)
        {
            LoadData("Ügyfelek", ugyfelekSql);
        }
        private void SzerelesekUjHozzaadasa_Click(object sender, EventArgs e)
        {
            InputGuide Add = new InputGuide();
            Add.ShowDialog();
            InitializeTable(utmutatoSql);
        }

        // Placeholder for the "Módosítás" button click
        private void SzerelesekModositas_Click(object sender, EventArgs e)
        {
            if (table_DGV.SelectedRows.Count == 0 || table_DGV.SelectedRows.Count > 1)
            {
                MessageBox.Show("Kérlek válassz egy hibakódot a módosításhoz!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DataGridViewRow selectedRow = table_DGV.SelectedRows[0];

                // Get the data from the selected row
                string cim = selectedRow.Cells["Útmutató címe"].Value?.ToString();
                string utmutato = selectedRow.Cells["Útmutató"].Value?.ToString();
                string tipus = selectedRow.Cells["Autó típusa"].Value?.ToString();
                string sqlQuery = "SELECT tipus_id FROM tipus WHERE tipus = @nev";
                var parameters = new Dictionary<string, object>
                {
                    { "@nev", tipus }
                };
                int tipusID=databaseHandler.LookupID(sqlQuery, parameters);

                // Pass data to újHibakód for editing
                InputGuide Edit = new InputGuide(cim,utmutato,tipusID);
                Edit.ShowDialog();
                InitializeTable(utmutatoSql);
            }
        }
        private void SzerelesiUtmutatok_Click(object sender, EventArgs e)
        {
            panelTable.Controls.Clear();
            aktivMenu = "Útmutatók";
            InitializeTable(utmutatoSql);
            panelButtons.Controls.Clear();
            searchBar.Text = "";
            if (Role=="Adminisztrátor")
            {
                GenerateHoverPanel("Új hozzáadása", new Point(0, 0), new Size(180, 63), SzerelesekUjHozzaadasa_Click);
                GenerateHoverPanel("Módosítás", new Point(180, 0), new Size(140, 63), SzerelesekModositas_Click);
                GenerateHoverPanel("Vissza", new Point(140 + 180, 0), new Size(140, 63), Szerelesek_Click);
            }
            else
            {
                GenerateHoverPanel("Vissza", new Point(0, 0), new Size(140, 63), Szerelesek_Click);
            }
            
        }
        private void HibakodokUjHozzaadasa_Click(object sender, EventArgs e)
        {
            InputCode ujHibaKod = new InputCode();
            ujHibaKod.ShowDialog();
            InitializeTable(hibakodSql);
        }

        private void HibakodokModositas_Click(object sender, EventArgs e)
        {

                if (table_DGV.SelectedRows.Count == 0 || table_DGV.SelectedRows.Count>1)
                {
                    MessageBox.Show("Kérlek válassz egy hibakódot a módosításhoz!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DataGridViewRow selectedRow = table_DGV.SelectedRows[0];

                    // Get the data from the selected row
                    string hibakod = selectedRow.Cells["Hibakód"].Value?.ToString();
                    string leiras = selectedRow.Cells["Leírás"].Value?.ToString();

                    // Pass data to újHibakód for editing
                    InputCode hibakodForm = new InputCode(hibakod, leiras);
                    hibakodForm.ShowDialog();
                InitializeTable(hibakodSql);
                }
                
            
        }
        
        private void Hibakodok_Click(object sender, EventArgs e) 
        {
            panelTable.Controls.Clear();
            InitializeTable(hibakodSql);
            aktivMenu= "Hibakódok";
            panelButtons.Controls.Clear();
            searchBar.Text = "";
            if (Role == "Adminisztrátor")
            {
                GenerateHoverPanel("Új hozzáadása", new Point(0, 0), new Size(180, 63), HibakodokUjHozzaadasa_Click);
                GenerateHoverPanel("Módosítás", new Point(180, 0), new Size(140, 63), HibakodokModositas_Click);
                GenerateHoverPanel("Vissza", new Point(140 + 180, 0), new Size(140, 63), Szerelesek_Click);
            }
            else
            {
                GenerateHoverPanel("Vissza", new Point(0, 0), new Size(140, 63), Szerelesek_Click);
            }
        }
        private void MunkafolyamatSablonokUjHozzaadasa_Click(object sender, EventArgs e)
        {
            InputWorkFlow ujMunkaF = new InputWorkFlow();
            ujMunkaF.ShowDialog();
            InitializeTable(munkafolySql);
        }

        // Placeholder for the "Módosítás" button click
        private void MunkafolyamatSablonokModositas_Click(object sender, EventArgs e)
        {
            if (table_DGV.SelectedRows.Count == 0 || table_DGV.SelectedRows.Count > 1)
            {
                MessageBox.Show("Kérlek válassz egy sablont a módosításhoz!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DataGridViewRow selectedRow = table_DGV.SelectedRows[0];

                // Get the data from the selected row
                string nev = selectedRow.Cells["Sablon neve"].Value?.ToString();
                string lepesek = selectedRow.Cells["Lépések"].Value?.ToString();
                string becsultIdo = selectedRow.Cells["Becsült Idő"].Value?.ToString();

                // Pass data to újMunkafolyamat for editing
                InputWorkFlow sablonForm = new InputWorkFlow(nev, lepesek, becsultIdo);
                sablonForm.ShowDialog();

                // Refresh the table after editing
                InitializeTable(munkafolySql);
            }
        }
    
        private void MunkafolyamatSablonok_Click(object sender, EventArgs e) 
        {
            panelTable.Controls.Clear();
            InitializeTable(munkafolySql);
            aktivMenu = "Munkafolyamatok";
            panelButtons.Controls.Clear();
            searchBar.Text = "";
            if (Role == "Adminisztrátor")
            {
                GenerateHoverPanel("Új hozzáadása", new Point(0, 0), new Size(180, 63), MunkafolyamatSablonokUjHozzaadasa_Click);
                GenerateHoverPanel("Módosítás", new Point(180, 0), new Size(140, 63), MunkafolyamatSablonokModositas_Click);
                GenerateHoverPanel("Vissza", new Point(180 + 140, 0), new Size(140, 63), Szerelesek_Click);
            }
            else
            {
                GenerateHoverPanel("Vissza", new Point(0, 0), new Size(140, 63), Szerelesek_Click);
            }
        }
        private void SzervizCsomagok_Click(object sender, EventArgs e) 
        {
            panelTable.Controls.Clear();
            InitializeTable(szervizSql);
            aktivMenu = "Szervizcsomagok";
            panelButtons.Controls.Clear();
            searchBar.Text = "";
            if (Role == "Adminisztrátor")
            {
                GenerateHoverPanel("Új hozzáadása", new Point(0, 0), new Size(180, 63), SzervizCsomagokUjHozzaadasa_Click);
                GenerateHoverPanel("Módosítás", new Point(180, 0), new Size(140, 63), SzervizCsomagokModositas_Click);
                GenerateHoverPanel("Vissza", new Point(180 + 140, 0), new Size(140, 63), Szerelesek_Click);
            }
            else
            {
                GenerateHoverPanel("Vissza", new Point(0, 0), new Size(140, 63), Szerelesek_Click);
            }
        }
        private void SzervizCsomagokUjHozzaadasa_Click(object sender, EventArgs e) 
        {
            InputPackages ujSzervizCsomag = new InputPackages();
            ujSzervizCsomag.ShowDialog();
            InitializeTable(szervizSql);
        }
        private void SzervizCsomagokModositas_Click(object sender, EventArgs e)
        {
            if (table_DGV.SelectedRows.Count == 0 || table_DGV.SelectedRows.Count > 1)
            {
                MessageBox.Show("Kérlek válassz egy szervíz csomagot a módosításhoz!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DataGridViewRow selectedRow = table_DGV.SelectedRows[0];

                // Get the data from the selected row
                string csomag = selectedRow.Cells["Csomag"].Value?.ToString();
                string leiras = selectedRow.Cells["Leírás"].Value?.ToString();
                string arRaw = selectedRow.Cells["Ár"].Value?.ToString();
                int ar = Convert.ToInt32(arRaw.Substring(0, arRaw.Length - 6));
                var parameters = new Dictionary<string, object>
                {
                    { "@nev", csomag },
                    {"@leiras", leiras },
                    { "@ar", ar}
                };
                string sqlQuery = @"SELECT csomag_id FROM szervizcsomagok sz WHERE sz.nev=@nev AND sz.leiras=@leiras AND sz.ar=@ar;";
                int Id = databaseHandler.GetScalarValue(sqlQuery, parameters);

                // Pass data to InputPackages for editing
                InputPackages szervizForm = new InputPackages(Id, csomag, leiras, ar);
                szervizForm.ShowDialog();
                // Refresh the table after editing
                InitializeTable(szervizSql);
            }
        }

        private void LogOutPan_Clicked(object sender, EventArgs e)
        {
            CustomizeTable(table_DGV);
            this.Hide();

            using (LoginForm loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    this.Username = loginForm.Username;
                    this.Role = loginForm.Role;
                    this.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
            Main_Form_Load(sender, e);
        }

        private void SettingsPanel_Clicked(object sender, EventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            registrationForm.ShowDialog();
        }
        private void CustomizeTable(DataGridView table)
        {
            // Table background and border settings
            table.BackgroundColor = Color.FromArgb(46, 51, 73); // Matches the overall dark theme
            table.BorderStyle = BorderStyle.None;
            table.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            table.GridColor = Color.FromArgb(58, 63, 85); // Subtle contrast for grid lines

            // Alternating row styling
            table.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(50, 55, 78);
            table.DefaultCellStyle.BackColor = Color.FromArgb(58, 63, 85);
            table.DefaultCellStyle.ForeColor = Color.WhiteSmoke;

            // Selection styling
            table.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 153, 204);
            table.DefaultCellStyle.SelectionForeColor = Color.White;

            // Column headers
            table.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            table.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73);
            table.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(200, 200, 220);
            table.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            table.EnableHeadersVisualStyles = false;

            // Row header (left empty column for selection)
            table.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73); // Match background
            table.RowHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;
            table.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 153, 204); // Match selection style
            table.RowHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            table.RowHeadersWidth = 40; // Adjust width if needed
            table.RowHeadersVisible = true; // Set to false if the empty column is unnecessary
            table.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // Scrollbar styling
            table.ScrollBars = ScrollBars.Both; // Ensure scrollbars are enabled
            table.DefaultCellStyle.BackColor = Color.FromArgb(58, 63, 85); 
            // Table behavior
            table.AllowUserToAddRows = false;
            table.AllowUserToDeleteRows = false;
            table.ReadOnly = true;
            table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            table.RowTemplate.Height = 35;

            // Column settings
            foreach (DataGridViewColumn column in table.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            // Positioning
            table.Size = new Size(panelMain.Width - 30, panelTable.Height - 46);
            table.Location = new Point(20, 20);
        }
        private void Table_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int height = (aktivMenu == "Járművek") ? 400 : 300;

            // Ensure the double-click is on a valid cell (not the header)
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridView table = sender as DataGridView;
                DataGridViewRow row = table.Rows[e.RowIndex];

                // Create the detail form
                Form detailForm = new Form
                {
                    Text = "Részletek",
                    Icon = new Icon(new MemoryStream(Properties.Resources.Main_logo)),
                    Size = new Size(400, height),
                    StartPosition = FormStartPosition.CenterParent,
                    BackColor = Color.FromArgb(46, 51, 73),
                    FormBorderStyle = FormBorderStyle.FixedSingle,
                    AutoScroll = true // Enable scrolling if needed
                };

                int yOffset = 20; 
                int indentX = 40;
                int PrevX = 113;

                // Loop through the row cells and create labels
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string columnName = table.Columns[cell.ColumnIndex].HeaderText;
                    string cellValue = cell.Value?.ToString() ?? "";

                    if (columnName == "Előző javítások" && !string.IsNullOrWhiteSpace(cellValue))
                    {
                        string[] repairs = cellValue.Split(new[] { "\n" }, StringSplitOptions.None);

                        // First entry is on the same line as the column title
                        Label lblTitle = new Label
                        {
                            Text = $"{columnName}: {repairs[0]}", // First entry stays at X=20
                            AutoSize = true,
                            ForeColor = Color.WhiteSmoke,
                            Font = new Font("Segoe UI", 14),
                            Location = new Point(20, yOffset)
                        };
                        detailForm.Controls.Add(lblTitle);
                        yOffset += 20; // Move down for next entries
                        

                        // Labels for the remaining repairs (indented)
                        for (int i = 1; i < repairs.Length; i++)
                        {
                            Label lblRepair = new Label
                            {
                                Text = repairs[i],
                                AutoSize = true,
                                ForeColor = Color.WhiteSmoke,
                                Font = new Font("Segoe UI", 14),
                                Location = new Point(PrevX+indentX, yOffset) // Subsequent entries are offset
                            };
                            detailForm.Controls.Add(lblRepair);
                            yOffset += 20;
                        }
                    }
                    else
                    {
                        Label lblDetail = new Label
                        {
                            Text = $"{columnName}: {cellValue}",
                            AutoSize = true,
                            ForeColor = Color.WhiteSmoke,
                            Font = new Font("Segoe UI", 14),
                            Location = new Point(20, yOffset) // Normal alignment
                        };
                        detailForm.Controls.Add(lblDetail);
                        yOffset += 30;
                    }
                }

                detailForm.ShowDialog(); // Open as modal dialog
            }
        }

        private void ClearPanelContentsExceptOne(Panel panel, Control controlToKeep)
        {
            // Iterate through the controls in the panel
            foreach (Control control in panel.Controls.OfType<Control>().ToList())
            {
                // Remove the control from the panel, except the one you want to keep
                if (control != controlToKeep)
                {
                    panel.Controls.Remove(control);
                }
            }
        }
        private Dictionary<string, string> searchQueries = new Dictionary<string, string>
        {
    { "Ügyfelek",
        "SELECT nev AS Név, telefonszam AS Telefonszám, email AS 'E-mail', cim AS Cím FROM ugyfelek WHERE nev LIKE '%{0}%' OR telefonszam LIKE '%{0}%' OR email LIKE '%{0}%' OR cim LIKE '%{0}%'" },

    { "Alkatrészek",
        "SELECT nev AS Alkatrész, leiras AS Leírás, keszlet_mennyiseg AS Készlet, utanrendelesi_szint AS 'Utánrendelési szint' FROM alkatreszek WHERE nev LIKE '%{0}%' OR leiras LIKE '%{0}%' OR keszlet_mennyiseg LIKE '%{0}%' OR utanrendelesi_szint LIKE '%{0}%'" },

    { "Járművek",
        "SELECT j.rendszam AS Rendszám, t.tipus AS Típus, m.marka_neve AS Márka, h.kod AS Hibakód, mf.nev AS Sablon, j.gyartas_eve AS 'Gyártás éve', j.motor_adatok AS 'Motor adatok', j.alvaz_adatok AS 'Alváz adatok', j.elozo_javitasok AS 'Előző javítások' FROM jarmuvek j JOIN tipus t ON j.tipus_id = t.tipus_id JOIN hibakodok h ON h.kod_id = j.kod_id JOIN munkafolyamat_sablonok mf ON mf.sablon_id = j.sablon_id JOIN marka m ON t.marka_id = m.marka_id WHERE j.rendszam LIKE '%{0}%' OR t.tipus LIKE '%{0}%' OR m.marka_neve LIKE '%{0}%' OR h.kod LIKE '%{0}%'" },

    { "Hibakódok",
        "SELECT kod AS Hibakód, leiras AS Leírás FROM hibakodok WHERE kod LIKE '%{0}%' OR leiras LIKE '%{0}%'" },

    { "Munkafolyamatok",
        "SELECT nev AS 'Sablon neve', lepesek AS Lépések, becsult_ido AS 'Becsült idő' FROM munkafolyamat_sablonok WHERE nev LIKE '%{0}%' OR lepesek LIKE '%{0}%'" },

    { "Útmutatók",
        "SELECT s.cim AS 'Útmutató címe', s.tartalom AS Útmutató, m.marka_neve AS 'Autó márka', t.tipus AS 'Autó típusa' FROM szerelesi_utmutatok s JOIN tipus t ON s.jarmu_tipus = t.tipus_id JOIN marka m ON t.marka_id = m.marka_id WHERE s.cim LIKE '%{0}%' OR s.tartalom LIKE '%{0}%' OR m.marka_neve LIKE '%{0}%' OR t.tipus LIKE '%{0}%'" },

    { "Szervizcsomagok",
        "SELECT s.nev AS Csomag, s.leiras AS Leírás, CONCAT(s.ar,' Ft') AS Ár FROM szervizcsomagok s WHERE s.leiras LIKE '%{0}%' OR s.ar='{0}'"},

    { "Márkák",
        "SELECT t.tipus AS Modell, m.marka_neve AS Márka FROM tipus t RIGHT JOIN marka m ON m.marka_id=t.marka_id WHERE t.tipus LIKE '%{0}%' OR m.marka_neve='%{0}%'"}
};


        private void searchButton_Click(object sender, EventArgs e)
        {
            string keresettKifejezes = searchBar.Text.Trim();

            if (!string.IsNullOrEmpty(keresettKifejezes) && aktivMenu != null)
            {
                if (searchQueries.ContainsKey(aktivMenu))
                {
                    string[] keywords = keresettKifejezes.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string baseQuery = searchQueries[aktivMenu];

                    // Extract the WHERE clause and modify it for multiple search terms
                    string whereClause = "";
                    foreach (string keyword in keywords)
                    {
                        if (whereClause != "")
                            whereClause += " OR ";

                        whereClause += "(" + baseQuery.Substring(baseQuery.IndexOf("WHERE") + 6)
                                            .Replace("{0}", $"{keyword}") + ")";
                    }

                    // Reconstruct the full query
                    string finalQuery = baseQuery.Substring(0, baseQuery.IndexOf("WHERE") + 6) + " " + whereClause;

                    // Execute the query
                    InitializeTable(finalQuery);
                }
            }
        }

        private void Main_Form_Resize(object sender, EventArgs e)
        {
            table_DGV.Size = new Size(panelMain.Width - 30, panelTable.Height - 46);
            ResizeAppointmentCards();
        }
        private void panelTable_Click(object sender, EventArgs e)
        {
            searchBar.Focus();
        }
    }
}