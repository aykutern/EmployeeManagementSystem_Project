using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagement
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsernameLogin.Text == "" || txtPasswordLogin.Text == "")
            {
                MessageBox.Show("Please enter a valid username and password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Get the username and password from the text boxes
                string username = txtUsernameLogin.Text;
                string password = txtPasswordLogin.Text;
                string query = $"SELECT * FROM Users WHERE Username = '{{0}}' AND Password = '{{1}}'";
                DataTable dt = DatabaseHandler.ExecuteSelect(string.Format(query, username, password));
                // Check if the user exists in the database
                if (dt.Rows.Count > 0)
                {
                    string role = dt.Rows[0]["Role"].ToString();
                    frmMain main = new frmMain(username, role);
                    this.Hide();
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Invalid username or password!", "Error",  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // This method is called when the "Register" button is clicked
        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmRegister register = new frmRegister();
            register.Show();
        }
    }
}
