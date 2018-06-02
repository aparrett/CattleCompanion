var CowDetails = function () {
    var cowId;

    var init = function () {
        cowId = $('#cow-id').attr('data-id');
        $('#addEvent').on('click', '.add-event', createCowEvent);
        $(document).on('click', '.delete-cow', showDeleteConfirmation);
    };

    var createCowEvent = function() {
        var eventId = $('#Events').val();
        var date = $('#CowEventDate').val();
        var description = $('#CowEventDescription').val();
        var event = $(`#Events option[value="${eventId}"]`).text();

        $.post("/api/cowEvents/", { cowId: cowId, eventId: eventId, date: date, description: description })
            .done(function(cowEvent) {
                if ($('.empty-events').length)
                    $('.empty-events').remove();

                var date = new Date(cowEvent.date);
                date = date.toLocaleDateString("en-US");

                var html = `<li class="list-group-item list-group-item-action flex-column align-items-start">
                                <div class="d-flex w-100 justify-content-between">
                                    <h5 class="mb-1">${event}</h5>
                                    <small>${date}</small>
                                </div>
                                <p class="mb-1">${cowEvent.description}</p>
                            </li>`;

                $('#cow-events .list-group').append(html);
                $('#addEvent').modal('hide');

                resetCowEventModal();
            }).fail(function(response) {
                alert(response.responseJSON.message);
            });
    };

    var showDeleteConfirmation = function(e) {
        e.preventDefault();
        $('#deleteConfirmation').modal('show');
    };

    var resetCowEventModal = function() {
        $('[name="Events"], #CowEventDate, #CowEventDescription').val("");
    };

    return {
        init: init
    }
}();