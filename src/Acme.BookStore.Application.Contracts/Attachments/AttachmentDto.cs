using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Attachments
{
    public class AttachmentDto : AuditedEntityDto<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public Guid BookId { get; set; }
        public string Book { get; set; }
    }
}
