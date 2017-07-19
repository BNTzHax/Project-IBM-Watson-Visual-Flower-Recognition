using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Project2
{
    public partial class Form2 : Form
    {
        SqlConnection connection;
        public Form2()
        {
            InitializeComponent();
        }

        private void checkBoxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxShowPass.Checked)
            {
                textBoxPass.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxPass.UseSystemPasswordChar = true;
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            connection = new SqlConnection(@"Data Source=WINDOWS-QJFDAQQ\SQLEXPRESS;Initial Catalog=Login;Integrated Security=True");
            string sqlCommand = "Select Count(*) FROM DangNhap Where tenDangNhap='"+textBoxId.Text+"' And matKhau='"+textBoxPass.Text+"'";
            connection.Open();
            //SqlDataAdapter da = new SqlDataAdapter(sqlCommand,connection);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            SqlCommand command = new SqlCommand(sqlCommand, connection);
            int x = (int)command.ExecuteScalar();
            connection.Close();
            if(x==1)
            {
                MessageBox.Show("Đăng nhập thành công","Hoàn tất đăng nhập",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Visible=false;
                Form1 f1 = new Form1();
                f1.Show();
                
                
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại. Tài khoản hoặc mật khẩu không đúng.\n Vui lòng nhập lại", "Notification",MessageBoxButtons.OKCancel,MessageBoxIcon.Stop);
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            textBoxId.Text = null;
            textBoxPass.Text = null;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBoxPass.UseSystemPasswordChar = true;
        }
    }
}
