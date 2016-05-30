using chrismrgn.sdl.tridion.core.Logging;
using System;
using System.Collections.Generic;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.coreservice.Helpers
{
    public static class PublicationHelpers
    {
        public static IList<PublicationData> LoadAllPublications()
        {
            Logger.For(typeof(PublicationHelpers)).DebugFormat("Loading PublicationData");
            var filter = new PublicationsFilterData
            {
                BaseColumns = ListBaseColumns.Extended
            };

            var publications = TridionCoreServiceFactory.GetSystemWideList<PublicationData>(filter);
            Logger.For(typeof(PublicationHelpers)).DebugFormat("Found {0} PublicationData", publications.Count);
            return publications;
        }
    }
}
