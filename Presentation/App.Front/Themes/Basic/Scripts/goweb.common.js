
$(document).ready(function () {
    featurGoWeb.init();
});


var featurGoWeb = (function () {
    var slideOnHome = function () {
        if ($("#home-slider").length > 0 && $("#contenhomeslider").length > 0) {
            var slider = $("#contenhomeslider").bxSlider(
                {
                    nextText: '<i class="fa fa-angle-right"></i>',
                    prevText: '<i class="fa fa-angle-left"></i>',
                    auto: true
                }

            );
        }
    }

    return {
        init: function () {
            slideOnHome();
        }

    }

})();

if (typeof EGA === "undefined") EGA = {};
EGA.product = {};
EGA.plugin = {};
EGA.addScript = {};
EGA.header = {};
EGA.filter = {};
EGA.options = {
    money_format: "{{amount}}₫"
}