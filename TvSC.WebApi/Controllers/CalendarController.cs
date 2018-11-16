using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TvSC.Data.BindingModels.Calendar;
using TvSC.Services.Interfaces;

namespace TvSC.WebApi.Controllers
{
    [Route("Calendar")]
    public class CalendarController : Controller
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpGet("month/{monthNumber}")]
        public async Task<IActionResult> GetMonthEpisodes(int monthNumber)
        {
            var result = await _calendarService.GetMonthEpisodes(monthNumber);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("week")]
        public async Task<IActionResult> GetWeekEpisodes([FromBody] GetWeekEpisodesBindingModel getWeekEpisodesBindingModel)
        {
            var result = await _calendarService.GetWeekEpisodes(getWeekEpisodesBindingModel);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
