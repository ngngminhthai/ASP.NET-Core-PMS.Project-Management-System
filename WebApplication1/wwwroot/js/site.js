// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*import { signalR } from "../lib/microsoft/signalr/dist/browser/signalr";
*/
$(document).ready(function () {
    LoadProdData();

    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
    connection.start();

    connection.on("LoadProducts", function () {
        LoadProdData();
    })

   

    function LoadProdData() {
        $('#product-table tbody').html('');

        $.ajax({
            type: 'GET',
            url: '/Products/GetProducts',
            success: function (data) {
                $.each(data, function (index, product) {
                    $('#product-table tbody').append(
                        '<tr>' +
                        '<td>' + product.id + '</td>' +
                        '<td>' + product.name + '</td>' +
                        '<td>' + product.price + '</td>' +
                        '<td>' + `<a href="/Products/Edit?id=${product.id}">Edit</a> |` + '</td>' +
                        '<td>' + `<a href="/Products/Delete?id=${product.id}">Delete</a>` + '</td>' +

                        '</tr>'
                    );
                });
            }
        });

    }
})
