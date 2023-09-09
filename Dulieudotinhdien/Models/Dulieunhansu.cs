using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dulieudotinhdien.Models
{
    public class Dulieunhansu
    {
        public Dulieunhansu(int id, string station, string hoten, string nangluc, string macode, string chuCvu, string nhiemvu, string ngay, string model, string cell, string status, string gio, string dotinhdien)
        {
            this.Hoten = hoten;
            this.Nangluc = nangluc;
            this.Macode = macode;
            this.Chucvu = chuCvu;
            this.Nhiemvu = nhiemvu;
            this.Ngay = ngay;
            this.Ngay = ngay;
            this.ID = id;
            this.Model = model;
            this.Cell = cell;
            this.Status = status;
            this.Gio = gio;
            this.Dotinhdien = dotinhdien;
        }

        public Dulieunhansu(DataRow row)
        {
            this.Hoten = row["hovaten"].ToString();
            this.Nangluc = row["nangluc"].ToString();
            this.Macode = row["macode"].ToString();
            this.Chucvu = row["chucvu"].ToString();
            this.Nhiemvu = row["nhiemvu"].ToString();
            this.Station = row["station"].ToString();
            this.Ngay = row["ngay"].ToString();
            this.ID = (int)row["id"];
            this.Model = row["model"].ToString();
            this.status = row["trangthai"].ToString();
            this.Cell = row["cell"].ToString();
            this.Gio = row["gio"].ToString();
            this.Dotinhdien = row["dotinhdien"].ToString();
        }

        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string hoten;

        public string Hoten
        {
            get { return hoten; }
            set { hoten = value; }
        }

        private string nangluc;

        public string Nangluc
        {
            get { return nangluc; }
            set { nangluc = value; }
        }

        private string macode;

        public string Macode
        {
            get { return macode; }
            set { macode = value; }
        }

        private string chucvu;

        public string Chucvu
        {
            get { return chucvu; }
            set { chucvu = value; }
        }

        private string nhiemvu;

        public string Nhiemvu
        {
            get { return nhiemvu; }
            set { nhiemvu = value; }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private string gio;

        public string Gio
        {
            get { return gio; }
            set { gio = value; }
        }

        private string ngay;

        public string Ngay
        {
            get { return ngay; }
            set { ngay = value; }
        }

        private string station;

        public string Station
        {
            get { return station; }
            set { station = value; }
        }

        private string model;

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        private string cell;

        public string Cell
        {
            get { return cell; }
            set { cell = value; }
        }

        private string dotinhdien;

        public string Dotinhdien
        {
            get { return dotinhdien; }
            set { dotinhdien = value; }
        }
    }
}