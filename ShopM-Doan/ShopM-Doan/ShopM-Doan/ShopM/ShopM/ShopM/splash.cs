using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopM
{
    public partial class splash : Form
    {
        public splash()
        {
            InitializeComponent();
            timer1.Start();
        }

        int batdau = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            batdau += 1;
            LoadingBar.Value = batdau;
            Loadinglbl.Text = batdau + "%";
            if (LoadingBar.Value == 100)
            {
                LoadingBar.Value = 0;
                Login obj = new Login();
                obj.Show();
                this.Hide();
                timer1.Stop();
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void LoadingBar_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
