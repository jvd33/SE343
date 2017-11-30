function get_user(username, password) {}



function authenticate() {
    var form = $("#login");
    var inputs = $('#login :input').toArray().filter(input => input.id === "Username" || input.id === "Password");
    console.log(inputs);
    $.get("https://hr.kennuware.com/api/v1/employees/" + inputs[0].value, function (data) {
        
    }, "jsonP");
    form.submit();
}