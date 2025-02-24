﻿const activity = JSON.parse(JSON.stringify(viewActivityModel)).card;
const announces = JSON.parse(JSON.stringify(viewActivityModel)).announcements;
const username = JSON.parse(JSON.stringify(viewActivityModel)).userName;
const is_apply_on = JSON.parse(JSON.stringify(viewActivityModel)).isApplyOn;
const is_participate = JSON.parse(JSON.stringify(viewActivityModel)).participationStatus;
const out_of_date = JSON.parse(JSON.stringify(viewActivityModel)).outOfDate;

console.log(username);
console.log(activity);
console.log(announces);

console.log("Is Participate: " + is_participate);
console.log("Is Apply On: " + is_apply_on);
console.log("Out of Date: " + out_of_date);

const img_people_src = "../img/people.png";
const img_clock_src = "../img/clock.png";
const img_location_src = "../img/location-pin.png";
const img_delete_src = "../img/x.png";
    
// activity
const parti_width = document.querySelector(".participants").offsetWidth;
const fyi_text = document.getElementsByClassName("fyi-text")[0];
const parti_num = document.getElementById("parti-num");
const parti_bt = document.getElementById("parti-bt");

fyi_text.style.width = `${parti_width}px`;

if (activity.applyCount < activity.applyMax && out_of_date) {
    parti_num.style.color = "#ACACAC";
}
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
        window.location.href = `/EditActivity/Index?id=${activity.activityId}`;
    });
}

function view_participants() {
    parti_bt.textContent = "view";

    parti_bt.addEventListener("click", function(ev) {
        window.location.href = `/Select/Index?id=${activity.activityId}`;
    });
}

render_announcement();

document.addEventListener("DOMContentLoaded", function () {
    const login_popup = document.getElementById("loginPopup");
    
    // owner user
    if (username === activity.owner) {
        edit_activity();
        view_participants();
        add_new_announce();    
    }

    // others user
    if (username != activity.owner && username != null) {

        if (is_participate === "Participating") {
            parti_bt.textContent = "accepted";
            parti_bt.className = "participants-bt-2";
        }
        if (!is_participate === "Not Participating") {
            parti_bt.textContent = "rejected";
            parti_bt.className = "participants-bt-2";
        }
        if (!is_apply_on && out_of_date) {
            parti_bt.textContent = "closed";
            parti_bt.className = "participants-bt-2";
        }
        if (!is_apply_on && !out_of_date) {
            parti_bt.textContent = "apply";

            parti_bt.addEventListener("click", async function(ev) {
                ev.preventDefault();

                try {
                    const response = await fetch(`/ViewActivity/ApplyOn?activity_id=${activity.activityId}`, {
                        method: "POST",
                        headers: { "Content-Type": "application/json" }
                    });
        
                    const result = await response.json();
                    if (result.success) {
                        console.log("ApplyOn created successfully!");
                        window.location.reload();
                    } else {
                        alert(result.message);
                    }
                } catch (error) {
                    console.error("Error creating ApplyOn:", error);
                }
            });
        }
        if (is_apply_on && !out_of_date) {
            parti_bt.textContent = "cancel";
            parti_bt.style.backgroundColor = "#D1D1D1";

            parti_bt.addEventListener("click", async function(ev) {
                ev.preventDefault();

                try {
                    const response = await fetch(`/ViewActivity/Withdraw?activity_id=${activity.activityId}`, {
                        method: "POST",
                        headers: { "Content-Type": "application/json" }
                    });
        
                    const result = await response.json();
                    if (result.success) {
                        console.log("Cancel successfully!");
                        window.location.reload();
                    } else {
                        alert(result.message);
                    }
                } catch (error) {
                    console.error("Error Withdrawing:", error);
                }
            });
        }
    }
    // not logged in
    if (username == null) {
        if (!is_apply_on && out_of_date) {
            parti_bt.textContent = "closed";
            parti_bt.className = "participants-bt-2";
        }
        if (!is_apply_on && !out_of_date) {
            parti_bt.textContent = "apply";
            parti_bt.addEventListener("click", function() {
                login_popup.style.display = "block";
            });
        }
    }

    const elements = document.querySelectorAll("p, span, div, h1, h2, h3, h4, h5, h6, header");
    
    elements.forEach(el => {
        if (/[ก-๙]/.test(el.textContent)) { 
            el.style.fontFamily = '"Noto Sans Thai", serif';
        }
    });
});