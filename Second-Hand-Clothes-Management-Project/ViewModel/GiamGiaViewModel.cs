using Second_Hand_Clothes_Management_Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Second_Hand_Clothes_Management_Project.View;
using Second_Hand_Clothes_Management_Project.ViewModel;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows;
using MaterialDesignThemes.Wpf;
using System.Windows.Media;
using System.Windows.Controls;

namespace Second_Hand_Clothes_Management_Project.ViewModel
{
    public class GiamGiaViewModel : BaseViewModel
    {

        private ObservableCollection<GIAMGIA> _listVC1;
        public ObservableCollection<GIAMGIA> listVC1 { get => _listVC1; set { _listVC1 = value; OnPropertyChanged();} }
        public ICommand LoadCsCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public GiamGiaViewModel()
        {
            listVC1 = new ObservableCollection<GIAMGIA>(DataProvider.Ins.DB.GIAMGIAs);

            SearchCommand = new RelayCommand<GiamGiaView>((p) => { return p == null ? false : true; }, (p) => _SearchCommand(p));
            LoadCsCommand = new RelayCommand<GiamGiaView>((p) => true, (p) => _LoadCsCommand(p));
            AddCommand = new RelayCommand<GiamGiaView>((p) => { return p == null ? false : true; }, (p) => _AddCommand(p));
            DeleteCommand = new RelayCommand<GIAMGIA>((p) => { return p == null ? false : true; }, (p) => _DeleteCommand(p));


        }


        void _LoadCsCommand(GiamGiaView parameter)
        {

            listVC1 = new ObservableCollection<GIAMGIA>(DataProvider.Ins.DB.GIAMGIAs);
            _SearchCommand(parameter);
        }
        void _SearchCommand(GiamGiaView paramater)
        {
            ObservableCollection<GIAMGIA> temp = new ObservableCollection<GIAMGIA>();
            if (!string.IsNullOrEmpty(paramater.txbSearch.Text))
            {
                string searchKeyword = paramater.txbSearch.Text.ToLower();

                // Lọc các mục chứa MAGIAMGIA tương ứng trên danh sách gốc (listVC1)
                temp = new ObservableCollection<GIAMGIA>(listVC1.Where(s => s.MAGIAMGIA.ToLower().Contains(searchKeyword)));
            }
            else
            {
                // Hiển thị toàn bộ danh sách nếu không có từ khóa tìm kiếm
                temp = new ObservableCollection<GIAMGIA>(listVC1);
            }

            paramater.ListViewGG.ItemsSource = temp;
        }


        void _AddCommand(GiamGiaView parameter)
        {
            AddGiamGiaView addGiamGiaView = new AddGiamGiaView();
            double mainWindowRightEdge = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width;
            double dialogWidth = addGiamGiaView.Width; // Điều chỉnh theo kích thước thực tế của dialog
            double dialogLeft = mainWindowRightEdge - dialogWidth;

            // Thiết lập vị trí và khởi chạy cửa sổ dialog
            addGiamGiaView.Left = dialogLeft;
            addGiamGiaView.Top = Application.Current.MainWindow.Top+72;
            addGiamGiaView.WindowStartupLocation = WindowStartupLocation.Manual;
            addGiamGiaView.ShowDialog();
        }


        void _DeleteCommand(GIAMGIA selectedItem)
        {
            if (selectedItem != null)
            {
                // Xóa mục được chọn khỏi danh sách
                listVC1.Remove(selectedItem);

                // Remove the item from the database
                DataProvider.Ins.DB.GIAMGIAs.Remove(selectedItem);

                // Save changes to the database
                DataProvider.Ins.DB.SaveChanges();

            }
        }











    }

}
