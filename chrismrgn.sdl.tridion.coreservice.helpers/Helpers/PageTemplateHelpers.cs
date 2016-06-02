using System.Collections.Generic;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.coreservice.Helpers
{
    public static class PageTemplateHelpers
    {
        public static IList<PageTemplateData> LoadAllPageTemplatesByPublication(IList<PublicationData> publications = null)
        {
            return ItemHelpers.LoadAllByPublication<PageTemplateData>(publications);
        }
    }
}
