
















$(document).ready(function () {
    var like = {
        userId: $("#userId").val(),
        postId: $("#postId").val()
    }
    $('.postContainer').on('submit', '#likeForm', function () {
        $.ajax({
            type: "POST",
            url: '@Url.Action("AddLikeToPost","LoggedIn")',
            contentType: "application/json; charset=utf8",
            data: JSON.stringify(like),
            dataType: "json",
            success: function (data) {
                               
            },
            error: function () {
                alert("LIKE FAILED");
            }
        })
    })
});