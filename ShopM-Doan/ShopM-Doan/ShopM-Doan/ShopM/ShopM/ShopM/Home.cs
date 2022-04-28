using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopM
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            DemKH();
            DemNV();
            DemSP();
            DemHD();
        }

        SqlConnection sql = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\QuanLyShopdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DemKH()
        {
            //string SL = "KH";
            sql.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(Distinct KhachHangID) from KhachHangtbl", sql);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            KhachHanglbl.Text = dt.Rows[0][0].ToString();
            sql.Close();
        }
        private void DemNV()
        {
            sql.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(Distinct NhanVienID) from NhanVientbl", sql);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            NhanVienlbl.Text = dt.Rows[0][0].ToString();
            sql.Close();
        }
        private void DemSP()
        {
            sql.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(Distinct SanphamID) from SanPhamtbl", sql);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SanPhamlbl.Text = dt.Rows[0][0].ToString();
            sql.Close();
        }
        private void DemHD()
        {
            sql.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(Distinct HoaDonID) from HoaDontb", sql);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            HoaDonlbl.Text = dt.Rows[0][0].ToString();
            sql.Close();
        }
        private void label12_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {
            Sanpham obj = new Sanpham();
            obj.Show();
            this.Hide();
        }
        private void label3_Click(object sender, EventArgs e)
        {
            Nhanvien obj = new Nhanvien();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Khachhang obj = new Khachhang();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            HoaDon obj = new HoaDon();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
    }
}
