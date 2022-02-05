using Acme.BookStore.Attachments;
using Acme.BookStore.Books;
using Acme.BookStore.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Attachments
{
    public class AttachmentAppService :
        CrudAppService <
            Attachment ,
            AttachmentDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateAttachmentDto
            >,
        IAttachmentAppService
    {
        private readonly IRepository<Book,Guid> _bookRepository; 
        public AttachmentAppService( IRepository<Attachment, Guid> repository,
            IRepository<Book, Guid> bookRepository) :base(repository) {
            _bookRepository = bookRepository;
            GetPolicyName = BookStorePermissions.Attachments.Default;
            GetListPolicyName = BookStorePermissions.Attachments.Default;
            CreatePolicyName = BookStorePermissions.Attachments.Create;
            UpdatePolicyName = BookStorePermissions.Attachments.Edit;
            DeletePolicyName = BookStorePermissions.Attachments.Delete;
        }
        public override async Task<AttachmentDto> GetAsync(Guid id)
        {
            var queryable = await Repository.GetQueryableAsync();
            var query = from attachment in queryable
                        join book in await _bookRepository.GetQueryableAsync() on attachment.BookId equals book.Id
                        where attachment.Id == id
                        select new { attachment, book };

            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Book), id);
            }

            var attachmentDto = ObjectMapper.Map<Attachment, AttachmentDto>(queryResult.attachment);
            attachmentDto.Book = queryResult.book.Name;
            return attachmentDto;

        }

        public override async Task<PagedResultDto<AttachmentDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var qurable = await Repository.GetQueryableAsync();

            var query = from attachment in qurable
                        join book in await _bookRepository.GetQueryableAsync() on attachment.BookId equals book.Id
                        select new { attachment, book };
            query = query
                .OrderBy(x => (NormalizeSorting(input.Sorting)))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var queryResult = await AsyncExecuter.ToListAsync(query);

            var attachmentDtos = queryResult.Select(x =>
            {
                var attachmentDto = ObjectMapper.Map<Attachment, AttachmentDto>(x.attachment);
                attachmentDto.Book = x.book.Name;
                return attachmentDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<AttachmentDto>(
                totalCount,
                attachmentDtos
            );
        }
        public async Task<ListResultDto<BookLookupDto>> GetBookLookupAsync() { 
              var books = await _bookRepository.GetListAsync();
            return new ListResultDto<BookLookupDto>(
               ObjectMapper.Map<List<Book>, List<BookLookupDto>>(books)
           );
        }
        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"Attachment.{nameof(Attachment.Title)}";
            }

            if (sorting.Contains("book", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace(
                    "book",
                    "book.Name",
                    StringComparison.OrdinalIgnoreCase
                );
            }

            return $"Attachment.{sorting}";
        }

    }
}
