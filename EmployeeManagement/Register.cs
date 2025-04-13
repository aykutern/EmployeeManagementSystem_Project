using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagement
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;
                string confirmPassword = txtPasswordConfirm.Text;

                // Validate input
                if (password == "" || confirmPassword == "")
                {
                    MessageBox.Show("Please enter a valid password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (password != confirmPassword)
                {
                    MessageBox.Show("Passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if username already exists
                string checkQuery = $"SELECT * FROM Users WHERE Username = '{username}'";
                DataTable dt = DatabaseHandler.ExecuteSelect(checkQuery);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Username already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Insert new user into the database. Role is set to 'User' by default.
                string query = $"INSERT INTO Users (Username, Password, Role) VALUES ('{username}', '{password}', 'User')";
                           
                try
                {

                    bool isSuccessful = DatabaseHandler.ExecuteNonQuery(query);
                    if (isSuccessful)
                    {
                        MessageBox.Show("Registration Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        Login login = new Login();
                        login.Show();
                    }
                    else
                    {
                        MessageBox.Show("Registration failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
