using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ECARE.Models
{
    public class ECAREContext : IdentityDbContext<ApplicationUser>
    {
        public ECAREContext(DbContextOptions<ECAREContext> options)
       : base(options)
        {
        }

        public DbSet<PatientRegistrations> PatientRegistrations { get; set; }
        public DbSet<Service_Request> ServiceRequests { get; set; }
        public DbSet<Lab> Lab { get; set; }
        public DbSet<LabBranch> LabBranch { get; set; }
        public DbSet<PharmacyServiceRequest> PharmacyServiceRequests { get; set; }
        public DbSet<CareProgram> CarePrograms { get; set; }
        public DbSet<PharmacyBranch> pharmacyBranches { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Distributor> Distributors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "4",
                    Name = "PharmacyAdmin",
                    NormalizedName = "PHARMACYADMIN"
                }
                );
                
            builder.Entity<CareProgram>()
                .HasMany(psr => psr.Pharmacies)
                .WithMany()
                .UsingEntity(j => j.ToTable("ProgramPharmacies"));

            builder.Entity<CareProgram>()
                .HasMany(psr => psr.Distributors)
                .WithMany()
                .UsingEntity(j => j.ToTable("ProgramDistributors"));

            // Configure PatientRegistrations -> Program
            builder.Entity<PatientRegistrations>()
                .HasOne(pr => pr.CareProgram)
                .WithMany(p => p.PatientRegistrations)
                .HasForeignKey(pr => pr.CareProgramId)
                .OnDelete(DeleteBehavior.Cascade);
            // Seed Pharmacies and Branches
            var pharmacies = new List<Pharmacy>
            {
                  new Pharmacy { Id = 1, Name = "Amr tarrad pharmacy" },
                  new Pharmacy { Id = 2, Name = "al-abdellatif altarshoby pharmacy" },
                  new Pharmacy { Id = 3, Name = "sally pharmacy" },
                  new Pharmacy { Id = 4, Name = "nadia abdelsallam pharmacy" },
                  new Pharmacy { Id = 5, Name = "mohamed mostafa kamel pharmacy" },
                  new Pharmacy { Id = 6, Name = "ahmed ramadan pharmacy" },
                  new Pharmacy { Id = 7, Name = "mohamed mahrous pharmacy" }
            };
            builder.Entity<PharmacyServiceRequest>()
                .HasOne(psr => psr.CareProgram)
                .WithMany(p => p.PharmaciesServiceRequests)
                .HasForeignKey(psr => psr.CareProgramId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<PharmacyServiceRequest>()
                .HasOne(psr => psr.PatientRegistrations)
                .WithMany(pr => pr.PharmacyServiceRequests)
                .HasForeignKey(psr => psr.PRId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            var branches = new List<PharmacyBranch>
            {
                  // Amr tarrad pharmacy branches
                  new() { Id = 1, Name = "maadi", PharmacyId = 1 },
                  new() { Id = 2, Name = "mohandseen", PharmacyId = 1 },
                  
                  // al-abdellatif altarshoby branches
                  new() { Id = 3, Name = "Masr algadida", PharmacyId = 2 },
                  new() { Id = 4, Name = "Zayed", PharmacyId = 2 },
                  new() { Id = 5, Name = "Rehab", PharmacyId = 2 },
                  new() { Id = 6, Name = "Nasr city", PharmacyId = 2 },
                  new() { Id = 7, Name = "Madinty", PharmacyId = 2 },
                  new() { Id = 8, Name = "Portsaid", PharmacyId = 2 },
                  new() { Id = 9, Name = "Seuz", PharmacyId = 2 },
                  new() { Id = 10, Name = "islamilia", PharmacyId = 2 },
                  new() { Id = 11, Name = "zagazig", PharmacyId = 2 },
                  new() { Id = 12, Name = "manzla", PharmacyId = 2 },
                  new() { Id = 13, Name = "Damietta", PharmacyId = 2 },
                  new() { Id = 14, Name = "mansoura", PharmacyId = 2 },
                      // sally pharmacy branches
                  new() { Id = 15, Name = "Masr algadida", PharmacyId = 3 },
                  new() { Id = 16, Name = "Fifth settelment", PharmacyId = 3 },
                  new() { Id = 17, Name = "mansoura", PharmacyId = 3 },
                  
                  // Alexandria branches
                  new() { Id = 18, Name = "Alex", PharmacyId = 4 },
                  new() { Id = 19, Name = "Alex", PharmacyId = 5 },
                  new() { Id = 20, Name = "Alex", PharmacyId = 6 },
                  new() { Id = 21, Name = "Alex", PharmacyId = 7 }
            };

            var distributors = new List<Distributor>
            {
                  new() { Id = 1, Name = "Sofico pharma" },
                  new() { Id = 2, Name = "pharmaoverseas" },
                  new() { Id = 3, Name = "Ibn sina pharma" }
            };
            builder.Entity<Pharmacy>().HasData(pharmacies);
            builder.Entity<PharmacyBranch>().HasData(branches);
            builder.Entity<Distributor>().HasData(distributors);
        }

    }

}
