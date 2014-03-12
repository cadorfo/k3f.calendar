using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Mvc.Routes;


namespace K3F.Calendar
{
    public class Routes : IRouteProvider
    {
        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[]
            {
                
                new RouteDescriptor
                {
                    Priority = 5,
                    
                    Route = new Route("servicos-aos-investidores/agenda-eventos-1",

                        new RouteValueDictionary
                        {
                            {"area", "K3F.Calendar"},
                            {"controller", "Event"},
                            {"action", "ShowCalendar"}
                        }, new RouteValueDictionary(),
                        new RouteValueDictionary {{"area", "K3F.Calendar"}},
                        new MvcRouteHandler())
                }
            };
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (RouteDescriptor route in GetRoutes())
            {
                routes.Add(route);
            }
        }
    }
}