using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Workshop.API.Entities;

namespace Workshop.API.Data
{
    public static class Seed
    {
        public static async Task SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if(await userManager.Users.AnyAsync())
                return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeed.json");
            var users = JsonSerializer.Deserialize<List<User>>(userData);

            if(userData == null)
                return;

            var roles = new List<Role>()
            {
                new Role(){ Name = "Admin" },
                new Role(){ Name = "User" }
            };

            foreach(var role in roles)
                await roleManager.CreateAsync(role);

            foreach(var user in users)
            {
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRolesAsync(user, roles.Select(r => r.Name));
            }
        }
    }
}