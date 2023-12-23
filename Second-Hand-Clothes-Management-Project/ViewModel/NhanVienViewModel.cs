using MaterialDesignThemes.Wpf;
using Second_Hand_Clothes_Management_Project.Model;
using Second_Hand_Clothes_Management_Project.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
namespace Second_Hand_Clothes_Management_Project.ViewModel
{
    public class NhanVienViewModel : BaseViewModel
    {
        private string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
        private ObservableCollection<NGUOIDUNG> _listND;
        public ObservableCollection<NGUOIDUNG> listND { get => _listND; set { _listND = value; /*OnPropertyChanged();*/ } }
        private ObservableCollection<NGUOIDUNG> _listND1;
        public ObservableCollection<NGUOIDUNG> listND1 { get => _listND1; set { _listND1 = value; /*OnPropertyChanged();*/ } }
        public ICommand SearchCommand { get; set; }
        public ICommand DetailNDCommand { get; set; }
        public ICommand AddNDCommand { get; set; }
        public ICommand LoadCsCommand { get; set; }
        private ObservableCollection<string> _listTK;
        public ObservableCollection<string> listTK { get => _listTK; set { _listTK = value; OnPropertyChanged(); } }
        public ICommand Filter { get; set; }
        public NhanVienViewModel()
        {
            listTK = new ObservableCollection<string>() { "Mã NV", "Tên NV", "SDT" };
            listND1 = new ObservableCollection<NGUOIDUNG>(DataProvider.Ins.DB.NGUOIDUNGs.Where(p => p.QTV == false));
            listND = new ObservableCollection<NGUOIDUNG>(listND1.GroupBy(p => p.TENND).Select(grp => grp.FirstOrDefault()));
            AddNDCommand = new RelayCommand<NhanVienView>((p) => { return p == null ? false : true; }, (p) => _AddNDCommand(p));
            SearchCommand = new RelayCommand<NhanVienView>((p) => { return p == null ? false : true; }, (p) => _SearchCommand(p));
            DetailNDCommand = new RelayCommand<NhanVienView>((p) => { return p.ListViewNhanVien.SelectedItem == null ? false : true; }, (p) => _DetailND(p));
            LoadCsCommand = new RelayCommand<NhanVienView>((p) => true, (p) => _LoadCsCommand(p));
            //Filter = new RelayCommand<NhanVienView>((p) => true, (p) => _Filter(p));
        }
        void _LoadCsCommand(NhanVienView parameter)
        {
            listTK = new ObservableCollection<string>() { "Mã NV", "Tên NV", "SDT" };
            listND1 = new ObservableCollection<NGUOIDUNG>(DataProvider.Ins.DB.NGUOIDUNGs.Where(p => p.QTV == false));
            listND = new ObservableCollection<NGUOIDUNG>(listND1.GroupBy(p => p.TENND).Select(grp => grp.FirstOrDefault()));
            parameter.cbxTimKiemNhanVien.SelectedIndex = 0;
            //_Filter(parameter);
            _SearchCommand(parameter);
        }
        //void _Filter(NhanVienView parameter)
        //{
        //    switch (parameter.cbxBoLocNhanVien.SelectedIndex.ToString())
        //    {
        //        case "0":
        //            {
        //                listND = new ObservableCollection<NGUOIDUNG>(listND1.GroupBy(p => p.TENND).Select(grp => grp.FirstOrDefault()));
        //                parameter.ListViewNhanVien.ItemsSource = listND;
        //                break;
        //            }
        //        case "1":
        //            {
        //                listND = new ObservableCollection<NGUOIDUNG>(listND1.GroupBy(p => p.TENND).Select(grp => grp.FirstOrDefault()).Where(p => p.LOAISP == "Sơ mi"));
        //                parameter.ListViewNhanVien.ItemsSource = listND;
        //                break;
        //            }
        //        case "2":
        //            {
        //                listND = new ObservableCollection<NGUOIDUNG>(listND1.GroupBy(p => p.TENND).Select(grp => grp.FirstOrDefault()).Where(p => p.LOAISP == "Áo thun"));
        //                parameter.ListViewNhanVien.ItemsSource = listND;
        //                break;
        //            }
        //        case "3":
        //            {
        //                listND = new ObservableCollection<NGUOIDUNG>(listND1.GroupBy(p => p.TENND).Select(grp => grp.FirstOrDefault()).Where(p => p.LOAISP == "Hoodie"));
        //                parameter.ListViewNhanVien.ItemsSource = listND;
        //                parameter.ListViewNhanVien.ItemsSource = listND;
        //                break;
        //            }
        //    }

