using chrismgrn.sdl.tridion.coreservice.extensionmethods;
using chrismrgn.sdl.tridion.core;
using chrismrgn.sdl.tridion.core.Logging;
using chrismrgn.sdl.tridion.coreservice.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.samples.SchemaUsageExport
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
            var schemas = SchemaHelpers.LoadAllSchemasByPublication();

            foreach (var schemaGroup in schemas.GroupBy(x=>x.Purpose))
            {
                Logger.For(typeof(Program)).Info("");
                Logger.For(typeof(Program)).InfoFormat("{0} Schemas", schemaGroup.First().Purpose);
                Logger.For(typeof(Program)).Info("====================");

                Parallel.ForEach(schemaGroup,
                        new ParallelOptions
                        {
                            MaxDegreeOfParallelism = Settings.MaxThreads()
                        },
                        schema =>
                        {
                            Logger.For(typeof(Program)).InfoFormat("{0},{1},{2}", schema.Title, schema.BluePrintInfo.OwningRepository.Title, schema.GetAllUsageCount<ComponentData>());
                        }
                    );
            }
        }
    }
}
