async function LinkToChat(Name) {
    idUser = 0;
    var data = {
        NameChat: Name,
        IdUser: idUser,
    };
    var response = await apiFetch('ChatsApi/CheckChat', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(data)
    });

    if (response.ok) {
        let name = (await response.json());
        console.log(name);
        console.log("Okey response");
        document.location.href = "/Chats/Chat?actualChat=" + name.name;

    } else {
        console.log("NOT okey response");
    }
}