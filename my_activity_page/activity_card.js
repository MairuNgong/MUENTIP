//ดูการใช้งานเริ่มที่บรรทัด 152 โดยก่อนหน้านั้นเป็นแค่ของชั่วคราวเพราะเดี๋ยวตอนจริงจะ fetch
//เอาไปใช้โดยการ duplicate fileนี้ไปใส่folderหน้าของเราอีกที จะได้ใช้ของใตรของมัน แล้ว include scriptไว้ใต้body แบบในตัวอย่างที่ให้(component ต้องถูกโหลดก่อนจึงควรไว้ก่อนscriptอื่น)จากนั้นก็แก้ไขไฟล์นี้ตรงการเรียกใช้function parameter คือ containerที่จะใส่,tag list สำหรับการfilter หรือหน้าแรกที่จัดเป็นหมู่ๆ, จำนวนโดยถ้าไม่ใส่จะสร้างหมด
//search ไปเพิ่ม parameter เอาเอง แล้วก็ไปfilter เพิ่มใน functionเอา
//แก้ข้อมูลobjectให้ตรงที่ต้องการได้เลย ใส่เพิ่มก็ได้
 
class User{
  constructor(username){
    this.username = username
  }
}

class Tag{
  constructor(tagName){
    this.name = tagName
  }
}

class ActivityCard {
  constructor(
    title,
    owner,
    location,
    activity_date_time,
    deadline_date_time,
    tags_list,
    apply_count,
    apply_max
  ) {
    this.title = title; // Title of the activity
    this.owner = owner.username; // Owner or organizer of the activity
    this.location = location; // Location of the activity
    this.activity_date_time = activity_date_time; // Date and time of the activity
    this.deadline_date_time = deadline_date_time; // Deadline to apply
    this.tags_list = tags_list; // List of tags associated with the activity
    this.apply_count = apply_count; // Current number of applications
    this.apply_max = apply_max; // Maximum allowed applications
  }
}

// Create Tags
const tag1 = new Tag("Fitness");
const tag2 = new Tag("Wellness");
const tag3 = new Tag("Technology");
const tag4 = new Tag("Education");
const tag5 = new Tag("Recreation");

// Create Users
const user1 = new User("Alice");
const user2 = new User("Bob");
const user3 = new User("Charlie");

// Create Activities
const activities = [
  new ActivityCard(
    "Yoga Class",
    user1,
    "Community Center",
    "2025-02-10T10:00:00",
    "2025-02-08T23:59:59",
    [tag1, tag2],
    15,
    20
  ),
  new ActivityCard(
    "Tech Meetup",
    user2,
    "Tech Hub",
    "2025-02-15T14:00:00",
    "2024-02-14T12:00:00",
    [tag3, tag4],
    10,
    50
  ),
  new ActivityCard(
    "Painting Workshop",
    user3,
    "Art Studio",
    "2025-03-01T11:00:00",
    "2025-02-28T18:00:00",
    [tag4, tag5],
    8,
    25
  ),
  new ActivityCard(
    "Coding Bootcamp",
    user1,
    "Innovation Lab",
    "2025-02-20T09:00:00",
    "2025-02-18T20:00:00",
    [tag3, tag4],
    30,
    30
  ),
  new ActivityCard(
    "Hiking Trip",
    user2,
    "Mountain Trail",
    "2025-03-05T06:00:00",
    "2025-03-01T23:59:59",
    [tag1, tag5,tag2,tag3,tag4],
    18,
    15
  ),
  new ActivityCard(
    "Meditation Session",
    user3,
    "Wellness Center",
    "2025-02-18T17:00:00",
    "2025-02-17T12:00:00",
    [tag2],
    20,
    50
  ),
  new ActivityCard(
    "Robotics Workshop",
    user2,
    "Tech Academy",
    "2025-02-22T10:00:00",
    "2025-02-20T18:00:00",
    [tag3,tag2,tag5],
    18,
    25
  ),
  new ActivityCard(
    "Cooking Class",
    user1,
    "Culinary School",
    "2025-03-10T15:00:00",
    "2025-01-09T12:00:00",
    [tag4, tag5],
    22,
    30
  ),
  new ActivityCard(
    "Fitness Bootcamp",
    user3,
    "Outdoor Gym",
    "2025-03-15T07:00:00",
    "2025-03-13T20:00:00",
    [tag1, tag2],
    35,
    40
  ),
  new ActivityCard(
    "Gaming Tournament",
    user2,
    "Esports Arena",
    "2025-03-25T18:00:00",
    "2025-03-20T23:59:59",
    [tag5],
    50,
    100
  ),
];

const img_people_src = "../img/people.png";
const img_clock_src = "../img/clock.png";
const img_calendar_src = "../img/calendar.png";
const img_location_src = "../img/location-pin.png";
const img_arrow_src = "../img/right-arrow.png";

function renderActivities(container, tagList, maxActivities = Infinity) {
  // Remove all child elements from the container to overwrite it
  container.innerHTML = "";

  const filteredActivities = activities.filter(activity => 
    activity.tags_list.some(tag => 
      tagList.some(tagObj => tagObj.name === tag.name)
    )
  );
  // Limit to the specified maximum number of activities (or all if maxActivities is Infinity)
  const activitiesToShow = filteredActivities.slice(0, maxActivities);

  // Iterate over the selected activities
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
    img_location.src = img_location_src; // Assign the appropriate source for the location icon
    location.appendChild(img_location);
    location_text.textContent = activity.location;
    location.appendChild(location_text);

    const activity_date_time = document.createElement("div");
    const img_activity_date_time = document.createElement("img");
    const activity_date_time_text = document.createElement("span");
    activity_date_time.className = "activity_detail";
    img_activity_date_time.className = "icon";
    img_activity_date_time.src = img_clock_src; // Assign the appropriate source for the activity date time icon
    activity_date_time.appendChild(img_activity_date_time);
    let date_obj = new Date(activity.activity_date_time);
    let current_date = new Date();
    let date = date_obj.toISOString().split("T")[0];
    let time = date_obj.toTimeString().split(" ")[0].split(":").slice(0, 2).join(":");
    activity_date_time_text.textContent = `${date} ${time}`;
    activity_date_time.appendChild(activity_date_time_text);

    const deadline_date_time = document.createElement("div");
    const img_deadline_date_time = document.createElement("img");
    const deadline_date_time_text = document.createElement("span");

    deadline_date_time.className = "activity_detail";
    img_deadline_date_time.className = "icon";
    img_deadline_date_time.src = img_calendar_src; // Assign the appropriate source for the deadline date time icon
    deadline_date_time.appendChild(img_deadline_date_time);
    date_obj = new Date(activity.deadline_date_time);
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
    img_participant.src = img_people_src; // Assign the appropriate source for the participant icon
    participant_text.textContent = activity.apply_count + "/" + activity.apply_max;
    if (date_obj <= current_date) {
      participant_text.style.color = "#ACACAC";
    } else if (activity.apply_count < activity.apply_max) {
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
    activity.tags_list.forEach(tag => {
      const tag_element = document.createElement("span");
      tag_element.className = "activity_tag";
      tag_element.textContent = tag.name;
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
      window.location.href = "../view_activity_page/view_activity_page.html";
    };

    activity_card.appendChild(enter_button);
    container.appendChild(activity_card);
  });
}
const container = document.getElementById("container");
const tags = [tag1,tag2,tag3,tag4,tag5];
renderActivities(container, tags);
