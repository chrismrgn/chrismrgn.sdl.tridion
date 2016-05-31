using System;
using System.Collections.Generic;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.core
{
    public static class ItemTypeResolver
    {
        public static ItemType GetItemType(Type type)
        {
            var itemMapping = new Dictionary<Type, ItemType> {
                { typeof(SchemaData), ItemType.Schema },
                { typeof(PageData), ItemType.Page },
                { typeof(ComponentData), ItemType.Component },
                { typeof(FolderData), ItemType.Folder },
                { typeof(VirtualFolderData), ItemType.VirtualFolder },
                { typeof(CategoryData), ItemType.Category },
                { typeof(KeywordData), ItemType.Keyword },
                { typeof(ComponentTemplateData), ItemType.ComponentTemplate },
                { typeof(PageTemplateData), ItemType.PageTemplate },
                { typeof(TemplateBuildingBlockData), ItemType.TemplateBuildingBlock },
                { typeof(GroupData), ItemType.Group },
                { typeof(UserData), ItemType.User },
                { typeof(PublicationData), ItemType.Publication },
            };

            var itemType = ItemType.UnknownByClient;

            if (!itemMapping.TryGetValue(type, out itemType))
                throw new Exception("GetItemType failed to loopup ItemType for " + type.Name);

            return itemType;
        }
    }
}
