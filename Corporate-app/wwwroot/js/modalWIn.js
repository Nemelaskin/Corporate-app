var modal = document.getElementById("myModal");

var btn = document.getElementById("myBtn");

var span = document.getElementsByClassName("close")[0];

btn.onclick = function () {
    modal.style.display = "block";
    contentModal();
}

span.onclick = function () {
    modal.style.display = "none";
}

window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

function contentModal() {
    var conteiner = document.getElementById("container");
    var firstElm = document.createElement("h2");
    firstElm.textContent = "testing text";
    conteiner.append(firstElm);
}