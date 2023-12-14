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
    public class ChiTietNhanVienViewModel : BaseViewModel
    {
        public ICommand Back { get; set; }
        public ICommand UpdateProduct { get; set; }
        public ICommand GetName { get; set; }
        private string TenND1;
        public ICommand Loadwd { get; set; }
        public ICommand DeleteProduct { get; set; }
        public ChiTietNhanVienViewModel()
        {
            GetName = new RelayCommand<ChiTietNhanVien>((p) => true, (p) => _GetName(p));
            Back = new RelayCommand<ChiTietNhanVien>((p) => true, (p) => _Back(p));
            UpdateProduct = new RelayCommand<ChiTietNhanVien>((p) => true, (p) => _UpdateProduct(p));
            Loadwd = new RelayCommand<ChiTietNhanVien>((p) => true, (p) => _Loadwd(p));
            DeleteProduct = new RelayCommand<ChiTietNhanVien>((p) => true, (p) => _DeleteProduct(p));
        }
        void _Loadwd(ChiTietNhanVien parmater)
        {
            parmater.Email.IsEnabled = true;
            parmater.TaiKhoanND.IsEnabled = true;
            parmater.MatKhauND.IsEnabled = true;
            parmater.TenND.IsEnabled = true;
            if (Const.Admin)
            {
                parmater.btncapnhatnd.Visibility = Visibility.Visible;
                parmater.btnxoand.Visibility = Visibility.Visible;
            }
        }
        void _Back(ChiTietNhanVien p)
        {
            NhanVienView productViewPage = new NhanVienView();
            MainViewModel.MainFrame.Content = productViewPage;
        }
        void _DeleteProduct(ChiTietNhanVien parameter)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn xóa sản phẩm ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                foreach (NGUOIDUNG a in DataProvider.Ins.DB.NGUOIDUNGs.Where(pa => (pa.TENND == TenND1 && pa.TTND == true)))
                {
                    a.TTND = false;
                }
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Xóa sản phẩm thành công !", "THÔNG BÁO");
                NhanVienView productView = new NhanVienView();
                productView.ListViewNhanVien.ItemsSource = new ObservableCollection<NGUOIDUNG>(DataProvider.Ins.DB.NGUOIDUNGs.Where(p => (p.QTV == false && p.TTND == true)));
                MainViewModel.MainFrame.Content = productView;
            }
        }
        void _GetName(ChiTietNhanVien p)
        {
            TenND1 = p.TenND.Text;
        }
        void _UpdateProduct(ChiTietNhanVien p)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn cập nhật sản phẩm ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                if (string.IsNullOrEmpty(p.TenND.Text) || string.IsNullOrEmpty(p.Email.Text))
                {
                    MessageBox.Show("Thông tin chưa đầy đủ !", "THÔNG BÁO");
                }
                else
                {
                    foreach (NGUOIDUNG a in DataProvider.Ins.DB.NGUOIDUNGs.Where(pa => (pa.TENND == TenND1 && pa.QTV == false)))
                    {
                        a.TENND = p.TenND.Text;
                        a.MAIL = p.Email.Text;
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Cập nhật sản phẩm thành công !", "THÔNG BÁO");
                    NhanVienView productView = new NhanVienView();
                    productView.ListViewNhanVien.ItemsSource = new ObservableCollection<NGUOIDUNG>(DataProvider.Ins.DB.NGUOIDUNGs);
                    MainViewModel.MainFrame.Content = productView;
                }
            }
        }
    }
}
