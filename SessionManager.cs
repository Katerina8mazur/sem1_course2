﻿using Microsoft.Extensions.Caching.Memory;
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

        public static Session CreateSession(int accountId, string login)
        {
            var session = new Session(Guid.NewGuid(), accountId, login, DateTime.Now);
            cache.Set(session.Id, session);
            return session;
        }

        public static bool CheckSession(Guid id) 
            => cache.Get(id) != null;

        public static Session GetSession(Guid id) 
            => cache.Get<Session>(id);

        public static void DeleteSession(Guid id)
            => cache.Remove(id);
    }
}
