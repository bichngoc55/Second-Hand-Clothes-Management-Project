using Second_Hand_Clothes_Management_Project.Model;
using Second_Hand_Clothes_Management_Project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Second_Hand_Clothes_Management_Project.ViewModel;

namespace Second_Hand_Clothes_Management_Project.ViewModel
{
    public class AddGiamGiaViewModel : BaseViewModel
    {
        public ICommand AddVC { get; set; }

        public AddGiamGiaViewModel()
        {
            AddVC = new RelayCommand<AddGiamGiaView>((p) => true, (p) => _AddVC(p));
        }
        void _AddVC(AddGiamGiaView paramater)
        {
            if (string.IsNullOrEmpty(paramater.MAVC.Text) || string.IsNullOrEmpty(paramater.PTGG.Text) || string.IsNullOrEmpty(paramater.NGBD.Text) || string.IsNullOrEmpty(paramater.NGKT.Text))
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn thêm voucher mới ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (h == MessageBoxResult.Yes)
                {
                    if (DataProvider.Ins.DB.GIAMGIAs.Where(p => p.MAGIAMGIA == paramater.MAVC.Text).Count() > 0)
                    {
                        MessageBox.Show("Mã giảm giá đã tồn tại.", "Thông Báo");
                    }
                    else
                    {
                        GIAMGIA a = new GIAMGIA();
                        a.MAGIAMGIA = paramater.MAVC.Text;
                        a.PHANTRAM = paramater.PTGG.Text;


                        try
                        {
                            // Thử chuyển đổi paramater.NGBD.Text thành kiểu DateTime
                            if (DateTime.TryParse(paramater.NGBD.Text, out DateTime parsedDateTime))
                            {
                                // Chuyển đổi thành công, gán giá trị vào a.NGBD
                                a.NGBD = parsedDateTime;
                            }
                            else
                            {
                                // Chuyển đổi không thành công, hiển thị thông báo lỗi
                                MessageBox.Show("Ngày bắt đầu không hợp lệ!", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
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
                            // Thử chuyển đổi paramater.NGBD.Text thành kiểu DateTime
                            if (DateTime.TryParse(paramater.NGKT.Text, out DateTime parsedDateTime))
                            {
                                // Chuyển đổi thành công, gán giá trị vào a.NGBD
                                a.NGKT = parsedDateTime;
                            }
                            else
                            {
                                // Chuyển đổi không thành công, hiển thị thông báo lỗi
                                MessageBox.Show("Ngày kết thúc không hợp lệ!", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            // Xử lý ngoại lệ chung nếu có lỗi khác
                            MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        MessageBox.Show("Thêm voucher mới thành công !", "THÔNG BÁO");
                        DataProvider.Ins.DB.GIAMGIAs.Add(a);
                        DataProvider.Ins.DB.SaveChanges();
                        paramater.MAVC.Clear();
                        paramater.PTGG.Clear();
                        paramater.NGBD.Clear();
                        paramater.NGKT.Clear();
                        GiamGiaView giamGiaView = new GiamGiaView();
                        giamGiaView.ListViewGG.ItemsSource = new ObservableCollection<GIAMGIA>(DataProvider.Ins.DB.GIAMGIAs);
                        MainViewModel.MainFrame.Content = giamGiaView;
                    }
                }
            }
        }
    }
}