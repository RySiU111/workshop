using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Workshop.API.Entities;

namespace Workshop.API.Data
{
    public static class Seed
    {
        public static async Task SeedUsers(UserManager<User> userManager)
        {
            if(await userManager.Users.AnyAsync())
                return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeed.json");
            var users = JsonSerializer.Deserialize<List<User>>(userData);

            if(userData == null)
                return;

            foreach(var user in users)
                await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }
}