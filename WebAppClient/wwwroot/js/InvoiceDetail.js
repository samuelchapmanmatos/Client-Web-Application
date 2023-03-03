class InvoiceDetail {
    constructor() {        
    }

    CompleteInvoice() {

        var qty = document.getElementById("Qty").value;
        var price = document.getElementById("Price").value;
        var totalItbis = document.getElementById("TotalItbis").value;
        var subTotal = document.getElementById("SubTotal").value;
        var total = document.getElementById("Total").value;

        var data = {
            qty: qty,
            price: price,
            totalItbis: totalItbis,
            subTotal: subTotal,
            total: total
        };
        $.ajax({
            url: "CompleteInvoiceDetails",
            data: data,
            type: 'POST',
            success: (result) => {
                try {
                    var item = JSON.parse(result);
                    if (item.Qty > 0) { document.getElementById("Qty").value=item.Qty; }
                    if (item.Price > 0) { document.getElementById("Price").value=item.Price; }
                    if (item.TotalItbis > 0) { document.getElementById("TotalItbis").value=item.TotalItbis; }
                    if (item.SubTotal > 0) { document.getElementById("SubTotal").value=item.SubTotal; }
                    if (item.Total > 0) { document.getElementById("Total").value=item.Total; }
                } catch (e) {
                    
                }
            }
        });       
    }
}