function DodajAjaxEvente() {
    // Onload ajax
    $("[ajax-onload='da']").each(function (el) {
        $(this).attr("ajax-onload", "dodan");

        var urlZaPoziv = $(this).attr("ajax-url");
        var divZaRezultat = $(this).attr("ajax-rezultat");

        $.get(urlZaPoziv, function (data, status) {
            $("#" + divZaRezultat).html(data);
        });
    });

    $("button[ajax-poziv='da']").click(function (event) {
        $(this).attr("ajax-poziv", "dodan");

        event.preventDefault();
        var urlZaPoziv = $(this).attr("ajax-url");
        var divZaRezultat = $(this).attr("ajax-rezultat");

        $.get(urlZaPoziv, function (data, status) {
            $("#" + divZaRezultat).html(data);

        });
    });


    $(document).ready(function () {
        $("#bt").click(function (event) {
            ("#val").html("Opcija dopustena samo moderatoru");

        });
    });

    $("[ajax-onchange='da']").off().on("change", function () {
        let self = $(this);
        var urlZaPoziv = self.attr("ajax-url");

        if (typeof self.attr("ajax-add-value-param") != 'undefined') {
            urlZaPoziv += "?" + self.attr("name") + "=" + self.val();
        }

        $.ajax({
            type: "GET",
            url: urlZaPoziv,
            async: true,
            success: function (data) {
                self.removeClass('is-invalid');
                self.addClass('is-valid');
                $("#" + self.attr('id') + "Help").addClass('invisible');
            },
            error: function (data) {
                self.removeClass('is-valid');
                self.addClass('is-invalid');
                $("#" + self.attr('id') + "Help").removeClass('invisible');
            }
        });
    });

    $("a[ajax-poziv='ajaxDa']").off().click(function (event) {
        $(this).attr("ajax-poziv", "dodan");
        event.preventDefault();
        var urlZaPoziv1 = $(this).attr("ajax-url");
        var urlZaPoziv2 = $(this).attr("href");
        var divZaRezultat = $(this).attr("ajax-rezultat");

        var urlZaPoziv;

        if (urlZaPoziv1 instanceof String)
            urlZaPoziv = urlZaPoziv1;
        else
            urlZaPoziv = urlZaPoziv2;

        // Takođe provjeri linkove sa alertify.js popup-om
        if (typeof $(this).attr('data-alertify') !== 'undefined') {
            console.log("YES!");
            let self = $(this);
            let msg = '';
            if (self.attr('data-alertify-text')) {
                msg += `<p> ${self.attr('data-alertify-text')} </p>`;
            }
            if (self.attr('data-alertify-btn-pre-text')) {
                msg += `<span> ${self.attr('data-alertify-btn-pre-text')} </span>`;
            }
            if (self.attr('data-alertify-btn-text')) {
                msg += `<a class="btn btn-sm btn-danger center" href="#">${self.attr('data-alertify-btn-text')}</a>`;
            }
            alertify.log(msg);
        }

        $.ajax({
            type: "GET",
            url: urlZaPoziv,
            async: true,
            success: function (data) {
                $("#" + divZaRezultat).html(data);
            }
        });


    });

    $("a[ajax-poziv='da']").click(function (event) {
        $(this).attr("ajax-poziv", "dodan");
        event.preventDefault();
        var urlZaPoziv1 = $(this).attr("ajax-url");
        var urlZaPoziv2 = $(this).attr("href");
        var divZaRezultat = $(this).attr("ajax-rezultat");

        var urlZaPoziv;

        if (urlZaPoziv1 instanceof String)
            urlZaPoziv = urlZaPoziv1;
        else
            urlZaPoziv = urlZaPoziv2;

        $.get(urlZaPoziv, function (data, status) {
            $("#" + divZaRezultat).html(data);
        });
    });

    $("form[ajax-poziv='da']").off().submit(function (event) {
        var form = $(this);
        var checkErrors = form.attr('check-for-errors-on-submit');

        event.preventDefault();

        var valid = true;
        if (typeof checkErrors !== typeof undefined && checkErrors !== false) {
            form.find('input').each(function () {
                if ($(this).hasClass('is-invalid')) {
                    // check for alertify message popups
                    if (typeof form.attr('data-alertify') !== 'undefined') {
                        let msg = '';
                        if (form.attr('data-alertify-error')) {
                            msg += `<span> ${form.attr('data-alertify-error')} </span>`;
                        }
                        alertify.log(msg);
                    }
                    valid = false;
                    return false;
                }
            });
        }
        if (valid) {
            form.attr("ajax-poziv", "dodan");
            event.preventDefault();
            var urlZaPoziv1 = form.attr("ajax-url");
            var urlZaPoziv2 = form.attr("action");
            var divZaRezultat = form.attr("ajax-rezultat");

            var urlZaPoziv;
            if (urlZaPoziv1 instanceof String)
                urlZaPoziv = urlZaPoziv1;
            else
                urlZaPoziv = urlZaPoziv2;

            $.ajax({
                type: "POST",
                url: urlZaPoziv,
                data: form.serialize(),
                success: function (data) {
                    $("#" + divZaRezultat).html(data);
                    // check for alertify message popups
                    if (typeof form.attr('data-alertify') !== 'undefined') {
                        let msg = '';
                        if (form.attr('data-alertify-text')) {
                            msg += `<p> ${form.attr('data-alertify-text')} </p>`;
                        }
                        if (form.attr('data-alertify-btn-pre-text')) {
                            msg += `<span> ${form.attr('data-alertify-btn-pre-text')} </span>`;
                        }
                        if (form.attr('data-alertify-btn-text')) {
                            msg += `<a class="btn btn-sm btn-danger center" href="form.attr('data-alertify-goto-url')}">${form.attr('data-alertify-btn-text')}</a>`;
                        }
                        alertify.log(msg);
                    }
                }
            });
        }
    });
}
$(document).ready(function () {
    // izvršava nakon što glavni html dokument bude generisan
    DodajAjaxEvente();
});

$(document).ajaxComplete(function () {
    // izvršava nakon bilo kojeg ajax poziva
    DodajAjaxEvente();
});
