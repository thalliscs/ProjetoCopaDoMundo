using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fp_web_aula_1_core.Data;
using fp_web_aula_1_core.Models;
using Microsoft.AspNetCore.Identity;

namespace fp_18_web_aula_1_core.Identity
{
    public class Manager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly CopaContext _dbContext;

        public Manager(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            CopaContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;    
        }

        public async Task<bool> CreateAsync(string email, string password, string role)
        {
            var user = new ApplicationUser { UserName = email, Email = email};
            var result = await _userManager.CreateAsync(user, password);

            if(result.Succeeded)
            {    
                await _userManager.AddToRoleAsync(user, role);
                return true;
            }

            return false;
        }
    }
}