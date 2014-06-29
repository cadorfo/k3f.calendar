using K3F.Calendar.Models;
using Orchard.Data;
using System;
using System.Web.Mvc;
using System.Linq;
using System.Globalization;
using Orchard.Themes;
using System.Collections.Generic;

namespace K3F.Calendar.Controllers
{
    //[Admin] or [Themed] for a default layout. Can be placed on the controller or methods
    public class EventController : Controller
    {
        private readonly IRepository<EventRecord> _repositoryEvent;

        public EventController(IRepository<EventRecord> repositoryEvent)
        {
            this._repositoryEvent = repositoryEvent;
        }

        public ActionResult ListNextEvents(int page, int perPage = 3)
        {
            var ptBR = new CultureInfo("pt-BR");
            var info = ptBR.DateTimeFormat;

            var nextPage = (page - 1) * perPage;
            var totalEvento = _repositoryEvent.Count(x => x.StartsAt > DateTime.Today);
            var eventos = _repositoryEvent.Fetch(x => x.StartsAt > DateTime.Today).OrderBy(x => x.StartsAt).Skip(nextPage).Take(perPage).Select(x => new
            {
                Name = x.Name,
                Description = x.Description,

                StartsAt = x.StartsAt.ToString("yyyy-MM-dd"),
                StartsAtDay = x.StartsAt.Day,
                StartsAtWeekDay = info.GetDayName(x.StartsAt.DayOfWeek),
                StartsAtMonth = info.GetMonthName(x.StartsAt.Month),

                StartsAtInglishFormat = x.StartsAt.ToString("MM/dd/yyyy"),

                EndsAtAt = x.EndsAt.ToString("yyyy-MM-dd"),
                EndsAtDay = x.EndsAt.Day,
                EndsAtWeekDay = info.GetDayName(x.EndsAt.DayOfWeek),
                EndsAtMonth = info.GetMonthName(x.EndsAt.Month)
            }).ToList();

            var data = new
            {
                PrevPage = nextPage > 0,
                NextPage = (nextPage + perPage) <= totalEvento,
                Data = eventos
            };

            return new JsonResult() { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Themed]
        public ActionResult ShowCalendar()
        {


            int page = 1;
            int perPage = 10;
            var ptBR = new CultureInfo("pt-BR");
            var info = ptBR.DateTimeFormat;

            var nextPage = (page - 1) * perPage;
            var totalEvento = _repositoryEvent.Count(x => x.StartsAt > DateTime.Today);
            var eventos = _repositoryEvent.Fetch(x => x.StartsAt > DateTime.Today).OrderBy(x => x.StartsAt).Skip(nextPage).Take(perPage).ToList();

            return View(eventos);
        }

        public ActionResult GetDayEvents(String dateRequested = null)
        {
            var ptBR = new CultureInfo("pt-BR");
            var info = ptBR.DateTimeFormat;



            IEnumerable<EventRecord> eventos = null;

            if (!String.IsNullOrEmpty(dateRequested))
            {
                DateTime filterDate = DateTime.ParseExact(dateRequested, "dd/MM/yyyy", info);
                eventos = _repositoryEvent.Fetch(x => x.StartsAt >= filterDate && x.StartsAt <= filterDate.AddDays(1).AddMilliseconds(-1));
            }
            else
            {
                eventos = _repositoryEvent.Fetch(x => x.CreatedAt != null);
            }


            var events = eventos.Select(x => new
            {
                Name = x.Name,
                Description = x.Description,

                StartsAt = x.StartsAt.ToString("yyyy-MM-dd"),
                StartsAtDay = x.StartsAt.Day,
                StartsAtWeekDay = info.GetDayName(x.StartsAt.DayOfWeek),
                StartsAtMonth = info.GetMonthName(x.StartsAt.Month),
                StartsAtYear = x.StartsAt.Year,

                EndsAtAt = x.EndsAt.ToString("yyyy-MM-dd"),
                EndsAtDay = x.EndsAt.Day,
                EndsAtWeekDay = info.GetDayName(x.EndsAt.DayOfWeek),
                EndsAtMonth = info.GetMonthName(x.EndsAt.Month)
            }).ToList();

            return new JsonResult() { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = events };
        }

    }

}
