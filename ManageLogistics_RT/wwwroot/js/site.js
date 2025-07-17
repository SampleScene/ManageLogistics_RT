// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


/*
$(".sidebar ul li").on('click', function () {
    $(".sidebar ul li.active").removeClass('active');
    $(this).addClass('active');
    console.log(this);
});
*/

function test() {

    let url = window.location.href;

    Array.from(document.querySelectorAll(".sidebar ul li")).forEach(element => {

        if (url.includes(element.firstChild.href)) {
            element.classList.add("active");
        }

    });

}
test();

function btn_click() {
    const stop = document.getElementById("stop1");
    stop.remove();
    const article = document.createElement("div");
    article.classList.add("createdRouteItem");
    article.appendChild(stop);
    const inputField = document.createElement("input");
    inputField.style = ""
    inputField.type = "text";
    article.appendChild(inputField);
    const createdRoute = document.getElementById("createdRoute");
    createdRoute.appendChild(article);
    
    //console.log(article);
}
