using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IUserInfoService
    {
        Task<UserInfoViewModel> GetUserInfo();
    }
}
