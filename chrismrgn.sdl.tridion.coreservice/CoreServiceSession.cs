using System;
using System.Net;
using System.ServiceModel;
using System.Xml;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.coreservice
{
    public class CoreServiceSession : IDisposable
    {
        private string _coreServiceVersion;

        private CoreServiceClient _client;
        private SessionAwareCoreServiceClient _sessionAwareClient;

        public CoreServiceSession()
        {
            InitializeClient("CoreService", CredentialCache.DefaultNetworkCredentials);
        }

        public CoreServiceSession(string endPoint, NetworkCredential credentials, bool useSessionAware = false)
        {
            if(useSessionAware)
                InitializeSessionAwareClient(endPoint, credentials);
            else
                InitializeClient(endPoint, credentials);
        }

        private void InitializeClient(string endPoint, NetworkCredential credentials)
        {
            try
            {
                var binding = new BasicHttpBinding()
                {
                    MaxReceivedMessageSize = 2147483647,
                    ReaderQuotas = new XmlDictionaryReaderQuotas
                    {
                        MaxStringContentLength = 2147483647,
                        MaxArrayLength = 2147483647
                    },
                    Security = 
                    {
                        Mode = BasicHttpSecurityMode.TransportCredentialOnly,
                        Transport = new HttpTransportSecurity
                        {
                            ClientCredentialType = HttpClientCredentialType.Windows
                        }
                        
                    }
                };

                _client = new CoreServiceClient(binding, new EndpointAddress(endPoint + "/basicHttp"));

                if (_client.ClientCredentials != null)
                {
                    _client.ClientCredentials.Windows.ClientCredential = credentials;
                }
                
                if (_client != null) _coreServiceVersion = _client.GetApiVersion();
            }

            catch (EndpointNotFoundException e) { }
            catch (Exception e) { }
        }

        private void InitializeSessionAwareClient(string endPoint, NetworkCredential credentials)
        {
            //TODO: Resolve Error
            try
            {
                var binding = new WSHttpBinding
                {
                    MaxReceivedMessageSize = 2147483647,
                    ReaderQuotas = new XmlDictionaryReaderQuotas
                    {
                        MaxStringContentLength = 2147483647,
                        MaxArrayLength = 2147483647
                    }
                };

                var _sessionAwareClient = new SessionAwareCoreServiceClient(binding, new EndpointAddress(endPoint + "/wsHttp"));

                if (_sessionAwareClient.ClientCredentials != null)
                {
                    _sessionAwareClient.ClientCredentials.Windows.ClientCredential = credentials;
                }

                if (_sessionAwareClient != null) _coreServiceVersion = _sessionAwareClient.GetApiVersion();
            }

            catch (EndpointNotFoundException e) { }
            catch (Exception e) { }
        }

        public CoreServiceClient CoreServiceClient
        {
            get { return _client; }
        }

        public SessionAwareCoreServiceClient CoreServiceSessionAwareClient
        {
            get { return _sessionAwareClient; }
        }

        public UserData User
        {
            get { return _client.GetCurrentUser(); }
        }

        public string CoreServiceVersion
        {
            get { return _coreServiceVersion; }
        }

        public void Dispose()
        {
            if (_client.State == CommunicationState.Faulted)
            {
                _client.Abort();
            }
            else
            {
                _client.Close();
            }
        }
    }
}