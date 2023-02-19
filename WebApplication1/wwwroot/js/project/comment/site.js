

var projectId


function setId(id) {
    projectId = id;
}

$(document).ready(function () {

    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
    connection.start();
    connection.on("LoadProjectComment", function () {
        LoadProdData();
    })
    LoadProdData();


    var tableContent = "<div></div>";
    function LoadProdData(page, pageSize) {
        if (page == undefined) {
            page = 1;
            pageSize = 99;
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
                tableContent = "";
                nestedComment(data.ListComment);
                document.querySelector('#comment-box').innerHTML = tableContent;

            },
            error: (error) => {
                console.log(error)
            }
        });
    }

    function nestedComment(listComment) {
        if (listComment !== null) {
            for (var i = 0; i < listComment.length; i++) {
                var className = "comment d-flex";
                if (listComment[i].Level === 1) { className = "comment rep d-flex" }
                else if (listComment[i].Level >= 2) { className = "comment rep2 d-flex" };
                tableContent +=
                    "<div class='comment-container'>"
                    + "<div class='" + className + "'>"
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
                    + "                                                 <a id='repId_" + listComment[i].Id + "' onclick='clickReply(  " + listComment[i].Id + "," + listComment[i].Level + ")' class='reply'><i class='fas fa-reply-all'></i>Reply</a>"
                    + "                                             </div>"
                    + "                                         </div>"
                    + "                                     </div>"
                    + " <div class='divider'></div>"
                    + "<div class='col - sm - 7''>"
                    + "   <div class='dataTables_paginate paging_simple_numbers' id = 'datatable-checkbox_paginate' >"
                    + "       <ul id='paginationUL''></ul>"
                    + " </div >"
                    + "</div > "
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

const form = document.getElementById('comment_to_project');
const sendBtn = document.getElementById('send_comment');

sendBtn.addEventListener('click', function (e) {
    e.preventDefault(); // prevent the default form submission

    const formData = new FormData(form); // create a new FormData object from the form
    const xhr = new XMLHttpRequest(); // create a new XMLHttpRequest object

    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE) {
            // when the request is complete
            if (xhr.status === 200) { // if the response status is OK
                console.log(xhr.responseText); // log the response text to the console
            } else {
                console.error(xhr.statusText); // log the error status text to the console
            }
        }
    };

    xhr.open(form.method, form.action); // set the request method and URL
    xhr.setRequestHeader("Accept", "application/json"); // set the Accept header to application/json
    xhr.send(formData); // send the form data to the server

    const commentContent = document.querySelector('#comment_to_project textarea[name="comment.Content"]');

    commentContent.value = "";
});

function createInputBox(parentID, level) {
    var childLevel = level + 1;
    let div = document.createElement("div");
    div.setAttribute("class", "form-chat rep" + level + " d-flex");
    div.setAttribute("id", "comment_to_project_box" + parentID);



    div.innerHTML += "<form style='width:100%' class='comment_rep' id='comment_to_project" + parentID + "' action='/Projects/CreateComment' method='post' accept-charset='utf-8'>"
        + "<input type = 'hidden' name = 'comment.ParentID' value = " + parentID + " />"
        + " <input type='hidden' name='comment.Level' value=" + childLevel + " />"
        + "  <input type='hidden' name='comment.NumberOfLike' value='0' />"
        + " <div class='message-form-chat'>"

        + "   <span class='message-text'>"
        + "     <textarea placeholder='Type comment here' required='required' name='comment.Content' ></textarea>"
        + "  </span>"


        + " <span class='btn-send'>"
        + "    <button  onclick=' repCommment(  "+ parentID + ")'  class='waves-effect'>Send <i class='fas fa-paper-plane'></i></button>"
        + " </span>"


        + "   </div> "

        + "</form > ";

    return div;
}

function clickReply(parentID, level) {
    const delElement = document.getElementById("comment_to_project_box" + parentID);
    if (delElement !== null) {
        delElement.remove();
    }
    const repId = document.getElementById("repId_" + parentID);


    let closestCard = repId.closest(".comment-container");
    closestCard.appendChild(createInputBox(parentID, level));
}



function repCommment(parentId) {
    const formChild = document.getElementById('comment_to_project' + parentId);

    const formData = new FormData(formChild); // create a new FormData object from the form
    const xhr = new XMLHttpRequest(); // create a new XMLHttpRequest object

    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE) {
            // when the request is complete
            if (xhr.status === 200) { // if the response status is OK
                console.log(xhr.responseText); // log the response text to the console
            } else {
                console.error(xhr.statusText); // log the error status text to the console
            }
        }
    };

    xhr.open(formChild.method, formChild.action); // set the request method and URL
    xhr.setRequestHeader("Accept", "application/json"); // set the Accept header to application/json
    xhr.send(formData); // send the form data to the server

    const commentContent = document.querySelector("#comment_to_project_box" + parentId);


    commentContent.remove();
}