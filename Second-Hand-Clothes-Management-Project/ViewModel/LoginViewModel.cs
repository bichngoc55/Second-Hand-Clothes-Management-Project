using Second_Hand_Clothes_Management_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input; 
using Second_Hand_Clothes_Management_Project.View;
using Second_Hand_Clothes_Management_Project.ViewModel;

namespace Second_Hand_Clothes_Management_Project.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public static bool IsLogin { get; set; }
        private string _Username;
        public string Username { get => _Username; set { _Username = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        //public Button LoginButton { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public ICommand LoginCommand { get; set; }
        public LoginViewModel()
        {
            IsLogin = false;
            Password = "";
            Username = "";

            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            PasswordChangedCommand= new RelayCommand<PasswordBox> ((p) => { return true; }, (p) =>
            {
                Password = p.Password;
            }); 
        }

        private void Login(Window p)
        {
            var typedObject = (TAIKHOAN)obj;
            var USERNAME = typedObject.USERNAME;
            try {                 
                if (p == null)
                     return;
                string passEncode = Base64Encode(Password);
                var accCount = DataProvider.Ins.DB.TAIKHOAN.Where(x => x.USERNAME == Username).Count();
                if (accCount > 0)
                {
                    IsLogin = true;
                    Const.TenDangNhap = Username;
                    p.Close();
                }
                else
                {
                    IsLogin = false;
                    MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!");
                }           
            }
            catch
            {
                MessageBox.Show("Mất kết nối đến cơ sở dữ liệu!", "Thông báo", MessageBoxButton.OK);
            }
            p.
        }

        private string MD5Hash(string value)
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

        private string Base64Encode(string password)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(plainTextBytes);
        }

        
    }
}