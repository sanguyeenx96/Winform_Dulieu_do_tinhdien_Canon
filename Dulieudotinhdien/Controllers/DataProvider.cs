using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dulieudotinhdien.Controllers
{
    public class DataProvider
    {
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider()
        { }

        public List<string> getThongtincaidat()
        {
            string basepath = Application.StartupPath;
            string txtpath = basepath + @"/thongtincaidat.txt";
            List<string> thongtincaidats = new List<string>();
            try
            {
                if (File.Exists(txtpath))
                {
                    using (StreamReader sr = new StreamReader(txtpath))
                    {
                        while (sr.Peek() >= 0)
                        {
                            string line1 = sr.ReadLine();
                            string[] txtsplit = line1.Split(';');
                            string model1 = txtsplit[0].ToString();
                            string model2 = txtsplit[1].ToString();
                            string model3 = txtsplit[2].ToString();
                            string model4 = txtsplit[3].ToString();
                            string model5 = txtsplit[4].ToString();
                            string model6 = txtsplit[5].ToString();
                            string model7 = txtsplit[6].ToString();
                            string model8 = txtsplit[7].ToString();
                            string model9 = txtsplit[8].ToString();
                            string model10 = txtsplit[9].ToString();
                            string model11 = txtsplit[10].ToString();
                            string model12 = txtsplit[11].ToString();
                            string model13 = txtsplit[12].ToString();
                            string model14 = txtsplit[13].ToString();
                            string soluongcellcheck = txtsplit[14].ToString();
                            string thoigianloadlai = txtsplit[15].ToString();
                            string matkhauungdung = txtsplit[16].ToString();
                            thongtincaidats.Add(model1);
                            thongtincaidats.Add(model2);
                            thongtincaidats.Add(model3);
                            thongtincaidats.Add(model4);
                            thongtincaidats.Add(model5);
                            thongtincaidats.Add(model6);
                            thongtincaidats.Add(model7);
                            thongtincaidats.Add(model8);
                            thongtincaidats.Add(model9);
                            thongtincaidats.Add(model10);
                            thongtincaidats.Add(model11);
                            thongtincaidats.Add(model12);
                            thongtincaidats.Add(model13);
                            thongtincaidats.Add(model14);
                            thongtincaidats.Add(soluongcellcheck);
                            thongtincaidats.Add(thoigianloadlai);
                            thongtincaidats.Add(matkhauungdung);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return (thongtincaidats);
        }

        private static string getconnectionstring()
        {
            string basepath = Application.StartupPath;
            string txtpath = basepath + @"/setting.txt";
            string connectstring = "";
            try
            {
                if (File.Exists(txtpath))
                {
                    using (StreamReader sr = new StreamReader(txtpath))
                    {
                        while (sr.Peek() >= 0)
                        {
                            string ss = sr.ReadLine();
                            string[] txtsplit = ss.Split(';');
                            string sever = txtsplit[0].ToString();
                            string userid = txtsplit[1].ToString();
                            string password = txtsplit[2].ToString();
                            string database = txtsplit[3].ToString();
                            connectstring = sever + ";" + userid + ";" + password + ";" + database;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return (connectstring);
        }

        public DataTable ExecuteQuery(string query, object[] paramater = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(getconnectionstring()))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (paramater != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, paramater[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }

        public int ExcuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(getconnectionstring()))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteNonQuery();
                connection.Close();
            }
            return data;
        }

        public object ExecuteScalar(string query, object[] paramater = null)
        {
            object data = null;
            using (SqlConnection connection = new SqlConnection(getconnectionstring()))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (paramater != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, paramater[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteScalar();
            }
            return data;
        }
    }
}