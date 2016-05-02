using System;
using System.Collections.Generic;
using System.Linq;
using chrismrgn.sdl.tridion.coreservice;
using Tridion.ContentManager.CoreService.Client;

namespace chrismgrn.sdl.tridion.coreservice.extensionmethods
{
    public static class IdentifiableObjectExtensionMethods
    {
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
    }
}
