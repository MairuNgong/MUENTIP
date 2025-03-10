// ดึงปุ่มทั้งหมดจากเมนู
const buttons = document.querySelectorAll(".activity-menu li a");
const createContainer = document.querySelector(".create-container");
const container = document.getElementById("container");

//gobal
let activities = [];

// กำหนดค่าเริ่มต้นของ Active Tab จาก localStorage
const savedTab = localStorage.getItem("activeTab") || "createButton";
setActiveTab(savedTab);

// กำหนด Event Listeners สำหรับปุ่มเลือกแท็บ
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
        localStorage.setItem("activeTab", selectedButtonId); // บันทึกค่า activeTab
    }

    // แสดง/ซ่อน create-container ตามปุ่มที่เลือก
    createContainer.style.display = selectedButtonId === "createButton" ? "flex" : "none";

    // ตรวจสอบค่า tab ที่ได้รับ และเลือกกิจกรรมที่จะแสดง
    if (selectedButtonId === "createButton") {
        activities = createdActivity;
    }
    else if (selectedButtonId === "approvedButton") {
        activities = approvedActivity;
    }
    else if (selectedButtonId === "non-approvedButton") {
        activities = nonApproveActivity;
    }

    // // เรียกใช้ฟังก์ชัน renderActivities ใหม่หลังจากเปลี่ยนแท็บ
    renderActivities(container, tags);
}

const back_to_top_bt = document.getElementById("back-to-top");
const header_height = document.querySelector("header").offsetHeight;
const header_nav_height = document.querySelector(".header-nav").offsetHeight;
// const tag_nav_height = document.querySelector("#hot-tag-nav").offsetHeight;
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