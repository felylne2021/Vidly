// AJAX load and save

//function LoadMovie() {
//    $("#movieTbl tbody tr").remove();
//    $.ajax({
//        type: 'POST',
//        url: '/Movies/GetMovie',
//        dataType: 'json',
//        data: { id: id },
//        success: function (data) {

//            $.each(data, function (m, movie) {
//                console.log(movie.Genre);
//                var row = "<tr>" +
//                    "<td></td>" +
//                    "<td>" + movie.Name + "</td>" +
//                    "<td>" + movie.Genre.GenreName + "</td>" +
//                    "</tr>";
//                $('#movieTbl tbody').append(row);
//            });
//        }
//    });
//    return false;
//}

//$(function () {
//    LoadMovie();
//    $("#movieSave").click(function () {
//        var mov = {};
//        mov.Name = $("#movieName").val();
//        mov.ReleaseDate = $("#movieReleaseDate").val();
//        mov.DateAdded = $("movieDateAdded").val();
//        mov.GenreId = $("movieGenreId").val();

//        $.ajax({
//            type: "POST",
//            url: '@Url.Action("Save")',
//            data: '{mov: ' + JSON.stringify(mov) + '}',
//            dataType: "json",
//            contentType: "application/json; charset=utf-8",
//            success: function () {
//                LoadMovie();
//            },
//            error: function () {
//                alert("Failed to save movie");
//            }
//        });
//        return false;
//    });
//});

//// Nested table

//$(document).ready(function () {
//    var table = $("#movieTbl").DataTable({
//        "ajax": "array-json",
//        "columns": [
//            {
//                "className": 'editBtnControl',
//                "orderable": false,
//                "data": null,
//                "defaultContent": ''
//            },
//            { "data": "movie" },
//            { "data": "genre" },
//        ]
//    })
//});

// customer index
$(document).ready(function () {
    var custDataTable = $('#custTbl').DataTable({
        ajax: {
            url: "/api/customers",
            dataSrc: ""
        },
        columns: [
            {
                data: "name",
                render: function (data, type, customer) {
                    return "<a href='/Customers/details/" + customer.id + "'>" + customer.name + "</a>";
                }
            },
            {
                data: "membershipType.name"
            },

            {
                data: "id",
                render: function (data) {
                    return "<button data-customer-id=" + data + " class='btn-danger js-delete'>Delete</button>";
                }
            }
        ]

    });
    $("#custTbl").on("click", ".js-delete", function () {
        var button = $(this);
        bootbox.confirm("This customer will be permanently deleted. Procees?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/customers/" + button.attr("data-customer-id"),
                    method: "DELETE",
                    success: function () {
                        custDataTable.row(button.parents("tr")).remove().draw();
                    }
                });
            }
        });

    });
});

// customer save
$(document).ready(function () {
    $("#custSave").on("click", function () {
        var name = $("#cName").val();
        var membershipTypeId = $("#membershipTypeId").val();
        var birthdate = $("#birthdate").val();
        var isSubscribedToNewsletter = $("#isSubscribedToNewsletter").val();

        $.ajax({
            url: '@Url.Action("Save", "Customers")',
            type: 'POST',
            contentType: 'application/json',
            dataType: 'html',
            data: JSON.stringify({
                name: name,
                birthdate: birthdate,
                isSubscribedToNewsletter: isSubscribedToNewsletter,
                membershipTypeId: membershipTypeId
                //NestedObj: {
                //    NestedA: 'test A',
                //    NestedB: 'test B'
                //}
            }),
            success: function () {
                console.log("success");
                $("#cName").val('');
                $("#membershipTypeId").val('');
                $("#birthdate").val('');
                $("#isSubscribedToNewsletter").removeAttr('selected');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log("failed");
                console.log(jqXHR);
            }
        });

    });
});