$(function() {
    $("#PhoneNumber").blur(function() {
        App.blockUI({
            target: "body",
            boxed: true
        });
        $.post(baseUrl + "Customer/GetCustomerByPhone", { "phone": $(this).val() }, function (data) {
                if (data.success == true) {
                    $("#CustomerName").val(data.name);
                    $("#CustomerId").val(data.id);
                }
            }).done(function() {
            })
            .fail(function () {
                bootbox.alert("Đã có lỗi xảy ra vui lòng thử lại.");
            })
            .always(function() {
                App.unblockUI();
            });
    });
});