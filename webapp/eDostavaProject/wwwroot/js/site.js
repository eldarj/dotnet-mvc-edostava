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

        $(window).on("resize", function () {
            $korpa.dynamicWidth();
        })
    });
});