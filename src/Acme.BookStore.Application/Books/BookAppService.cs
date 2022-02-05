using Acme.BookStore.Authors;
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

namespace Acme.BookStore.Books
{
    public class BookAppService : CrudAppService<
            Book, //The Book entity
            BookDto, //Used to show books
            Guid, //Primary key of the book entity
            BookTitleSearchDto, //Used for paging/sorting
            CreateUpdateBookDto>, //Used to create/update a book
        IBookAppService //implement the IBookAppService
    {
        private readonly IRepository<Author, Guid> _authorRepository;
        public BookAppService(IRepository<Book, Guid> repository,
            IRepository<Author, Guid> authorRepository)
            : base(repository)
        {
            _authorRepository = authorRepository;
            GetPolicyName = BookStorePermissions.Books.Default;
            GetListPolicyName = BookStorePermissions.Books.Default;
            CreatePolicyName = BookStorePermissions.Books.Create;
            UpdatePolicyName = BookStorePermissions.Books.Edit;
            DeletePolicyName = BookStorePermissions.Books.Delete;

        }
        public override async Task<BookDto> GetAsync(Guid id)
        {
            //Get the IQueryable<Book> from the repository
            var queryable = await Repository.GetQueryableAsync();
            
            //Prepare a query to join books and authors
            var query = from book in queryable
                        join author in await _authorRepository.GetQueryableAsync() on book.AuthorId equals author.Id
                        where book.Id == id
                        select new { book, author };

            //Execute the query and get the book with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Book), id);
            }

            var bookDto = ObjectMapper.Map<Book, BookDto>(queryResult.book);
            bookDto.Author = queryResult.author.Name;
            return bookDto;
        }
      
        public override async Task<PagedResultDto<BookDto>> GetListAsync(BookTitleSearchDto input)
        {

            var queryable = await base.Repository.GetQueryableAsync();
            var query = from book in queryable
                        join author in await _authorRepository.GetQueryableAsync() on book.AuthorId equals author.Id
                        select new { book, author };


            var filter = query.WhereIf(!string.IsNullOrWhiteSpace(input.BookName), book => book.book.Name.ToLower()
                .Contains(input.BookName.ToLower()))
                .OrderBy(x=>x.book.Name)
                .PageBy(input.SkipCount,input.MaxResultCount);

            var totalCount = await AsyncExecuter.CountAsync(filter);
            var books = await AsyncExecuter.ToListAsync(filter);

            //Convert the query result to a list of BookDto objects
            var bookDtos = books.Select(x =>
            {
                var bookDto = ObjectMapper.Map<Book, BookDto>(x.book);
                bookDto.Author = x.author.Name;
                return bookDto;
            }).ToList();

            return new PagedResultDto<BookDto>(
                totalCount,
                bookDtos
            );
        }
        
        public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
        {
            var authors = await _authorRepository.GetListAsync();

            return new ListResultDto<AuthorLookupDto>(
                ObjectMapper.Map<List<Author>, List<AuthorLookupDto>>(authors)
            );
        }
       
    }
}