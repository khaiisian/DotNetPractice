using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotNetPractice.RealtimeChartApp1.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BlogTbl> BlogTbls { get; set; }

    public virtual DbSet<StudentTable> StudentTables { get; set; }

    public virtual DbSet<StudentTbl> StudentTbls { get; set; }

    public virtual DbSet<TblExtra> TblExtras { get; set; }

    public virtual DbSet<TblPieChart> TblPieCharts { get; set; }

    public virtual DbSet<TblPizza> TblPizzas { get; set; }

    public virtual DbSet<TblPizzaDetailOrder> TblPizzaDetailOrders { get; set; }

    public virtual DbSet<TblPizzaOrder> TblPizzaOrders { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.;Database=DotNetSelfStudy;User ID=sa;Password=sa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogTbl>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("Blog_tbl");

            entity.Property(e => e.BlogAuthor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BlogContent)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BlogTitle)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StudentTable>(entity =>
        {
            entity.HasKey(e => e.StudentId);

            entity.ToTable("student_Table");

            entity.Property(e => e.StudentId).HasColumnName("studentID");
            entity.Property(e => e.StudentAddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("studentAddress");
            entity.Property(e => e.StudentAge).HasColumnName("studentAge");
            entity.Property(e => e.StudentMail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("studentMail");
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("studentName");
        });

        modelBuilder.Entity<StudentTbl>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Student_Tbl");

            entity.Property(e => e.AvgScore).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.StudentId).ValueGeneratedOnAdd();
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblExtra>(entity =>
        {
            entity.HasKey(e => e.ExtraId);

            entity.ToTable("Tbl_Extra");

            entity.Property(e => e.ExtraId).HasColumnName("Extra_Id");
            entity.Property(e => e.ExtraName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Extra_Name");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TblPieChart>(entity =>
        {
            entity.HasKey(e => e.PieChartId);

            entity.ToTable("Tbl_PieChart");

            entity.Property(e => e.PieChartName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PieChartValue).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TblPizza>(entity =>
        {
            entity.HasKey(e => e.PizzaId);

            entity.ToTable("Tbl_Pizza");

            entity.Property(e => e.PizzaId).HasColumnName("Pizza_Id");
            entity.Property(e => e.PizzaName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Pizza_Name");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TblPizzaDetailOrder>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId);

            entity.ToTable("Tbl_PizzaDetailOrder");

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetail_Id");
            entity.Property(e => e.ExtraId).HasColumnName("Extra_Id");
            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
        });

        modelBuilder.Entity<TblPizzaOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_Tbl_PizzaDetail");

            entity.ToTable("Tbl_PizzaOrder");

            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.InvoiceNum)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Invoice_Num");
            entity.Property(e => e.PizzaId).HasColumnName("Pizza_Id");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Total_Amount");

            entity.HasOne(d => d.Pizza).WithMany(p => p.TblPizzaOrders)
                .HasForeignKey(d => d.PizzaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_PizzaDetail_Tbl_PizzaDetail");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
