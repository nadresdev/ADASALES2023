using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Sales.API.Data;
using Sales.Shared.DTOs;
using Sales.Shared.Entities;

namespace Sales.API.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User>_singInManager;

        public UserHelper(DataContext dataContext, UserManager<User> userManager, 
            RoleManager<IdentityRole>roleManager, SignInManager<User> singInManager)  {
            this._context = dataContext;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._singInManager = singInManager;
        }

       

        public  async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole
                { Name= roleName });

            }
        }

        public async Task<User> GetUserAsync(string email)
        {
            var user =await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user!;

        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginDTO model)
        {
            return await _singInManager.PasswordSignInAsync(model.Email, model.Password, false, false); //manejo de intentos de loggin
        }

        public async Task LogoutAsync()
        {
             await _singInManager.SignOutAsync();
        }
    }
}
