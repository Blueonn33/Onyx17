using Microsoft.AspNetCore.Identity;
using Onyx17.Areas.Identity.Data;
using Onyx17.Constants;

namespace Onyx17.Data
{
    public class UserSeeder
    {
        public static async Task SeedUsersAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var adminEmail = configuration["AdminUser:Email"];
            var adminPassword = configuration["AdminUser:Password"];
            var adminName = "Администратор";
            var adminDescription = "Администратор на уеб приложение 'Оникс'";
            var adminProfileImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Logo.png");

            await CreateUserWithRole(userManager, adminEmail, adminPassword, adminName, adminDescription,
                adminProfileImage, Roles.Admin);
        }

        public static async Task CreateUserWithRole(UserManager<User> userManager, string email,
            string password, string name, string description, string profileImagePath,
            string role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new User
                {
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email,
                    Name = name,
                    Description = description,
                };

                if (!string.IsNullOrEmpty(profileImagePath) && File.Exists(profileImagePath))
                {
                    user.ImageData = await File.ReadAllBytesAsync(profileImagePath);
                    user.ImageMimeType = "image/png";
                }

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    throw new Exception($"Проблем при създаването на потребител с email {user.Email}. Грешки: {string.Join(",", result.Errors)}");
                }
            }
        }
    }
}
