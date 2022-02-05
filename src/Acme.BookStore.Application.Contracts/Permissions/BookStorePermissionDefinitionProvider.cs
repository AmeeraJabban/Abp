using Acme.BookStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Acme.BookStore.Permissions;

public class BookStorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var bookStoreGroup = context.AddGroup(BookStorePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(BookStorePermissions.MyPermission1, L("Permission:MyPermission1"));


        var booksPermission = bookStoreGroup.AddPermission(BookStorePermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(BookStorePermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(BookStorePermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(BookStorePermissions.Books.Delete, L("Permission:Books.Delete"));


        var authorsPermission = bookStoreGroup.AddPermission(BookStorePermissions.Authors.Default, L("Permission:Authors"));
        authorsPermission.AddChild(BookStorePermissions.Authors.Create, L("Permission:Authors.Create"));
        authorsPermission.AddChild(BookStorePermissions.Authors.Edit, L("Permission:Authors.Edit"));
        authorsPermission.AddChild(BookStorePermissions.Authors.Delete, L("Permission:Authors.Delete"));


        var attachmentsPermission = bookStoreGroup.AddPermission(BookStorePermissions.Attachments.Default, L("Permission:Attachments"));
        attachmentsPermission.AddChild(BookStorePermissions.Attachments.Create, L("Permission:Attachments.Create"));
        attachmentsPermission.AddChild(BookStorePermissions.Attachments.Edit, L("Permission:Attachments.Edit"));
        attachmentsPermission.AddChild(BookStorePermissions.Attachments.Delete, L("Permission:Attachments.Delete"));



    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookStoreResource>(name);
    }
}
