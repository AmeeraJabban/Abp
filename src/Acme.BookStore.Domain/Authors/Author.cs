using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Authors
{
    public class Author : AuditedAggregateRoot<Guid>
    {
        public string Name { get;  set; }
        public DateTime BirthDate { get; set; }
        public string ShortBio { get; set; }

    }
}
