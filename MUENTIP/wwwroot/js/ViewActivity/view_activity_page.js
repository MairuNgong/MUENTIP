const activity = JSON.parse(JSON.stringify(viewActivityModel)).card;

console.log(activity);

const img_people_src = "../img/people.png";
const img_clock_src = "../img/clock.png";
const img_location_src = "../img/location-pin.png";

document.addEventListener("DOMContentLoaded", function() {
    const parti_width = document.querySelector(".participants").offsetWidth;
    const fyi_text = document.getElementsByClassName("fyi-text")[0];
    const parti_num = document.getElementById("parti-num");
    
    fyi_text.style.width = `${parti_width}px`;

    if (activity.applyCount > activity.applyMax) {
        fyi_text.style.display = "block";
        parti_num.style.color = "#FF0000";
    }
    
});

