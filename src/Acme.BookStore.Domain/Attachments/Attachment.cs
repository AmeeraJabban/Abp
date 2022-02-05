using Acme.BookStore.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Attachments
{
    public class Attachment : AuditedAggregateRoot<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }   
        public Guid BookId { get; set; }   
        public virtual Book Book { get; set; }   
    }
}
