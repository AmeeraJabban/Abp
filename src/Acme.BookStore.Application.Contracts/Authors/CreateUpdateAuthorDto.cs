using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Authors
{
    public class CreateUpdateAuthorDto : AuditedEntityDto<Guid>
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }
        [Required]
        public string ShortBio { get; set; }

    }
}
