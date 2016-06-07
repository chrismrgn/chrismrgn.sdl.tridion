using chrismrgn.sdl.tridion.core;
using chrismrgn.sdl.tridion.core.Logging;
using chrismrgn.sdl.tridion.coreservice.helpers.Helpers;
using chrismrgn.sdl.tridion.coreservice.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.samples.componentsmatchingcontent
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            timer.Start();

            Logger.Debug("Starting...");

            FindComponentsWithContent("box.com");

            timer.Stop();
            Logger.Debug("Finished in {0} seconds ...", timer.Elapsed.Seconds);

            Console.ReadKey();
        }

        private static void FindComponentsWithContent(string v)
        {
            var components = ComponentHelpers.LoadAllComponentsByPublication();

            Parallel.ForEach(components,
                        new ParallelOptions
                        {
                            MaxDegreeOfParallelism = Settings.MaxThreads
                        },
                        component =>
                        {
                            var item = ItemHelpers.LoadItem<ComponentData>(component.Id);
                            Logger.Debug("Processing Component {0}", item.Id);
                            if (item.Content.ToLowerInvariant().Contains(v))
                            {
                                Logger.Info("{0}", item.Id);
                            }
                        }
                    );
        }
    }
}