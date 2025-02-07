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
            //no paddig on tablelayoutpan
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
            panelTable.Controls.Clear();
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
                InputCustomers inputCustomers = new InputCustomers();
                inputCustomers.ShowDialog();
                LoadData("Ügyfelek", ugyfelekSql);
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
                    InputCustomers inputCustomers = new InputCustomers(nev,telefon,cim,email);
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
            panelButtons.Controls.Clear();
            panelTable.Controls.Clear();
            InitSzerelesek();
            GenerateHoverPanel("Szerelési Útmutatók", new Point(0, 0), new Size(246, 63), SzerelesiUtmutatok_Click);
            GenerateHoverPanel("Hibakódok", new Point(246, 0), new Size(160, 63), Hibakodok_Click);
            GenerateHoverPanel("Munkafolyamat Sablonok", new Point(160+246, 0), new Size(300, 63), MunkafolyamatSablonok_Click);
        }

        private int currentPage = 1;
        private int totalPages = 1;
        private const int pageSize = 4;
        private void InitSzerelesek()
        {
            string query = $"SELECT COUNT(*) FROM idopontfoglalasok";
            int totalRecords = databaseHandler.GetScalarValue(query);
            totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            LoadAppointments();
            AddPagingButtons();
            ResizeAppointmentCards();
        }
        private void LoadAppointments()
        {
            int offset = (currentPage - 1) * pageSize;
            string query = $"SELECT u.nev, j.rendszam, t.tipus, m.marka_neve, sz.nev, idopont, allapot " +
                           "FROM idopontfoglalasok i " +
                           "LEFT JOIN jarmuvek j ON j.jarmu_id=i.jarmu_id " +
                           "LEFT JOIN tipus t ON t.tipus_id = j.tipus_id " +
                           "LEFT JOIN marka m ON m.marka_id = t.marka_id " +
                           "LEFT JOIN ugyfel_jarmuvek uj ON j.jarmu_id= uj.jarmu_id " +
                           "LEFT JOIN ugyfelek u ON u.ugyfel_id = uj.ugyfel_id " +
                           "LEFT JOIN szervizcsomagok sz ON sz.csomag_id = i.csomag_id " +
                           $"LIMIT {pageSize} OFFSET {offset}";

            var (results, _) = databaseHandler.Select(query);
            DisplayAppointmentCards(results);
            AddPagingButtons();
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

                Panel card = new Panel
                {
                    Size = new Size(cardWidth, cardHeight),
                    Location = new Point(spacingX + col * (cardWidth + spacingX), spacingY + row * (cardHeight + spacingY)),
                    BackColor = Color.FromArgb(24, 30, 54),
                    BorderStyle = BorderStyle.FixedSingle,
                    ForeColor = Color.FromArgb(245, 245, 241)
                };

                // Create a TextBox for the card content
                TextBox txtCardContent = new TextBox
                {
                    Multiline = true,
                    ReadOnly = true,
                    BackColor = card.BackColor,
                    ForeColor = Color.White,
                    BorderStyle = BorderStyle.None,
                    Location = new Point(10, 10),
                    Size = new Size(cardWidth - 20, cardHeight - 20), // Adjust size for text area
                    Font = new Font("Segoe UI", Math.Max(13, panelWidth / 60), FontStyle.Regular),
                    Text = GetCardText(results[i]), // Populate the content
                    HideSelection = true, // Prevent selection highlight and cursor display
                    Cursor = Cursors.Default // Remove typing cursor
                };

                // Add the TextBox to the card
                card.Controls.Add(txtCardContent);

                // Add card to the panel
                panelTable.Controls.Add(card);
            }
        }


        private string GetCardText(List<object> result)
        {
            // Combine all the text for the card in one string
            DateTime appointmentTime;
            var dateString = result[5]?.ToString();
            string appointmentText = string.IsNullOrEmpty(dateString) || dateString == "0000-00-00 00:00:00" ? "Érvénytelen dátum" :
                DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out appointmentTime) ?
                appointmentTime.ToString("yyyy. MM. dd. HH:mm") : "Érvénytelen dátum";

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
                    btnPrev.Location = new Point(panelWidth - 140, panelHeight - 35); // Correct location for Previous button
                }
                else if (control is Label lblPageNumber)
                {
                    lblPageNumber.Location = new Point(panelWidth - 105, panelHeight - 35); // Correct location for Page Number
                }
                else if (control is Button btnNext && btnNext.Text == "▶")
                {
                    btnNext.Location = new Point(panelWidth - 70, panelHeight - 35); // Correct location for Next button
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
                    }
                }
            }
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
            panelTable.Controls.Clear();
            aktivMenu = "Útmutatók";
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
            panelTable.Controls.Clear();
            InitializeTable(hibakodSql);
            aktivMenu= "Hibakódok";
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
            panelTable.Controls.Clear();
            InitializeTable(munkafolySql);
            aktivMenu = "Munkafolyamatok";
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
        "FROM jarmuvek j JOIN tipus t ON j.tipus_id = t.tipus_id " +
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
            if (aktivMenu!=null)
            {
                if (searchQueries.ContainsKey(aktivMenu))
                {   
                    string query = string.Format(searchQueries[aktivMenu], keresettKifejezes);
                    InitializeTable(query);
                }
            }
            
        }

        private void Main_Form_Resize(object sender, EventArgs e)
        {
            table_DGV.Size = new Size(panelMain.Width - 30, panelTable.Height-46);
            ResizeAppointmentCards();
        }
        private void panelTable_Click(object sender, EventArgs e)
        {
            searchBar.Focus();
        }
    }
}