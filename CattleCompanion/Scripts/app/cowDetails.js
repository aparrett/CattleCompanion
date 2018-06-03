var CowDetails = function () {
    var cowId;

    var init = function () {
        cowId = $('#cow-id').attr('data-id');

        addEventHandlers();
    };

    var addEventHandlers = function() {
        $('#addEvent').on('click', '.add-event', createCowEvent);
        $(document).on('click', '.delete-cow', showDeleteConfirmation);

        $(document).on('click', '.add-mother', showAddMother);
        $(document).on('click', '.cancel-add-mother', cancelAddMother);
        $(document).on('click', '.save-mother', saveMother);

        $(document).on('click', '.add-father', showAddFather);
        $(document).on('click', '.cancel-add-father', cancelAddFather);
        $(document).on('click', '.save-father', saveFather);

        $(document).on('click', '.add-child', showAddChild);
        $(document).on('click', '.cancel-add-child', cancelAddChild);
        $(document).on('click', '.save-child', saveChild);
    };

    var createCowEvent = function() {
        var eventId = $('#Events').val();
        var date = $('#CowEventDate').val();
        var description = $('#CowEventDescription').val();
        var event = $(`#Events option[value="${eventId}"]`).text();

        $.post("/api/cowEvents/", { cowId: cowId, eventId: eventId, date: date, description: description })
            .done(function(cowEvent) {
                var date = new Date(cowEvent.date);
                date = date.toLocaleDateString("en-US");

                var html = `<li class="list-group-item list-group-item-action flex-column align-items-start">
                                <div class="d-flex w-100 justify-content-between">
                                    <h5 class="mb-1">${event}</h5>
                                    <small>${date}</small>
                                </div>
                                <p class="mb-1">${cowEvent.description}</p>
                            </li>`;

                if ($('.empty-events').length) {
                    $('.empty-events').remove();
                    $('#cow-events').append("<ul class='list-group'></ul>");
                }
                    
                $('#cow-events .list-group').append(html);
                $('#addEvent').modal('hide');

                resetCowEventModal();
            }).fail(function(response) {
                alert(response.responseJSON.message);
            });
    };

    var resetCowEventModal = function () {
        $('[name="Events"], #CowEventDate, #CowEventDescription').val("");
    };

    var showDeleteConfirmation = function(e) {
        e.preventDefault();
        $('#deleteConfirmation').modal('show');
    };

    var showAddMother = function () {
        if ($('#mothers option').length > 0) {
            $('.add-mother').addClass('d-none');
            $('.add-mother-menu').removeClass('d-none');
        } else {
            $('#newCowLabel').text("There are no available mothers. Would you like to add a new cow?");
            $('#newCow').modal('show');
        }
    };

    var cancelAddMother = function () {
        $('.add-mother').removeClass('d-none');
        $('.add-mother-menu').addClass('d-none');
    };

    var saveMother = function () {
        var id = $('#mothers').val();
        $.post("/api/cattle/" + cowId, { motherId: id })
            .done(displayMother)
            .fail(function () {
                showAlert("We're sorry, we were unable to add the selected mother. Please try again later.");
            });
    };

    var displayMother = function (cow) {
        var html = `<p class="list-group-item">
                        <a href="/cattle/details/${cow.motherId}">
                            ${cow.mother.givenId}
                        </a> - Mother
                    </p>`;
        $('.add-mother').before(html);
        $('.add-mother, .add-mother-menu').remove();
    };

    var showAddFather = function () {
        if ($('#fathers option').length > 0) {
            $('.add-father').addClass('d-none');
            $('.add-father-menu').removeClass('d-none');
        } else {
            $('#newCowLabel').text("There are no available fathers. Would you like to add a new cow?");
            $('#newCow').modal('show');
        }
    };

    var cancelAddFather = function() {
        $('.add-father').removeClass('d-none');
        $('.add-father-menu').addClass('d-none');
    };



    var saveFather = function() {
        var id = $('#fathers').val();
        $.post("/api/cattle/" + cowId, { fatherId: id })
            .done(displayFather)
            .fail(function () {
                showAlert("We're sorry, we were unable to add the selected father. Please try again later.");
            });
    };

    var displayFather = function(cow) {
        var html = `<p class="list-group-item">
                        <a href="/cattle/details/${cow.fatherId}">
                            ${cow.father.givenId}
                        </a> - Father
                    </p>`;
        $('.add-father').before(html);
        $('.add-father, .add-father-menu').remove();
    };

    var showAddChild = function () {
        if ($('#children option').length > 0) {
            $('.add-child').addClass('d-none');
            $('.add-child-menu').removeClass('d-none');
        } else {
            $('#newCowLabel').text("There are no available children. Would you like to add a new cow?");
            $('#newCow').modal('show');
        }
    };

    var cancelAddChild = function () {
        $('.add-child').removeClass('d-none');
        $('.add-child-menu').addClass('d-none');
    };



    var saveChild = function () {
        var id = $('#children').val();
        $.post("/api/cattle/" + cowId, { childId: id })
            .done(displayChild)
            .fail(function () {
                showAlert("We're sorry, we were unable to add the selected child. Please try again later.");
            });
    };

    var displayChild = function (cow) {
        $(`#children option[value="${cow.id}"]`).remove();
        var html = `<li class="list-group-item">
                        <a href="/cattle/details/${cow.id}">
                            ${cow.givenId}
                        </a>
                    </li>`;

        if ($('#children-container ul').length === 0) {
            $('#empty-children').remove();
            $('#children-container h5').after('<ul class="list-group mb-3"></ul>');
        }

        $('#children-container ul').append(html);
        $('.add-child-menu').addClass('d-none');
        $('.add-child').removeClass('d-none');
    };

    var showAlert = function (text) {
        $('#alertLabel').text(text);
        $('#alert').modal('show');
    };

    return {
        init: init
    }
}();