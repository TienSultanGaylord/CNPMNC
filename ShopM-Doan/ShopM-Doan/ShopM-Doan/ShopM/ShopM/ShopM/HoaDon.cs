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
    public partial class HoaDon : Form
    {
        public HoaDon()
        {
            InitializeComponent();
            //NhanVienName.Text = Login.NhanVien;
            GetCustomers();
            DisplayProduct();
            DisplayTotal();
        }
        SqlConnection sql = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\QuanLyShopdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void GetCustomers()
        {
            sql.Open();
            SqlCommand cmd = new SqlCommand("Select KhachHangID from KhachHangtbl ", sql);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("KhachHangID", typeof(int));
            dt.Load(Rdr);
            KhachHangID.ValueMember = "KhachHangID";
            KhachHangID.DataSource = dt;
            sql.Close();
        }
        private void DisplayProduct()
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
        private void DisplayTotal()
        {
            sql.Open();
            string Query = "Select * from HoaDontb";
            SqlDataAdapter sda = new SqlDataAdapter(Query, sql);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TotalView.DataSource = ds.Tables[0];
            sql.Close();
        }
        private void GetCustName()
        {
            if (sql.State != ConnectionState.Open)
            sql.Open();
            string Query = "Select * from KhachHangtbl where KhachHangID = " + KhachHangID.SelectedValue.ToString();
            SqlCommand cmd = new SqlCommand(Query, sql);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                KhachHangName.Text = dr["KhachHangName"].ToString();
            }
            sql.Close();
        }
        private void UpdateStock()
        {
            try
            {
                int NewQty = Stock - Convert.ToInt32(SoluongSanPham.Text);
                sql.Open();
                SqlCommand cmd = new SqlCommand("Update SanPhamtbl set SoLuongSanPham=@PQ where SanPhamID=@PKey", sql);
                cmd.Parameters.AddWithValue("@PQ", NewQty);
                cmd.Parameters.AddWithValue("@PKey", Key);
                cmd.ExecuteNonQuery();
                sql.Close();
                DisplayProduct();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        int n = 0, GrdTotal = 0;
        private void Addbtn_Click(object sender, EventArgs e)
        {
            if(SoluongSanPham.Text == "" || Convert.ToInt32(SoluongSanPham.Text) > Stock)
            {
                MessageBox.Show("Không đủ");
            }else if(SoluongSanPham.Text == "" || Key == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm");
            }else
            {

                
                bool existed = false;
                int i = 0;
                foreach (DataGridViewRow row in HoadonView.Rows)
                {

                    if (row.Cells[1].Value != null)
                    {
                        if (row.Cells[1].Value.ToString() == SanPhamName.Text)
                        {
                            existed = true;
                            break;
                        }
                        i++;
                    }
                }
                if(existed)
                {
                    int newTotal = Convert.ToInt32(SoluongSanPham.Text) * Convert.ToInt32(GiaSanPham.Text);
                    int newSL = Convert.ToInt32(SoluongSanPham.Text);
                    HoadonView.Rows[i].Cells[2].Value = newSL + Convert.ToInt32(HoadonView.Rows[i].Cells[2].Value.ToString());
                    HoadonView.Rows[i].Cells[4].Value = newTotal + Convert.ToInt32(HoadonView.Rows[i].Cells[4].Value.ToString());                   
                    UpdateStock();
                    Reset();
                }
                else
                {
                    int total = Convert.ToInt32(SoluongSanPham.Text) * Convert.ToInt32(GiaSanPham.Text);
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(HoadonView);
                    newRow.Cells[0].Value = (++n);
                    newRow.Cells[1].Value = SanPhamName.Text;
                    newRow.Cells[2].Value = SoluongSanPham.Text;
                    newRow.Cells[3].Value = GiaSanPham.Text;
                    newRow.Cells[4].Value = total;
                    GrdTotal = GrdTotal + total;
                    HoadonView.Rows.Add(newRow);
                    UpdateStock();
                    Reset();
                }
            }
        }
        private void KhachHangIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCustName();
        }
        int Key = 0, Stock = 0;
        private void Reset()
        {
            GiaSanPham.Text = "";
            SanPhamName.Text = "";
            SoluongSanPham.Text = "";
            Stock = 0;
            Key = 0;
        }

        private void Resetbtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void SanphamList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SanPhamName.Text = SanphamList.SelectedRows[0].Cells[1].Value.ToString();
            //Stock = Convert.ToInt32(SanphamList.SelectedRows[0].Cells[3].Value.ToString());
            GiaSanPham.Text = SanphamList.SelectedRows[0].Cells[4].Value.ToString();
            if(SanPhamName.Text == "")
            {
                Key = 0;
                Stock = 0;           
            }
            else
            {
                Stock = Convert.ToInt32(SanphamList.SelectedRows[0].Cells[3].Value.ToString());
                Key = Convert.ToInt32(SanphamList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void InsertBill()
        {
            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand("insert into HoaDontb (NgayHoaDon,KhachHangID,KhachHangName,PhuongThucThanhToan,TongCong) values(@NHD,@KHID,@KHN,@PTTT,@TC)", sql);
                cmd.Parameters.AddWithValue("@NHD", DateTime.Today.Date);
                cmd.Parameters.AddWithValue("@KHID", KhachHangID.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@KHN", KhachHangName.Text);
                cmd.Parameters.AddWithValue("@PTTT", PTTT.Text);
                //cmd.Parameters.AddWithValue("@NVN", SanPhamName.Text);
                //cmd.Parameters.AddWithValue("@SPN", SanPhamName.Text);
                //cmd.Parameters.AddWithValue("@SPG", GiaSanPham.Text);
                //cmd.Parameters.AddWithValue("@SPSL", SoluongSanPham.Text);
                cmd.Parameters.AddWithValue("@TC", GrdTotal);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã lưu hóa đơn");
                sql.Close();
                DisplayTotal();
                //Clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void Printbtn_Click(object sender, EventArgs e)
        {
            InsertBill();
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        string spten;
        int spid, spsl, spgia, tong, pos = 60;

        private void HoadonView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           /* SanPhamName.Text = HoadonView.SelectedRows[0].Cells[1].Value.ToString();
            SoluongSanPham.Text = HoadonView.SelectedRows[0].Cells[2].Value.ToString();
            GiaSanPham.Text = HoadonView.SelectedRows[0].Cells[3].Value.ToString();
            int total = Convert.ToInt32(SoluongSanPham.Text) * Convert.ToInt32(GiaSanPham.Text);
            total = (int)HoadonView.SelectedRows[0].Cells[4].Value;

            if (SanPhamName.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(HoadonView.SelectedRows[0].Cells[0].Value.ToString());
            }*/
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void ClearHoaDon()
        {   
            KhachHangID.Text = "";
            KhachHangName.Text = "";
            GrdTotal = 0;
        }
        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Chọn hóa đơn để xóa");
            }
            else
            {
                try
                {                    
                    sql.Open();
                    SqlCommand cmd = new SqlCommand("Delete from HoaDontb where HoaDonID = @Key", sql);
                    cmd.Parameters.AddWithValue("@Key", Key);
                    cmd.ExecuteNonQuery();
                    int rowindex = TotalView.CurrentCell.RowIndex;
                    TotalView.Rows.RemoveAt(rowindex);
                    MessageBox.Show("Xóa thành công");
                    sql.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }


        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            sql.Open();
            SqlCommand cmd = sql.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //.CommandText = "Select * from HoaDontb Where HoaDonID = '" + txtTimKiem.Text + "'";
            //cmd.CommandText = "Select * from KhachHangtbl Where KhachHangName like N'%" + txtTimKiem.Text + "%'";
            cmd.CommandText = "Select * from HoaDontb Where HoaDonID like '" + txtTimKiem.Text + "' or KhachHangName like N'%" + txtTimKiem.Text + "%' or KhachHangID  like N'%" + txtTimKiem.Text + "%'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            TotalView.DataSource = dt;
            sql.Close();
        }

        private void DeleteSearch_Click(object sender, EventArgs e)
        {
            sql.Open();
            string Query = "Select * from HoaDontb";
            SqlDataAdapter sda = new SqlDataAdapter(Query, sql);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TotalView.DataSource = ds.Tables[0];
            sql.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
        }

        private void label23_Click(object sender, EventArgs e)
        {
            Khachhang Obj = new Khachhang();
            Obj.Show();
            this.Hide();
        }

        private void label26_Click(object sender, EventArgs e)
        {
            Nhanvien Obj = new Nhanvien();
            Obj.Show();
            this.Hide();
        }

        private void label24_Click(object sender, EventArgs e)
        {
            Sanpham Obj = new Sanpham();
            Obj.Show();
            this.Hide();
        }

        private void TotalView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            KhachHangID.Text = TotalView.SelectedRows[0].Cells[2].Value.ToString();
            KhachHangName.Text = TotalView.SelectedRows[0].Cells[3].Value.ToString();
            if (KhachHangID.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(TotalView.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Di Cho Gium", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.DeepSkyBlue, new Point(80));
            e.Graphics.DrawString("ID SanPham Gia SoLuong Tong", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.DeepSkyBlue, new Point(26, 40));
            foreach (DataGridViewRow row in HoadonView.Rows)
            {
                spid = Convert.ToInt32(row.Cells[0].Value);
                spten = "" + row.Cells[1].Value;
                spgia = Convert.ToInt32(row.Cells[3].Value);
                spsl = Convert.ToInt32(row.Cells[2].Value);
                tong = Convert.ToInt32(row.Cells[4].Value);
                if (spid != 0)
                {
                    e.Graphics.DrawString("" + spid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(26, pos));
                    e.Graphics.DrawString("" + spten, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(45, pos));
                    e.Graphics.DrawString("" + spgia, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(120, pos));
                    e.Graphics.DrawString("" + spsl, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(170, pos));
                    e.Graphics.DrawString("" + tong, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(235, pos));
                    pos = pos + 20;
                }
            }
            e.Graphics.DrawString("Tong Cong: " + GrdTotal + " VND" , new Font("Century Gothic", 12, FontStyle.Bold), Brushes.DeepSkyBlue, new Point(50, pos + 50));
            e.Graphics.DrawString("******************************************", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.DeepSkyBlue, new Point(10, pos));
            HoadonView.Rows.Clear();
            HoadonView.Refresh();
            pos = 100;
            GrdTotal = 0;
            n = 0;
        }
    }
}
