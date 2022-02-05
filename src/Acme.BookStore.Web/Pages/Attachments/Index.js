$(function () {
    var l = abp.localization.getResource('BookStore');
    var createModal = new abp.ModalManager(abp.appPath + 'Attachments/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Attachments/EditModal');

    var dataTable = $('#AttachmentsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(acme.bookStore.attachments.attachment.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('BookStore.Attachments.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                }
                                , {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('BookStore.Attachments.Delete'),

                                    confirmMessage: function (data) {
                                        return l('AttachmentDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        acme.bookStore.attachments.attachment
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Title'),
                    data: "title"
                },
                {
                    title: l('Link'),
                    data: "link"
                },
                {
                    title: l('Description'),
                    data: "description",
                   
                },
                {
                    title: l('Book'),
                    data: "book"
                },
                {
                    title: l('CreationTime'), data: "creationTime",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                    }
                }
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
        abp.notify.info(l('SuccessfullyAdded'));

    });
    editModal.onResult(function () {
        dataTable.ajax.reload();
        abp.notify.info(l('SuccessfullyEdited'));
    });

    $('#NewAttachmentButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
