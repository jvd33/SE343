$(window).on("load", function () {
    $('#OrderNumber').blur(validateOrder);
});
var amount = 0;

function validateOrder() {
    var orderId = $('#OrderNumber').val();
    $.ajax({
        url: 'https://cors-anywhere.herokuapp.com/http://sales.kennuware.com/api/salesorder/' + orderId,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#productId').val("F0001N");
            $('#OrderNumber').css({ 'background-color': 'rgba(255, 0, 0, 0.0)' });

            $('#orderErr').text('');
            $('#OrderNumber').prop('valid', true);
            console.log(data);
            amount = data.Order_total
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
    var failed = false;
    if ($('#OrderNumber').prop('valid') && $('#refund').prop('checked')) {
        $.ajax({
            url: 'https://cors-anywhere.herokuapp.com/http://accounting.kennuware.com/api/refund',
            data: JSON.stringify({ reason: 'Ticket Generated Refund', amount: amount, RequestingSilo: 1 }), 
            type: 'POST',
            dataType: 'json',
            contentType: "application/json",
            success: function (data) {

            },
            error: function () {
                alert('Failed to initiate refund');
                failed = true;
            }
        });
    }
    if (!failed) {
        $('#ticketForm').submit();
    } 
   
}