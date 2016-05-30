using System.Collections.Generic;
using chrismrgn.sdl.tridion.coreservice;
using Tridion.ContentManager.CoreService.Client;
using chrismrgn.sdl.tridion.core;
using System.Linq;
using System;
using chrismrgn.sdl.tridion.core.Logging;

namespace chrismgrn.sdl.tridion.coreservice.extensionmethods
{
    public static class RepositoryDataExtensions
    {
        public static IList<T> LoadAll<T>(this RepositoryData item, SchemaPurpose[] schemaPurposesToInclude = null) where T : RepositoryLocalObjectData
        {
            Logger.For(typeof(RepositoryDataExtensions)).DebugFormat("Loading {0} for {1}", typeof(T).Name, item.Title);
            var items = TridionCoreServiceFactory.GetList<T>(item.Id, new RepositoryItemsFilterData
            {
                BaseColumns = ListBaseColumns.Extended,
                ItemTypes = new[] { ItemTypeResolver.GetItemType(typeof(T)) },
                Recursive = true,
                SchemaPurposes = schemaPurposesToInclude
            });

            //Remove Local Copies
            items = items.Where(x=>x.BluePrintInfo.IsShared == false || x.BluePrintInfo.IsLocalized == true).ToList();
            Logger.For(typeof(RepositoryDataExtensions)).DebugFormat("Found {0} {1} for {2}", items.Count, typeof(T).Name, item.Title);
            return items; 
        }
    }
}
