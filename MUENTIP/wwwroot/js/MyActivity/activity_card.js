const createdActivity = JSON.parse(JSON.stringify(activityModel)).createdActivity;
const approvedActivity = JSON.parse(JSON.stringify(activityModel)).approvedActivity;
const nonApproveActivity = JSON.parse(JSON.stringify(activityModel)).nonApproveActivity;
const tags = JSON.parse(JSON.stringify(activityModel)).filterTags;

document.addEventListener("DOMContentLoaded", function () {
    // ดึงปุ่มทั้งหมดจากเมนู
    const buttons = document.querySelectorAll(".activity-menu li a");
    const createContainer = document.querySelector(".create-container");
    const container = document.getElementById("container");

    const img_people_src = "/img/people.png";
    const img_clock_src = "/img/clock.png";
    const img_calendar_src = "/img/calendar.png";
    const img_location_src = "/img/location-pin.png";
    const img_arrow_src = "/img/right-arrow.png";

    // กำหนดค่าเริ่มต้นของ Active Tab จาก localStorage
    const savedTab = localStorage.getItem("activeTab") || "createButton";
    setActiveTab(savedTab);
    console.log('savedTab', savedTab);

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

    function setActiveTab(selectedButtonId) {
        buttons.forEach(button => button.classList.remove("active"));

        const selectedButton = document.getElementById(selectedButtonId);
        if (selectedButton) {
            selectedButton.classList.add("active");
            localStorage.setItem("activeTab", selectedButtonId); // บันทึกค่า activeTab
        }

        // แสดง/ซ่อน create-container ตามปุ่มที่เลือก
        createContainer.style.display = selectedButtonId === "createButton" ? "flex" : "none";

        // เรียกใช้ฟังก์ชัน renderActivities ใหม่หลังจากเปลี่ยนแท็บ
        renderActivities(container, tags, selectedButtonId);
    }

    document.getElementById("create-button")?.addEventListener("click", function () {
        window.location.href = ViewActivityURL;
    });

    function renderActivities(container, ShowedtagList, tab) {
        container.innerHTML = "";

        let activities = [];

        // ตรวจสอบค่า tab ที่ได้รับ และเลือกกิจกรรมที่จะแสดง
        if (tab === "createButton") {
            activities = createdActivity;
        }
        else if (tab === "approvedButton") {
            activities = approvedActivity;
        }
        else if (tab === "non-approvedButton") {
            activities = nonApproveActivity;
        }

        console.log('activities', activities);

        // กรองข้อมูลตาม Tag ที่เลือก
        const filteredActivities = activities.filter(activity =>
            activity.tagsList.some(tag => ShowedtagList.some(showedTag => showedTag.tagName === tag))
        );

        const activitiesToShow = filteredActivities;
        console.log('activitiesToShow', activitiesToShow);

        if (!activitiesToShow || activitiesToShow.length === 0) {
            container.innerHTML = "<p>No activities found</p>";
            return;
        }

        activitiesToShow.forEach(activity => {
            const activity_card = document.createElement("div");
            activity_card.className = "activity_card";

            const left_group = document.createElement("div");
            left_group.className = "left_group";

            const left_content_text = document.createElement("div");
            left_content_text.className = "left_content_text";

            const title = document.createElement("div");
            title.className = "activity_title";
            title.textContent = activity.title;

            const owner = document.createElement("div");
            const img_people = document.createElement("img");
            const owner_text = document.createElement("span");
            owner.className = "activity_detail";
            img_people.className = "icon";
            img_people.src = img_people_src;
            owner.appendChild(img_people);
            owner_text.textContent = activity.owner;
            owner.appendChild(owner_text);

            const location = document.createElement("div");
            const img_location = document.createElement("img");
            const location_text = document.createElement("span");
            location.className = "activity_detail";
            img_location.className = "icon";
            img_location.src = img_location_src;
            location.appendChild(img_location);
            location_text.textContent = activity.location;
            location.appendChild(location_text);

            const activity_date_time = document.createElement("div");
            const img_activity_date_time = document.createElement("img");
            const activity_date_time_text = document.createElement("span");
            activity_date_time.className = "activity_detail";
            img_activity_date_time.className = "icon";
            img_activity_date_time.src = img_clock_src;
            activity_date_time.appendChild(img_activity_date_time);
            let date_obj = new Date(activity.activityDateTime.replace(' ', 'T'));
            let current_date = new Date();
            let date = activity.activityDateTime.split(' ')[0];
            let time = date_obj.toTimeString().split(" ")[0].split(":").slice(0, 2).join(":");
            activity_date_time_text.textContent = `${date} ${time}`;
            activity_date_time.appendChild(activity_date_time_text);

            const deadline_date_time = document.createElement("div");
            const img_deadline_date_time = document.createElement("img");
            const deadline_date_time_text = document.createElement("span");
            deadline_date_time.className = "activity_detail";
            img_deadline_date_time.className = "icon";
            img_deadline_date_time.src = img_calendar_src;
            deadline_date_time.appendChild(img_deadline_date_time);
            date_obj = new Date(activity.deadlineDateTime.replace(' ', 'T'));
            if (date_obj > current_date) {
                deadline_date_time.style.color = "#57C543";
                date = date_obj.toISOString().split("T")[0];
                time = date_obj.toTimeString().split(" ")[0].split(":").slice(0, 2).join(":");
                deadline_date_time_text.textContent = `open till ${date} ${time}`;
            } else {
                deadline_date_time.style.color = "#FF0000";
                deadline_date_time_text.textContent = "Closed";
            }
            deadline_date_time.appendChild(deadline_date_time_text);

            left_content_text.appendChild(title);
            left_content_text.appendChild(owner);
            left_content_text.appendChild(location);
            left_content_text.appendChild(activity_date_time);
            left_content_text.appendChild(deadline_date_time);

            const participant = document.createElement("div");
            const img_participant = document.createElement("img");
            const participant_text = document.createElement("span");
            participant.className = "activity_participant";
            img_participant.src = img_people_src;
            participant_text.textContent = activity.applyCount + "/" + activity.applyMax;
            if (date_obj <= current_date) {
                participant_text.style.color = "#ACACAC";
            } else if (activity.applyCount < activity.applyMax) {
                participant_text.style.color = "#57C543";
            } else {
                participant_text.style.color = "#FF0000";
            }
            participant.appendChild(img_participant);
            participant.appendChild(participant_text);

            left_group.appendChild(left_content_text);
            left_group.appendChild(participant);

            const left_group_and_tags = document.createElement("div");
            left_group_and_tags.className = "left_group_and_tags";
            const tags = document.createElement("div");
            tags.className = "tags_group";
            activity.tagsList.forEach(tag => {
                const tag_element = document.createElement("span");
                tag_element.className = "activity_tag";
                tag_element.textContent = tag;
                tags.appendChild(tag_element);
            });

            left_group_and_tags.appendChild(left_group);
            left_group_and_tags.appendChild(tags);

            activity_card.appendChild(left_group_and_tags);

            const enter_button = document.createElement("button");
            const img_arrow = document.createElement("img");
            img_arrow.src = img_arrow_src;
            enter_button.className = "enter_button";
            enter_button.appendChild(img_arrow);

            enter_button.onclick = function () {
                window.location.href = ViewActivityURL;
            };

            activity_card.appendChild(enter_button);
            container.appendChild(activity_card);
        });
    }

});
