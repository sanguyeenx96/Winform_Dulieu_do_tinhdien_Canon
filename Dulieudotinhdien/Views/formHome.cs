using DevExpress.XtraBars.FluentDesignSystem;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using Dulieudotinhdien.Controllers;
using Dulieudotinhdien.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;

namespace Dulieudotinhdien
{
    public partial class formHome : DevExpress.XtraEditors.XtraForm
    {
        private List<string> thongitncaidats;
        private ChartControl[] chartControls;
        private SimpleButton[] buttons;
        private int countdownSeconds;

        public formHome()
        {
            InitializeComponent();
            thongitncaidats = DataProvider.Instance.getThongtincaidat();
            foreach (var i in thongitncaidats)
            {
                if (string.IsNullOrEmpty(i))
                {
                    MessageBox.Show("Thiếu dữ liệu cài đặt!");
                }
            }
            chartControls = new ChartControl[]
               {
                    c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14
               };
            buttons = new SimpleButton[]
                {
                    btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btn10, btn11, btn12, btn13, btn14
                };
            countdownSeconds = Convert.ToInt32(thongitncaidats[15]);
            timerRefresh.Interval = countdownSeconds;
        }

        private void UpdateChart(string model, ChartControl chartControl, SimpleButton btn)
        {
            string ngay = DateTime.Now.ToString("yyyy/MM/dd");
            DataTable data = DulieudotinhdienController.Instance.chart_model_tongsonguoi(model, ngay);
            chartControl.Titles.Clear();
            chartControl.Titles.Add(new ChartTitle());
            chartControl.Titles[0].Text = model;
            chartControl.Titles[0].Dock = ChartTitleDockStyle.Bottom;

            if (data != null && data.Rows.Count > 0)
            {
                chartControl.Series["Đã đo"].DataSource = DulieudotinhdienController.Instance.chart_model_songuoidado(model, ngay);
                chartControl.Series["Đã đo"].ArgumentDataMember = "ngay";
                chartControl.Series["Đã đo"].ValueDataMembers[0] = "soluong";

                chartControl.Series["Tổng số"].DataSource = DulieudotinhdienController.Instance.chart_model_tongsonguoi(model, ngay);
                chartControl.Series["Tổng số"].ArgumentDataMember = "ngay";
                chartControl.Series["Tổng số"].ValueDataMembers[0] = "soluong";

                chartControl.Series["Chưa đo"].DataSource = DulieudotinhdienController.Instance.chart_model_songuoichuado(model, ngay);
                chartControl.Series["Chưa đo"].ArgumentDataMember = "ngay";
                chartControl.Series["Chưa đo"].ValueDataMembers[0] = "soluong";
                btn.Enabled = true;
                btn.Name = model;
            }
            else
            {
                btn.Enabled = false;
            }
        }

        private void formHome_Load(object sender, EventArgs e)
        {
            txtLabel.Text = "Dữ liệu đo tĩnh điện toàn nhà máy hôm nay " + DateTime.Now.ToString("yyyy/MM/dd");
            string[] model = new string[14];
            for (int i = 0; i < 14; i++)
            {
                model[i] = thongitncaidats[i].ToString();
                UpdateChart(model[i], chartControls[i], buttons[i]);
            }
            foreach (SimpleButton button in buttons)
            {
                button.Click += new EventHandler(button_Click);
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            SimpleButton clickedButton = (SimpleButton)sender;
            string buttonName = clickedButton.Name;
            openFormModel(buttonName);
        }

        private void openFormModel(string Model)
        {
            formModel form = new formModel(Model);
            form.ShowDialog();
        }

        private void timerWatch_Tick(object sender, EventArgs e)
        {
            txtWatch.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            string[] model = new string[14];
            for (int i = 0; i < 14; i++)
            {
                model[i] = thongitncaidats[i].ToString();
                UpdateChart(model[i], chartControls[i], buttons[i]);
            }
        }

        private void btnCaidat_Click(object sender, EventArgs e)
        {
            formSetting form = new formSetting();
            form.ShowDialog();
        }

        private void timerCountdown_Tick(object sender, EventArgs e)
        {
            countdownSeconds = countdownSeconds - 1000;
            lblCountdown.Text = $"{countdownSeconds / 1000}s";
            if (countdownSeconds == 0)
            {
                lblCountdown.Text = "Cập nhật...";
                countdownSeconds = Convert.ToInt32(thongitncaidats[15]);
            }
        }
    }
}