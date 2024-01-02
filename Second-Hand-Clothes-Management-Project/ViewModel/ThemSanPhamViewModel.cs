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
using System.Windows.Media.Imaging;

namespace Second_Hand_Clothes_Management_Project.ViewModel
{
    public class ThemSanPhamViewModel : BaseViewModel
    {
        private ObservableCollection<SANPHAM> _listSP;
        public ObservableCollection<SANPHAM> listSP { get => _listSP; set { _listSP = value; /*OnPropertyChanged();*/ } }

        private ObservableCollection<SANPHAM> _listSP1;
        public ObservableCollection<SANPHAM> listSP1 { get => _listSP1; set { _listSP1 = value; /*OnPropertyChanged();*/ } }

        private string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
        public ICommand Closewd { get; set; }
        public ICommand Minimizewd { get; set; }
        public ICommand MoveWindow { get; set; }
        public ICommand AddImage { get; set; }
        private string _linkimage;
        public string linkimage { get => _linkimage; set { _linkimage = value; OnPropertyChanged(); } }
        public ICommand AddProduct { get; set; }
        public ICommand Loadwd { get; set; }
        public ThemSanPhamViewModel()
        {
            linkimage = "/ResourceXAML/Image/add.png";
            AddImage = new RelayCommand<Image>((p) => true, (p) => _AddImage(p));
            AddProduct = new RelayCommand<ThemSanPhamView>((p) => true, (p) => _AddProduct(p));
            Loadwd = new RelayCommand<ThemSanPhamView>((p) => true, (p) => _Loadwd(p));
            Closewd = new RelayCommand<ThemSanPhamView>((p) => true, (p) => Close(p));
            Minimizewd = new RelayCommand<ThemSanPhamView>((p) => true, (p) => Minimize(p));
            MoveWindow = new RelayCommand<ThemSanPhamView>((p) => true, (p) => moveWindow(p));
        }
        void moveWindow(ThemSanPhamView p)
        {
            p.DragMove();
        }
        void Close(ThemSanPhamView p)
        {
            p.Close();
        }
        void Minimize(ThemSanPhamView p)
        {
            p.WindowState = WindowState.Minimized;
        }
        void _Loadwd(ThemSanPhamView paramater)
        {
            linkimage = "/ResourceXAML/Image/add.png";
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
                        a.SIZE = paramater.SizeSp.Text;


                        //if(paramater.Voucher.Text != "NULL")

                            //a.MAGIAMGIA = paramater.Voucher.Text;
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
                        string imageFileName = Path.GetFileNameWithoutExtension(linkimage) + ((linkimage.Contains(".jpg")) ? ".jpg" : ".png").ToString();
                        a.HINHSP = "/ResourceXAML/ImageProduct/" + imageFileName;
                        //a.HINHSP = "/ResourceXAML/ImageProduct/" + Path.GetFileNameWithoutExtension(linkimage) + ((linkimage.Contains(".jpg")) ? ".jpg" : ".png").ToString();
                        try
                        {
                            File.Copy(linkimage, Path.Combine(_localLink, "ResourceXAML", "ImageProduct", imageFileName), true);
                            //File.Copy(linkimage, _localLink + @"ResourceXAML\ImageProduct\" + Path.GetFileNameWithoutExtension(linkimage) + ((linkimage.Contains(".jpg")) ? ".jpg" : ".png").ToString(), true);
                        }
                        catch { }
                        MessageBox.Show("Thêm sản phẩm mới thành công !", "THÔNG BÁO");
                        DataProvider.Ins.DB.SANPHAMs.Add(a);
                        DataProvider.Ins.DB.SaveChanges();
                        listSP1 = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(p => p.SL >= 0));
                        listSP = new ObservableCollection<SANPHAM>(listSP1.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()));
                        paramater.TenSp.Clear();
                        paramater.LoaiSp.SelectedItem = null;
                        paramater.GiaSp.Clear();
                        paramater.SlSp.Clear();
                        paramater.SizeSp.SelectedItem = null;
                        Uri fileUri = new Uri(Const._localLink + "/ResourceXAML/Image/add.png");
                        paramater.HinhAnh.Source = new BitmapImage(fileUri);
                        SanPhamView productViewPage = new SanPhamView();
                        paramater.Close();
                        MainViewModel.MainFrame.Content = productViewPage;
                    }
                }
            }
        }
    }
}
