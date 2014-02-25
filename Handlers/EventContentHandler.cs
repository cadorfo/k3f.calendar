using K3F.Calendar.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;


namespace K3F.Calendar.Handlers
{
    public class EventContentHandler : ContentHandler
    {
        public EventContentHandler(IRepository<EventRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}