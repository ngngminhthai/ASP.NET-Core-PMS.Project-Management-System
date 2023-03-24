/*
Author       : Dreamguys
Template Name: SmartHR - Bootstrap Admin Template
Version      : 3.6
*/


$(document).ready(function () {



    if ($('.kanban-wrap').length > 0) {
        $(".kanban-wrap").sortable({
            connectWith: ".kanban-wrap",
            handle: ".kanban-box",
            placeholder: "drag-placeholder",
            receive: function (event, ui) {
                // Lấy thông tin thành phần bị drag
                var draggedItem = ui.item;
                const formInput = draggedItem[0].querySelectorAll('input[value]');
             
                const idKanban = formInput[0].value;
                console.log(idKanban)
                var newIndex = ui.item.index(); // lấy vị trí mới của item
                console.log("Item được kéo có index mới là: " + newIndex);

                // Lấy thông tin container được drop
                var droppedContainer = $(this);
                console.log("Đã thả vào: " + droppedContainer.attr("id"));
                var cid = droppedContainer.attr("id")
             
                // Construct the data to be sent to the server
                

                $.ajax({
                    type: 'POST',
                    url: '/ProjectTasks/Update',
                    data: {
                        id: idKanban,
                        columeId: cid,
                        order: newIndex,
                    },
                    success: function (data, textStatus, jqXHR) {

                    }
                });
            },
            update: function (event, ui) {
                var newIndex = ui.item.index(); // lấy vị trí mới của item
                console.log("Item được kéo có index mới là: " + newIndex);
                var draggedItem = ui.item;
                const formInput = draggedItem[0].querySelectorAll('input[value]');

                const idKanban = formInput[0].value;
                console.log(idKanban);
                $.ajax({
                    type: 'POST',
                    url: '/ProjectTasks/ChangeOrder',
                    data: {
                        id: idKanban,
                        order: newIndex,
                    },
                    success: function (data, textStatus, jqXHR) {

                    }
                });

            }
        });
    }
    if ($('.datetimepicker').length > 0) {
        $('.datetimepicker').datetimepicker({
            format: 'DD/MM/YYYY',
            icons: {
                up: "bx bx-chevron-up",
                down: "bx bx-chevron-down",
                next: 'bx bx-chevron-right',
                previous: 'bx bx-chevron-left'
            }
        });
    }

});

// Loader

$(window).on('load', function () {
    $('#loader').delay(100).fadeOut('slow');
    $('#loader-wrapper').delay(500).fadeOut('slow');
});

function myFunctionDelete(id) {
    console.log(id)
    const form = document.getElementById('delete_' + id);

    console.log(form);
    form.submit();
}
//$(document).ready(function () {



//    if ($('.kanban-wrap').length > 0) {
//        $(".kanban-wrap").sortable({
//            connectWith: ".kanban-wrap",
//            handle: ".kanban-box",
//            placeholder: "drag-placeholder"
//        });
//    }

//    if ($('.datetimepicker').length > 0) {
//        $('.datetimepicker').datetimepicker({
//            format: 'DD/MM/YYYY',
//            icons: {
//                up: "bx bx-chevron-up",
//                down: "bx bx-chevron-down",
//                next: 'bx bx-chevron-right',
//                previous: 'bx bx-chevron-left'
//            }
//        });
//    }

//    // Make the kanban-box elements draggable
//    $(".kanban-box").draggable({
//        helper: "clone"
//    });

//    // Make the kanban-wrap elements droppable
//    $(".kanban-wrap").on({
//        drop: function (event, ui) {
//            // Get the dragged element
//            var draggedElement = ui.draggable;
//            const formInput = draggedElement[0].querySelectorAll('input[value]');
//            console.log(draggedElement)
//            const idKanban = formInput[0].value;

//            console.log(idKanban);
//            var wrapper = $('<div>').addClass('panel').append(draggedElement)
//            console.log(wrapper);


//            const formInputChild = this.querySelectorAll('input[value]');


//            // Do something with the dragged element here, such as appending it to the droppable element



//            const cid = formInputChild[0].value;
//            $(this).append(wrapper);
//            // Construct the data to be sent to the server
//            const data = {
//                id: idKanban,
//                columeId: formInputChild[0].value
//            };

//            $.ajax({
//                type: 'POST',
//                url: '/ProjectTasks/Update',
//                data: {
//                    id: idKanban,
//                    columeId: cid,
//                },
//                success: function (data, textStatus, jqXHR) {

//                }
//            });

//        }
//    });



//});

// Loader

$(window).on('load', function () {
    $('#loader').delay(100).fadeOut('slow');
    $('#loader-wrapper').delay(500).fadeOut('slow');
});



// var $a = self.$widget.find('a[data-rating-value="' + ratingValue() + '"]');


// resetStyle();


// $a.addClass('br-selected br-current')[nextAllorPreviousAll()]()
//     .addClass('br-selected');

// if (!getData('ratingMade') && $.isNumeric(initialRating)) {
//     if ((initialRating <= baseValue) || !f) {
//         return;
//     }

//     $all = self.$widget.find('a');

//     $fractional = ($a.length) ?
//         $a[(getData('userOptions').reverse) ? 'prev' : 'next']() :
//         $all[(getData('userOptions').reverse) ? 'last' : 'first']();

//     $fractional.addClass('br-fractional');
//     $fractional.addClass('br-fractional-' + f);
// }