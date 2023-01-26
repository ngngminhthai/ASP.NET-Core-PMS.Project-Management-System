var pagination;

$(document).ready(function () {

    LoadProdData();

    function LoadProdData(page, pageSize) {
        if (page == undefined) {
            page = 1;
            pageSize = 3;
        }
        $('#product-table tbody tr').fadeOut(500, function () { $(this).remove(); });

        $.ajax({
            type: 'GET',
            url: '/Products/GetProducts',
            data: {
                searchTerm: "",
                page: page,
                pageSize: pageSize
            },
            success: function (data, textStatus, jqXHR) {
                pagination = JSON.parse(jqXHR.getResponseHeader("Pagination"));
                var tableContent;
                $.each(data, function (index, product) {
                    tableContent += `
                        <tr> +
                        <td> ${product.Id} </td> +
                        <td> ${product.Name} </td> +
                        <td> ${product.Price} </td> +
                        <td> <a href="/Products/Edit?id=${product.Id}">Edit</a> |  </td> +
                        <td> <a href="/Products/Delete?id=${product.Id}">Delete</a>  </td> +

                        </tr>
                    `
                });
                $('#product-table tbody').html(tableContent);
                loadPagination(pagination);
            }
        });
    }


    function loadPagination() {
        console.log("Load pagination")

        $('#paginationUL').twbsPagination({
            totalPages: pagination.totalCount, 
            startPage: pagination.currentPage,
            onPageClick: function (event, page) {
                LoadProdData(page, 3);
                return false;
            }
        });
    }
    

})


