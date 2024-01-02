using Second_Hand_Clothes_Management_Project.Model;
using Second_Hand_Clothes_Management_Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Collections.ObjectModel;

namespace Second_Hand_Clothes_Management_Project.ViewModel
{
    public class ThemNhanVienViewModel : BaseViewModel
    {
        private string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
        public ICommand Closewd { get; set; }
        public ICommand Minimizewd { get; set; }
        public ICommand MoveWindow { get; set; }
        public ICommand AddImage { get; set; }
        public ICommand Back { get; set; }
        private string _linkimage;
        public string linkimage { get => _linkimage; set { _linkimage = value; OnPropertyChanged(); } }
        public ICommand AddND { get; set; }
        public ICommand Loadwd { get; set; }
        public ThemNhanVienViewModel()
        {
            linkimage = "/ResourceXAML/Image/add.png";
            AddImage = new RelayCommand<Image>((p) => true, (p) => _AddImage(p));
            Back = new RelayCommand<ThemNhanVienView>((p) => true, (p) => _Back(p));
            AddND = new RelayCommand<ThemNhanVienView>((p) => true, (p) => _AddND(p));
            Loadwd = new RelayCommand<ThemNhanVienView>((p) => true, (p) => _Loadwd(p));
            Closewd = new RelayCommand<ThemNhanVienView>((p) => true, (p) => Close(p));
            Minimizewd = new RelayCommand<ThemNhanVienView>((p) => true, (p) => Minimize(p));
            MoveWindow = new RelayCommand<ThemNhanVienView>((p) => true, (p) => moveWindow(p));
        }
        void moveWindow(ThemNhanVienView p)
        {
            p.DragMove();
        }
        void Close(ThemNhanVienView p)
        {
            p.Close();
        }
        void Minimize(ThemNhanVienView p)
        {
            p.WindowState = WindowState.Minimized;
        }
        void _Loadwd(ThemNhanVienView paramater)
        {
            linkimage = "/ResourceXAML/Image/add.png";
        }
        void _Back(ThemNhanVienView p)
        {
            NhanVienView nhanvienViewPage = new NhanVienView();
            MainViewModel.MainFrame.Content = nhanvienViewPage;
        }
        void _AddImage(Image img)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.png)|*.jpg; *.png";
            if (open.ShowDialog() == true)
            {
                linkimage = open.FileName;
            };
            if (linkimage == "/ResourceXAML/Image/add.png")
            {
                Uri fileUri = new Uri(linkimage, UriKind.Relative);
                img.Source = new BitmapImage(fileUri);
            }
            else
            {
                Uri fileUri = new Uri(linkimage);
                img.Source = new BitmapImage(fileUri);
            }
        }
        bool check(string m)
        {
            foreach (NGUOIDUNG temp in DataProvider.Ins.DB.NGUOIDUNGs)
            {
                if (temp.MAND == m)
                    return true;
            }
            return false;
        }
        string rdma()
        {
            string ma;
            do
            {
                Random rand = new Random();
                ma = "NV" + rand.Next(0, 10000).ToString();
            } while (check(ma));
            return ma;
        }
        void _AddND(ThemNhanVienView paramater)
        {
            if (string.IsNullOrEmpty(paramater.MaNd.Text) || string.IsNullOrEmpty(paramater.TenNd.Text) || string.IsNullOrEmpty(paramater.NgaySinhNd.Text) || string.IsNullOrEmpty(paramater.SdtNd.Text) || string.IsNullOrEmpty(paramater.DiaChiNd.Text) || linkimage == "/Resource/Image/add.png")
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn thêm nhân viên mới ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (h == MessageBoxResult.Yes)
                {
                    if (DataProvider.Ins.DB.NGUOIDUNGs.Where(p => p.MAND == paramater.MaNd.Text).Count() > 0)
                    {
                        MessageBox.Show("Mã nhân viên đã tồn tại.", "Thông Báo");
                    }
                    else
                    {
                        NGUOIDUNG a = new NGUOIDUNG();
                        a.MAND = paramater.MaNd.Text;
                        a.TENND = paramater.TenNd.Text;
                        a.DIACHI = paramater.DiaChiNd.Text;
                        a.NGSINH = (DateTime)paramater.NgaySinhNd.SelectedDate;
                        a.GIOITINH = paramater.GioiTinhNd.Text;
                        a.SDT = paramater.SdtNd.Text;
                        a.AVA = "/ResourceXAML/Avatar/" + paramater.MaNd.Text + ((linkimage.Contains(".jpg")) ? ".jpg" : ".png").ToString();
                        MessageBox.Show("Thêm nhân viên mới thành công !", "THÔNG BÁO");
                        DataProvider.Ins.DB.NGUOIDUNGs.Add(a);
                        DataProvider.Ins.DB.SaveChanges();
                        paramater.TenNd.Clear();
                        paramater.NgaySinhNd.SelectedDate = null;
                        paramater.DiaChiNd.Clear();
                        paramater.SdtNd.Clear();
                        NhanVienView NhanVienViewPage = new NhanVienView();
                        NhanVienViewPage.ListViewNhanVien.ItemsSource = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(p => p.SL >= 0));
                        MainViewModel.MainFrame.Content = NhanVienViewPage;
                    }
                }
            }
        }
    }
}
