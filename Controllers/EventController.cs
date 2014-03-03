using K3F.Calendar.Models;
using Orchard.Data;
using System;
using System.Web.Mvc;
using System.Linq;
using System.Globalization;

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

        public ActionResult ListNextEvents(int page)
        {
            var ptBR = new CultureInfo("pt-BR");
            var info = ptBR.DateTimeFormat;
            var pearPage = 3;
            var nextPage = (page - 1) * pearPage;
            var totalEvento = _repositoryEvent.Count(x => x.StartsAt > DateTime.Today);
            var eventos = _repositoryEvent.Fetch(x => x.StartsAt > DateTime.Today).Skip(nextPage).Take(pearPage).Select(x => new
            {
                Name = x.Name,
                Description = x.Description,

                StartsAt = x.StartsAt.ToString("yyyy-MM-dd"),
                StartsAtDay = x.StartsAt.Day,
                StartsAtWeekDay = info.GetDayName(x.StartsAt.DayOfWeek),
                StartsAtMonth = info.GetMonthName(x.StartsAt.Month),

                EndsAtAt = x.EndsAt.ToString("yyyy-MM-dd"),
                EndsAtDay = x.EndsAt.Day,
                EndsAtWeekDay = info.GetDayName(x.EndsAt.DayOfWeek),
                EndsAtMonth = info.GetMonthName(x.EndsAt.Month)
            }).ToList();
            var data = new {
                PrevPage = nextPage > 0,
                NextPage = (nextPage + pearPage) <= totalEvento,
                Data = eventos
            };

            return new JsonResult() { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }

}