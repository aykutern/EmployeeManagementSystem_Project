using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace EmployeeManagement
{
    public partial class frmMain : Form
    {
        // Declare variables to hold username and role
        String frm_username;
        String frm_role;

        // Constructor to initialize the form with username and role
        public frmMain(String username, String role)
        {
            InitializeComponent();
            this.AcceptButton = btnManageEmployees;
            frm_username = username;
            frm_role = role;
            lblWelcome.Text = "Welcome " + frm_username + "!";
        }

        private void btnManageEmployees_Click(object sender, EventArgs e)
        {
            this.Hide();
            // Create an instance of EmployeeManegement and pass the role
            EmployeeManegement employee_Management = new EmployeeManegement(frm_role);
            employee_Management.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            Login loginForm = new Login();
            loginForm.Show();
        }
    }
}
