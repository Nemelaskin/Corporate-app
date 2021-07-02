const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chats")
    .build();

