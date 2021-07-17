const urlParams = new URLSearchParams(window.location.search);
const myParam = urlParams.get('actualChat');

const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chats?chatName=" + myParam)
    .build();

