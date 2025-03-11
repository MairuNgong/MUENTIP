var Appliers = JSON.parse(JSON.stringify(selectModel)).appliers;
var ApplyMax = JSON.parse(JSON.stringify(selectModel)).applyMax;
var activity_id = JSON.parse(JSON.stringify(selectModel)).activity_id;

window.addEventListener("pageshow", function (event) {
    let perfEntries = performance.getEntriesByType("navigation");
    let historyTraversal = event.persisted || (perfEntries.length && perfEntries[0].type === "back_forward");

    if (historyTraversal) {
        window.location.reload();
    }
});

console.log(activity_id)
console.log(selectModel)
console.log(ApplyMax)

function renderApplier(container) {
    // Remove all child elements from the container to overwrite it
    container.innerHTML = "";

    // Track the total number of activities (checkboxes)
    const totalCheckboxes = Appliers.length;
    // Iterate over the activities (we are only displaying owner info now)
    Appliers.forEach(applicant => {
        const applicant_card = document.createElement("div");
        applicant_card.className = "applicant_card";

        const left_group = document.createElement("div");
        left_group.className = "left_group";

        const left_content_text = document.createElement("div");
        left_content_text.className = "left_content_text";

        const owner = document.createElement("div");
        const img_people = document.createElement("img");
        img_people.className = "icon";
        if (applicant.userImgLink != null) {
            img_people.src = applicant.userImgLink; // Display the applicant's profile picture
        }
        else {
            img_people.src = "/img/default-profile.png"
        }

        img_people.addEventListener("click", function () {
            window.location.href = `/Profile/Index?id=${applicant.userId}`;
        });

        owner.className = "applicant_detail";
        owner.appendChild(img_people);

        const owner_text = document.createElement("span");
        owner_text.textContent = `${applicant.userName}`; // Display username and email
        owner_text.style.marginLeft = "10px"; // Adjust the value as needed

        owner.appendChild(owner_text);

        left_content_text.appendChild(owner);

        left_group.appendChild(left_content_text);

        // Add checkbox
        const checkboxWrapper = document.createElement("div");
        checkboxWrapper.className = "checkbox_wrapper";

        const checkbox = document.createElement("input");
        checkbox.type = "checkbox";
        checkbox.className = "checkbox";
        checkbox.id = `checkbox-${applicant.userName}`; // Set ID with userName
        checkbox.style.marginLeft = "240px";


        checkboxWrapper.appendChild(checkbox);
        left_group.appendChild(checkboxWrapper);

        applicant_card.appendChild(left_group);

        container.appendChild(applicant_card);

    });

    document.getElementById("confirmBtn").addEventListener("click", async function () {
        // หาชื่อของ checkbox ที่ถูกเลือก
        const selectedCheckboxes = document.querySelectorAll(".checkbox:checked");
        const Button = document.getElementById("confirmBtn");
        Button.disabled = true;
        Button.textContent = "Sending...";
        Button.style.backgroundColor = "#45a049";
        Button.style.cursor = "not-allowed";
        // สร้าง Set สำหรับเก็บ user_id ที่ถูกส่งไปแล้ว
        const sentUserIds = new Set();
        console.log("Sending");
        // วนลูปผ่าน checkbox ที่ถูกเลือก
        for (const checkbox of selectedCheckboxes) {
            // หา applicant ที่ตรงกับ checkbox โดยใช้ id ของ checkbox
            const userName = checkbox.id.replace("checkbox-", "");  // ดึงชื่อผู้ใช้ออกจาก id ของ checkbox
            const applicant = Appliers.find(app => app.userName === userName); // หา applicant ที่ตรงกับชื่อผู้ใช้

            if (applicant) {
                const user_id = applicant.userId;

                // ตรวจสอบว่าผู้สมัครคนนี้ถูกส่งข้อมูลไปแล้วหรือยัง
                if (sentUserIds.has(user_id)) {
                    // ข้ามไปหากส่งข้อมูลไปแล้ว
                    continue;
                }

                // ถ้ายังไม่เคยส่งข้อมูล, เพิ่ม user_id ใน Set
                sentUserIds.add(user_id);

                const activity_id = JSON.parse(JSON.stringify(selectModel)).activity_id; // เช่นเดียวกับก่อนหน้า

                // สร้าง FormData และเพิ่มข้อมูลที่ต้องการ
                const formData = new FormData();
                formData.append("user_id", user_id);
                formData.append("activity_id", activity_id);

                // ส่งคำขอ POST ไปยังเซิร์ฟเวอร์
                try {
                    const response = await fetch("/Select/select_create", {
                        method: "POST",
                        body: new URLSearchParams(formData),
                        headers: { "Content-Type": "application/x-www-form-urlencoded" }
                    });

                    // รับผลลัพธ์จากเซิร์ฟเวอร์ (ถ้าต้องการ)
                    const data = await response.json();
                    console.log(data);
                } catch (error) {
                    console.error('Error:', error);
                    Button.disabled = false;
                    Button.textContent = "Confirm";
                    Button.style.backgroundColor = "";
                    Button.style.cursor = "pointer";
                }
            }

        }
        console.log("ApplyOn created successfully!");

        const overlay = document.getElementById("overlay");
        const popup = document.getElementById("notify-popup");
        const progressBar = document.getElementById("progress-bar");

        overlay.style.display = "block";
        popup.style.display = "flex";
        popup.classList.remove("fade-out");
        overlay.classList.remove("fade-out");
        progressBar.style.width = "0%";

        setTimeout(() => {
            progressBar.style.width = "100%";
        }, 50);

        setTimeout(() => {
            popup.classList.add("fade-out");
            overlay.classList.add("fade-out");

            setTimeout(() => {
                popup.style.display = "none";
                overlay.style.display = "none";
                window.location.reload();
            }, 1000);
        }, 1000);
        setTimeout(function () {
            window.location.href = '/Home';
        }, 500);  // รอ 2 วินาที (2000 มิลลิวินาที)
    });



    // Add event listeners to checkboxes after rendering
    document.querySelectorAll(".checkbox").forEach(checkbox => {
        checkbox.addEventListener("change", function () {
            // Track the number of checked checkboxes
            const checkedCheckboxes = document.querySelectorAll(".checkbox:checked").length;

            // Update the selected count display
            document.getElementById("selectedCount").textContent = `${checkedCheckboxes} / ${totalCheckboxes}`;
        });
    });

    // Initial update of selected count
    const initialCheckedCheckboxes = document.querySelectorAll(".checkbox:checked").length;
    document.getElementById("selectedCount").textContent = `${initialCheckedCheckboxes} / ${totalCheckboxes}`;
}

// Add selectAll functionality
document.getElementById("selectAllBtn").addEventListener("click", function () {
    // Get all checkboxes within the container
    const checkboxes = document.querySelectorAll(".checkbox");

    // Loop through all checkboxes and set the "checked" property to true
    checkboxes.forEach(checkbox => {
        checkbox.checked = true;  // Check each checkbox
    });

    // Update the selected count
    const checkedCheckboxes = document.querySelectorAll(".checkbox:checked").length;
    const totalCheckboxes = Appliers.length;
    document.getElementById("selectedCount").textContent = `${checkedCheckboxes} / ${totalCheckboxes}`;
});

document.getElementById("picture").addEventListener("click", function () {
    window.location.href = `/Profile/Index?id=${selectModel.ownerId}`;
})


// Assuming you have a container with the id "container" in your HTML
const container = document.getElementById("container");
renderApplier(container);