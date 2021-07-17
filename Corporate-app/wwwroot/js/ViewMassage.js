const dataUser = document.getElementById("user");
const user = dataUser.dataset.user;
const conteinerHistory = document.getElementById("chatroom");


function ViewMessage(textMessage) {
    var tempText = textMessage.split(":");
    textMessage = tempText[0] + ": " + "\n" + tempText[1];
    messageContainer = document.createElement("div");

    let elem = document.createElement("p");

    if (tempText[0] != user) {
        elem.appendChild(document.createTextNode(tempText[0] + ": "));

        messageContainer.style.border = '2px solid #dedede';
        messageContainer.style.background = '#f1f1f1';
        messageContainer.style.border.radius = '5px';
        messageContainer.style.padding = '5px';
        messageContainer.style.margin = '10px 10px';
    } else {
        elem.appendChild(document.createTextNode("You: "));

        messageContainer.style.border = '2px solid #006666';
        messageContainer.style.background = '#CCFFFF';
        messageContainer.style.border.radius = '5px';
        messageContainer.style.padding = '5px';
        messageContainer.style.margin = '10px 10px';
    }
    

    elem.appendChild(document.createElement("br"));
    elem.appendChild(document.createTextNode(tempText[1]));
    messageContainer.appendChild(elem);
    conteinerHistory.appendChild(messageContainer);
}