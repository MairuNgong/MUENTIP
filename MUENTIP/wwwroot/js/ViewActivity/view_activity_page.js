const activity = JSON.parse(JSON.stringify(viewActivityModel)).card;
const announces = JSON.parse(JSON.stringify(viewActivityModel)).announcements;

console.log(announces);

const img_people_src = "../img/people.png";
const img_clock_src = "../img/clock.png";
const img_location_src = "../img/location-pin.png";

document.addEventListener("DOMContentLoaded", function() {
    
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

    // render_announcement
    const act_announce_div = document.getElementsByClassName("activity-announce")[0];
    if(announces.length == 0) {
        const announce_div = document.createElement("div");
        const content = document.createElement("p");
        announce_div.className = "announce-div";
        content.textContent = "No announcements available for this activity.";
        announce_div.appendChild(content);
        act_announce_div.appendChild(announce_div);
    } else {
        announces.forEach(announce => {
            const announce_div = document.createElement("div");
            const date = document.createElement("p");
            const content = document.createElement("p");
            announce_div.className = "announce-div";
            date.textContent = announce.announceDate;
            content.textContent = announce.content;
            announce_div.appendChild(date);
            announce_div.appendChild(content);
            act_announce_div.appendChild(announce_div);
        });
    }

});

