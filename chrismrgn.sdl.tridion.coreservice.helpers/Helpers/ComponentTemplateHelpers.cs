using System.Collections.Generic;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.coreservice.Helpers
{
    public static class ComponentTemplateHelpers
    {
        public static IList<ComponentTemplateData> LoadAllComponentTemplatesByPublication(IList<PublicationData> publications = null)
        {
            return ItemHelpers.LoadAllByPublication<ComponentTemplateData>(publications);
        }

        public static ComponentTemplateData LoadComponentTemplate(string id)
        {
            return ItemHelpers.LoadItem<ComponentTemplateData>(id);
        }
    }
}
