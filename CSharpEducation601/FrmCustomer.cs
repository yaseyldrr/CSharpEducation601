using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CSharpEducation601
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }

        string connectionString = "Server = localhost; port = 5432; Database= CustomerDB; " +
            "user Id = postgres; password=1234;";

        void GetAllCustomers()
        { 
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Select * from customers";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command); // Köprü görevi görür
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            GetAllCustomers();
        }

        private void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            string customerName = txtEmployeeName.Text;
            string customerSurname = txtEmployeeSurname.Text;
            string customerCity = txtEmployeeSalary.Text;

            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Insert into customers (customername, customersurname, customercity) " +
                "values (@customername, @customersurname, @customercity)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@customername", customerName);
            command.Parameters.AddWithValue("@customersurname", customerSurname);
            command.Parameters.AddWithValue("@customercity", customerCity);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Customer added successfully.");
            GetAllCustomers();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtEmployeeId.Text);
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Delete from customers where customerid = @customerid";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@customerid", id);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Customer deleted successfully.");
            GetAllCustomers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string customerName = txtEmployeeName.Text;
            string customerSurname = txtEmployeeSurname.Text;
            string customerCity = txtEmployeeSalary.Text;
            int id = int.Parse(txtEmployeeId.Text);
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Update customers " +
                "set customername = @customername, " +
                "customersurname = @customersurname, " +
                "customercity = @customercity " +
                "where customerid = @customerid";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@customername", customerName);
            command.Parameters.AddWithValue("@customersurname", customerSurname);
            command.Parameters.AddWithValue("@customercity", customerCity);
            command.Parameters.AddWithValue("@customerid", id);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Customer updated successfully.");
            GetAllCustomers();


        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtEmployeeId.Text);
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Select * from customers where customerid = @customerid";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@customerid", id);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }
    }
}
