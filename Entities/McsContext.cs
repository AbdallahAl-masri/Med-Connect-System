using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MCS.Entities;

public partial class McsContext : IdentityDbContext<DeptStaff, ApplicationRole, long, AspNetUserClaim, AspNetUserRole, AspNetUserLogin, AspNetRoleClaim, AspNetUserToken>
{

    public McsContext(DbContextOptions<McsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Clinic> Clinics { get; set; }

    public virtual DbSet<ClinicStaff> ClinicStaffs { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DeptStaff> DeptStaffs { get; set; }

    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorAttendance> DoctorAttendances { get; set; }

    public virtual DbSet<Inpatient> Inpatients { get; set; }

    public virtual DbSet<Insurance> Insurances { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Medication> Medications { get; set; }

    public virtual DbSet<Outpatient> Outpatients { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientAppointment> PatientAppointments { get; set; }

    public virtual DbSet<PatientRadiology> PatientRadiologies { get; set; }

    public virtual DbSet<PatientVisit> PatientVisits { get; set; }

    public virtual DbSet<Pharmacy> Pharmacies { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Radiology> Radiologies { get; set; }

    public virtual DbSet<Test> Tests { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configure ApplicationRole
        modelBuilder.Entity<ApplicationRole>(entity =>
        {
            base.OnModelCreating(modelBuilder);
            entity.ToTable("ApplicationRoles"); // Unique table name for ApplicationRole
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("getutcdate()");
        });
        // Configure IdentityRoleClaim<long> to use a distinct table
        modelBuilder.Entity<IdentityRoleClaim<long>>(entity =>
        {
            entity.ToTable("IdentityRoleClaims"); // Unique table name for IdentityRoleClaim<long>
        });
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.Status).HasColumnType("text");
            entity.Property(e => e.Timeslot).HasColumnType("datetime");

            entity.HasOne(d => d.Department).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_appoitments_department");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.Property(e => e.ConcurrencyStamp).HasColumnType("text");
            entity.Property(e => e.Id).HasColumnType("text");
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.NormalizedName)
                .HasMaxLength(256)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.Property(e => e.ClaimType).HasColumnType("text");
            entity.Property(e => e.ClaimValue).HasColumnType("text");
            entity.Property(e => e.RoleId).HasColumnType("text");
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.Property(e => e.ConcurrencyStamp).HasColumnType("text");
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Id).HasColumnType("text");
            entity.Property(e => e.LockoutEnd).HasColumnType("datetime");
            entity.Property(e => e.NormalizedEmail)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.NormalizedUserName)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash).HasColumnType("text");
            entity.Property(e => e.PhoneNumber).HasColumnType("text");
            entity.Property(e => e.SecurityStamp).HasColumnType("text");
            entity.Property(e => e.UserName)
                .HasMaxLength(256)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClaimType).HasColumnType("text");
            entity.Property(e => e.ClaimValue).HasColumnType("text");
            entity.Property(e => e.UserId).HasColumnType("text");
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
           entity.Property(e => e.LoginProvider)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.ProviderDisplayName).HasColumnType("text");
            entity.Property(e => e.ProviderKey)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnType("text");
        });

        modelBuilder.Entity<AspNetUserRole>(entity =>
        {
            entity.Property(e => e.RoleId).HasColumnType("text");
            entity.Property(e => e.UserId).HasColumnType("text");
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
           entity.Property(e => e.LoginProvider)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnType("text");
            entity.Property(e => e.Value).HasColumnType("text");
        });

        modelBuilder.Entity<Clinic>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Clinic");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasColumnType("text");
        });

        modelBuilder.Entity<ClinicStaff>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ClinicStaff");

            entity.Property(e => e.ClinicId).HasColumnName("ClinicID");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DeptHeadId).HasColumnName("DeptHeadID");
            entity.Property(e => e.Location).HasColumnType("text");
            entity.Property(e => e.Name).HasColumnType("text");
            entity.Property(e => e.WorkingHours).HasColumnType("text");
        });

        modelBuilder.Entity<DeptStaff>(entity =>
        {
            entity.ToTable("DeptStaff");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ConcurrencyStamp).HasColumnType("text");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.EmailConfirmed).HasColumnType("text");
            entity.Property(e => e.FirstName).HasColumnType("text");
            entity.Property(e => e.LastName).HasColumnType("text");
            entity.Property(e => e.NormalizedEmail).HasColumnType("text");
            entity.Property(e => e.NormalizedUserName).HasColumnType("text");
            entity.Property(e => e.PasswordHash).HasColumnType("varchar(max)");
            entity.Property(e => e.PhoneNumber).HasColumnType("text");
            entity.Property(e => e.Role).HasColumnType("text");
            entity.Property(e => e.SecurityStamp).HasColumnType("text");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.UserName).HasColumnType("varchar(max)");

            entity.HasOne(d => d.Doctor).WithMany(p => p.DeptStaffs)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_dept_doctor");
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.ToTable("Diagnosis");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Diagnosis1)
                .HasColumnType("text")
                .HasColumnName("diagnosis");
            entity.Property(e => e.DiagnosisDate).HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.PatientName).HasColumnType("text");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.ToTable("Doctor");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClinicId).HasColumnName("ClinicID");
            entity.Property(e => e.Location)
                .HasColumnType("text")
                .HasColumnName("location");
            entity.Property(e => e.Name).HasColumnType("text");
            entity.Property(e => e.PasswordHash).HasColumnType("text");
            entity.Property(e => e.Speciality).HasColumnType("text");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");
        });

        modelBuilder.Entity<DoctorAttendance>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DoctorAttendance");

            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
        });

        modelBuilder.Entity<Inpatient>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Inpatient");

            entity.Property(e => e.AdmissionReason).HasColumnType("text");
            entity.Property(e => e.DeptId).HasColumnName("DeptID");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Insurance>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Insurance");

            entity.Property(e => e.ClassType).HasColumnType("text");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.Provider).HasColumnType("text");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Invoice");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
        });

        modelBuilder.Entity<Medication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Medicati__3214EC0788BED373");

            entity.Property(e => e.Dosage).HasMaxLength(50);
            entity.Property(e => e.Duration).HasMaxLength(50);
            entity.Property(e => e.MedicineName).HasMaxLength(100);

            entity.HasOne(d => d.Prescription).WithMany(p => p.Medications)
                .HasForeignKey(d => d.PrescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Medicatio__Presc__06CD04F7");
        });

        modelBuilder.Entity<Outpatient>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Outpatient");

            entity.Property(e => e.CheckBackDate).HasColumnType("datetime");
            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasColumnType("text");
            entity.Property(e => e.InsuranceCompany).HasColumnType("text");
            entity.Property(e => e.Name).HasColumnType("text");
            entity.Property(e => e.OfficialId)
                .HasColumnType("text")
                .HasColumnName("OfficialID");
            entity.Property(e => e.PasswordHash).HasColumnType("text");
        });

        modelBuilder.Entity<PatientAppointment>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PatientAppointment");

            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.ClinicId).HasColumnName("ClinicID");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
        });

        modelBuilder.Entity<PatientRadiology>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PatientRadiology");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
        });

        modelBuilder.Entity<PatientVisit>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PatientVisit");

            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.VisitDate).HasColumnType("datetime");
            entity.Property(e => e.VisitId).HasColumnName("VisitID");
        });

        modelBuilder.Entity<Pharmacy>(entity =>
        {
            entity.HasKey(e => e.MedicineId);

            entity.ToTable("Pharmacy");

            entity.Property(e => e.MedicineId).HasColumnName("MedicineID");
            entity.Property(e => e.MedicineName).HasColumnType("text");
            entity.Property(e => e.Shelf).HasColumnName("shelf");
            entity.Property(e => e.Stock).HasColumnName("stock");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Prescrip__3214EC070206757A");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PatientName).HasMaxLength(100);
        });

        modelBuilder.Entity<Radiology>(entity =>
        {
            entity.ToTable("Radiology");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.ImageDate).HasColumnType("datetime");
            entity.Property(e => e.ImagePath).HasColumnType("text");
            entity.Property(e => e.ImageType).HasColumnType("text");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.PatientName).HasColumnType("text");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.TechnicianName).HasColumnType("text");
            entity.Property(e => e.TechnicianNotes).HasColumnType("text");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.ToTable("Test");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.NormalRange).HasColumnType("text");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.PatientName).HasColumnType("text");
            entity.Property(e => e.Results).HasColumnType("text");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.Technician).HasColumnType("text");
            entity.Property(e => e.TestDate).HasColumnType("datetime");
            entity.Property(e => e.TestType).HasColumnType("text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
