using chrismgrn.sdl.tridion.coreservice.extensionmethods;
using chrismrgn.sdl.tridion.core;
using chrismrgn.sdl.tridion.core.Logging;
using chrismrgn.sdl.tridion.coreservice.Helpers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.samples.SchemaUsageExport
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            timer.Start();
            Logger.For(typeof(PublicationHelpers)).Debug("Starting...");
            ProcessSchemasAndUsage();
            timer.Stop();
            Logger.For(typeof(PublicationHelpers)).DebugFormat("Finished in {0} seconds ...", timer.Elapsed.Seconds);
            Console.ReadKey();
        }

        private static void ProcessSchemasAndUsage()
        {
            var schemas = SchemaHelpers.LoadAllSchemasByPublication();

            foreach (var schemaGroup in schemas.GroupBy(x=>x.Purpose))
            {
                Logger.For(typeof(Program)).Debug("");
                Logger.For(typeof(Program)).DebugFormat("{0} Schemas", schemaGroup.First().Purpose);
                Logger.For(typeof(Program)).Debug("====================");

                Parallel.ForEach(schemaGroup,
                        new ParallelOptions
                        {
                            MaxDegreeOfParallelism = Settings.MaxThreads
                        },
                        schema =>
                        {
                            //CSV Format, to be opened in Excel
                            Logger.For(typeof(Program)).InfoFormat("{0},{1},{2},{3}", schema.Purpose, schema.Title, schema.BluePrintInfo.OwningRepository.Title, schema.GetAllUsageCount<ComponentData>());
                        }
                    );
            }
        }
    }
}
