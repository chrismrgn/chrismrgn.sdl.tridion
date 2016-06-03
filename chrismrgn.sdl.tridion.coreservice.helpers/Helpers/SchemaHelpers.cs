using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.coreservice.Helpers
{
    public static class SchemaHelpers
    {
        public static IList<SchemaData> LoadAllSchemasByPublication(IList<PublicationData> publications = null, Expression<Func<SchemaData, bool>> filterPredicate = null, SchemaPurpose[] schemaPurposesToInclude = null)
        {
            return ItemHelpers.LoadAllByPublication<SchemaData>(publications, filterPredicate, schemaPurposesToInclude);
        }
    }
}
