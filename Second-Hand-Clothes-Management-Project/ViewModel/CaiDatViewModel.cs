using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using Second_Hand_Clothes_Management_Project.View;
using Second_Hand_Clothes_Management_Project.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Documents;

namespace Second_Hand_Clothes_Management_Project.ViewModel
{
    public class CaiDatViewModel : BaseViewModel
    {
        private string _Code;
        public string Code { get => _Code; set { _Code = value; OnPropertyChanged(); } }
        private string _Ava;
        public string Ava { get => _Ava; set { _Ava = value; OnPropertyChanged(); } }
        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        private string _DoB;
        public string DoB { get => _DoB; set { _DoB = value; OnPropertyChanged(); } }
        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }
        private string _Mail;
        public string Mail { get => _Mail; set { _Mail = value; OnPropertyChanged(); } }
        private string _GioiTinh;
        public string GioiTinh { get => _GioiTinh; set { _GioiTinh = value; OnPropertyChanged(); } }
        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }
        private string _TenTK;

        private string _PassWord;
        public string PassWord { get => _PassWord; set { _PassWord = value; OnPropertyChanged(); } }
        public string TenTK { get => _TenTK; set { _TenTK = value; OnPropertyChanged(); } }
        private NGUOIDUNG _User;
        public NGUOIDUNG User { get => _User; set { _User = value; OnPropertyChanged(); } }
        public ICommand Loadwd { get; set; }
        public ICommand UpdateInfo { get; set; }
        public ICommand AddImage { get; set; }
        public ICommand ChangePass { get; set; }
        public ICommand AddNDCommand { get; set; }
        public ICommand GetName { get; set; }
        private string TenND1;
        public CaiDatViewModel()
        {
            Loadwd = new RelayCommand<CaiDatView>((p) => true, (p) => _Loadwd(p));
            AddImage = new RelayCommand<ImageBrush>((p) => true, (p) => _AddImage(p));
            AddNDCommand = new RelayCommand<CaiDatView>((p) => true, (p) => _AddND(p));
            UpdateInfo = new RelayCommand<CaiDatView>((p) => true, (p) => _UdpateInfo(p));
            GetName = new RelayCommand<CaiDatView>((p) => true, (p) => _GetName(p));
            //ChangePass = new RelayCommand<CaiDatView>((p) => true, (p) => _ChangePass());
        }
        //void _ChangePass()
        //{
        //    ChangePassword change = new ChangePassword();
        //    MainViewModel.MainFrame.Content = change;
        //}
        void _GetName(CaiDatView p)
        {
            TenND1 = p.NameBox.Text;
        }
        void _AddImage(ImageBrush p)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.png)|*.jpg; *.png";
            if (open.ShowDialog() == true)
            {
                Ava = open.FileName;
            }
            p.ImageSource = new BitmapImage(new Uri(Ava));
        }
        void _Loadwd(CaiDatView p)
        {
            if (LoginViewModel.IsLogin)
            {
                string a = Const.TenDangNhap;
                User = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.USERNAME == a).FirstOrDefault();
                Code = User.MAND;
                Ava = User.AVA;
                Name = User.TENND;
                DoB = User.NGSINH.ToString();
                DiaChi = User.DIACHI;
                GioiTinh = User.GIOITINH;
                SDT = User.SDT;
                TenTK = User.USERNAME;
                Mail = User.MAIL;
                PassWord = User.PASS;
            }
        }
        void _UdpateInfo(CaiDatView p)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn cập nhật nhân viên ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                if (string.IsNullOrEmpty(p.NameBox.Text) || string.IsNullOrEmpty(p.Mail.Text) || string.IsNullOrEmpty(p.SDTBox.Text) || string.IsNullOrEmpty(p.DateBox.Text) || string.IsNullOrEmpty(p.GTBox.Text) || string.IsNullOrEmpty(p.AddressBox.Text))
                {
                    MessageBox.Show("Thông tin chưa đầy đủ !", "THÔNG BÁO");
                }
                else
                {
                    foreach (NGUOIDUNG a in DataProvider.Ins.DB.NGUOIDUNGs.Where(pa => (pa.MAND == Code)))
                    {
                        a.TENND = p.NameBox.Text;
                        a.MAIL = p.Mail.Text;
                        a.SDT = p.SDTBox.Text;
                        a.NGSINH = (DateTime)p.DateBox.SelectedDate;
                        a.DIACHI = p.AddressBox.Text;
                        a.GIOITINH = p.GTBox.Text;
                        break;
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Cập nhật nhân viên thành công !", "THÔNG BÁO");
                }
            }
            CaiDatView temp = new CaiDatView();
            MainViewModel.MainFrame.Content = temp;
        }
        //static string StringGenerator()
        //{
        //    Random rd = new Random();
        //    int length = rd.Next(5, 20);
        //    StringBuilder str_build = new StringBuilder();
        //    Random random = new Random();
        //    char letter;
        //    for (int i = 0; i < length; i++)
        //    {
        //        double flt = random.NextDouble();
        //        int shift = Convert.ToInt32(Math.Floor(25 * flt));
        //        letter = Convert.ToChar(shift + 65);
        //        str_build.Append(letter);
        //    }
        //    return str_build.ToString();
        //}
        void _AddND(CaiDatView parameter)
        {
            TaoTaiKhoanView addNDView = new TaoTaiKhoanView();
            MainViewModel.MainFrame.Content = addNDView;
        }
    }
}
