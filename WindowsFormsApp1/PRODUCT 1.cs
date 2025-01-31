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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Runtime.InteropServices;





namespace WindowsFormsApp1
{
    public partial class Form2 : Form
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
        private DataSet dataset;
        private string Usernamer;
        public Form2(string username)
        {
            InitializeComponent();
            Usernamer = username;
            label2.Text = "Welcome: " + Usernamer;


            
            string connectionString = "Data Source=D:\\SEMESTER 3\\Projects\\Last 2\\Project\\productList.db;Version=3;";
            connection = new SQLiteConnection(connectionString);
            connection.Open();

            
            dataAdapter = new SQLiteDataAdapter("SELECT * FROM Product", connection);
            dataset = new DataSet();

           
            dataAdapter.Fill(dataset, "Product");

            
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = dataset.Tables["Product"];
            USER_DATA.DataSource = bindingSource;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button2.Width, button2.Height, 30, 30));
            panel1.BackColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

        private void USER_DATA_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(dataAdapter);
                dataAdapter.Update(dataset, "Product");
                MessageBox.Show("Data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void DeleteSelectedRow()
        {
            if (USER_DATA.SelectedRows.Count > 0)
            {
                
                int selectedIndex = USER_DATA.SelectedRows[0].Index;

               
                dataset.Tables["Product"].Rows[selectedIndex].Delete();

                
                try
                {
                    SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(dataAdapter);
                    dataAdapter.Update(dataset, "Product");

                    
                    dataset.AcceptChanges();

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteSelectedRow();
        }
    }
}
