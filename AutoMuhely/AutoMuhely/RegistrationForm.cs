using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace AutoMuhely
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnRegister_Click(object sender, MouseEventArgs e)
        {
            string username = txtUsername.Text;
            string password1 = txtPassword1.Text;
            string password2 = txtPassword2.Text;
            string role = cmbRole.SelectedItem?.ToString();

            // Validate input fields
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password1) || string.IsNullOrEmpty(password2) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Kérem töltse ki a mezőket.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (password1 != password2)
            {
                MessageBox.Show("A jelszavak nem egyeznek.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool success = false; // Flag to track success

            try
            {
                DatabaseHandler dbHandler = new DatabaseHandler();

                // Use MySQL to hash the password during insertion
                string insertQuery = @"
            INSERT INTO felhasznalok (felhasznalonev, jelszo_hash, szerep) 
            VALUES (@Username, SHA2(@Password, 256), @Role);";

                // Prepare the parameters
                var parameters = new Dictionary<string, object>
        {
            { "@Username", username },
            { "@Password", password1 }, // Raw password is passed; MySQL will hash it
            { "@Role", role }
        };

                // Insert into the database
                dbHandler.Insert(insertQuery, parameters);

                success = true; // Mark success if no exception occurs
            }
            catch (MySqlException sqlEx) when (sqlEx.Number == 1062) // Duplicate entry
            {
                MessageBox.Show("A felhasználónév már létezik. Kérem válasszon másikat.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a regisztráció során: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Execute success logic only if registration was successful
            if (success)
            {
                MessageBox.Show($"'{username}' regisztrálva '{role}'-ként!", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear the form fields after successful registration
                txtUsername.Clear();
                txtPassword1.Clear();
                txtPassword2.Clear();
                cmbRole.SelectedIndex = -1;
                this.Close();
            }
        }

    }
}
