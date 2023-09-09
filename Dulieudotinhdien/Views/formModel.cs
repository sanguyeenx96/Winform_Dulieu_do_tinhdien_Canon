using DevExpress.Data.Filtering.Helpers;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Design;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Native;
using Dulieudotinhdien.Controllers;
using Dulieudotinhdien.Models;
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
    public partial class formModel : Form
    {
        private List<string> thongitncaidats;
        private string tenModel;
        public string dateToday = DateTime.Now.ToString("yyyy/MM/dd");

        public formModel(string Model)
        {
            tenModel = Model;
            thongitncaidats = DataProvider.Instance.getThongtincaidat();
            InitializeComponent();
        }

        private void windowsUIButtonPanel1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Clear()
        {
            flowLayoutPanel.Controls.Clear();
            chartCell1.Enabled = false;
            chartCell2.Enabled = false;
            chartCell3.Enabled = false;
            chartCell4.Enabled = false;
            btnChitiet_c1.Enabled = false;
            btnChitiet_c2.Enabled = false;
            btnChitiet_c3.Enabled = false;
            btnChitiet_c4.Enabled = false;
            txtTencell.Text = "--";
            txtTongso.Text = "--";
            txtChuado.Text = "--";
            txtDado.Text = "--";
            txtHovaten.Text = "--";
            txtChucvu.Text = "--";
            txtId.Text = "--";
            txtStation.Text = "--";
            labelChitetcell.Text = "Không có dữ liệu!";
        }

        private void formModel_Load_1(object sender, EventArgs e)
        {
            dateEditXemtheongay.EditValue = DateTime.Now;
            Clear();
            lbChuado.BackColor = Color.FromArgb(0xC0, 0x50, 0x4D);
            lbChuado.ForeColor = Color.WhiteSmoke;
            txtLabel.Text = "Dữ liệu chi tiết " + tenModel;
            CheckDataForCells(dateToday, dateToday);
        }

        private void CheckDataForCells(string begin, string end)
        {
            string model = tenModel;
            List<string> cellcodulieu = new List<string>();
            for (int cell = 1; cell <= 4; cell++)
            {
                DataTable data = DulieudotinhdienController.Instance.chart_cell_tongsonguoi(model, begin, end, cell.ToString());
                if (data != null && data.Rows.Count > 0)
                {
                    cellcodulieu.Add(cell.ToString());
                    switch (cell)
                    {
                        case 1:
                            chartCell1.Enabled = true;
                            btnChitiet_c1.Enabled = true;
                            ShowChiTietCell("1");
                            labelChitetcell.Text = "Chi tiết Cell 1 - " + tenModel;
                            break;

                        case 2:
                            chartCell2.Enabled = true;
                            btnChitiet_c2.Enabled = true;
                            break;

                        case 3:
                            chartCell3.Enabled = true;
                            btnChitiet_c3.Enabled = true;
                            break;

                        case 4:
                            chartCell4.Enabled = true;
                            btnChitiet_c4.Enabled = true;
                            break;

                        default:
                            break;
                    }
                }
            }
            UpdateChartCell(chartCell1, model, begin, end, "1");
            UpdateChartCell(chartCell2, model, begin, end, "2");
            UpdateChartCell(chartCell3, model, begin, end, "3");
            UpdateChartCell(chartCell4, model, begin, end, "4");
            labelDulieutrongngay.Text = "Dữ liệu " + tenModel + " trong ngày " + begin + " - " + cellcodulieu.Count().ToString() + " Cell hoạt động";
        }

        private void UpdateChartCell(ChartControl chartControl, string model, string begin, string end, string cell)
        {
            chartControl.Series["Đã đo"].DataSource = DulieudotinhdienController.Instance.chart_cell_songuoidado(model, begin, end, cell);
            chartControl.Series["Đã đo"].ArgumentDataMember = "ngay";
            chartControl.Series["Đã đo"].ValueDataMembers[0] = "Songuoidado";
            chartControl.Series["Chưa đo"].DataSource = DulieudotinhdienController.Instance.chart_cell_songuoichuado(model, begin, end, cell);
            chartControl.Series["Chưa đo"].ArgumentDataMember = "ngay";
            chartControl.Series["Chưa đo"].ValueDataMembers[0] = "Songuoichuado";
            chartControl.Series["Tổng số"].DataSource = DulieudotinhdienController.Instance.chart_cell_tongsonguoi(model, begin, end, cell);
            chartControl.Series["Tổng số"].ArgumentDataMember = "ngay";
            chartControl.Series["Tổng số"].ValueDataMembers[0] = "Tongsonguoi";
        }

        private void btnChitiet_c1_Click(object sender, EventArgs e)
        {
            labelChitetcell.Text = "Chi tiết Cell 1 - " + tenModel;
            ShowChiTietCell("1");
        }

        private void btnChitiet_c2_Click(object sender, EventArgs e)
        {
            labelChitetcell.Text = "Chi tiết Cell 2 - " + tenModel;

            ShowChiTietCell("2");
        }

        private void btnChitiet_c3_Click(object sender, EventArgs e)
        {
            labelChitetcell.Text = "Chi tiết Cell 3 - " + tenModel;
            ShowChiTietCell("3");
        }

        private void btnChitiet_c4_Click(object sender, EventArgs e)
        {
            labelChitetcell.Text = "Chi tiết Cell 4 - " + tenModel;
            ShowChiTietCell("4");
        }

        private void ShowChiTietCell(string tenCell)
        {
            txtTencell.Text = "--";
            txtTongso.Text = "0";
            txtChuado.Text = "0";
            txtDado.Text = "0";
            txtHovaten.Text = "--";
            txtChucvu.Text = "--";
            txtId.Text = "--";
            txtStation.Text = "--";
            int chuadotinhdien = 0;
            int dadotinhdien = 0;
            string ngay = dateToday;
            string model = tenModel;
            flowLayoutPanel.Controls.Clear();
            txtTencell.Text = tenCell;
            txtTongso.Text = ((int)DataProvider.Instance.ExecuteScalar("SELECT COUNT (*) FROM dotinhdien where cell= '" + tenCell + "'and ngay='" + ngay + "'and model='" + model + "'")).ToString() + " người";
            List<Dulieunhansu> list = DulieudotinhdienController.Instance.LoadTableList_Nhansu(tenCell, ngay, model);
            foreach (Dulieunhansu item in list)
            {
                string maid = item.Macode;
                DataTable trangthaidotinhdien = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.dotinhdien WHERE macode='" + maid + "' AND ngay='" + dateToday + "'");
                string trangthai = trangthaidotinhdien.Rows[0][14].ToString();
                if (trangthai == "1")
                {
                    dadotinhdien++;
                    Button btn = new Button() { Width = DulieudotinhdienController.TableWidth, Height = DulieudotinhdienController.TableHeight };
                    btn.Text = item.Station + Environment.NewLine + item.Chucvu;
                    btn.BackColor = Color.FromArgb(0x9B, 0xBB, 0x59);
                    btn.ForeColor = Color.Black;
                    btn.Click += btn_Click;
                    btn.Tag = item;
                    flowLayoutPanel.Controls.Add(btn);
                    txtDado.Text = dadotinhdien.ToString() + " người";
                }
                if (trangthai == "")
                {
                    chuadotinhdien++;
                    Button btn = new Button() { Width = DulieudotinhdienController.TableWidth, Height = DulieudotinhdienController.TableHeight };
                    btn.Text = item.Station + Environment.NewLine + item.Chucvu;
                    btn.BackColor = Color.FromArgb(0xC0, 0x50, 0x4D);
                    btn.ForeColor = Color.White;
                    btn.Click += btn_Click;
                    btn.Tag = item;
                    flowLayoutPanel.Controls.Add(btn);
                    txtChuado.Text = chuadotinhdien.ToString() + " người";
                }
            }
        }

        private int ck = 0;

        private void btn_Click(object sender, EventArgs e)
        {
            if (ck == 0)
            {
                ck = 1;
            }
            if (ck == 1)
            {
                string hoten = ((sender as Button).Tag as Dulieunhansu).Hoten;
                string chucvu = ((sender as Button).Tag as Dulieunhansu).Chucvu;
                string congdoan = ((sender as Button).Tag as Dulieunhansu).Station;
                string maid = ((sender as Button).Tag as Dulieunhansu).Macode;
                txtHovaten.Text = hoten;
                txtChucvu.Text = chucvu;
                txtStation.Text = congdoan;
                txtId.Text = maid;
            }
        }

        private void btnXacnhanXemtheongay_Click(object sender, EventArgs e)
        {
            Clear();
            DateTime dt = Convert.ToDateTime(dateEditXemtheongay.EditValue);
            string datepick = dt.ToString("yyyy/MM/dd");
            CheckDataForCells(datepick, datepick);
        }

        private void btnLocXacnhan_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime tn = Convert.ToDateTime(dateEdit_Loctungay.EditValue);
                string begin = tn.ToString("yyyy/MM/dd");
                DateTime dn = Convert.ToDateTime(dateEdit_Locdenngay.EditValue);
                string end = dn.ToString("yyyy/MM/dd");
                UpdateChartCell(chartLoc_c1, tenModel, begin, end, "1");
                UpdateChartCell(chartLoc_c2, tenModel, begin, end, "2");
                UpdateChartCell(chartLoc_c3, tenModel, begin, end, "3");
                UpdateChartCell(chartLoc_c4, tenModel, begin, end, "4");
            }
            catch
            {
                MessageBox.Show("Bạn chưa chọn ngày tháng lọc dữ liệu!");
            }
        }
    }
}