var CowDetails = function () {
    var cowId;
    var childId;
    var RelationshipType = {
        Mother: 0,
        Father: 1
    };

    var init = function () {
        cowId = $('#cow-id').attr('data-id');

        addEventHandlers();
    };

    var addEventHandlers = function() {
        $('#addEvent').on('click', '.add-event', createCowEvent);
        $('#cow-id').on('click', '.delete-cow', showDeleteConfirmation);

        $('#parents-container').on('click', '.add-mother', showAddMother);
        $('#parents-container').on('click', '.cancel-add-mother', cancelAddMother);
        $('#parents-container').on('click', '.save-mother', saveMother);
        $('#parents-container').on('click', '.remove-mother-confirm', showRemoveMotherConfirmation);
        $('#removeConfirmation').on('click', '.remove-mother', removeMother);

        $('#parents-container').on('click', '.add-father', showAddFather);
        $('#parents-container').on('click', '.cancel-add-father', cancelAddFather);
        $('#parents-container').on('click', '.save-father', saveFather);
        $('#parents-container').on('click', '.remove-father-confirm', showRemoveFatherConfirmation);
        $('#removeConfirmation').on('click', '.remove-father', removeFather);

        $('#children-container').on('click', '.add-child', showAddChild);
        $('#children-container').on('click', '.cancel-add-child', cancelAddChild);
        $('#children-container').on('click', '.save-child', saveChild);
        $('#children-container').on('click', '.remove-child-confirm', showRemoveChildConfirmation);
        $('#removeConfirmation').on('click', '.remove-child', removeChild);

        $('#removeConfirmation').on('click', '.remove-no', cancelRemoveConfirmation);
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
        var givenId = $(`#mothers [value="${id}"`).text();

        $.post("/api/relationships", { cow1Id: id, cow2Id: cowId, type: RelationshipType.Mother })
            .done(relationship => displayMother(relationship, givenId))
            .fail(function () {
                showAlert("We're sorry, we were unable to add the selected mother. Please try again later.");
            });
    };

    var displayMother = function (relationship, givenId) {
        var element = `<p id="mother" class="list-group-item d-flex" data-id="${relationship.cow1Id}">
                            <a class="given-id" href="/cattle/${relationship.cow1Id}">
                                ${givenId}
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
            type: "DELETE",
            url: '/api/relationships/' + $('#mother').attr('data-id') + "?cow2Id=" + cowId
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
        var givenId = $(`#fathers [value="${id}"`).text();

        $.post("/api/relationships", { cow1Id: id, cow2Id: cowId, type: RelationshipType.Father })
            .done(relationship => displayFather(relationship, givenId))
            .fail(function () {
                showAlert("We're sorry, we were unable to add the selected father. Please try again later.");
            });
    };

    var displayFather = function (relationship, givenId) {
        var element = `<p id="father" class="list-group-item d-flex" data-id="${relationship.cow1Id}">
                            <a class="given-id" href="/cattle/${relationship.cow1Id}">
                                ${givenId}
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
            type: "DELETE",
            url: '/api/relationships/' + $('#father').attr('data-id') + "?cow2Id=" + cowId
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
        var givenId = $(`#children [value="${id}"`).text();
        var type = $("#cow-id").attr('data-gender') === "M" ? RelationshipType.Father : RelationshipType.Mother;

        $.post("/api/relationships", { cow1Id: cowId, cow2Id: id, type })
            .done(() => displayChild(id, givenId))
            .fail(function () {
                showAlert("We're sorry, we were unable to add the selected child. Please try again later.");
            });
    };

    var displayChild = function (id, givenId) {
        $(`#children option[value="${id}"]`).remove();
        var element = `<li class="list-group-item d-flex" data-id="${id}">
                            <a href="/cattle/${id}" class="given-id">
                                ${givenId}
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
            type: "DELETE",
            url: '/api/relationships/' + cowId + "?cow2Id=" + childId
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
            if ($el.attr('data-father-id') === parentId || $el.attr('data-mother-id') === parentId)
                $el.remove();
        });
    };

    var showNewSiblings = function (siblingsFromDb) {
        window.siblingsFromDb = siblingsFromDb;
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
        siblingsFromDb.forEach(function (sibling) {
            if (!siblingsInDom.includes(sibling.id)) {
                var motherRelationship = sibling.parentRelationships.find(r => r.type === RelationshipType.Mother);
                var motherIdAttr = "";

                if (motherRelationship) {
                    motherIdAttr = ` data-mother-id="${motherRelationship.cow1Id}" `;
                }

                var fatherRelationship = sibling.parentRelationships.find(r => r.type === RelationshipType.Father);
                var fatherIdAttr = "";

                if (fatherRelationship) {
                    fatherIdAttr = ` data-father-id="${fatherRelationship.cow1Id}" `;
                }

                var element = `<li class="list-group-item" data-id="${sibling.id}" ${motherIdAttr} ${fatherIdAttr}>
                                    <a href="/cattle/${sibling.id}">${sibling.givenId}</a>
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