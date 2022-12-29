using HttpServer_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1.ORM
{
    internal class SessionDAO
    {
        private MyORM orm;
        public SessionDAO(string connectionString)
        {
            orm = new MyORM(connectionString, "Sessions");
        }

        public Session? Get(Guid id)
            => orm.Select<Session>(id);

        public void Create(Session session)
            => orm.Insert(session);

        public void Delete(Guid id)
            => orm.Delete(id);
    }
}
