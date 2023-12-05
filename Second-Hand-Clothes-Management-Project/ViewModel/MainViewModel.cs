using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Second_Hand_Clothes_Management_Project.Model;
using Second_Hand_Clothes_Management_Project.View;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;

namespace Second_Hand_Clothes_Management_Project.ViewModel
{
    public  class MainViewModel : BaseViewModel
    {
        public static Frame MainFrame { get; set; }
        public ICommand LoadPage { get; set; }
        public ICommand Quyen_Load { get; set; }
        public ICommand signoutCM { get; set; }
        public MainViewModel()
        {
            Quyen_Load = new RelayCommand<MainView>((p) => true, (p) => _LoadQuyen(p));
            LoadPage = new RelayCommand<MainView>((p) => true, (p) => _LoadPage(p));
        }

        private void _LoadQuyen(MainView p)
        {
            throw new NotImplementedException();
        }

        private void _LoadPage(MainView p)
        {
            if (LoginViewModel.IsLogin)
            {
                string a = Const.TenDangNhap;
                User = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.USERNAME == a).FirstOrDefault();
                Const.ND = User;
                SetQuanLy = User.QTV ? Visibility.Visible : Visibility.Collapsed;
                Const.Admin = User.QTV;
                Ava = User.AVA;
                LoadTenND(p);
            }
        }
    }
}
