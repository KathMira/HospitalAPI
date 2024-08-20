using HospitalAPI.Modelos;
using Microsoft.AspNetCore.Identity;

namespace HospitalAPI.Banco;

public static class SeedManager
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        await SeedRoles(serviceProvider);
        await SeedAdmin(serviceProvider);
    }
    public static async Task SeedRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Administrador));
        await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Medico));
        await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Paciente));
    }
    public static async Task SeedAdmin(IServiceProvider serviceProvider)
    {

    }
}
