using Second_Hand_Clothes_Management_Project.Model;
using Second_Hand_Clothes_Management_Project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Second_Hand_Clothes_Management_Project.ViewModel
{
    public class NhapKhoViewModel : BaseViewModel
    {
        private ObservableCollection<NHAP> _listSP;
        public ObservableCollection<NHAP> listSP { get => _listSP; set { _listSP = value; OnPropertyChanged();} }
    

        public ICommand SearchCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand LoadCsCommand { get; set; }

        public NhapKhoViewModel()
        {
            listSP = new ObservableCollection<NHAP>(DataProvider.Ins.DB.NHAPs);
            SearchCommand = new RelayCommand<NhapKhoView>((p) => { return p == null ? false : true; }, (p) => _SearchCommand(p));
            AddCommand = new RelayCommand<NhapKhoView>((p) => { return p == null ? false : true; }, (p) => _AddCommand(p));
            LoadCsCommand = new RelayCommand<NhapKhoView>((p) => true, (p) => _LoadCsCommand(p));
            DeleteCommand = new RelayCommand<NHAP>((p) => { return p == null ? false : true; }, (p) => _DeleteCommand(p));

        }
        void _LoadCsCommand(NhapKhoView parameter)
        {
            listSP = new ObservableCollection<NHAP>(DataProvider.Ins.DB.NHAPs);
            
            _AddCommand(parameter);
            _SearchCommand(parameter);
        }

        void _SearchCommand(NhapKhoView paramater)
        {
            ObservableCollection<NHAP> temp = new ObservableCollection<NHAP>();
            if (!string.IsNullOrEmpty(paramater.txbSearch.Text))
            {
                string searchKeyword = paramater.txbSearch.Text.ToLower();

                // Lọc các mục chứa MAGIAMGIA tương ứng trên danh sách gốc (listVC1)
                temp = new ObservableCollection<NHAP>(listSP.Where(s => s.MASP.ToLower().Contains(searchKeyword)));
            }
            else
            {
                // Hiển thị toàn bộ danh sách nếu không có từ khóa tìm kiếm
                temp = new ObservableCollection<NHAP>(listSP);
            }

            paramater.ListViewNK.ItemsSource = temp;
        }

        void _AddCommand(NhapKhoView parameter)
        {

            AddNhapKhoView addNhapKhoView = new AddNhapKhoView();
           
            double mainWindowRightEdge = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width;
            double dialogWidth = addNhapKhoView.Width; // Điều chỉnh theo kích thước thực tế của dialog
            double dialogLeft = mainWindowRightEdge - dialogWidth;

            // Thiết lập vị trí và khởi chạy cửa sổ dialog
            addNhapKhoView.Left = dialogLeft;
            addNhapKhoView.Top = Application.Current.MainWindow.Top + 72;
            addNhapKhoView.WindowStartupLocation = WindowStartupLocation.Manual;
            addNhapKhoView.ShowDialog();
        }

        void _DeleteCommand(NHAP selectedItem)
        {
          
                if (selectedItem != null)
                {
                    // Xóa mục được chọn khỏi danh sách
                    listSP.Remove(selectedItem);
                    // Remove the item from the database
                    DataProvider.Ins.DB.NHAPs.Remove(selectedItem);

                    // Save changes to the database
                    DataProvider.Ins.DB.SaveChanges();

                   
                }
            
        }


    }



}
