using Second_Hand_Clothes_Management_Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Second_Hand_Clothes_Management_Project.View;

namespace Second_Hand_Clothes_Management_Project.ViewModel
{
    class ChiTietHoaDonViewModel
    {
        public ICommand Loadwd { get; set; }
        public ICommand Back { get; set; }
        public ICommand Delete { get; set; }
        public ChiTietHoaDonViewModel()
        {
            Back = new RelayCommand<ChiTietHoaDonView>((p) => true, (p) => _Back(p));
            Delete = new RelayCommand<ChiTietHoaDonView>((p) => true, (p) => _Delete(p));
        }
        void _Back(ChiTietHoaDonView p)
        {
            ThanhToanView importView = new ThanhToanView();
            MainViewModel.MainFrame.Content = importView;
        }
        void _Delete(ChiTietHoaDonView parameter)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn xóa phiếu nhập này?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                foreach (HOADON temp in DataProvider.Ins.DB.HOADONs)
                {
                    if (temp.SOHD == parameter.SoHD.Text)
                    {
                        foreach (CTHD temp1 in temp.CTHDs)
                        {
                            foreach (SANPHAM temp2 in DataProvider.Ins.DB.SANPHAMs)
                            {
                                if (temp1.MASP == temp2.MASP)
                                {
                                    if (temp2.SL - temp1.SL < 0)
                                    {
                                        MessageBox.Show("Không thể xóa phiếu nhập vì sản phẩm nhập đã được bán !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return;
                                    }
                                    else
                                        temp2.SL -= temp1.SL;
                                }
                            }
                        }
                        DataProvider.Ins.DB.HOADONs.Remove(temp);
                    }
                }
                DataProvider.Ins.DB.SaveChanges();
                ThanhToanView importView = new ThanhToanView();
                importView.ListViewProduct.ItemsSource = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
                MainViewModel.MainFrame.Content = importView;
            }
        }
    }
}
