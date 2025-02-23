const activity = JSON.parse(JSON.stringify(viewActivityModel)).card;
const announces = JSON.parse(JSON.stringify(viewActivityModel)).announcements;
const username = JSON.parse(JSON.stringify(viewActivityModel)).userName;

console.log(username);
console.log(activity);
console.log(announces);

const img_people_src = "../img/people.png";
const img_clock_src = "../img/clock.png";
const img_location_src = "../img/location-pin.png";
const img_delete_src = "../img/x.png";
    
// activity
const parti_width = document.querySelector(".participants").offsetWidth;
const fyi_text = document.getElementsByClassName("fyi-text")[0];
const parti_num = document.getElementById("parti-num");

fyi_text.style.width = `${parti_width}px`;

if (activity.applyCount > activity.applyMax) {
    fyi_text.style.display = "block";
    parti_num.style.color = "#FF0000";
}

if (!activity.description) {
    const detail_p = document.getElementById("descript");

    detail_p.textContent = "This activity has no details at the moment.";
    detail_p.style.color = "grey";
}

function render_announcement() {
    const act_announce_div = document.getElementsByClassName("activity-announce")[0];
    if(announces.length == 0) {
        const announce_div = document.createElement("div");
        const content = document.createElement("p");
    
        announce_div.className = "announce-div";
        content.textContent = "No announcements available for this activity.";
        content.style.color = "grey";
    
        announce_div.appendChild(content);
        act_announce_div.appendChild(announce_div);
    } else {
        announces.forEach(announce => {
            const announce_div = document.createElement("div");
            const date = document.createElement("p");
            const content = document.createElement("p");
    
            if(username == activity.owner) {
                const delete_img = document.createElement("img");
                const delete_bt = document.createElement("button");
    
                announce_div.style.width = "calc(100% - 120px)";
                delete_bt.id = "delete-announce-bt";
                delete_bt.type = "button";
                delete_img.src = img_delete_src;
                delete_bt.appendChild(delete_img);
                announce_div.appendChild(delete_bt);
    
                delete_bt.addEventListener("click", async function (ev) {
                    ev.preventDefault();
    
                    if (!confirm("Are you sure you want to delete this announcement?")) return;
    
                    try 
                    {
                        const response = await fetch(`/ViewActivity/DeleteAnnounce?annoucement_id=${announce.announcementId}`, {
                            method: "POST",
                            headers: { "Content-Type": "application/json" }
                        });
    
                        const result = await response.json();
                        if (result.success) {
                            announce_div.remove();
                        } else {
                            alert("Error: " + result.message);
                        }
                    } 
                    catch (error) 
                    {
                        console.error("Error deleting announcement:", error);
                    }
                });
            }
    
            announce_div.className = "announce-div";
            date.textContent = `${announce.announceDate.split("T")[0]} ${announce.announceDate.split("T")[1].slice(0,5)}`;
            content.textContent = announce.content;
    
            announce_div.appendChild(date);
            announce_div.appendChild(content);
            act_announce_div.appendChild(announce_div);
        });
    }
}

function expandTextarea(textarea) {
    textarea.style.height = "auto"; 
    textarea.style.height = `${textarea.scrollHeight - 30}px`;
}

function add_new_announce() {
    const add_new_annunce_form = document.getElementById("add-new-announce");

    add_new_annunce_form.addEventListener("submit", async function (event) {
        event.preventDefault();
        
        const contentTextArea = this.querySelector("textarea");
        const contentValue = contentTextArea.value.trim();

        const formData = new URLSearchParams();
        formData.append("ActivityId", activity.activityId);
        formData.append("Content", contentValue);

        try {
            const response = await fetch("/ViewActivity/CreateAnnounce", {
                method: "POST",
                body: formData,
                headers: { "Content-Type": "application/x-www-form-urlencoded" }
            });

            const result = await response.json();
            if (result.success) {
                console.log("Announcement created successfully!");
                window.location.reload();
            } else {
                alert(result.message);
            }
        } catch (error) {
            console.error("Error submitting announcement:", error);
        }
    });
}

function edit_activity() {
    const edit_act_bt = document.getElementById("edit-act-bt");

    edit_act_bt.addEventListener("click", function(ev) {
        window.location.href = "/";
    });
}

function view_participants() {
    const view_parti_bt = document.getElementById("parti-bt");

    edit_act_bt.addEventListener("click", function(ev) {
        window.location.href = "/";
    });
}

render_announcement();

// owner user
if (username === activity.owner) {
    // edit_activity();
    // view_participants();
    add_new_announce();    
}

// others user
if (username != activity.owner && username != null) {
    // participant bt Apply On
}

document.addEventListener("DOMContentLoaded", function () {
    const elements = document.querySelectorAll("p, span, div, h1, h2, h3, h4, h5, h6, header");

    elements.forEach(el => {
        if (/[ก-๙]/.test(el.textContent)) { 
            el.style.fontFamily = '"Noto Sans Thai", serif';
        }
    });
});