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

namespace Second_Hand_Clothes_Management_Project.ViewModel
{

    class AddHoaDonViewModel : BaseViewModel
    {
        private List<SANPHAM> _LSP;
        public List<SANPHAM> LSP { get => _LSP; set { _LSP = value; OnPropertyChanged(); } }

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

        public ICommand SavePN { get; set; }
        public ICommand Choose { get; set; }
        public int tongtien { get; set; }
        public AddHoaDonViewModel()
        {
            tongtien = 0;
            LSPSelected = new ObservableCollection<SANPHAM>();
            LHT = new ObservableCollection<Display>();
            LCTHD = new ObservableCollection<CTHD>();
            Choose = new RelayCommand<AddHoaDon>((p) => true, (p) => _Choose(p));
            Loadwd = new RelayCommand<AddHoaDon>((p) => true, (p) => _Loadwd(p));
            AddSP = new RelayCommand<AddHoaDon>((p) => true, (p) => _AddSP(p));
            DeleteSP = new RelayCommand<AddHoaDon>((p) => true, (p) => _DeleteSP(p));
            SavePN = new RelayCommand<AddHoaDon>((p) => true, (p) => _SavePN(p));
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
        void _AddSP(AddHoaDon paramater)
        {
            if (paramater.SoHD.Text == "")
            {
                System.Windows.MessageBox.Show("Bạn chưa nhập mã phiếu nhập!", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            foreach (HOADON s in DataProvider.Ins.DB.HOADONs)
            {
                if (paramater.SoHD.Text == s.SOHD)
                {
                    System.Windows.MessageBox.Show("Mã phiếu nhập đã tồn tại !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            try
            {
                if (int.Parse(paramater.SL.Text) < 10)
                {
                    System.Windows.MessageBox.Show("Số lượng nhập không được nhỏ hơn 10!", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
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
            tongtien += int.Parse(paramater.SL.Text) * (int)(a.GIA);
            paramater.ListViewSP.ItemsSource = LHT;
            paramater.ListViewSP.Items.Refresh();
            paramater.SP.ItemsSource = LSP;
            paramater.SP.Items.Refresh();
            paramater.SP.SelectedItem = null;
            paramater.SL.Text = "";
            paramater.Voucher.Text = "Giảm giá " + paramater.KM.Text + "%";
            int tiensaukhigiam;
            tiensaukhigiam = tongtien - tongtien * int.Parse(paramater.KM.Text) / 100;
            paramater.TT.Text = "Thành tiền " + tiensaukhigiam.ToString("#,### VNĐ");

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
                tongtien = 0;
                paramater.TT.Text = String.Format("{0:0,0}", tongtien) + " VND";
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
            if (paramater.ListViewSP.Items.Count == 0)
            {
                System.Windows.MessageBox.Show("Thông tin phiếu nhập chưa đầy đủ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult h = System.Windows.MessageBox.Show("Xác nhận nhập hàng?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {

                HOADON temp = new HOADON()
                {
                    SOHD = paramater.SoHD.Text,
                    TRIGIA = tongtien,
                    MAKH = paramater.MaKH.Text,
                    MAND = Const.ND.MAND,
                    NGAYBAN = DateTime.Now,
                    CTHDs = new ObservableCollection<CTHD>(LCTHD),
                };

                foreach (CTHD s in LCTHD)
                {
                    foreach (SANPHAM x in DataProvider.Ins.DB.SANPHAMs)
                    {
                        if (x.MASP == s.SANPHAM.MASP)
                        {
                            x.SL += s.SL;
                        }
                    }
                }
                DataProvider.Ins.DB.HOADONs.Add(temp);
                DataProvider.Ins.DB.SaveChanges();
                System.Windows.MessageBox.Show("Nhập hàng thành công", "THÔNG BÁO");
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

    }
}
