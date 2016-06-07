using chrismrgn.sdl.tridion.coreservice.Helpers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.coreservice.helpers.Helpers
{
    public class ComponentHelpers
    {
        public static IList<ComponentData> LoadAllComponentsByPublication(IList<PublicationData> publications = null, Expression<Func<ComponentData, bool>> filterPredicate = null, SchemaPurpose[] schemaPurposesToInclude = null)
        {
            return ItemHelpers.LoadAllByPublication<ComponentData>(publications, filterPredicate, schemaPurposesToInclude);
        }
    }
}
