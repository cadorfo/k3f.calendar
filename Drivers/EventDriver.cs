using K3F.Calendar.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using System;

namespace K3F.Calendar.Drivers
{
    public class EventDriver : ContentPartDriver<EventPart>
    {
        protected override string Prefix
        {
            get { return "EventPart"; }
        }

        protected override DriverResult Display(EventPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_EventPart",
                () => shapeHelper.Parts_EventPart(
                    Model: part));
        }

        protected override DriverResult Editor(EventPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_EventPart_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/EventPart",
                    Model: part,
                    Prefix: Prefix));
        }

        protected override DriverResult Editor(EventPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            if (part.CreatedAt != null || part.CreatedAt == DateTime.MinValue)
            {
                part.CreatedAt = DateTime.Now;
            }
            return Editor(part, shapeHelper);
        }

    }
}