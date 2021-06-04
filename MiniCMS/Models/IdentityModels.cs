using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MiniCMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Users : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Users> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public bool isactive { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<Users>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Language> languages { get; set; }


        public DbSet<SiteData> Sitedata { get; set; }

        public DbSet<SiteSlider> SiteSlider { get; set; }

        public DbSet<SiteNews> SiteNews { get; set; }

        public DbSet<SiteServices> SiteServices { get; set; }

        public DbSet<SiteAboutUs> SiteAbout { get; set; }

        public DbSet<SiteVideos> SiteVideos { get; set; }

        public DbSet<SiteImages> SiteImages { get; set; }

        public DbSet<SitePartners> SitePartners { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        //public System.Data.Entity.DbSet<MiniCMS.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}