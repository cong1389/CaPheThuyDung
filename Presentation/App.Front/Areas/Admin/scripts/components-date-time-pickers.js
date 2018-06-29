var ComponentsDateTimePickers = function () {

    var handleDatePickers = function () {

        if (jQuery().datepicker) {
            $(".date-picker").datepicker({
                rtl: App.isRTL(),
                orientation: "left",
                autoclose: true
            });
            //$('body').removeClass("modal-open"); // fix bug when inline picker is used in modal
        }

        /* Workaround to restrict daterange past date select: http://stackoverflow.com/questions/11933173/how-to-restrict-the-selectable-date-ranges-in-bootstrap-datepicker */
    } 
     

    return {
        //main function to initiate the module
        init: function () {
            handleDatePickers(); 
        }
    };

}();

jQuery(document).ready(function () {
    ComponentsDateTimePickers.init();
});