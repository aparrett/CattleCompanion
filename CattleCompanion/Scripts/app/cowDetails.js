﻿var CowDetails = function () {
    var cowId;
    var childId;

    var init = function () {
        cowId = $('#cow-id').attr('data-id');

        addEventHandlers();
    };

    var addEventHandlers = function() {
        $(document).on('click', '.add-event', createCowEvent);
        $(document).on('click', '.delete-cow', showDeleteConfirmation);

        $(document).on('click', '.add-mother', showAddMother);
        $(document).on('click', '.cancel-add-mother', cancelAddMother);
        $(document).on('click', '.save-mother', saveMother);
        $(document).on('click', '.remove-mother-confirm', showRemoveMotherConfirmation);
        $(document).on('click', '.remove-mother', removeMother);

        $(document).on('click', '.add-father', showAddFather);
        $(document).on('click', '.cancel-add-father', cancelAddFather);
        $(document).on('click', '.save-father', saveFather);
        $(document).on('click', '.remove-father-confirm', showRemoveFatherConfirmation);
        $(document).on('click', '.remove-father', removeFather);

        $(document).on('click', '.add-child', showAddChild);
        $(document).on('click', '.cancel-add-child', cancelAddChild);
        $(document).on('click', '.save-child', saveChild);
        $(document).on('click', '.remove-child-confirm', showRemoveChildConfirmation);
        $(document).on('click', '.remove-child', removeChild);

        $(document).on('click', '.remove-no', cancelRemoveConfirmation);
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

                var element = `<li class="list-group-item list-group-item-action flex-column align-items-start">
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
                    
                $('#cow-events .list-group').append(element);
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
        $.ajax({
            type: "PUT",
            url: "/api/cattle/" + cowId, 
            data: { motherId: id }
        })
        .done(displayMother)
        .fail(function () {
            showAlert("We're sorry, we were unable to add the selected mother. Please try again later.");
        });
    };

    var displayMother = function (cow) {
        var element = `<p id="mother" class="list-group-item d-flex" data-id="${cow.motherId}">
                            <a class="given-id" href="/cattle/details/${cow.motherId}">
                                ${cow.mother.givenId}
                            </a>
                            <span>&nbsp;- Mother</span>
                            <small class="remove-item remove-mother-confirm">Remove</small>
                        </p>`;
        $('.add-mother').before(element);
        $('.add-mother, .add-mother-menu').addClass("d-none");
        getSiblings();
    };

    var showRemoveMotherConfirmation = function () {
        var givenId = $('#mother .given-id').text();
        var label = `Are you sure you want to remove ${givenId} as the mother?`;
        $('#removeConfirmationLabel').text(label);
        $('#removeConfirmation .remove-yes').addClass('remove-mother');
        $('#removeConfirmation').modal('show');
    };

    var removeMother = function() {
        $.ajax({
            type: "PUT",
            url: '/api/cattle/' + cowId,
            data: { motherId: 0 }
        })
            .done(function () {
                var motherEl = $('#mother');
                var motherId = motherEl.attr('data-id');
                motherEl.remove();
                $('.add-mother').removeClass('d-none');
                $('#removeConfirmation .remove-yes').removeClass('remove-mother');
                $('#removeConfirmation').modal('hide');
                removeSiblings(motherId);
            })
            .fail(function() {
                var message = $('#mother a').text() + " could not be removed. Please try again later";
                showAlert(message);
            });
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
        $.ajax({
            type: "PUT",
            url: "/api/cattle/" + cowId, 
            data: { fatherId: id }
        })
        .done(displayFather)
        .fail(function () {
            showAlert("We're sorry, we were unable to add the selected father. Please try again later.");
        });
    };

    var displayFather = function(cow) {
        var element = `<p id="father" class="list-group-item d-flex" data-id="${cow.fatherId}">
                            <a class="given-id" href="/cattle/details/${cow.fatherId}">
                                ${cow.father.givenId}
                            </a>
                            <span>&nbsp;- Father</span>
                            <small class="remove-item remove-father-confirm">Remove</small>
                        </p>`;
        $('.add-father').before(element);
        $('.add-father, .add-father-menu').addClass('d-none');
        getSiblings();
    };

    var showRemoveFatherConfirmation = function () {
        var givenId = $('#father .given-id').text();
        var label = `Are you sure you want to remove ${givenId} as the father?`;
        $('#removeConfirmationLabel').text(label);
        $('#removeConfirmation .remove-yes').addClass('remove-father');
        $('#removeConfirmation').modal('show');
    };

    var cancelRemoveConfirmation = function() {
        $('#removeConfirmation .remove-yes')
            .removeClass('remove-father')
            .removeClass('remove-mother')
            .removeClass('remove-child');
    };

    var removeFather = function () {
        $.ajax({
            type: "PUT",
            url: '/api/cattle/' + cowId,
            data: { fatherId: 0 }
        })
            .done(function () {
                var fatherEl = $('#father');
                var fatherId = fatherEl.attr('data-id');
                fatherEl.remove();
                $('.add-father').removeClass('d-none');
                $('#removeConfirmation .remove-yes').removeClass('remove-father');
                $('#removeConfirmation').modal('hide');
                removeSiblings(fatherId);
            })
            .fail(function () {
                var message = $('#father a').text() + " could not be removed. Please try again later";
                showAlert(message);
            });
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
        $.ajax({
            type: "PUT",
            url: "/api/cattle/" + cowId,
            data: { childId: id }
        })
        .done(displayChild)
        .fail(function () {
            showAlert("We're sorry, we were unable to add the selected child. Please try again later.");
        });
    };

    var displayChild = function (cow) {
        $(`#children option[value="${cow.id}"]`).remove();
        var element = `<li class="list-group-item d-flex" data-id="${cow.id}">
                            <a href="/cattle/details/${cow.id}" class="given-id">
                                ${cow.givenId}
                            </a>
                            <small class="remove-item remove-child-confirm">Remove</small>
                        </li>`;

        if ($('#children-container ul').length === 0) {
            $('#empty-children').remove();
            $('#children-container h5').after('<ul class="list-group mb-3"></ul>');
        }

        $('#children-container ul').append(element);
        $('.add-child-menu').addClass('d-none');
        $('.add-child').removeClass('d-none');
    };

    var showRemoveChildConfirmation = function (e) {
        var cowEl = $(e.target).parent();
        childId = cowEl.attr('data-id');
        var givenId = cowEl.children('.given-id').text();
        var label = `Are you sure you want to remove child ${givenId}?`;
        $('#removeConfirmationLabel').text(label);
        $('#removeConfirmation .remove-yes').addClass('remove-child');
        $('#removeConfirmation').modal('show');
    };

    var removeChild = function () {
        var givenId = $('#children-container .list-group-item[data-id="' + childId + '"] a.given-id').text();
        $.ajax({
            type: "PUT",
            url: '/api/cattle/' + childId,
            data: { parentId: cowId }
        })
        .done(function () {
            $('#children-container .list-group-item[data-id="' + childId + '"]').remove();
            $('#removeConfirmation .remove-yes').removeClass('remove-child');
            $('#removeConfirmation').modal('hide');
            $('#children').append('<option value="' + childId + '">' + givenId + '</option>');
        })
        .fail(function () {
            var message = givenId + " could not be removed. Please try again later";
            showAlert(message);
        });
    };


    var getSiblings = function() {
        $.get("/api/cattle/" + cowId + "/siblings")
            .done(showNewSiblings)
            .fail(function() {
                showAlert("Something went wrong and we were unable to retrieve the new siblings.");
            });
    };

    var removeSiblings = function(parentId) {
        $('#siblings .list-group-item').each(function (i, el) {
            var $el = $(el);
            if ($el.attr('data-father-id') == parentId || $el.attr('data-mother-id') == parentId)
                $el.remove();
        });
    };

    var showNewSiblings = function(siblingsFromDb) {
        var $emptySiblings = $('#empty-siblings');
        var siblingsGroup = `<ul class="list-group my-3"></ul>`;

        if ($emptySiblings.length > 0) {
            $emptySiblings.after(siblingsGroup);
            $emptySiblings.remove();
        }

        var siblingsInDom = [];
        $('#siblings li.list-group-item').each(function(i, el) {
            siblingsInDom.push(parseInt($(el).attr('data-id')));
        });

        // Could also remove all siblings in DOM and just add all siblings
        // from response but add them this way makes it more obvious which
        // siblings are new.
        siblingsFromDb.forEach(function(sibling) {
            if (!siblingsInDom.includes(sibling.id)) {
                var element = `<li class="list-group-item" 
                                    data-id="${sibling.id}" 
                                    data-mother-id="${sibling.motherId}" 
                                    data-father-id="${sibling.fatherId}">
                                    <a href="/cattle/details/${sibling.id}">${sibling.givenId}</a>
                                </li>`;
                $('#siblings ul').append(element);
            }
        });
    };

    var showAlert = function (text) {
        $('#alertLabel').text(text);
        $('#alert').modal('show');
    };

    return { init: init };
}();