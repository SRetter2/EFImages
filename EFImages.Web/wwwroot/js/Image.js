$(() => {
    setInterval(() => {
        const id = $("#like-btn").val();
        $.get('/home/numberoflikes', { id }, function (amount) {
            $("#number-of-likes").text(amount);
        });
    }, 1000)


    $("#like-btn").on('click', () => {
        const id = $("#like-btn").val();
        $.post('/home/like', { id }, function(image) {
            $("#like-btn").prop("disabled", true);
        });
    });
});