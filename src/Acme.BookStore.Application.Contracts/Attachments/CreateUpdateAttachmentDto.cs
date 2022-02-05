using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Attachments
{
    public class CreateUpdateAttachmentDto : AuditedEntityDto<Guid>
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(150)]

        public string Description { get; set; }
        [Required]
        [StringLength(50)]

        public string Link { get; set; }
        [Required]

        public Guid BookId { get; set; }
        public string Book { get; set; }
    }
}
