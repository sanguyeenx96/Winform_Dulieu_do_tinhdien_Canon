using Dulieudotinhdien.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dulieudotinhdien.Controllers
{
    public class DulieudotinhdienController
    {
        private static DulieudotinhdienController instance;

        public static DulieudotinhdienController Instance
        {
            get { if (instance == null) instance = new DulieudotinhdienController(); return DulieudotinhdienController.instance; }
            private set { DulieudotinhdienController.instance = value; }
        }

        private DulieudotinhdienController()
        { }

        public List<Dulieunhansu> LoadTableList_Nhansu(string cell, string ngay, string model)
        {
            List<Dulieunhansu> tableList = new List<Dulieunhansu>();

            DataTable data = DataProvider.Instance.ExecuteQuery("Select * from dotinhdien where cell= '" + cell + "'and ngay='" + ngay + "'and model='" + model + "'");

            foreach (DataRow item in data.Rows)
            {
                Dulieunhansu table = new Dulieunhansu(item);
                tableList.Add(table);
            }

            return tableList;
        }

        public static int TableWidth = 63;
        public static int TableHeight = 40;

        public DataTable load_chart_soluongdadotinhdien(string model, string begin, string end)
        {
            //string query = string.Format("SELECT ngay, cell, COUNT(ngay) AS Soluongnguoi FROM dotinhdien WHERE(model = '" + model + "' and (ngay  BETWEEN('" + begin + "') AND('" + end + "'))) GROUP BY ngay,cell");
            string query = string.Format("SELECT ngay, COUNT(ngay) AS Soluongnguoi FROM dotinhdien WHERE(model = '" + model + "' and dotinhdien= '1' and (ngay  BETWEEN('" + begin + "') AND('" + end + "'))) GROUP BY ngay  ");
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result;
        }

        public DataTable load_chart_soluongchuadotinhdien(string model, string begin, string end)
        {
            //string query = string.Format("SELECT ngay, cell, COUNT(ngay) AS Soluongnguoi FROM dotinhdien WHERE(model = '" + model + "' and (ngay  BETWEEN('" + begin + "') AND('" + end + "'))) GROUP BY ngay,cell");
            string query = string.Format("SELECT ngay, COUNT(ngay) AS Soluongnguoi FROM dotinhdien WHERE(model = '" + model + "' and dotinhdien is null and (ngay  BETWEEN('" + begin + "') AND('" + end + "'))) GROUP BY ngay  ");
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result;
        }

        public DataTable load_chart_tongsoluongnguoi(string model, string begin, string end)
        {
            string query = string.Format("SELECT ngay, COUNT(ngay) AS Tongsoluongnguoi FROM dotinhdien WHERE(model = '" + model + "' and (ngay  BETWEEN('" + begin + "') AND('" + end + "'))) GROUP BY ngay");
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result;
        }

        public DataTable chart_cell_songuoidado(string model, string begin, string end, string cell)
        {
            string query = string.Format("SELECT ngay, COUNT(ngay) AS Songuoidado FROM dotinhdien WHERE(model = '" + model + "' and cell ='" + cell + "' and dotinhdien ='1' and (ngay  BETWEEN('" + begin + "') AND('" + end + "'))) GROUP BY ngay");
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result;
        }

        public DataTable chart_cell_songuoichuado(string model, string begin, string end, string cell)
        {
            string query = string.Format("SELECT ngay, COUNT(ngay) AS Songuoichuado FROM dotinhdien WHERE(model = '" + model + "' and cell ='" + cell + "' and dotinhdien is null and (ngay  BETWEEN('" + begin + "') AND('" + end + "'))) GROUP BY ngay");
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result;
        }

        public DataTable chart_cell_tongsonguoi(string model, string begin, string end, string cell)
        {
            string query = string.Format("SELECT ngay, COUNT(ngay) AS Tongsonguoi FROM dotinhdien WHERE(model = '" + model + "' and cell ='" + cell + "' and (ngay  BETWEEN('" + begin + "') AND('" + end + "'))) GROUP BY ngay");
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result;
        }

        public DataTable chart_model_songuoidado(string model, string ngay)
        {
            string query = string.Format("SELECT ngay, COUNT(ngay) AS soluong FROM dotinhdien WHERE(model = '" + model + "' and dotinhdien ='1' and ngay  ='" + ngay + "') GROUP BY ngay");
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result;
        }

        public DataTable chart_model_songuoichuado(string model, string ngay)
        {
            string query = string.Format("SELECT ngay, COUNT(ngay) AS soluong FROM dotinhdien WHERE(model = '" + model + "' and dotinhdien is null and ngay  ='" + ngay + "') GROUP BY ngay");
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result;
        }

        public DataTable chart_model_tongsonguoi(string model, string ngay)
        {
            string query = string.Format("SELECT ngay, COUNT(ngay) AS soluong FROM dotinhdien WHERE(model = '" + model + "'and ngay  ='" + ngay + "') GROUP BY ngay");
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result;
        }

        public bool Doicaidat(string model1, string model2, string model3, string model4, string model5, string model6
            , string model7, string model8, string model9, string model10, string model11, string model12, string model13
            , string model14, string soluongcellcheck, string thoigianloadlai)
        {
            List<string> thongtincaidats = DataProvider.Instance.getThongtincaidat();
            thongtincaidats[0] = model1;
            thongtincaidats[1] = model2;
            thongtincaidats[2] = model3;
            thongtincaidats[3] = model4;
            thongtincaidats[4] = model5;
            thongtincaidats[5] = model6;
            thongtincaidats[6] = model7;
            thongtincaidats[7] = model8;
            thongtincaidats[8] = model9;
            thongtincaidats[9] = model10;
            thongtincaidats[10] = model11;
            thongtincaidats[11] = model12;
            thongtincaidats[12] = model13;
            thongtincaidats[13] = model14;
            thongtincaidats[14] = soluongcellcheck;
            thongtincaidats[15] = thoigianloadlai;

            string basepath = Application.StartupPath;
            string txtpath = basepath + @"/thongtincaidat.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(txtpath))
                {
                    string dulieu = string.Join(";", thongtincaidats);
                    sw.WriteLine(dulieu);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}