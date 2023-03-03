// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var invoiceDetail = new InvoiceDetail();
var invoice = new Invoice();

$(document).ready(function () {
    $("#Qty").keyup(function (event) {
        event.preventDefault();
        invoiceDetail.CompleteInvoice();
    });
    $("#Price").keyup(function (event) {
        event.preventDefault();
        invoiceDetail.CompleteInvoice();
    });
    $("#TotalItbis").keyup(function (event) {
        event.preventDefault();
        invoiceDetail.CompleteInvoice();
    });
    $("#SubTotal").keyup(function (event) {
        event.preventDefault();
        invoiceDetail.CompleteInvoice();
    });
    $("#Total").keyup(function (event) {
        event.preventDefault();
        invoiceDetail.CompleteInvoice();
    });

    $("#TotalI").keyup(function (event) {
        event.preventDefault();
        invoice.CompleteInvoiceI();
    });
});