(function($) {
    // DOM ready
    $(function() {
        // Append the mobile icon nav
        $('.main-nav').append($('<div class="nav-mobile"></div>'));
        // Add a <span> to every .nav-item that has a <ul> inside
        $('.nav-item').has('ul').prepend('<span class="nav-click"><i class="nav-arrow"></i></span>');
        $('.nav-submenu-item').has('ul').prepend('<span class="nav-sub-click"><i class="nav-arrow"></i></span>');
        $('.nav-submenu-item').has('ul').addClass('arrow-sub');
        // Click to reveal the nav
        $('.nav-mobile').click(function() {
            $('.nav-list').slideToggle();
        });

        // Dynamic binding to on 'click'
        $('.nav-list').on('click', '.nav-click', function() {
            //$('.nav-list').find('.nav-submenu').has('style').toggle();
            // Toggle the nested nav
            $(this).siblings('.nav-submenu').slideToggle();
            // Toggle the arrow using CSS3 transforms
            $(this).children('.nav-arrow').toggleClass('nav-rotate');

        });
        $('.nav-list').on('click', '.nav-sub-click', function() {
            //$('.nav-list').find('.nav-submenu').has('style').toggle();
            // Toggle the nested nav
            $(this).siblings('.submenu-nav').slideToggle();
            // Toggle the arrow using CSS3 transforms
            $(this).children('.nav-arrow').toggleClass('nav-rotate');

        });
    });

})(jQuery);