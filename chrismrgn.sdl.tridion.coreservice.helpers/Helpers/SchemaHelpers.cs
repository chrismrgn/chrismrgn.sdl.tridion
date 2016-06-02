using System.Collections.Generic;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.coreservice.Helpers
{
    public static class SchemaHelpers
    {
        public static IList<SchemaData> LoadAllSchemasByPublication(IList<PublicationData> publications = null, SchemaPurpose[] schemaPurposesToInclude = null)
        {
            return ItemHelpers.LoadAllByPublication<SchemaData>(publications, schemaPurposesToInclude);
        }
    }
}
