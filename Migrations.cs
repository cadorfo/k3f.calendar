using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace K3F.Calendar
{
    public class Migrations : DataMigrationImpl
    {

        public int Create()
        {
            SchemaBuilder.CreateTable("EventRecord", table => table.ContentPartRecord()
                .Column<String>("Name", x => x.NotNull())
                .Column<String>("Description")
                .Column<DateTime>("StartsAt")
                .Column<DateTime>("EndsAt")
                .Column<DateTime>("CreatedAt")
                );

            ContentDefinitionManager.AlterTypeDefinition("Event",
                type => type.Creatable()
                    .WithPart("EventPart")
                    .WithPart("CommonPart")
                    .WithPart("TitlePart")
                    .WithPart("AutoroutePart")
                .WithPart("PostallAddressPart"))
                ;

            return 1;
        }

        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterPartDefinition("CalendarWidgetPart", part => part.WithDescription("Calendar part"));
            ContentDefinitionManager.AlterTypeDefinition("CalendarWidget",
                cfg => cfg.WithPart("CalendarWidgetPart")
                    .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));
            return 2;
        }
        public int UpdateFrom2()
        {
            ContentDefinitionManager.AlterTypeDefinition("CalendarWidget", type => type.RemovePart("CalendarWidgetPart"));
            return 3;
        }
    }
}