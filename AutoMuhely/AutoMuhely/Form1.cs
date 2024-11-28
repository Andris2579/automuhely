using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
        static string ugyfelekSql= "SELECT nev AS Név, elerhetoseg AS Elérhetőség, cim AS Cím FROM ugyfelek";
        static string alkatreszekSql= "SELECT nev AS Alkatrész, leiras AS Leírás, keszlet_mennyiseg AS Készlet, utanrendelesi_szint AS 'Utánrendelési szint' FROM alkatreszek";
        static string jarmuvekSql= "SELECT rendszam AS Rendszám, gyartas_eve AS 'Gyártás éve', motor_adatok AS 'Motor adatok', alvaz_adatok AS 'Alváz adatok', elozo_javitasok AS 'Előző javítások' FROM jarmuvek;";
        static string hibakodSql = "SELECT kod AS Hibakód, leiras AS Leírás FROM hibakodok";
        static string munkafolySql = "SELECT nev AS 'Sablon neve', lepesek AS Lépések, becsult_ido AS 'Becsült idő' FROM munkafolyamat_sablonok";

        public Main_Form()
        {
            InitializeComponent();

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
            }
            

        }
        private void UjHozzaadasa_Click(object sender, EventArgs e)
        {
            if (aktivMenu == "Ügyfelek")
            {
            }
            else if (aktivMenu == "Alkatrészek")
            {
                alkatrészInputs aInput = new alkatrészInputs();
                aInput.ShowDialog();
                LoadData("Alkatrészek", alkatreszekSql);
            }
            else if (aktivMenu == "Járművek")
            {
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
                if (table_DGV.SelectedRows.Count == 0)
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
                    alkatrészInputs aInput = new alkatrészInputs(alkatrész, leiras, keszlet, utanrendeles);
                    aInput.ShowDialog();
                }
                InitializeTable(alkatreszekSql);
            }
            else if (aktivMenu == "Járművek")
            {
            }
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
                string contact = selectedRow.Cells["Elérhetőség"]?.Value?.ToString();
                string address = selectedRow.Cells["Cím"]?.Value?.ToString();

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(contact) && !string.IsNullOrEmpty(address))
                {
                    // SQL query to fetch cars for the selected customer using joins
                    string query = @"SELECT j.rendszam AS Rendszám, j.gyartas_eve AS 'Gyártás éve', j.motor_adatok AS 'Motor Adatok', j.alvaz_adatok AS 'Alváz Adatok', j.elozo_javitasok AS 'Előző Javítások' FROM ugyfelek u INNER JOIN ugyfel_jarmuvek uj ON u.ugyfel_id = uj.ugyfel_id INNER JOIN jarmuvek j ON uj.jarmu_id = j.jarmu_id WHERE u.nev = @name AND u.elerhetoseg = @contact AND u.cim = @address;";
                    // Load the car data into the table
                    var parameters = new Dictionary<string, object>
                    {
                        { "@name", name },
                        { "@contact", contact },
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
                    Location = new Point(186, 6), // Right-aligned padding
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
            MessageBox.Show("Új hozzáadása clicked!");
        }

        // Placeholder for the "Módosítás" button click
        private void SzerelesekModositas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módosítás clicked!");
        }
        private void SzerelesiUtmutatok_Click(object sender, EventArgs e)
        {
            LabelTable.Text ="Szerelési Útmutatók";
            InitializeTable("SELECT * FROM szerelesi_utmutatok");
            panelButtons.Controls.Clear();
            GenerateHoverPanel("Új hozzáadása", new Point(0, 0), new Size(200, 63), SzerelesekUjHozzaadasa_Click);
            GenerateHoverPanel("Módosítás", new Point(200, 0), new Size(140, 63), SzerelesekModositas_Click);
            GenerateHoverPanel("Vissza", new Point(140 + 200, 0), new Size(140, 63), Szerelesek_Click);
        }
        private void HibakodokUjHozzaadasa_Click(object sender, EventArgs e)
        {
            újHibakód ujHibaKod = new újHibakód();
            ujHibaKod.ShowDialog();
            InitializeTable(hibakodSql);
        }

        // Placeholder for the "Módosítás" button click
        private void HibakodokModositas_Click(object sender, EventArgs e)
        {

                if (table_DGV.SelectedRows.Count == 0)
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
                    újHibakód hibakodForm = new újHibakód(hibakod, leiras);
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
            újMunkafolyamat ujMunkaF = new újMunkafolyamat();
            ujMunkaF.ShowDialog();
            InitializeTable(munkafolySql);
        }

        // Placeholder for the "Módosítás" button click
        private void MunkafolyamatSablonokModositas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módosítás clicked!");
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

            foreach (DataGridViewColumn column in table.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            table.Size = new Size(panelMain.Width - 30, panelMain.Height - 130);
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
            { "Ügyfelek", "SELECT * FROM ugyfelek WHERE nev LIKE '%{0}%' OR elerhetoseg LIKE '%{0}%' OR cim LIKE '%{0}%'" },
            { "Alkatrészek", "SELECT * FROM alkatreszek WHERE nev LIKE '%{0}%' OR leiras LIKE '%{0}%' OR keszlet_mennyiseg LIKE '%{0}%' OR utanrendelesi_szint LIKE '%{0}%'" },
            { "Járművek", "SELECT jarmuvek.jarmu_id, jarmuvek.rendszam, jarmuvek.tipus_id, jarmuvek.kod_id, jarmuvek.sablon_id, jarmuvek.gyartas_eve, jarmuvek.motor_adatok, jarmuvek.alvaz_adatok, jarmuvek.elozo_javitasok, tipus.tipus, hibakodok.kod, munkafolyamat_sablonok.nev FROM jarmuvek AS j INNER JOIN tipus AS t ON j.tipus_id = t.tipus_id INNER JOIN hibakodok AS k ON j.kod_id = k.kod_id INNER JOIN munkafolyamat_sablonok AS s ON j.sablon_id = s.sablon_id WHERE j.rendszam LIKE '%{0}%' OR t.tipus LIKE '%{0}%'" }
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
            table_DGV.Size = new Size(panelMain.Width - 30, panelMain.Height - 130);
        }
    }
}