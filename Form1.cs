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

        private Size originalFormSize;
        public Main_Form()
        {
            InitializeComponent();
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            originalFormSize = this.Size;
            custBtnOriginalRectangle = new Rectangle(CustBtn.Location.X, CustBtn.Location.Y, CustBtn.Width, CustBtn.Height);
            partsBtnOriginalRectangle = new Rectangle(PartsBtn.Location.X, PartsBtn.Location.Y, PartsBtn.Width, PartsBtn.Height);
            repairBtnOriginalRectangle = new Rectangle(RepairBtn.Location.X, RepairBtn.Location.Y, RepairBtn.Width, RepairBtn.Height);
            menuPanelOriginalRectangle = new Rectangle(MenuPanel.Location.X, MenuPanel.Location.Y, MenuPanel.Width, MenuPanel.Height);
            mainPanelOriginalRectangle= new Rectangle(MainPanel.Location.X, MainPanel.Location.Y, MainPanel.Width, MainPanel.Height);
            searchPanelOriginalRectangle= new Rectangle(SearchPanel.Location.X, SearchPanel.Location.Y, SearchPanel.Width, SearchPanel.Height);
            mainTxtBoxOriginalRectangle = new Rectangle(MainTxtBox.Location.X, MainTxtBox.Location.Y, MainTxtBox.Width, MainTxtBox.Height);
            searchTxtBoxOriginalRectangle = new Rectangle(SearchTxtBox.Location.X, SearchTxtBox.Location.Y, SearchTxtBox.Width, SearchTxtBox.Height);
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
            ResizeControl(custBtnOriginalRectangle, CustBtn);
            ResizeControl(partsBtnOriginalRectangle, PartsBtn);
            ResizeControl(repairBtnOriginalRectangle, RepairBtn);
            ResizeControl(menuPanelOriginalRectangle, MenuPanel);
            ResizeControl(mainPanelOriginalRectangle, MainPanel);
            ResizeControl(searchPanelOriginalRectangle, SearchPanel);
            ResizeControl(mainTxtBoxOriginalRectangle, MainTxtBox);
            ResizeControl(searchTxtBoxOriginalRectangle, SearchTxtBox);
        }

        private void CustBtn_Click(object sender, EventArgs e)
        {

        }

        private void PartsBtn_Click(object sender, EventArgs e)
        {

        }

        private void RepairBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
