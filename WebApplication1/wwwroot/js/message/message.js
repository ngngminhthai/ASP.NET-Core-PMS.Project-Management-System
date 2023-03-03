

$(document).ready(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
    connection.start();

    //render chat module components

    function renderPage(conversationId) {
        if (conversationId != undefined) {
            renderConversation(conversationId);
            renderMessage(conversationId);
            groupId = conversationId;
        }
        else {
            renderConversation(groupId);
            renderMessage(groupId);
        }
       
    }



    renderPage();

  

    

    $('.btn-send').click(async function () {
        const textareaValue = $('#message').val();
        const groupName = "g" + groupId;
        connection.invoke("SendMsg", groupName, textareaValue).then(function (result) {
        });
    });


    connection.on("ReceiveMessage", async function (message) {
        console.log("Received message: " + message);
        await addMessage(groupId, message, currentUserId)
        renderMessage(groupId); // Call the renderMessage function
    });

    async function addMessage(conversationId, textareaValue, currentUserId) {
        return new Promise((resolve, reject) => {
            $.ajax({
                type: 'POST',
                url: '/Conversation/AddMessage',
                data: {
                    conversationId: conversationId,
                    text: textareaValue,
                    senderId: currentUserId
                },
                success: function (data, textStatus, jqXHR) {
                    resolve(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    reject(errorThrown);
                }
            });
        });
    }


    function renderConversation(groupId) {
        $.ajax({
            type: 'GET',
            url: '/Conversation/ConversationsOfUser',
            data: {
                id: currentUserId,
            },
            success: function (data, textStatus, jqXHR) {
                console.log(data);
                var tableContent = "";
                $.each(data, function (index, c) {
                    tableContent += `
                        


                                   <li class="waves-effect waves-teal ${groupId == c.Id ? "active" : ""}"  data-id="${c.Id}">
                                        <div class="left d-flex">
                                            <div class="avatar">
                                                <img src="/images/avatar/message-2.png" alt="">
                                                <div class="pulse-css-1"></div>
                                            </div>
                                            <div class="content">
                                                <div class="username">
                                                    <div class="name h6">
                                                        ${c.Name}
                                                    </div>
                                                </div>
                                                <div class="text">
                                                    <p>${c.Description}</p>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <!-- /.left -->

                                        <div class="clearfix"></div>
                                    </li>
                                   
                    `
                });
                $('.message-list').html(tableContent);
                var liElements = document.querySelectorAll("li.waves-effect");
                for (var i = 0; i < liElements.length; i++) {
                    liElements[i].addEventListener("click", function () {
                        var conversationId = this.getAttribute("data-id");
                        renderPage(conversationId);
                    });
                }
            }
        });
    }

    function renderMessage(conversationId) {

        $.ajax({
            type: 'GET',
            url: '/Conversation/GetMessageOfConversation',
            data: {
                id: conversationId,
            },
            success: function (data, textStatus, jqXHR) {
                console.log(data);
                var tableContent = "";
                $.each(data, function (index, msg) {
                    if (msg && msg.SenderId != currentUserId) {
                        tableContent += `
                      
                      <div class="message-in">
                                        <div class="message-pic">
                                            <img src="./images/avatar/message-1.png" alt="">
                                            <div class="pulse-css-1"></div>
                                        </div>
                                        <div class="message-body">
                                            <div class="message-text">
                                                <p>${msg.Text}</p>
                                            </div>
                                            <div class="message-meta">
                                                <p class="mt-10">Sunday, march 17, 2021 at 2:39 PM</p>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                      <div class="clearfix"></div>
                    `
                    }
                    else {
                        tableContent += `
                     <div class="message-out">
                                        <div class="message-pic">
                                            <img src="./images/profile/profile.png" alt="">
                                            <div class="pulse-css-1"></div>
                                        </div>
                                        <div class="message-body">
                                            <div class="message-text">
                                        <p>${msg.Text}</p>
                                    </div>
                                            <div class="message-meta">
                                                <p class="mt-10">Sunday, march 17, 2021 at 2:45 PM</p>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                      <div class="clearfix"></div>
                    `
                    }
                });
                $('.message-box').html(tableContent);
            }
        });
    }

});
