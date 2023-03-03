class Invoice {
    constructor() {
    }

    CompleteInvoiceI() {

        var totalI = document.getElementById("TotalI").value;

        var data = {
            totalI: totalI
        };
        console.log(data);
        $.ajax({
            url: "CompleteInvoiceI",
            data: data,
            type: 'POST',
            success: (result) => {
                try {
                    var item = JSON.parse(result);
                    if (item.TotalItbis > 0) { document.getElementById("TotalItbisI").value = item.TotalItbis; }
                    if (item.SubTotal > 0) { document.getElementById("SubTotalI").value = item.SubTotal; }
                    console.log(item);
                } catch (e) {
                    console.log(e);
                }
            }
        });
    }
}