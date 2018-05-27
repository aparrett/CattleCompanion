var CowDetails = function () {
    var cowId;

    var init = function (id) {
        cowId = $('#given-id').attr('data-id');
        $('#addEvent').on('click', '.add-event', createCowEvent);
    };

    var createCowEvent = function (e) {
        var eventId = $('#Events').val();
        var date = $('#Date').val();

        $.post("/api/cowEvents/", { cowId: cowId, eventId: eventId, date: date })
            .done(function () {
                alert('The event was successfully added to the cow');
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