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
    public partial class Khachhang : Form
    {
        public Khachhang()
        {
            InitializeComponent();
            DisplayKhachHang();
        }
        SqlConnection sql = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\QuanLyShopdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayKhachHang()
        {
            sql.Open();
            string Query = "Select * from KhachHangtbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, sql);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            KhachHangList.DataSource = ds.Tables[0];
            sql.Close();
        }
        private void Clear()
        {
            KhachHangName.Text = "";
            KhachHangDiaChi.Text = "";
            KhachHangNgaySinh.Text = "";
            KhachHangSDT.Text = "";
            KhachHangTaiKhoan.Text = "";
            KhachHangPass.Text = "";
            KhachHangEmail.Text = "";
            KhachHangCMND.Text = "";
        }
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (KhachHangName.Text == "" || KhachHangDiaChi.Text == "" || KhachHangSDT.Text == "" || KhachHangTaiKhoan.Text == "" || KhachHangPass.Text == "" || KhachHangEmail.Text == "" || KhachHangCMND.Text == "" || KhachHangNgaySinh.Text == "")
            {
                MessageBox.Show("Nhập thiếu thông tin");
            }
            else
            {
                try
                {
                    sql.Open();
                    SqlCommand cmd = new SqlCommand("insert into KhachHangtbl (KhachHangName,KhachHangDiaChi,KhachHangNgaySinh,KhachHangSDT,KhachHangCMND,KhachHangTaiKhoan,KhachHangPass,KhachHangEmail) values(@KHA,@KHDC,@KHNS,@KHSDT,@KHCMND,@KHTK,@KHP,@KHE)", sql);
                    cmd.Parameters.AddWithValue("@KHA", KhachHangName.Text);
                    cmd.Parameters.AddWithValue("@KHDC", KhachHangDiaChi.Text);
                    cmd.Parameters.AddWithValue("@KHNS", KhachHangNgaySinh.Value.Date);
                    cmd.Parameters.AddWithValue("@KHSDT", KhachHangSDT.Text);
                    cmd.Parameters.AddWithValue("@KHCMND", KhachHangCMND.Text);
                    cmd.Parameters.AddWithValue("@KHTK", KhachHangTaiKhoan.Text);
                    cmd.Parameters.AddWithValue("@KhP", KhachHangPass.Text);
                    cmd.Parameters.AddWithValue("@KHE", KhachHangEmail.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm khách hàng");
                    sql.Close();
                    DisplayKhachHang();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void KhachHangList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            KhachHangName.Text = KhachHangList.SelectedRows[0].Cells[1].Value.ToString();
            KhachHangDiaChi.Text = KhachHangList.SelectedRows[0].Cells[2].Value.ToString();
            KhachHangNgaySinh.Text = KhachHangList.SelectedRows[0].Cells[3].Value.ToString();
            KhachHangSDT.Text = KhachHangList.SelectedRows[0].Cells[4].Value.ToString();
            KhachHangCMND.Text = KhachHangList.SelectedRows[0].Cells[5].Value.ToString();
            KhachHangTaiKhoan.Text = KhachHangList.SelectedRows[0].Cells[6].Value.ToString();
            KhachHangPass.Text = KhachHangList.SelectedRows[0].Cells[7].Value.ToString();
            KhachHangEmail.Text = KhachHangList.SelectedRows[0].Cells[8].Value.ToString();
            if (KhachHangName.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(KhachHangList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Chọn khách hàng để xóa ");
            }
            else
            {
                try
                {
                    sql.Open();
                    SqlCommand cmd = new SqlCommand("Delete from KhachHangtbl where KhachHangID = @Key", sql);
                    cmd.Parameters.AddWithValue("@Key", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa");
                    sql.Close();
                    DisplayKhachHang();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (KhachHangName.Text == "" || KhachHangDiaChi.Text == "" || KhachHangSDT.Text == "" || KhachHangTaiKhoan.Text == "" || KhachHangPass.Text == "" || KhachHangEmail.Text == "" || KhachHangCMND.Text == "" || KhachHangNgaySinh.Text == "")
            {
                MessageBox.Show("Nhập thiếu thông tin");
            }
            else
            {
                try
                {
                    sql.Open();
                    SqlCommand cmd = new SqlCommand("Update KhachHangtbl set KhachHangName = @KHA , KhachHangDiaChi = @KHDC ,KhachHangNgaySinh = @KHNS , KhachHangSDT = @KHSDT, KhachHangCMND = @KHCMND , KhachHangTaiKhoan = @KHTK , KhachHangPass = @KHP, KhachHangEmail = @KHE where KhachHangID = @Key", sql);
                    cmd.Parameters.AddWithValue("@KHA", KhachHangName.Text);
                    cmd.Parameters.AddWithValue("@KHDC", KhachHangDiaChi.Text);
                    cmd.Parameters.AddWithValue("@KHNS", KhachHangNgaySinh.Value.Date);
                    cmd.Parameters.AddWithValue("@KHSDT", KhachHangSDT.Text);
                    cmd.Parameters.AddWithValue("@KHCMND", KhachHangCMND.Text);
                    cmd.Parameters.AddWithValue("@KHTK", KhachHangTaiKhoan.Text);
                    cmd.Parameters.AddWithValue("@KHP", KhachHangPass.Text);
                    cmd.Parameters.AddWithValue("@KHE", KhachHangEmail.Text);
                    cmd.Parameters.AddWithValue("@Key", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã cập nhật nhân viên");
                    sql.Close();
                    DisplayKhachHang();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Sanpham Obj = new Sanpham();
            Obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Nhanvien Obj = new Nhanvien();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            HoaDon Obj = new HoaDon();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
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
            //cmd.CommandText = "Select * from KhachHangtbl Where KhachHangID = '" + txtTimKiem.Text + "'";
            //cmd.CommandText = "Select * from KhachHangtbl Where KhachHangName like N'%" + txtTimKiem.Text + "%'";
            cmd.CommandText = "Select * from   KhachHangtbl Where KhachHangID like '" + txtTimKiem.Text + "' or KhachHangName like N'%" + txtTimKiem.Text + "%'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            KhachHangList.DataSource = dt;
            sql.Close();
        }

        private void DeleteSearch_Click(object sender, EventArgs e)
        {
            sql.Open();
            string Query = "Select * from KhachHangtbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, sql);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            KhachHangList.DataSource = ds.Tables[0];
            sql.Close();
        }
    } 
}

