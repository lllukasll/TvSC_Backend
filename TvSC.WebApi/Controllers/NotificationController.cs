using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TvSC.Data.BindingModels.Notification;
using TvSC.Services.Interfaces;
using TvSC.WebApi.Helpers;

namespace TvSC.WebApi.Controllers
{
    [Route("Notification")]
    [CustomActionFilters]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetNotifications()
        {
            string user = User.Identity.Name;
            var result = await _notificationService.GetNotifications(user);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("{tvSeriesId}")]
        public async Task<IActionResult> AddNotification(
            [FromBody] AddNotificationBindingModel addNotificationBindingModel, int tvSeriesId)
        {
            string user = User.Identity.Name;
            var result = await _notificationService.AddNotification(addNotificationBindingModel, tvSeriesId, user);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{notificationId}")]
        public async Task<IActionResult> DeleteNotification(int notificationId)
        {
            var result = await _notificationService.DeleteNotification(notificationId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
