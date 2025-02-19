const back_to_top_bt = document.getElementById("back-to-top");
const header_height = document.querySelector("header").offsetHeight;
const header_nav_height = document.querySelector(".header-nav").offsetHeight;
const tag_nav_height = document.querySelector("#hot-tag-nav").offsetHeight;
const target_ele = document.getElementById("hot-tag-nav");
var height = header_height + header_nav_height + tag_nav_height;

window.onscroll = function() {
    if (document.body.scrollTop > height ||
        document.documentElement.scrollTop > height) 
    {
        back_to_top_bt.style.display = "block"; 
    } else {
        back_to_top_bt.style.display = "none"; 
    }
};

back_to_top_bt.onclick = function () {
    target_ele.scrollIntoView({
      block: "start"
    });
};

document.addEventListener("DOMContentLoaded", function () {
    const elements = document.querySelectorAll("p, span, div, h1, h2, h3, h4, h5, h6");

    elements.forEach(el => {
        if (/[ก-๙]/.test(el.textContent)) { 
            el.style.fontFamily = '"Noto Sans Thai", serif';
        }
    });
});