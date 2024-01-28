using System;
using System.Collections.Generic;
using ManageEmployees.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Infrastructures.Database;

public partial class ManageEmployeeDbContext : DbContext
{
    public ManageEmployeeDbContext()
    {
    }

    public ManageEmployeeDbContext(DbContextOptions<ManageEmployeeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeesDepartment> EmployeesDepartments { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<LeaveRequestStatus> LeaveRequestStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=8889;user=root;password=root;database=ManageEmployees", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.39-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PRIMARY");

            entity.HasIndex(e => e.EmployeeId, "EmployeeId");

            entity.Property(e => e.AttendanceId).HasColumnType("int(11)");
            entity.Property(e => e.EmployeeId).HasColumnType("int(11)");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("attendances_ibfk_1");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PRIMARY");

            entity.HasIndex(e => e.Name, "Name").IsUnique();

            entity.Property(e => e.DepartmentId).HasColumnType("int(11)");
            entity.Property(e => e.Address).HasMaxLength(150);
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

            entity.HasIndex(e => e.Email, "UK_Employees_Email").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnType("int(11)");
            entity.Property(e => e.BirthdDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(13);
            entity.Property(e => e.Position).HasMaxLength(150);
        });

        modelBuilder.Entity<EmployeesDepartment>(entity =>
        {
            entity.HasKey(e => e.EmployeesDepartmentsId).HasName("PRIMARY");

            entity.HasIndex(e => e.DepartmentId, "DepartmentId");

            entity.HasIndex(e => e.EmployeeId, "EmployeeId");

            entity.Property(e => e.EmployeesDepartmentsId).HasColumnType("int(11)");
            entity.Property(e => e.DepartmentId).HasColumnType("int(11)");
            entity.Property(e => e.EmployeeId).HasColumnType("int(11)");

            entity.HasOne(d => d.Department).WithMany(p => p.EmployeesDepartments)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employeesdepartments_ibfk_2");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeesDepartments)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employeesdepartments_ibfk_1");
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.LeaveRequestId).HasName("PRIMARY");

            entity.HasIndex(e => e.EmployeeId, "EmployeeId");

            entity.HasIndex(e => e.LeaveRequestStatusId, "LeaveRequestStatusId");

            entity.Property(e => e.LeaveRequestId).HasColumnType("int(11)");
            entity.Property(e => e.EmployeeId).HasColumnType("int(11)");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.LeaveRequestStatusId).HasColumnType("int(11)");
            entity.Property(e => e.RequestDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("leaverequests_ibfk_1");

            entity.HasOne(d => d.LeaveRequestStatus).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.LeaveRequestStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("leaverequests_ibfk_2");
        });

        modelBuilder.Entity<LeaveRequestStatus>(entity =>
        {
            entity.HasKey(e => e.LeaveRequestStatusId).HasName("PRIMARY");

            entity.ToTable("LeaveRequestStatus");

            entity.HasIndex(e => e.Status, "Status").IsUnique();

            entity.Property(e => e.LeaveRequestStatusId).HasColumnType("int(11)");
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
