using MaterialDesignThemes.Wpf;
using Second_Hand_Clothes_Management_Project.Model;
using Second_Hand_Clothes_Management_Project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Second_Hand_Clothes_Management_Project.ViewModel
{
    public class SanPhamViewModel : BaseViewModel
    {
        private string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
        private ObservableCollection<SANPHAM> _listSP;
        public ObservableCollection<SANPHAM> listSP { get => _listSP; set { _listSP = value; /*OnPropertyChanged();*/ } }

        private ObservableCollection<SANPHAM> _listSP1;
        public ObservableCollection<SANPHAM> listSP1 { get => _listSP1; set { _listSP1 = value; /*OnPropertyChanged();*/ } }
        private ObservableCollection<GIAMGIA> _listVoucher;
        public ObservableCollection<GIAMGIA> listVoucher { get => _listVoucher; set { _listVoucher = value; } }
        private ObservableCollection<string> _listVC;
        public ObservableCollection<string> listVC { get => _listVC; set { _listVC = value; OnPropertyChanged(); } }

        public ICommand SearchCommand { get; set; }
        public ICommand DetailPdCommand { get; set; }
        public ICommand AddPdPdCommand { get; set; }
        public ICommand LoadCsCommand { get; set; }
        private ObservableCollection<string> _listTK;
        public ObservableCollection<string> listTK { get => _listTK; set { _listTK = value; OnPropertyChanged(); } }
        public ICommand Filter { get; set; }
        public SanPhamViewModel()
        {
            listTK = new ObservableCollection<string>() { "Tên SP", "Giá SP" };
            listSP1 = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(p => p.SL >= 0));
            listSP = new ObservableCollection<SANPHAM>(listSP1.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()));
            AddPdPdCommand = new RelayCommand<SanPhamView>((p) => { return p == null ? false : true; }, (p) => _AddPdCommand(p));
            SearchCommand = new RelayCommand<SanPhamView>((p) => { return p == null ? false : true; }, (p) => _SearchCommand(p));
            DetailPdCommand = new RelayCommand<SanPhamView>((p) => { return p.ListViewProduct.SelectedItem == null ? false : true; }, (p) => _DetailPd(p));
            LoadCsCommand = new RelayCommand<SanPhamView>((p) => true, (p) => _LoadCsCommand(p));
            Filter = new RelayCommand<SanPhamView>((p) => true, (p) => _Filter(p));
        }
        void _LoadCsCommand(SanPhamView parameter)
        {
            listTK = new ObservableCollection<string>() { "Tên SP", "Giá SP" };
            listSP1 = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(p => p.SL >= 0));
            listSP = new ObservableCollection<SANPHAM>(listSP1.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()));
            parameter.cbxBoLoc.SelectedIndex = 0;
            parameter.cbxTimKiem.SelectedIndex = 0;
            _Filter(parameter);
            _SearchCommand(parameter);
        }
        void _Filter(SanPhamView parameter)
        {
            switch (parameter.cbxBoLoc.SelectedIndex.ToString())
            {
                case "0":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP1.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
                case "1":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP1.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()).Where(p => p.LOAISP == "Sơ mi"));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
                case "2":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP1.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()).Where(p => p.LOAISP == "Áo thun"));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
                case "3":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP1.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()).Where(p => p.LOAISP == "Hoodie"));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
                case "4":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP1.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()).Where(p => p.LOAISP == "Yếm"));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
                case "5":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP1.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()).Where(p => p.LOAISP == "Váy"));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
                case "6":
                    {
                        listSP = new ObservableCollection<SANPHAM>(listSP1.GroupBy(p => p.TENSP).Select(grp => grp.FirstOrDefault()).Where(p => p.LOAISP == "Trackpant"));
                        parameter.ListViewProduct.ItemsSource = listSP;
                        break;
                    }
            }

        }
        void _SearchCommand(SanPhamView paramater)
        {
            ObservableCollection<SANPHAM> temp = new ObservableCollection<SANPHAM>();
            if (paramater.cbxTimKiem.Text != "")
            {
                switch (paramater.cbxTimKiem.SelectedItem.ToString())
                {
                    case "Tên SP":
                        {
                            foreach (SANPHAM s in listSP)
                            {
                                if (s.TENSP.ToLower().Contains(paramater.tbxTimKiem.Text.ToLower()))
                                {
                                    temp.Add(s);
                                }
                            }
                            break;
                        }
                    case "Giá SP":
                        {
                            try
                            {
                                foreach (SANPHAM s in listSP)
                                {
                                    if (s.GIA <= int.Parse(paramater.tbxTimKiem.Text))
                                    {
                                        temp.Add(s);
                                    }
                                }
                            }
                            catch { }
                            break;
                        }
                    default:
                        {
                            foreach (SANPHAM s in listSP)
                            {
                                if (s.TENSP.ToLower().Contains(paramater.tbxTimKiem.Text.ToLower()))
                                {
                                    temp.Add(s);
                                }
                            }
                            break;
                        }
                }
                paramater.ListViewProduct.ItemsSource = temp;
            }
            else
                paramater.ListViewProduct.ItemsSource = listSP;
        }
        void _DetailPd(SanPhamView paramater)
        {
            ChiTietSanPham detailProduct = new ChiTietSanPham();
            SANPHAM temp = (SANPHAM)paramater.ListViewProduct.SelectedItem;
            detailProduct.TenSP.Text = temp.TENSP;
            detailProduct.GiaSP.Text = string.Format("{0:0,0}", temp.GIA) + " VNĐ";
            detailProduct.LoaiSP.Text = temp.LOAISP;
            string SL = listSP1.Where(p => p.TENSP == temp.TENSP && p.SL >= 0).Select(p => p.SL).Sum().ToString();
            detailProduct.SLSP.Text = "Số lượng: " + SL;
            detailProduct.kichco.ItemsSource = new ObservableCollection<SANPHAM>(listSP1.Where(p => p.TENSP == temp.TENSP && p.SL >= 0));
            detailProduct.Mota.Text = temp.MOTA;

            //if(temp.MAGIAMGIA == null)
            //{
            //    detailProduct.VoucherSP.Text = "None";
            //}
            //else
            //{
            //    detailProduct.VoucherSP.Text = temp.MAGIAMGIA;
            //}


            Uri fileUri = new Uri(temp.HINHSP, UriKind.Relative);
            detailProduct.HinhAnh.Source = new BitmapImage(fileUri);
            listSP1 = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(p => p.SL > 0));
            paramater.ListViewProduct.ItemsSource = listSP1;
            paramater.ListViewProduct.SelectedItem = null;
            _Filter(paramater);
            _SearchCommand(paramater);
            //MainViewModel.MainFrame.Content = detailProduct;
            detailProduct.ShowDialog();
        }
        bool check(string m)
        {
            foreach (SANPHAM temp in DataProvider.Ins.DB.SANPHAMs)
            {
                if (temp.MASP == m)
                    return true;
            }
            return false;
        }
        string rdma()
        {
            string ma;
            do
            {
                Random rand = new Random();
                ma = "PD" + rand.Next(0, 10000).ToString();
            } while (check(ma));
            return ma;
        }
        void _AddPdCommand(SanPhamView paramater)
        {
            ThemSanPhamView themSanPhamView = new ThemSanPhamView();
            themSanPhamView.MaSp.Text = rdma();
            listSP1 = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(p => p.SL >= 0));
            _Filter(paramater);
            _SearchCommand(paramater);
            paramater.ListViewProduct.ItemsSource = listSP1;
            paramater.ListViewProduct.Items.Refresh();
            listVoucher = new ObservableCollection<GIAMGIA>(DataProvider.Ins.DB.GIAMGIAs);
            listVC = new ObservableCollection<string> { };
            foreach (GIAMGIA p in listVoucher)
            {
                listVC.Add(p.MAGIAMGIA.ToString());
            }
            listVC.Add("NULL");
            themSanPhamView.Voucher.ItemsSource= listVC;
            themSanPhamView.ShowDialog();
        }
    }
}
