using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Excel = Microsoft.Office.Interop.Excel;

namespace EmployeeManagement
{
    
    public partial class EmployeeManegement : Form
    {
        // Declare variables to hold username and role
        String frm_role = "User";
        public EmployeeManegement(String frm_role)
        {
            InitializeComponent();
            this.AcceptButton = btnAdd;
            this.frm_role = frm_role;
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtPosition.Text == "" || txtSalary.Text == "")
            {
                MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Check if the user has permission to add employees
                string name = txtName.Text;
                string position = txtPosition.Text;
                string salary = txtSalary.Text;
                string query = $"INSERT INTO Employees (Name, Position, Salary) VALUES ('{name}', '{position}', {salary})";
                if (DatabaseHandler.ExecuteNonQuery(query))
                    LoadEmployees();
                MessageBox.Show("Employee added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // Clear the fields after adding an employee
            ClearFields();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {


            // Check if the user has permission to edit employees and changes the text lowercase
            if (frm_role.ToLower() == "user")
            {
                MessageBox.Show("You do not have permission to edit employees.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (txtName.Text == "" || txtPosition.Text == "" || txtSalary.Text == "")
                {
                    MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Check if any row is selected
                    string name = txtName.Text;
                    string position = txtPosition.Text;
                    string salary = txtSalary.Text;
                    // Get the selected employee ID
                    if (dgvEmployees.SelectedRows.Count == 0) return;
                    //Cell [0]  refers to the EmployeeID column 
                    int id = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells[0].Value);
                    string query = $"UPDATE Employees SET Name = '{name}', Position = '{position}', Salary = '{salary}' WHERE EmployeeID = '{id}'";
                    if (DatabaseHandler.ExecuteNonQuery(query))
                    {
                        LoadEmployees();
                    }

                }

                MessageBox.Show("Employee edited successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // Clear the fields after editing an employee
            ClearFields();
        }
        // Load the employees into the DataGridView
        private void LoadEmployees()
        {
            string query = "SELECT * FROM Employees";
            DataTable dt = DatabaseHandler.ExecuteSelect(query);
            dgvEmployees.DataSource=dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Check if the user has permission to delete employees and changes the text lowercase
            if (frm_role.ToLower() == "user")
            {
                MessageBox.Show("You do not have permission to delete employees!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Check if any row is selected
                if (dgvEmployees.SelectedRows.Count == 0) return;
                // Get the selected employee ID
                //Cell [0]  refers to the EmployeeID column
                int id = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells[0].Value);
                string query = $"DELETE FROM Employees WHERE EmployeeID={id}";
                if (DatabaseHandler.ExecuteNonQuery(query))

                {
                    LoadEmployees();
                    MessageBox.Show("Employee deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }
            // Clear the fields after deleting an employee
            ClearFields();

        }
        

        private void EmployeeManegement_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'employeeManagementDataSet.Employees' table. You can move, or remove it, as needed.
            this.employeesTableAdapter.Fill(this.employeeManagementDataSet.Employees);

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.employeesTableAdapter.FillBy(this.employeeManagementDataSet.Employees);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
        // This method is called when the DataGridView selection changes
        private void dgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                txtName.Text = dgvEmployees.SelectedRows[0].Cells[1].Value.ToString();
                txtPosition.Text = dgvEmployees.SelectedRows[0].Cells[2].Value.ToString();
                txtSalary.Text = dgvEmployees.SelectedRows[0].Cells[3].Value.ToString();
            }
        }
        // This method is called when the filter button is clicked for searching employees by name
        private void btnFilter_Click(object sender, EventArgs e)
        {
            
            if (txtFilter.Text == "")
            {
                MessageBox.Show("Please enter a name!", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string filter = txtFilter.Text;
                string query = $"SELECT * FROM Employees WHERE Name = '{filter}'";
                DataTable dt = DatabaseHandler.ExecuteSelect(query);
                dgvEmployees.DataSource = dt;
            }
        }
        //Clears all fields after delete-update-insert transactions
        private void ClearFields()
        {
            txtName.Text = "";
            txtPosition.Text = "";
            txtSalary.Text = "";
            txtFilter.Text = "";
        }
        // This method is called when the export to CVS button is clicked 
        private void btnCVS_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV file (*.csv)|*.csv";
            saveFileDialog.Title = "Save as CSV File";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToCSV(dgvEmployees, saveFileDialog.FileName);
            }
        }

        private void ExportToCSV(DataGridView dgv, string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                // Write header
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    sw.Write(dgv.Columns[i].HeaderText);
                    if (i < dgv.Columns.Count - 1) sw.Write(",");
                }
                sw.WriteLine();

                // Write rows
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        for (int i = 0; i < dgv.Columns.Count; i++)
                        {
                            sw.Write(row.Cells[i].Value?.ToString());
                            if (i < dgv.Columns.Count - 1) sw.Write(",");
                        }
                        sw.WriteLine();
                    }
                }
            }

            MessageBox.Show("CSV file has been saved!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        // This method is called when the export to Excel button is clicked
        private void ExportToExcel(DataGridView dgv)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            worksheet.Name = "Exported from DataGridView";

            // Add header
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
            }

            // Add data rows
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value?.ToString();
                }
            }

            // Show Excel and cleanup
            excelApp.Visible = true;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvEmployees);
        }

        // This method blocks any non-numeric input in the salary text box
        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow digits, backspace, and one dot
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.' && e.KeyChar != ','))
            {
                e.Handled = true;
            }

            // Only allow one decimal point (either dot or comma)
            if ((e.KeyChar == '.' || e.KeyChar == ',') &&
                (txtSalary.Text.Contains('.') || txtSalary.Text.Contains(',')))
            {
                e.Handled = true;
            }
        }
    }
}
