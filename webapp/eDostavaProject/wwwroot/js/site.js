// Write your JavaScript code.
$(document).ready(function () {
    $(function () {
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
                    msg += `<a class="btn btn-sm btn-danger center" href="#">${self.attr('data-alertify-btn-text')}</a>`;
                }
                alertify.log(msg);
            });
        })


        // Korpa element
        let $korpa = $(".korpa-outer-wrap");
        $korpa.dynamicWidth = function () {
            $korpa.css("width", $korpa[0].getBoundingClientRect().width);
        }
        $korpa.dynamicWidth();

        let $smartAffixAElements = $('[data-smart-affix]');
        $smartAffixAElements.each(function () {
            console.log($(this).offset().top);
            $(this).affix({
                offset: {
                    top: $(this).offset().top - 50,
                }
            })
        })
        $(window).on("resize", function () {
            $korpa.dynamicWidth();
            $smartAffixAElements.each(function () {
                $(this).data('bs.affix').options.offset.top = $(this).offset().top - 50
            })
        })
    });
});