using System;
using System.Collections.Generic;
using System.Linq;
using chrismrgn.sdl.tridion.coreservice;
using Tridion.ContentManager.CoreService.Client;

namespace chrismgrn.sdl.tridion.coreservice.extensionmethods
{
    public static class IdentifiableObjectExtensionMethods
    {
       #region ISessionAwareCoreService Methods

        public static PublishTransactionData PublishItem<T>(this T item, string[] targets, PublishInstructionData publishInstruction = null, PublishPriority priority = PublishPriority.Normal, ReadOptions readOptions = null) where T : RepositoryLocalObjectData
        {
            if (item == null)
                throw new ArgumentNullException();
            if (targets == null || !targets.Any())
                throw new ArgumentNullException();

            return PublishItems(new string[] {item.Id}, targets, publishInstruction, priority, readOptions).FirstOrDefault();
        }
        public static PublishTransactionData[] PublishItems<T>(this T items, string[] targets, PublishInstructionData publishInstruction = null, PublishPriority priority = PublishPriority.Normal, ReadOptions readOptions = null) where T : IEnumerable<RepositoryLocalObjectData>
        {
            if (items == null || !items.Any())
                throw new ArgumentNullException();
            if (targets == null || !targets.Any())
                throw new ArgumentNullException();

            return PublishItems(items.Select(x=>x.Id).ToArray(), targets, publishInstruction, priority, readOptions);
        }
        public static PublishTransactionData[] PublishItems(this string[] items, string[] targets, PublishInstructionData publishInstruction = null, PublishPriority priority = PublishPriority.Normal, ReadOptions readOptions = null)
        {
            if (items == null || !items.Any())
                throw new ArgumentNullException();
            if (targets == null || !targets.Any())
                throw new ArgumentNullException();

            return TridionCoreServiceFactory.Publish(items, targets, publishInstruction, priority, readOptions);
        }
        
        //public static void CheckInItem<T>(this T item, ISessionAwareCoreService client, bool removeLock = false, string userComment = "", ReadOptions readOptions = null) where T : VersionedItemData
        //{
        //    if (readOptions == null)
        //        readOptions = ReadOptions;

        //    client.CheckIn(item.Id, removeLock, userComment, readOptions);
        //}
        //public static void CheckOutItem<T>(this T item, ISessionAwareCoreService client, bool removeLock = false, ReadOptions readOptions = null) where T : VersionedItemData
        //{
        //    if (readOptions == null)
        //        readOptions = ReadOptions;

        //    client.CheckOut(item.Id, removeLock, readOptions);
        //}
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

        #endregion

        #region ICoreSession Methods

        //public static void PublishItem<T>(this T item, ICoreService client, string[] targets, PublishInstructionData publishInstruction = null, PublishPriority priority = PublishPriority.Normal, ReadOptions readOptions = null) where T : RepositoryLocalObjectData
        //{
        //    PublishItem(new string[] { item.Id }, client, targets, publishInstruction, priority, readOptions);
        //}
        //public static void PublishItem(this string[] items, ICoreService client, string[] targets, PublishInstructionData publishInstruction = null, PublishPriority priority = PublishPriority.Normal, ReadOptions readOptions = null)
        //{
        //    if (readOptions == null)
        //        readOptions = ReadOptions;

        //    if (publishInstruction == null)
        //        publishInstruction = PublishInstruction;

        //    client.Publish(items, publishInstruction, targets, priority, readOptions);
        //}
        //public static void CheckInItem<T>(this T item, ICoreService client, bool removeLock = false, string userComment = "", ReadOptions readOptions = null) where T : VersionedItemData
        //{
        //    if (readOptions == null)
        //        readOptions = ReadOptions;

        //    client.CheckIn(item.Id, removeLock, userComment, readOptions);
        //}
        //public static void CheckOutItem<T>(this T item, ICoreService client, bool removeLock = false, ReadOptions readOptions = null) where T : VersionedItemData
        //{
        //    if (readOptions == null)
        //        readOptions = ReadOptions;

        //    client.CheckOut(item.Id, removeLock, readOptions);
        //}
        //public static void LocalizeItem<T>(this T item, ICoreService client, ReadOptions readOptions = null) where T : VersionedItemData
        //{
        //    if (readOptions == null)
        //        readOptions = ReadOptions;

        //    client.Localize(item.Id, readOptions);
        //}
        //public static void UnLocalizeItem<T>(this T item, ICoreService client, ReadOptions readOptions = null) where T : VersionedItemData
        //{
        //    if (readOptions == null)
        //        readOptions = ReadOptions;

        //    client.UnLocalize(item.Id, readOptions);
        //}
        //public static void CreateItem<T>(this T item, ICoreService client, ReadOptions readOptions = null) where T : VersionedItemData
        //{
        //    if (readOptions == null)
        //        readOptions = ReadOptions;

        //    client.Create(item, readOptions);
        //}
        //public static void SaveItem<T>(this T item, ICoreService client, ReadOptions readOptions = null) where T : VersionedItemData
        //{
        //    if (readOptions == null)
        //        readOptions = ReadOptions;

        //    client.Save(item, readOptions);
        //}
        //public static void DeleteItem<T>(this T item, ICoreService client) where T : VersionedItemData
        //{
        //    client.Delete(item.Id);
        //}
        //public static IEnumerable<TReturn> WhereUsed<T, TReturn>(this T item, ICoreService client, UsingItemsFilterData usingItemsFilter) where T : VersionedItemData where TReturn : VersionedItemData
        //{
        //    if (usingItemsFilter == null)
        //        usingItemsFilter = UsingItemsFilter;

        //    return client.GetList(item.Id, usingItemsFilter).Cast<TReturn>();
        //}

        #endregion

    }
}
