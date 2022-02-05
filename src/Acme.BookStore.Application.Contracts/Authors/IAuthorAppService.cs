using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Authors
{
    public interface IAuthorAppService :
       ICrudAppService<
            AuthorDto,
            Guid,
            PagedAndSortedResultRequestDto,
             CreateUpdateAuthorDto>
    {
    }
}
