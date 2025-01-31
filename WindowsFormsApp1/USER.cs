using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace WindowsFormsApp1
{



    public partial class User : Form
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
           
        private const string ConnectionString = "Data Source=D:\\SEMESTER 3\\Projects\\Last 2\\Project\\usersList.db;Version=3;";
        public User()
        {
            InitializeComponent();
        }
        

        private void Alogin_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string NIC = textBox1.Text;
            string password = textBox2.Text;

            if (IsValidUser(NIC, password, out string userName))
            {
                
                Form2 form2 = new Form2(userName);
                form2.Show();
                this.Hide(); 
            }
            else
            {
                MessageBox.Show("Invalid NIC or password. Please try again.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private bool IsValidUser(string NIC, string password, out string userName)
        {
            userName = string.Empty;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand($"SELECT Name FROM Users WHERE NIC=@NIC AND Password=@Password", connection))
                {
                    command.Parameters.AddWithValue("NIC", NIC);
                    command.Parameters.AddWithValue("@Password", password);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        userName = result.ToString();
                        return true;
                    }

                    return false;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            Main main = new Main();
            main.Show();
            this.Hide();




        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void User_Load(object sender, EventArgs e)
        {
            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 30, 30));
            button2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button2.Width, button2.Height, 30, 30));
            panel1.BackColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
