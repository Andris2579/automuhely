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
        private Rectangle custBtnOriginalRectangle;
        private Rectangle partsBtnOriginalRectangle;
        private Rectangle repairBtnOriginalRectangle;
        private Rectangle menuPanelOriginalRectangle;
        private Rectangle mainPanelOriginalRectangle;
        private Rectangle searchPanelOriginalRectangle;
        private Rectangle mainTxtBoxOriginalRectangle;
        private Rectangle searchTxtBoxOriginalRectangle;
        private Rectangle searchPanelLineOriginalRectangle;
        private Rectangle searchIconOriginalRectangle;
        public string Username { get; set; }
        public string Role { get; set; }
        static DatabaseHandler databaseHandler = new DatabaseHandler();

        private Size originalFormSize;
        public Main_Form()
        {
            InitializeComponent();

            hoverPanel1.PanelClicked += HoverPanel1_ÜgyfelekClicked;
            hoverPanel2.PanelClicked += HoverPanel2_AlkatrészekClicked;
            
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            originalFormSize = this.Size;
            userTextBox.Text = Username + "\r\n" + Role;
        }
        private void ResizeControl(Rectangle r, Control c)
        {
            float xRatio = (float)this.Width / originalFormSize.Width;
            float yRatio = (float)this.Height / originalFormSize.Height;

            int newX = (int)(r.X * xRatio);
            int newY = (int)(r.Y * yRatio);
            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);

            c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);
        }

        private void Main_Form_Resize(object sender, EventArgs e)
        {
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

        private void ugyfelek_Generate()
        {
            panelTable.Controls.Clear();
            table_DGV.Columns.Clear();
            table_DGV.Rows.Clear();
            var (eredmeny, oszlopNevek) = databaseHandler.Select("SELECT * FROM ugyfelek");
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
            var (eredmeny, oszlopNevek) = databaseHandler.Select("SELECT * FROM alkatreszek");
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
