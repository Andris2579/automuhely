using System;

namespace AutoMuhely
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private CustomButton btnLogin;
        private CustomButton btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.btnLogin = new AutoMuhely.CustomButton();
            this.btnCancel = new AutoMuhely.CustomButton();
            this.LoginLabel = new System.Windows.Forms.Label();
            this.Line1 = new System.Windows.Forms.Panel();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.Line2 = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.PicUser = new System.Windows.Forms.PictureBox();
            this.PicLock = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PicUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicLock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnLogin.BorderRadius = 30;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(100, 297);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(170, 49);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnCancel.BorderRadius = 23;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(125, 360);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // LoginLabel
            // 
            this.LoginLabel.AutoSize = true;
            this.LoginLabel.BackColor = System.Drawing.Color.Transparent;
            this.LoginLabel.Font = new System.Drawing.Font("Bauhaus 93", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.LoginLabel.Location = new System.Drawing.Point(102, 108);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(166, 54);
            this.LoginLabel.TabIndex = 4;
            this.LoginLabel.Text = "LOG IN";
            // 
            // Line1
            // 
            this.Line1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.Line1.Location = new System.Drawing.Point(28, 223);
            this.Line1.Name = "Line1";
            this.Line1.Size = new System.Drawing.Size(300, 2);
            this.Line1.TabIndex = 5;
            // 
            // txtUsername
            // 
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtUsername.Location = new System.Drawing.Point(66, 192);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(262, 25);
            this.txtUsername.TabIndex = 1;
            // 
            // Line2
            // 
            this.Line2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.Line2.Location = new System.Drawing.Point(28, 276);
            this.Line2.Name = "Line2";
            this.Line2.Size = new System.Drawing.Size(300, 2);
            this.Line2.TabIndex = 6;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.Window;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F);
            this.txtPassword.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtPassword.Location = new System.Drawing.Point(66, 245);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(262, 25);
            this.txtPassword.TabIndex = 2;
            // 
            // PicUser
            // 
            this.PicUser.Image = ((System.Drawing.Image)(resources.GetObject("PicUser.Image")));
            this.PicUser.Location = new System.Drawing.Point(28, 187);
            this.PicUser.Name = "PicUser";
            this.PicUser.Size = new System.Drawing.Size(32, 30);
            this.PicUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicUser.TabIndex = 8;
            this.PicUser.TabStop = false;
            // 
            // PicLock
            // 
            this.PicLock.Image = ((System.Drawing.Image)(resources.GetObject("PicLock.Image")));
            this.PicLock.Location = new System.Drawing.Point(28, 240);
            this.PicLock.Name = "PicLock";
            this.PicLock.Size = new System.Drawing.Size(32, 30);
            this.PicLock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicLock.TabIndex = 9;
            this.PicLock.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(125, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // LoginForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(354, 461);
            this.Controls.Add(this.PicLock);
            this.Controls.Add(this.PicUser);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.Line2);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.Line1);
            this.Controls.Add(this.LoginLabel);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(370, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(370, 500);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.PicUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicLock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label LoginLabel;
        private System.Windows.Forms.Panel Line1;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Panel Line2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.PictureBox PicUser;
        private System.Windows.Forms.PictureBox PicLock;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}