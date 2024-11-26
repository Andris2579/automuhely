using System.Drawing;
using System.Windows.Forms;

namespace AutoMuhely
{
    partial class alkatrészInputs
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
        private Button alkatrészHozzáadás_Btn;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(alkatrészInputs));
            this.alkatrészNév_Lb = new System.Windows.Forms.Label();
            this.alkatrészNév_Tb = new System.Windows.Forms.TextBox();
            this.alkatrészLeírás_Lb = new System.Windows.Forms.Label();
            this.alkatrészLeírás_Tb = new System.Windows.Forms.TextBox();
            this.alkatrészKezdetiKészlet_Lb = new System.Windows.Forms.Label();
            this.alkatrészKezdetiKészletMennyiség_NUD = new System.Windows.Forms.NumericUpDown();
            this.alkatrészHozzáadás_Btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.alkatrészKezdetiKészletMennyiség_NUD)).BeginInit();
            this.SuspendLayout();
            // 
            // alkatrészNév_Lb
            // 
            this.alkatrészNév_Lb.Location = new System.Drawing.Point(25, 25);
            this.alkatrészNév_Lb.Name = "alkatrészNév_Lb";
            this.alkatrészNév_Lb.Size = new System.Drawing.Size(250, 25);
            this.alkatrészNév_Lb.TabIndex = 0;
            this.alkatrészNév_Lb.Text = "Alkatrész neve";
            // 
            // alkatrészNév_Tb
            // 
            this.alkatrészNév_Tb.Location = new System.Drawing.Point(25, 50);
            this.alkatrészNév_Tb.MaxLength = 100;
            this.alkatrészNév_Tb.Name = "alkatrészNév_Tb";
            this.alkatrészNév_Tb.Size = new System.Drawing.Size(250, 26);
            this.alkatrészNév_Tb.TabIndex = 1;
            // 
            // alkatrészLeírás_Lb
            // 
            this.alkatrészLeírás_Lb.Location = new System.Drawing.Point(25, 90);
            this.alkatrészLeírás_Lb.Name = "alkatrészLeírás_Lb";
            this.alkatrészLeírás_Lb.Size = new System.Drawing.Size(250, 25);
            this.alkatrészLeírás_Lb.TabIndex = 2;
            this.alkatrészLeírás_Lb.Text = "Alkatrész leírása";
            // 
            // alkatrészLeírás_Tb
            // 
            this.alkatrészLeírás_Tb.Location = new System.Drawing.Point(25, 115);
            this.alkatrészLeírás_Tb.Multiline = true;
            this.alkatrészLeírás_Tb.Name = "alkatrészLeírás_Tb";
            this.alkatrészLeírás_Tb.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.alkatrészLeírás_Tb.Size = new System.Drawing.Size(250, 100);
            this.alkatrészLeírás_Tb.TabIndex = 3;
            // 
            // alkatrészKezdetiKészlet_Lb
            // 
            this.alkatrészKezdetiKészlet_Lb.Location = new System.Drawing.Point(25, 225);
            this.alkatrészKezdetiKészlet_Lb.Name = "alkatrészKezdetiKészlet_Lb";
            this.alkatrészKezdetiKészlet_Lb.Size = new System.Drawing.Size(250, 25);
            this.alkatrészKezdetiKészlet_Lb.TabIndex = 4;
            this.alkatrészKezdetiKészlet_Lb.Text = "Kezdeti készlet mennyiség";
            // 
            // alkatrészKezdetiKészletMennyiség_NUD
            // 
            this.alkatrészKezdetiKészletMennyiség_NUD.Location = new System.Drawing.Point(25, 250);
            this.alkatrészKezdetiKészletMennyiség_NUD.Name = "alkatrészKezdetiKészletMennyiség_NUD";
            this.alkatrészKezdetiKészletMennyiség_NUD.Size = new System.Drawing.Size(250, 26);
            this.alkatrészKezdetiKészletMennyiség_NUD.TabIndex = 5;
            // 
            // alkatrészHozzáadás_Btn
            // 
            this.alkatrészHozzáadás_Btn.Location = new System.Drawing.Point(50, 290);
            this.alkatrészHozzáadás_Btn.Name = "alkatrészHozzáadás_Btn";
            this.alkatrészHozzáadás_Btn.Size = new System.Drawing.Size(200, 50);
            this.alkatrészHozzáadás_Btn.TabIndex = 6;
            this.alkatrészHozzáadás_Btn.Text = "Hozzáadás";
            // 
            // alkatrészInputs
            // 
            this.ClientSize = new System.Drawing.Size(325, 400);
            this.Controls.Add(this.alkatrészNév_Lb);
            this.Controls.Add(this.alkatrészNév_Tb);
            this.Controls.Add(this.alkatrészLeírás_Lb);
            this.Controls.Add(this.alkatrészLeírás_Tb);
            this.Controls.Add(this.alkatrészKezdetiKészlet_Lb);
            this.Controls.Add(this.alkatrészKezdetiKészletMennyiség_NUD);
            this.Controls.Add(this.alkatrészHozzáadás_Btn);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(341, 439);
            this.MinimumSize = new System.Drawing.Size(341, 439);
            this.Name = "alkatrészInputs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alkatrész Hozzáadás";
            ((System.ComponentModel.ISupportInitialize)(this.alkatrészKezdetiKészletMennyiség_NUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
