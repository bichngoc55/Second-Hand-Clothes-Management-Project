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
        GiamGiaViewModel VM;
        private ObservableCollection<GIAMGIA> _listVC;
        public ObservableCollection<GIAMGIA> listVC { get => _listVC; set { _listVC = value; /*OnPropertyChanged();*/ } }
        public GiamGiaView()
        {
            InitializeComponent();
            //VM = DataContext as GiamGiaViewModel;
            this.DataContext = new GiamGiaViewModel();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Gọi đến hàm xử lý tìm kiếm từ ViewModel
            if (DataContext is GiamGiaViewModel viewModel)
            {
                viewModel.SearchCommand.Execute(this);
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra nếu phím nhấn là Enter
            if (e.Key == Key.Enter)
            {
                // Gọi đến hàm xử lý tìm kiếm từ ViewModel
                if (DataContext is GiamGiaViewModel viewModel)
                {
                    viewModel.SearchCommand.Execute(this);
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Gọi đến hàm xử lý tìm kiếm từ ViewModel
            if (DataContext is GiamGiaViewModel viewModel)
            {
                viewModel.AddCommand.Execute(this);
            }
        }

        
    }
}
