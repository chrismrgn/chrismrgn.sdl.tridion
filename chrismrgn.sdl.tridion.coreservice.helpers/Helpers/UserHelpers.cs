using System.Collections.Generic;
using Tridion.ContentManager.CoreService.Client;

namespace chrismrgn.sdl.tridion.coreservice.helpers.Helpers
{
    public static class UserHelpers
    {
        public static IList<UserData> GetAllUsers()
        {
            var filter = new UsersFilterData
            {
                BaseColumns = ListBaseColumns.Extended
            };

            var users = TridionCoreServiceFactory.GetSystemWideList<UserData>(filter);
            return users;
        }
    }
}
