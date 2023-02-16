var projectId
function setId(id) {
    projectId = id;
}

$(document).ready(function () {

    LoadProdData();
    var tableContent = "<div></div>";
    function LoadProdData(page, pageSize) {
        if (page == undefined) {
            page = 1;
            pageSize = 3;
        }



        $.ajax({
            type: 'GET',
            url: '/Projects/GetProjectAndComment',
            data: {
                id: projectId,
                page: page,
                pageSize: pageSize

            },
            success: function (data, textStatus, jqXHR) {
                pagination = JSON.parse(jqXHR.getResponseHeader("Pagination"));

                nestedComment(data.ListComment);
                document.querySelector('#comment-box').innerHTML = tableContent;

            }
        });
    }

    function nestedComment(listComment) {
        if (listComment !== null) {
            for (var i = 0; i < listComment.length; i++) {
                var className = "comment d-flex a111";
                if (listComment[i].Level === 1) { className = "comment rep d-flex" }
                else if (listComment[i].Level >= 2) { className = "comment rep2 d-flex" };
                tableContent +=
                    "<div class='" + className + "'>"
                    + "                     <div class='left d-flex'>"
                    + "           <div class='comment-pic'>"
                    + "                <img src='./images/avatar/cmt-02.png' alt=''>"
                    + "            </div>"
                    + "            <div class='comment-body'>"
                    + "                <div class='name'>"
                    + "                  <h5 class='font-w600 fs-18'>" + listComment[i].Author + "</h5>"
                    + "                  <p class='text mb-0 fs-18'>" + listComment[i].Content + "</p>"
                    + "                                                 </div>"
                    + "                                             </div>"
                    + "                                         </div>"
                    + "                                         <div class='right'>"
                    + "                                             <div class='group-action mt-10'>"
                    + "                                                 <a href='#' class='like'><i class='fas fa-thumbs-up'></i>" + listComment[i].NumberOfLike + "</a>"
                    + "                                                 <a href='#' class='reply'><i class='fas fa-reply-all'></i>Reply</a>"
                    + "                                             </div>"
                    + "                                         </div>"
                    + "                                     </div>"
                    + "                                    <div class='divider'></div>"
                    + "<div class='col - sm - 7''>"
                    + "   <div class='dataTables_paginate paging_simple_numbers' id = 'datatable-checkbox_paginate' >"
                    + "       <ul id='paginationUL''></ul>"
                    + " </div >"
                    + "</div > "

                nestedComment(listComment[i].ChildComments);
            }
        }
    }

    function loadPagination() {
        //console.log("Load pagination")

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