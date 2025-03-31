using System.Drawing;
using System.Windows.Forms;

namespace AutoMuhely
{
    partial class UpdateAppointment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateAppointment));
            this.label5 = new System.Windows.Forms.Label();
            this.LoginLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TimePicker = new System.Windows.Forms.MaskedTextBox();
            this.StatusPicker = new AutoMuhely.CustomComboBox();
            this.btnAdd = new AutoMuhely.CustomButton();
            this.btnCancel = new AutoMuhely.CustomButton();
            this.DatePicker = new AutoMuhely.CustomDateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Bauhaus 93", 29.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.label5.Location = new System.Drawing.Point(96, 158);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 44);
            this.label5.TabIndex = 39;
            this.label5.Text = "KEZELÉSE";
            // 
            // LoginLabel
            // 
            this.LoginLabel.AutoSize = true;
            this.LoginLabel.BackColor = System.Drawing.Color.Transparent;
            this.LoginLabel.Font = new System.Drawing.Font("Bauhaus 93", 29.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.LoginLabel.Location = new System.Drawing.Point(91, 114);
            this.LoginLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(187, 44);
            this.LoginLabel.TabIndex = 38;
            this.LoginLabel.Text = "FOGLALÁS";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::AutoMuhely.Properties.Resources.logoX;
            this.pictureBox1.Location = new System.Drawing.Point(125, -6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 40;
            this.pictureBox1.TabStop = false;
            // 
            // TimePicker
            // 
            this.TimePicker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(81)))));
            this.TimePicker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimePicker.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.TimePicker.Location = new System.Drawing.Point(239, 224);
            this.TimePicker.Mask = "00:00";
            this.TimePicker.Name = "TimePicker";
            this.TimePicker.Size = new System.Drawing.Size(59, 31);
            this.TimePicker.TabIndex = 45;
            // 
            // StatusPicker
            // 
            this.StatusPicker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(81)))));
            this.StatusPicker.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(88)))), ((int)(((byte)(92)))));
            this.StatusPicker.BorderThickness = 2;
            this.StatusPicker.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.StatusPicker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StatusPicker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StatusPicker.Font = new System.Drawing.Font("Open Sans Condensed", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusPicker.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.StatusPicker.FormattingEnabled = true;
            this.StatusPicker.Items.AddRange(new object[] {
            "Foglalt",
            "Folyamatban",
            "Befejezett",
            "Lemondva"});
            this.StatusPicker.Location = new System.Drawing.Point(71, 225);
            this.StatusPicker.Margin = new System.Windows.Forms.Padding(0);
            this.StatusPicker.Name = "StatusPicker";
            this.StatusPicker.OriginalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(81)))));
            this.StatusPicker.OriginalForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.StatusPicker.Size = new System.Drawing.Size(227, 34);
            this.StatusPicker.TabIndex = 46;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.btnAdd.BorderRadius = 23;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(80, 290);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(210, 49);
            this.btnAdd.TabIndex = 47;
            this.btnAdd.Text = "Tovább";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.btnCancel.BorderRadius = 23;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(114, 344);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 40);
            this.btnCancel.TabIndex = 48;
            this.btnCancel.Text = "Mégse";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // DatePicker
            // 
            this.DatePicker.BackDisabledColor = System.Drawing.SystemColors.Control;
            this.DatePicker.CustomFormat = "yyyy. MM. dd.";
            this.DatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatePicker.Image = null;
            this.DatePicker.Location = new System.Drawing.Point(71, 224);
            this.DatePicker.Name = "DatePicker";
            this.DatePicker.Size = new System.Drawing.Size(147, 31);
            this.DatePicker.TabIndex = 44;
            this.DatePicker.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.DatePicker.Value = new System.DateTime(2025, 2, 20, 0, 0, 0, 0);
            // 
            // UpdateAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(354, 406);
            this.Controls.Add(this.StatusPicker);
            this.Controls.Add(this.TimePicker);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.DatePicker);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LoginLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.Name = "UpdateAppointment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Foglalás kezelése";
            this.Load += new System.EventHandler(this.UpdateAppointment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LoginLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private CustomDateTimePicker DatePicker;
        private CustomButton btnAdd;
        private CustomButton btnCancel;
        private MaskedTextBox TimePicker;
        private CustomComboBox StatusPicker;
    }
}