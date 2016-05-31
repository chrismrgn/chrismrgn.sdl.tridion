using System;
using System.Collections.Generic;
using System.Linq;
using chrismrgn.sdl.tridion.coreservice;
using Tridion.ContentManager.CoreService.Client;
using chrismrgn.sdl.tridion.core;
using chrismrgn.sdl.tridion.core.Logging;
using chrismrgn.sdl.tridion.core.FileCache;

namespace chrismgrn.sdl.tridion.coreservice.extensionmethods
{
    public static class IdentifiableObjectExtensionMethods
    {
        public static PublishTransactionData PublishItem<T>(this T item, string[] targets, PublishInstructionData publishInstruction = null, PublishPriority priority = PublishPriority.Normal, ReadOptions readOptions = null) where T : RepositoryLocalObjectData
        {
            if (item == null)
                throw new ArgumentNullException();
            if (targets == null || !targets.Any())
                throw new ArgumentNullException();

            return PublishItems(new string[] {item.Id}, targets, publishInstruction, priority, readOptions).FirstOrDefault();
        }
        public static PublishTransactionData[] PublishItems<T>(this T items, string[] targets, PublishInstructionData publishInstruction = null, PublishPriority priority = PublishPriority.Normal, ReadOptions readOptions = null) where T : IEnumerable<RepositoryLocalObjectData>
        {
            if (items == null || !items.Any())
                throw new ArgumentNullException();
            if (targets == null || !targets.Any())
                throw new ArgumentNullException();

            return PublishItems(items.Select(x=>x.Id).ToArray(), targets, publishInstruction, priority, readOptions);
        }
        public static PublishTransactionData[] PublishItems(this string[] items, string[] targets, PublishInstructionData publishInstruction = null, PublishPriority priority = PublishPriority.Normal, ReadOptions readOptions = null)
        {
            if (items == null || !items.Any())
                throw new ArgumentNullException();
            if (targets == null || !targets.Any())
                throw new ArgumentNullException();

            return TridionCoreServiceFactory.Publish(items, targets, publishInstruction, priority, readOptions);
        }
        public static int GetAllUsageCount<T>(this IdentifiableObjectData item) where T : IdentifiableObjectData
        {
            return item.GetAllUsages<T>().Count;
        }
        public static IList<T> GetAllUsages<T>(this IdentifiableObjectData item) where T : IdentifiableObjectData
        {
            Logger.For(typeof(IdentifiableObjectExtensionMethods)).DebugFormat("Loading usages of {0} for {1}", typeof(T).Name, item.Title);

            string filename = string.Format("Usages/{0} - {1}.txt", typeof(T).Name, item.Title);
            var items = FileCache.LoadFromFile<IList<T>>(filename);

            if (items == null)
            {
                var filter = new UsingItemsFilterData
                {
                    ItemTypes = new[] { ItemTypeResolver.GetItemType(typeof(T)) },
                    IncludedVersions = VersionCondition.OnlyLatestAndCheckedOutVersions,
                    IncludeLocalCopies = false,
                    BaseColumns = ListBaseColumns.Extended
                };

                items = TridionCoreServiceFactory.GetList<T>(item.Id, filter);
                Logger.For(typeof(IdentifiableObjectExtensionMethods)).DebugFormat("Found {0} {1} for {2}", items.Count, typeof(T).Name, item.Title);
                FileCache.SaveToFile(filename, items);
            }
            else
                Logger.For(typeof(RepositoryDataExtensions)).DebugFormat("Loading usages of {0} for {1} from cache", typeof(T).Name, item.Title);
            return items;
        }
    }
}
