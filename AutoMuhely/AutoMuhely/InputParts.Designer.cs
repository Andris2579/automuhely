using System.Drawing;
using System.Windows.Forms;

namespace AutoMuhely
{
    partial class InputParts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Define controls as class-level variables
        private Label alkatrészNév_Lb;
        private TextBox alkatrészNév_Tb;
        private Label alkatrészLeírás_Lb;
        private TextBox alkatrészLeírás_Tb;
        private Label alkatrészKezdetiKészlet_Lb;
        private NumericUpDown alkatrészKezdetiKészletMennyiség_NUD;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputParts));
            this.alkatrészNév_Lb = new System.Windows.Forms.Label();
            this.alkatrészNév_Tb = new System.Windows.Forms.TextBox();
            this.alkatrészLeírás_Lb = new System.Windows.Forms.Label();
            this.alkatrészLeírás_Tb = new System.Windows.Forms.TextBox();
            this.alkatrészKezdetiKészlet_Lb = new System.Windows.Forms.Label();
            this.alkatrészKezdetiKészletMennyiség_NUD = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LoginLabel = new System.Windows.Forms.Label();
            this.lblIsEdit = new System.Windows.Forms.Label();
            this.btnAdd = new AutoMuhely.CustomButton();
            this.btnCancel = new AutoMuhely.CustomButton();
            this.label2 = new System.Windows.Forms.Label();
            this.numAlkatreszUtanrendelesMenny = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.alkatrészKezdetiKészletMennyiség_NUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAlkatreszUtanrendelesMenny)).BeginInit();
            this.SuspendLayout();
            // 
            // alkatrészNév_Lb
            // 
            this.alkatrészNév_Lb.Font = new System.Drawing.Font("Open Sans", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alkatrészNév_Lb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.alkatrészNév_Lb.Location = new System.Drawing.Point(53, 221);
            this.alkatrészNév_Lb.Name = "alkatrészNév_Lb";
            this.alkatrészNév_Lb.Size = new System.Drawing.Size(250, 25);
            this.alkatrészNév_Lb.TabIndex = 0;
            this.alkatrészNév_Lb.Text = "Alkatrész neve";
            // 
            // alkatrészNév_Tb
            // 
            this.alkatrészNév_Tb.Location = new System.Drawing.Point(53, 249);
            this.alkatrészNév_Tb.MaxLength = 100;
            this.alkatrészNév_Tb.Name = "alkatrészNév_Tb";
            this.alkatrészNév_Tb.Size = new System.Drawing.Size(250, 26);
            this.alkatrészNév_Tb.TabIndex = 1;
            // 
            // alkatrészLeírás_Lb
            // 
            this.alkatrészLeírás_Lb.Font = new System.Drawing.Font("Open Sans", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alkatrészLeírás_Lb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.alkatrészLeírás_Lb.Location = new System.Drawing.Point(53, 290);
            this.alkatrészLeírás_Lb.Name = "alkatrészLeírás_Lb";
            this.alkatrészLeírás_Lb.Size = new System.Drawing.Size(250, 25);
            this.alkatrészLeírás_Lb.TabIndex = 2;
            this.alkatrészLeírás_Lb.Text = "Alkatrész leírása";
            // 
            // alkatrészLeírás_Tb
            // 
            this.alkatrészLeírás_Tb.Location = new System.Drawing.Point(53, 318);
            this.alkatrészLeírás_Tb.Multiline = true;
            this.alkatrészLeírás_Tb.Name = "alkatrészLeírás_Tb";
            this.alkatrészLeírás_Tb.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.alkatrészLeírás_Tb.Size = new System.Drawing.Size(250, 100);
            this.alkatrészLeírás_Tb.TabIndex = 3;
            // 
            // alkatrészKezdetiKészlet_Lb
            // 
            this.alkatrészKezdetiKészlet_Lb.Font = new System.Drawing.Font("Open Sans", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alkatrészKezdetiKészlet_Lb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.alkatrészKezdetiKészlet_Lb.Location = new System.Drawing.Point(53, 430);
            this.alkatrészKezdetiKészlet_Lb.Name = "alkatrészKezdetiKészlet_Lb";
            this.alkatrészKezdetiKészlet_Lb.Size = new System.Drawing.Size(250, 25);
            this.alkatrészKezdetiKészlet_Lb.TabIndex = 4;
            this.alkatrészKezdetiKészlet_Lb.Text = "Készlet";
            // 
            // alkatrészKezdetiKészletMennyiség_NUD
            // 
            this.alkatrészKezdetiKészletMennyiség_NUD.Location = new System.Drawing.Point(53, 458);
            this.alkatrészKezdetiKészletMennyiség_NUD.Name = "alkatrészKezdetiKészletMennyiség_NUD";
            this.alkatrészKezdetiKészletMennyiség_NUD.Size = new System.Drawing.Size(250, 26);
            this.alkatrészKezdetiKészletMennyiség_NUD.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::AutoMuhely.Properties.Resources.logoX;
            this.pictureBox1.Location = new System.Drawing.Point(125, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // LoginLabel
            // 
            this.LoginLabel.AutoSize = true;
            this.LoginLabel.BackColor = System.Drawing.Color.Transparent;
            this.LoginLabel.Font = new System.Drawing.Font("Bauhaus 93", 29.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.LoginLabel.Location = new System.Drawing.Point(80, 133);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(210, 44);
            this.LoginLabel.TabIndex = 12;
            this.LoginLabel.Text = "ALKATRÉSZ";
            // 
            // lblIsEdit
            // 
            this.lblIsEdit.AutoSize = true;
            this.lblIsEdit.BackColor = System.Drawing.Color.Transparent;
            this.lblIsEdit.Font = new System.Drawing.Font("Bauhaus 93", 29.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.lblIsEdit.Location = new System.Drawing.Point(55, 177);
            this.lblIsEdit.Name = "lblIsEdit";
            this.lblIsEdit.Size = new System.Drawing.Size(259, 44);
            this.lblIsEdit.TabIndex = 13;
            this.lblIsEdit.Text = "HOZZÁADÁSA";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.btnAdd.BorderRadius = 23;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(75, 572);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(210, 49);
            this.btnAdd.TabIndex = 7;
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
            this.btnCancel.Location = new System.Drawing.Point(109, 627);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 40);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Mégse";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Open Sans", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(135)))), ((int)(((byte)(246)))));
            this.label2.Location = new System.Drawing.Point(53, 501);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(267, 34);
            this.label2.TabIndex = 14;
            this.label2.Text = "Utánrendelt mennyiség";
            // 
            // numAlkatreszUtanrendelesMenny
            // 
            this.numAlkatreszUtanrendelesMenny.Location = new System.Drawing.Point(53, 538);
            this.numAlkatreszUtanrendelesMenny.Name = "numAlkatreszUtanrendelesMenny";
            this.numAlkatreszUtanrendelesMenny.Size = new System.Drawing.Size(250, 26);
            this.numAlkatreszUtanrendelesMenny.TabIndex = 15;
            // 
            // alkatrészInputs
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(354, 681);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numAlkatreszUtanrendelesMenny);
            this.Controls.Add(this.lblIsEdit);
            this.Controls.Add(this.LoginLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.alkatrészNév_Lb);
            this.Controls.Add(this.alkatrészNév_Tb);
            this.Controls.Add(this.alkatrészLeírás_Lb);
            this.Controls.Add(this.alkatrészLeírás_Tb);
            this.Controls.Add(this.alkatrészKezdetiKészlet_Lb);
            this.Controls.Add(this.alkatrészKezdetiKészletMennyiség_NUD);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(370, 720);
            this.MinimumSize = new System.Drawing.Size(370, 680);
            this.Name = "alkatrészInputs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alkatrész Hozzáadás";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.alkatrészInputs_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.alkatrészKezdetiKészletMennyiség_NUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAlkatreszUtanrendelesMenny)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomButton btnAdd;
        private CustomButton btnCancel;
        private PictureBox pictureBox1;
        private Label LoginLabel;
        private Label lblIsEdit;
        private Label label2;
        private NumericUpDown numAlkatreszUtanrendelesMenny;
    }
}
