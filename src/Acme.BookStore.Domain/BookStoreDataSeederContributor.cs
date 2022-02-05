using Acme.BookStore.Attachments;
using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore
{
    public class BookStoreDataSeederContributor
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IRepository<Author, Guid> _authorRepository;
        private readonly IRepository<Attachment, Guid> _attachmentRepository;

        public BookStoreDataSeederContributor(IRepository<Book, Guid> bookRepository,
            IRepository<Author, Guid> authorRepository,
            IRepository<Attachment, Guid> attachmentRepository
            )
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _attachmentRepository = attachmentRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _bookRepository.GetCountAsync() > 0)
            {
                return;
            }

            
           var firstAuthor =  await _authorRepository.InsertAsync(
                    new Author
                    {
                        Name = "Anas",
                        ShortBio = "Hello",
                        BirthDate = new DateTime(1949, 6, 8),
                    },
                    autoSave: true
                );

              var secondAuthor=  await _authorRepository.InsertAsync(
                    new Author
                    {
                        Name = "Karam",
                        ShortBio = "Hi",
                        BirthDate = new DateTime(1949, 6, 8),
                    },
                    autoSave: true
                );
            
           
            var firstBook = await _bookRepository.InsertAsync(
                    new Book
                    {
                        Name = "1984",
                        Type = BookType.Dystopia,
                        PublishDate = new DateTime(1949, 6, 8),
                        Price = 19.84f,
                        AuthorId = firstAuthor.Id
                    },
                    autoSave: true
                );

              var secondBook =  await _bookRepository.InsertAsync(
                    new Book
                    {
                        Name = "The Hitchhiker's Guide to the Galaxy",
                        Type = BookType.ScienceFiction,
                        PublishDate = new DateTime(1995, 9, 27),
                        Price = 42.0f,
                        AuthorId = secondAuthor.Id
                    },
                    autoSave: true
                );

            await _attachmentRepository.InsertAsync(new Attachment { 
            BookId = firstBook.Id,
            Description= "Attach 1",
            Title = "Book 1 Attach",
            Link= "https://www.w3schools.com/"
            },
                                autoSave: true
);
            await _attachmentRepository.InsertAsync(new Attachment { 
            BookId = firstBook.Id,
            Description= "Attach 2",
            Title = "Book 1 Attach",
            Link= "https://www.google.com/"
            },
                                autoSave: true
);
            await _attachmentRepository.InsertAsync(new Attachment { 
            BookId = secondBook.Id,
            Description= "Attach 1",
            Title = "Book 2 Attach",
            Link= "https://www.w3schools.com/"
            }, autoSave: true
);
            await _attachmentRepository.InsertAsync(new Attachment { 
            BookId = secondBook.Id,
            Description= "Attach 2",
            Title = "Book 2 Attach",
            Link= "https://www.google.com/"
            }, autoSave: true
);
        }
    }
}
