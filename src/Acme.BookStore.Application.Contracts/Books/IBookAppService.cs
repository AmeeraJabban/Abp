using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Books
{
    public interface IBookAppService :
        ICrudAppService< //Defines CRUD method
            BookDto, //Used to show books
            Guid, //Primary key of the book entity
            BookTitleSearchDto, //Used for paging/sorting
            CreateUpdateBookDto>
    {
        Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync();
    }
}
