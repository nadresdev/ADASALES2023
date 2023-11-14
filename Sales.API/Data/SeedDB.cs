using Microsoft.EntityFrameworkCore;
using Sales.API.Helpers;
using Sales.API.Services;
using Sales.Shared.Entities;
using Sales.Shared.Enums;

namespace Sales.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IApiService _apiService;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IApiService apiService, IUserHelper userHelper)
        {
            _context = context;
            _apiService = apiService;
            _userHelper = userHelper;

        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckCategoriesAsync();

            await CheckUserAsync("1018", "andy", "valencia", "nadresdev@yopmail.com", "3153816265", "Calle falsa123", UserType.Admin);


        }


        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Deportes" });
                _context.Categories.Add(new Category { Name = "Calzado" });
                _context.Categories.Add(new Category { Name = "Tecnología " });
                
                _context.Categories.Add(new Category { Name = "Motocicletas" });
                
                _context.Categories.Add(new Category { Name = "Video Juegos" });
                await _context.SaveChangesAsync();
            }
        }








        private async Task<User> CheckUserAsync(string document, 
            string firstName, string lastName, string email, 
            string phone, string address, UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user,"123@Admin");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }





    }
}