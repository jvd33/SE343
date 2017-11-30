$(window).on("load", function () {
    $('#OrderNumber').blur(validateOrder);
});

function validateOrder() {
    var orderId = $('#OrderNumber').val();
    $.ajax({
        url: 'http://sales.kennuware.com/api/salesorder/' + orderId,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#productId').val("F0001N");
            $('#OrderNumber').prop('valid', true);
        },
        error: function () {
            $('#OrderNumber').css({ 'background-color': 'rgba(255, 0, 0, 0.6)' });
            $('#productId').val("F0001N");

            $('#OrderNumber').prop('valid', true);
            $('#orderErr').text('Order Number Invalid');

        }
    });
}


function onTicketCreate() {
    if ($('#OrderNumber').prop('valid') && $('#refund').prop('checked')) {
        $.ajax({
            url: 'http://accounting.kennuware.com/api/refund/',
            headers: {
                'Content-Type': 'application/json'
            },
            data: { reason: 'Testing', amount: 1, requestingSilo: 1 }, 
            type: 'POST',
            dataType: 'jsonp',
            success: function (data) {

            },
            error: function () {

            }
        });
    }
    $('#ticketForm').submit();
}