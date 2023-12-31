﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Second_Hand_Clothes_Management_Project.Model;
using Second_Hand_Clothes_Management_Project.View;
using System.Net;
using System.Net.Mail;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;
using System.Windows;

namespace Second_Hand_Clothes_Management_Project.ViewModel
{
    public class Review
    {
        public string Type { get; set; }
        public int Num { get; set; }
        public Review(string Type = "", int Num = 0)
        {
            this.Type = Type;
            this.Num = Num;
        }
    } 
    public class ThongKeViewModel : BaseViewModel
    {
        public DateTime Ngay { get; set; }
        public string TenSP { get; set; }
        public string MaSP { get; set; }
        public long ThisMonth { get; set; }
        public long LastMonth { get; set; }
        private Visibility _Up;
        public Visibility Up { get => _Up; set { _Up = value; OnPropertyChanged(); } }
        private Visibility _Down;
        public Visibility Down { get => _Down; set { _Down = value; OnPropertyChanged(); } }
        public int SL { get; set; }
        public int MaxSell { get; set; }
        public string BestKH { get; set; } 
        public ICommand LoadDT { get; set; } 
        public int MaxNV { get; set; }
        public string NVName { get; set; }
        public string NVBest { get; set; }
        public ICommand LoadDonut { get; set; }
        public ICommand LoadPie { get; set; }
        public List<Review> Reviews { get; set; }
        public ICommand Loadwd { get; set; }
        public ICommand LoadKH { get; set; }
        public ICommand LoadNV { get; set; }
        private Visibility _SetMain;
        public Visibility SetMain { get => _SetMain; set { _SetMain = value; OnPropertyChanged(); } }
        //private ObservableCollection<HOADON> _listHD;
        //public ObservableCollection<HOADON> listHD { get => _listHD; set { _listHD = value; OnPropertyChanged(); } }
        public ThongKeViewModel()
        {
             
            LoadDonut = new RelayCommand<ThongKeView>((p) => true, (p) => DonutChart(p));
            LoadPie = new RelayCommand<ThongKeView>((p) => true, (p) => PieChart(p));
            Loadwd = new RelayCommand<ThongKeView>((p) => true, (p) => _loadwd(p));
            LoadNV = new RelayCommand<ThongKeView>((p) => true, (p) => NVCount(p));
            LoadDT= new RelayCommand<ThongKeView>((p) => true, (p) => DoanhThu(p));
            //listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
        }

        private void DoanhThu(ThongKeView p)
        {
            if (DataProvider.Ins.DB.HOADONs.Where(x => x.NGAYBAN.Month == DateTime.Now.Month).Count() == 0)
            {
                ThisMonth = 0;
            }
            else
            {
                ThisMonth = DataProvider.Ins.DB.HOADONs.Where(x => x.NGAYBAN.Month == DateTime.Now.Month).Sum(x => x.TRIGIA);
            }
            if (DataProvider.Ins.DB.HOADONs.Where(x => x.NGAYBAN.Month == DateTime.Now.Month - 1).Count() == 0)
            {
                LastMonth = 0;
            }
            else
            {
                LastMonth = DataProvider.Ins.DB.HOADONs.Where(x => x.NGAYBAN.Month == DateTime.Now.Month - 1).Sum(x => x.TRIGIA);
            }
            long temp = ThisMonth - LastMonth;
            if (temp >= 0)
            {
                p.DoanhThu.Text = temp.ToString("#,###"); 
                Up = Visibility.Visible;
                Down = Visibility.Collapsed;
            }
            else
            {
                p.DoanhThu.Text = temp.ToString("#,###"); 
                Up = Visibility.Collapsed;
                Down = Visibility.Visible;
            }
        }

