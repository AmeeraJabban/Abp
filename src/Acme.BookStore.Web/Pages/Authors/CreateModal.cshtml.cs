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
    public class CreateModal : BookStorePageModel
    {
        [BindProperty]
        public CreateAuthorViewModel Author { get; set; }


        private readonly IAuthorAppService _authorAppService;

        public CreateModal(
            IAuthorAppService authorAppService)
        {
            _authorAppService = authorAppService;
        }

        public async Task OnGetAsync()
        {
            Author = new CreateAuthorViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _authorAppService.CreateAsync(
                ObjectMapper.Map<CreateAuthorViewModel, CreateUpdateAuthorDto>(Author)
                );
            return NoContent();
        }

        public class CreateAuthorViewModel
        {
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
