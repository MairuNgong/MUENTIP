document.addEventListener("DOMContentLoaded", function () {
  // ดึงปุ่มทั้งหมดจากเมนู
  const buttons = document.querySelectorAll(".activity-menu li a");
  const createContainer = document.querySelector(".create-container");

  // กำหนดค่าเริ่มต้นของ Active Tab จาก localStorage
  const savedTab = localStorage.getItem("activeTab") || "createButton";
  setActiveTab(savedTab);

  //Event Listener
  document.getElementById("createButton")?.addEventListener("click", function () {
    setActiveTab("createButton");
  });

  document.getElementById("approvedButton")?.addEventListener("click", function () {
    setActiveTab("approvedButton");
  });

  document.getElementById("non-approvedButton")?.addEventListener("click", function () {
    setActiveTab("non-approvedButton");
  });

  function setActiveTab(selectedButtonId) {
    // ลบคลาส active จากทุกปุ่ม
    buttons.forEach(button => button.classList.remove("active"));

    // เพิ่มคลาส active ให้ปุ่มที่เลือก
    const selectedButton = document.getElementById(selectedButtonId);
    if (selectedButton) {
      selectedButton.classList.add("active");
      localStorage.setItem("activeTab", selectedButtonId); // บันทึกค่า activeTab
    }

    // แสดง/ซ่อน create-container ตามปุ่มที่เลือก
    if (selectedButtonId === "createButton") {
      createContainer.style.display = "flex";
    } else {
      createContainer.style.display = "none";
    }
  }

  // ปุ่ม create-button ลิงก์ไปหน้าอื่น
  document.getElementById("create-button")?.addEventListener("click", function () {
    window.location.href = "../view_activity_page/view_activity_page.html";
  });
});
