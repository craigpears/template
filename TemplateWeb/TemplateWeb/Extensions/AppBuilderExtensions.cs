using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Template.Data;

namespace TemplateWeb.Extensions;

public static class AppBuilderExtensions
{
    public static async Task MigrateDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
    }
    
    public static async Task SeedRolesAndAdminUser(this IApplicationBuilder app, ConfigurationManager configurationManager)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roleNames = { "Admin", "User" };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                // Create the roles and seed them
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Create a default admin user
        var adminEmail = configurationManager["AdminEmail"];
        var adminPassword = configurationManager["AdminPassword"];

        if (string.IsNullOrEmpty(adminEmail))
            return;

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail
            };

            await userManager.CreateAsync(adminUser, adminPassword);
        }

        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    
        adminUser.EmailConfirmed = true;
        await userManager.UpdateAsync(adminUser);
    }
}