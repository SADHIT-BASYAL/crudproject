var dtable;
$(document).ready(function () {
    dtable = $('#myTable').DataTable(

        "ajax": {
        "url": "/Admin/Product/AllProducts"
    },
        "columns": [
        { "data": "Product Name" },
        { data: 'position' },
        { data: 'salary' },
        { data: 'office' }
    ]
        });
});