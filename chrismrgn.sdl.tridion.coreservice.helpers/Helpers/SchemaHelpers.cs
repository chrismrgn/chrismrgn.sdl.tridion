using chrismgrn.sdl.tridion.coreservice.extensionmethods;
using chrismrgn.sdl.tridion.core;
using chrismrgn.sdl.tridion.core.FileCache;
using chrismrgn.sdl.tridion.core.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
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
