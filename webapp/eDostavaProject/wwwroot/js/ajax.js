function DodajAjaxEvente() {
    var DEVELOPER_MODE = true;

    // Onload ajax
    $("[ajax-onload='da']").each(function (el) {
        $(this).attr("ajax-onload", "dodan");

        var urlZaPoziv = $(this).attr("ajax-url");
        var divZaRezultat = $(this).attr("ajax-rezultat");

        console.log(urlZaPoziv);

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

        if (typeof self.attr("ajax-add-value-param") !== 'undefined') {
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
        var self = $(this);
        self.attr("ajax-poziv", "dodan");
        event.preventDefault();
        var urlZaPoziv1 = $(this).attr("ajax-url");
        var urlZaPoziv2 = $(this).attr("href");
        var divZaRezultat = $(this).attr("ajax-rezultat");

        var executeAfter = 0;

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
                msg += `<a class="btn btn-sm btn-danger center" href="${self.attr('data-alertify-goto-url')}">${self.attr('data-alertify-btn-text')}</a>`;
            }
            alertify.log(msg);
        }

        if (self.hasClass('ajax-delete-row')) {
            self.closest('tr').css('background', 'yellow').fadeOut(1000);
            executeAfter = 1100;
        }

        setTimeout(() => {
            $.ajax({
                type: "GET",
                url: urlZaPoziv,
                async: true,
                success: function (data) {

                    if (divZaRezultat.indexOf('addRow') !== -1) {
                        self.closest('tr').css("background", "yellow");
                        var colspan = self.closest('tr').children('td').length;
                        if (!$("#" + divZaRezultat).length) {
                            var newTr = $("<tr class='divRow'></tr>");
                            var newTd = $("<td id='" + divZaRezultat + "' colspan='" + colspan + "'></td>");
                            newTr.insertAfter(self.closest('tr')).append(newTd);
                        }
                    }

                    $("#" + divZaRezultat).html(data);
                }
            });
        }, executeAfter);
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

        // Provjeri custom js validaciju prije submita
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
                //data: form.serialize(),
                data: new FormData(form.get(0)),
                //
                processData: false,
                //
                contentType: false,
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
                        console.log($('.field-validation-error').length);
                        if ($('.field-validation-error').length === 0) {
                            alertify.log(msg);
                        }
                    }
                },
                error: function (data) {
                    if (DEVELOPER_MODE) {
                        console.log(data);
                        console.log(data.status);
                        console.log(data.statusCode);
                        console.log(data.statusText);
                        $("html").html(data.responseText)
                            .prepend(`<a href='/AdminModul/Moderator'
                                         style='background: red; padding: 10px; color: #fff; border-radius: 5px;
                                                position: absolute; top: 10px; right: 5px; position: fixed;'>
                                            Nazad na aplikaciju
                                      </a>`);
                    }
                }
            });
        }
    });
}

// Ponovo parsiraj npr. forme za validaciju
function ApplyUnobtrusiveValidation()
{
    let form = $(this);
    form.each(function () {
        $.data(form[0], 'validator', false);
    });
    $.validator.unobtrusive.parse("form");
}

// Izvršava nakon što glavni html dokument bude generisan
$(document).ready(function () {
    DodajAjaxEvente();
});

// Izvršava nakon bilo kojeg ajax poziva
$(document).ajaxComplete(function () {
    DodajAjaxEvente();
    ApplyUnobtrusiveValidation();
});
