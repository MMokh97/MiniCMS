namespace MiniCMS.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MiniCMS.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MiniCMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MiniCMS.Models.ApplicationDbContext";
        }

        protected override void Seed(MiniCMS.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            setLanguage(context);

            User_Roles(context);

            DefaultUser(context);

        }

        private static void DefaultUser(ApplicationDbContext context)
        {
            var DefaultUser = context.Users.Where(u => u.Email == "mostafa_mahmoud@MiniCMS").FirstOrDefault();
            if (DefaultUser == null)
            {
                Users App_user = new Users();
                App_user.Email = "mostafa_mahmoud@MiniCMS";
                App_user.UserName = "mostafa_mahmoud@MiniCMS";
                App_user.PhoneNumber = "01009433965";
                UserStore<Users> userStore = new UserStore<Users>(context);
                UserManager<Users> userManager = new UserManager<Users>(userStore);
                var result = userManager.Create(App_user, "P@ssw0rd");
                if (result.Succeeded)
                {
                    userManager.AddToRole(App_user.Id, "SuperAdmin");
                }

            }
        }

        private static void User_Roles(ApplicationDbContext context)
        {
            var UserRole_Super = context.Roles.Where(R => R.Name == "SuperAdmin").FirstOrDefault();
            if (UserRole_Super == null)
            {
                IdentityRole U_role = new IdentityRole();
                U_role.Name = "SuperAdmin";
                context.Roles.Add(U_role);
                context.SaveChanges();
            }

            var UserRole_Admin = context.Roles.Where(R => R.Name == "Admin").FirstOrDefault();
            if (UserRole_Admin == null)
            {
                IdentityRole U_role = new IdentityRole();
                U_role.Name = "Admin";
                context.Roles.Add(U_role);
                context.SaveChanges();
            }
        }

        private static void setLanguage(ApplicationDbContext context)
        {
            var ArabicLang = context.languages.Where(N => N.LanguageName == "Arabic").FirstOrDefault();
            if (ArabicLang == null)
            {
                Language lang = new Language();
                lang.LanguageName = "Arabic";
                context.languages.Add(lang);
                context.SaveChanges();
            }


            var EnglishLang = context.languages.Where(N => N.LanguageName == "English").FirstOrDefault();
            if (EnglishLang == null)
            {
                Language lang = new Language();
                lang.LanguageName = "English";
                context.languages.Add(lang);
                context.SaveChanges();
            }
        }
    }
}
