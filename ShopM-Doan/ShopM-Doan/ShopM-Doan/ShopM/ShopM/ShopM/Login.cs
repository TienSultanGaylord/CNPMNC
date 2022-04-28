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

namespace ShopM
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection sql = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\QuanLyShopdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            sql.Open();
            //Gọi biến đếm hàng
            int i = 0;
            SqlCommand cmd = new SqlCommand("select * from NhanVientbl where NhanVienTaiKhoan='" + textBox1.Text + "' and NhanVienPass='" + textBox2.Text + "'", sql);
            cmd.ExecuteNonQuery();
            //tạo bảng chứa dữ liệu
            DataTable dt = new DataTable();
            // tạo dữ liệu adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());
            //Dùng biến đếm để check có đúng tk và mk
            if(i == 0)
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
            }
            else
            {
                //MessageBox.Show("Đăng nhập thành công");
                this.Hide();
                Home home = new Home();
                home.Show();
            }
            sql.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
