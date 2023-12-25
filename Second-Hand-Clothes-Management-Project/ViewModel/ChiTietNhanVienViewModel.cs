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
        public ICommand Closewd { get; set; }
        public ICommand Minimizewd { get; set; }
        public ICommand MoveWindow { get; set; }
        public ICommand UpdateNhanVien { get; set; }
        public ICommand GetName { get; set; }
        private string TenND1;
        public ICommand Loadwd { get; set; }
        public ICommand DeleteNhanVien { get; set; }
        public ChiTietNhanVienViewModel()
        {
            Closewd = new RelayCommand<ChiTietNhanVien>((p) => true, (p) => Close(p));
            Minimizewd = new RelayCommand<ChiTietNhanVien>((p) => true, (p) => Minimize(p));
            MoveWindow = new RelayCommand<ChiTietNhanVien>((p) => true, (p) => moveWindow(p));
            GetName = new RelayCommand<ChiTietNhanVien>((p) => true, (p) => _GetName(p));
            UpdateNhanVien = new RelayCommand<ChiTietNhanVien>((p) => true, (p) => _UpdateNhanVien(p));
            Loadwd = new RelayCommand<ChiTietNhanVien>((p) => true, (p) => _Loadwd(p));
            DeleteNhanVien = new RelayCommand<ChiTietNhanVien>((p) => true, (p) => _DeleteNhanVien(p));
        }
        void moveWindow(ChiTietNhanVien p)
        {
            p.DragMove();
        }
        void Close(ChiTietNhanVien p)
        {
            p.Close();
        }
        void Minimize(ChiTietNhanVien p)
        {
            p.WindowState = WindowState.Minimized;
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
        void _DeleteNhanVien(ChiTietNhanVien parameter)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn xóa nhân viên ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                foreach (NGUOIDUNG a in DataProvider.Ins.DB.NGUOIDUNGs.Where(pa => (pa.TENND == TenND1 && pa.TTND == true)))
                {
                    DataProvider.Ins.DB.NGUOIDUNGs.Remove(a);
                }
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Xóa nhân viên thành công !", "THÔNG BÁO");
                NhanVienView nhanvienView = new NhanVienView();
                nhanvienView.ListViewNhanVien.ItemsSource = new ObservableCollection<NGUOIDUNG>(DataProvider.Ins.DB.NGUOIDUNGs.Where(p => (p.QTV == false && p.TTND == true)));
                MainViewModel.MainFrame.Content = nhanvienView;
                parameter.Close();
            }
        }
        void _GetName(ChiTietNhanVien p)
        {
            TenND1 = p.TenND.Text;
        }
        void _UpdateNhanVien(ChiTietNhanVien p)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn cập nhật nhân viên ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                if (string.IsNullOrEmpty(p.TenND.Text) || string.IsNullOrEmpty(p.Email.Text) || string.IsNullOrEmpty(p.SDTND.Text) || string.IsNullOrEmpty(p.NgSinhND.Text) || string.IsNullOrEmpty(p.gioitinh.Text) || string.IsNullOrEmpty(p.DiaChiND.Text))
                {
                    MessageBox.Show("Thông tin chưa đầy đủ !", "THÔNG BÁO");
                }
                else
                {
                    foreach (NGUOIDUNG a in DataProvider.Ins.DB.NGUOIDUNGs.Where(pa => (pa.TENND == TenND1 && pa.QTV == false)))
                    {
                        a.TENND = p.TenND.Text;
                        a.MAIL = p.Email.Text;
                        a.SDT = p.SDTND.Text;
                        a.NGSINH = (DateTime)p.NgSinhND.SelectedDate;
                        a.DIACHI = p.DiaChiND.Text;
                        a.GIOITINH = p.gioitinh.Text;
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Cập nhật nhân viên thành công !", "THÔNG BÁO");
                    NhanVienView nhanvienView = new NhanVienView();
                    nhanvienView.ListViewNhanVien.ItemsSource = new ObservableCollection<NGUOIDUNG>(DataProvider.Ins.DB.NGUOIDUNGs);
                    MainViewModel.MainFrame.Content = nhanvienView;
                    p.Close();
                }
            }
        }
    }
}
