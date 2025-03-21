using Microsoft.EntityFrameworkCore;
using PROGETTO_U5_S2_L5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PROGETTO_U5_S2_L5.Data {
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>> {
        public AppDbContext() {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }

        public DbSet<Cliente> Clienti {
            get; set;
        }

        public DbSet<Camera> Camere {
            get; set;
        }

        public DbSet<Prenotazione> Prenotazioni {
            get; set;
        }

        public DbSet<ApplicationUser> ApplicationUsers {
            get; set;
        }

        public DbSet<ApplicationRole> ApplicationRoles {
            get; set;
        }

        public DbSet<ApplicationUserRole> ApplicationUserRoles {
            get; set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(ur => ur.User).WithMany(u => u.ApplicationUserRoles).HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(ur => ur.Role).WithMany(r => r.ApplicationUserRole).HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<ApplicationUserRole>().Property(p => p.Date).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<Prenotazione>().HasOne(p => p.Cliente).WithMany(c => c.Prenotazioni).HasForeignKey(p => p.ClienteId);

            modelBuilder.Entity<Prenotazione>().HasOne(p => p.Camera).WithMany(c => c.Prenotazioni).HasForeignKey(p => p.CameraId);
        }
    }
}
