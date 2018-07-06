
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

    var owlCarousel = function () {
        if ($(".gw-news-related__owl").length > 0) {
            $(".gw-news-related__owl").each(function (index, el) {
                var config = $(this).data();
                config.nav = false;
                config.navText = ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'];
                config.smartSpeed = "300";
                config.margin = 10;
                config.responsive = {
                    // breakpoint from 0 up
                    0: {
                        items: 2
                    },
                    // breakpoint from 480 up
                    480: {
                        items: 2
                    },
                    // breakpoint from 768 up
                    768: {
                        items: 3
                    },
                    1000: {
                        items: 5
                    }
                };
                if ($(this).hasClass("owl-style2")) {
                    config.animateOut = "fadeOut";
                    config.animateIn = "fadeIn";
                }
                $(this).owlCarousel(config);
            });
        }

    };

    var owlBanner = function () {
        if ($(".gw-banner-top").length > 0) {
            $(".gw-banner-top").each(function (index, el) {
                var config = $(this).data();
                config.nav = false;
                config.navText = ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'];
                config.smartSpeed = "300";
                config.margin = 1;
                config.responsive = {
                    // breakpoint from 0 up
                    0: {
                        items: 2
                    },
                    // breakpoint from 480 up
                    480: {
                        items: 2
                    },
                    // breakpoint from 768 up
                    768: {
                        items: 3
                    },
                    1000: {
                        items: 5
                    }
                };
                if ($(this).hasClass("owl-style2")) {
                    config.animateOut = "fadeOut";
                    config.animateIn = "fadeIn";
                }
                $(this).owlCarousel(config);
            });
        }

    };

    return {
        init: function () {
            slideOnHome();
            owlCarousel();
            owlBanner();
        }

    }

})();