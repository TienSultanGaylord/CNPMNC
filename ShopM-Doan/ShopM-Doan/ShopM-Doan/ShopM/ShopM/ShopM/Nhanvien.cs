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
    public partial class Nhanvien : Form
    {
        public Nhanvien()
        {
            InitializeComponent();
            DisplayNhanVien();
        }
        SqlConnection sql = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\QuanLyShopdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayNhanVien()
        {
            sql.Open();
            string Query = "Select * from NhanVientbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, sql);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            NhanVienList.DataSource = ds.Tables[0];
            sql.Close();
        }
        private void Clear()
        {
            NhanVienName.Text = "";
            NhanVienDiaChi.Text = "";
            NhanVienNgaySinh.Text = "";
            NhanVienSDT.Text = "";
            NhanVienTaiKhoan.Text = "";
            NhanVienMatKhau.Text = "";
            NhanVienEmail.Text = "";
            NhanVienCMND.Text = "";
        }
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (NhanVienName.Text == "" || NhanVienDiaChi.Text == "" || NhanVienSDT.Text == "" || NhanVienTaiKhoan.Text == "" || NhanVienMatKhau.Text == "" || NhanVienEmail.Text == "" || NhanVienCMND.Text == "" || NhanVienNgaySinh.Text == "")
            {
                MessageBox.Show("Nhập thiếu thông tin");
            }
            else
            {
                try
                {
                    sql.Open();
                    SqlCommand cmd = new SqlCommand("insert into NhanVientbl (NhanVienName,NhanVienDiaChi,NhanVienNgaySinh,NhanVienSDT,NhanVienCMND,NhanVienTaiKhoan,NhanVienPass,NhanVienEmail) values(@NVA,@NVDC,@NCNS,@NVSDT,@NVCMND,@NVTK,@NVP,@NVE)", sql);
                    cmd.Parameters.AddWithValue("@NVA", NhanVienName.Text);
                    cmd.Parameters.AddWithValue("@NVDC", NhanVienDiaChi.Text);
                    cmd.Parameters.AddWithValue("@NCNS", NhanVienNgaySinh.Value.Date);
                    cmd.Parameters.AddWithValue("@NVSDT", NhanVienSDT.Text);
                    cmd.Parameters.AddWithValue("@NVCMND", NhanVienCMND.Text);
                    cmd.Parameters.AddWithValue("@NVTK", NhanVienTaiKhoan.Text);
                    cmd.Parameters.AddWithValue("@NVP", NhanVienMatKhau.Text);
                    cmd.Parameters.AddWithValue("@NVE", NhanVienEmail.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm nhân viên");
                    sql.Close();
                    DisplayNhanVien();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;

        private void NhanVienList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NhanVienName.Text = NhanVienList.SelectedRows[0].Cells[1].Value.ToString();
            NhanVienDiaChi.Text = NhanVienList.SelectedRows[0].Cells[2].Value.ToString();
            NhanVienNgaySinh.Text = NhanVienList.SelectedRows[0].Cells[3].Value.ToString();
            NhanVienSDT.Text = NhanVienList.SelectedRows[0].Cells[4].Value.ToString();
            NhanVienCMND.Text = NhanVienList.SelectedRows[0].Cells[5].Value.ToString();
            NhanVienTaiKhoan.Text = NhanVienList.SelectedRows[0].Cells[6].Value.ToString();
            NhanVienMatKhau.Text = NhanVienList.SelectedRows[0].Cells[7].Value.ToString();
            NhanVienEmail.Text = NhanVienList.SelectedRows[0].Cells[8].Value.ToString();
            if (NhanVienName.Text == "")
            {
                Key = 0;
            } else { 
                Key = Convert.ToInt32(NhanVienList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (NhanVienName.Text == "" || NhanVienDiaChi.Text == "" || NhanVienSDT.Text == "" || NhanVienTaiKhoan.Text == "" || NhanVienMatKhau.Text == "" || NhanVienEmail.Text == "" || NhanVienCMND.Text == "" || NhanVienNgaySinh.Text == "")
            {
                MessageBox.Show("Nhập thiếu thông tin");
            }
            else
            {
                try
                {
                    sql.Open();
                    SqlCommand cmd = new SqlCommand("Update NhanVientbl set NhanVienName = @NVA , NhanVienDiaChi = @NVDC , NhanVienNgaySinh = @NCNS , NhanVienSDT = @NVSDT, NhanVienCMND = @NVCMND , NhanVienTaiKhoan = @NVTK , NhanVienPass = @NVP, NhanVienEmail = @NVE where NhanVienID = @Key", sql);
                    cmd.Parameters.AddWithValue("@NVA", NhanVienName.Text);
                    cmd.Parameters.AddWithValue("@NVDC", NhanVienDiaChi.Text);
                    cmd.Parameters.AddWithValue("@NCNS", NhanVienNgaySinh.Value.Date);
                    cmd.Parameters.AddWithValue("@NVSDT", NhanVienSDT.Text);
                    cmd.Parameters.AddWithValue("@NVCMND", NhanVienCMND.Text);
                    cmd.Parameters.AddWithValue("@NVTK", NhanVienTaiKhoan.Text);
                    cmd.Parameters.AddWithValue("@NVP", NhanVienMatKhau.Text);
                    cmd.Parameters.AddWithValue("@NVE", NhanVienEmail.Text);
                    cmd.Parameters.AddWithValue("@Key", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã cập nhật nhân viên");
                    sql.Close();
                    DisplayNhanVien();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (Key ==0)
            {
                MessageBox.Show("Chọn nhân viên để xóa ");
            }
            else
            {
                try
                {
                    sql.Open();
                    SqlCommand cmd = new SqlCommand("Delete from Nhanvientbl where NhanVienID = @Key", sql);
                    cmd.Parameters.AddWithValue("@Key", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa thành công");
                    sql.Close();
                    DisplayNhanVien();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Khachhang Obj = new Khachhang();
            Obj.Show();
            this.Hide();
        }

        private void Nhanvien_Load(object sender, EventArgs e)
        {
            Sanpham Obj = new Sanpham();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            HoaDon Obj = new HoaDon();
            Obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sql.Open();
            SqlCommand cmd = sql.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "Select * from NhanVientbl Where NhanVienID = '" + txtTimKiem.Text + "'";
            //cmd.CommandText = "Select * from NhanVientbl Where NhanVienName like N'%" + txtTimKiem.Text + "%'";
            cmd.CommandText = "Select * from   NhanVientbl Where NhanVienID like '" + txtTimKiem.Text + "' or NhanVienName like N'%" + txtTimKiem.Text + "%'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            NhanVienList.DataSource = dt;
            sql.Close();
        }
        private void DeleteSearch_Click(object sender, EventArgs e)
        {
            sql.Open();
            string Query = "Select * from NhanVientbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, sql);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            NhanVienList.DataSource = ds.Tables[0];
            sql.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
    }
}
