using chrismgrn.sdl.tridion.coreservice.extensionmethods;
using chrismrgn.sdl.tridion.core;
using chrismrgn.sdl.tridion.core.Logging;
using chrismrgn.sdl.tridion.coreservice.Helpers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.samples.ComponentTemplateUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            timer.Start();
            Logger.For(typeof(PublicationHelpers)).Debug("Starting...");
            ProcessComponentTemplatesAndUsage();
            timer.Stop();
            Logger.For(typeof(PublicationHelpers)).DebugFormat("Finished in {0} seconds ...", timer.Elapsed.Seconds);
            Console.ReadKey();
        }

        private static void ProcessComponentTemplatesAndUsage()
        {
            var componentTemplates = ComponentTemplateHelpers.LoadAllComponentTemplatesByPublication();

            Parallel.ForEach(componentTemplates,
                        new ParallelOptions
                        {
                            MaxDegreeOfParallelism = Settings.MaxThreads
                        },
                        componentTemplate =>
                        {
                            //CSV Format, to be opened in Excel
                            Logger.For(typeof(Program)).InfoFormat("{0},{1},{2}", componentTemplate.Title, componentTemplate.BluePrintInfo.OwningRepository.Title, componentTemplate.GetAllUsageCount<PageData>());
                        }
                    );
        }
    }
}
