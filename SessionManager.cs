using HttpServer_1.Controllers;
using HttpServer_1.ORM;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1
{
    internal static class SessionManager
    {
        private static MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        private static SessionDAO sessionDAO = new SessionDAO(Controller.connectionString);

        public static Session CreateSession(int accountId, bool rememberMe)
        {
            var session = new Session(Guid.NewGuid(), accountId, DateTime.Now);
            cache.Set(session.Id, session);
            if (rememberMe)
                sessionDAO.Create(session);
            return session;
        }

        public static bool CheckSession(Guid id) 
            => cache.Get(id) != null || sessionDAO.Get(id) != null;

        public static Session? GetSession(Guid id)
        {
            return cache.Get<Session>(id) ?? sessionDAO.Get(id);
        }

        public static void DeleteSession(Guid id)
        {
            cache.Remove(id);
            sessionDAO.Delete(id);
        }
    }
}
