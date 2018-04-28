using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace fp_18_web_aula_1_core.Data
{
    public static class ApplicationDbInitializer
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                IdentityResult roleResult = roleManager.CreateAsync(new IdentityRole("admin")).Result;
            }
        }
    }
}
