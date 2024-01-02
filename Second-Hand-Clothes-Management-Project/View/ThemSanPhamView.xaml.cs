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
using System.Windows.Shapes;

namespace Second_Hand_Clothes_Management_Project.View
{
    /// <summary>
    /// Interaction logic for ThemSanPhamView.xaml
    /// </summary>
    public partial class ThemSanPhamView : Window
    {
        public ThemSanPhamView()
        {
            InitializeComponent();
            this.Left = SystemParameters.PrimaryScreenWidth / 2 + 500;
            this.Top = SystemParameters.PrimaryScreenHeight / 2 - this.Height / 2;
        }
    }
}
