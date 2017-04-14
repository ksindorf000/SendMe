using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SendMe.Models;

[assembly: OwinStartupAttribute(typeof(SendMe.Startup))]
namespace SendMe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }


        // In this method we will create default User roles and Admin user for login   
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

           if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
               role.Name = "Admin";
               roleManager.Create(role);
           }            
        }
    }
}
