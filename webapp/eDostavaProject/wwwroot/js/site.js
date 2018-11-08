// Write your JavaScript code.
$(document).ready(function () {


    $(function () {
        window.FontAwesomeConfig = {
            searchPseudoElements: true
        }

        // Live form edits (npr profil)
        $("#korisnik-right-wrap").on('click', '.live-edit-section', function () {
            $(this).closest(".form-group").toggleClass('edit');
            if ($('.form-group.edit').length > 0) {
                $("form.live-edit").addClass("edit");
            } else {
                $("form.live-edit").removeClass("edit")
            }
        });

        // Alertify messages 
        let $alertBtns = $("[data-alertify]");
        $alertBtns.each(function () {
            let self = $(this);
            self.click(function () {
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
            });
        })

        // Handle Korpa width on resize
        $(window).on("resize", function () {
            $korpa.dynamicWidth();
        })
    });

    DodajHandlere();
});

function DodajHandlere() {
    // Toggle password fields visibility
    $(".see-password-input").off().on('click', function () {
        let targetId = $(this).attr('data-target'),
            targetEl = $('#' + targetId),
            toggleIcon = $("[data-icon-for='" + targetId + "']");

        if (targetEl.attr('type') === "password") {
            targetEl.attr('type', 'text');
            toggleIcon.removeClass("fa-eye")
                .addClass("fa-eye-slash");
        } else {
            targetEl.attr('type','password');
            toggleIcon.removeClass("fa-eye-slash")
                .addClass("fa-eye");
        }
    });

    // Check if fields are same
    $("[data-check-is-same]").off().on("change", function () {
        let checkAgainstId = $(this).attr('data-check-is-same'),
            checkAgainstEl = $('#' + checkAgainstId),
            checkErrorMsgEl = $('#' + $(this).attr('data-check-erorr-id'));

        if ($(this).val() === checkAgainstEl.val()) {
            checkErrorMsgEl.addClass('invisible');
            $(this).removeClass('is-invalid')
                   .addClass('is-valid');
            checkAgainstEl.removeClass('is-invalid')
                          .addClass('is-valid');
        } else {
            checkErrorMsgEl.removeClass('invisible');
            $(this).removeClass('is-valid')
                   .addClass('is-invalid');
            checkAgainstEl.removeClass('is-valid')
                          .addClass('is-invalid');
        }
    });
}

$(document).ajaxComplete(function () {
    // izvršava nakon bilo kojeg ajax poziva
    DodajHandlere();

});
