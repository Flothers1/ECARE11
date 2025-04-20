using ECARE.Interface;
using ECARE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECARE.Services
{
    public class UsersService : IUsersService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ECAREContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersService(IHttpContextAccessor httpContextAccessor, ECAREContext context, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserAfterLogin()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var principal = httpContext?.User;
            if (principal?.Identity?.IsAuthenticated != true)
            {
                return null;
            }

            var user = await _userManager.GetUserAsync(principal);

            return user;
        }

        public async Task<IList<string>> GetUserRole(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

    }
}
