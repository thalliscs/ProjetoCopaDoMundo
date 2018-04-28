using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace fp_18_web_aula_1_core.Identity

{
    public class Authentication
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        public Authentication(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;

        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
            return result.Succeeded;   
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}