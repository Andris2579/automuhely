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
        static DatabaseHandler databaseHandler = new DatabaseHandler();

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

        private Size originalFormSize;
        public Main_Form()
        {
            InitializeComponent();
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

        DataGridView ugyfelek_DGV = new DataGridView();

        private void hoverPanel1_Paint(object sender, PaintEventArgs e)
        {
            ugyfelek_DGV.Location = new Point(300, 75); //300, 75
            ugyfelek_DGV.Size = new Size(620, 450);
            ugyfelek_DGV.ForeColor = Color.White;
            ugyfelek_DGV.Font = new Font("Times New Roman", 14);
            var (eredmeny, oszlopNevek) = databaseHandler.Select("SELECT * FROM ugyfelek");
            if (eredmeny != null && oszlopNevek != null)
            {
                foreach (var oszlopNev in oszlopNevek)
                {
                    ugyfelek_DGV.Columns.Add(oszlopNev, oszlopNev);
                }

                foreach (var sor in eredmeny)
                {
                    ugyfelek_DGV.Rows.Add(sor.ToArray());
                }
            }

            MainPanel.Controls.Add(ugyfelek_DGV);
        }
    }
}
