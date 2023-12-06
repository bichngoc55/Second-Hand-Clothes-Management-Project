using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Second_Hand_Clothes_Management_Project.Model
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=QLBHEntities")
        {
        }

        public virtual DbSet<GIAMGIA> GIAMGIAs { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public virtual DbSet<KHO> KHOes { get; set; }
        public virtual DbSet<MUAHANG> MUAHANGs { get; set; }
        public virtual DbSet<NHACUNGCAP> NHACUNGCAPs { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<NHAP> NHAPs { get; set; }
        public virtual DbSet<SANPHAM> SANPHAMs { get; set; }
        public virtual DbSet<TAIKHOAN> TAIKHOANs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GIAMGIA>()
                .Property(e => e.MAGIAMGIA)
                .IsUnicode(false);

            modelBuilder.Entity<GIAMGIA>()
                .Property(e => e.PHANTRAM)
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.MAKH)
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.LOAIKH)
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .HasMany(e => e.MUAHANGs)
                .WithRequired(e => e.KHACHHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KHO>()
                .Property(e => e.MAKHO)
                .IsUnicode(false);

            modelBuilder.Entity<KHO>()
                .HasMany(e => e.NHAPs)
                .WithRequired(e => e.KHO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MUAHANG>()
                .Property(e => e.MANV)
                .IsUnicode(false);

            modelBuilder.Entity<MUAHANG>()
                .Property(e => e.MASP)
                .IsUnicode(false);

            modelBuilder.Entity<MUAHANG>()
                .Property(e => e.MAKH)
                .IsUnicode(false);

            modelBuilder.Entity<NHACUNGCAP>()
                .Property(e => e.MANHACUNGCAP)
                .IsUnicode(false);

            modelBuilder.Entity<NHACUNGCAP>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<NHACUNGCAP>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<NHACUNGCAP>()
                .HasMany(e => e.NHAPs)
                .WithRequired(e => e.NHACUNGCAP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.MANV)
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .HasMany(e => e.MUAHANGs)
                .WithRequired(e => e.NHANVIEN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NHAP>()
                .Property(e => e.MASP)
                .IsUnicode(false);

            modelBuilder.Entity<NHAP>()
                .Property(e => e.MANHACUNGCAP)
                .IsUnicode(false);

            modelBuilder.Entity<NHAP>()
                .Property(e => e.MAKHO)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.MASP)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.GIA)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.SIZE)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.HINHSP)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.MAGIAMGIA)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.MUAHANGs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.NHAPs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.MATAIKHOAN)
                .IsUnicode(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.USERNAME)
                .IsUnicode(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.AVATAR)
                .IsUnicode(false);
        }
    }
}