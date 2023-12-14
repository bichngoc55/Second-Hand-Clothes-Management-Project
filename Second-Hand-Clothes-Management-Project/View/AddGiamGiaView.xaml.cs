using System;
using System.Collections.Generic;
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
using Second_Hand_Clothes_Management_Project.Model;
using Second_Hand_Clothes_Management_Project.ViewModel;

namespace Second_Hand_Clothes_Management_Project.View
{
    /// <summary>
    /// Interaction logic for AddGiamGiaView.xaml
    /// </summary>
    public partial class AddGiamGiaView : Page
    {
        public AddGiamGiaView()
        {
            InitializeComponent();
            this.DataContext = new AddGiamGiaViewModel();

        }

        private void AddVoucher_Click(object sender, RoutedEventArgs e)
        {
            // Gọi đến hàm xử lý tìm kiếm từ ViewModel
            if (DataContext is AddGiamGiaViewModel viewModel)
            {
                viewModel.AddVC.Execute(this);
            }
        }
    }
}
