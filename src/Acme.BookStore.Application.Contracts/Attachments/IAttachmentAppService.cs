using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Attachments
{
    public interface IAttachmentAppService : 
        ICrudAppService <
            AttachmentDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateAttachmentDto
            >
    {
        Task<ListResultDto<BookLookupDto>> GetBookLookupAsync();

    }
}
