using System;
using System.Net;
using System.ServiceModel;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.coreservice
{
    public class CoreServiceSession : IDisposable
    {
        private string _coreServiceVersion;

        private CoreServiceClient _client;

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
                _client = new CoreServiceClient(endPoint);
                _client.ChannelFactory.Credentials.Windows.ClientCredential = credentials;
                
                if (_client != null) _coreServiceVersion = _client.GetApiVersion();
            }

            catch (EndpointNotFoundException e) { }
            catch (Exception e) { }
        }

        private void InitializeSessionAwareClient(string endPoint, NetworkCredential credentials)
        {
            try
            {
                var _client = new SessionAwareCoreServiceClient(endPoint);
                _client.ChannelFactory.Credentials.Windows.ClientCredential = credentials;

                if (_client != null) _coreServiceVersion = _client.GetApiVersion();
            }

            catch (EndpointNotFoundException e) { }
            catch (Exception e) { }
        }

        public CoreServiceClient CoreServiceClient
        {
            get { return _client; }
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