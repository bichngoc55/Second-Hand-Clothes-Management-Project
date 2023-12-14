using System;
using Second_Hand_Clothes_Management_Project.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Second_Hand_Clothes_Management_Project.Model
{
    public  class Const :BaseViewModel
    {
        public static string TenDangNhap { get; set; }
        public static bool Admin { get; set; }
        public static NGUOIDUNG ND { get; set; }

        public static string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
    }
}
