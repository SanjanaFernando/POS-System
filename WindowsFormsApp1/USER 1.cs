using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        [DllImport("GDi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeft,
            int nTop,
            int nRight,
            int nBottom,
            int nWidthEllipse,
            int nHeightEllipse
            );
        private SQLiteConnection connection;
        private SQLiteDataAdapter dataAdapter;
        private DataSet dataSet;
        public Form1()
        {
            InitializeComponent();

            
            string connectionString = "Data Source=D:\\SEMESTER 3\\Projects\\Last 2\\Project\\usersList.db;Version=3;";
            connection = new SQLiteConnection(connectionString);
            connection.Open();

            
            dataAdapter = new SQLiteDataAdapter("SELECT * FROM Users", connection);
            dataSet = new DataSet();

            
            dataAdapter.Fill(dataSet, "Users");

           
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = dataSet.Tables["Users"];
            dataGridView1.DataSource = bindingSource;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button2.Width, button2.Height, 30, 30));
            panel1.BackColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();

        }
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            connection.Close();
        }

  

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            try
            {
                SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(dataAdapter);
                dataAdapter.Update(dataSet, "Users");
                MessageBox.Show("Data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }

        private void DeleteSelectedRow()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                int selectedIndex = dataGridView1.SelectedRows[0].Index;

                
                dataSet.Tables["Users"].Rows[selectedIndex].Delete();

                
                try
                {
                    SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(dataAdapter);
                    dataAdapter.Update(dataSet, "Users");

                    
                    dataSet.AcceptChanges();

                    MessageBox.Show("Row deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting row: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteSelectedRow();
        }
    }
}
