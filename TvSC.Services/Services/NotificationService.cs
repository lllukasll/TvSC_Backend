using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using TvSC.Data.BindingModels.Notification;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Data.Keys;
using TvSC.Repo.Interfaces;
using TvSC.Services.Interfaces;

namespace TvSC.Services.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IRepository<TvShow> _tvSeriesRepository;
        private readonly IMapper _mapper;

        public NotificationService(IRepository<Notification> notificationRepository, IRepository<TvShow> tvSeriesRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _tvSeriesRepository = tvSeriesRepository;
            _mapper = mapper;
        }

        public async Task<ResponsesDto<GetNotificationsDto>> GetNotifications(string userId)
        {
            var response = new ResponsesDto<GetNotificationsDto>();

            if (userId == null)
            {
                response.AddError(Model.Account, Error.account_Login);
                return response;
            }

            var notifications = _notificationRepository.GetAllBy(x => x.UserId == userId, x => x.TvShow).OrderByDescending(x => x.CreateDateTime);
            var notificationsMapped = new List<GetNotificationsDto>();

            if (notifications.Any())
            {
                foreach (var notification in notifications)
                {
                    notificationsMapped.Add(new GetNotificationsDto
                    {
                        Type = notification.Type,
                        CreateDateTime = notification.CreateDateTime,
                        Id = notification.Id,
                        FirstPart = notification.FirstPart,
                        SecondPart = notification.SecondPart,
                        TvShowName = notification.TvShow.Name,
                        TvShowId = notification.TvShowId
                    });
                }

                response.DtoObject = notificationsMapped;
            }

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> AddNotification(
            AddNotificationBindingModel addNotificationBindingModel, int tvSeriesId, string userId)
        {
            var response = new ResponseDto<BaseModelDto>();

            var tvSeriesExists = await _tvSeriesRepository.ExistAsync(x => x.Id == tvSeriesId);
            if (!tvSeriesExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
                return response;
            }

            if (userId == null)
            {
                response.AddError(Model.Account, Error.account_Login);
                return response;
            }

            Notification notification = new Notification();
            notification.CreateDateTime = DateTime.Now;
            notification.TvShowId = tvSeriesId;
            notification.UserId = userId;
            notification.Type = addNotificationBindingModel.Type;

            if (addNotificationBindingModel.Type == "watchedEpisode")
            {
                notification.FirstPart = "Dodano odcinek " + addNotificationBindingModel.EpisodeNumber + " serialu ";
                notification.SecondPart = " do obejrzanych";
            } else if (addNotificationBindingModel.Type == "ratedTvSeries")
            {
                notification.FirstPart = "Oceniono serial ";
                notification.SecondPart = "";
            } else if (addNotificationBindingModel.Type == "favouriteTvSeries")
            {
                notification.FirstPart = "Dodano serial ";
                notification.SecondPart = " do ulubionych";
            } else if (addNotificationBindingModel.Type == "commentedTvSeries")
            {
                notification.FirstPart = "Dodano komentarz do serialu ";
                notification.SecondPart = "";
            }

            var result = await _notificationRepository.AddAsync(notification);
            if (!result)
            {
                response.AddError(Model.Notification, Error.notification_Adding);
                return response;
            }

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> DeleteNotification(int notificationId)
        {
            var response = new ResponseDto<BaseModelDto>();

            var notificationExists = await _notificationRepository.ExistAsync(x => x.Id == notificationId);
            if (!notificationExists)
            {
                response.AddError(Model.Notification, Error.notification_NotFound);
                return response;
            }

            var notification = await _notificationRepository.GetByAsync(x => x.Id == notificationId);

            if (notification != null)
            {
                var result = await _notificationRepository.Remove(notification);
                if (!result)
                {
                    response.AddError(Model.Notification, Error.notification_Deleting);
                    return response;
                }
            }

            return response;
        }
    }
}
