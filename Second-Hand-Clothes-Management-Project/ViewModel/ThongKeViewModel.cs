using System;
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
        public string TenSP { get; set; }
        public string MaSP { get; set; }
        public int SL { get; set; }
        public int MaxSell { get; set; }
        public string BestKH { get; set; }
        public string KHName { get; set; }
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
            LoadKH = new RelayCommand<ThongKeView>((p) => true, (p) => KHCount(p));
            //listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
        }

        private void KHCount(ThongKeView p)
        {
            MaxSell = 0;
            foreach (MUAHANG hd in DataProvider.Ins.DB.MUAHANGs.Where(x => x.NGAYBAN.Month == DateTime.Now.Month))
            {
                int temp = DataProvider.Ins.DB.MUAHANGs.Where(x => x.MAKH == hd.MAKH).Count();
                if (MaxSell < temp)
                {
                    MaxSell = temp;
                    BestKH = hd.MAKH;
                    KHName = hd.KHACHHANG.TENKH;
                }
            }
            if (MaxSell == 0)
            {
                BestKH = "";
                KHName = "(chưa có)";
            }
            p.MaxKH.Text = BestKH;
            p.KHName.Text = KHName;
        }

        private void NVCount(ThongKeView p)
        {
            MaxNV = int.MinValue;
            foreach (MUAHANG hd in DataProvider.Ins.DB.MUAHANGs)
            {
                int temp = DataProvider.Ins.DB.MUAHANGs.Where(x => x.MAND == hd.MAND).Count();
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
            if (DataProvider.Ins.DB.SANPHAMs.Where(x => x.LOAISP == "Hoodie").Count() > 0)
                hoodie = DataProvider.Ins.DB.SANPHAMs.Where(x => x.LOAISP == "Hoodie").Sum(x => x.SL);
            if (DataProvider.Ins.DB.SANPHAMs.Where(x => x.LOAISP == "Váy").Count() > 0)
                vay = DataProvider.Ins.DB.SANPHAMs.Where(x => x.LOAISP == "Váy").Sum(x => x.SL);
            if (DataProvider.Ins.DB.SANPHAMs.Where(x => x.LOAISP == "Áo Thun").Count() > 0)
                at = DataProvider.Ins.DB.SANPHAMs.Where(x => x.LOAISP == "Áo Thun").Sum(x => x.SL);
            if (DataProvider.Ins.DB.SANPHAMs.Where(x => x.LOAISP == "Yếm").Count() > 0)
                yem = DataProvider.Ins.DB.SANPHAMs.Where(x => x.LOAISP == "Yếm").Sum(x => x.SL);
            if (DataProvider.Ins.DB.SANPHAMs.Where(x => x.LOAISP == "Sơ mi").Count() > 0)
                sh = DataProvider.Ins.DB.SANPHAMs.Where(x => x.LOAISP == "Sơ mi").Sum(x => x.SL);
            if (DataProvider.Ins.DB.SANPHAMs.Where(x => x.LOAISP == "Trackpant").Count() > 0)
                quan = DataProvider.Ins.DB.SANPHAMs.Where(x => x.LOAISP == "Trackpant").Sum(x => x.SL);
            if (DataProvider.Ins.DB.SANPHAMs.Where(x => x.LOAISP == "Sweater").Count() > 0)
                sw = DataProvider.Ins.DB.SANPHAMs.Where(x => x.LOAISP == "Sweater").Sum(x => x.SL);
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
            throw new NotImplementedException();
        }
    }

}
