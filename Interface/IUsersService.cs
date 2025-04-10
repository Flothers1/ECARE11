using ECARE.Models;

namespace ECARE.Interface
{
    public interface IUsersService
    {
        Task<ApplicationUser> GetUserAfterLogin();
        Task<IList<string>> GetUserRole(ApplicationUser user);
    }
}
