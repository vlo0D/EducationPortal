using EducationPortal.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DAL.Initializer
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            if (userManager == null || roleManager == null)
            {
                throw new ArgumentNullException();
            }

            //need use appsettings
            string emailAdmin = "Admin_1@gmail.com";
            string passwordAdmin = "Admin_1";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int> { Name = "admin" });
            }

            if (await roleManager.FindByNameAsync("student") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int> { Name = "student" });
            }

            if (await roleManager.FindByNameAsync("moder") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int> { Name = "moder" });
            }

            if (await userManager.FindByNameAsync(emailAdmin) == null)
            {
                User admin = new User { Email = emailAdmin, UserName = emailAdmin, FirstName = emailAdmin, LastName = emailAdmin };
                IdentityResult adminResult = await userManager.CreateAsync(admin, passwordAdmin);

                if (adminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
