var CowDetails = function () {
    var cowId;

    var init = function () {
        cowId = $('#cow-id').attr('data-id');
        $('#addEvent').on('click', '.add-event', createCowEvent);
        $(document).on('click', '.delete-cow', showDeleteConfirmation);
    };

    var createCowEvent = function () {
        var eventId = $('#Events').val();
        var date = $('#Date').val();
        var description = $('#Description').val();
        var event = $(`#Events option[value="${eventId}"]`).text();

        $.post("/api/cowEvents/", { cowId: cowId, eventId: eventId, date: date, description: description })
            .done(function() {
                if ($('.empty-events').length)
                    $('.empty-events').remove();

                var html = `<h4>${event}  (${date})</h4>
                            <p>${description}</p>`;

                $('#cow-events').append(html);
                $('#addEvent').modal('hide');
            }).fail(function(response) {
                alert(response.responseJSON.message);
            });
    }

    var showDeleteConfirmation = function(e) {
        e.preventDefault();
        $('#deleteConfirmation').modal('show');
    };

    return {
        init: init
    }
}();