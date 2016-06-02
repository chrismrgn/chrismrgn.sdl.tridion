﻿using chrismrgn.sdl.tridion.core.FileCache;
using chrismrgn.sdl.tridion.core.Logging;
using System.Collections.Generic;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.coreservice.Helpers
{
    public static class PublicationHelpers
    {
        public static IList<PublicationData> LoadAllPublications()
        {
            Logger.For(typeof(PublicationHelpers)).DebugFormat("Loading PublicationData");

            string filename = "PublicationData.txt";
            var publications = FileCache.LoadFromFile<IList<PublicationData>>(filename);
            
            if(publications == null)
            { 
                var filter = new PublicationsFilterData
                {
                    BaseColumns = ListBaseColumns.Extended
                };

                publications = TridionCoreServiceFactory.GetSystemWideList<PublicationData>(filter);
                FileCache.SaveToFile(filename, publications);
            }
            else
                Logger.For(typeof(PublicationHelpers)).DebugFormat("Loading publications from cache");

            Logger.For(typeof(PublicationHelpers)).DebugFormat("Found {0} PublicationData", publications.Count);

            return publications;
        }
    }
}
