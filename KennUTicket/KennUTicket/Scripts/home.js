$(document).ready(function () {
    if (Cookies.get('s')) {
        $("#login").hide();
        $("#msg").text("Welcome, " + Cookies.get('username'));
        $("#msg").show();
    }

});