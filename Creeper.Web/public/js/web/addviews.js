$(document).ready(function () {
    $.ajax({
        url: "/views",
        type: "post",
        data: { type: "website" },
        dataType: "json",
        success: function (data) {
            if (data.Ret != 1) {
                toastrShow(data.Ret, data.Message);
            }
        }
    });
});