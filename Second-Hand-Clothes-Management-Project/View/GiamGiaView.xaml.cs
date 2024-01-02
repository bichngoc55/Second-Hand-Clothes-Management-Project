using Second_Hand_Clothes_Management_Project.Model;
using Second_Hand_Clothes_Management_Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Second_Hand_Clothes_Management_Project.View
{
    /// <summary>
    /// Interaction logic for GiamGiaView.xaml
    /// </summary>
    public partial class GiamGiaView : Page
    {
        public GiamGiaView()
        {
            InitializeComponent();
            Loaded += GiamGiaView_Loaded;
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is GiamGiaViewModel viewModel)
            {
                // Lấy đối tượng GIAMGIA từ CommandParameter
                var selectedGiamGia = (sender as Button)?.CommandParameter as GIAMGIA;

                // Kiểm tra xem selectedGiamGia có tồn tại không
                if (selectedGiamGia != null)
                {
                    // Gọi hàm xóa từ ViewModel và truyền đối tượng cần xóa
                    viewModel.DeleteCommand.Execute(selectedGiamGia);
                    ListViewGG.ItemsSource = new ObservableCollection<GIAMGIA>(DataProvider.Ins.DB.GIAMGIAs);
                }
            }
        }

        private void GiamGiaView_Loaded(object sender, RoutedEventArgs e)
        {
            // Cập nhật danh sách voucher khi trang được tải
            ListViewGG.ItemsSource = new ObservableCollection<GIAMGIA>(DataProvider.Ins.DB.GIAMGIAs);
        }
    }
}
