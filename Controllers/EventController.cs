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

        public ActionResult ListNextEvents()
        {
            var ptBR = new CultureInfo("pt-BR");
            var info = ptBR.DateTimeFormat;
            var eventos = _repositoryEvent.Fetch(x => x.StartsAt > DateTime.Today).Take(4).Select(x => new
            {
                Day = x.StartsAt.Day,
                WeekDay = info.GetDayName(x.StartsAt.DayOfWeek),
                Month = info.GetMonthName(x.StartsAt.Month),
                Name = x.Name
            }).ToList();
            return new JsonResult() { Data = eventos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}