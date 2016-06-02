using chrismgrn.sdl.tridion.coreservice.extensionmethods;
using chrismrgn.sdl.tridion.core;
using chrismrgn.sdl.tridion.core.Logging;
using chrismrgn.sdl.tridion.coreservice.Helpers;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.samples.PageTemplateUsageExport
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            timer.Start();
            Logger.For(typeof(PublicationHelpers)).Debug("Starting...");
            ProcessPageTemplatesAndUsage();
            timer.Stop();
            Logger.For(typeof(PublicationHelpers)).DebugFormat("Finished in {0} seconds ...", timer.Elapsed.Seconds);
            Console.ReadKey();
        }

        private static void ProcessPageTemplatesAndUsage()
        {
            var pageTemplates = PageTemplateHelpers.LoadAllPageTemplatesByPublication();

            Parallel.ForEach(pageTemplates,
                        new ParallelOptions
                        {
                            MaxDegreeOfParallelism = Settings.MaxThreads
                        },
                        pageTemplate =>
                        {
                            //CSV Format, to be opened in Excel
                            Logger.For(typeof(Program)).InfoFormat("{0},{1},{2}", pageTemplate.Title, pageTemplate.BluePrintInfo.OwningRepository.Title, pageTemplate.GetAllUsageCount<PageData>());
                        }
                    );
        }
    }
}
