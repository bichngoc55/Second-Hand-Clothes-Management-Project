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
    public class ThemSanPhamViewModel : BaseViewModel
    {
        private string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
        public ICommand Back { get; set; }
        public ICommand AddImage { get; set; }
        private string _linkimage;
        public string linkimage { get => _linkimage; set { _linkimage = value; OnPropertyChanged(); } }
        public ICommand AddProduct { get; set; }
        public ICommand Loadwd { get; set; }
        public ThemSanPhamViewModel()
        {
            linkimage = "/ResourceXAML/Image/add.png";
            Back = new RelayCommand<ThemSanPhamView>((p) => true, (p) => _Back(p));
            AddImage = new RelayCommand<Image>((p) => true, (p) => _AddImage(p));
            AddProduct = new RelayCommand<ThemSanPhamView>((p) => true, (p) => _AddProduct(p));
            Loadwd = new RelayCommand<ThemSanPhamView>((p) => true, (p) => _Loadwd(p));
        }
        void _Loadwd(ThemSanPhamView paramater)
        {
            linkimage = "/ResourceXAML/Image/add.png";
        }
        void _Back(ThemSanPhamView p)
        {
            SanPhamView productViewPage = new SanPhamView();
            MainViewModel.MainFrame.Content = productViewPage;
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
            foreach (SANPHAM temp in DataProvider.Ins.DB.SANPHAMs)
            {
                if (temp.MASP == m)
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
                ma = "PD" + rand.Next(0, 10000).ToString();
            } while (check(ma));
            return ma;
        }
        void _AddProduct(ThemSanPhamView paramater)
        {
            if (string.IsNullOrEmpty(paramater.MaSp.Text) || string.IsNullOrEmpty(paramater.TenSp.Text) || string.IsNullOrEmpty(paramater.LoaiSp.Text) || string.IsNullOrEmpty(paramater.GiaSp.Text) || string.IsNullOrEmpty(paramater.SlSp.Text) || linkimage == "/Resource/Image/add.png")
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn thêm sản phẩm mới ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (h == MessageBoxResult.Yes)
                {
                    if (DataProvider.Ins.DB.SANPHAMs.Where(p => p.MASP == paramater.MaSp.Text).Count() > 0)
                    {
                        MessageBox.Show("Mã sản phẩm đã tồn tại.", "Thông Báo");
                    }
                    else
                    {
                        SANPHAM a = new SANPHAM();
                        a.MASP = paramater.MaSp.Text;
                        a.TENSP = paramater.TenSp.Text;
                        try
                        {
                            a.GIA = int.Parse(paramater.GiaSp.Text);
                        }
                        catch
                        {
                            MessageBox.Show("Giá sản phẩm không hợp lệ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (int.Parse(paramater.GiaSp.Text) < 0)
                        {
                            MessageBox.Show("Giá sản phẩm không hợp lệ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        a.LOAISP = paramater.LoaiSp.Text;
                        try
                        {
                            a.SL = int.Parse(paramater.SlSp.Text);
                        }
                        catch
                        {
                            MessageBox.Show("Số lượng sản phẩm không hợp lệ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (a.SL < 0)
                        {
                            MessageBox.Show("Số lượng sản phẩm không hợp lệ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        a.MOTA = paramater.MotaSp.Text;
                        a.HINHSP = "/Resource/ImgProduct/" + "product_" + paramater.MaSp.Text + ((linkimage.Contains(".jpg")) ? ".jpg" : ".png").ToString();
                        try
                        {
                            File.Copy(linkimage, _localLink + @"ResourceXAML\ImageProduct\" + "product_" + paramater.MaSp.Text + ((linkimage.Contains(".jpg")) ? ".jpg" : ".png").ToString(), true);
                        }
                        catch { }
                        MessageBox.Show("Thêm sản phẩm mới thành công !", "THÔNG BÁO");
                        DataProvider.Ins.DB.SANPHAMs.Add(a);
                        DataProvider.Ins.DB.SaveChanges();
                        paramater.TenSp.Clear();
                        paramater.LoaiSp.SelectedItem = null;
                        paramater.GiaSp.Clear();
                        paramater.SlSp.Clear();
                        SanPhamView productViewPage = new SanPhamView();
                        productViewPage.ListViewProduct.ItemsSource = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(p => p.SL >= 0));
                        MainViewModel.MainFrame.Content = productViewPage;
                    }
                }
            }
        }
    }
}
