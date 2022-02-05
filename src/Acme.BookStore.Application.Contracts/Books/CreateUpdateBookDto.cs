using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Books
{
    public class CreateUpdateBookDto : AuditedEntityDto<Guid>
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        [Required]

        public BookType Type { get; set; }
        [Required]
        [DataType(DataType.Date)]

        public DateTime PublishDate { get; set; }
        [Required]

        public float Price { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        public string Author { get; set; }
    }
}
