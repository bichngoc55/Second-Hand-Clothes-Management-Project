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
    /// Interaction logic for NhapKhoView.xaml
    /// </summary>
    public partial class NhapKhoView : Page
    {
        public NhapKhoView()
        {
            InitializeComponent();
            Loaded += NhapKhoView_Loaded;
        }

        private void NhapKhoView_Loaded(object sender, RoutedEventArgs e)
        {
            // Cập nhật danh sách voucher khi trang được tải
            ListViewNK.ItemsSource = new ObservableCollection<NHAP>(DataProvider.Ins.DB.NHAPs);
        }
    }
}
