//EXTERNAL
function validate_order(orderNum) {
    $.get("/test/getorder", { orderNumber: orderNum }, function (data) {
        console.log(data);
        result.innerHTML = "Order valid!";
    });
}


function refund_order(orderNum, amount) {
    $.get("/test/refund", { orderNumber: orderNum }, function (data) {
        console.log(data);
        output.innerHTML = "Order refund initiated.\n";
    });
}

function get_order_info(product_id, quant, t) {
    $.get("/test/orderrequest", { productId: product_id, quantity: quant, type:t }, function (data) {
        console.log(data);
        productInfo.innerHTML = data
    });
}


//INTERNAL, WE WONT CALL THESE
function confirm_product_received(id) {
    $.get("/api/acceptproductreceived", { ticketID: id }, function (data) {
        console.log(data);
        internal1.innerHTML = "Product Received. Ticket status updated to: " + data
    });
}

function confirm_refund(id) {
    $.get("/api/completerefund", { ticketID: id }, function (data) {
        console.log(data);
        internal2.innerHTML = "Product Refunded. Ticket status updated to: " + data
    });
}