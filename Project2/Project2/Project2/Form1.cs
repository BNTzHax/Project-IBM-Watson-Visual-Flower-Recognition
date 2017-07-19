using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Project2
{
    public partial class Form1 : Form
    {
        string path = null;
        public Form1()
        {
            InitializeComponent();
            
        }

        private static bool ZipFile = false;
        private void buttonSelectImage_Click(object sender, EventArgs e)
        {
            try {
                openFileDialog.ShowDialog();// Lấy đường dẫn file

                
                
                if (openFileDialog.FileName != null)
                {
                    path = openFileDialog.FileName;
                    pictureBox.Image = Image.FromFile(path);
                    textBoxImage.Text = path;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBoxImage.Text == "") || (textBoxThreshold.Text == ""))
                {
                    MessageBox.Show("Bạn chưa chọn ảnh hoặc độ chính xác mong muốn", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
                else
                {
                    double checkNumber = Convert.ToDouble(textBoxThreshold.Text);
                    if((0<=checkNumber)&&(checkNumber<=1))// Kiểm tra tính chính xác của threshold
                    {
                        if (ZipFile==false)
                        {
                            richTextBoxResponse.Text = "";
                            Connection connector = new Connection();
                            string result = connector.Connect(textBoxImage, textBoxThreshold);
                            richTextBoxResponse.AppendText("Server response: " + Environment.NewLine + result);// In trả lời của server
                        }
                        else
                        {
                            richTextBoxResponse.Text = "";
                            Connection connector = new Connection();
                            string result = connector.ConnectZip(textBoxImage, textBoxThreshold);
                            richTextBoxResponse.AppendText("Server response: " + Environment.NewLine + result);
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Threshold chỉ trong khoảng từ 0 tới 1", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    }
                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void buttonZipFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.ShowDialog();

                string path = openFileDialog.FileName;
                if (path != null)
                {
                    ZipFile = true;
                    pictureBox.Image = Image.FromFile(@"D:\Image\LinhTinh\NoImage.jpg");
                    textBoxImage.Text = path;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
