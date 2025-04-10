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
            var userId = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated ?
                        _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value
                        : Guid.Empty.ToString();

            var userIdGuid = Guid.Empty;
            if (!Guid.TryParse(userId, out userIdGuid))
            {
                return null;
            }

            return await _userManager.FindByIdAsync(userIdGuid.ToString());
        }

        public async Task<IList<string>> GetUserRole(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

    }
}
