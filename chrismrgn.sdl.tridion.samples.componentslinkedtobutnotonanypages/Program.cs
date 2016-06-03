using chrismrgn.sdl.tridion.core;
using chrismrgn.sdl.tridion.core.Logging;
using chrismrgn.sdl.tridion.coreservice.Helpers;
using chrismgrn.sdl.tridion.coreservice.extensionmethods;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Tridion.ContentManager.CoreService.Client;
using System.Linq;

namespace chrismrgn.sdl.tridion.samples.componentslinkedtobutnotonanypages
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            timer.Start();

            Logger.Debug("Starting...");

            GetAllComponentsLinkedToButNotOnPage();

            timer.Stop();
            Logger.Debug("Finished in {0} seconds ...", new object[] { timer.Elapsed.Seconds });
            Console.ReadKey();
        }

        private static void GetAllComponentsLinkedToButNotOnPage()
        {
            var schemas = SchemaHelpers.LoadAllSchemasByPublication(filterPredicate: x=>x.Title.ToLower().Contains("[deprecated]"));
            Logger.Info("found {0} schemas", schemas.Count);

            Parallel.ForEach(schemas,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = Settings.MaxThreads
                },
                schema =>
                {
                    ProcessSchema(schema);
                });
        }

        private static void ProcessSchema(SchemaData schema)
        {
            var components = schema.GetAllUsages<ComponentData>();

            foreach (var component in components)
            {
                var usingComponents = component.GetAllUsages<ComponentData>(filterPredicate: x => new TcmUri(x.Schema.IdRef).ItemId != 18521 && new TcmUri(x.Schema.IdRef).ItemId != 18147);

                if(usingComponents.Count > 0)
                Logger.Warn("Found {0} components using Component {1} based on Schema {2}: {3}", usingComponents.Count, component.Id, schema.Title, string.Join(", ", usingComponents.Select(x=> x.Id + " - " + x.Title)));
            }
        }
    }
}