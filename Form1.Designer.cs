namespace AutoMuhely
{
    partial class Main_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MenuPanel = new System.Windows.Forms.Panel();
            this.RepairBtn = new System.Windows.Forms.Button();
            this.PartsBtn = new System.Windows.Forms.Button();
            this.CustBtn = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.MainTxtBox = new System.Windows.Forms.TextBox();
            this.SearchPanel = new System.Windows.Forms.Panel();
            this.SearchTxtBox = new System.Windows.Forms.TextBox();
            this.MenuPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.SearchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuPanel
            // 
            this.MenuPanel.Controls.Add(this.RepairBtn);
            this.MenuPanel.Controls.Add(this.PartsBtn);
            this.MenuPanel.Controls.Add(this.CustBtn);
            this.MenuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.MenuPanel.Location = new System.Drawing.Point(0, 0);
            this.MenuPanel.Name = "MenuPanel";
            this.MenuPanel.Size = new System.Drawing.Size(173, 561);
            this.MenuPanel.TabIndex = 1;
            // 
            // RepairBtn
            // 
            this.RepairBtn.Location = new System.Drawing.Point(12, 344);
            this.RepairBtn.Name = "RepairBtn";
            this.RepairBtn.Size = new System.Drawing.Size(146, 50);
            this.RepairBtn.TabIndex = 2;
            this.RepairBtn.Text = "Szerelések";
            this.RepairBtn.UseVisualStyleBackColor = true;
            this.RepairBtn.Click += new System.EventHandler(this.RepairBtn_Click);
            // 
            // PartsBtn
            // 
            this.PartsBtn.Location = new System.Drawing.Point(12, 245);
            this.PartsBtn.Name = "PartsBtn";
            this.PartsBtn.Size = new System.Drawing.Size(146, 50);
            this.PartsBtn.TabIndex = 1;
            this.PartsBtn.Text = "Alkatrészek";
            this.PartsBtn.UseVisualStyleBackColor = true;
            this.PartsBtn.Click += new System.EventHandler(this.PartsBtn_Click);
            // 
            // CustBtn
            // 
            this.CustBtn.Location = new System.Drawing.Point(12, 147);
            this.CustBtn.Name = "CustBtn";
            this.CustBtn.Size = new System.Drawing.Size(146, 50);
            this.CustBtn.TabIndex = 0;
            this.CustBtn.Text = "Ügyfelek";
            this.CustBtn.UseVisualStyleBackColor = true;
            this.CustBtn.Click += new System.EventHandler(this.CustBtn_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.MainPanel.Controls.Add(this.MainTxtBox);
            this.MainPanel.Controls.Add(this.SearchPanel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(173, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(761, 561);
            this.MainPanel.TabIndex = 2;
            // 
            // MainTxtBox
            // 
            this.MainTxtBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.MainTxtBox.Location = new System.Drawing.Point(132, 147);
            this.MainTxtBox.Multiline = true;
            this.MainTxtBox.Name = "MainTxtBox";
            this.MainTxtBox.Size = new System.Drawing.Size(512, 367);
            this.MainTxtBox.TabIndex = 1;
            // 
            // SearchPanel
            // 
            this.SearchPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.SearchPanel.Controls.Add(this.SearchTxtBox);
            this.SearchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SearchPanel.Location = new System.Drawing.Point(0, 0);
            this.SearchPanel.Name = "SearchPanel";
            this.SearchPanel.Size = new System.Drawing.Size(761, 100);
            this.SearchPanel.TabIndex = 0;
            // 
            // SearchTxtBox
            // 
            this.SearchTxtBox.Location = new System.Drawing.Point(11, 31);
            this.SearchTxtBox.Name = "SearchTxtBox";
            this.SearchTxtBox.Size = new System.Drawing.Size(738, 35);
            this.SearchTxtBox.TabIndex = 0;
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(934, 561);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.MenuPanel);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MinimumSize = new System.Drawing.Size(950, 600);
            this.Name = "Main_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Autóműhley Karbantartás";
            this.Load += new System.EventHandler(this.Main_Form_Load);
            this.Resize += new System.EventHandler(this.Main_Form_Resize);
            this.MenuPanel.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.SearchPanel.ResumeLayout(false);
            this.SearchPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MenuPanel;
        private System.Windows.Forms.Button CustBtn;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel SearchPanel;
        private System.Windows.Forms.Button RepairBtn;
        private System.Windows.Forms.Button PartsBtn;
        private System.Windows.Forms.TextBox MainTxtBox;
        private System.Windows.Forms.TextBox SearchTxtBox;
    }
}

