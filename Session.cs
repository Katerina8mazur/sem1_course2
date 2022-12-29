using HttpServer_1.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer_1
{
    internal class Session
    {
        [DBField("id")]
        public string IdSql { get => Id.ToString(); }

        public Guid Id { get; set; }

        [DBField("account_id")]
        public int AccountId { get; set; }

        [DBField("date_time")]
        public string CreateDateTimeSql { get => CreateDateTime.ToString("yyyy-MM-dd HH:mm:ss"); }
        public DateTime CreateDateTime { get; set; }

        public Session(Guid id, int accountId, DateTime createDateTime)
        {
            Id = id;
            AccountId = accountId;
            CreateDateTime = createDateTime;
        }

        public Session(string id, int accountId, DateTime createDateTime) : this(Guid.Parse(id), accountId, createDateTime)
        {

        }
    }
}
