using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using chrismrgn.sdl.tridion.coreservice.Extensions;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.coreservice
{
    public static class TridionCoreServiceFactory
    {
        public static CoreServiceSession CreateCoreServiceSession(bool isSessionAware = false)
        {
            var endPoint = ConfigurationManager.AppSettings["EndPointUrl"] ?? "CoreService";
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["Username"] ?? "DOMAIN\\USERNAME",
                                                        ConfigurationManager.AppSettings["Password"] ?? "PASSWORD");
            return new CoreServiceSession(endPoint, credentials, isSessionAware);
        }

        public static CoreServiceClient CreateCoreServiceClient(bool isSessionAware = false)
        {
            if (!isSessionAware)
                bool.TryParse(ConfigurationManager.AppSettings["isSessionAware"], out isSessionAware);

            return CreateCoreServiceSession(isSessionAware).CoreServiceClient;
        }

        //TODO:
        //public static SessionAwareCoreServiceClient CreateCoreServiceClient(bool isSessionAware = false)
        //{
        //    if (!isSessionAware)
        //        bool.TryParse(ConfigurationManager.AppSettings["isSessionAware"], out isSessionAware);
            
        //    return CreateCoreServiceSession(isSessionAware).CoreServiceSessionAwareClient;
        //}

        public static PublishTransactionData[] Publish(string [] itemIds, string[] targets, PublishInstructionData publishInstruction = null, PublishPriority priority = PublishPriority.Normal, ReadOptions readOptions = null)
        {
            PublishTransactionData[] obj = null;
            CreateCoreServiceClient().Using(client =>
            {
                try
                {
                    if (readOptions == null) readOptions = new ReadOptions { LoadFlags = LoadFlags.Expanded }; ;
                    if (publishInstruction == null) publishInstruction = new PublishInstructionData { };;

                    if (readOptions == null) readOptions = new ReadOptions { LoadFlags = LoadFlags.Expanded };
                    obj = client.Publish(itemIds, publishInstruction, targets, priority, readOptions);
                }
                catch (Exception e)
                {
                    obj = null;
                }
            });
            return obj;
        }
        public static T Get<T>(string id, ReadOptions readOptions = null) where T : IdentifiableObjectData
        {
            object obj = null;
            CreateCoreServiceClient().Using(client =>
            {
                try
                {
                    if (readOptions == null) readOptions = new ReadOptions { LoadFlags = LoadFlags.Expanded };
                    obj = client.Read(id, readOptions);
                }
                catch (Exception e)
                {
                    obj = null;
                }
            });
            return obj as T;
        }
        public static SchemaFieldsData GetSchemaFields(string id, bool expandEmbeddedFields = true, ReadOptions readOptions = null)
        {
            SchemaFieldsData obj = null;
            CreateCoreServiceClient().Using(client =>
            {
                try
                {
                    if (readOptions == null) readOptions = new ReadOptions { LoadFlags = LoadFlags.Expanded };
                    obj = client.ReadSchemaFields(id, expandEmbeddedFields, readOptions);
                }
                catch (Exception e)
                {
                    obj = null;
                }
            });
            return obj;
        }
        public static IList<T> GetSearchResults<T>(SearchQueryData filter) where T : IdentifiableObjectData
        {
            IList<T> obj = null;
            CreateCoreServiceClient().Using(client =>
            {
                try
                {
                    obj = client.GetSearchResults(filter).Cast<T>().ToList();
                }
                catch (Exception e)
                {
                    obj = null;
                }
            });
            return obj;
        }
        public static IList<T> GetSystemWideList<T>(SystemWideListFilterData filter) where T : IdentifiableObjectData
        {
            IList<T> obj = null;
            CreateCoreServiceClient().Using(client =>
            {
                try
                {
                    obj = client.GetSystemWideList(filter).Cast<T>().ToList();
                }
                catch (Exception e)
                {
                    obj = null;
                }
            });
            return obj;
        }
        public static XElement GetSystemWideListXml(SystemWideListFilterData filter)
        {
            XElement obj = null;
            CreateCoreServiceClient().Using(client =>
            {
                try
                {
                    obj = client.GetSystemWideListXml(filter);
                }
                catch (Exception e)
                {
                    obj = null;
                }
            });
            return obj;
        }
        public static UserData GetCurrentUser()
        {
            UserData obj = null;
            CreateCoreServiceClient().Using(client =>
            {
                try
                {
                    obj = client.GetCurrentUser();
                }
                catch (Exception e)
                {
                    obj = null;
                }
            });
            return obj;
        }
        public static IList<T> GetList<T>(string id, SubjectRelatedListFilterData filter) where T : IdentifiableObjectData
        {
            IList<T> obj = null;
            CreateCoreServiceClient().Using(client =>
            {
                try
                {
                    obj = client.GetList(id, filter).Cast<T>().ToList();
                }
                catch (Exception e)
                {
                    obj = null;
                }
            });
            return obj;
        }
        public static XElement GetListXml(string id, SubjectRelatedListFilterData filter)
        {
            XElement obj = null;
            CreateCoreServiceClient().Using(client =>
            {
                try
                {
                    obj = client.GetListXml(id, filter);
                }
                catch (Exception e)
                {
                    obj = null;
                }
            });
            return obj;
        }
        public static T CheckIn<T>(string id, bool removePermanentLock = false, string userComment = "", ReadOptions readOptions = null) where T : VersionedItemData
        {
            object obj = null;
            CreateCoreServiceClient().Using(client =>
            {
                try
                {
                    if (readOptions == null) readOptions = new ReadOptions { LoadFlags = LoadFlags.Expanded };
                    obj = client.CheckIn(id, removePermanentLock, userComment, readOptions);
                }
                catch (Exception e)
                {
                    obj = null;
                }
            });
            return obj as T;
        }
        public static T CheckOut<T>(string id, bool removePermanentLock = false, ReadOptions readOptions = null) where T : VersionedItemData
        {
            object obj = null;
            CreateCoreServiceClient().Using(client =>
            {
                try
                {
                    if (readOptions == null) readOptions = new ReadOptions { LoadFlags = LoadFlags.Expanded };
                    obj = client.CheckOut(id, removePermanentLock, readOptions);
                }
                catch (Exception e)
                {
                    obj = null;
                }
            });
            return obj as T;
        }
        //Implement other exposed client methods here
    }
}