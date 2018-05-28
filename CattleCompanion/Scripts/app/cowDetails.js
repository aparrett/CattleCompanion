var CowDetails = function () {
    var cowId;

    var init = function (id) {
        cowId = $('#given-id').attr('data-id');
        $('#addEvent').on('click', '.add-event', createCowEvent);
    };

    var createCowEvent = function (e) {
        var eventId = $('#Events').val();
        var date = $('#Date').val();
        var event = $(`#Events option[value="${eventId}"]`).text();

        $.post("/api/cowEvents/", { cowId: cowId, eventId: eventId, date: date })
            .done(function () {
                if ($('.empty-events').length)
                    $('.empty-events').remove();

                var html = `<h4>${event} - ${date}<h4>
                            <p>This is a test description</p>`;

                $('#cow-events').append(html);
            }).fail(function () {
                alert("Sorry, the event could not be added at this time.");
            }).always(function () {
                $('#addEvent').modal('hide');
            });
    }

    return {
        init: init
    }
}();