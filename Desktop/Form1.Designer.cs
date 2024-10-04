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
            this.MenuPanel.Size = new System.Drawing.Size(200, 561);
            this.MenuPanel.TabIndex = 1;
            // 
            // RepairBtn
            // 
            this.RepairBtn.Location = new System.Drawing.Point(22, 351);
            this.RepairBtn.Name = "RepairBtn";
            this.RepairBtn.Size = new System.Drawing.Size(160, 73);
            this.RepairBtn.TabIndex = 2;
            this.RepairBtn.Text = "Szerelések";
            this.RepairBtn.UseVisualStyleBackColor = true;
            // 
            // PartsBtn
            // 
            this.PartsBtn.Location = new System.Drawing.Point(22, 239);
            this.PartsBtn.Name = "PartsBtn";
            this.PartsBtn.Size = new System.Drawing.Size(160, 73);
            this.PartsBtn.TabIndex = 1;
            this.PartsBtn.Text = "Alkatrészek";
            this.PartsBtn.UseVisualStyleBackColor = true;
            // 
            // CustBtn
            // 
            this.CustBtn.Location = new System.Drawing.Point(22, 128);
            this.CustBtn.Name = "CustBtn";
            this.CustBtn.Size = new System.Drawing.Size(160, 73);
            this.CustBtn.TabIndex = 0;
            this.CustBtn.Text = "Ügyfelek";
            this.CustBtn.UseVisualStyleBackColor = true;
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.AutoSize = true;
            this.MainPanel.Controls.Add(this.MainTxtBox);
            this.MainPanel.Controls.Add(this.SearchPanel);
            this.MainPanel.Location = new System.Drawing.Point(200, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(734, 561);
            this.MainPanel.TabIndex = 2;
            // 
            // MainTxtBox
            // 
            this.MainTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainTxtBox.Location = new System.Drawing.Point(118, 147);
            this.MainTxtBox.Multiline = true;
            this.MainTxtBox.Name = "MainTxtBox";
            this.MainTxtBox.Size = new System.Drawing.Size(512, 367);
            this.MainTxtBox.TabIndex = 1;
            // 
            // SearchPanel
            // 
            this.SearchPanel.Controls.Add(this.SearchTxtBox);
            this.SearchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SearchPanel.Location = new System.Drawing.Point(0, 0);
            this.SearchPanel.Name = "SearchPanel";
            this.SearchPanel.Size = new System.Drawing.Size(734, 100);
            this.SearchPanel.TabIndex = 0;
            // 
            // SearchTxtBox
            // 
            this.SearchTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchTxtBox.Location = new System.Drawing.Point(11, 31);
            this.SearchTxtBox.Name = "SearchTxtBox";
            this.SearchTxtBox.Size = new System.Drawing.Size(711, 42);
            this.SearchTxtBox.TabIndex = 0;
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 35F);
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
            this.MenuPanel.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.SearchPanel.ResumeLayout(false);
            this.SearchPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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

