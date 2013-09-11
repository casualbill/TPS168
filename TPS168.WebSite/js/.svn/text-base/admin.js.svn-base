$(document).ready(function () {
    admin_menu_toggle();
})

function admin_menu_toggle() {
    $(".admin-left").children().children().children().children("dt").click(function () {
        //$(this).nextAll("dd").toggle();

        if ($(this).nextAll("dd").css("display") == "none") {
            $(this).nextAll("dd").slideDown(500);
        }
        else {
            $(this).nextAll("dd").slideUp(500);
        }

    })

}