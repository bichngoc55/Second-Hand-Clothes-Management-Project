using Second_Hand_Clothes_Management_Project.Model;
using Second_Hand_Clothes_Management_Project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;


namespace Second_Hand_Clothes_Management_Project.ViewModel
{
    public class Display
    {
        public string MaSp { get; set; }
        public string TenSP { get; set; }
        public int Dongia { get; set; }
        public int SL { get; set; }
        public string Size { get; set; }
        public int Tiennhap { get; set; }
        public Display(string MaSp = "", string TenSP = "", string Size = "", int Dongia = 0, int SL = 0, int Tiennhap = 0)
        {
            this.MaSp = MaSp;
            this.TenSP = TenSP;
            this.SL = SL;
            this.Dongia = Dongia;
            this.Size = Size;
            this.Tiennhap = Tiennhap;
        }
    }
    public class ThanhToanViewModel : BaseViewModel
    {
        private ObservableCollection<HOADON> _listHD;
        public ObservableCollection<HOADON> listHD { get => _listHD; set { _listHD = value; /*OnPropertyChanged();*/ } }


        private ObservableCollection<GIAMGIA> _listVoucher;
        public ObservableCollection<GIAMGIA> listVoucher { get => _listVoucher; set { _listVoucher = value; } }


        private ObservableCollection<string> _listVC;
        public ObservableCollection<string> listVC { get => _listVC; set { _listVC = value; OnPropertyChanged(); } }

        public ICommand SearchCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand LoadCsCommand { get; set; }
        public ICommand Detail { get; set; }

        public ThanhToanViewModel()
        {
            listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            SearchCommand = new RelayCommand<ThanhToanView>((p) => { return p == null ? false : true; }, (p) => _SearchCommand(p));
            AddCommand = new RelayCommand<ThanhToanView>((p) => { return p == null ? false : true; }, (p) => _AddCommand(p));
            LoadCsCommand = new RelayCommand<ThanhToanView>((p) => true, (p) => _LoadCsCommand(p));
            Detail = new RelayCommand<ThanhToanView>((p) => p.ListViewProduct.SelectedItem != null ? true : false, (p) => _Detail(p));


        }

        void _LoadCsCommand(ThanhToanView parameter)
        {
            listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            parameter.cbxBoLoc.SelectedIndex = 0;

            _SearchCommand(parameter);
        }

        void _SearchCommand(ThanhToanView paramater)
        {
            ObservableCollection<HOADON> temp = new ObservableCollection<HOADON>();
            if (paramater.cbxBoLoc.Text != "")
            {
                switch (paramater.cbxBoLoc.Text)
                {
                    case "SỐ HD":
                        {
                            foreach (HOADON s in listHD)
                            {
                                if (s.SOHD.ToLower().Contains(paramater.tbxTimKiem.Text.ToLower()))
                                {
                                    temp.Add(s);
                                }
                            }
                            break;
                        }
                    case "MÃ KHÁCH HÀNG":
                        {
                            try
                            {
                                foreach (HOADON s in listHD)
                                {
                                    if (s.MAKH.ToLower().Contains(paramater.tbxTimKiem.Text.ToLower()))
                                    {
                                        temp.Add(s);
                                    }
                                }
                            }
                            catch { }
                            break;
                        }
                    case "THÀNH TIỀN":
                        {
                            try
                            {
                                foreach (HOADON s in listHD)
                                {
                                    if (s.TRIGIA == int.Parse(paramater.tbxTimKiem.Text))
                                    {
                                        temp.Add(s);
                                    }
                                }
                            }
                            catch { }
                            break;
                        }
                    case "NGÀY THANH TOÁN":
                        {
                            try
                            {
                                foreach (HOADON s in listHD)
                                {
                                    // Use DateTime.TryParse to convert the string to DateTime
                                    if (DateTime.TryParse(paramater.tbxTimKiem.Text, out DateTime searchDate) && s.NGAYBAN == searchDate)
                                    {
                                        temp.Add(s);
                                    }
                                }
                            }
                            catch { }
                            break;
                        }


                }
                paramater.ListViewProduct.ItemsSource = temp;
            }
            else
                paramater.ListViewProduct.ItemsSource = listHD;
        }

        void _Detail(ThanhToanView p)
        {
            AddHoaDon addHoaDon = new AddHoaDon();
            ChiTietHoaDonView detailImport = new ChiTietHoaDonView();
            HOADON temp = (HOADON)p.ListViewProduct.SelectedItem;
            detailImport.MaND.Text = temp.NGUOIDUNG.MAND;
            detailImport.TenND.Text = temp.NGUOIDUNG.TENND;
            detailImport.MaKH.Text = temp.MAKH;
            detailImport.Ngay.Text = temp.NGAYBAN.ToString("dd/MM/yyyy hh:mm tt");
            detailImport.SoHD.Text = temp.SOHD.ToString();
            detailImport.KM.Text=temp.MAGIAMGIA;
            detailImport.TenKH.Text = temp.KHACHHANG.TENKH;
            List<Display> list = new List<Display>();
            int tong = 0;
            foreach (CTHD a in temp.CTHDs)
            {
                list.Add(new Display(a.MASP, a.SANPHAM.TENSP, a.SANPHAM.SIZE, (int)((float)a.SANPHAM.GIA), a.SL, (int)((float)(a.SL * a.SANPHAM.GIA) )));
                tong += (int)((float)(a.SL * a.SANPHAM.GIA));
            }
            GIAMGIA c = null;
            listVoucher = new ObservableCollection<GIAMGIA>(DataProvider.Ins.DB.GIAMGIAs);
            foreach (GIAMGIA b in listVoucher)
            {
                if (temp.MAGIAMGIA == b.MAGIAMGIA)
                {
                    c=b;
                    break;
                }
            }
            string voucherText = c.PHANTRAM;

            string numericPart = new string(voucherText.Where(char.IsDigit).ToArray());
            int phantram ;

            // Chuyển đổi sang kiểu int
            if (int.TryParse(numericPart, out phantram))
            {
                tong = tong * (100-phantram) / 100;
            }
            detailImport.thanhtien.Text = String.Format("Thành tiền: " + "{0:0,0}", tong) + " VND";
            detailImport.ListViewSP.ItemsSource = list;
            p.ListViewProduct.SelectedItem = null;
            listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            p.ListViewProduct.ItemsSource = listHD;
            _SearchCommand(p);
            MainViewModel.MainFrame.Content = detailImport;
        }
        void _AddCommand(ThanhToanView parameter)
        {

            AddHoaDon addHoaDonView = new AddHoaDon();
            listVoucher = new ObservableCollection<GIAMGIA>(DataProvider.Ins.DB.GIAMGIAs);
            listVC = new ObservableCollection<string> { };
            DateTime dateTime = DateTime.Now;
            foreach (GIAMGIA p in listVoucher)
            {
                if(p.NGBD < dateTime && p.NGKT > dateTime)
                    listVC.Add(p.MAGIAMGIA.ToString());
            }
          
            addHoaDonView.Voucher.ItemsSource = listVC;
            MainViewModel.MainFrame.Content = addHoaDonView;
        }
    }
}
