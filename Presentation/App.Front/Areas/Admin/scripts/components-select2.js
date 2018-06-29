var ComponentsSelect2 = function() {

    var handleSelect2= function() {

        // Set the "bootstrap" theme as the default theme for all Select2
        // widgets.
        //
        // @see https://github.com/select2/select2/issues/2927
        $.fn.select2.defaults.set("theme", "bootstrap");

        $(".select2").select2({
            placeholder: "--Select--",
            width: null
        });

        $(".select2-allow-clear").select2({
            allowClear: true,
            placeholder: "--Select--",
            width: null
        });

         
        // copy Bootstrap validation states to Select2 dropdown
        //
        // add .has-waring, .has-error, .has-succes to the Select2 dropdown
        // (was #select2-drop in Select2 v3.x, in Select2 v4 can be selected via
        // body > .select2-container) if _any_ of the opened Select2's parents
        // has one of these forementioned classes (YUCK! ;-))
        $(".select2").on("select2:open", function() {
            if ($(this).parents("[class*='has-']").length) {
                var classNames = $(this).parents("[class*='has-']")[0].className.split(/\s+/);

                for (var i = 0; i < classNames.length; ++i) {
                    if (classNames[i].match("has-")) {
                        $("body > .select2-container").addClass(classNames[i]);
                    }
                }
            }
        }); 
    }

    return {
        //main function to initiate the module
        init: function() {
            handleSelect2();
        }
    };

}();
jQuery(document).ready(function () {
    ComponentsSelect2.init();
});