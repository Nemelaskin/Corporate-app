const modalBody = document.getElementById("modalbody");
const dataObject = document.getElementById("arrayUsers");
const jsonData = dataObject.dataset.array;
const arrayUsers = JSON.parse(jsonData);

function CreatePersonalChat() {
    contentModal();
}

function CreateGroupChat() {
    modalBody.innerHTML = "";
}

function contentModal() {
    modalBody.innerHTML = "";
    let conteiner = document.createElement("div");
    let btnGroupCreate = document.createElement('button');

    for (let i = 0; i < arrayUsers.length; i++) {
        let btnGroupCreate = document.createElement('button');
        btnGroupCreate.textContent = arrayUsers[i].Name + " " + arrayUsers[i].SurName;
        btnGroupCreate.classList = "list-group-item list-group-item-action";
        btnGroupCreate.onclick = async () => {
            console.log(arrayUsers[i].Name + " " + arrayUsers[i].SurName);
            let actualChat = await CheckChat(arrayUsers[i].Name + " " + arrayUsers[i].SurName, arrayUsers[i].UserId);
        };
        conteiner.append(btnGroupCreate);
    }
    modalBody.append(conteiner);
}

async function CheckChat(nameChat, idUser) {
    var data = {
        NameChat: nameChat,
        IdUser: idUser,
    };
    var response = await apiFetch('ChatsApi/CheckChat?nameChat=' + nameChat, {
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
        CreateChat(nameChat, idUser);
    }
}

async function CreateChat(nameChat, idUser) {
    var data = {
        NameChat: nameChat,
        IdUser: idUser,
    };
    response = await apiFetch('ChatsApi/CreateChat', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(data)
    });
    document.location.href = "/Chats/AllChats";
}