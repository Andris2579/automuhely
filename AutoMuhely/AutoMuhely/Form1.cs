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

            hoverPanel1.PanelClicked += HoverPanel1_ÜgyfelekClicked;
            hoverPanel2.PanelClicked += HoverPanel2_AlkatrészekClicked;
            LogOutPan.PanelClicked += LogOutPan_Clicked;
            
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            originalFormSize = this.Size;
            LabelUser.Text = Username;
            if (Role=="Adminisztrátor")
            {
                PicBoxRole.Image = AutoMuhely.Properties.Resources.admin;
                PicBoxRole.Location = new Point(18, 21);
            }
            else if (Role=="Szerelő")
            {
                PicBoxRole.Image = AutoMuhely.Properties.Resources.user;
                PicBoxRole.Location = new Point(18, 16);
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
            hoverPanel1OriginalRectangle = new Rectangle(hoverPanel1.Location.X, hoverPanel1.Location.Y, hoverPanel1.Width, hoverPanel1.Height);
        hoverPanel2OriginalRectangle = new Rectangle(hoverPanel2.Location.X, hoverPanel2.Location.Y, hoverPanel2.Width, hoverPanel2.Height);
        hoverPanel3OriginalRectangle = new Rectangle(LogOutPan.Location.X, LogOutPan.Location.Y, LogOutPan.Width, LogOutPan.Height);
        hoverPanel4OriginalRectangle = new Rectangle(hoverPanel4.Location.X, hoverPanel4.Location.Y, hoverPanel4.Width, hoverPanel4.Height);
    
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

        private void HoverPanel1_ÜgyfelekClicked(object sender, EventArgs e)
        {
            ugyfelek_Generate();
        }

        private void HoverPanel2_AlkatrészekClicked(object sender, EventArgs e)
        {
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

            Button törlésAlkatrész = new Button();
            törlésAlkatrész.Text = "Alkatrész törlés";
            törlésAlkatrész.Size = gombMéret ;
            törlésAlkatrész.Location = new Point(újAlkatrész.Location.X + újAlkatrész.Width + Convert.ToInt32(gombMéret.Width / 3), újAlkatrész.Location.Y);
            törlésAlkatrész.BackColor = Color.White;
            újAlkatrész.Font = new Font("Arial Rounded MT", 12);
            alkatrészekMenü.Controls.Add(törlésAlkatrész);
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

        private void searchBar_TextChanged(object sender, EventArgs e)
        {
            string keresettKifejezes = searchBar.Text.Trim();

            string query = $"SELECT * FROM ugyfelek WHERE nev LIKE '%{keresettKifejezes}%'";

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