        private void NVCount(ThongKeView p)
        {
            MaxNV = int.MinValue;
            foreach (HOADON hd in DataProvider.Ins.DB.HOADONs)
            {
                int temp = DataProvider.Ins.DB.HOADONs.Where(x => x.MAND == hd.MAND).Count();
                if (MaxNV < temp)
                {
                    MaxNV = temp;
                    NVBest = hd.MAND;
                }
            }
            NVName = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.MAND == NVBest).Select(x => x.TENND).FirstOrDefault();
            p.NVBest.Text = NVBest;
            p.NVName.Text = string.Join(" ", NVName.Split().Reverse().Take(2).Reverse());
        }

        private void _loadwd(ThongKeView p)
        { 
            SetMain = Visibility.Visible;
        }

        private void PieChart(ThongKeView p)
        {
            int hoodie = 0, vay = 0, at = 0, yem = 0, sh = 0, quan = 0, sw = 0;
            if (DataProvider.Ins.DB.CTHDs.Where(x => x.SANPHAM.LOAISP == "Hoodie").Count() > 0)
                hoodie = DataProvider.Ins.DB.CTHDs.Where(x => x.SANPHAM.LOAISP == "Hoodie").Sum(x => x.SL);
            if (DataProvider.Ins.DB.CTHDs.Where(x => x.SANPHAM.LOAISP == "Váy").Count() > 0)
                vay = DataProvider.Ins.DB.CTHDs.Where(x => x.SANPHAM.LOAISP == "Váy").Sum(x => x.SL);
            if (DataProvider.Ins.DB.CTHDs.Where(x => x.SANPHAM.LOAISP == "Áo Thun").Count() > 0)
                at = DataProvider.Ins.DB.CTHDs.Where(x => x.SANPHAM.LOAISP == "Áo Thun").Sum(x => x.SL);
            if (DataProvider.Ins.DB.CTHDs.Where(x => x.SANPHAM.LOAISP == "Yếm").Count() > 0)
                yem = DataProvider.Ins.DB.CTHDs.Where(x => x.SANPHAM.LOAISP == "Yếm").Sum(x => x.SL);
            if (DataProvider.Ins.DB.CTHDs.Where(x => x.SANPHAM.LOAISP == "Sơ mi").Count() > 0)
                sh = DataProvider.Ins.DB.CTHDs.Where(x => x.SANPHAM.LOAISP == "Sơ mi").Sum(x => x.SL);
            if (DataProvider.Ins.DB.CTHDs.Where(x => x.SANPHAM.LOAISP == "Trackpant").Count() > 0)
                quan = DataProvider.Ins.DB.CTHDs.Where(x => x.SANPHAM.LOAISP == "Trackpant").Sum(x => x.SL);
            if (DataProvider.Ins.DB.CTHDs.Where(x => x.SANPHAM.LOAISP == "Sweater").Count() > 0)
                sw = DataProvider.Ins.DB.CTHDs.Where(x => x.SANPHAM.LOAISP == "Sweater").Sum(x => x.SL);
            Reviews = new List<Review>();
            Review r1 = new Review()
            {
                Type = "Hoodie",
                Num = hoodie
            };
            Review r2 = new Review()
            {
                Type = "Váy",
                Num = vay
            };
            Review r3 = new Review()
            {
                Type = "Áo Thun",
                Num = at
            };
            Review r4 = new Review()
            {
                Type = "Yếm",
                Num = yem
            };
            Review r5 = new Review()
            {
                Type = "Sơ mi",
                Num = sw
            }; Review r6 = new Review()
            {
                Type = "Trackpant",
                Num = quan
            };
            Review r7 = new Review()
            {
                Type = "Sweater",
                Num = sw
            };
            Reviews.Add(r1);
            Reviews.Add(r2);
            Reviews.Add(r3);
            Reviews.Add(r4);
            Reviews.Add(r5);
            Reviews.Add(r6);
            Reviews.Add(r7);
            p.Pie.ItemsSource = Reviews;
            p.Pie.AdornmentsInfo = new Syncfusion.UI.Xaml.Charts.ChartAdornmentInfo()
            {
                ShowLabel = true,
                ShowConnectorLine = true,
                Margin = new Thickness(2)
            };
            p.Pie.ExplodeOnMouseClick = true;
        }

