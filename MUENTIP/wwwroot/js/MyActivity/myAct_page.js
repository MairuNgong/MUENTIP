
const buttons = document.querySelectorAll(".activity-menu li a");
const createContainer = document.querySelector(".create-container");
const container = document.getElementById("container");

window.addEventListener("pageshow", function (event) {
    let perfEntries = performance.getEntriesByType("navigation");
    let historyTraversal = event.persisted || (perfEntries.length && perfEntries[0].type === "back_forward");

    if (historyTraversal) {
        window.location.reload();
    }
});


let activities = [];


const savedTab = localStorage.getItem("activeTab") || "createButton";
setActiveTab(savedTab);


document.getElementById("createButton")?.addEventListener("click", function () {
    setActiveTab("createButton");
});
document.getElementById("approvedButton")?.addEventListener("click", function () {
    setActiveTab("approvedButton");
});
document.getElementById("non-approvedButton")?.addEventListener("click", function () {
    setActiveTab("non-approvedButton");
});

document.getElementById("create-button")?.addEventListener("click", function () {
    window.location.href = "/CreateActivity/";
});

function setActiveTab(selectedButtonId) {
    buttons.forEach(button => button.classList.remove("active"));

    const selectedButton = document.getElementById(selectedButtonId);
    if (selectedButton) {
        selectedButton.classList.add("active");
        localStorage.setItem("activeTab", selectedButtonId); 
    }

    
    createContainer.style.display = selectedButtonId === "createButton" ? "flex" : "none";


    if (selectedButtonId === "createButton") {
        activities = createdActivity;
    }
    else if (selectedButtonId === "approvedButton") {
        activities = approvedActivity;
    }
    else if (selectedButtonId === "non-approvedButton") {
        activities = nonApproveActivity;
    }

    
    renderActivities(container, tags);
}

const back_to_top_bt = document.getElementById("back-to-top");
const header_height = document.querySelector("header").offsetHeight;
const header_nav_height = document.querySelector(".header-nav").offsetHeight;

const target_ele = document.getElementById("activity-nav");
var height = header_height + header_nav_height;

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


const elements = document.querySelectorAll("p, span, div, h1, h2, h3, h4, h5, h6");

elements.forEach(el => {
    if (/[ก-๙]/.test(el.textContent)) {
        el.style.fontFamily = '"Noto Sans Thai", serif';
    }
});