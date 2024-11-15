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
    public partial class Main_Form : Form
    {
        private Rectangle menuPanelOriginalRectangle, mainPanelOriginalRectangle;
        private Rectangle hoverPanel1OriginalRectangle, hoverPanel2OriginalRectangle, hoverPanel3OriginalRectangle, hoverPanel4OriginalRectangle;
        private Rectangle panelTableOriginalRectangle, searchPanelOriginalRectangle;
        private Font label1OriginalFont, label2OriginalFont, label3OriginalFont, label4OriginalFont, LabelUserOriginalFont, searchBarOriginalFont;
        private Rectangle custBtnOriginalRectangle;
        private Rectangle partsBtnOriginalRectangle;
        private Rectangle repairBtnOriginalRectangle;
        private Rectangle mainTxtBoxOriginalRectangle;
        private Rectangle searchTxtBoxOriginalRectangle;
        private Rectangle searchPanelLineOriginalRectangle;
        private Rectangle searchIconOriginalRectangle;
        private Rectangle searchBarOriginalRectangle;
        public string Username { get; set; }
        public string Role { get; set; }
        static DatabaseHandler databaseHandler = new DatabaseHandler();

        private Size originalFormSize;
        public Main_Form()
        {
            InitializeComponent();

            customersPanel.PanelClicked += HoverPanel1_ÜgyfelekClicked;
            partsPanel.PanelClicked += HoverPanel2_AlkatrészekClicked;
            repairsPanel.PanelClicked += HoverPanel4_SzerelésekClicked;
            carsPanel.PanelClicked += HoverPanel5_JárművekClicked;
            logOutPan.PanelClicked += LogOutPan_Clicked;
            
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            originalFormSize = this.Size;
            LabelUser.Text = Username;
            if (Role=="Adminisztrátor")
            {
                PicBoxRole.Image = AutoMuhely.Properties.Resources.admin;
                PicBoxRole.Location = new Point(22, 34);
                settingsPanel.Visible = true;
            }
            else if (Role=="Szerelő")
            {
                PicBoxRole.Image = AutoMuhely.Properties.Resources.user;
                PicBoxRole.Location = new Point(22, 27);
                settingsPanel.Visible=false;
            }
            else
            {
                //MessageBox.Show("Ehhez a szinthez nincs hozzáférése.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            originalFormSize = this.Size;

    // Capture original positions and sizes of panels
        menuPanelOriginalRectangle = new Rectangle(MenuPanel.Location.X, MenuPanel.Location.Y, MenuPanel.Width, MenuPanel.Height);
        mainPanelOriginalRectangle = new Rectangle(MainPanel.Location.X, MainPanel.Location.Y, MainPanel.Width, MainPanel.Height);
            searchBarOriginalRectangle = new Rectangle(searchBar.Location.X, searchBar.Location.Y, searchBar.Width, searchBar.Height);
            hoverPanel1OriginalRectangle = new Rectangle(customersPanel.Location.X, customersPanel.Location.Y, customersPanel.Width, customersPanel.Height);
        hoverPanel2OriginalRectangle = new Rectangle(partsPanel.Location.X, partsPanel.Location.Y, partsPanel.Width, partsPanel.Height);
        hoverPanel3OriginalRectangle = new Rectangle(logOutPan.Location.X, logOutPan.Location.Y, logOutPan.Width, logOutPan.Height);
        hoverPanel4OriginalRectangle = new Rectangle(repairsPanel.Location.X, repairsPanel.Location.Y, repairsPanel.Width, repairsPanel.Height);
    
        panelTableOriginalRectangle = new Rectangle(panelTable.Location.X, panelTable.Location.Y, panelTable.Width, panelTable.Height);
        searchPanelOriginalRectangle = new Rectangle(panelSearchBar.Location.X, panelSearchBar.Location.Y, panelSearchBar.Width, panelSearchBar.Height);
            label1OriginalFont = label1.Font;
            label2OriginalFont = label2.Font;
            label3OriginalFont = label3.Font;
            label4OriginalFont = label4.Font;
            LabelUserOriginalFont = LabelUser.Font;
            searchBarOriginalFont = searchBar.Font;
        }
        private void ResizeControl(Rectangle originalRect, Control control, Font originalFont)
        {
            float xRatio = (float)this.Width / originalFormSize.Width;
            float yRatio = (float)this.Height / originalFormSize.Height;

            int newX = (int)(originalRect.X * xRatio);
            int newY = (int)(originalRect.Y * yRatio);
            int newWidth = (int)(originalRect.Width * xRatio);
            int newHeight = (int)(originalRect.Height * yRatio);

            control.Location = new Point(newX, newY);
            control.Size = new Size(newWidth, newHeight);

            // Adjust font size
            float newFontSize = originalFont.Size * Math.Min(xRatio, yRatio);
            control.Font = new Font(originalFont.FontFamily, newFontSize, originalFont.Style);
        }

        private void Main_Form_Resize(object sender, EventArgs e)
        {
            // Resize panels and fonts
            ResizeControl(mainPanelOriginalRectangle, MainPanel, LabelUserOriginalFont);
            
            ResizeControl(panelTableOriginalRectangle, panelTable, label1OriginalFont);


            ResizeControl(searchPanelOriginalRectangle, panelSearchBar, label1OriginalFont);
            ResizeControl(searchBarOriginalRectangle, searchBar, searchBarOriginalFont);
            /*
            ResizeControl(menuPanelOriginalRectangle, MenuPanel, LabelUserOriginalFont);
            ResizeControl(hoverPanel1OriginalRectangle, hoverPanel1, label1OriginalFont);
            ResizeControl(hoverPanel2OriginalRectangle, hoverPanel2, label2OriginalFont);
            ResizeControl(hoverPanel3OriginalRectangle, hoverPanel3, label3OriginalFont);
            ResizeControl(hoverPanel4OriginalRectangle, hoverPanel4, label4OriginalFont);
            */
        }
        static DataGridView table_DGV = new DataGridView();

        private string aktívMenü;

        private void HoverPanel1_ÜgyfelekClicked(object sender, EventArgs e)
        {
            aktívMenü = "ügyfelek";
            ugyfelek_Generate();
        }

        private void HoverPanel2_AlkatrészekClicked(object sender, EventArgs e)
        {
            aktívMenü = "alkatrészek";
            alkatreszek_Generate();
        }
        private void LogOutPan_Clicked(object sender, EventArgs e)
        {
            // Close the current form
            this.Hide();

            // Open the login form again
            using (LoginForm loginForm = new LoginForm())
            {
                // Show login form as a dialog and check the result
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // If login is successful, update the Username and Role and re-show the main form
                    this.Username = loginForm.Username;
                    this.Role = loginForm.Role;
                    this.Show();
                }
                else
                {
                    // If login is canceled, close the application
                    Application.Exit();
                }
            }
            Main_Form_Load(sender, e);
        }

        private void HoverPanel4_SzerelésekClicked(object sender, EventArgs e)
        {
            szerelések_Generate();
        }

        private void HoverPanel5_JárművekClicked(object sender, EventArgs e)
        {
            aktívMenü = "járművek";
            járművek_Generate();
        }

        Button szerelésiÚtmutatók_Btn;
        Button hibakódok_Btn;
        Button munkafolyamatSablonok_Btn;

        private void szerelések_Generate()
        {
            panelTable.Controls.Clear();

            szerelésiÚtmutatók_Btn_Generate();
            hibakódok_Btn_Generate();
            munkafolyamatSablonok_Btn_Generate();
        }

        private void szerelésiÚtmutatók_Btn_Generate()
        {
            szerelésiÚtmutatók_Btn = new Button();
            szerelésiÚtmutatók_Btn.Location = new Point(20, 405);
            szerelésiÚtmutatók_Btn.Size = new Size(200, 50);
            szerelésiÚtmutatók_Btn.Text = "Szerelési Útmutatók";
            szerelésiÚtmutatók_Btn.Font = new Font("Arial Rounded MT", 10);
            szerelésiÚtmutatók_Btn.BackColor = Color.White;
            szerelésiÚtmutatók_Btn.Click += SzerelésiÚtmutatók_Btn_Click;
            panelTable.Controls.Add(szerelésiÚtmutatók_Btn);
        }

        private void SzerelésiÚtmutatók_Btn_Click(object sender, EventArgs e)
        {
            panelTable.Controls.Clear();
            table_DGV.Columns.Clear();
            table_DGV.Rows.Clear();

            var (eredmeny, oszlopNevek) = databaseHandler.Select("SELECT * FROM szerelesi_utmutatok");
            table_DGV.Location = new Point(30, 5);
            int table_DGV_Width = panelTable.Width - 60;
            int table_DGV_Height = panelTable.Height - 70;
            table_DGV.Size = new Size(table_DGV_Width, table_DGV_Height);
            table_DGV.ColumnHeadersHeight = 40;
            table_DGV.ForeColor = Color.Black;
            table_DGV.RowHeadersVisible = false;
            table_DGV.Font = new Font("Arial Rounded MT", 10);
            panelTable.Controls.Add(table_DGV);
            if (eredmeny != null && oszlopNevek != null)
            {
                foreach (var oszlopNev in oszlopNevek)
                {
                    table_DGV.Columns.Add(oszlopNev, oszlopNev);
                    table_DGV.Columns[oszlopNev].Width = Convert.ToInt32((table_DGV.Width / oszlopNevek.Count));
                }

                foreach (var sor in eredmeny)
                {
                    table_DGV.Rows.Add(sor.ToArray());
                }
            }
            table_DGV.ScrollBars = ScrollBars.None;

            szerelésekVissza_Btn_Generate();

            Button újÚtmutató_Btn = new Button();
            újÚtmutató_Btn.Location = new Point(230, 405);
            újÚtmutató_Btn.Size = new Size(200, 50);
            újÚtmutató_Btn.Text = "Új Útmutató";
            újÚtmutató_Btn.Font = new Font("Arial Rounded MT", 10);
            újÚtmutató_Btn.BackColor = Color.White;
            újÚtmutató_Btn.Click += ÚjÚtmutató_Btn_Click;
            panelTable.Controls.Add(újÚtmutató_Btn);

            Button módosításÚtmutató_Btn = new Button();
            módosításÚtmutató_Btn.Location = new Point(440, 405);
            módosításÚtmutató_Btn.Size = new Size(200, 50);
            módosításÚtmutató_Btn.Text = "Útmutató Módosítása";
            módosításÚtmutató_Btn.Font = new Font("Arial Rounded MT", 10);
            módosításÚtmutató_Btn.BackColor = Color.White;
            módosításÚtmutató_Btn.Click += MódosításÚtmutató_Btn_Click;
            panelTable.Controls.Add(módosításÚtmutató_Btn);
        }

        private void SzerelésiÚtmutatók_Generate()
        {
            table_DGV.Columns.Clear();
            table_DGV.Rows.Clear();

            var (eredmeny, oszlopNevek) = databaseHandler.Select("SELECT * FROM szerelesi_utmutatok");
            table_DGV.Location = new Point(30, 5);
            int table_DGV_Width = panelTable.Width - 60;
            int table_DGV_Height = panelTable.Height - 70;
            table_DGV.Size = new Size(table_DGV_Width, table_DGV_Height);
            table_DGV.ColumnHeadersHeight = 40;
            table_DGV.ForeColor = Color.Black;
            table_DGV.RowHeadersVisible = false;
            table_DGV.Font = new Font("Arial Rounded MT", 10);
            panelTable.Controls.Add(table_DGV);
            if (eredmeny != null && oszlopNevek != null)
            {
                foreach (var oszlopNev in oszlopNevek)
                {
                    table_DGV.Columns.Add(oszlopNev, oszlopNev);
                    table_DGV.Columns[oszlopNev].Width = Convert.ToInt32((table_DGV.Width / oszlopNevek.Count));
                }

                foreach (var sor in eredmeny)
                {
                    table_DGV.Rows.Add(sor.ToArray());
                }
            }
            table_DGV.ScrollBars = ScrollBars.None;
        }

        private void ÚjÚtmutató_Btn_Click(object sender, EventArgs e)
        {
            újÚtmutató újÚtmutató = new újÚtmutató();
            újÚtmutató.ÚtmutatóHozzáadva += újÚtmutató_ÚtmutatóHozzáadva;
            újÚtmutató.Show();
        }

        private void újÚtmutató_ÚtmutatóHozzáadva(object sender, EventArgs e)
        {
            SzerelésiÚtmutatók_Generate();
        }

        private void MódosításÚtmutató_Btn_Click(object sender, EventArgs e)
        {
            módosítÚtmutató módosítÚtmutató = new módosítÚtmutató();
            módosítÚtmutató.ÚtmutatóMódosítva += módosítÚtmutató_ÚtmutatóMódosítva;
            módosítÚtmutató.Show();
        }

        private void módosítÚtmutató_ÚtmutatóMódosítva(object sender, EventArgs e)
        {
            SzerelésiÚtmutatók_Generate();
        }

        private void Hibakódok_Generate()
        {
            table_DGV.Columns.Clear();
            table_DGV.Rows.Clear();

            var (eredmeny, oszlopNevek) = databaseHandler.Select("SELECT * FROM hibakodok");
            table_DGV.Location = new Point(30, 5);
            int table_DGV_Width = panelTable.Width - 60;
            int table_DGV_Height = panelTable.Height - 70;
            table_DGV.Size = new Size(table_DGV_Width, table_DGV_Height);
            table_DGV.ColumnHeadersHeight = 40;
            table_DGV.ForeColor = Color.Black;
            table_DGV.RowHeadersVisible = false;
            table_DGV.Font = new Font("Arial Rounded MT", 10);
            panelTable.Controls.Add(table_DGV);
            if (eredmeny != null && oszlopNevek != null)
            {
                foreach (var oszlopNev in oszlopNevek)
                {
                    table_DGV.Columns.Add(oszlopNev, oszlopNev);
                    table_DGV.Columns[oszlopNev].Width = Convert.ToInt32((table_DGV.Width / oszlopNevek.Count));
                }

                foreach (var sor in eredmeny)
                {
                    table_DGV.Rows.Add(sor.ToArray());
                }
            }
            table_DGV.ScrollBars = ScrollBars.None;
        }

        private void hibakódok_Btn_Generate()
        {
            hibakódok_Btn = new Button();
            hibakódok_Btn.Location = new Point(230, 405);
            hibakódok_Btn.Size = new Size(200, 50);
            hibakódok_Btn.Text = "Hibakódok";
            hibakódok_Btn.Font = new Font("Arial Rounded MT", 10);
            hibakódok_Btn.BackColor = Color.White;
            hibakódok_Btn.Click += Hibakódok_Btn_Click;
            panelTable.Controls.Add(hibakódok_Btn);
        }

        private void Hibakódok_Btn_Click(object sender, EventArgs e)
        {
            panelTable.Controls.Clear();

            Hibakódok_Generate();

            szerelésekVissza_Btn_Generate();

            Button újHibakód_Btn = new Button();
            újHibakód_Btn.Location = new Point(230, 405);
            újHibakód_Btn.Size = new Size(200, 50);
            újHibakód_Btn.Text = "Új Hibakód";
            újHibakód_Btn.Font = new Font("Arial Rounded MT", 10);
            újHibakód_Btn.BackColor = Color.White;
            újHibakód_Btn.Click += ÚjHibakód_Btn_Click;
            panelTable.Controls.Add(újHibakód_Btn);

            Button módosításHibakód_Btn = new Button();
            módosításHibakód_Btn.Location = new Point(440, 405);
            módosításHibakód_Btn.Size = new Size(200, 50);
            módosításHibakód_Btn.Text = "Hibakód Módosítása";
            módosításHibakód_Btn.Font = new Font("Arial Rounded MT", 10);
            módosításHibakód_Btn.BackColor = Color.White;
            módosításHibakód_Btn.Click += MódosítHibakód_Btn_Click;
            panelTable.Controls.Add(módosításHibakód_Btn);
        }

        private void ÚjHibakód_Btn_Click(object sender, EventArgs e)
        {
            újHibakód újHibakód = new újHibakód();
            újHibakód.ÚjHibakód_HibakódHozzáadva += újHibakód_HibakódHozzáad;
            újHibakód.Show();
        }

        private void újHibakód_HibakódHozzáad(object sender, EventArgs e)
        {
            Hibakódok_Generate();
        }

        private void MódosítHibakód_Btn_Click(object sender, EventArgs e)
        {
            módosítHibakód módosítHibakód = new módosítHibakód();
            módosítHibakód.MódosítHibakód += MódosítHibakód;
            módosítHibakód.Show();
        }

        private void MódosítHibakód(object sender, EventArgs e)
        {
            Hibakódok_Generate();
        }

        private void munkafolyamatSablonok_Btn_Generate()
        {
            munkafolyamatSablonok_Btn = new Button();
            munkafolyamatSablonok_Btn.Location = new Point(440, 405);
            munkafolyamatSablonok_Btn.Size = new Size(200, 50);
            munkafolyamatSablonok_Btn.Text = "Munkafolyamat Sablonok";
            munkafolyamatSablonok_Btn.Font = new Font("Arial Rounded MT", 10);
            munkafolyamatSablonok_Btn.BackColor = Color.White;
            munkafolyamatSablonok_Btn.Click += MunkafolyamatSablonok_Btn_Click;
            panelTable.Controls.Add(munkafolyamatSablonok_Btn);
        }

        private void MunkafolyamatSablonok_Btn_Click(object sender, EventArgs e)
        {
            panelTable.Controls.Clear();
            table_DGV.Columns.Clear();
            table_DGV.Rows.Clear();

            var (eredmeny, oszlopNevek) = databaseHandler.Select("SELECT * FROM munkafolyamat_sablonok");
            table_DGV.Location = new Point(30, 5);
            int table_DGV_Width = panelTable.Width - 60;
            int table_DGV_Height = panelTable.Height - 70;
            table_DGV.Size = new Size(table_DGV_Width, table_DGV_Height);
            table_DGV.ColumnHeadersHeight = 40;
            table_DGV.ForeColor = Color.Black;
            table_DGV.RowHeadersVisible = false;
            table_DGV.Font = new Font("Arial Rounded MT", 10);
            panelTable.Controls.Add(table_DGV);
            if (eredmeny != null && oszlopNevek != null)
            {
                foreach (var oszlopNev in oszlopNevek)
                {
                    table_DGV.Columns.Add(oszlopNev, oszlopNev);
                    table_DGV.Columns[oszlopNev].Width = Convert.ToInt32((table_DGV.Width / oszlopNevek.Count));
                }

                foreach (var sor in eredmeny)
                {
                    table_DGV.Rows.Add(sor.ToArray());
                }
            }
            table_DGV.ScrollBars = ScrollBars.None;

            szerelésekVissza_Btn_Generate();

            Button újMunkafolyamat_Btn = new Button();
            újMunkafolyamat_Btn.Location = new Point(230, 405);
            újMunkafolyamat_Btn.Size = new Size(200, 50);
            újMunkafolyamat_Btn.Text = "Új Munkafolyamat";
            újMunkafolyamat_Btn.Font = new Font("Arial Rounded MT", 10);
            újMunkafolyamat_Btn.BackColor = Color.White;
            újMunkafolyamat_Btn.Click += újMunkafolyamat_Btn_Click;
            panelTable.Controls.Add(újMunkafolyamat_Btn);

            Button módosításMunkafolyamat_Btn = new Button();
            módosításMunkafolyamat_Btn.Location = new Point(440, 405);
            módosításMunkafolyamat_Btn.Size = new Size(200, 50);
            módosításMunkafolyamat_Btn.Text = "Munkafolyamat Módosítása";
            módosításMunkafolyamat_Btn.Font = new Font("Arial Rounded MT", 10);
            módosításMunkafolyamat_Btn.BackColor = Color.White;
            módosításMunkafolyamat_Btn.Click += módosításMunkafolyamat_Btn_Click;
            panelTable.Controls.Add(módosításMunkafolyamat_Btn);
        }

        private void újMunkafolyamat_Btn_Click(object sender, EventArgs e)
        {
            /*újMunkafolyamat újMunkafolyamat= new újMunkafolyamat();
            újMunkafolyamat.újMunkafolyamatHozzáadva += újMunkafolyamatHozzáadva;
            újMunkafolyamat.Show();*/
        }

        private void módosításMunkafolyamat_Btn_Click(object sender, EventArgs e)
        {
            /*módosítMunkafolyamat módosítMunkafolyamat = new módosítMunkafolyamat();
            módosítMunkafolyamat.módosítMunkafolyamatMódosítva += módosítMunkafolyamatMódosítva;
            módosítMunkafolyamat.Show();*/
        }

        Button szerelésekVissza_Btn;

        private void szerelésekVissza_Btn_Generate()
        {
            szerelésekVissza_Btn = new Button();
            szerelésekVissza_Btn.Location = new Point(20, 405);
            szerelésekVissza_Btn.Size = new Size(200, 50);
            szerelésekVissza_Btn.Text = "Vissza";
            szerelésekVissza_Btn.Font = new Font("Arial Rounded MT", 10);
            szerelésekVissza_Btn.BackColor = Color.White;
            szerelésekVissza_Btn.Click += szerelésekVissza_Btn_Click;
            panelTable.Controls.Add(szerelésekVissza_Btn);
        }

        private void szerelésekVissza_Btn_Click(object sender, EventArgs e)
        {
            panelTable.Controls.Clear();
            szerelések_Generate();
        }

        private void járművek_Generate()
        {
            panelTable.Controls.Clear();
            table_DGV.Columns.Clear();
            table_DGV.Rows.Clear();

            var (eredmeny, oszlopNevek) = databaseHandler.Select("SELECT * FROM jarmuvek");
            table_DGV.Location = new Point(30, 5);
            int table_DGV_Width = panelTable.Width - 60;
            int table_DGV_Height = panelTable.Height - 30;
            table_DGV.Size = new Size(table_DGV_Width, table_DGV_Height);
            table_DGV.ColumnHeadersHeight = 40;
            table_DGV.ForeColor = Color.Black;
            table_DGV.RowHeadersVisible = false;
            table_DGV.Font = new Font("Arial Rounded MT", 10);
            panelTable.Controls.Add(table_DGV);
            if (eredmeny != null && oszlopNevek != null)
            {
                foreach (var oszlopNev in oszlopNevek)
                {
                    table_DGV.Columns.Add(oszlopNev, oszlopNev);
                    table_DGV.Columns[oszlopNev].Width = Convert.ToInt32((table_DGV.Width / oszlopNevek.Count) + 0.5);
                }

                foreach (var sor in eredmeny)
                {
                    table_DGV.Rows.Add(sor.ToArray());
                }
            }
        }

        private void ugyfelek_Generate()
        {
            panelTable.Controls.Clear();
            table_DGV.Columns.Clear();
            table_DGV.Rows.Clear();
            var (eredmeny, oszlopNevek) = databaseHandler.Select("SELECT nev AS Név, elerhetoseg AS Elérhetőség, cim AS Cím FROM ugyfelek");
            table_DGV.Location = new Point(30, 5);
            int table_DGV_Width = panelTable.Width - 60;
            int table_DGV_Height = panelTable.Height - 30;
            table_DGV.Size = new Size(table_DGV_Width, table_DGV_Height);
            table_DGV.ColumnHeadersHeight = 40;
            table_DGV.ForeColor = Color.Black;
            table_DGV.RowHeadersVisible = false;
            table_DGV.Font = new Font("Arial Rounded MT", 14);
            panelTable.Controls.Add(table_DGV);
            if (eredmeny != null && oszlopNevek != null)
            {
                foreach (var oszlopNev in oszlopNevek)
                {
                    table_DGV.Columns.Add(oszlopNev, oszlopNev);
                    table_DGV.Columns[oszlopNev].Width = (table_DGV.Width / oszlopNevek.Count) - 1;
                }

                foreach (var sor in eredmeny)
                {
                    table_DGV.Rows.Add(sor.ToArray());
                }
            }
        }

        UserControl alkatrészekMenü;

        private void alkatreszek_Generate()
        {
            panelTable.Controls.Clear();
            table_DGV.Columns.Clear();
            table_DGV.Rows.Clear();
            var (eredmeny, oszlopNevek) = databaseHandler.Select("SELECT nev AS Alkatrész, leiras AS Leírás, keszlet_mennyiseg AS készlet, utanrendelesi_szint AS 'Utánrendelési szint' FROM alkatreszek");
            table_DGV.Location = new Point(30, 5);
            int table_DGV_Width = panelTable.Width - 60;
            int table_DGV_Height = Convert.ToInt32(panelTable.Height / 1.5);
            table_DGV.Size = new Size(table_DGV_Width, table_DGV_Height);
            table_DGV.ColumnHeadersHeight = 40;
            table_DGV.ForeColor = Color.Black;
            table_DGV.RowHeadersVisible = false;
            table_DGV.Font = new Font("Arial Rounded MT", 12);
            panelTable.Controls.Add(table_DGV);
            if (eredmeny != null && oszlopNevek != null)
            {
                foreach (var oszlopNev in oszlopNevek)
                {
                    table_DGV.Columns.Add(oszlopNev, oszlopNev);
                    table_DGV.Columns[oszlopNev].Width = (table_DGV.Width / oszlopNevek.Count) - 1;
                }

                foreach (var sor in eredmeny)
                {
                    table_DGV.Rows.Add(sor.ToArray());
                }
            }

            alkatrészekMenü = new UserControl();
            alkatrészekMenü.Location = new Point(30, table_DGV.Location.Y + table_DGV.Height + 5);
            int alkatrészekMenü_Width = panelTable.Width - 60;
            int alkatrészekMenü_Height = panelTable.Height - table_DGV.Height - 30;
            alkatrészekMenü.MinimumSize = new Size(alkatrészekMenü_Width, alkatrészekMenü_Height);
            panelTable.Controls.Add(alkatrészekMenü);

            Size gombMéret = new Size(alkatrészekMenü.Width / 3 - 30, 75);

            Button újAlkatrész = new Button();
            újAlkatrész.Text = "Új Alkatrész";
            újAlkatrész.Size = gombMéret;
            újAlkatrész.Location = new Point(0, alkatrészekMenü_Height / 2 - 30);
            újAlkatrész.BackColor = Color.White;
            újAlkatrész.Font = new Font("Arial Rounded MT", 12);
            újAlkatrész.Click += újAlkatrész_Click;
            alkatrészekMenü.Controls.Add(újAlkatrész);
        }

        private void újAlkatrész_Click(object sender, EventArgs e)
        {
            alkatrészInputs alkatrészInputs = new alkatrészInputs();
            alkatrészInputs.AlkatrészHozzáadva += AlkatrészInputs_AlkatrészHozzáadva;
            alkatrészInputs.Show();
        }

        private void AlkatrészInputs_AlkatrészHozzáadva(object sender, EventArgs e)
        {
            alkatreszek_Generate();
        }

        private Dictionary<string, string> searchQueries = new Dictionary<string, string>
        {
            { "ügyfelek", "SELECT * FROM ugyfelek WHERE nev LIKE '%{0}%' OR elerhetoseg LIKE '%{0}%' OR cim LIKE '%{0}%'" },
            { "alkatrészek", "SELECT * FROM alkatreszek WHERE nev LIKE '%{0}%' OR leiras LIKE '%{0}%' OR keszlet_mennyiseg LIKE '%{0}%' OR utanrendelesi_szint LIKE '%{0}%'" },
            //{ "szerelesek", "SELECT * FROM szerelesek WHERE nev LIKE '%{0}%'" }
            { "járművek", "SELECT jarmuvek.jarmu_id, jarmuvek.rendszam, jarmuvek.tipus_id, jarmuvek.kod_id, jarmuvek.sablon_id, jarmuvek.gyartas_eve, jarmuvek.motor_adatok, jarmuvek.alvaz_adatok, jarmuvek.elozo_javitasok, tipus.tipus, hibakodok.kod, munkafolyamat_sablonok.nev FROM jarmuvek AS j INNER JOIN tipus AS t ON j.tipus_id = t.tipus_id INNER JOIN hibakodok AS k ON j.kod_id = k.kod_id INNER JOIN munkafolyamat_sablonok AS s ON j.sablon_id = s.sablon_id;" }
        };

        private void searchBar_TextChanged(object sender, EventArgs e)
        {
            string keresettKifejezes = searchBar.Text.Trim();

            if (searchQueries.ContainsKey(aktívMenü))
            {
                string query = string.Format(searchQueries[aktívMenü], keresettKifejezes);

                var (eredmeny, oszlopNevek) = databaseHandler.Select(query);

                table_DGV.Rows.Clear();

                if (eredmeny != null && oszlopNevek != null)
                {
                    foreach (var sor in eredmeny)
                    {
                        table_DGV.Rows.Add(sor.ToArray());
                    }
                }
            }
        }
    }
}
