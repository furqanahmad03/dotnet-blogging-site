$(document).ready(function () {
    let table = $('#myTable').DataTable();

    $('.dashboardButton').click(function (e) {
    e.preventDefault();
    var target = $(this).data('target');
    $('#adminTable').load('/Admin/LoadSection?section=' + target, function (response, status, xhr) {
        if (status === 'error') {
        console.log("Error loading content:", xhr.status, xhr.statusText);
        } else {
        table.destroy();
        table = $('#myTable').DataTable();
        }
    });

    $('.dashboardButton').removeClass('dashboardActiveButton');
    $(this).addClass('dashboardActiveButton');
    });

    $('.refreshButton').click(function (e) {
    e.preventDefault();
    $('#adminTable').empty();
    $('.dashboardButton').removeClass('dashboardActiveButton');
    });


});