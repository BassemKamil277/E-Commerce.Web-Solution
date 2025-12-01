using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.IdentityData.DataSeed
{
    public class IdentityDataIntialiaze : IDataIntializer
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<IdentityDataIntialiaze> logger;

        public IdentityDataIntialiaze(UserManager<ApplicationUser> userManager , 
            RoleManager<IdentityRole> roleManager , ILogger<IdentityDataIntialiaze> logger)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }

        public async Task IntializeAsync()
        {
            try
            {
                if(!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if(!userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        DisplayName = "Mohamed Tarek",
                        UserName = "MohamedTarek",
                        Email = "MohamedTarek@gmail.com",
                        PhoneNumber = "01236652652"
                    };
                    var User02 = new ApplicationUser()
                    {
                        DisplayName = "Salma Tarek",
                        UserName = "SalmaTarek",
                        Email = "SalmaTarek@gmail.com",
                        PhoneNumber = "01236652652"
                    };

                    await userManager.CreateAsync(User01 , "P@ssw0rd");
                    await userManager.CreateAsync(User02, "P@ssw0rd");

                    await userManager.AddToRoleAsync(User01, "Admin");
                    await userManager.AddToRoleAsync(User02, "SuperAdmin");
                }
            }
            catch(Exception ex) 
            {
                logger.LogError($"Error while seeding Identity database : Message {ex.Message}");
            }
        }
    }
}
