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
            var schemas = ItemHelpers.LoadAllByPublication<SchemaData>(schemaPurposesToInclude: new SchemaPurpose[] { SchemaPurpose.Multimedia });

            foreach (var schema in schemas)
            {
                Logger.For(typeof(Program)).InfoFormat("    Schema {0} used {1} times", schema.Title, schema.GetAllUsageCount<ComponentData>());
            }
        }
    }
}
