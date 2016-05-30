using chrismgrn.sdl.tridion.coreservice.extensionmethods;
using chrismrgn.sdl.tridion.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.coreservice.Helpers
{
    public static class ItemHelpers
    {
        public static IList<T> LoadAllByPublication<T>(IList<PublicationData> publications = null, SchemaPurpose[] schemaPurposesToInclude = null) where T : RepositoryLocalObjectData
        {
            if (publications == null)
                publications = PublicationHelpers.LoadAllPublications();

            var items = new List<T>();
            Parallel.ForEach(publications,
                        new ParallelOptions
                        {
                            MaxDegreeOfParallelism = Settings.MaxThreads()
                        },
                        publication =>
                        {
                            items.AddRange(publication.LoadAll<T>(schemaPurposesToInclude));
                        }
                    );

            //Apply TypeFilter

            return items;
        }
    }
}
