using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AutoMuhely
{
    public partial class Main_Form : Form
    {

        private string aktívMenü;
        static DataGridView table_DGV = new DataGridView();

        public string Username { get; set; }
        public string Role { get; set; }
        static DatabaseHandler databaseHandler = new DatabaseHandler();

        public Main_Form()
        {
            InitializeComponent();

            customersPanel.PanelClicked += (sender, e) => LoadData("ügyfelek", "SELECT nev AS Név, elerhetoseg AS Elérhetőség, cim AS Cím FROM ugyfelek");
            partsPanel.PanelClicked += (sender, e) => LoadData("alkatrészek", "SELECT nev AS Alkatrész, leiras AS Leírás, keszlet_mennyiseg AS Készlet, utanrendelesi_szint AS 'Utánrendelési szint' FROM alkatreszek");
            repairsPanel.PanelClicked += (sender, e) => LoadSzerelesek();
            carsPanel.PanelClicked += (sender, e) => LoadData("járművek", "SELECT * FROM jarmuvek");
            logOutPan.PanelClicked += LogOutPan_Clicked;
            settingsPanel.PanelClicked += SettingsPanel_Clicked;
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            LabelUser.Text = Username;

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

        private void LoadData(string menu, string query)
        {
            aktívMenü = menu;
            panelTable.Controls.Clear();
            InitializeTable(query);
            panelButtons.Controls.Clear();           
        }

        private void InitializeTable(string query)
        {
            CustomizeTable(table_DGV);
            table_DGV.Rows.Clear();
            table_DGV.Columns.Clear();
            var (results, columnNames) = databaseHandler.Select(query);
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
            aktívMenü = "szerelések";
            
            panelButtons.Controls.Clear();
            panelTable.Controls.Clear();
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
            InitializeTable("SELECT * FROM szerelesi_utmutatok");
            panelButtons.Controls.Clear();
            GenerateHoverPanel("Új hozzáadása", new Point(0, 0), new Size(200, 63), SzerelesekUjHozzaadasa_Click);
            GenerateHoverPanel("Módosítás", new Point(200, 0), new Size(140, 63), SzerelesekModositas_Click);
            GenerateHoverPanel("Vissza", new Point(140 + 200, 0), new Size(140, 63), Szerelesek_Click);
        }
        private void HibakodokUjHozzaadasa_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Új hozzáadása clicked!");
        }

        // Placeholder for the "Módosítás" button click
        private void HibakodokModositas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módosítás clicked!");
        }
        private void Hibakodok_Click(object sender, EventArgs e) 
        {
            InitializeTable("SELECT * FROM hibakodok");
            panelButtons.Controls.Clear();
            GenerateHoverPanel("Új hozzáadása", new Point(0, 0), new Size(200, 63), HibakodokUjHozzaadasa_Click);
            GenerateHoverPanel("Módosítás", new Point(200, 0), new Size(140, 63), HibakodokModositas_Click);
            GenerateHoverPanel("Vissza", new Point(140 + 200, 0), new Size(140, 63), Szerelesek_Click);
        }
        private void MunkafolyamatSablonokUjHozzaadasa_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Új hozzáadása clicked!");
        }

        // Placeholder for the "Módosítás" button click
        private void MunkafolyamatSablonokModositas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módosítás clicked!");
        }
        private void MunkafolyamatSablonok_Click(object sender, EventArgs e) 
        {
            InitializeTable("SELECT * FROM munkafolyamat_sablonok");
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

            table.Size = new Size(panelMain.Width - 20, panelMain.Height - 100);
            table.Location = new Point(10, 10);
        }

        private Dictionary<string, string> searchQueries = new Dictionary<string, string>
        {
            { "ügyfelek", "SELECT * FROM ugyfelek WHERE nev LIKE '%{0}%' OR elerhetoseg LIKE '%{0}%' OR cim LIKE '%{0}%'" },
            { "alkatrészek", "SELECT * FROM alkatreszek WHERE nev LIKE '%{0}%' OR leiras LIKE '%{0}%' OR keszlet_mennyiseg LIKE '%{0}%' OR utanrendelesi_szint LIKE '%{0}%'" },
            { "járművek", "SELECT jarmuvek.jarmu_id, jarmuvek.rendszam, jarmuvek.tipus_id, jarmuvek.kod_id, jarmuvek.sablon_id, jarmuvek.gyartas_eve, jarmuvek.motor_adatok, jarmuvek.alvaz_adatok, jarmuvek.elozo_javitasok, tipus.tipus, hibakodok.kod, munkafolyamat_sablonok.nev FROM jarmuvek AS j INNER JOIN tipus AS t ON j.tipus_id = t.tipus_id INNER JOIN hibakodok AS k ON j.kod_id = k.kod_id INNER JOIN munkafolyamat_sablonok AS s ON j.sablon_id = s.sablon_id WHERE j.rendszam LIKE '%{0}%' OR t.tipus LIKE '%{0}%'" }
        };
        private void SearchBar_TextChanged(object sender, EventArgs e)
        {
            string keresettKifejezes = searchBar.Text.Trim();
            if (searchQueries.ContainsKey(aktívMenü))
            {
                string query = string.Format(searchQueries[aktívMenü], keresettKifejezes);
                InitializeTable(query);
            }
        }

        private void Main_Form_Resize(object sender, EventArgs e)
        {
            table_DGV.Size = new Size(panelMain.Width - 20, panelMain.Height - 100);
        }
    }
}