using chrismrgn.sdl.tridion.coreservice;
using Tridion.ContentManager.CoreService.Client;

namespace chrismgrn.sdl.tridion.coreservice.extensionmethods
{
    public static class VersionedItemDataExtensions
    {
        public static T CheckInItem<T>(this T item, bool removeLock = false, string userComment = "", ReadOptions readOptions = null) where T : VersionedItemData
        {
            return TridionCoreServiceFactory.CheckIn<T>(item.Id, removeLock, userComment, readOptions);
        }
        public static T CheckOutItem<T>(this T item, bool removeLock = false, ReadOptions readOptions = null) where T : VersionedItemData
        {
            return TridionCoreServiceFactory.CheckOut<T>(item.Id, removeLock, readOptions);
        }

        public static FullVersionInfo FullVersionInfo<T>(this T item) where T : VersionedItemData
        {
            return (FullVersionInfo)item.VersionInfo;
        }


        //public static void LocalizeItem<T>(this T item, ISessionAwareCoreService client, ReadOptions readOptions = null) where T : VersionedItemData
        //{
        //    if (readOptions == null)
        //        readOptions = ReadOptions;

        //    client.Localize(item.Id, readOptions);
        //}
        //public static void UnLocalizeItem<T>(this T item, ISessionAwareCoreService client, ReadOptions readOptions = null) where T : VersionedItemData
        //{
        //    if (readOptions == null)
        //        readOptions = ReadOptions;

        //    client.UnLocalize(item.Id, readOptions);
        //}
        //public static void CreateItem<T>(this T item, ISessionAwareCoreService client, ReadOptions readOptions = null) where T : VersionedItemData
        //{
        //    if (readOptions == null)
        //        readOptions = ReadOptions;

        //    client.Create(item, readOptions);
        //}
        //public static void SaveItem<T>(this T item, ISessionAwareCoreService client, ReadOptions readOptions = null) where T : VersionedItemData
        //{
        //    if (readOptions == null)
        //        readOptions = ReadOptions;

        //    client.Save(item, readOptions);
        //}
        //public static void DeleteItem<T>(this T item, ISessionAwareCoreService client) where T : VersionedItemData
        //{
        //    client.Delete(item.Id);
        //}
        //public static IEnumerable<TReturn> WhereUsed<T, TReturn>(this T item, ISessionAwareCoreService client, UsingItemsFilterData usingItemsFilter) where T : VersionedItemData where TReturn : VersionedItemData
        //{
        //    if (usingItemsFilter == null)
        //        usingItemsFilter = UsingItemsFilter;

        //    return client.GetList(item.Id, usingItemsFilter).Cast<TReturn>();
        //}
    }
}
