$(function ()
{
    $('body').on('submit', "#likeForm", function () {
        
        var theForm = $(this);

        $.ajax({
            type: 'POST',
            url: theForm.attr('action'),
            data: theForm.serialize(),
        }).done(function (result) {
            
            
        })
    })
})