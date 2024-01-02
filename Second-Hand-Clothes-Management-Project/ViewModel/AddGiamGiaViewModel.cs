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
        public ICommand Closewd { get; set; }
        public ICommand Minimizewd { get; set; }
        public ICommand MoveWindow { get; set; }

        public AddGiamGiaViewModel()
        {
            AddVC = new RelayCommand<AddGiamGiaView>((p) => true, (p) => _AddVC(p));
            Closewd = new RelayCommand<AddGiamGiaView>((p) => true, (p) => Close(p));
            Minimizewd = new RelayCommand<AddGiamGiaView>((p) => true, (p) => Minimize(p));
            MoveWindow = new RelayCommand<AddGiamGiaView>((p) => true, (p) => moveWindow(p));
        }
        void moveWindow(AddGiamGiaView p)
        {
            p.DragMove();
        }
        void Close(AddGiamGiaView p)
        {
            p.Close();
        }
        void Minimize(AddGiamGiaView p)
        {
            p.WindowState = WindowState.Minimized;
        }
        void _AddVC(AddGiamGiaView paramater)
        {
            if (string.IsNullOrEmpty(paramater.MAVC.Text) || string.IsNullOrEmpty(paramater.PTGG.Text) || string.IsNullOrEmpty(paramater.NgBD.Text) || string.IsNullOrEmpty(paramater.NgKT.Text))
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
                        a.NGBD= (DateTime)paramater.NgBD.SelectedDate;
                        a.NGKT= (DateTime)paramater.NgKT.SelectedDate;



                        MessageBox.Show("Thêm voucher mới thành công !", "THÔNG BÁO");
                        DataProvider.Ins.DB.GIAMGIAs.Add(a);
                        DataProvider.Ins.DB.SaveChanges();
                        paramater.MAVC.Clear();
                        paramater.PTGG.Clear();
                       
                        GiamGiaView giamGiaView = new GiamGiaView();
                        giamGiaView.ListViewGG.ItemsSource = new ObservableCollection<GIAMGIA>(DataProvider.Ins.DB.GIAMGIAs);
                        MainViewModel.MainFrame.Content = giamGiaView;
                    }
                }
            }
        }
    }
}