using Tridion.ContentManager.CoreService.Client;

namespace chrismgrn.sdl.tridion.coreservice.extensionmethods
{
    public static class SchemaDataExtensions
    {
        public static ItemType GetItemType(this SchemaData item)
        {
            return ItemType.Schema;
        }
    }
}
