using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
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
        //Sql lekérdezések
        static string ugyfelekSql= "SELECT nev AS Név, telefonszam AS Telefonszám, email AS 'E-mail' ,cim AS Cím FROM ugyfelek; ";
        static string alkatreszekSql= "SELECT nev AS Alkatrész, leiras AS Leírás, keszlet_mennyiseg AS Készlet, utanrendelesi_szint AS 'Utánrendelési szint' FROM alkatreszek";
        static string jarmuvekSql= "SELECT rendszam AS Rendszám, t.tipus AS Típus, m.marka_neve AS Márka, h.kod AS Hibakód, mf.nev AS Sablon, gyartas_eve AS 'Gyártás éve', motor_adatok AS 'Motor adatok', alvaz_adatok AS 'Alváz adatok', elozo_javitasok AS 'Előző javítások' FROM jarmuvek j JOIN tipus t ON j.tipus_id= t.tipus_id JOIN hibakodok h ON h.kod_id = j.kod_id JOIN munkafolyamat_sablonok mf ON mf.sablon_id = j.sablon_id JOIN marka m ON t.marka_id= m.marka_id;";
        static string hibakodSql = "SELECT kod AS Hibakód, leiras AS Leírás FROM hibakodok";
        static string munkafolySql = "SELECT nev AS 'Sablon neve', lepesek AS Lépések, becsult_ido AS 'Becsült idő' FROM munkafolyamat_sablonok";
        static string utmutatoSql = "SELECT s.cim AS 'Útmutató címe', s.tartalom AS Útmutató, m.marka_neve AS 'Autó márka', t.tipus AS 'Autó típusa' FROM szerelesi_utmutatok s JOIN tipus t ON s.jarmu_tipus=t.tipus_id JOIN marka m ON m.marka_id= t.marka_id";
        
        public Main_Form()
        {
            InitializeComponent();
            //no paddig
            foreach (Control control in this.tableLayoutPanel1.Controls)
            {
                control.Margin = new Padding(0);
            }
            customersPanel.PanelClicked += (sender, e) => LoadData("Ügyfelek",ugyfelekSql);
            partsPanel.PanelClicked += (sender, e) => LoadData("Alkatrészek", alkatreszekSql);
            repairsPanel.PanelClicked += (sender, e) => LoadSzerelesek();
            carsPanel.PanelClicked += (sender, e) => LoadData("Járművek", jarmuvekSql);
            logOutPan.PanelClicked += LogOutPan_Clicked;
            settingsPanel.PanelClicked += SettingsPanel_Clicked;
            LabelTable.Text = "";
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
            }
            else if (Role == "Szerelő")
            {
                PicBoxRole.Image = AutoMuhely.Properties.Resources.user;
                PicBoxRole.Location = new Point(22, 27);
                settingsPanel.Visible = false;
            }
        }

        private void AdjustLabelFontSize(string text)
        {
            // If text length is 7 characters or fewer, use the default font size
            if (text.Length <= 7)
            {
                LabelUser.Font = new Font("Open Sans Extrabold", 27.75F, FontStyle.Bold);
            }
            else if (text.Length <= 10)
            {
                LabelUser.Font = new Font("Open Sans Extrabold", 22F, FontStyle.Bold);  // Slightly smaller font size for longer text
            }
            else if (text.Length <= 15)
            {
                LabelUser.Font = new Font("Open Sans Extrabold", 18F, FontStyle.Bold);  // Even smaller
            }
            else if (text.Length <= 20)
            {
                LabelUser.Font = new Font("Open Sans Extrabold", 11F, FontStyle.Bold);  // Smaller for very long text
            }
            else
            {
                LabelUser.Font = new Font("Open Sans Extrabold", 10F, FontStyle.Bold);  // Smallest font size for extremely long text
            }
        }
        private void LoadData(string menu, string query)
        {
            aktivMenu = menu;
            LabelTable.Text = aktivMenu;
            ClearPanelContentsExceptOne(panelTable, LabelTable);
            InitializeTable(query);
            panelButtons.Controls.Clear();
            if (aktivMenu== "Ügyfelek")
            {
                GenerateHoverPanel("Új hozzáadása", new Point(0, 0), new Size(200, 63), UjHozzaadasa_Click);
                GenerateHoverPanel("Módosítás", new Point(200, 0), new Size(140, 63), Modositas_Click);
            }
            else if(aktivMenu == "Alkatrészek")
            {
                GenerateHoverPanel("Új hozzáadása", new Point(0, 0), new Size(200, 63), UjHozzaadasa_Click);
                GenerateHoverPanel("Módosítás", new Point(200, 0), new Size(140, 63), Modositas_Click);
            }
            else if(aktivMenu == "Járművek")
            {
                GenerateHoverPanel("Új hozzáadása", new Point(0, 0), new Size(200, 63), UjHozzaadasa_Click);
                GenerateHoverPanel("Módosítás", new Point(200, 0), new Size(140, 63), Modositas_Click);
                GenerateHoverPanel("Ügyfélhez csatolás", new Point(340, 0), new Size(240, 63), Ugyfel_Jarmuvek);
            }
            

        }
        private void UjHozzaadasa_Click(object sender, EventArgs e)
        {
            if (aktivMenu == "Ügyfelek")
            {
            }
            else if (aktivMenu == "Alkatrészek")
            {
                InputParts aInput = new InputParts();
                aInput.ShowDialog();
                LoadData("Alkatrészek", alkatreszekSql);
            }
            else if (aktivMenu == "Járművek")
            {
                InputVehicle newVehicle = new InputVehicle();
                newVehicle.ShowDialog();
                LoadData("Járművek", jarmuvekSql);
            }
        }

        // Placeholder for the "Módosítás" button click
        private void Modositas_Click(object sender, EventArgs e)
        {
            if (aktivMenu == "Ügyfelek")
            {
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
                InitializeTable(jarmuvekSql); // Refresh the table after editing
            }
        }
        private void Ugyfel_Jarmuvek(object sender, EventArgs e)
        {

        }
        private void InitializeTable(string query, Dictionary<string, object> parameters = null)
        {
            CustomizeTable(table_DGV);
            table_DGV.Rows.Clear();
            table_DGV.Columns.Clear();

            table_DGV.CellDoubleClick -= Table_DGV_CellDoubleClick; // Avoid duplicate event handlers
            table_DGV.CellDoubleClick += Table_DGV_CellDoubleClick;
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
        private void Table_DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (aktivMenu == "Ügyfelek" && e.RowIndex >= 0) // Ensure it's the "ügyfelek" table and a valid row
            {
                DataGridViewRow selectedRow = table_DGV.Rows[e.RowIndex];

                // Extract all columns to uniquely identify the customer
                string name = selectedRow.Cells["Név"]?.Value?.ToString();
                string address = selectedRow.Cells["Cím"]?.Value?.ToString();

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(address))
                {
                    // Eredeti sql lekérdezésből alakítjuk az újat
                    string query = jarmuvekSql.Substring(0, jarmuvekSql.Length - 1)+ @" j JOIN ugyfel_jarmuvek uj ON j.jarmu_id = uj.jarmu_id JOIN ugyfelek u ON uj.ugyfel_id = uj.ugyfel_id WHERE u.nev = @name AND u.cim = @address;";
                    // Load the car data into the table
                    var parameters = new Dictionary<string, object>
                    {
                        { "@name", name },
                        { "@address", address }
                    };
                    InitializeTable(query, parameters);
                    
                }
                else
                {
                    MessageBox.Show("Nem található az ügyfél!", "Hiba");
                }
            }
            else if (aktivMenu == "Alkatrészek")
            {
                //alkatrész rendelési státusz
                
            }
            else if (aktivMenu == "Járművek")
            {
                //járműhöz kapcsolódó ügyfél időpont
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
            LabelTable.Text = "Szerelések";
            panelButtons.Controls.Clear();
            ClearPanelContentsExceptOne(panelTable, LabelTable);
            GenerateHoverPanel("Szerelési Útmutatók", new Point(0, 0), new Size(246, 63), SzerelesiUtmutatok_Click);
            GenerateHoverPanel("Hibakódok", new Point(246, 0), new Size(160, 63), Hibakodok_Click);
            GenerateHoverPanel("Munkafolyamat Sablonok", new Point(160+246, 0), new Size(300, 63), MunkafolyamatSablonok_Click);
        }
        private void Szerelesek_Click(object sender, EventArgs e)
        {
            LoadSzerelesek();
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
            LabelTable.Text ="Szerelési Útmutatók";
            InitializeTable(utmutatoSql);
            panelButtons.Controls.Clear();
            GenerateHoverPanel("Új hozzáadása", new Point(0, 0), new Size(200, 63), SzerelesekUjHozzaadasa_Click);
            GenerateHoverPanel("Módosítás", new Point(200, 0), new Size(140, 63), SzerelesekModositas_Click);
            GenerateHoverPanel("Vissza", new Point(140 + 200, 0), new Size(140, 63), Szerelesek_Click);
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

            InitializeTable(hibakodSql);
            LabelTable.Text = "Hibakódok";
            panelButtons.Controls.Clear();
            GenerateHoverPanel("Új hozzáadása", new Point(0, 0), new Size(200, 63), HibakodokUjHozzaadasa_Click);
            GenerateHoverPanel("Módosítás", new Point(200, 0), new Size(140, 63), HibakodokModositas_Click);
            GenerateHoverPanel("Vissza", new Point(140 + 200, 0), new Size(140, 63), Szerelesek_Click);
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
            InitializeTable(munkafolySql);
            LabelTable.Text = "Munkafolyamat Sablonok";
            panelButtons.Controls.Clear();
            GenerateHoverPanel("Új hozzáadása", new Point(0, 0), new Size(200, 63), MunkafolyamatSablonokUjHozzaadasa_Click);
            GenerateHoverPanel("Módosítás", new Point(200, 0), new Size(140, 63), MunkafolyamatSablonokModositas_Click);
            GenerateHoverPanel("Vissza", new Point(200 + 140, 0), new Size(140, 63), Szerelesek_Click);
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
            table.BackgroundColor = Color.WhiteSmoke;
            table.BorderStyle = BorderStyle.None;
            table.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            table.GridColor = Color.LightGray;

            table.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            table.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            table.DefaultCellStyle.SelectionForeColor = Color.White;

            table.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            table.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            table.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            table.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            table.EnableHeadersVisualStyles = false;

            table.AllowUserToAddRows = false;
            table.AllowUserToDeleteRows = false;
            table.ReadOnly = true;
            table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            table.RowTemplate.Height = 35;

            foreach (DataGridViewColumn column in table.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            table.Size = new Size(panelMain.Width - 30, panelMain.Height - 150);
            table.Location = new Point(20, 70);
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
        "SELECT nev AS Név, telefonszam AS Telefonszám, email AS 'E-mail', cim AS Cím " +
        "FROM ugyfelek WHERE nev LIKE '%{0}%' OR telefonszam LIKE '%{0}%' OR email LIKE '%{0}%' OR cim LIKE '%{0}%';" },

    { "Alkatrészek",
        "SELECT nev AS Alkatrész, leiras AS Leírás, keszlet_mennyiseg AS Készlet, utanrendelesi_szint AS 'Utánrendelési szint' " +
        "FROM alkatreszek WHERE nev LIKE '%{0}%' OR leiras LIKE '%{0}%' OR keszlet_mennyiseg LIKE '%{0}%' OR utanrendelesi_szint LIKE '%{0}%';" },

    { "Járművek",
        "SELECT j.rendszam AS Rendszám, t.tipus AS Típus, m.marka_neve AS Márka, h.kod AS Hibakód, mf.nev AS Sablon, " +
        "j.gyartas_eve AS 'Gyártás éve', j.motor_adatok AS 'Motor adatok', j.alvaz_adatok AS 'Alváz adatok', j.elozo_javitasok AS 'Előző javítások' " +
        "FROM jarmuvek j " +
        "JOIN tipus t ON j.tipus_id = t.tipus_id " +
        "JOIN hibakodok h ON h.kod_id = j.kod_id " +
        "JOIN munkafolyamat_sablonok mf ON mf.sablon_id = j.sablon_id " +
        "JOIN marka m ON t.marka_id = m.marka_id " +
        "WHERE j.rendszam LIKE '%{0}%' OR t.tipus LIKE '%{0}%' OR m.marka_neve LIKE '%{0}%' OR h.kod LIKE '%{0}%';" },

    { "Hibakódok",
        "SELECT kod AS Hibakód, leiras AS Leírás " +
        "FROM hibakodok WHERE kod LIKE '%{0}%' OR leiras LIKE '%{0}%';" },

    { "Munkafolyamatok",
        "SELECT nev AS 'Sablon neve', lepesek AS Lépések, becsult_ido AS 'Becsült idő' " +
        "FROM munkafolyamat_sablonok WHERE nev LIKE '%{0}%' OR lepesek LIKE '%{0}%';" },

    { "Útmutatók",
        "SELECT s.cim AS 'Útmutató címe', s.tartalom AS Útmutató, m.marka_neve AS 'Autó márka', t.tipus AS 'Autó típusa' " +
        "FROM szerelesi_utmutatok s " +
        "JOIN tipus t ON s.jarmu_tipus = t.tipus_id " +
        "JOIN marka m ON t.marka_id = m.marka_id " +
        "WHERE s.cim LIKE '%{0}%' OR s.tartalom LIKE '%{0}%' OR m.marka_neve LIKE '%{0}%' OR t.tipus LIKE '%{0}%';" }
};
        private void SearchBar_TextChanged(object sender, EventArgs e)
        {
            string keresettKifejezes = searchBar.Text.Trim();
            if (searchQueries.ContainsKey(aktivMenu))
            {
                string query = string.Format(searchQueries[aktivMenu], keresettKifejezes);
                InitializeTable(query);
            }
        }

        private void Main_Form_Resize(object sender, EventArgs e)
        {
            table_DGV.Size = new Size(panelMain.Width - 30, panelMain.Height - 10);
        }

    }
}