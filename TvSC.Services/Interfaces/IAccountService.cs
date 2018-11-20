using System.Threading.Tasks;
using TvSC.Data.BindingModels;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Account;

namespace TvSC.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseDto<BaseModelDto>> Register(RegisterBindingModel model);
        Task<ResponseDto<GetLoggedUserDto>> Login(LoginBindingModel model);
        Task LogOut();
        Task<ResponseDto<BaseModelDto>> ChangePassword(string userId, ChangePasswordBindingModel model);
        Task<ResponseDto<GetLoggedUserDto>> GetUserByCookie(string userId);
    }
}