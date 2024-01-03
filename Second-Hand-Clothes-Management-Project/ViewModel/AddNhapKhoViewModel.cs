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
using System.Windows.Documents;
using System.Globalization;

namespace Second_Hand_Clothes_Management_Project.ViewModel
{
    public class AddNhapKhoViewModel : BaseViewModel
    {

       
        public ICommand AddSP { get; set; }
        public ICommand Closewd { get; set; }
        public ICommand Minimizewd { get; set; }
        public ICommand MoveWindow { get; set; }


        public AddNhapKhoViewModel()
        {
            
            AddSP = new RelayCommand<AddNhapKhoView>((p) => true, (p) => _AddSP(p));
            Closewd = new RelayCommand<AddNhapKhoView>((p) => true, (p) => Close(p));
            Minimizewd = new RelayCommand<AddNhapKhoView>((p) => true, (p) => Minimize(p));
            MoveWindow = new RelayCommand<AddNhapKhoView>((p) => true, (p) => moveWindow(p));
        }
        void moveWindow(AddNhapKhoView p)
        {
            p.DragMove();
        }
        void Close(AddNhapKhoView p)
        {
            p.Close();
        }
        void Minimize(AddNhapKhoView p)
        {
            p.WindowState = WindowState.Minimized;
        }
        void _AddSP(AddNhapKhoView paramater)
        {
            if (string.IsNullOrEmpty(paramater.MASP.Text) || string.IsNullOrEmpty(paramater.cbxNCC.SelectedItem.ToString()) || string.IsNullOrEmpty(paramater.SLSP.Text) || string.IsNullOrEmpty(paramater.cbxkho.SelectedItem.ToString()) || string.IsNullOrEmpty(paramater.NgayNhap.Text))
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
                    a.MANHACUNGCAP = paramater.cbxNCC.Text;
                    a.MAKHO = paramater.cbxkho.Text;

                    a.NGNHAP = (DateTime)paramater.NgayNhap.SelectedDate;

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


                    MessageBox.Show("Thêm sản phẩm nhập kho mới thành công !", "THÔNG BÁO");
                    DataProvider.Ins.DB.NHAPs.Add(a);
                    DataProvider.Ins.DB.SaveChanges();
                    // Xóa thông tin các TextBox
                    paramater.MASP.Clear();
                    paramater.cbxNCC.SelectedIndex = -1;
                    paramater.cbxkho.SelectedIndex = -1;
                    paramater.SLSP.Clear();


                    NhapKhoView nhapKhoView = new NhapKhoView();
                    nhapKhoView.ListViewNK.ItemsSource = new ObservableCollection<NHAP>(DataProvider.Ins.DB.NHAPs);
                    MainViewModel.MainFrame.Content = nhapKhoView;
                }

            }
        }

    }
}
