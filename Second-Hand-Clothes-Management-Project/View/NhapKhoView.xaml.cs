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
using Xamarin.Forms;
using Page = System.Windows.Controls.Page;
using SelectionChangedEventArgs = System.Windows.Controls.SelectionChangedEventArgs;

namespace Second_Hand_Clothes_Management_Project.View
{
    /// <summary>
    /// Interaction logic for NhapKhoView.xaml
    /// </summary>
    public partial class NhapKhoView : Page
    {
        public NhapKhoView()
        {
            InitializeComponent();
            Loaded += NhapKhoView_Loaded;
        }

        private void ListViewNK_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                // Assuming NHAP is the type of your items in the ListViewNK
                var selectedNhapKho = e.AddedItems[0] as NHAP;

            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is NhapKhoViewModel viewModel)
            {
                // Lấy đối tượng NHAP từ ListViewNK
                var selectedNhapKho = ListViewNK.SelectedItem as NHAP;

                // Kiểm tra xem selectedNhapKho có tồn tại không
                if (selectedNhapKho != null)
                {
                    // Gọi hàm xóa từ ViewModel và truyền đối tượng cần xóa
                    viewModel.DeleteCommand.Execute(selectedNhapKho);

                    // Update the ListViewNK's item source if needed
                    ListViewNK.ItemsSource = new ObservableCollection<NHAP>(DataProvider.Ins.DB.NHAPs);
                }
            }
        }

        private void NhapKhoView_Loaded(object sender, RoutedEventArgs e)
        {
            // Cập nhật danh sách voucher khi trang được tải
            ListViewNK.ItemsSource = new ObservableCollection<NHAP>(DataProvider.Ins.DB.NHAPs);
        }
    }
}
