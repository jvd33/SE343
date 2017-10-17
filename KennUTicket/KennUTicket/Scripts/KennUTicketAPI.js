//EXTERNAL
function validate_order(orderNum) {
    $.post("/api/testintegration/getorder?orderNumber=" + orderNum, function (data) {
        console.log(data);
        result.innerHTML = "Order valid!";
    });
}


function refund_order(orderNum) {

    $.post("/api/testintegration/refundorder?orderNumber=" + orderNum, function (data) {
        console.log(data);
        output.innerHTML = "Order refund initiated.\n";
    });
}

function get_order(productNum, orderNum) {

    $.post("/api/testintegration/orderrequest", { SKU: productNumber, quantity: 1, orderNumber: orderNum }, function (data) {
        console.log(data);
    });
}


//INTERNAL, WE WONT CALL THESE
function confirm_product_received(id) {
    $.post("/api/api/acceptproductreceived?ticketid=" + id, function (data) {
        console.log(data);
        internal1.innerHTML = "Product Received. Ticket status updated to: " + data
    });
}

function confirm_refund(id) {
    $.post("/api/api/confirmproductrefunded?ticketid=" + id, function (data) {
        console.log(data);
        internal2.innerHTML = "Product Refunded. Ticket status updated to: " + data
    });
}