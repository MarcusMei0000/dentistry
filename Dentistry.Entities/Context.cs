using Microsoft.EntityFrameworkCore;
using Dentistry.Entities.Models;

public class Context : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Speciality> Specialities { get; set; }
    public DbSet<DoctorSpeciality> DoctorSpecialities { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Reception> Receptions { get; set; }

    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        #region Users

        builder.Entity<User>().ToTable("users");
        builder.Entity<User>().HasKey(x => x.Id);

        #endregion

        #region Doctors

        builder.Entity<Doctor>().ToTable("doctors");
        builder.Entity<Doctor>().HasKey(x => x.Id);

        #endregion

        #region Patient

        builder.Entity<Patient>().ToTable("patients");
        builder.Entity<Patient>().HasKey(x => x.Id);

        #endregion

        #region Specialities

        builder.Entity<Speciality>().ToTable("specialities");
        builder.Entity<Speciality>().HasKey(x => x.Id);

        #endregion

        #region DoctorSpeciality

        builder.Entity<DoctorSpeciality>().ToTable("doctors_specialities");
        builder.Entity<DoctorSpeciality>().HasKey(x => x.Id);
        builder.Entity<DoctorSpeciality>().HasOne(x => x.Doctor)
                                    .WithMany(x => x.Specialities)
                                    .HasForeignKey(x => x.DoctorId)
                                    .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<DoctorSpeciality>().HasOne(x => x.Speciality)
                                    .WithMany(x => x.Doctors)
                                    .HasForeignKey(x => x.SpecialityId)
                                    .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region Schedules

        builder.Entity<Schedule>().ToTable("schedules");
        builder.Entity<Schedule>().HasKey(x => x.Id);
        builder.Entity<Schedule>().HasOne(x => x.Doctor)
                                    .WithMany(x => x.Schedules)
                                    .HasForeignKey(x => x.DoctorId)
                                    .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region Receptions

        builder.Entity<Reception>().ToTable("receptions");
        builder.Entity<Reception>().HasKey(x => x.Id);
        builder.Entity<Reception>().HasOne(x => x.Patient)
                                    .WithMany(x => x.Receptions)
                                    .HasForeignKey(x => x.PatientId)
                                    .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Reception>().HasOne(x => x.Schedule)
                                    .WithMany(x => x.Receptions)
                                    .HasForeignKey(x => x.ScheduleId)
                                    .OnDelete(DeleteBehavior.Cascade);                        

        #endregion
    }
}


