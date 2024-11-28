using System.Windows.Forms;
using System;

namespace AutoMuhely
{
    partial class RegistrationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrationForm));
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.RegisterLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PicLock = new System.Windows.Forms.PictureBox();
            this.PicUser = new System.Windows.Forms.PictureBox();
            this.txtPassword1 = new System.Windows.Forms.TextBox();
            this.Line2 = new System.Windows.Forms.Panel();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.Line1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPassword2 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRegister = new AutoMuhely.CustomButton();
            this.btnCancel = new AutoMuhely.CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicLock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbRole
            // 
            this.cmbRole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.Font = new System.Drawing.Font("Open Sans", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.cmbRole.Items.AddRange(new object[] {
            "Szerelő",
            "Adminisztrátor"});
            this.cmbRole.Location = new System.Drawing.Point(65, 344);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(262, 36);
            this.cmbRole.TabIndex = 2;
            // 
            // RegisterLabel
            // 
            this.RegisterLabel.AutoSize = true;
            this.RegisterLabel.BackColor = System.Drawing.Color.Transparent;
            this.RegisterLabel.Font = new System.Drawing.Font("Bauhaus 93", 29.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.RegisterLabel.Location = new System.Drawing.Point(40, 108);
            this.RegisterLabel.Name = "RegisterLabel";
            this.RegisterLabel.Size = new System.Drawing.Size(283, 44);
            this.RegisterLabel.TabIndex = 11;
            this.RegisterLabel.Text = "REGISZTRÁCIÓ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::AutoMuhely.Properties.Resources.logoX;
            this.pictureBox1.Location = new System.Drawing.Point(125, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // PicLock
            // 
            this.PicLock.Image = global::AutoMuhely.Properties.Resources.lock_icon;
            this.PicLock.Location = new System.Drawing.Point(27, 238);
            this.PicLock.Name = "PicLock";
            this.PicLock.Size = new System.Drawing.Size(32, 30);
            this.PicLock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicLock.TabIndex = 18;
            this.PicLock.TabStop = false;
            // 
            // PicUser
            // 
            this.PicUser.Image = global::AutoMuhely.Properties.Resources.user_icon;
            this.PicUser.Location = new System.Drawing.Point(27, 185);
            this.PicUser.Name = "PicUser";
            this.PicUser.Size = new System.Drawing.Size(32, 30);
            this.PicUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicUser.TabIndex = 17;
            this.PicUser.TabStop = false;
            // 
            // txtPassword1
            // 
            this.txtPassword1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.txtPassword1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F);
            this.txtPassword1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.txtPassword1.Location = new System.Drawing.Point(65, 243);
            this.txtPassword1.Name = "txtPassword1";
            this.txtPassword1.PasswordChar = '●';
            this.txtPassword1.Size = new System.Drawing.Size(262, 25);
            this.txtPassword1.TabIndex = 14;
            // 
            // Line2
            // 
            this.Line2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.Line2.Location = new System.Drawing.Point(27, 274);
            this.Line2.Name = "Line2";
            this.Line2.Size = new System.Drawing.Size(300, 2);
            this.Line2.TabIndex = 16;
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.txtUsername.Location = new System.Drawing.Point(65, 190);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(262, 25);
            this.txtUsername.TabIndex = 13;
            // 
            // Line1
            // 
            this.Line1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.Line1.Location = new System.Drawing.Point(27, 221);
            this.Line1.Name = "Line1";
            this.Line1.Size = new System.Drawing.Size(300, 2);
            this.Line1.TabIndex = 15;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AutoMuhely.Properties.Resources.admin;
            this.pictureBox2.Location = new System.Drawing.Point(27, 346);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 30);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 24;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::AutoMuhely.Properties.Resources.lock_icon;
            this.pictureBox3.Location = new System.Drawing.Point(27, 293);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 30);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 23;
            this.pictureBox3.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.panel1.Location = new System.Drawing.Point(27, 382);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 2);
            this.panel1.TabIndex = 22;
            // 
            // txtPassword2
            // 
            this.txtPassword2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.txtPassword2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.txtPassword2.Location = new System.Drawing.Point(65, 298);
            this.txtPassword2.Name = "txtPassword2";
            this.txtPassword2.PasswordChar = '●';
            this.txtPassword2.Size = new System.Drawing.Size(262, 25);
            this.txtPassword2.TabIndex = 19;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.panel2.Location = new System.Drawing.Point(27, 329);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(300, 2);
            this.panel2.TabIndex = 21;
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.btnRegister.BorderRadius = 23;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(80, 412);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(210, 49);
            this.btnRegister.TabIndex = 5;
            this.btnRegister.Text = "Tovább";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BtnRegister_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.btnCancel.BorderRadius = 23;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(115, 475);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 40);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Mégse";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // RegistrationForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(354, 534);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtPassword2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.PicLock);
            this.Controls.Add(this.PicUser);
            this.Controls.Add(this.txtPassword1);
            this.Controls.Add(this.Line2);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.Line1);
            this.Controls.Add(this.RegisterLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbRole);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(370, 573);
            this.MinimumSize = new System.Drawing.Size(370, 573);
            this.Name = "RegistrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Regisztráció";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicLock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private ComboBox cmbRole;
        #endregion

        private CustomButton btnRegister;
        private CustomButton btnCancel;
        private Label RegisterLabel;
        private PictureBox pictureBox1;
        private PictureBox PicLock;
        private PictureBox PicUser;
        private TextBox txtPassword1;
        private Panel Line2;
        private TextBox txtUsername;
        private Panel Line1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Panel panel1;
        private TextBox txtPassword2;
        private Panel panel2;
    }
}