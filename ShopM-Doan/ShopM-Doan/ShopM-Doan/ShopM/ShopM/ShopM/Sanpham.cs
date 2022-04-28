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
    public partial class Sanpham : Form
    {
        public Sanpham()
        {
            InitializeComponent();
            DisplaySanpham();
        }
        SqlConnection sql = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\QuanLyShopdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplaySanpham()
        {
            sql.Open();
            string Query = "Select * from SanPhamtbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, sql);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SanphamList.DataSource = ds.Tables[0];
            sql.Close();
        }
        private void Clear()
        {
            SanphamName.Text = "";
            DanhmucCb.Text = "";
            SoluongTb.Text = "";
            GiaTb.Text = "";
            NhacungcapTb.Text = "";
            HSD1.Text = "";
            HSD2.Text = "";
        }

        private void Luubtn_Click(object sender, EventArgs e)
        {
            if (SanphamName.Text == "" || DanhmucCb.Text == "" || SoluongTb.Text == "" || GiaTb.Text == "" || NhacungcapTb.Text == "" || HSD1.Text =="" ||  HSD2.Text =="")
            {
                MessageBox.Show("Nhập thiếu thông tin");
            }
            else
            {
                try
                {
                    sql.Open();
                    SqlCommand cmd = new SqlCommand("insert into SanPhamtbl (SanPhamName,DanhMucSanPham,SoLuongSanPham,GiaSanPham,NhaCungCap,SanPhamHSD1,SanPhamHSD2) values(@SPN,@DM,@SL,@G,@NCC,@HSD1,@HSD2)", sql);
                    cmd.Parameters.AddWithValue("@SPN", SanphamName.Text);
                    cmd.Parameters.AddWithValue("@DM", DanhmucCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SL", SoluongTb.Text);
                    cmd.Parameters.AddWithValue("@G", GiaTb.Text);
                    cmd.Parameters.AddWithValue("@NCC", NhacungcapTb.Text);
                    cmd.Parameters.AddWithValue("@HSD1", HSD1.Value.Date);
                    cmd.Parameters.AddWithValue("@HSD2", HSD2.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm sản phẩm");
                    sql.Close();
                    DisplaySanpham();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void SanphamList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SanphamName.Text = SanphamList.SelectedRows[0].Cells[1].Value.ToString();
            DanhmucCb.Text = SanphamList.SelectedRows[0].Cells[2].Value.ToString();
            SoluongTb.Text = SanphamList.SelectedRows[0].Cells[3].Value.ToString();
            GiaTb.Text = SanphamList.SelectedRows[0].Cells[4].Value.ToString();
            NhacungcapTb.Text = SanphamList.SelectedRows[0].Cells[5].Value.ToString();
            HSD1.Text = SanphamList.SelectedRows[0].Cells[6].Value.ToString();
            HSD2.Text = SanphamList.SelectedRows[0].Cells[7].Value.ToString();

            if (SanphamName.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(SanphamList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void Xoabtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Chọn sản phẩm để xóa ");
            }
            else
            {
                try
                {
                    sql.Open();
                    SqlCommand cmd = new SqlCommand("Delete from SanPhamtbl where SanPhamID = @Key", sql);
                    cmd.Parameters.AddWithValue("@Key", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa thành công");
                    sql.Close();
                    DisplaySanpham();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void Chinhsuabtn_Click(object sender, EventArgs e)
        {
            if (SanphamName.Text == "" || DanhmucCb.Text == "" || SoluongTb.Text == "" || GiaTb.Text == "" || NhacungcapTb.Text == "" || HSD1.Text == "" || HSD2.Text == "")
            {
                MessageBox.Show("Nhập thiếu thông tin");
            }
            else
            {
                try
                {
                    sql.Open();
                    SqlCommand cmd = new SqlCommand("Update SanPhamtbl set SanPhamName = @SPN , DanhMucSanPham = @DM , SoLuongSanPham = @SL , GiaSanPham = @G, NhaCungCap = @NCC , SanPhamHSD1= @HSD1 , SanPhamHSD2 = @HSD2 where SanphamID = @Key", sql);
                    cmd.Parameters.AddWithValue("@SPN", SanphamName.Text);
                    cmd.Parameters.AddWithValue("@DM", DanhmucCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SL", SoluongTb.Text);
                    cmd.Parameters.AddWithValue("@G", GiaTb.Text);
                    cmd.Parameters.AddWithValue("@NCC", NhacungcapTb.Text);
                    cmd.Parameters.AddWithValue("@HSD1", HSD1.Value.Date);
                    cmd.Parameters.AddWithValue("@HSD2", HSD2.Value.Date);
                    cmd.Parameters.AddWithValue("@Key", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã cập nhật sản phẩm");
                    sql.Close();
                    DisplaySanpham();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
        }

        private void label24_Click(object sender, EventArgs e)
        {
            Sanpham Obj = new Sanpham();
            Obj.Show();
            this.Hide();
        }

        private void label26_Click(object sender, EventArgs e)
        {
            Nhanvien Obj = new Nhanvien();
            Obj.Show();
            this.Hide();
        }

        private void label23_Click(object sender, EventArgs e)
        {
            Khachhang Obj = new Khachhang();
            Obj.Show();
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            HoaDon Obj = new HoaDon();
            Obj.Show();
            this.Hide();
        }

        private void Sanpham_Load(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sql.Open();
            SqlCommand cmd = sql.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from SanPhamtbl Where SanPhamID like '" + txtTimKiem.Text + "' or SanPhamName like N'%" + txtTimKiem.Text + "%' or DanhMucSanPham like N'%" + txtTimKiem.Text + "%'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            SanphamList.DataSource = dt;
            sql.Close();
        }

        private void DeleteSearch_Click(object sender, EventArgs e)
        {
            sql.Open();
            string Query = "Select * from SanPhamtbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, sql);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SanphamList.DataSource = ds.Tables[0];
            sql.Close();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
    }
}

