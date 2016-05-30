using chrismgrn.sdl.tridion.coreservice.extensionmethods;
using chrismrgn.sdl.tridion.core.Logging;
using chrismrgn.sdl.tridion.coreservice.Helpers;
using System;
using Tridion.ContentManager.CoreService.Client;

namespace Schneider.SchemaUsageExport
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.For(typeof(PublicationHelpers)).Info("Starting...");
            ProcessSchemasAndUsage();

            Logger.For(typeof(PublicationHelpers)).Info("Finished...");
            Console.ReadKey();
        }

        private static void ProcessSchemasAndUsage()
        {
            Logger.For(typeof(Program)).Info("Multimedia Schemas");
            Logger.For(typeof(Program)).Info("====================");
            var schemas = ItemHelpers.LoadAllByPublication<SchemaData>(schemaPurposesToInclude: new SchemaPurpose[] { SchemaPurpose.Multimedia });
            foreach (var schema in schemas)
            {
                Logger.For(typeof(Program)).InfoFormat("Schema {0} used {1} times", schema.Title, schema.GetAllUsageCount<ComponentData>());
            }

            Logger.For(typeof(Program)).Info("Content Schemas");
            Logger.For(typeof(Program)).Info("====================");
            schemas = ItemHelpers.LoadAllByPublication<SchemaData>(schemaPurposesToInclude: new SchemaPurpose[] { SchemaPurpose.Component });
            foreach (var schema in schemas)
            {
                Logger.For(typeof(Program)).InfoFormat("Schema {0} used {1} times", schema.Title, schema.GetAllUsageCount<ComponentData>());
            }

            Logger.For(typeof(Program)).Info("Embedded Schemas");
            Logger.For(typeof(Program)).Info("====================");
            schemas = ItemHelpers.LoadAllByPublication<SchemaData>(schemaPurposesToInclude: new SchemaPurpose[] { SchemaPurpose.Embedded });
            foreach (var schema in schemas)
            {
                Logger.For(typeof(Program)).InfoFormat("Schema {0} used {1} times", schema.Title, schema.GetAllUsageCount<ComponentData>());
            }

            Logger.For(typeof(Program)).Info("Metadata Schemas");
            Logger.For(typeof(Program)).Info("====================");
            schemas = ItemHelpers.LoadAllByPublication<SchemaData>(schemaPurposesToInclude: new SchemaPurpose[] { SchemaPurpose.Metadata });
            foreach (var schema in schemas)
            {
                Logger.For(typeof(Program)).InfoFormat("Schema {0} used {1} times", schema.Title, schema.GetAllUsageCount<ComponentData>());
            }
        }
    }
}
