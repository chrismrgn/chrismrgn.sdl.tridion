using System;
using System.Configuration;
using System.Net;
using chrismrgn.sdl.tridion.coreservice.Extensions;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.coreservice
{
    public static class TridionCoreServiceFactory
    {
        public static CoreServiceClient CreateCoreService()
        {
            var endPoint = ConfigurationManager.AppSettings["EndPoint"] ?? "CoreService";
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["Username"] ?? "DOMAIN\\USERNAME",
                                                        ConfigurationManager.AppSettings["Password"] ?? "PASSWORD");
            return new CoreServiceSession(endPoint, credentials).CoreServiceClient;
        }

        public static PublishTransactionData[] Publish(string [] itemIds, string[] targets, PublishInstructionData publishInstruction = null, PublishPriority priority = PublishPriority.Normal, ReadOptions readOptions = null)
        {
            PublishTransactionData[] obj = null;
            CreateCoreService().Using(client =>
            {
                try
                {
                    if (readOptions == null) readOptions =new ReadOptions { LoadFlags = LoadFlags.Expanded }; ;
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

        public static T Get<T>(string id, ReadOptions readOptions = null) where T : class
        {
            object obj = null;
            CreateCoreService().Using(client =>
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

        public static T GetSchemaFields<T>(string id, ReadOptions readOptions = null) where T : class
        {
            object obj = null;
            CreateCoreService().Using(client =>
            {
                try
                {
                    if (readOptions == null) readOptions = new ReadOptions { LoadFlags = LoadFlags.Expanded };
                    obj = client.ReadSchemaFields(id, true, readOptions);
                }
                catch (Exception e)
                {
                    obj = null;
                }
            });
            return obj as T;
        }

        public static T GetSearchResults<T>(SearchQueryData filter) where T : class
        {
            object obj = null;
            CreateCoreService().Using(client =>
            {
                try
                {
                    obj = client.GetSearchResults(filter);
                }
                catch (Exception e)
                {
                    obj = null;
                }
            });
            return obj as T;
        }

        public static T GetSystemWideList<T>(SystemWideListFilterData filter) where T : class
        {
            object obj = null;
            CreateCoreService().Using(client =>
            {
                try
                {
                    obj = client.GetSystemWideList(filter);
                }
                catch (Exception e)
                {
                    obj = null;
                }
            });
            return obj as T;
        }

        public static T GetSystemWideListXml<T>(SystemWideListFilterData filter) where T : class
        {
            object obj = null;
            CreateCoreService().Using(client =>
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
            return obj as T;
        }

        public static T GetCurrentUser<T>() where T : class
        {
            object obj = null;
            CreateCoreService().Using(client =>
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
            return obj as T;
        }

        public static T GetList<T>(string id, SubjectRelatedListFilterData filter) where T : class
        {
            object obj = null;
            CreateCoreService().Using(client =>
            {
                try
                {
                    obj = client.GetList(id, filter);
                }
                catch (Exception e)
                {
                    obj = null;
                    //Console.WriteLine(e.Message);
                }
            });
            return obj as T;
        }

        public static T GetListXml<T>(string id, SubjectRelatedListFilterData filter) where T : class
        {
            object obj = null;
            CreateCoreService().Using(client =>
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
            return obj as T;
        }

        //Implement other exposed client methods here
    }
}