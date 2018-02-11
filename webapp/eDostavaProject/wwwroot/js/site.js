// Write your JavaScript code.
jQuery(document).ready(function ($) {

    // Dodavanjem klase 'row-link-clickable' i atributa 'data-href', pravi linkove od cijelog jednog <tr> reda u tabelama
    // nrp. data-href="/Restorani/Jelovnik=restoranid=1"
    $(".row-link-clickable").click(function () {
        window.location = $(this).data("href");
    });

});