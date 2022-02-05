using Acme.BookStore.Attachments;
using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using AutoMapper;
using static Acme.BookStore.Web.Pages.Attachments.CreateModal;
using static Acme.BookStore.Web.Pages.Attachments.EditModal;
using static Acme.BookStore.Web.Pages.Books.CreateModal;
using static Acme.BookStore.Web.Pages.Books.EditModal;
//using static Acme.BookStore.Web.Pages.Authors.CreateModal;
//using static Acme.BookStore.Web.Pages.Authors.EditModal;

namespace Acme.BookStore.Web;

public class BookStoreWebAutoMapperProfile : Profile
{
    public BookStoreWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<Pages.Books.CreateModalModel.CreateBookViewModel, CreateUpdateBookDto>();
        CreateMap<BookDto, Pages.Books.EditModalModel.EditBookViewModel>();
        CreateMap<Pages.Books.EditModalModel.EditBookViewModel, CreateUpdateBookDto>();
        CreateMap<CreateAuthorViewModel, CreateUpdateAuthorDto>();
        CreateMap<AuthorDto, EditAuthorViewModel>();
        CreateMap<EditAuthorViewModel, CreateUpdateAuthorDto>();
         CreateMap<CreateAttachmentViewModel, CreateUpdateAttachmentDto>();
        CreateMap<AttachmentDto, EditAttachmentViewModel>();
        CreateMap<EditAttachmentViewModel, CreateUpdateAttachmentDto>();

    }
}
