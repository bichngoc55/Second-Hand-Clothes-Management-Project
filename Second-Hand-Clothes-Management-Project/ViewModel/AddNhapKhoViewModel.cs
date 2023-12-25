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
    public class AddNhapKhoViewModel : BaseViewModel
    {


        public ICommand AddSP { get; set; }

        public AddNhapKhoViewModel()
        {

            AddSP = new RelayCommand<AddNhapKhoView>((p) => true, (p) => _AddSP(p));
        }
        void _AddSP(AddNhapKhoView paramater)
        {
            if (string.IsNullOrEmpty(paramater.MASP.Text) || string.IsNullOrEmpty(paramater.MANCC.Text) || string.IsNullOrEmpty(paramater.SLSP.Text) || string.IsNullOrEmpty(paramater.makho.Text) || string.IsNullOrEmpty(paramater.NgayNhap.Text))
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn thêm sản phẩm mới ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (h == MessageBoxResult.Yes)
                {


                    NHAP a = new NHAP();
                    a.MASP = paramater.MASP.Text;
                    a.MANHACUNGCAP = paramater.MANCC.Text;
                    a.MAKHO = paramater.makho.Text;


                    try
                    {

                        if (int.TryParse(paramater.SLSP.Text, out int intSLSP))
                        {
                            // Nếu nhập thành công, gán giá trị vào a.SL
                            a.SL = intSLSP;
                        }
                        else
                        {

                            MessageBox.Show("Số lượng sản phẩm không hợp lệ!", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Xử lý ngoại lệ chung nếu có lỗi khác
                        MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    try
                    {
                        // Thử chuyển đổi paramater.NGNHAP.Text thành kiểu DateTime
                        if (DateTime.TryParse(paramater.NgayNhap.Text, out DateTime parsedDateTime))
                        {
                            // Chuyển đổi thành công, gán giá trị vào a.NGBD
                            a.NGNHAP = parsedDateTime;
                        }
                        else
                        {
                            // Chuyển đổi không thành công, hiển thị thông báo lỗi
                            MessageBox.Show("Ngày nhập không hợp lệ!", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Xử lý ngoại lệ chung nếu có lỗi khác
                        MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    MessageBox.Show("Thêm sản phẩm nhập kho mới thành công !", "THÔNG BÁO");
                    DataProvider.Ins.DB.NHAPs.Add(a);
                    DataProvider.Ins.DB.SaveChanges();
                    // Xóa thông tin các TextBox
                    paramater.MASP.Clear();
                    paramater.MANCC.Clear();
                    paramater.SLSP.Clear();
                    paramater.NgayNhap.Clear();
                    paramater.makho.Clear();
                    NhapKhoView nhapKhoView = new NhapKhoView();
                    nhapKhoView.ListViewNK.ItemsSource = new ObservableCollection<NHAP>(DataProvider.Ins.DB.NHAPs);
                    MainViewModel.MainFrame.Content = nhapKhoView;
                }

            }
        }
    }
}
