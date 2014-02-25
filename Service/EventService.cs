using Orchard;

namespace K3F.Calendar.Service
{


    public class EventService : IEventService
    {

        public void teste()
        {
            Orchard.ContentManagement.IContentManager content = null;


        }

        public System.Collections.Generic.IList<Models.EventPart> ListNextEvents(int pageSize, int page)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IList<Models.EventPart> ListEvents(int pageSize, int page)
        {
            throw new System.NotImplementedException();
        }

        public Models.EventPart Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Salvar(Models.EventPart evento)
        {
            throw new System.NotImplementedException();
        }
    }
}