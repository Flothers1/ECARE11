using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

    }
}
