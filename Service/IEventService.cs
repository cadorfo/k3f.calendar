using K3F.Calendar.Models;
using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K3F.Calendar.Service
{
    
    public interface IEventService : IDependency
    {

        #region Events
        /// <summary>
        /// Retrieve events ordered by date, first event is the imediately after from <value>DateTime.Now</value>, it's possible retrieve passed events with negative page parameter value.
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        IList<EventPart> ListNextEvents(int pageSize, int page);
        /// <summary>
        /// Retrive events ordered by name
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        IList<EventPart> ListEvents(int pageSize, int page);

        EventPart Get(int id);

        void Salvar(EventPart evento);
        #endregion

        

    }
}
