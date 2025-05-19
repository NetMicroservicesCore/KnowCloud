var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        order: [[0, 'desc']],
        "ajax": { url: "/Product/All" },
        "error": function (xhr, error, thrown) {
            console.log("XHR:", xhr.responseText);
        },
        "columns": [
            { data: 'productId', "width": "5%" },
            { data: 'name', "width": "25%" },
            { data: 'price', "width": "20%" },
            { data: 'description', "width": "10%" },
            { data: 'categoryName', "width": "10%" },
            {
                data: 'productId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/Product/ProductEdit?productId=${data}" class="btn btn-primary mx-2"><i class="fas fa-edit"></i></a>
                    </div>`
                },
                "width": "10%"
            },
        ],
    })
}
