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

        static DatabaseHandler databaseHandler = new DatabaseHandler();

        private Size originalFormSize;
        public Main_Form()
        {
            InitializeComponent();

            hoverPanel1.PanelClicked += HoverPanel1_PanelClicked;
            hoverPanel2.PanelClicked += HoverPanel2_PanelClicked;
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            originalFormSize = this.Size;
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

        private void HoverPanel1_PanelClicked(object sender, EventArgs e)
        {
            if (!panelTable.Controls.Contains(table_DGV))
            {
                ugyfelek_Generate();
            }
            else
            {
                table_DGV.Rows.Clear();
                table_DGV.Columns.Clear();
                ugyfelek_Generate();
            }
        }

        private void HoverPanel2_PanelClicked(object sender, EventArgs e)
        {
            if (!panelTable.Controls.Contains(table_DGV))
            {
                alkatreszek_Generate();
            }
            else
            {
                table_DGV.Rows.Clear();
                table_DGV.Columns.Clear();
                alkatreszek_Generate();
            }
        }

        private void ugyfelek_Generate()
        {
            var (eredmeny, oszlopNevek) = databaseHandler.Select("SELECT * FROM ugyfelek");
            table_DGV.Location = new Point(30, panelSearchBar.Location.Y + 5); //300, 75
            table_DGV.Width = panelTable.Width - 60;
            table_DGV.ColumnHeadersHeight = 40;
            table_DGV.Height = panelTable.Height - 30;
            table_DGV.ForeColor = Color.Black;
            table_DGV.RowHeadersVisible = false;
            table_DGV.Font = new Font("Times New Roman", 14);
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

        private void alkatreszek_Generate()
        {
            var (eredmeny, oszlopNevek) = databaseHandler.Select("SELECT * FROM alkatreszek");
            table_DGV.Location = new Point(30, panelSearchBar.Location.Y + 5); //300, 75
            table_DGV.Width = panelTable.Width - 60;
            table_DGV.ColumnHeadersHeight = 40;
            table_DGV.Height = Convert.ToInt32((panelTable.Height / 1.5));
            table_DGV.ForeColor = Color.Black;
            table_DGV.RowHeadersVisible = false;
            table_DGV.Font = new Font("Times New Roman", 14);
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
