
//// header
//let label = document.querySelector(".account .username");
//let showList = document.querySelector(".list-account");
////label.addEventListener("click", (e) => {
////    showList.classList.toggle("show-list")
////    e.stopPropagation()
////})

//document.addEventListener("click", (e) => {
//    if (e.target !== label) {
//        showList.classList.remove("show-list")
//    }
//})

//let labelIcon = document.querySelector(".account .username-icon");
////labelIcon.addEventListener("click", (e) => {
////    showList.classList.toggle("show-list")
////    e.stopPropagation()
////})

//document.addEventListener("click", (e) => {
//    if (e.target !== labelIcon) {
//        showList.classList.remove("show-list")
//    }
//})

//// sidebar
//let barIcon = document.querySelector('.li-bar-icon');
//let TextLinks = document.querySelectorAll('.text-links');
//let showListLi = document.querySelectorAll(".list-account a span");
//let showListSvg = document.querySelectorAll(".list-account a");
//let searchForm = document.querySelector(".search-form");

//barIcon.addEventListener('click', () => {

//    window.localStorage.setItem("show", "show")
//    barIcon.classList.toggle("show");

//    document.querySelector(".larg-logo").classList.toggle("hide-larglogo");
//    document.querySelector(".min-logo").classList.toggle("show-minlogo");
//    document.querySelector(".account .username-icon").classList.toggle("username-icon-show");
//    document.querySelector(".account .username").classList.toggle("username-hide");

//    if (searchForm) {
//        document.querySelector(".search-form").classList.toggle("username-hide");
//    }


//    showListSvg.forEach((svg) => {
//        svg.classList.toggle("list-menu-svg");
//    });

//    showListLi.forEach((li) => {
//        li.classList.toggle("username-hide");
//    });

//    TextLinks.forEach((t) => {
//        t.classList.toggle("onclick-links");
//    });

//    document.querySelector('.side-bar').classList.toggle("onclick-side-bar");
//    document.querySelectorAll(".icon-side-bar").forEach((i) => {
//        i.classList.toggle("onclick-icon-link");
//    })

//    document.querySelector(".bar-icon").classList.toggle("onclick-bar-icon");

//    if (barIcon.classList.contains("show")) {
//        window.localStorage.removeItem("show")
//    }

//})

//if (!window.localStorage.getItem("show")) {

//    barIcon.classList.add("show")
//    document.querySelector(".larg-logo").classList.add("hide-larglogo");
//    document.querySelector(".min-logo").classList.add("show-minlogo");
//    document.querySelector(".account .username-icon").classList.add("username-icon-show");
//    document.querySelector(".account .username").classList.add("username-hide");

//    if (searchForm) {
//        document.querySelector(".search-form").classList.add("username-hide");
//    }

//    showListSvg.forEach((svg) => {
//        svg.classList.toggle("list-menu-svg");
//    });

//    showListLi.forEach((li) => {
//        li.classList.toggle("username-hide");
//    });

//    TextLinks.forEach((t) => {
//        t.classList.add("onclick-links");
//    });

//    document.querySelector('.side-bar').classList.add("onclick-side-bar");
//    document.querySelectorAll(".icon-side-bar").forEach((i) => {
//        i.classList.add("onclick-icon-link");
//    });

//    document.querySelector(".bar-icon").classList.add("onclick-bar-icon");
//}
$(document).ready(function () {
    $('select').select2();
});