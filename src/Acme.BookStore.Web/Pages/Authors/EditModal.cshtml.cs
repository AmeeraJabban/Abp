using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Authors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Acme.BookStore.Web.Pages.Books
{
    public class EditModal : BookStorePageModel
    {
        [BindProperty]
        public EditAuthorViewModel Author { get; set; }


        private readonly IAuthorAppService _authorAppService;

        public EditModal(
            IAuthorAppService authorAppService)
        {
            _authorAppService = authorAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            var authorDto = await _authorAppService.GetAsync(id);
            Author = ObjectMapper.Map<AuthorDto, EditAuthorViewModel>(authorDto); 
        }
        public async Task<IActionResult> OnPostAsync()
        {

            await _authorAppService.UpdateAsync(
                Author.Id,
                ObjectMapper.Map<EditAuthorViewModel, CreateUpdateAuthorDto>(Author)
                );
            return NoContent();
        }

        public class EditAuthorViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }
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
}
