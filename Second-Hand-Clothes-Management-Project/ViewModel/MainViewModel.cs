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
    public  class MainViewModel : BaseViewModel
    {
        private TAIKHOAN _User;
        private string _Ava;

        public static Frame MainFrame { get; set; }
        public ICommand LoadPage { get; set; }
        public ICommand Quyen_Load { get; set; }
        public ICommand signoutCM { get; set; }
        public TAIKHOAN User { get => _User; set { _User = value; OnPropertyChanged(); } }
        public string Ava { get => _Ava; set { _Ava = value; OnPropertyChanged(); } }
        private Visibility _SetQuanLy;
        public Visibility SetQuanLy { get => _SetQuanLy; set { _SetQuanLy = value; OnPropertyChanged(); } }


        //public MainViewModel()
        //{
        //    Quyen_Load = new RelayCommand<MainView>((p) => true, (p) => _LoadQuyen(p));
        //    LoadPage = new RelayCommand<MainView>((p) => true, (p) => _LoadPage(p));
        //}

        private void _LoadQuyen(MainView p)
        {
            p.Quyen.Text = (bool)User.LOAITK ? "Quản lý" : "Nhân viên";
        }

        //private void _LoadPage(MainView p)
        //{
        //    if (LoginViewModel.IsLogin)
        //    {
        //        string a = Const.TenDangNhap;
        //        User = DataProvider.Ins.DB.NHANVIEN.Where(x => x.USERNAME == a).FirstOrDefault();
        //        Const.NguoiDung = User;
        //        SetQuanLy = (bool)User.LOAITK ? Visibility.Visible : Visibility.Collapsed;
        //        Const.Admin = User.QTV;
        //        LoadTenND(p);
        //    }
        //}

        //private void LoadTenND(MainView p)
        //{
        //    p.TenDangNhap.Text = string.Join(" ", User.TENND.Split().Reverse().Take(2).Reverse());
        //}
    }
}
