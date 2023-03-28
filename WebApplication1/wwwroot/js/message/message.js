

$(document).ready(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
    connection.start();


    //render chat module components

    async function renderPage(conversationId) {
        if (conversationId != undefined) {
            await renderConversation(conversationId);
            renderMessage(conversationId);
            groupId = conversationId;
        }
        else {
            await renderConversation(groupId);
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
        //renderMessage(groupId); // Call the renderMessage function
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
                    var messageBox = $('.message-box'); // select the element with class "message-box"
                    console.log(data);

                    var message = ''; // variable to hold the message box

                    if (data && data.SenderId != currentUserId) {
                        // create incoming message box
                        message = `
            <div class="message-in">
                <div class="message-pic">
                    <img style="height: 60px; width: 60px; object-fit: cover" src="./uploads/${data.User.ImageProfile}" alt="">
                    <div class="pulse-css-1"></div>
                </div>
                <div class="message-body">
                    <div class="message-text">
                        <p>${data.Text}</p>
                    </div>
                    <div class="message-meta">
                        <p class="mt-10">${data.DateCreated}</p>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="clearfix"></div>
        `
                    }
                    else {
                        // create outgoing message box
                        message = `
            <div class="message-out">
                <div class="message-pic">
                    <img style="height: 60px; width: 60px; object-fit: cover" src="./uploads/${data.User.ImageProfile}" alt="">
                    <div class="pulse-css-1"></div>
                </div>
                <div class="message-body">
                    <div class="message-text">
                        <p>${data.Text}</p>
                    </div>
                    <div class="message-meta">
                        <p class="mt-10">${data.DateCreated}</p>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="clearfix"></div>
        `
                    }

                    messageBox.append(message); // append the message box to the element with class "message-box"
                    var scrollHeight = messageBox[0].scrollHeight;
                    if (scrollHeight > 0) {
                        messageBox[0].scrollTop = scrollHeight;
                    }
                    resolve(data);
                },

                error: function (jqXHR, textStatus, errorThrown) {
                    reject(errorThrown);
                }
            });
        });
    }



    const $messageBox = $('.message-box');


    $messageBox.on('scroll', function () {

        // Check if the scrollbar is at the top of the message box
        if ($messageBox.scrollTop() === 0) {
            console.log('Scrollbar is at the top');
            loadMessage(groupId);


        }
    });

    function loadMessage(conversationId) {
        const messageOutCount = $('.message-out').length;
        const messageInCount = $('.message-in').length;
        const total = messageOutCount + messageInCount;

        $.ajax({
            type: 'GET',
            url: '/Conversation/GetMessageOfConversation',
            data: {
                id: conversationId,
                skipCount: total
            },
            success: function (data, textStatus, jqXHR) {
                console.log(data);
                var tableContent = "";
                $.each(data, function (index, msg) {
                    if (msg && msg.SenderId != currentUserId) {
                        tableContent += `
            <div class="message-in">
              <div class="message-pic">
                    <img style="height: 60px; width: 60px; object-fit: cover" src="./uploads/${msg.User.ImageProfile}" alt="">
                <div class="pulse-css-1"></div>
              </div>
              <div class="message-body">
                <div class="message-text">
                  <p>${msg.Text}</p>
                </div>
                <div class="message-meta">
                  <p class="mt-10">${msg.DateCreated}</p>
                </div>
              </div>
              <div class="clearfix"></div>
            </div>
            <div class="clearfix"></div>
          `;
                    } else {
                        tableContent += `
            <div class="message-out">
              <div class="message-pic">
                    <img style="height: 60px; width: 60px; object-fit: cover" src="./uploads/${msg.User.ImageProfile}" alt="">
                <div class="pulse-css-1"></div>
              </div>
              <div class="message-body">
                <div class="message-text">
                  <p>${msg.Text}</p>
                </div>
                <div class="message-meta">
                  <p class="mt-10">${msg.DateCreated}</p>
                </div>
              </div>
              <div class="clearfix"></div>
            </div>
            <div class="clearfix"></div>
          `;
                    }
                });
                // Prepend the new messages to the top of the existing messages
                $('.message-box').prepend(tableContent);

                setTimeout(function () {
                    var messageBox = $('.message-box')[0];
                    messageBox.scrollTop = 20;
                }, 500);

            }
        });
    }

    /* function addMessage(groupId) {
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
     }*/


    var conversationsResponse;
    async function renderConversation(groupId) {
        return new Promise((resolve, reject) => {
            $.ajax({
                type: 'GET',
                url: '/Conversation/ConversationsOfUser',
                data: {
                    id: currentUserId,
                },
                success: function (data, textStatus, jqXHR) {
                    conversationsResponse = data;
                    var tableContent = "";
                    $.each(data, function (index, c) {
                        tableContent += `
                    
                                   <li class="waves-effect waves-teal ${groupId == c.Id ? "active" : ""}"  data-id="${c.Id}">
                                        <div class="left d-flex">
                                           
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
                    resolve(data);
                }
            });
        }
    )};

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
                    <img style="height: 60px; width: 60px; object-fit: cover" src="./uploads/${msg.User.ImageProfile}" alt="">
                                            <div class="pulse-css-1"></div>
                                        </div>
                                        <div class="message-body">
                                            <div class="message-text">
                                                <p>${msg.Text}</p>
                                            </div>
                                            <div class="message-meta">
                  <p class="mt-10">${msg.DateCreated}</p>
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
                    <img style="height: 60px; width: 60px; object-fit: cover" src="./uploads/${msg.User.ImageProfile}" alt="">
                                            <div class="pulse-css-1"></div>
                                        </div>
                                        <div class="message-body">
                                            <div class="message-text">
                                        <p>${msg.Text}</p>
                                    </div>
                                            <div class="message-meta">
                  <p class="mt-10">${msg.DateCreated}</p>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                      <div class="clearfix"></div>
                    `
                    }
                });
                $('.message-box').html(tableContent);
                var messageBox = $('.message-box')[0];
                var scrollHeight = messageBox.scrollHeight;
                if (scrollHeight > 0) {
                    messageBox.scrollTop = scrollHeight;
                }
                
                let conversation = conversationsResponse.find(conversation => conversation.Id == conversationId);
                $(".group-name").html(conversation.Name)
                $(".group-description").html(conversation.Description)
                $('a#CurrentConversation').attr('href', `/Conversations/Member?id=${conversation.Id}`);

            }
        });
    }

});
