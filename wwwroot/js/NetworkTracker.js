
// netowrk minitor page
let selectStatus = document.querySelector(".select-status");
let showListStatus = document.querySelector(".list-status");
let textSelectStatus = document.querySelector(".select-status p");
let liListStatus = document.querySelectorAll(".list-status li");
// to handel active css style
liListStatus.forEach((li) => {
    li.addEventListener("click", () => {
        liListStatus.forEach((li) => li.classList.remove("list-active"));
        li.classList.add("list-active");
        textSelectStatus.textContent = document.querySelector(".list-active a").textContent;
    });
});
// to show and hide list of status
selectStatus.addEventListener("click", (e) => {
    showListStatus.classList.toggle("show-list")
    e.stopPropagation()
});

document.addEventListener("click", (e) => {
    if (e.target !== selectStatus) {
        showListStatus.classList.remove("show-list")
    }
});
