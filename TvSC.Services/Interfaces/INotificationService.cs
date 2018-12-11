using System.Threading.Tasks;
using TvSC.Data.BindingModels.Notification;
using TvSC.Data.DtoModels;

namespace TvSC.Services.Interfaces
{
    public interface INotificationService
    {
        Task<ResponseDto<BaseModelDto>> AddNotification(AddNotificationBindingModel addNotificationBindingModel,
            int tvSeriesId, string userId);

        Task<ResponseDto<BaseModelDto>> DeleteNotification(int notificationId);
        Task<ResponsesDto<GetNotificationsDto>> GetNotifications(string userId);
    }
}