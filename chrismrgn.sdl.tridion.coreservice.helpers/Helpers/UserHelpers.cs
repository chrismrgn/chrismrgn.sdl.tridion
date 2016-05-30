using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
