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
using System.Windows.Controls;
using System.Security.Cryptography;

namespace Second_Hand_Clothes_Management_Project.ViewModel
{
    internal class TaoTaiKhoanViewModel : BaseViewModel
    {
        private string _linkaddimage;
        public string linkaddimage { get => _linkaddimage; set { _linkaddimage = value; OnPropertyChanged(); } }
        public ICommand AddNDCommand { get; set; }
        public ICommand AddImage { get; set; }
        public ICommand Back { get; set; }
        public static Frame MainFrame { get; set; }
        public TaoTaiKhoanViewModel()
        {
            linkaddimage = "/Resource/Image/addava.png";
            AddNDCommand = new RelayCommand<TaoTaiKhoanView>((p) => true, (p) => _AddND(p));
            AddImage = new RelayCommand<ImageBrush>((p) => true, (p) => _AddImage(p));
            Back = new RelayCommand<TaoTaiKhoanView>((p) => true, (p) => _Back(p));

            void _Back(TaoTaiKhoanView p)
            {
                CaiDatView productViewPage = new CaiDatView();
                MainViewModel.MainFrame.Content = productViewPage;
            }

            void _AddImage(ImageBrush img)
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpg; *.png)|*.jpg; *.png";

                if (open.ShowDialog() == true)
                {
                    if (open.FileName != "")
                        linkaddimage = open.FileName;
                };
                Uri fileUri = new Uri(linkaddimage);
                img.ImageSource = new BitmapImage(fileUri);
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
            void _AddND(TaoTaiKhoanView addNDView)
            {
                MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn thêm người dùng ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (h == MessageBoxResult.Yes)
                {
                    if (String.IsNullOrEmpty(addNDView.MaND.Text) || String.IsNullOrEmpty(addNDView.TenND.Text) || String.IsNullOrEmpty(addNDView.SDT.Text) || String.IsNullOrEmpty(addNDView.GT.Text) || String.IsNullOrEmpty(addNDView.QTV.Text) || addNDView.NS.SelectedDate == null)
                    {
                        MessageBox.Show("Bạn chưa nhập đầy đủ thông tin !", "THÔNG BÁO");
                        return;
                    }
                    NGUOIDUNG temp = new NGUOIDUNG();
                    foreach (NGUOIDUNG a in DataProvider.Ins.DB.NGUOIDUNGs.Where(p => p.TTND == true))
                    {
                        if (addNDView.MaND.Text == a.MAND)
                        {
                            MessageBox.Show("Mã ND đã tồn tại !", "THÔNG BÁO");
                            return;
                        }
                    }
                    foreach (NGUOIDUNG temp5 in DataProvider.Ins.DB.NGUOIDUNGs)
                    {
                        if (temp5.MAIL == addNDView.Mail.Text)
                        {
                            MessageBox.Show("Email này đã được sử dụng !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    string match = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                    Regex reg = new Regex(match);
                    if (!reg.IsMatch(addNDView.Mail.Text))
                    {
                        MessageBox.Show("Email không hợp lệ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    string match1 = @"^((09(\d){8})|(086(\d){7})|(088(\d){7})|(089(\d){7})|(01(\d){9}))$";
                    Regex reg1 = new Regex(match1);
                    if (!reg1.IsMatch(addNDView.SDT.Text))
                    {
                        MessageBox.Show("Số điện thoại không hợp lệ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    temp.MAND = addNDView.MaND.Text;
                    temp.TENND = addNDView.TenND.Text;
                    temp.SDT = addNDView.SDT.Text;
                    temp.DIACHI = addNDView.DC.Text;
                    temp.GIOITINH = addNDView.GT.Text;
                    temp.MAIL = addNDView.Mail.Text;
                    temp.NGSINH = (DateTime)addNDView.NS.SelectedDate;
                    if (addNDView.QTV.Text == "Quản lý")
                    {
                        temp.QTV = true;
                        temp.USERNAME = "admin" + addNDView.MaND.Text;
                    }
                    else
                    {
                        temp.QTV = false;
                        temp.USERNAME = "user" + addNDView.MaND.Text;
                    }
                    temp.TTND = true;
                    temp.PASS = LoginViewModel.MD5Hash(LoginViewModel.Base64Encode(addNDView.PassWord.Text));
                    if (linkaddimage == "/ResourceXAML/Image/addava.png")
                        temp.AVA = "/ResourceXAML/Image/addava.png";
                    else
                        temp.AVA = "/ResourceXAML/Avatar/" + Path.GetFileNameWithoutExtension(linkaddimage) + ((linkaddimage.Contains(".jpg")) ? ".jpg" : ".png").ToString();
                    DataProvider.Ins.DB.NGUOIDUNGs.Add(temp);
                    try
                    {
                        File.Copy(linkaddimage, Const._localLink + @"ResourceXAML\Avatar\" + temp.MAND + ((linkaddimage.Contains(".jpg")) ? ".jpg" : ".png").ToString(), true);
                    }
                    catch { }
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thêm người dùng thành công !", "THÔNG BÁO");
                    addNDView.MaND.Text = rdma();
                    addNDView.TenND.Clear();
                    addNDView.GT.SelectedItem = null;
                    addNDView.GT.Items.Refresh();
                    addNDView.QTV.SelectedItem = null;
                    addNDView.QTV.Items.Refresh();
                    addNDView.NS.SelectedDate = null;
                    addNDView.SDT.Clear();
                    addNDView.DC.Clear();
                    addNDView.Mail.Clear();
                    linkaddimage = Const._localLink + "/ResourceXAML/Image/addava.png";
                    Uri fileUri = new Uri(linkaddimage);
                    addNDView.HinhAnh1.ImageSource = new BitmapImage(fileUri);
                    CaiDatView qLNVView = new CaiDatView();
                    MainViewModel.MainFrame.Content = qLNVView;
                }
            }
        }
    }
}
