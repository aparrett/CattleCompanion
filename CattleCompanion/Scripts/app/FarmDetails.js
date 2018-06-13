const FarmDetails = function() {
    const init = function() {
        $(document).on('click', '.delete-farm', showDeleteConfirmation);
    };

    const showDeleteConfirmation = e => {
        e.preventDefault();
        $('#deleteConfirmation').modal('show');
    };

    return {
        init: init
    };
}();