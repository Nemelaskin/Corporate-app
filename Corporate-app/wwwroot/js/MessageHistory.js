

ViewMesssages();
function ViewMesssages() {
    TakeMessageHistory();
}

async function TakeMessageHistory() {
    var response = await apiFetch('MessageLists/GetChatMessageLists?nameChat=' + myNameChat, {
        method: 'GET'
    });
    var list = (await response.json());
    var ds = list[0].Сontent;
    for (var i = 0; i < list.length; i++) {
        var text = list[i].Сontent;
        ViewMessage(text);
    }
}