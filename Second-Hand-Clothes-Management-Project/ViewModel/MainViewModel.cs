using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Second_Hand_Clothes_Management_Project.Model;
using Second_Hand_Clothes_Management_Project.View;
using System.Windows.Input;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Windows;
using Second_Hand_Clothes_Management_Project.Properties;

namespace Second_Hand_Clothes_Management_Project.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        //field
        private NGUOIDUNG _NhanVien;
        private NGUOIDUNG _User;
        private string _Ava;
        public static Frame MainFrame { get; set; }
        //Command 
        public ICommand ThongKeCM { get; set; }
        public ICommand GiamGiaCM { get; set; }
        public ICommand NhapKhoCM { get; set; }
        public ICommand NhanVienCM { get; set; }
        public ICommand ThanhToanCM { get; set; }
        public ICommand CaiDatCM { get; set; }
       // public ICommand ChiTietNhanVienCM { get; set; }
        public ICommand LoadPage { get; set; }
        public ICommand Quyen_Loaded { get; set; }
        public ICommand Username_Loaded { get; set; }
        public ICommand SignoutCM { get; set; }
        //public ICommand CreateAccountCM { get; set; }
        public ICommand LoadPageCM { get; set; } 
        public ICommand Loadwd { get; set; }
        
        public ICommand HomeCM { get; set; }
        
        //property
        public NGUOIDUNG User { get => _User; set { _User = value; OnPropertyChanged(); } }
        public string Ava { get => _Ava; set { _Ava = value; OnPropertyChanged(); } }
        public NGUOIDUNG NhanVien { get => _NhanVien; set { _NhanVien = value; OnPropertyChanged(); } }

        private Visibility _SetQuanLy;
        public Visibility SetQuanLy { get => _SetQuanLy; set { _SetQuanLy = value; OnPropertyChanged(); } }

        public MainViewModel()
        {
            Quyen_Loaded = new RelayCommand<MainView>((p) => true, (p) => _LoadQuyen(p));
            Username_Loaded = new RelayCommand<MainView>((p) => true, (p) => _LoadUsername(p));
            Loadwd = new RelayCommand<MainView>((p) => true, (p) => _Loadwd(p));
            LoadPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content =  new SanPhamView();
            });
            HomeCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content =  new SanPhamView();
            });
            ThongKeCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content =  new ThongKeView();
            });
            GiamGiaCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content =  new GiamGiaView();
            });
             
             
            NhanVienCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content =   new NhanVienView();
            });
             
             
            CaiDatCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new CaiDatView();
            });
             
            SignoutCM = new RelayCommand<FrameworkElement>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetParentWindow(p);
                var w = window as Window;
                if (w != null)
                {
                    w.Hide();
                    LoginView w1 = new LoginView();
                    w1.ShowDialog();
                    w.Close();
                }
            });
        }

        private FrameworkElement GetParentWindow(FrameworkElement p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }

        private void _Loadwd(MainView p)
        {
            if (LoginViewModel.IsLogin)
            {
                string a = Const.TenDangNhap;
                User = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.USERNAME == a).FirstOrDefault();
                Const.ND = User;
                if (User.QTV == true)
                { SetQuanLy = Visibility.Visible; }
                else
                { SetQuanLy = Visibility.Collapsed; }
                Ava = User.AVA;
                _LoadUsername(p);
            }
        }

        private void _LoadUsername(MainView p)
        { 
            p.TenDangNhap.Text = string.Join(" ", User.USERNAME.Split().Reverse().Take(2).Reverse());
        }

        private void _LoadQuyen(MainView p)
        { 
            p.Quyen.Text = (bool)User.QTV ? "1" : "0";
        }
    }
}
