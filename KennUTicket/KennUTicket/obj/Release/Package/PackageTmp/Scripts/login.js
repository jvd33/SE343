function get_user(username, password) {}



function authenticate() {
    var form = $("#login");
    var inputs = $('#login :input').toArray().filter(input => input.id === "Username" || input.id === "Password");
    var base = "https://cors-anywhere.herokuapp.com/http://hr.kennuware.com/api/v1/";
    $.ajax({
        url: base + "sessions/new-session",
        data: { username: inputs[0].value, password: inputs[1].value },
        method: 'POST',
        datatype: 'json',
        success: function (result, status, xhr) {
            form.submit()
        },
        error: function (xhr, status, error) {
            $('#err').text("Invalid username or password")
        }
    });
    //form.submit();
}