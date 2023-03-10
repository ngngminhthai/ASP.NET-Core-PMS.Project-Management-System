

var projectId;


function setId(id) {
    projectId = id;
}
var authorEmail;

function setAuthor(email) {
    authorEmail = email;
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
                var className = "comment d-flex justify-content-between";
                if (listComment[i].Level === 1) { className = "comment rep d-flex justify-content-between" }
                else if (listComment[i].Level >= 2) { className = "comment rep2 d-flex justify-content-between" };


                const value = (listComment[i].level == 2) ? listComment[i].parentID + "," + (listComment[i].Level - 1) : listComment[i].Id + "," + listComment[i].Level;

                let canDelete = (authorEmail === listComment[i].Author) ?
                    "<form id='delete_comment" + listComment[i].Id + "' action='/Projects/DeleteComment'"
                    + "method = 'post' accept - charset='utf-8' > "
                    + " <input type='hidden' name='id' value='" + listComment[i].Id + "' />"
                    + " <a style='margin-left: 20px;' onclick=' deleteComment(  " + listComment[i].Id + ")' class='reply'><i class='fa fa-trash-o' aria-hidden='true'></i></a>"
                    + "</form> " :
                    "";
                let canUpdate = (authorEmail === listComment[i].Author) ?

                    " <a style='margin-left: 20px;' onclick='clickUpdate(  " + listComment[i].Id + ")' class='reply'><i class='fa fa-pencil-square-o' aria-hidden='true'></i></a>" :
                    "";
                tableContent +=
                    "<div class='comment-container'>"
                    + "<div class='" + className + "'>"
                    + "                     <div class=' d-flex'>"
                    + "           <div class='comment-pic'>"
                    + "                <img src='/images/avatar/cmt-02.png' alt=''>"
                    + "            </div>"
                    + "            <div class='comment-body'>"
                    + "                <div class='name'>"
                    + "                  <h5 class='font-w600 fs-18'>" + listComment[i].Author + "</h5>"
                    + "                  <p Id='content_" + listComment[i].Id + "' class='text mb-0 fs-18'>"
                    + listComment[i].Content
                    + "                  </p>"
                    + "                                                 </div>"
                    + "                                             </div>"
                    + "                                         </div>"
                    + "                                         <div class=''>"
                + "                                             <div class='group-action d-flex'>"
                    + canDelete
                    + canUpdate
                    + "                                                 <a style='margin-left: 20px;' id='repId_" + listComment[i].Id + "' onclick='clickReply(  "
                    + value + ")' class='reply' ><i class='fa fa-reply' aria-hidden='true'></i></a>"
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
    //  var childLevel = level + 1;
    let div = document.createElement("div");
    div.setAttribute("class", "form-chat rep" + level + " d-flex");
    div.setAttribute("id", "comment_to_project_box" + parentID);

    div.innerHTML += "<form style='width:100%' class='comment_rep' id='comment_to_project" + parentID + "' action='/Projects/CreateComment' method='post' accept-charset='utf-8'>"
        + "<input type = 'hidden' name = 'comment.ParentID' value = " + parentID + " />"
        + " <input type='hidden' name='comment.Level' value=" + level + 1 + " />"
        + "  <input type='hidden' name='comment.NumberOfLike' value='0' />"
        + " <div class='message-form-chat'>"

        + "   <span class='message-text'>"
        + "     <textarea placeholder='Type comment here' required='required' name='comment.Content' ></textarea>"
        + "  </span>"


        + " <span class='btn-send'>"
        + "    <button  onclick=' repCommment(  " + parentID + ")'  class='waves-effect'>Send <i class='fas fa-paper-plane'></i></button>"
        + " </span>"


        + "   </div> "

        + "</form > ";

    return div;
}

function clickReply(parentID, level) {
    const delElement = document.getElementById("comment_to_project_box" + parentID);
    if (delElement !== null) {
        delElement.remove();
        return
    }
    const delElementUpddate = document.getElementById("update_comment_box" + parentID);
    if (delElementUpddate !== null) {
        delElementUpddate.remove();
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

function deleteComment(id) {
    const formChild = document.getElementById('delete_comment' + id);
    // console.log("delete");
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
}

function clickUpdate(id) {

    const delElement = document.getElementById("comment_to_project_box" + id);
    if (delElement !== null) {
        delElement.remove();
        return
    }
    const delElementUpddate = document.getElementById("update_comment_box" + id);
    if (delElementUpddate !== null) {
        delElementUpddate.remove();
        return;
    }
    const content = document.getElementById("content_" + id).textContent;
 
    const repId = document.getElementById("repId_" + id);
    let closestCard = repId.closest(".comment-container");
    closestCard.appendChild(createInputUpdateBox(id, content));
}

function createInputUpdateBox(id, content) {
    //  var childLevel = level + 1;
    let div = document.createElement("div");
    div.setAttribute("class", "form-chat rep2 d-flex");
    div.setAttribute("id", "update_comment_box" + id);

    div.innerHTML += "<form style='width:100%' class='comment_rep' id='update_comment" + id + "' action='/Projects/UpdateComment' method='post' accept-charset='utf-8'>"
        + "<input type = 'hidden' name = 'id' value = " + id + " />"

        + " <div class='message-form-chat'>"
        + "   <span class='message-text'>"
        + "     <textarea placeholder='Type comment here' required='required' name='content' >" + content + "</textarea>"
        + "  </span>"


        + " <span class='btn-send'>"
        + "    <button onclick=' updateComment(  " + id + ")' >Update <i class='fas fa-paper-plane'></i></button>"
        + " </span>"


        + "   </div> "

        + "</form> ";

    return div;
}
function updateComment(id) {
    console.log(id);
    const formChild = document.getElementById("update_comment" + id);
    const formData = new FormData(formChild); // create a new FormData object from the form // create a new FormData object from the form
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

    const commentcontent = document.querySelector("#update_comment_box" + id);


    commentcontent.remove();
}