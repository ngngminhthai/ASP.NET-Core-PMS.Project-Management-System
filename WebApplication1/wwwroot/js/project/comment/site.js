$(document).ready(function () {

    LoadProdData();

    function LoadProdData(page, pageSize, id) {
        if (page == undefined) {
            page = 1;
            pageSize = 3;
        }



        $.ajax({
            type: 'GET',
            url: '/Projects/Details?'+id,
            data: {
                searchTerm: "",
                page: page,
                pageSize: pageSize
            },
            success: function (data, textStatus, jqXHR) {
                pagination = JSON.parse(jqXHR.getResponseHeader("Pagination"));
                var tableContent;
                data.forEach(function (comment) {
                    
                });
                document.querySelector('#comment-box').innerHTML = tableContent;
            }
        });
    }

    function nestedComment(listComment) {
        if (listComment !== null) {
            "<div class='@className'>"
                + "                     <div class='left d-flex'>"
                + "           <div class='comment-pic'>"
                + "                <img src='./images/avatar/cmt-02.png' alt=''>"
                + "            </div>"
                + "            <div class='comment-body'>"
                + "                <div class='name'>"
                + "                  <h5 class='font-w600 fs-18'>@cmt.Author</h5>"
                + "                  <p class='text mb-0 fs-18'>"
                + "                                                         @cmt.Content"
                + "                                                     </p>"
                + "                                                 </div>"
                + "                                             </div>"
                + "                                         </div>"
                + "                                         <div class='right'>"
                + "                                             <div class='group-action mt-10'>"
                + "                                                 <a href='#' class='like'><i class='fas fa-thumbs-up'></i>@cmt.NumberOfLike</a>"
                + "                                                 <a href='#' class='reply'><i class='fas fa-reply-all'></i>Reply</a>"
                + "                                             </div>"
                + "                                         </div>"
                + "                                     </div>"
                + "                                    <div class='divider'></div>"
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