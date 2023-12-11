﻿using Second_Hand_Clothes_Management_Project.Model;
using Second_Hand_Clothes_Management_Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;

namespace Second_Hand_Clothes_Management_Project.ViewModel
{
    public class ChiTietSanPhamViewModel : BaseViewModel
    {
        public ICommand Back { get; set; }
        public ICommand UpdateProduct { get; set; }
        public ICommand GetName { get; set; }
        private string TenSP1;
        public ICommand Loadwd { get; set; }
        public ICommand DeleteProduct { get; set; }
        public ChiTietSanPhamViewModel()
        {
            GetName = new RelayCommand<ChiTietSanPham>((p) => true, (p) => _GetName(p));
            Back = new RelayCommand<ChiTietSanPham>((p) => true, (p) => _Back(p));
            UpdateProduct = new RelayCommand<ChiTietSanPham>((p) => true, (p) => _UpdateProduct(p));
            Loadwd = new RelayCommand<ChiTietSanPham>((p) => true, (p) => _Loadwd(p));
            DeleteProduct = new RelayCommand<ChiTietSanPham>((p) => true, (p) => _DeleteProduct(p));
        }
        void _Loadwd(ChiTietSanPham parmater)
        {
            if (Const.Admin)
            {
                parmater.TenSP.IsEnabled = true;
                parmater.Mota.IsEnabled = true;
                parmater.btncapnhat.Visibility = Visibility.Visible;
                parmater.btnxoa.Visibility = Visibility.Visible;
            }
            else
            {
                parmater.TenSP.IsEnabled = false;
                parmater.Mota.IsEnabled = false;
                parmater.Mota.Height = 200;
            }
        }
        void _Back(ChiTietSanPham p)
        {
            SanPhamView productViewPage = new SanPhamView();
            MainViewModel.MainFrame.Content = productViewPage;
        }
        void _DeleteProduct(ChiTietSanPham parameter)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn xóa sản phẩm ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                foreach (SANPHAM a in DataProvider.Ins.DB.SANPHAMs.Where(pa => (pa.TENSP == TenSP1 && pa.SL >= 0)))
                {
                    a.SL = -1;
                }
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Xóa sản phẩm thành công !", "THÔNG BÁO");
                SanPhamView productView = new SanPhamView();
                productView.ListViewProduct.ItemsSource = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(p => p.SL > 0));
                MainViewModel.MainFrame.Content = productView;
            }
        }
        void _GetName(ChiTietSanPham p)
        {
            TenSP1 = p.TenSP.Text;
        }
        void _UpdateProduct(ChiTietSanPham p)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn cập nhật sản phẩm ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                if (string.IsNullOrEmpty(p.TenSP.Text) || string.IsNullOrEmpty(p.Mota.Text) || string.IsNullOrEmpty(p.Mota.Text))
                {
                    MessageBox.Show("Thông tin chưa đầy đủ !", "THÔNG BÁO");
                }
                else
                {
                    foreach (SANPHAM a in DataProvider.Ins.DB.SANPHAMs.Where(pa => (pa.TENSP == TenSP1 && pa.SL >= 0)))
                    {
                        a.TENSP = p.TenSP.Text;
                        a.MOTA = p.Mota.Text;
                        a.MOTA = p.Mota.Text;
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Cập nhật sản phẩm thành công !", "THÔNG BÁO");
                    SanPhamView productView = new SanPhamView();
                    productView.ListViewProduct.ItemsSource = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs);
                    MainViewModel.MainFrame.Content = productView;
                }
            }
        }
    }
}