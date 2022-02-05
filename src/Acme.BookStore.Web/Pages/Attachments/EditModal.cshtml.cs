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
    public class EditModal : BookStorePageModel
    {
        [BindProperty]
        public EditAttachmentViewModel Attachment { get; set; }

        public List<SelectListItem> Books { get; set; }

        private readonly IAttachmentAppService _attachmentAppService;

        public EditModal(
            IAttachmentAppService attachmentAppService)
        {
            _attachmentAppService = attachmentAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            Attachment = new EditAttachmentViewModel();

            var attachmentdto = await _attachmentAppService.GetAsync(id);
            Attachment = ObjectMapper.Map<AttachmentDto, EditAttachmentViewModel>(attachmentdto);
                        var BooksLookup = await _attachmentAppService.GetBookLookupAsync();
            Books = BooksLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _attachmentAppService.UpdateAsync(
                Attachment.Id,
                ObjectMapper.Map<EditAttachmentViewModel, CreateUpdateAttachmentDto>(Attachment)
                );
            return NoContent();
        }

        public class EditAttachmentViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }


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
