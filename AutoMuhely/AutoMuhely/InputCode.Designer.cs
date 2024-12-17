namespace AutoMuhely
{
    partial class InputCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputCode));
            this.label1 = new System.Windows.Forms.Label();
            this.editLabel = new System.Windows.Forms.Label();
            this.alkatrészNév_Lb = new System.Windows.Forms.Label();
            this.txtHibakod = new System.Windows.Forms.TextBox();
            this.alkatrészLeírás_Lb = new System.Windows.Forms.Label();
            this.txtHibakodLeiras = new System.Windows.Forms.TextBox();
            this.btnAdd = new AutoMuhely.CustomButton();
            this.btnCancel = new AutoMuhely.CustomButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Bauhaus 93", 29.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.label1.Location = new System.Drawing.Point(90, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 44);
            this.label1.TabIndex = 24;
            this.label1.Text = "HIBAKÓD";
            // 
            // editLabel
            // 
            this.editLabel.AutoSize = true;
            this.editLabel.BackColor = System.Drawing.Color.Transparent;
            this.editLabel.Font = new System.Drawing.Font("Bauhaus 93", 29.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.editLabel.Location = new System.Drawing.Point(47, 171);
            this.editLabel.Name = "editLabel";
            this.editLabel.Size = new System.Drawing.Size(259, 44);
            this.editLabel.TabIndex = 23;
            this.editLabel.Text = "MÓDOSÍTÁSA";
            // 
            // alkatrészNév_Lb
            // 
            this.alkatrészNév_Lb.Font = new System.Drawing.Font("Open Sans", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alkatrészNév_Lb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.alkatrészNév_Lb.Location = new System.Drawing.Point(49, 215);
            this.alkatrészNév_Lb.Name = "alkatrészNév_Lb";
            this.alkatrészNév_Lb.Size = new System.Drawing.Size(250, 25);
            this.alkatrészNév_Lb.TabIndex = 14;
            this.alkatrészNév_Lb.Text = "Hibakód";
            // 
            // txtHibakod
            // 
            this.txtHibakod.Location = new System.Drawing.Point(49, 243);
            this.txtHibakod.MaxLength = 100;
            this.txtHibakod.Name = "txtHibakod";
            this.txtHibakod.Size = new System.Drawing.Size(250, 23);
            this.txtHibakod.TabIndex = 15;
            // 
            // alkatrészLeírás_Lb
            // 
            this.alkatrészLeírás_Lb.Font = new System.Drawing.Font("Open Sans", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alkatrészLeírás_Lb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.alkatrészLeírás_Lb.Location = new System.Drawing.Point(49, 284);
            this.alkatrészLeírás_Lb.Name = "alkatrészLeírás_Lb";
            this.alkatrészLeírás_Lb.Size = new System.Drawing.Size(250, 25);
            this.alkatrészLeírás_Lb.TabIndex = 16;
            this.alkatrészLeírás_Lb.Text = "Hibakód leírása";
            // 
            // txtHibakodLeiras
            // 
            this.txtHibakodLeiras.Location = new System.Drawing.Point(49, 312);
            this.txtHibakodLeiras.Multiline = true;
            this.txtHibakodLeiras.Name = "txtHibakodLeiras";
            this.txtHibakodLeiras.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtHibakodLeiras.Size = new System.Drawing.Size(250, 100);
            this.txtHibakodLeiras.TabIndex = 17;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.btnAdd.BorderRadius = 23;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(71, 444);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(210, 49);
            this.btnAdd.TabIndex = 20;
            this.btnAdd.Text = "Tovább";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.btnCancel.BorderRadius = 23;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(105, 499);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 40);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Mégse";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::AutoMuhely.Properties.Resources.logoX;
            this.pictureBox1.Location = new System.Drawing.Point(125, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // újHibakód
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(354, 577);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.alkatrészNév_Lb);
            this.Controls.Add(this.txtHibakod);
            this.Controls.Add(this.alkatrészLeírás_Lb);
            this.Controls.Add(this.txtHibakodLeiras);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(370, 616);
            this.MinimumSize = new System.Drawing.Size(370, 616);
            this.Name = "újHibakód";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hibakód";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.újHibakód_FormClosed);
            this.Load += new System.EventHandler(this.újHibakód_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label editLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private CustomButton btnAdd;
        private CustomButton btnCancel;
        private System.Windows.Forms.Label alkatrészNév_Lb;
        private System.Windows.Forms.TextBox txtHibakod;
        private System.Windows.Forms.Label alkatrészLeírás_Lb;
        private System.Windows.Forms.TextBox txtHibakodLeiras;
    }
}