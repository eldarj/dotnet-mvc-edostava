// Write your JavaScript code.
$(document).ready(function () {
    $(function () {
        // Korpa element
        let korpa = $(".korpa-outer-wrap");
        korpa.dynamicWidth = function () {
            korpa.css("width", korpa[0].getBoundingClientRect().width);
        }
        korpa.dynamicWidth();

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
            korpa.dynamicWidth();
            $smartAffixAElements.each(function () {
                $(this).data('bs.affix').options.offset.top = $(this).offset().top - 50
            })
        })
    });
});