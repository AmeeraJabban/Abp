using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Attachments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Acme.BookStore.Web.Pages.Attachments
{
    public class CreateModal : BookStorePageModel
    {
        [BindProperty]
        public CreateAttachmentViewModel Attachment { get; set; }

        public List<SelectListItem> Books { get; set; }

        private readonly IAttachmentAppService _attachmentAppService;

        public CreateModal(
            IAttachmentAppService attachmentAppService)
        {
            _attachmentAppService = attachmentAppService;
        }

        public async Task OnGetAsync()
        {
            Attachment = new CreateAttachmentViewModel();

            var BooksLookup = await _attachmentAppService.GetBookLookupAsync();
            Books = BooksLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _attachmentAppService.CreateAsync(
                ObjectMapper.Map<CreateAttachmentViewModel, CreateUpdateAttachmentDto>(Attachment)
                );
            return NoContent();
        }

        public class CreateAttachmentViewModel
        {
            [SelectItems(nameof(Books))]
            [DisplayName("Book")]
            public Guid BookId { get; set; }

            [Required]
            [StringLength(128)]
            public string Title { get; set; }
            [Required]

            public string Description { get; set; }
            [Required]

            public string Link { get; set; }
        }
    }
}
