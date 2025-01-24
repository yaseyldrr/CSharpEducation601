using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace CSharpEducation601
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        string connectionString = "Server=localhost;port = 5432;" +
            " Database = CustomerDB; User Id = postgres; Password = 1234;";

        void EmployeeList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM employees";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            var dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close(); 
        }

        void DepartmentList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM departments";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            var dt = new DataTable();
            adapter.Fill(dt);
            cmbEmployeeDepartment.DisplayMember = "DepartmentName";
            cmbEmployeeDepartment.ValueMember = "DepartmentId";
            cmbEmployeeDepartment.DataSource = dt;
            connection.Close();

        }
        private void btnList_Click(object sender, EventArgs e)
        {
            EmployeeList();
        }

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            EmployeeList();
            DepartmentList();
        }

        private void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            string employeeName = txtEmployeeName.Text;
            string employeeSurname = txtEmployeeSurname.Text;
            decimal employeeSalary = decimal.Parse(txtEmployeeSalary.Text);
            int departmentId = int.Parse(cmbEmployeeDepartment.SelectedValue.ToString());

            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "INSERT INTO employees (employeename, employeesurname, employeesalary, departmentid) " +
                "VALUES (@employeename, @employeesurname, @employeesalary, @departmentid)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@employeename", employeeName);
            command.Parameters.AddWithValue("@employeesurname", employeeSurname);
            command.Parameters.AddWithValue("@employeesalary", employeeSalary);
            command.Parameters.AddWithValue("@departmentid", departmentId);
            command.ExecuteNonQuery();
            MessageBox.Show("Employee added successfully.");
            connection.Close();
            EmployeeList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtCustomerId.Text);
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "DELETE FROM employees WHERE employeeid = @employeeid";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@employeeid", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Employee deleted successfully.");
            connection.Close();
            EmployeeList(); 
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtCustomerId.Text);
            string employeeName = txtEmployeeName.Text;
            string employeeSurname = txtEmployeeSurname.Text;
            decimal employeeSalary = decimal.Parse(txtEmployeeSalary.Text);
            int departmentId = int.Parse(cmbEmployeeDepartment.SelectedValue.ToString());

            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "UPDATE employees SET employeename = @employeename, employeesurname = @employeesurname, " +
                "employeesalary = @employeesalary, departmentid = @departmentid WHERE employeeid = @employeeid";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@employeename", employeeName);
            command.Parameters.AddWithValue("@employeesurname", employeeSurname);
            command.Parameters.AddWithValue("@employeesalary", employeeSalary);
            command.Parameters.AddWithValue("@departmentid", departmentId);
            command.Parameters.AddWithValue("@employeeid", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Employee updated successfully.");
            connection.Close();
            EmployeeList();
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtCustomerId.Text);
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM employees WHERE employeeid = @employeeid";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@employeeid", id);
            var adapter = new NpgsqlDataAdapter(command);
            var dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();

        }
    }
}
