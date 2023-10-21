

//Xác nhận  hóa đơn đang chờ
$(document).ready(function () {
    $('body').on('click', '#btn-confirm-order', function () {
        var id = $(this).data('id'); // Lấy ID sản phẩm từ thuộc tính data
        $.ajax({
            url: '@Url.Action("ConfirmOrder", "Orders")',
            type: 'POST', // Sử dụng phương thức POST để truyền ID sản phẩm
            data: { idOrder: id }, // Truyền ID sản phẩm
            success: function (data) {
                if (data.success) { 
                    alert('Đơn hàng đã được xác nhận.');
                }
            },
            error: function () {
                alert("Có lỗi xảy ra!")
            },
        });
    });
});

//Không chấp nhận bán hàng
$(document).ready(function () {
    $('body').on('click', '#btn-cancel-order', function () {
        var id = $(this).data('id'); // Lấy ID sản phẩm từ thuộc tính data
        $.ajax({
            url: '@Url.Action("ConfirmOrder", "Orders")',
            type: 'POST', // Sử dụng phương thức POST để truyền ID sản phẩm
            data: { idOrder: id }, // Truyền ID sản phẩm
            success: function (data) {
                if (data.success) {
                    $('#trow_' + id).remove();
                    alert('Đơn hàng đã được xác nhận.');
                }
            },
            error: function () {
                // Xử lý lỗi nếu có
            },
        });
    });
});