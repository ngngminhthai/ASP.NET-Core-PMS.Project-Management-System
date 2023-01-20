$(document).ready(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
    connection.start();


    $('.btn-send').click(function () {
        connection.invoke("SendMsg", "C1").then(function (result) {

          
            
        });
    });


    connection.on("ReceiveMessage", function (message) {
        console.log("Received message: " + message);
    });

});
