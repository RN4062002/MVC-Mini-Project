var TotalCount;
var TotalRecords;
var PageNumber ;
var PageSize ;
var expanded = false;

function BookGetList() {
    var formdata = $('#searchForm').serialize();
    formdata += '&PageNumber=' + PageNumber;
    formdata += '&PageSize=' + PageSize;
    debugger;

    $.ajax({
        url: '/Index/GetBookList',
        method: "GET",
        data: formdata,
        dataType: "json",
        success: function (data) {
            console.log("data is",data)
            console.log(data.PageNumber)
            TotalCount = data.counter;
            TotalRecords = data.TotalRecordes
            showBooks(data.bookList);
            paggination();
            display();
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function DeleteFun(BookID, BookName) {
    Swal.fire({
        title: "Are you sure?",
        html: "You want to delete <b>" + BookName + '</b>',
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
            var obj =
                {
                    BookID: BookID
                };
            $.ajax({
                url: '/Index/Delete',
                type: "GET",
                data: obj,
                success: function () {
                    BookGetList();
                },
                error: function () {
                    console.log('Error deleting Book.');
                }
            });
        }
    });
}

function EditFun(BookID) {

    $.ajax({
        url: '/Index/Update',
        type: 'GET',
        data: {
            BookID: BookID,
        },
        success: function (data) {
            $('#addBookContainer').html(data);
            $('#addBookModal').modal('show');

        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });

}

function display() {
    var startRecord = (PageNumber - 1) * PageSize + 1;
    var endRecord = (PageNumber * PageSize);
    debugger;
    if (endRecord > TotalRecords) {
        endRecord = TotalRecords;
    }
    $('#displayResult').html(`<h5><strong>Showing</strong> ${startRecord} - ${endRecord}<strong> of </strong> ${TotalRecords}<strong> records </strong> </h5>`);

}

function showBooks(books) {
    var tableBody = $('#tbody');
    tableBody.empty();
    $.each(books, function (index, book) {
        var row = '<tr>' +
            '<td class="text-center">' + book.BookID + '</td>' +
            '<td class="text-center">' + book.BookName + '</td>' +
            '<td class="text-center">' + book.BookPrice + '</td>' +
            '<td class="text-center">' + book.BookStock + '</td>' +
            '<td class="text-center">' + book.PublisherName + '</td>' +
            '<td class="text-center">' + book.AuthorName + '</td>' +
            '<td class="text-center">' +
             '<a href="/Index/Edit?BookID=' + book.BookID + '" role="button" class="btn btn-primary " id="EditBtn">Edit on newPage</a>' +
            '&nbsp;&nbsp;' +
            '<button class="btn btn-primary EditBtn" id="Edit" value="' + book.BookID + '">Edit on Popup</button>&nbsp;&nbsp;' +
            '<button class="btn btn-danger deleteBtn" data-name="' + book.BookName + '" value="' + book.BookID + '">Delete</button>' +
            '</td> ' +
            '</tr>';
        tableBody.append(row);
    });
}

function search() {
    BookGetList();
}
           
function paggination() {

    $("#pagination").empty();
    var numPagesToShow = 5;

    if (PageNumber == null) {
        PageNumber = 1;
    }

    // Calculate start and end page numbers to display
    var startPage = Math.max(1, PageNumber - Math.floor(numPagesToShow / 2));
    var endPage = Math.min(TotalCount, startPage + numPagesToShow - 1);


    if (PageNumber > 1) {
        $("#pagination").append(`<li class="page-item" id="prevPage"><a class="page-link">Previous</a></li>`)
    }

    for (var i = startPage; i <= endPage; i++) {
        if (i == PageNumber) {
            $("#pagination").append(`<li class="page-item active" id="paging" value = ${i}><a class="page-link">${i}</a></li>`);
        }
        else {
            $("#pagination").append(`<li class="page-item" id="paging" value = ${i}  ><a class="page-link">${i}</a></li>`);
        }
    }
    if (PageNumber < TotalCount) {
        $("#pagination").append(` <li class="page-item" id="nextPage"><a class="page-link">Next</a></li>`)
    }

}


function authorCheckBoxes() {
    debugger
    var $authorcheckboxes = $('#authorcheckboxes');
    if (!expanded) {
        $authorcheckboxes.show();
        expanded = true;
    } else {
        $authorcheckboxes.hide();
        expanded = false;
    }
}


function showCheckboxes() {
    debugger
    var $checkboxes = $("#checkboxes");

    if (!expanded) {
        $checkboxes.show();
        expanded = true;
    } else {
        $checkboxes.hide();
        expanded = false;
    }
}

    
$(document).ready(function () {
    PageNumber = document.getElementById('xyz').getAttribute('data-page-number');
    PageSize = $('#PageSize').val();
    console.log(PageNumber);

    $(document).on('click', '#paging', function () {
        PageSize = $('#PageSize').val();
        PublisherID = $('#PublisherID').val();
        AuthorID = $('#AuthorID').val();
        BookName = $('#BookName').val().trim();
        PageNumber = $(this).val();
        console.log("PageNumber in paggination is==>" + PageNumber)
        BookGetList(BookName, PublisherID, AuthorID, PageSize, PageNumber);
        // sessionStorage.setItem('pagenum', PageNumber);
    });

    $(document).on('click', '#Edit', function () {
        var BookID = $(this).val();
        console.log("Page Number in Edit click  button ==>" + PageNumber)
        debugger;
        EditFun(BookID)
    });

    $(document).on('click', '.deleteBtn', function () {
        var BookID = $(this).val();
        var BookName = $(this).data('name');
        DeleteFun(BookID, BookName)
    });

    $(document).on('click', '#prevPage', function () {
        if (PageNumber > 1) {
            PageNumber--;
            console.log("PageNumber in prve page is==>" + PageNumber)
            sessionStorage.setItem('pagenum', PageNumber);
            PageSize = $('#PageSize').val();
            PublisherID = $('#PublisherID').val();
            AuthorID = $('#AuthorID').val();
            BookName = $('#BookName').val().trim();
            BookGetList(BookName, PublisherID, AuthorID, PageSize, PageNumber);
        }
    });

    $(document).on('click', '#nextPage', function () {
        PageNumber++;
        console.log("PageNumber in next page is==>" + PageNumber)
        sessionStorage.setItem('pagenum', PageNumber);
        PageSize = $('#PageSize').val();
        PublisherID = $('#PublisherID').val();
        AuthorID = $('#AuthorID').val();
        BookName = $('#BookName').val().trim();
        BookGetList(BookName, PublisherID, AuthorID, PageSize, PageNumber);
    });

    $('#create').click(function () {
        $.ajax({
            url: '/Index/Insert',
            method: "GET",
            success: function (response) {

                $('#addBookContainer').html(response);
                $('#addBookModal').modal('show');
            },
            error: function (err) {
                console.log(err);
            }
        });
    });

    $('#searchForm').submit(function (event) {  
        event.preventDefault(); 
        var $checkboxes = $("#checkboxes");
        $checkboxes.hide();

        var $authorcheckboxes = $('#authorcheckboxes');
        $authorcheckboxes.hide();
        expanded = false;
        search()
    });

    $('#PageSize').change(function () {
        PublisherID = $('#PublisherID').val();
        AuthorID = $('#AuthorID').val();
        BookName = $('#BookName').val().trim();
        PageSize = $(this).val();
        PageNumber = $('#paging').val();
        console.log("PageNumber in pagesize is==>" + PageNumber)
        BookGetList(BookName, PublisherID, AuthorID, PageSize, PageNumber);
    });

    $(document).on('click', '#clear', function () {
        //sessionStorage.clear();
        BooKName = $('#BookName').val(null);
        PublisherID = $('#PublisherID').val(null);
        AuthorID = $('#AuthorID').val(null);
        $('input[name="MultiAuthorIDList"]').prop('checked', false);
        $('input[name="MultiPublisherIDList"]').prop('checked', false)
        BookGetList(BooKName, null, null, PageSize, PageNumber);
    });

    ////---parsial view Save Button-----//
    $(document).on('click', '#EditSaveBtn', function (e) {
        e.preventDefault();

        var formData = $('#myForm').serialize();
        formData += '&PageNumber=' + PageNumber;
        var BookID = $('#BookID').val();
        console.log("PageNumber value on EditSaveBtn ==>" + PageNumber);
        var url = BookID > 0 ? '/Index/Update/' + BookID : '/Index/Insert/';

        $.ajax({
            url: url,
            method: "POST",
            data: formData,
            dataType: "json",
            success: function (response) {
                console.log(response);
                $('#HideModel').click();
                BookGetList('', '', '', PageSize, PageNumber);
            },
            error: function (err) {
                console.log(err);
            }
        });
    })

    BookGetList();

});

