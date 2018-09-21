using System.Threading.Tasks;
using TvSC.Data.BindingModels;
using TvSC.Data.DtoModels;

namespace TvSC.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseDto<BaseModelDto>> Register(RegisterBindingModel model);
        Task<ResponseDto<LoginDto>> Login(LoginBindingModel model);
        Task LogOut();
        Task<ResponseDto<BaseModelDto>> ChangePassword(string userId, ChangePasswordBindingModel model);
    }
}