const dataName = document.getElementById("dataName").innerHTML;

hubConnection.on("SendMessageForGroupTest", function (data) {
    ViewMessage(data);
});

document.getElementById("sendBtn").addEventListener("click", function (e) {
    let message = document.getElementById("message").value;
    if (message != "") {
        hubConnection.invoke("SendMessageForGroup", message, dataName);
        document.getElementById("message").value = "";
        addMessage(message);
    }
    
});

hubConnection.start();