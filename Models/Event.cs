using K3F.PostalAddress.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System;
using System.Web.Mvc;


namespace K3F.Calendar.Models
{
    public class EventRecord : ContentPartRecord
    {
        public EventRecord()
        {
            StartsAt = DateTime.Today;
            EndsAt = DateTime.Today;
            CreatedAt = DateTime.Today;
            Name = "";
        }

        public virtual String Name { get; set; }
        public virtual String Description { get; set; }
        public virtual DateTime StartsAt { get; set; }
        public virtual DateTime EndsAt { get; set; }
        public virtual DateTime CreatedAt { get; set; }
    }

    public class EventPart : ContentPart<EventRecord>
    {

        public virtual String Name
        {
            get { return Record.Name; }
            set { Record.Name = value; }
        }

        public virtual String Description
        {
            get { return Record.Description; }
            set { Record.Description = value; }
        }

        public virtual DateTime StartsAt
        {
            get { return Record.StartsAt; }
            set { Record.StartsAt = value; }
        }

        public virtual DateTime EndsAt
        {
            get { return Record.EndsAt; }
            set { Record.EndsAt = value; }
        }

        public virtual DateTime CreatedAt
        {
            get { return Record.CreatedAt; }
            set { Record.CreatedAt = value; }
        }

    }
}