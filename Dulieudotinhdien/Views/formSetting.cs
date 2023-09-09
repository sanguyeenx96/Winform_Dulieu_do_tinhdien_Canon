using DevExpress.Utils.MVVM.Services;
using Dulieudotinhdien.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dulieudotinhdien.Views
{
    public partial class formSetting : Form
    {
        private List<string> thongitncaidats;

        public formSetting()
        {
            InitializeComponent();
            thongitncaidats = DataProvider.Instance.getThongtincaidat();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void formSetting_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            string[] model = new string[14];
            for (int i = 0; i < 14; i++)
            {
                model[i] = thongitncaidats[i].ToString();
            }
            txtModel1.Text = model[0];
            txtModel2.Text = model[1];
            txtModel3.Text = model[2];
            txtModel4.Text = model[3];
            txtModel5.Text = model[4];
            txtModel6.Text = model[5];
            txtModel7.Text = model[6];
            txtModel8.Text = model[7];
            txtModel9.Text = model[8];
            txtModel10.Text = model[9];
            txtModel11.Text = model[10];
            txtModel12.Text = model[11];
            txtModel13.Text = model[12];
            txtModel14.Text = model[13];

            txtSoluongCell.Text = thongitncaidats[14].ToString();
            txtThoigianlammoi.Text = thongitncaidats[15].ToString();
        }

        private void textboxMatkhau_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = (textboxMatkhau.Text == thongitncaidats[16]);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            bool result = DulieudotinhdienController.Instance.Doicaidat(txtModel1.Text, txtModel2.Text, txtModel3.Text,
                txtModel4.Text, txtModel5.Text, txtModel6.Text, txtModel7.Text, txtModel8.Text, txtModel9.Text, txtModel10.Text,
                txtModel11.Text, txtModel12.Text, txtModel13.Text, txtModel14.Text, txtSoluongCell.Text, txtThoigianlammoi.Text);
            if (result)
            {
                MessageBox.Show("Cài đặt đã được thay đổi thành công. Phần mềm sẽ khởi động lại sau khi ấn OK.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                Application.Restart();
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}