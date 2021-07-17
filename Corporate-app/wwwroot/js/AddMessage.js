const urlNameChat = new URLSearchParams(window.location.search);
const myNameChat = urlNameChat.get('actualChat');

async function addMessage(message) {
    var messageReq = {
        nameChat: myNameChat,
        content: message,
    };
    var response = await apiFetch('MessageLists/PostMessageList', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(messageReq)
    });
}