using chrismrgn.sdl.tridion.core.Logging;
using System;
using System.Diagnostics;

namespace chrismrgn.sdl.tridion.samples.componenttemplaterecentlyaddedtopage
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            timer.Start();

            Logger.Debug("Starting...");
            //Logger.For(typeof(PublicationHelpers)).Debug("Starting...");

            //Solution Segment Info Article Left (BDP)
            //ProcessComponentTemplateRecentlyAddedToPage("tcm:8-18441-32");
            //Service Segment Info Article Left (BDP)
            //ProcessComponentTemplateRecentlyAddedToPage("tcm:8-62707-32");

            timer.Stop();
            Logger.Debug("Finished in {0} seconds ...", new object[] { timer.Elapsed.Seconds });
            //Logger.For(typeof(PublicationHelpers)).DebugFormat("Finished in {0} seconds ...", timer.Elapsed.Seconds);
            Console.ReadKey();
        }

        //private static void ProcessComponentTemplateRecentlyAddedToPage(string id)
        //{
        //    //CSV Format, to be opened in Excel
        //    Logger.For(typeof(Program)).InfoFormat("{0},{1},{2},{3},{4}", "pageid", "Current version", "Version without CT", "Version with CT", "Version with creation date");

        //    var componentTemplate = ComponentTemplateHelpers.LoadComponentTemplate(id);

        //    var usageCount = componentTemplate.GetAllUsageCount<PageData>(new UsingItemsFilterData
        //                                                {
        //                                                    ItemTypes = new[] { ItemTypeResolver.GetItemType(typeof(PageData)) },
        //                                                    IncludedVersions = VersionCondition.AllVersions,
        //                                                    IncludeLocalCopies = false,
        //                                                    BaseColumns = ListBaseColumns.Extended
        //                                                });
        //    Logger.For(typeof(Program)).DebugFormat("Count {0} {1}", componentTemplate.Title, usageCount);

        //    Parallel.ForEach(componentTemplate.GetAllUsages<PageData>(),
        //                new ParallelOptions
        //                {
        //                    MaxDegreeOfParallelism = Settings.MaxThreads
        //                },
        //                componentTemplateUsage =>
        //                {
        //                    ProcessComponentTemplateUsage(id, componentTemplateUsage);
        //                }
        //            );
        //}

        //private static void ProcessComponentTemplateUsage(string componentTemplateId, PageData componentTemplateUsage)
        //{
        //    var page = ItemHelpers.LoadItem<PageData>(componentTemplateUsage.Id);
        //    Logger.For(typeof(Program)).DebugFormat("Processing {0} {1} {2}", page.Id, page.FullVersionInfo().Version, page.Title);

        //    var version = page.FullVersionInfo().Version;
        //    PageData pageVersion = null;
        //    PageData previousVersion = page;
        //    bool match = false;
        //    while (version > 1)
        //    {
        //        version--;
        //        pageVersion = ItemHelpers.LoadItem<PageData>(componentTemplateUsage.Id+"-v"+version);

        //        if (!pageVersion.ComponentPresentations.Any(x => new TcmUri(x.ComponentTemplate.IdRef).ItemId == new TcmUri(componentTemplateId).ItemId))
        //        {
        //            Logger.For(typeof(Program)).DebugFormat("   Missing CT while processing {0} {1} {2}", pageVersion.Id, pageVersion.FullVersionInfo().Version, pageVersion.Title);
        //            match = true;
        //            break;
        //        }
        //        else
        //        {
        //            Logger.For(typeof(Program)).DebugFormat("   Found CT while processing {0} {1} {2}", pageVersion.Id, pageVersion.FullVersionInfo().Version, pageVersion.Title);
        //            previousVersion = pageVersion;
        //        }
        //    }

        //    if (match)
        //    {
        //        Logger.For(typeof(Program)).WarnFormat("   On {0} version {1} does not have CT. Subsequent {2} does, created on {3}", page.Id, pageVersion.FullVersionInfo().Version, previousVersion.FullVersionInfo().Version, previousVersion.FullVersionInfo().RevisionDate);
        //        //CSV Format, to be opened in Excel
        //        Logger.For(typeof(Program)).InfoFormat("{0},{1},{2},{3},{4}", page.Id, page.FullVersionInfo().Version, pageVersion.FullVersionInfo().Version, previousVersion.FullVersionInfo().Version, previousVersion.FullVersionInfo().RevisionDate.Value.ToString("dd-MM-yy"));
        //    }
        //}
    }
}
