function createDataTables() {
    $('.bs-datatable:not(.added)').DataTable({
        "pageLength": 5,
        "lengthMenu": [[5, 10, 25, -1], [5, 10, 25, 'Prikaži sve']],
        "language": {
            "lengthMenu": "Prikaži _MENU_ zapisa po stranici",
            "zeroRecords": "Izvinjavamo se, ne postoje traženi podaci",
            "info": "Stranica _PAGE_ od ukupno _PAGES_",
            "infoEmpty": "0 pronađeno",
            "infoFiltered": "(od ukupno _MAX_ zapisa)",
            "search": "Pretraga:",
            "paginate": {
                "first": "Prvi",
                "last": "Posljednji",
                "next": "Sljedeći",
                "previous": "Prethodni"
            },
        }
    });
    $('.bs-datatable').addClass('added');
}

function addHandlers() {
    $('.divRow .closeDivRow').off().click(function () {
        $(this).closest('.divRow').prev('tr').css('background', 'transparent');
        $(this).closest('.divRow').remove();
    });
}

$(document).ready(function () {

   // Convert these to Boostrap datatables
    createDataTables();

   // Othe handlers
    addHandlers();

});

$(document).ajaxComplete(function () {

    // Convert these to Boostrap datatables
    createDataTables();

    // Othe handlers
    addHandlers();
});