        //}
        void _SearchCommand(NhanVienView paramater)
        {
            ObservableCollection<NGUOIDUNG> temp = new ObservableCollection<NGUOIDUNG>();
            if (paramater.cbxTimKiemNhanVien.Text != "")
            {
                switch (paramater.cbxTimKiemNhanVien.SelectedItem.ToString())
                {
                    case "Tên NV":
                        {
                            foreach (NGUOIDUNG s in listND)
                            {
                                if (s.TENND.ToLower().Contains(paramater.tbxTimKiemNhanVien.Text.ToLower()))
                                {
                                    temp.Add(s);
                                }
                            }
                            break;
                        }
                    case "SDT":
                        {
                            try
                            {
                                foreach (NGUOIDUNG s in listND)
                                {
                                    if (s.SDT.ToLower().Contains(paramater.tbxTimKiemNhanVien.Text.ToLower()))
                                    {
                                        temp.Add(s);
                                    }
                                }
                            }
                            catch { }
                            break;
                        }
                    case "Mã NV":
                        {
                            foreach (NGUOIDUNG s in listND)
                            {
                                if (s.MAND.ToLower().Contains(paramater.tbxTimKiemNhanVien.Text.ToLower()))
                                {
                                    temp.Add(s);
                                }
                            }
                            break;
                        }
                    default:
                        {
                            foreach (NGUOIDUNG s in listND)
                            {
                                if (s.TENND.ToLower().Contains(paramater.tbxTimKiemNhanVien.Text.ToLower()))
                                {
                                    temp.Add(s);
                                }
                            }
                            break;
                        }
                }
                paramater.ListViewNhanVien.ItemsSource = temp;
            }
            else
                paramater.ListViewNhanVien.ItemsSource = listND;
        }
        void _DetailND(NhanVienView paramater)
        {
            ChiTietNhanVien detailProduct = new ChiTietNhanVien();
            NGUOIDUNG temp = (NGUOIDUNG)paramater.ListViewNhanVien.SelectedItem;
            detailProduct.TenND.Text = temp.TENND;
            detailProduct.DiaChiND.Text = temp.DIACHI;
            detailProduct.NgSinhND.Text = temp.NGSINH.ToString();
            detailProduct.SDTND.Text = temp.SDT;
            detailProduct.gioitinh.Text = temp.GIOITINH;
            detailProduct.Email.Text = temp.MAIL;
            detailProduct.TaiKhoanND.Text = temp.USERNAME;
            detailProduct.MatKhauND.Text = temp.PASS;
            Uri fileUri = new Uri(temp.AVA, UriKind.Relative);
            detailProduct.HinhAnhNhanVien.Source = new BitmapImage(fileUri);
            listND1 = new ObservableCollection<NGUOIDUNG>(DataProvider.Ins.DB.NGUOIDUNGs.Where(p => (p.TTND == true && p.QTV == false)));
            paramater.ListViewNhanVien.ItemsSource = listND1;
            paramater.ListViewNhanVien.SelectedItem = null;
            //_Filter(paramater);
            _SearchCommand(paramater);
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
                ma = "NV" + rand.Next(0, 10000).ToString();
            } while (check(ma));
            return ma;
        }
        public static string MD5Hash(string value)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(Encoding.UTF8.GetBytes(value));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static string Base64Encode(string password)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(plainTextBytes);
        }
        void _AddNDCommand(NhanVienView paramater)
        {
            ThemNhanVienView themSanPhamView = new ThemNhanVienView();
            themSanPhamView.MaNd.Text = rdma();
            listND1 = new ObservableCollection<NGUOIDUNG>(DataProvider.Ins.DB.NGUOIDUNGs.Where(p => p.QTV == false));
            //_Filter(paramater);
            _SearchCommand(paramater);
            paramater.ListViewNhanVien.ItemsSource = listND1;
            paramater.ListViewNhanVien.Items.Refresh();
            themSanPhamView.ShowDialog();
        }
    }
}
