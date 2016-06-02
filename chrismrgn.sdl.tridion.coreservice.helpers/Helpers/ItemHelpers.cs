using chrismgrn.sdl.tridion.coreservice.extensionmethods;
using chrismrgn.sdl.tridion.core;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.coreservice.Helpers
{
    public static class ItemHelpers
    {
        internal static IList<T> LoadAllByPublication<T>(IList<PublicationData> publications = null, SchemaPurpose[] schemaPurposesToInclude = null) where T : RepositoryLocalObjectData
        {
            if (publications == null)
                publications = PublicationHelpers.LoadAllPublications();

            var items = new List<T>();
            Parallel.ForEach(publications,
                        new ParallelOptions
                        {
                            MaxDegreeOfParallelism = Settings.MaxThreads
                        },
                        publication =>
                        {
                            items.AddRange(publication.LoadAll<T>(schemaPurposesToInclude));
                        }
                    );

            return items;
        }

        public static T LoadItem<T>(string id) where T : IdentifiableObjectData
        {
            return TridionCoreServiceFactory.Get<T>(id);
        }
    }
}
