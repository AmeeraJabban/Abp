using Acme.BookStore.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Authors
{
    public class AuthorAppService:
        CrudAppService<
            Author,
            AuthorDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateAuthorDto
            >, IAuthorAppService
    {
        public AuthorAppService(IRepository<Author,Guid> repository)
            :base(repository) {
            GetPolicyName = BookStorePermissions.Authors.Default;
            GetListPolicyName = BookStorePermissions.Authors.Default;
            CreatePolicyName = BookStorePermissions.Authors.Create;
            UpdatePolicyName = BookStorePermissions.Authors.Edit;
            DeletePolicyName = BookStorePermissions.Authors.Delete;

        }
    }
}
