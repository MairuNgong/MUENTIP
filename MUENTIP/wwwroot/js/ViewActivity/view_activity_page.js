const activity = JSON.parse(JSON.stringify(viewActivityModel)).card;

const img_people_src = "../img/people.png";
const img_clock_src = "../img/clock.png";
const img_location_src = "../img/location-pin.png";

const parti_width = document.querySelector("#participants").offsetWidth;
const fyi_text = document.getElementsByClassName("fyi-text")[0];
fyi_text.style.width = "10px";
fyi_text.style.color = "green";