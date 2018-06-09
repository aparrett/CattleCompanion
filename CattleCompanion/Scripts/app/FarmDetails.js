var FarmDetails = function() {
    var init = function() {
        $(document).on('click', '.delete-farm', showDeleteConfirmation);
    };

    var showDeleteConfirmation = function(e) {
        e.preventDefault();
        $('#deleteConfirmation').modal('show');
    };

    return {
        init: init
    };
}();