        private void DonutChart(ThongKeView p)
        {
            var query = from a in DataProvider.Ins.DB.CTHDs
                        join b in DataProvider.Ins.DB.SANPHAMs on a.MASP equals b.MASP
                        where a.MASP == b.MASP && a.HOADON.NGAYBAN.Month == DateTime.Now.Month && a.HOADON.NGAYBAN.Year == DateTime.Now.Year
                        select new ThongKeViewModel()
                        {
                            SL = a.SL,
                            MaSP = a.MASP,
                            TenSP = b.TENSP,
                            Ngay = a.HOADON.NGAYBAN
                        };
            string sp1 = "", sp2 = "", sp3 = "", sp4 = "", sp5 = "";
            int max1 = 0;
            foreach (ThongKeViewModel obj in query)
            {
                int temp = query.Where(x => x.TenSP == obj.TenSP && x.Ngay.Month == DateTime.Now.Month && x.Ngay.Year == DateTime.Now.Year).Sum(x => x.SL);
                if (max1 < temp)
                {
                    max1 = temp;
                    sp1 = obj.TenSP;
                }
            }
            int max2 = 0;
            foreach (ThongKeViewModel obj in query)
            {
                if (obj.TenSP == sp1) continue;
                int temp = query.Where(x => x.TenSP == obj.TenSP && x.Ngay.Month == DateTime.Now.Month && x.Ngay.Year == DateTime.Now.Year).Sum(x => x.SL);
                if (max2 < temp)
                {
                    max2 = temp;
                    sp2 = obj.TenSP;
                }
            }
            int max3 = 0;
            foreach (ThongKeViewModel obj in query)
            {
                if (obj.TenSP == sp1 || obj.TenSP == sp2) continue;
                int temp = query.Where(x => x.TenSP == obj.TenSP && x.Ngay.Month == DateTime.Now.Month && x.Ngay.Year == DateTime.Now.Year).Sum(x => x.SL);
                if (max3 < temp)
                {
                    max3 = temp;
                    sp3 = obj.TenSP;
                }
            }
            int max4 = 0;
            foreach (ThongKeViewModel obj in query)
            {
                if (obj.TenSP == sp1 || obj.TenSP == sp2 || obj.TenSP == sp3) continue;
                int temp = query.Where(x => x.TenSP == obj.TenSP && x.Ngay.Month == DateTime.Now.Month && x.Ngay.Year == DateTime.Now.Year).Sum(x => x.SL);
                if (max4 < temp)
                {
                    max4 = temp;
                    sp4 = obj.TenSP;
                }
            }
            int max5 = 0;
            foreach (ThongKeViewModel obj in query)
            {
                if (obj.TenSP == sp1 || obj.TenSP == sp2 || obj.TenSP == sp3 || obj.TenSP == sp4) continue;
                int temp = query.Where(x => x.TenSP == obj.TenSP && x.Ngay.Month == DateTime.Now.Month && x.Ngay.Year == DateTime.Now.Year).Sum(x => x.SL);
                if (max5 < temp)
                {
                    max5 = temp;
                    sp5 = obj.TenSP;
                }
            }
            Reviews = new List<Review>();
            Review r1 = new Review()
            {
                Type = sp1,
                Num = max1
            };
            Review r2 = new Review()
            {
                Type = sp2,
                Num = max2
            };
            Review r3 = new Review()
            {
                Type = sp3,
                Num = max3
            };
            Review r4 = new Review()
            {
                Type = sp4,
                Num = max4
            };
            Review r5 = new Review()
            {
                Type = sp5,
                Num = max5
            };
            Reviews.Add(r1);
            Reviews.Add(r2);
            Reviews.Add(r3);
            Reviews.Add(r4);
            Reviews.Add(r5);
            p.Donut.ItemsSource = Reviews;
            p.Donut.AdornmentsInfo = new Syncfusion.UI.Xaml.Charts.ChartAdornmentInfo()
            {
                ShowLabel = true,
                ShowConnectorLine = true,
                Margin = new Thickness(2)
            };
        }
    }

}
