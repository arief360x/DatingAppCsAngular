using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(ApplicationDbContext db)
        {   
        
            if (await db.Users.AnyAsync()) return;

            var userSeed = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");


            var users = JsonSerializer.Deserialize<List<AppUser>>(userSeed);
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("seedpassword"));
                user.PasswordSalt = hmac.Key;

                db.Users.Add(user);
            }
            await db.SaveChangesAsync();
        }
    }
}