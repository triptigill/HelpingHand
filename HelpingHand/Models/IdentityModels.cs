using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using HelpingHand.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HelpingHand.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDBContext, Configuration>());
        }
    
        public DbSet<Bill> Bill { get; set; }
        public DbSet<BillDetails> BillDetail { get; set; }
        public DbSet<ClientDetail> ClientDetail { get; set; }
        public DbSet<CompanyDetail> CompanyDetail { get; set; }
        public DbSet<ProductDetail> ProductDetail { get; set; }

        public static ApplicationDBContext Create()
        {
            return new ApplicationDBContext();
        }
    }
}