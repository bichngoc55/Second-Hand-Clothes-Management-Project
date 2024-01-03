using Second_Hand_Clothes_Management_Project.Model;
using Second_Hand_Clothes_Management_Project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Controls;

namespace Second_Hand_Clothes_Management_Project.ViewModel
{

    class AddHoaDonViewModel : BaseViewModel
    {
        private List<SANPHAM> _LSP;
        public List<SANPHAM> LSP { get => _LSP; set { _LSP = value; OnPropertyChanged(); } }
        private List<GIAMGIA> _LVC;
        public List<GIAMGIA> LVC { get => _LVC; set { _LVC = value; OnPropertyChanged(); } }
        private ObservableCollection<GIAMGIA> _listVoucher;
        public ObservableCollection<GIAMGIA> listVoucher { get => _listVoucher; set { _listVoucher = value; } }
       

        private List<KHACHHANG> _LKH;
        public List<KHACHHANG> LKH { get => _LKH; set { _LKH = value; OnPropertyChanged(); } }
        private ObservableCollection<Display> _LHT;
        public ObservableCollection<Display> LHT { get => _LHT; set { _LHT = value; OnPropertyChanged(); } }
        private ObservableCollection<SANPHAM> _LSPSelected;
        public ObservableCollection<SANPHAM> LSPSelected { get => _LSPSelected; set { _LSPSelected = value; OnPropertyChanged(); } }
        private ObservableCollection<CTHD> _LCTHD;
        public ObservableCollection<CTHD> LCTHD { get => _LCTHD; set { _LCTHD = value; OnPropertyChanged(); } }

     
        public ICommand Loadwd { get; set; }
        public ICommand AddSP { get; set; }
        public ICommand DeleteSP { get; set; }
        public ICommand Back { get; set; }
        public ICommand SavePN { get; set; }
        public ICommand Choose { get; set; }
        public ICommand ChooseVC { get; set; }
        public int tongtien { get; set; }
        public AddHoaDonViewModel()
        {
            tongtien = 0;
            LSPSelected = new ObservableCollection<SANPHAM>();
            LHT = new ObservableCollection<Display>();
            LCTHD = new ObservableCollection<CTHD>();
            Choose = new RelayCommand<AddHoaDon>((p) => true, (p) => _Choose(p));
            ChooseVC = new RelayCommand<AddHoaDon>((p) => true, (p) => _ChooseVC(p));
            Loadwd = new RelayCommand<AddHoaDon>((p) => true, (p) => _Loadwd(p));
            AddSP = new RelayCommand<AddHoaDon>((p) => true, (p) => _AddSP(p));
            DeleteSP = new RelayCommand<AddHoaDon>((p) => true, (p) => _DeleteSP(p));
            SavePN = new RelayCommand<AddHoaDon>((p) => true, (p) => _SavePN(p));
            Back = new RelayCommand<AddHoaDon>((p) => true, (p) => _Back(p));
        }
        void _Loadwd(AddHoaDon paramater)
        {
            LSP = DataProvider.Ins.DB.SANPHAMs.Where(p => p.SL >= 0).ToList();
            paramater.SP.ItemsSource = LSP;
            LKH = DataProvider.Ins.DB.KHACHHANGs.ToList();

            paramater.MaND.Text = Const.ND.MAND;
            paramater.TenND.Text = Const.ND.TENND;
            paramater.Ngay.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        }
        void _Choose(AddHoaDon paramater)
        {
            if (paramater.SP.SelectedItem != null)
            {
                SANPHAM temp = (SANPHAM)paramater.SP.SelectedItem;
                paramater.DG.Text = String.Format("{0:#,###} VNĐ", ((int)(float)temp.GIA));
            }
            else
            {
                paramater.DG.Text = "";
            }

        }
        void _Back(AddHoaDon p)
        {
            ThanhToanView importView = new ThanhToanView();
            MainViewModel.MainFrame.Content = importView;
        }
        void _AddSP(AddHoaDon paramater)
        {
            if (paramater.SoHD.Text == "")
            {
                System.Windows.MessageBox.Show("Bạn chưa nhập mã hóa đơn!", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            foreach (HOADON s in DataProvider.Ins.DB.HOADONs)
            {
                if (paramater.SoHD.Text == s.SOHD)
                {
                    System.Windows.MessageBox.Show("Mã hóa đơn đã tồn tại !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            try
            {
                if (int.Parse(paramater.SL.Text) < 1)
                {
                    System.Windows.MessageBox.Show("Số lượng nhập không được nhỏ hơn 1!", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Số lượng nhập không hợp lệ!", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SANPHAM a = (SANPHAM)paramater.SP.SelectedItem;
            foreach (Display display in paramater.ListViewSP.Items)
            {
                if (display.MaSp == a.MASP)
                {
                    display.SL += int.Parse(paramater.SL.Text);
                    display.Tiennhap = display.SL * (int)(a.GIA);
                    foreach (CTHD ct in LCTHD)
                    {
                        if (ct.MASP == display.MaSp)
                            ct.SL = display.SL;
                    }
                    goto There;
                }
            }
            Display b = new Display(a.MASP, a.TENSP, a.SIZE, (int)((float)a.GIA), int.Parse(paramater.SL.Text), (int)((float)(int.Parse(paramater.SL.Text) * a.GIA)));
            CTHD CTHD = new CTHD()
            {
                MASP = a.MASP,
                SL = int.Parse(paramater.SL.Text),
                SANPHAM = a,
                SOHD = paramater.SoHD.Text,
            };
            LCTHD.Add(CTHD);
            LHT.Add(b);
        There:
            tongtien += int.Parse(paramater.SL.Text) * (int)(a.GIA) ;
            paramater.ListViewSP.ItemsSource = LHT;
            paramater.ListViewSP.Items.Refresh();
            paramater.SP.ItemsSource = LSP;
            paramater.SP.Items.Refresh();
            paramater.SP.SelectedItem = null;
            paramater.SL.Text = "";
           
            int tiensaukhigiam,phantram=0;
            int tiendagiam = 0;
            string voucherText = paramater.voucherperc.Text;

            string numericPart = new string(voucherText.Where(char.IsDigit).ToArray());

            // Chuyển đổi sang kiểu int
            if (int.TryParse(numericPart, out phantram))
            {
                tiendagiam = tongtien * phantram/100;
            }
            tiensaukhigiam = tongtien - tiendagiam;
            paramater.discountmoney.Text = "Tiền đã giảm: " + tiendagiam.ToString("#,### VNĐ");
            paramater.summoney.Text = "Thành tiền " + tiensaukhigiam.ToString("#,### VNĐ");
            tiendagiam = tiensaukhigiam = phantram = 0;

        }
        void _DeleteSP(AddHoaDon paramater)
        {
            if (paramater.ListViewSP.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Bạn chưa chọn sản phẩm !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult h = System.Windows.MessageBox.Show("  Bạn có chắc muốn xóa sản phẩm.", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                Display a = (Display)paramater.ListViewSP.SelectedItem;

                int tiensaukhigiam, phantram = 0;
                int tiendagiam = 0;
                string voucherText = paramater.voucherperc.Text;

                string numericPart = new string(voucherText.Where(char.IsDigit).ToArray());

                // Chuyển đổi sang kiểu int
                if (int.TryParse(numericPart, out phantram))
                {
                    tiendagiam = tongtien * phantram / 100;
                }
                tiensaukhigiam = tongtien - tiendagiam;
                int tiengiamsauxoa = 0;
                int tientongsauxoa = 0;
                tiengiamsauxoa = tiendagiam - a.Dongia*a.SL*phantram/100;
                tientongsauxoa = tongtien -a.Dongia*a.SL - tiengiamsauxoa;
                tongtien-= a.Dongia*a.SL;
                paramater.discountmoney.Text = "Tiền đã giảm: " + tiengiamsauxoa.ToString("#,### VNĐ");
                paramater.summoney.Text = "Thành tiền " + tientongsauxoa.ToString("#,### VNĐ");
               
                LHT.Remove(a);
                foreach (SANPHAM b in LSPSelected)
                {
                    if (b.MASP == a.MaSp)
                    {
                        LSPSelected.Remove(b);
                        break;
                    }
                }
                foreach (CTHD b in LCTHD)
                {
                    if (b.MASP == a.MaSp && b.SL == a.SL)
                    {
                        LCTHD.Remove(b);
                        break;
                    }
                }
                paramater.ListViewSP.Items.Refresh();
            }
            else
                return;
        }

        void _SavePN(AddHoaDon paramater)
        {
            ChiTietHoaDonView hoadontoPrint = new ChiTietHoaDonView();
            if (paramater.ListViewSP.Items.Count == 0)
            {
                System.Windows.MessageBox.Show("Thông tin hóa đơn chưa đầy đủ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult h = System.Windows.MessageBox.Show("Xác nhận nhập hàng?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                if (paramater.cbxKH.SelectedItem is KHACHHANG selectedKH)
                {
                    string tempMAKH = selectedKH.MAKH;


                    HOADON temp = new HOADON()
                    {
                        SOHD = paramater.SoHD.Text,
                        MAKH = tempMAKH,
                        MAND = Const.ND.MAND,
                        NGAYBAN = DateTime.Now,
                        MAGIAMGIA = paramater.Voucher.Text,
                        CTHDs = new ObservableCollection<CTHD>(LCTHD),
                    };

                    ThanhToanView thanhToanView = new ThanhToanView();

                    GIAMGIA c = null;
                    listVoucher = new ObservableCollection<GIAMGIA>(DataProvider.Ins.DB.GIAMGIAs);
                    foreach (GIAMGIA b in listVoucher)
                    {
                        if (temp.MAGIAMGIA == b.MAGIAMGIA)
                        {
                            c = b;
                            break;
                        }
                    }
                   
                    string voucherText = c.PHANTRAM;

                    
                   
                        string numericPart = new string(voucherText.Where(char.IsDigit).ToArray());
                         
                    int phantram;

                    // Chuyển đổi sang kiểu int
                    if (int.TryParse(numericPart, out phantram))
                    {
                        tongtien = tongtien * (100 - phantram) / 100;
                    }
                    temp.TRIGIA = tongtien;
                    foreach (CTHD s in LCTHD)
                    {
                        foreach (SANPHAM x in DataProvider.Ins.DB.SANPHAMs)
                        {
                            if (x.MASP == s.SANPHAM.MASP)
                            {
                                x.SL -= s.SL;
                            }
                        }
                    }
                    DataProvider.Ins.DB.HOADONs.Add(temp);
                    
                    hoadontoPrint.SoHD.Text = paramater.SoHD.Text;
                    hoadontoPrint.MaKH.Text = tempMAKH;
                    hoadontoPrint.MaND.Text = Const.ND.MAND;
                    hoadontoPrint.Ngay.Text = temp.NGAYBAN.ToString("dd/MM/yyyy hh:mm tt");
                    hoadontoPrint.TenND.Text = temp.NGUOIDUNG.TENND;
                    hoadontoPrint.thanhtien.Text= String.Format("Thành tiền: " + "{0:0,0}", tongtien) + " VND";
                    hoadontoPrint.KM.Text = paramater.Voucher.Text;
                    hoadontoPrint.ListViewSP.ItemsSource = LHT;
                    hoadontoPrint.TenKH.Text = temp.KHACHHANG.TENKH;

                }
                DataProvider.Ins.DB.SaveChanges();
                System.Windows.MessageBox.Show("Nhập hàng thành công", "THÔNG BÁO");
                MessageBoxResult printResult = System.Windows.MessageBox.Show("Bạn có muốn in hóa đơn không?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (printResult == MessageBoxResult.Yes)
                {
                    // Gọi hàm in hóa đơn và truyền đối tượng hoadonToPrint
                    InHoaDon(hoadontoPrint);
                }
                LHT = new ObservableCollection<Display>();
                paramater.SoHD.Clear();
                LCTHD = new ObservableCollection<CTHD>();
                paramater.ListViewSP.ItemsSource = LHT;
                LSP = DataProvider.Ins.DB.SANPHAMs.Where(p => p.SL >= 0).ToList();
                paramater.SP.Items.Refresh();
                ThanhToanView importView = new ThanhToanView();
                importView.ListViewProduct.ItemsSource = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
                MainViewModel.MainFrame.Content = importView;
            }
            else
                return;
        }


        void _ChooseVC(AddHoaDon parameter)
        {
            GIAMGIA temp=null ;
            listVoucher = new ObservableCollection<GIAMGIA>(DataProvider.Ins.DB.GIAMGIAs);
            foreach (GIAMGIA p in listVoucher)
            {
                if (parameter.Voucher.SelectedItem == p.MAGIAMGIA.ToString())
                {
                    temp= p;
                    break;
                }
               
            }
           
            if (temp != null)
            {
                parameter.voucherperc.Text = "Voucher: "+ temp.PHANTRAM;
               
            }
            else
                parameter.voucherperc.Text = "Voucher: 0%";
        }
        void InHoaDon(ChiTietHoaDonView parameter)
        {
            try
            {
                parameter.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(parameter.ChiTietHoaDonwd, "Hóa đơn");
                }
            }
            finally
            {
                parameter.IsEnabled = true;
            }
        }

    }
}
